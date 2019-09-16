<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDeclaresOT.ascx.vb"
    Inherits="Attendance.ctrlDeclaresOT" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="35px">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="80px">
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
                                ToolTip="">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgDeclaresOT" runat="server"  Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID" ClientDataKeyNames="ID,EMPLOYEE_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn DataField="ID,EMPLOYEE_ID" Visible="false" />
                                    <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                        UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" HeaderStyle-Width="130px" ItemStyle-Width="130px" />--%>
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                         HeaderStyle-Width="80px" ItemStyle-Width="80px" UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULLNAME" UniqueName="FULLNAME"
                                         HeaderStyle-Width="140px" ItemStyle-Width="140px" SortExpression="FULLNAME" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="DEPARTMENT_NAME"
                                         HeaderStyle-Width="200px" ItemStyle-Width="200px" UniqueName="DEPARTMENT_NAME" SortExpression="DEPARTMENT_NAME"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                         HeaderStyle-Width="270px" ItemStyle-Width="270px" UniqueName="TITLE_NAME" SortExpression="TITLE_NAME"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày làm thêm %>" DataField="REGIST_DATE"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" UniqueName="REGIST_DATE" SortExpression="REGIST_DATE"
                                        DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ký hiệu ca %>" DataField="SIGN_CODE"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" UniqueName="SIGN_CODE" SortExpression="SIGN_CODE" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Làm thêm buổi sáng từ %>" DataField="FROM_HOUR_AM" UniqueName="FROM_HOUR_AM"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="FROM_HOUR_AM" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Làm thêm buổi sáng đến %>" DataField="TO_HOUR_AM" UniqueName="TO_HOUR_AM"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="TO_HOUR_AM" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Làm thêm buổi chiều từ %>" DataField="FROM_HOUR_PM" UniqueName="FROM_HOUR_PM"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="FROM_HOUR_PM" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Làm thêm buổi chiều đến %>" DataField="TO_HOUR_PM" UniqueName="TO_HOUR_PM"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="TO_HOUR_PM" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số giờ làm thêm %>" DataField="TOTAL_OT"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" UniqueName="TOTAL_OT" SortExpression="TOTAL_OT" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: 100% %>" DataField="OT_100" UniqueName="OT_100"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="OT_100" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: 150% %>" DataField="OT_150" UniqueName="OT_150"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="OT_150" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: 200% %>" DataField="OT_200" UniqueName="OT_200"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="OT_200" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: 210% %>" DataField="OT_210" UniqueName="OT_210"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="OT_210" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: 270% %>" DataField="OT_270" UniqueName="OT_270"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="OT_270" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: 300% %>" DataField="OT_300" UniqueName="OT_300"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="OT_300" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: 390% %>" DataField="OT_370" UniqueName="OT_370"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="OT_370" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Lí do làm thêm %>" DataField="NOTE"
                                         HeaderStyle-Width="200px" ItemStyle-Width="200px" UniqueName="NOTE" SortExpression="NOTE" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Người cập nhật %>" DataField="MODIFIED_NAME"
                                         HeaderStyle-Width="140px" ItemStyle-Width="140px" UniqueName="MODIFIED_NAME" SortExpression="MODIFIED_NAME" />
                                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cập nhật %>" DataField="MODIFIED_DATE"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" UniqueName="MODIFIED_DATE" SortExpression="MODIFIED_DATE"
                                        DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                                    <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do không duyệt %>" DataField="REASON"
                                         HeaderStyle-Width="200px" ItemStyle-Width="200px" UniqueName="REASON" SortExpression="REASON" />--%>
                        </Columns>
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
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        var splitterID = 'ctl00_MainContent_ctrlDeclaresOT_RadSplitter3';
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
            window.open('/Default.aspx?mid=Attendance&fid=ctrlDeclaresOTNewEdit&group=Business&FormType=0&periodid=' + periodID, "_self"); /*
            oWindow.setSize(900, 600);
            oWindow.center(); */
        }
        function OpenEditWindow() {
            var grid = $find('<%# rgDeclaresOT.ClientID %>');
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            var id = 0
            var empID = 0
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            if (gridSelected != "") {
                id = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                empID = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
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
                window.open('/Default.aspx?mid=Attendance&fid=ctrlDeclaresOTNewEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id + '&periodid=' + periodID + '&employeeID=' + empID, "_self"); /*
                oWindow.setSize(900, 600);
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
                $find("<%= rgDeclaresOT.ClientID %>").get_masterTableView().rebind();
            }
        }
        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgDeclaresOT.ClientID %>').get_masterTableView().get_selectedItems().length;
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
            } else if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgDeclaresOT.ClientID %>");
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
    </script>
</tlk:RadCodeBlock>

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
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="110px">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdtungay" MaxLength="12" runat="server"
                                ToolTip="">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdtungay"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Từ ngày %>" ToolTip="<%$ Translate: Bạn phải chọn Từ ngày %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDenngay" MaxLength="12" runat="server"
                                ToolTip="" Width="150px">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdDenngay"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Đến ngày %>" ToolTip="<%$ Translate: Bạn phải chọn Đến ngày %>"> </asp:RequiredFieldValidator>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Nhân viên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmployee" runat="server"></tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Loại đăng ký")%>
                        </td>
                        <td>
                             <tlk:RadComboBox runat="server" ID="cboMANUAL_ID">
                            </tlk:RadComboBox>
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Nhân viên nghỉ việc %>" />
                        </td>
                        
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                     <tr>
                        <td class="lb">
                            <%# Translate("Lý do Hoàn duyệt")%>
                        </td>
                         <td colspan ="3">
                            <tlk:RadTextBox ID="txtReason" runat="server" Width="100%"></tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgRegisterLeave" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <ClientEvents OnCommand="ValidateFilter" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="4" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_CODE,IS_APP,STATUS">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="VN_FULLNAME"
                                SortExpression="VN_FULLNAME" UniqueName="VN_FULLNAME" HeaderStyle-Width="120px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh công việc %>" HeaderStyle-Width="120px"
                                DataField="TITLE_NAME" SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" HeaderStyle-Width="200px"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại nghỉ %>" DataField="MANUAL_NAME" HeaderStyle-Width="200px"
                                SortExpression="MANUAL_NAME" UniqueName="MANUAL_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Nghỉ từ ngày %>" DataField="LEAVE_FROM"
                                UniqueName="LEAVE_FROM" DataFormatString="{0:dd/MM/yyyy}" SortExpression="LEAVE_FROM">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ buổi %>" DataField="FROM_SESSION_NAME" HeaderStyle-Width="100px"
                                SortExpression="FROM_SESSION_NAME" UniqueName="FROM_SESSION_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Nghỉ đến ngày %>" DataField="LEAVE_TO"
                                UniqueName="LEAVE_TO" DataFormatString="{0:dd/MM/yyyy}" SortExpression="LEAVE_TO">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến buổi %>" DataField="TO_SESSION_NAME" HeaderStyle-Width="100px"
                                SortExpression="TO_SESSION_NAME" UniqueName="TO_SESSION_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày %>" DataField="DAY_NUM"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="DAY_NUM"
                                UniqueName="DAY_NUM">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Các ngày nghỉ trong đơn %>" DataField="DAY_LIST" HeaderStyle-Width="200px"
                                SortExpression="DAY_LIST" UniqueName="DAY_LIST" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do nghỉ %>" DataField="REASON_LEAVE_NAME" HeaderStyle-Width="200px"
                                SortExpression="REASON_LEAVE_NAME" UniqueName="REASON_LEAVE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do nghỉ chi tiết %>" DataField="NOTE" HeaderStyle-Width="200px"
                                SortExpression="NOTE" UniqueName="NOTE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME" HeaderStyle-Width="200px"
                                SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người tạo đơn %>" DataField="CREATED_BY_EMP_NAME"
                                UniqueName="CREATED_BY_EMP_NAME" SortExpression="CREATED_BY_EMP_NAME" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày tạo đơn %>" DataField="CREATED_DATE"
                                UniqueName="CREATED_DATE" SortExpression="CREATED_DATE" ItemStyle-Width="100px"
                                DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="100px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người cập nhật cuối cùng %>" DataField="MODIFIED_BY_EMP_NAME"
                                UniqueName="MODIFIED_BY_EMP_NAME" SortExpression="MODIFIED_BY_EMP_NAME" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cập nhật cuối cùng %>" DataField="MODIFIED_DATE"
                                UniqueName="MODIFIED_DATE" SortExpression="MODIFIED_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người hoàn duyệt %>" DataField="RESTORED_BY_NAME"
                                UniqueName="RESTORED_BY_NAME" SortExpression="RESTORED_BY_NAME" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hoàn duyệt %>" DataField="RESTORED_DATE"
                                UniqueName="RESTORED_DATE" SortExpression="RESTORED_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do hoàn duyệt %>" DataField="RESTORED_REASON"
                                UniqueName="RESTORED_REASON" SortExpression="RESTORED_REASON" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ IP máy tính %>" DataField="CREATED_LOG"
                                UniqueName="CREATED_LOG" SortExpression="CREATED_LOG" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
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

function OpenEditWindow(states) {
    var grid = $find('<%# rgRegisterLeave.ClientID %>');
    var gridSelected = grid.get_masterTableView().get_selectedItems();
    var id = 0
    var gridSelected = grid.get_masterTableView().get_selectedItems();
    if (gridSelected != "") {
        id = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
    }
    if (id > 0) {
        window.open('/Default.aspx?mid=Attendance&fid=ctrlRegisterCONewEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id, "_self");
    }
}

function OpenInsertWindow() {
    //var oWindow = radopen('Dialog.aspx?mid=Attendance&fid=ctrlRegisterCONewEdit&group=Business&FormType=0', "rwPopup");
    //var pos = $("html").offset();
    //oWindow.moveTo(pos.left, pos.top);
    //oWindow.setSize($(window).width(), $(window).height());
    window.open('/Default.aspx?mid=Attendance&fid=ctrlRegisterCONewEdit&group=Business&FormType=0', "_self");
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

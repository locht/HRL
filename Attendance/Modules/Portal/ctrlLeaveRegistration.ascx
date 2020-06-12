<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlLeaveRegistration.ascx.vb"
    Inherits="Attendance.ctrlLeaveRegistration
    " %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidValid" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="80px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboYear" SkinID="dDropdownList" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ công")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboPeriod" SkinID="dDropdownList" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdtungay" MaxLength="12" runat="server" ToolTip="">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdtungay"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Từ ngày %>" ToolTip="<%$ Translate: Bạn phải chọn Từ ngày %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDenngay" MaxLength="12" runat="server" ToolTip="" Width="150px">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdDenngay"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Đến ngày %>" ToolTip="<%$ Translate: Bạn phải chọn Đến ngày %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm kiếm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Trạng thái")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboStatus">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Loại đăng ký")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboManual">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgMain" runat="server" Height="100%" Width="100%" AllowPaging="true"
                    AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID, EMPLOYEE_ID, EMPLOYEE_CODE, VN_FULLNAME, ORG_NAME, LEAVE_FROM, LEAVE_TO, MANUAL_NAME,  NOTE, STATUS, STATUS_NAME, IMPORT"
                        ClientDataKeyNames="ID, EMPLOYEE_ID, EMPLOYEE_CODE, VN_FULLNAME,LEAVE_FROM, LEAVE_TO, MANUAL_NAME, NOTE, STATUS, STATUS_NAME, IMPORT">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="VN_FULLNAME"
                                UniqueName="VN_FULLNAME" SortExpression="VN_FULLNAME" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại nghỉ %>" DataField="MANUAL_NAME"
                                UniqueName="MANUAL_NAME" SortExpression="MANUAL_NAME" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Nghỉ từ ngày %>" DataField="LEAVE_FROM"
                                UniqueName="LEAVE_FROM" SortExpression="LEAVE_FROM" DataFormatString="{0:dd/MM/yyyy}"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ buổi %>" DataField="FROM_SESSION_NAME"
                                UniqueName="FROM_SESSION_NAME" SortExpression="FROM_SESSION_NAME" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Thời gian kết thúc nghỉ phép %>"
                                DataField="LEAVE_TO" UniqueName="LEAVE_TO" SortExpression="LEAVE_TO" DataFormatString="{0:dd/MM/yyyy}"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến buổi %>" DataField="TO_SESSION_NAME"
                                UniqueName="TO_SESSION_NAME" SortExpression="TO_SESSION_NAME" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày %>" DataField="DAY_NUM"
                                UniqueName="DAY_NUM" SortExpression="DAY_NUM" ItemStyle-HorizontalAlign="Right"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridNumericColumn>
                            <%--<tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày trong kế hoạch %>" DataField="IN_PLAN_DAYS"
                                UniqueName="IN_PLAN_DAYS" SortExpression="IN_PLAN_DAYS" ItemStyle-HorizontalAlign="Right"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày ngoài kế hoạch %>" DataField="NOT_IN_PLAN_DAYS"
                                UniqueName="NOT_IN_PLAN_DAYS" SortExpression="NOT_IN_PLAN_DAYS" ItemStyle-HorizontalAlign="Right"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridNumericColumn>--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lí do nghỉ phép %>" DataField="REASON_LEAVE_NAME"
                                UniqueName="REASON_LEAVE_NAME" SortExpression="REASON_LEAVE_NAME" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lí do chi tiết %>" DataField="NOTE"
                                UniqueName="NOTE" SortExpression="NOTE" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
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
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do không duyệt %>" DataField="REASON"
                                UniqueName="REASON" SortExpression="REASON" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="500px"
            OnClientClose="OnClientClose" Height="300px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            OnClientBeforeClose="OnClientBeforeClose" Modal="true" ShowContentDuringLoad="false"
            Title="<%$ Translate: Thông tin đăng ký nghỉ %>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var idCtrl = 'ctrlLeaveRegistration';
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientClose(sender, args) {
            var m;
            debugger;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgMain.ClientID %>").get_masterTableView().rebind();
            }
        }

        function OnClientBeforeClose(sender, eventArgs) {
            var arg = eventArgs.get_argument();
            if (!arg) {
                if (!confirm("Bạn có muốn đóng màn hình không?")) {
                    //if cancel is clicked prevent the window from closing
                    eventArgs.set_cancel(true);
                }
            }
        }

        function OpenInsertWindow() {
            var oWindow = radopen('Dialog.aspx?mid=Attendance&fid=ctrlLeaveRegistrationNewEdit&id=0&typeUser=User', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
        }

        function OpenEditWindow(states) {
            var id = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var oWindow = radopen('Dialog.aspx?mid=Attendance&fid=ctrlLeaveRegistrationNewEdit&id=' + id + '&view=TRUE&typeUser=User&idCtrl=' + idCtrl, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "EDIT") {
                var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
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
                }
                else {
                    OpenEditWindow();
                    args.set_cancel(true);
                }
            }
            else if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            else if (args.get_item().get_commandName() == "CREATE") {
                if ($('#<%= hidValid.ClientID %>').val() == "1") {
                    var m = '<%= Translate(CommonMessage.MESSAGE_EXIST_INFOR) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'error' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return;
                }

                OpenInsertWindow();
                args.set_cancel(true);
            }
            else if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            else if (args.get_item().get_commandName() == "SUBMIT") {
                var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {//Check danh sách submit hop le
                    var rg = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems();
                    for (i = 0; i < bCheck; i++) {
                        var id = rg[i].getDataKeyValue('ID');
                        var status = rg[i].getDataKeyValue('STATUS');
                        if (status == 17 || status == 18 || status == 21) {
                            m = '<%# Translate("The action only applies for the records that have status as Saved or Unverified by HR. Please select other record.") %>';
                            var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                            setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                            args.set_cancel(true);
                        }

                    }
                }
            }
            else if (args.get_item().get_commandName() == 'DELETE') {
                bCheck = $find('<%# rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }
            //else if (args.get_item().get_commandName() == 'SENDMAIL') {
            //    window.open('/Default.aspx?mid=Attendance&fid=ctrlTR_ProgramNotify&group=Business', "_self"); 
            //    var pos = $("html").offset();
            //    oWindow.moveTo(pos.left, pos.top);
            //    oWindow.setSize(300, 50);
            //    args.set_cancel(true);
            //}

            else {
                // Nếu nhấn các nút khác thì resize default
            }
        }

        function gridRowDblClick(sender, args) {
            var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }
            else if (bCheck > 1) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }
            else {
                OpenEditWindow();
                args.set_cancel(true);
            }
        }
    </script>
</tlk:RadScriptBlock>

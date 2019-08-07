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
            <tlk:RadPane ID="RadPane4" runat="server" Height="40px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="txtYear" runat="server" SkinID="Number" NumberFormat-GroupSeparator=""
                                ShowSpinButtons="true" MaxLength="4" MinValue="2000" NumberFormat-DecimalDigits="0"
                                Width="60px">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trạng thái")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboStatus">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm kiếm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
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
                    <MasterTableView DataKeyNames="ID, EMPLOYEE_ID, EMPLOYEE_CODE, VN_FULLNAME, ORG_NAME, JOBTITLE, LEAVE_FROM, LEAVE_TO, MANUAL_NAME,  NOTE, STATUS, STATUS_NAME"
                        ClientDataKeyNames="ID, EMPLOYEE_ID, EMPLOYEE_CODE, VN_FULLNAME,LEAVE_FROM, LEAVE_TO, MANUAL_NAME, NOTE, STATUS, STATUS_NAME">
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
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="JOBTITLE"
                                UniqueName="JOBTITLE" SortExpression="JOBTITLE" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại nghỉ %>" DataField="MANUAL_NAME"
                                UniqueName="MANUAL_NAME" SortExpression="MANUAL_NAME" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <%--<tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Ngày làm việc %>" DataField="IS_WORK_DAY" DataType="System.Boolean" FilterControlWidth="30px" 
                               ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center"
                               SortExpression="IS_WORK_DAY" UniqueName="IS_WORK_DAY" >
                               <HeaderStyle Width="30px" /> 
                            </tlk:GridCheckBoxColumn>--%>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Thời gian bắt đầu nghỉ phép %>"
                                DataField="LEAVE_FROM" UniqueName="LEAVE_FROM" SortExpression="LEAVE_FROM" DataFormatString="{0:dd/MM/yyyy}"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Thời gian kết thúc nghỉ phép %>"
                                DataField="LEAVE_TO" UniqueName="LEAVE_TO" SortExpression="LEAVE_TO" DataFormatString="{0:dd/MM/yyyy}"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridDateTimeColumn>
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
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lí do nghỉ phép %>" DataField="NOTE"
                                UniqueName="NOTE" SortExpression="NOTE" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Người cập nhật %>" DataField="MODIFIED_BY"
                                UniqueName="MODIFIED_BY" SortExpression="MODIFIED_BY" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cập nhật %>" DataField="MODIFIED_DATE"
                                UniqueName="MODIFIED_DATE" SortExpression="MODIFIED_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridDateTimeColumn>--%>
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
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
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
                    var id = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID')
                    OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlLeaveRegistrationNewEdit&id=' + id + '&view=TRUE&typeUser=User');
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

                OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlLeaveRegistrationNewEdit&id=0&typeUser=User');
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
                var id = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID')
                OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlLeaveRegistrationNewEdit&id=' + id + '&view=TRUE&typeUser=User');
            }
        }

        function OpenInNewTab(url) {
            window.location.href = url;
        }
    </script>
</tlk:RadCodeBlock>

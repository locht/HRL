<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalAtRegistrationShift.ascx.vb"
    Inherits="Attendance.ctrlPortalAtRegistrationShift
    " %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None" Height="100%">
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
                                <tlk:RadComboBox runat="server" ID="cboYear" SkinID="dDropdownList"  AutoPostBack="true">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Kỳ công")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboPeriod" SkinID="dDropdownList"  AutoPostBack="true">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Từ ngày")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="rdRegDateFrom">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Đến ngày")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="rdRegDateTo">
                                </tlk:RadDatePicker>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="rdRegDateTo"
                                    ControlToCompare="rdRegDateFrom" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Đến ngày phải lớn hơn Từ ngày %>"
                                    ToolTip="<%$ Translate: Đến ngày phải lớn hơn Từ ngày %>"></asp:CompareValidator>
                            </td>
                            
                            <td>
                                <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm kiếm %>" SkinID="ButtonFind">
                                </tlk:RadButton>
                            </td>
                        </tr>
                    </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgMain" runat="server" Height="100%" Width="100%" AllowPaging="true" AllowMultiRowSelection="true">
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="true" />
                                <ClientEvents OnRowDblClick="gridRowDblClick" />
                            </ClientSettings>
                            <MasterTableView DataKeyNames="ID, EMPLOYEE_ID" ClientDataKeyNames="ID, EMPLOYEE_ID">
                                <Columns>
                                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                         HeaderStyle-Width="80px" ItemStyle-Width="80px" UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME"
                                         HeaderStyle-Width="170px" ItemStyle-Width="170px" SortExpression="EMPLOYEE_NAME" />
                                    
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                         HeaderStyle-Width="270px" ItemStyle-Width="270px" UniqueName="TITLE_NAME" SortExpression="TITLE_NAME"
                                        ItemStyle-HorizontalAlign="Center" />

                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                         HeaderStyle-Width="200px" ItemStyle-Width="200px" UniqueName="ORG_NAME" SortExpression="ORG_NAME"
                                        ItemStyle-HorizontalAlign="Center" />

                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca làm việc %>" DataField="SHIFT_CODE"
                                         HeaderStyle-Width="100px" ItemStyle-Width="150px" UniqueName="SHIFT_CODE" SortExpression="SHIFT_CODE" ItemStyle-HorizontalAlign="Center" />
                                    
                                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="DATE_FROM"
                                         HeaderStyle-Width="150px" ItemStyle-Width="150px" UniqueName="DATE_FROM" SortExpression="DATE_FROM"
                                        DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                                    
                                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="DATE_TO"
                                         HeaderStyle-Width="150px" ItemStyle-Width="150px" UniqueName="DATE_TO" SortExpression="DATE_TO"
                                        DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />

                                    <tlk:GridBoundColumn HeaderText="Lý do" DataField="REASON"
                                         HeaderStyle-Width="200px" ItemStyle-Width="200px" UniqueName="REASON" SortExpression="REASON" />

                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Người tạo %>" DataField="CREATED_BY_NAME"
                                         HeaderStyle-Width="200px" ItemStyle-Width="200px" UniqueName="CREATED_BY_NAME" SortExpression="CREATED_BY_NAME" />

                                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày tạo %>" DataField="CREATED_DATE"
                                         HeaderStyle-Width="150px" ItemStyle-Width="150px" UniqueName="CREATED_DATE" SortExpression="CREATED_DATE"
                                        DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
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
                    OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlAtShiftNewEdit&id=' + id);
                    args.set_cancel(true);
                }
            }

            else if (args.get_item().get_commandName() == "CREATE") {
                OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlAtShiftNewEdit');
                args.set_cancel(true);
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
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
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
                //args.set_cancel(true);
            }
            else if (bCheck > 1) {
                var m = '<%= Translate("CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                //args.set_cancel(true);
            }
            else {
                var id = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID')
                OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlAtShiftNewEdit&id=' + id);
            }
        }

        function OpenInNewTab(url) {
            window.location.href = url;
        }
    </script>
</tlk:RadCodeBlock>

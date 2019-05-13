<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOTRegistration.ascx.vb"
    Inherits="Attendance.ctrlOTRegistration
    " %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidValid" runat="server" />
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
                                <%# Translate("Từ ngày làm thêm")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="rdRegDateFrom">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Đến ngày làm thêm")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="rdRegDateTo">
                                </tlk:RadDatePicker>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="rdRegDateTo"
                                    ControlToCompare="rdRegDateFrom" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Đến ngày làm thêm phải lớn hơn Từ ngày làm thêm %>"
                                    ToolTip="<%$ Translate: Đến ngày làm thêm phải lớn hơn Từ ngày làm thêm %>"></asp:CompareValidator>
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
                <tlk:RadGrid ID="rgMain" runat="server" Height="100%" Width="100%" AllowPaging="true" AllowMultiRowSelection="true">
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="true" />
                                <ClientEvents OnRowDblClick="gridRowDblClick" />
                            </ClientSettings>
                            <MasterTableView DataKeyNames="ID, ID_EMPLOYEE, EMPLOYEE_CODE, EMPLOYEE_NAME, DEPARTMENT, JOBTITLE, 
                 FROM_DATE,SIGN_CODE, NOTE,STATUS, STATUS_NAME, ID_REGGROUP" ClientDataKeyNames="ID, ID_EMPLOYEE, EMPLOYEE_CODE, EMPLOYEE_NAME, DEPARTMENT, JOBTITLE, 
                 FROM_DATE,SIGN_CODE, NOTE,STATUS, STATUS_NAME, ID_REGGROUP">
                                <Columns>
                                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS"
                                        UniqueName="STATUS" SortExpression="STATUS" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                        <ItemTemplate>
                                            <%# If(Eval("STATUS") = "0", Translate("Chưa gửi duyệt"), Translate(""))%>
                                            <%# If(Eval("STATUS") = "1", Translate("Đã duyệt"), Translate(""))%>
                                            <%# If(Eval("STATUS") = "2", Translate("Chờ phê duyệt"), Translate(""))%>
                                            <%# If(Eval("STATUS") = "3", Translate("Từ chối"), Translate(""))%>
                                            <%# If(Eval("STATUS") = "4", Translate("Từ chối phê duyệt và xóa"), Translate(""))%>
                                        </ItemTemplate>
                                    </tlk:GridTemplateColumn>
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                         HeaderStyle-Width="80px" ItemStyle-Width="80px" UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME"
                                         HeaderStyle-Width="140px" ItemStyle-Width="140px" SortExpression="EMPLOYEE_NAME" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="DEPARTMENT"
                                         HeaderStyle-Width="200px" ItemStyle-Width="200px" UniqueName="DEPARTMENT" SortExpression="DEPARTMENT"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="JOBTITLE"
                                         HeaderStyle-Width="270px" ItemStyle-Width="270px" UniqueName="JOBTITLE" SortExpression="JOBTITLE"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="FROM_DATE"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" UniqueName="FROM_DATE" SortExpression="FROM_DATE"
                                        DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                                        <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="TO_DATE"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" UniqueName="TO_DATE" SortExpression="TO_DATE"
                                        DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ký hiệu ca %>" DataField="SIGN_CODE"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" UniqueName="SIGN_CODE" SortExpression="SIGN_CODE" ItemStyle-HorizontalAlign="Center" />
                                    <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Loại làm thêm %>" DataField="OT_TYPE_NAME"
                                         HeaderStyle-Width="150px" ItemStyle-Width="150px" UniqueName="OT_TYPE_NAME" SortExpression="OT_TYPE_NAME"
                                        ItemStyle-HorizontalAlign="Center" /> --%>                                   
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Lí do làm thêm %>" DataField="NOTE"
                                         HeaderStyle-Width="200px" ItemStyle-Width="200px" UniqueName="NOTE" SortExpression="NOTE" />
                                    <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Người cập nhật %>" DataField="MODIFIED_BY"
                                         HeaderStyle-Width="140px" ItemStyle-Width="140px" UniqueName="MODIFIED_BY" SortExpression="MODIFIED_BY" />
                                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cập nhật %>" DataField="MODIFIED_DATE"
                                         HeaderStyle-Width="100px" ItemStyle-Width="100px" UniqueName="MODIFIED_DATE" SortExpression="MODIFIED_DATE"
                                        DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />--%>
                                    <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do không duyệt %>" DataField="REASON"
                                         HeaderStyle-Width="200px" ItemStyle-Width="200px" UniqueName="REASON" SortExpression="REASON" />--%>
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
                    var empId = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID_EMPLOYEE');
                    OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlOTRegistrationNewEdit&id=' + id + '&typeUser=User&empId=' + empId);
                    args.set_cancel(true);
                }
            }

            else if (args.get_item().get_commandName() == "CREATE") {
                if ($('#<%= hidValid.ClientID %>').val() == "1") {
                    var m = '<%= Translate(CommonMessage.MESSAGE_EXIST_INFOR) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'error' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return;
                }

                OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlOTRegistrationNewEdit&id=0&typeUser=User');
                args.set_cancel(true);
            }
            else if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            else if (args.get_item().get_commandName() == "SUBMIT") {
                bCheck = $find('<%# rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
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
                            m = '<%# Translate("The action only applies for the records that have status as Saved, Unapproved by LM or Unverified by HR. Please select other record.") %>';
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
                //args.set_cancel(true);
            }
            else if (bCheck > 1) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                //args.set_cancel(true);
            }
            else {
                var id = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID')
                var empId = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID_EMPLOYEE');
                OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlOTRegistrationNewEdit&id=' + id + '&typeUser=User&empId=' + empId);
                //args.set_cancel(true);
            }
        }

        function OpenInNewTab(url) {
            window.location.href = url;
        }
    </script>
</tlk:RadCodeBlock>

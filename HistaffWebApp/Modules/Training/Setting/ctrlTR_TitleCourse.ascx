<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_TitleCourse.ascx.vb"
    Inherits="Training.ctrlTR_TitleCourse" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="120px" Width="100%" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Chức danh")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTitle" runat="server" AutoPostBack="true" CausesValidation="false">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="item-head" colspan="6">
                          
                                <%# Translate("Thiết lập khóa đào tạo theo chuẩn chức danh")%>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Khóa đào tạo")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboCourse" runat="server" AutoPostBack="false" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboCourse"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập khóa đào tạo %>" ToolTip="<%$ Translate: Bạn phải nhập khóa đào tạo %>"> 
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <%-- <b>
                    <%# Translate("Danh sách khóa đào tạo theo chức danh")%></b>
                <hr />--%>
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true"
                    SkinID="GridSingleSelect" Scrolling="Y">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,TR_COURSE_CODE,TR_COURSE_NAME">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="TR_COURSE_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã khóa đào tạo %>" DataField="TR_COURSE_CODE"
                                SortExpression="TR_COURSE_CODE" UniqueName="TR_COURSE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên khóa đào tạo %>" DataField="TR_COURSE_NAME"
                                SortExpression="TR_COURSE_NAME" UniqueName="TR_COURSE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="TR_COURSE_REMARK"
                                SortExpression="TR_COURSE_REMARK" UniqueName="TR_COURSE_REMARK" />
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


        function cusTitle(oSrc, args) {
            var cbo = $find("<%# cboTitle.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            if (args.get_item().get_commandName() == 'CREATE') {
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'DELETE') {
                bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }
        }

    </script>
</tlk:RadCodeBlock>

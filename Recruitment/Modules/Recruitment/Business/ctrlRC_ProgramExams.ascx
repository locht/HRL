﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ProgramExams.ascx.vb"
    Inherits="Recruitment.ctrlRC_ProgramExams" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidProgramID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="180px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Phòng ban")%>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblOrgName" runat="server" Font-Bold="true">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Vị trí tuyển dụng")%><span class="lbReq">*</span>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblTitleName" runat="server" Font-Bold="true">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <b>
                                <%# Translate("Thiết lập vòng phỏng vấn")%></b>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Tên vòng phỏng vấn")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtName" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Thứ tự sắp xếp")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtExamsOrder" runat="server" SkinID="number">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" SkinID="GridSingleSelect">
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,NAME,POINT_LADDER,POINT_PASS,EXAMS_ORDER,IS_PV,COEFFICIENT,REMARK">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên vòng phỏng vấn %>" DataField="NAME"
                                SortExpression="NAME" UniqueName="NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Thư tự sắp xếp %>" DataField="EXAMS_ORDER"
                                SortExpression="EXAMS_ORDER" UniqueName="EXAMS_ORDER" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                                UniqueName="REMARK" />
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

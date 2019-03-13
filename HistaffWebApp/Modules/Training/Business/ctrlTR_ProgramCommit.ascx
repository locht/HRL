﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_ProgramCommit.ascx.vb"
    Inherits="Training.ctrlTR_ProgramCommit" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidProgramID" runat="server" />
<asp:HiddenField ID="hidApproveID" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane2" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Height="250px" Scrolling="None">
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin chương trình đào tạo")%>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên chương trình")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtProgramName" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Hình thức")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtHinhThuc">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Từ ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFromDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdToDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin cam kết")%>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số cam kết")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCommitNo" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày ký")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdCommitDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Thời gian quy đổi")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnConvertedTime" runat="server" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Người phê duyệt")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtAproveName" MaxLength="255" runat="server" Width="130px" ReadOnly="true">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindApprove" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtAproveName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Người phê duyệt %>" ToolTip="<%$ Translate: Bạn phải nhập Người phê duyệt %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtApproveTitle" MaxLength="255" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Thời gian cam kết làm việc")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCommitWork" runat="server" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" AllowMultiRowEdit="true" AllowSorting="false">
            <MasterTableView DataKeyNames="ID,EMPLOYEE_ID" ClientDataKeyNames="ID,COMMIT_NO,COMMIT_DATE,CONVERED_TIME,COMMIT_WORK,APPROVER_ID,APPROVER_NAME,APPROVER_TITLE,COMMIT_REMARK">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã học viên %>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="70px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="EMPLOYEE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                        UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số cam kết %>" DataField="COMMIT_NO"
                        UniqueName="COMMIT_NO" SortExpression="COMMIT_NO" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày ký %>" DataField="COMMIT_DATE"
                        UniqueName="COMMIT_DATE" SortExpression="COMMIT_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian quy đổi %>"
                        DataField="CONVERED_TIME" UniqueName="CONVERED_TIME" SortExpression="CONVERED_TIME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian cam kết làm việc %>"
                        DataField="COMMIT_WORK" UniqueName="COMMIT_WORK" SortExpression="COMMIT_WORK" />
                    <tlk:GridBoundColumn DataField="APPROVER_ID" UniqueName="APPROVER_ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Người phê duyệt %>" DataField="APPROVER_NAME"
                        UniqueName="APPROVER_NAME" SortExpression="APPROVER_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="APPROVER_TITLE"
                        UniqueName="APPROVER_TITLE" SortExpression="APPROVER_TITLE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="COMMIT_REMARK"
                        UniqueName="COMMIT_REMARK" SortExpression="COMMIT_REMARK" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" Width="150px" />
            <ClientSettings EnablePostBackOnRowClick="True">
                <Scrolling FrozenColumnsCount="4" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindLecture" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }

        }

    </script>
</tlk:RadCodeBlock>

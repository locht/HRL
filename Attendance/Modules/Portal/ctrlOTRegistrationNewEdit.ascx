<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOTRegistrationNewEdit.ascx.vb"
    Inherits="Attendance.ctrlOTRegistrationNewEdit" %>
<asp:HiddenField ID="hifStartDate" runat="server" />
<asp:HiddenField ID="hifEndDate" runat="server" />
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidValid" runat="server" />
<asp:HiddenField ID="hidStatus" runat="server" />
<asp:HiddenField ID="hid100" runat="server" />
<asp:HiddenField ID="hid150" runat="server" />
<asp:HiddenField ID="hid200" runat="server" />
<asp:HiddenField ID="hid210" runat="server" />
<asp:HiddenField ID="hid270" runat="server" />
<asp:HiddenField ID="hid300" runat="server" />
<asp:HiddenField ID="hid390" runat="server" />
<asp:HiddenField ID="hidTotal" runat="server" />
<asp:HiddenField ID="hidSignId" runat="server" />
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<%--<tlk:RadSplitter runat="server" ID="splitFull" Orientation="Horizontal" Width="100%"
    Height="100%">
    <tlk:RadPane runat="server" ID="pnlRegistInfo" Height="100px" Visible="true">
        
    </tlk:RadPane>    
</tlk:RadSplitter>--%>
<div style="vertical-align: middle; height: 65px; padding-top: 5px">
            <table class="table-form">
            <tr>
        <td colspan="4">
            <b style="color: red"><%# Translate("Thông tin nhân viên")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Họ tên")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtFullName" ReadOnly="true"></tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Phòng ban")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtDepartment" ReadOnly="true"></tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Mã nhân viên")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtEmpCode" ReadOnly="true"></tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Chức danh")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtTitle" ReadOnly="true"></tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <b style="color: red"><%# Translate("Thông tin đăng ký")%></b>
            <hr />
        </td>
    </tr>		
            <tr>
                <td style="text-align: left" class="lb">
                    <%# Translate("Từ ngày ")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdFromDate">
                    </tlk:RadDatePicker>
                </td>
                <td style="text-align: left" class="lb">
                    <%# Translate("đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdToDate">
                    </tlk:RadDatePicker>
                </td>
            </tr>
			<tr>
			     <td style="text-align: left" class="lb">
                    <%# Translate("Từ giờ")%>
                </td>
                <td>
                   <%-- <tlk:RadTimePicker ID="rtpFROM" runat="server">
                    </tlk:RadTimePicker>--%>
                    <tlk:RadComboBox runat="server" ID="cboHoursFrom"></tlk:RadComboBox>
                    <tlk:RadComboBox runat="server" ID="cboMinuteFrom"></tlk:RadComboBox>
                </td>
                <td style="text-align: left" class="lb">
                    <%# Translate("đến giờ")%>
                </td>
                <td>
                    <%--<tlk:RadTimePicker ID="rtpTO" runat="server">
                    </tlk:RadTimePicker>--%>
                    <tlk:RadComboBox runat="server" ID="cboHoursTo"></tlk:RadComboBox>
                    <tlk:RadComboBox runat="server" ID="cboMinuteTo"></tlk:RadComboBox>
                </td>
                <td> <asp:CheckBox runat="server" ID="chkNB" Text=<%# Translate("Quy đổi nghỉ bù")%> /></td>
			</tr>
            <tr>
                <td style="text-align: left" class="lb">
                    <%# Translate("Lý do")%>
                </td>
                <td colspan="8">
                    <tlk:RadTextBox runat="server" ID="txtReason" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <%--<td>
                    <tlk:RadButton ID="btnRegister" runat="server" Text="<%$ Translate: Đăng ký %>">
                    </tlk:RadButton>
                </td>--%>
            </tr>
        </table>
        </div>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function OnClientTimeSlotContextMenuItemClicking(sender, args) {
            if (sender.get_selectedSlots().length > 0) {
                var firstSlotFromSelection = sender.get_selectedSlots()[0];
                var lastSlotFromSelection = sender.get_selectedSlots()[sender.get_selectedSlots().length - 1];
                var hifStart = $("#<%= hifStartDate.ClientID %>");
                var hifEnd = $("#<%= hifEndDate.ClientID%>");
                hifStart.val(firstSlotFromSelection.get_startTime().format('dd/MM/yyyy'));
                hifEnd.val(lastSlotFromSelection.get_startTime().format('dd/MM/yyyy'));
            }
        }
        function OnClientAppointmentMoveStart(sender, eventArgs) {
            eventArgs.set_cancel(true);
        }
        function clientButtonClicking(sender, args) {
            //if (args.get_item().get_commandName() == "CANCEL") {
            //    OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlOTRegistration');
            //    args.set_cancel(true);
            //}
        }
    </script>
</tlk:RadCodeBlock>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOTRegistrationNewEdit.ascx.vb"
    Inherits="Attendance.ctrlOTRegistrationNewEdit" %>
<%@ Import Namespace="Common" %>
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
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<asp:Label runat="server" ID="lbStatus" ForeColor="Red"></asp:Label>
<table class="table-form">
    <tr>
        <td colspan="4">
            <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
        </td>
    </tr>
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
            <b style="color: red"><%# Translate("Thông tin giờ làm thêm thực tế")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ngày làm thêm")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdRegDate" AutoPostBack="true"></tlk:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdRegDate"
                runat="server" ErrorMessage="<%$ Translate: Chưa chọn ngày làm thêm. %>"
                ToolTip="<%$ Translate: Chưa chọn ngày làm thêm. %>"></asp:RequiredFieldValidator>
        </td>
        <td class="lb">
            <%# Translate("Ký hiệu công")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtSignCode" ReadOnly="true"></tlk:RadTextBox>            
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Giờ làm thêm:")%><span class="lbReq">*</span>
        </td>
        <td colspan="3"></td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("AM:")%><span class="lbReq">*</span>
        </td>
        <td>
            <%# Translate("Từ:")%>
            <tlk:RadNumericTextBox runat="server" ID="rntbFromAM" SkinID="Number" Width="50px"
                NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="2" MinValue="0" MaxValue="12">
            </tlk:RadNumericTextBox>
            <%# Translate(" giờ:")%>
            <tlk:RadComboBox runat="server" ID="cboFromAM" Width="55px"></tlk:RadComboBox>
            <%# Translate(" phút:")%>
        </td>
        <td colspan="2" style="padding-left: 65px;">
            <%# Translate("Đến:")%>
            <tlk:RadNumericTextBox runat="server" ID="rntbToAM" SkinID="Number"  Width="50px"
                NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="2" MinValue="0" MaxValue="12">
            </tlk:RadNumericTextBox>
            <%# Translate(" giờ:")%>
            <tlk:RadComboBox runat="server" ID="cboToAM"  Width="55px"></tlk:RadComboBox>
            <%# Translate(" phút:")%>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("PM:")%><span class="lbReq">*</span>
        </td>
        <td>
            <%# Translate("Từ:")%>
            <tlk:RadNumericTextBox runat="server" ID="rntbFromPM" SkinID="Number"  Width="50px"
                NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="2" MinValue="0" MaxValue="12">
            </tlk:RadNumericTextBox>
            <%# Translate(" giờ:")%>
            <tlk:RadComboBox runat="server" ID="cboFromPM"  Width="55px"></tlk:RadComboBox>
            <%# Translate(" phút:")%>
        </td>
        <td colspan="2" style="padding-left: 65px;">
            <%# Translate("Đến:")%>
            <tlk:RadNumericTextBox runat="server" ID="rntbToPM" SkinID="Number"  Width="50px"
                NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="2" MinValue="0" MaxValue="12">
            </tlk:RadNumericTextBox>
            <%# Translate(" giờ:")%>
            <tlk:RadComboBox runat="server" ID="cboToPM"  Width="55px"></tlk:RadComboBox>
            <%# Translate(" phút:")%>
        </td>
    </tr>
    <%--<tr>
        <td class="lb">
            <%# Translate("Loại làm thêm")%><span class="lbReq">*</span>
        </td>
        <td colspan="3">
            <tlk:RadComboBox runat="server" ID="cboTypeOT"></tlk:RadComboBox>
        </td>
    </tr>--%>
    <tr>
        <td class="lb">
            <%# Translate("Lý do làm thêm:")%><span class="lbReq">*</span>
        </td>
        <td colspan="3">
            <tlk:RadTextBox runat="server" ID="txtNote" Rows="3" Width="100%"></tlk:RadTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNote"
                runat="server" ErrorMessage="<%$ Translate: Overtime reason is require. %>"
                ToolTip="<%$ Translate: Overtime reason is require. %>"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Tổng số giờ làm thêm trong năm:")%>
        </td>
        <td>
            <%--<tlk:RadNumericTextBox runat="server" ID="rntTotalAccumulativeOTHours" SkinID="Number"
                NumberFormat-GroupSeparator="" ShowSpinButtons="true" ReadOnly="true">
            </tlk:RadNumericTextBox>--%>
            <tlk:RadTextBox runat="server" ID="rntTotalAccumulativeOTHours" SkinID="Number" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
    </tr>
</table>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function clientButtonClicking(sender, args) {
            //if (args.get_item().get_commandName() == "CANCEL") {
            //    OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlOTRegistration');
            //    args.set_cancel(true);
            //}
        }
        function OpenInNewTab(url) {
            window.location.href = url;
        }
        //function clearSelectRadcombo(cbo) {
        //    if (cbo) {
        //        cbo.clearItems();
        //        cbo.clearSelection();
        //        cbo.set_text('');
        //    }
        //}
        //function clearSelectRadtextbox(cbo) {
        //    if (cbo) {
        //        cbo.clear();
        //    }
        //}
    </script>
</tlk:RadCodeBlock>

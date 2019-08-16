﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlListUserNewEdit.ascx.vb"
    Inherits="Common.ctrlListUserNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmployeeID" runat="server" />
<asp:ValidationSummary ID="valSum" runat="server" />
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<table class="table-form">
    <tr>
        <td class="lb" style="width: 100px">
            <%# Translate("Tên tài khoản")%>: <span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadTextBox ID="txtUSERNAME" MaxLength="255" runat="server">
            </tlk:RadTextBox>
        </td>
        <td>
            <asp:CheckBox ID="cbIS_AD" runat="server" Text="<%$ Translate: AD User%>" />
            <asp:RequiredFieldValidator ID="reqUSERNAME" ControlToValidate="txtUSERNAME" runat="server"
                ErrorMessage="<%$ Translate: Bạn phải nhập tên tài khoản. %>" ToolTip="<%$ Translate: Bạn phải nhập tên tài khoản. %>"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="validateUSERNAME" ControlToValidate="txtUSERNAME" runat="server"
                ErrorMessage="<%$ Translate: Tên tài khoản đã tồn tại. %>" ToolTip="<%$ Translate: Tên tài khoản đã tồn tại. %>"></asp:CustomValidator>
        </td>
        <td class="lb">
            <%# Translate("Họ và tên")%>: <span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadTextBox ID="txtFULLNAME" MaxLength="255" runat="server">
            </tlk:RadTextBox>
            <asp:RequiredFieldValidator ID="reqFULLNAME" ControlToValidate="txtFULLNAME" runat="server"
                ErrorMessage="<%$ Translate: Bạn phải nhập họ và tên. %>" ToolTip="<%$ Translate: Bạn phải nhập họ và tên. %>"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Mật khẩu")%>: <span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadTextBox ID="txtPASSWORD" runat="server" SkinID="TextboxPassword" Width="156px">
                <PasswordStrengthSettings IndicatorElementID="CustomIndicator1" />
            </tlk:RadTextBox>
        </td>
        <td>
            <asp:CustomValidator ID="cvalPASSWORD_2" runat="server"></asp:CustomValidator>
            <span id="CustomIndicator1">&nbsp;</span>
        </td>
        <td class="lb">
            <%# Translate("Nhập lại mật khẩu")%>:
        </td>
        <td>
            <tlk:RadTextBox ID="txtPASSWORD_AGAIN" MaxLength="255" TextMode="Password" runat="server">
            </tlk:RadTextBox>
            <asp:CompareValidator ID="comparePASSWORD" runat="server" ControlToValidate="txtPASSWORD"
                ControlToCompare="txtPASSWORD_AGAIN" ErrorMessage="<%$ Translate: Mật khẩu nhập lại không khớp. %>"
                ToolTip="<%$ Translate: Mật khẩu nhập lại không khớp. %>"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Email")%>: <%--<span class="lbReq">*</span>--%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtEMAIL" MaxLength="255" runat="server">
            </tlk:RadTextBox>
        </td>
        <td>
           <%-- <asp:RequiredFieldValidator ID="reqEMAIL" ControlToValidate="txtEMAIL" runat="server"
                ErrorMessage="<%$ Translate: Bạn phải nhập Email. %>" ToolTip="<%$ Translate: Bạn phải nhập Email. %>"></asp:RequiredFieldValidator>--%>
            <asp:CustomValidator ID="cvalEMAIL" ControlToValidate="txtEMAIL" runat="server" ErrorMessage="<%$ Translate: Địa chỉ Email đã tồn tại. %>"
                ToolTip="<%$ Translate: Địa chỉ Email đã tồn tại. %>"></asp:CustomValidator>
            <asp:RegularExpressionValidator ID="regEMAIL" ControlToValidate="txtEMAIL" ValidationExpression="^([a-zA-Z0-9_\.-]+)@([a-zA-Z0-9_\.-]+)\.([a-zA-Z\.]{2,6})$"
                runat="server" ErrorMessage="<%$ Translate: Định dạng Email không chính xác %>"
                ToolTip="<%$ Translate: Định dạng Email không chính xác %>"></asp:RegularExpressionValidator>
        </td>
        <td class="lb">
            <%# Translate("Mobile")%>: <%--<span class="lbReq">*</span>--%>
        </td>
        <td>
            <tlk:RadMaskedTextBox ID="txtTELEPHONE" runat="server" Mask="(84) ############">
            </tlk:RadMaskedTextBox>
            <%--<asp:RequiredFieldValidator ID="reqTELEPHONE" ControlToValidate="txtTELEPHONE" runat="server"
                ErrorMessage="<%$ Translate: Bạn phải nhập số điện thoại. %>" ToolTip="<%$ Translate: Bạn phải nhập số điện thoại. %>"></asp:RequiredFieldValidator>--%>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:CheckBox ID="cbIS_APP" runat="server" Text="<%$ Translate: App User%>" />
        </td>
        <td>
            <asp:CheckBox ID="cbIS_PORTAL" runat="server" Text="<%$ Translate: Portal User%>" />
        </td>
        <td class="lb">
            <%# Translate("Ngày hiệu lực")%>: <span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdEFFECT_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy">
            </tlk:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rdEFFECT_DATE"
                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"
                ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Mã nhân viên")%>: <span class="lbReq">*</span>  
        </td>
        <td>
            <tlk:RadTextBox ID="txtEMPLOYEE_CODE" SkinID="Readonly"
                        ReadOnly="true" runat="server">
            </tlk:RadTextBox>
            <tlk:RadButton EnableEmbeddedSkins="false" ID="btnEmployee" SkinID="ButtonView" runat="server"
                            CausesValidation="false" Width="40px">
            </tlk:RadButton>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEMPLOYEE_CODE" runat="server"
                ErrorMessage="<%$ Translate: Bạn phải nhập mã nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập mã nhân viên. %>"></asp:RequiredFieldValidator>

            <%--<asp:CustomValidator ID="validateEMPLOYEE_CODE" ControlToValidate="txtEMPLOYEE_CODE"
                runat="server" ErrorMessage="<%$ Translate: Mã nhân viên không tồn tại. %>" ToolTip="<%$ Translate: Mã nhân viên không tồn tại. %>"></asp:CustomValidator>--%>
        </td>
        <td>
        </td>
        <td class="lb">
            <%# Translate("Ngày hết hiệu lực")%>:
        </td>
        <td>
            <tlk:RadDatePicker ID="rdEXPIRE_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy">
            </tlk:RadDatePicker>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdEXPIRE_DATE"
                ControlToCompare="rdEFFECT_DATE" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực. %>"
                ToolTip="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực. %>"></asp:CompareValidator>
        </td>
    </tr>
</table>
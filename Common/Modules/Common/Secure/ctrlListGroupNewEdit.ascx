﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlListGroupNewEdit.ascx.vb"
    Inherits="Common.ctrlListGroupNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:ValidationSummary ID="valSum" runat="server" />
<table class="table-form">
    <tr>
        <td class="lb" style="width: 100px">
            <%# Translate("Mã nhóm")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadTextBox ID="rtGROUP_CODE" MaxLength="35" runat="server" SkinID="Textbox35">
            </tlk:RadTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rtGROUP_CODE"
                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập mã nhóm. %>" ToolTip="<%$ Translate: Bạn phải nhập mã nhóm. %>"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="CustomValidator1" ControlToValidate="rtGROUP_CODE" runat="server"
                ErrorMessage="<%$ Translate: Mã nhóm đã tồn tại. %>" ToolTip="<%$ Translate: Mã nhóm đã tồn tại. %>"></asp:CustomValidator>
        </td>
        <td class="lb" style="width: 100px">
            <%# Translate("Tên nhóm")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadTextBox ID="rtGROUP_NAME" MaxLength="255" runat="server">
            </tlk:RadTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rtGROUP_NAME"
                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên nhóm. %>" ToolTip="<%$ Translate: Bạn phải nhập tên nhóm. %>"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="CustomValidator2" ControlToValidate="rtGROUP_NAME" runat="server"
                ErrorMessage="<%$ Translate: Tên nhóm đã tồn tại. %>" ToolTip="<%$ Translate: Tên nhóm đã tồn tại. %>"></asp:CustomValidator>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdEFFECT_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy">
            </tlk:RadDatePicker>
            <asp:RequiredFieldValidator ID="reqEFFECT_DATE" ControlToValidate="rdEFFECT_DATE"
                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"
                ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
        </td>
        <td class="lb">
            <%# Translate("Ngày hết hiệu lực")%>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdEXPIRE_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy">
            </tlk:RadDatePicker>
            <asp:CompareValidator ID="compareEXPIRE_DATE" runat="server" ControlToValidate="rdEXPIRE_DATE"
                ControlToCompare="rdEFFECT_DATE" ErrorMessage="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực %>"
                ToolTip="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực %>" Operator="GreaterThanEqual"></asp:CompareValidator>
        </td>
    </tr>
</table>

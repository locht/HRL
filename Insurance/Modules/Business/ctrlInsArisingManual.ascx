<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsArisingManual.ascx.vb"
    Inherits="Insurance.ctrlInsArisingManual" %>
<%@ Register Src="~/Modules/Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox"
    TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
    <tlk:RadToolBar ID="rtbMain" runat="server" />
    <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />        
        <div style="display: none;">
            <tlk:RadTextBox ID="txtID" Text="0" runat="server">
            </tlk:RadTextBox>
            <tlk:RadTextBox ID="txtEMPID" Text="0" runat="server">
            </tlk:RadTextBox>
        </div>
        <div style="margin-left: 30px; margin-top: 5px; width: 1270px;">
            <fieldset style="width: auto; height: auto">
                <legend>
                    <b><%# Translate("Thông tin chung")%></b>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb" style="width:100px">
                            <%# Translate("MSNV")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEMPLOYEE_ID" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                            <tlk:RadButton EnableEmbeddedSkins="false" runat="server" ID="btnSearchEmp" SkinID="ButtonView" TabIndex="1"
                                CausesValidation="false">
                            </tlk:RadButton>
                            <asp:RequiredFieldValidator ID="reqtxtEMPLOYEE_ID" ControlToValidate="txtEMPLOYEE_ID"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Nhân viên. %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Nhân viên. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb" style="width:80px">
                            <%# Translate("Họ & tên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtFULLNAME" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Phòng ban")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtDEP" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Chức danh bảo hiểm")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtPOSITION" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("CMND")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtCMND" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày cấp")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ReadOnly="true"
                                ID="txtDateIssue" TabIndex="5">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày sinh")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ReadOnly="true"
                                ID="txtDoB" TabIndex="5">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Nơi sinh")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtBirthPlace" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div style="margin-left:30px;margin-top: 5px; width: 1270px;">
            <fieldset style="width: auto; height: auto">
                <legend>
                    <b><%# Translate("Thông tin bảo hiểm")%></b>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb" style="width:100px">
                            <%# Translate("Đơn vị bảo hiểm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="ddlINS_ORG_ID" runat="server" TabIndex="2">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb" style="width:120px">
                            <%# Translate("Loại biến động")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="ddlINS_ARISING_TYPE_ID" AutoPostBack="true" runat="server" TabIndex="3">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqddlINS_ARISING_TYPE_ID" ControlToValidate="ddlINS_ARISING_TYPE_ID"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Loại biến động. %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Loại biến động. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                        <td class="lb" colspan="3" style="text-align:left;">
                            <tlk:RadButton ID="chkSI" AutoPostBack="true" Text='<%# Translate("BHXH")%>' ToggleType="CheckBox"
                                ButtonType="ToggleButton" runat="server" TabIndex="4">
                            </tlk:RadButton>
                            <tlk:RadButton ID="chkHI" AutoPostBack="true" Text='<%# Translate("BHYT")%>' ToggleType="CheckBox"
                                ButtonType="ToggleButton" runat="server" TabIndex="5">
                            </tlk:RadButton>
                            <tlk:RadButton ID="chkUI" AutoPostBack="true" Text='<%# Translate("BHTN")%>' ToggleType="CheckBox"
                                ButtonType="ToggleButton" runat="server" TabIndex="6">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Lương kỳ trước")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtSALARY_PRE_PERIOD" TabIndex="7"
                                runat="server">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Lương kỳ này")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtSALARY_NOW_PERIOD" runat="server" TabIndex="8">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb" style="width:120px">
                            <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="txtEFFECTIVE_DATE" AutoPostBack="true" TabIndex="9">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqtxtEFFECTIVE_DATE" ControlToValidate="txtEFFECTIVE_DATE"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày hiệu lực. %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb" style="width:120px">
                            <%# Translate("Ngày kết thúc")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="txtEXPRIE_DATE" AutoPostBack="true" TabIndex="10">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Biến động Từ tháng")%>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtARISING_FROM_MONTH"
                                TabIndex="11" Culture="en-US">
                            </tlk:RadMonthYearPicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến tháng")%>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtARISING_TO_MONTH"
                                TabIndex="12" Culture="en-US">
                            </tlk:RadMonthYearPicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đợt khai báo")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <%--<tlk:RadDatePicker runat="server" ID="txtDECLARE_DATE" AutoPostBack="true" TabIndex="13">
                            </tlk:RadDatePicker>--%>
                            <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtDECLARE_DATE" 
                                   TabIndex="13" Culture="en-US" AutoPostBack="true">
                            </tlk:RadMonthYearPicker>
                            <asp:RequiredFieldValidator ID="reqtxtDECLARE_DATE" ControlToValidate="txtDECLARE_DATE"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Đợt khai báo. %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Đợt khai báo. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày trả thẻ BHYT")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="txtHEALTH_RETURN_DATE" AutoPostBack="true"
                                TabIndex="15">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="7">
                            <tlk:RadTextBox ID="txtNOTE" Width="100%" runat="server" TabIndex="16">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div style="margin-left:30px; margin-top: 5px; width: 1270px;">
            <fieldset style="width: auto; height: auto">
                <legend>
                    <b><%# Translate("Thông tin truy thu/ thoái thu")%></b>
                </legend>
                <div style="margin-left: 44px; float:left">
                    <fieldset style="width: auto; height: auto">
                        <legend>
                            <b><%# Translate("Thông tin truy thu")%></b>
                        </legend>
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                    <%# Translate("Từ tháng")%>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtA_FROM"
                                        AutoPostBack="true" TabIndex="17" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Đến tháng")%>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtA_TO"
                                        AutoPostBack="true" TabIndex="18" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("BH_XH")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtA_SI" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Bảo hiểm Y tế")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtA_HI" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Bảo hiểm thất nghiệp")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtA_UI" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
                <div style="margin-left: 80px; float:left">
                    <fieldset style="width: auto; height: auto">
                        <legend>
                            <b><%# Translate("Thông tin thoái thu")%></b>
                        </legend>
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                    <%# Translate("Từ tháng")%>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtR_FROM"
                                        AutoPostBack="true" TabIndex="19" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Đến tháng")%>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtR_TO"
                                        AutoPostBack="true" TabIndex="20" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("BH_XH")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtR_SI" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Bảo hiểm Y tế")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtR_HI" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Bảo hiểm thất nghiệp")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtR_UI" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
                <div style="margin-right: 60px; float:right">
                    <fieldset style="width: auto; height: auto">
                        <legend>
                            <b><%# Translate("Thông tin bổ sung")%></b>
                        </legend>
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                    <%# Translate("Từ tháng")%>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtO_FROM"
                                        AutoPostBack="true" TabIndex="22" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Đến tháng")%>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtO_TO"
                                        AutoPostBack="true" TabIndex="23" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Bảo hiểm Y tế")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtO_HI" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Bảo hiểm thất nghiệp")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtO_UI" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">                                    
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" style="display:none" Value="0" ID="RadNumericTextBox1" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>                            
                            </tr>
                        </table>
                    </fieldset>
                </div>                
            </fieldset>
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }

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
        $(document).keypress(function (e) {
            if (e.which == 13) {
                e.preventDefault();
            }
        });
    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
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
                    <b><asp:Label runat="server" ID="lbCommonInfo" Text="Thông tin chung"></asp:Label></b>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb" style="width:100px">
                            <asp:Label runat="server" ID="lbEMPLOYEE_ID" Text="MSNV"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEMPLOYEE_ID" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                            <tlk:RadButton EnableEmbeddedSkins="false" runat="server" ID="btnSearchEmp" SkinID="ButtonView" TabIndex="1"
                                CausesValidation="false">
                            </tlk:RadButton>
                            <asp:RequiredFieldValidator ID="reqtxtEMPLOYEE_ID" ControlToValidate="txtEMPLOYEE_ID"
                                runat="server" Text="*" ErrorMessage="Bạn phải nhập Nhân viên."
                                ToolTip="Bạn phải nhập Nhân viên."></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb" style="width:80px">
                            <asp:Label runat="server" ID="lbFULLNAME" Text="Họ & tên"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtFULLNAME" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbDEP" Text="Phòng ban"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtDEP" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbPOSITION" Text="Chức danh bảo hiểm"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtPOSITION" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbCMND" Text="CMND"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtCMND" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbDateIssue" Text="Ngày cấp"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ReadOnly="true"
                                ID="txtDateIssue" TabIndex="5">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbDoB" Text="Ngày sinh"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ReadOnly="true"
                                ID="txtDoB" TabIndex="5">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbBirthPlace" Text="Nơi sinh"></asp:Label>
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
                    <b><asp:Label runat="server" ID="lbInsInfo" Text="Thông tin bảo hiểm"></asp:Label></b>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb" style="width:100px">
                            <asp:Label runat="server" ID="lbINS_ORG_ID" Text="Đơn vị bảo hiểm"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="ddlINS_ORG_ID" runat="server" TabIndex="2">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb" style="width:120px">
                            <asp:Label runat="server" ID="lbINS_ARISING_TYPE_ID" Text="Loại biến động"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="ddlINS_ARISING_TYPE_ID" AutoPostBack="true" runat="server" TabIndex="3">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqddlINS_ARISING_TYPE_ID" ControlToValidate="ddlINS_ARISING_TYPE_ID"
                                runat="server" Text="*" ErrorMessage="Bạn phải nhập Loại biến động."
                                ToolTip="Bạn phải nhập Loại biến động."></asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                        <td class="lb" colspan="3" style="text-align:left;">
                            <tlk:RadButton ID="chkSI" AutoPostBack="true" Text="BHXH" ToggleType="CheckBox"
                                ButtonType="ToggleButton" runat="server" TabIndex="4">
                            </tlk:RadButton>
                            <tlk:RadButton ID="chkHI" AutoPostBack="true" Text="BHYT" ToggleType="CheckBox"
                                ButtonType="ToggleButton" runat="server" TabIndex="5">
                            </tlk:RadButton>
                            <tlk:RadButton ID="chkUI" AutoPostBack="true" Text="BHTN" ToggleType="CheckBox"
                                ButtonType="ToggleButton" runat="server" TabIndex="6">
                            </tlk:RadButton>
                            <tlk:RadButton ID="chkTNLD_BNN" AutoPostBack="true" Text="BH TNLD, BNN" ToggleType="CheckBox"
                                ButtonType="ToggleButton" runat="server" TabIndex="6">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbSALARY_PRE_PERIOD" Text="Lương kỳ trước"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtSALARY_PRE_PERIOD" TabIndex="7"
                                runat="server">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbSALARY_NOW_PERIOD" Text="Lương kỳ này"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtSALARY_NOW_PERIOD" runat="server" TabIndex="8">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb" style="width:120px">
                            <asp:Label runat="server" ID="lbEFFECTIVE_DATE" Text="Ngày hiệu lực"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="txtEFFECTIVE_DATE" AutoPostBack="true" TabIndex="9">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqtxtEFFECTIVE_DATE" ControlToValidate="txtEFFECTIVE_DATE"
                                runat="server" Text="*" ErrorMessage="Bạn phải nhập Ngày hiệu lực."
                                ToolTip="Bạn phải nhập Ngày hiệu lực."></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb" style="width:120px">
                            <asp:Label runat="server" ID="lbEXPRIE_DATE" Text="Ngày kết thúc"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="txtEXPRIE_DATE" AutoPostBack="true" TabIndex="10">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbARISING_FROM_MONTH" Text="Biến động Từ tháng"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtARISING_FROM_MONTH"
                                TabIndex="11" Culture="en-US">
                            </tlk:RadMonthYearPicker>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbARISING_TO_MONTH" Text="Đến tháng"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtARISING_TO_MONTH"
                                TabIndex="12" Culture="en-US">
                            </tlk:RadMonthYearPicker>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbDECLARE_DATE" Text="Đợt khai báo"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <%--<tlk:RadDatePicker runat="server" ID="txtDECLARE_DATE" AutoPostBack="true" TabIndex="13">
                            </tlk:RadDatePicker>--%>
                            <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtDECLARE_DATE" 
                                   TabIndex="13" Culture="en-US" AutoPostBack="true">
                            </tlk:RadMonthYearPicker>
                            <asp:RequiredFieldValidator ID="reqtxtDECLARE_DATE" ControlToValidate="txtDECLARE_DATE"
                                runat="server" Text="*" ErrorMessage="Bạn phải nhập Đợt khai báo."
                                ToolTip="Bạn phải nhập Đợt khai báo."></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbHEALTH_RETURN_DATE" Text="Ngày trả thẻ BHYT"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="txtHEALTH_RETURN_DATE" AutoPostBack="true"
                                TabIndex="15">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbNOTE" Text="Ghi chú"></asp:Label>
                        </td>
                        <td colspan="7">
                            <tlk:RadTextBox ID="txtNOTE" Width="100%" runat="server" TabIndex="16">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div style="margin-left:30px; margin-top: 5px; width: 1270px; display: none;">
            <fieldset style="width: auto; height: auto">
                <legend>
                    <b><asp:Label runat="server" ID="lbA_SInfo" Text="Thông tin truy thu/ thoái thu"></asp:Label></b>
                </legend>
                <div style="margin-left: 44px; float:left">
                    <fieldset style="width: auto; height: auto">
                        <legend>
                            <b><asp:Label runat="server" ID="lbA_Info" Text="Thông tin truy thu"></asp:Label></b>
                        </legend>
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbA_FROM" Text="Từ tháng"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtA_FROM"
                                        AutoPostBack="true" TabIndex="17" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbA_TO" Text="Đến tháng"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtA_TO"
                                        AutoPostBack="true" TabIndex="18" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbA_SI" Text="BH_XH"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtA_SI" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbA_HI" Text="Bảo hiểm Y tế"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtA_HI" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbA_UI" Text="Bảo hiểm thất nghiệp"></asp:Label>
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
                            <b><asp:Label runat="server" ID="lbS_Info" Text="Thông tin thoái thu"></asp:Label></b>
                        </legend>
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbR_FROM" Text="Từ tháng"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtR_FROM"
                                        AutoPostBack="true" TabIndex="19" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbR_TO" Text="Đến tháng"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtR_TO"
                                        AutoPostBack="true" TabIndex="20" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbR_SI" Text="BH_XH"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtR_SI" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbR_HI" Text="Bảo hiểm Y tế"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtR_HI" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbR_UI" Text="Bảo hiểm thất nghiệp"></asp:Label>
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
                            <b><asp:Label runat="server" ID="lbBonusInfo" Text="Thông tin bổ sung"></asp:Label></b>
                        </legend>
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbO_FROM" Text="Từ tháng"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtO_FROM"
                                        AutoPostBack="true" TabIndex="22" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbO_TO" Text="Đến tháng"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtO_TO"
                                        AutoPostBack="true" TabIndex="23" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbO_HI" Text="Bảo hiểm Y tế"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtO_HI" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbO_UI" Text="Bảo hiểm thất nghiệp"></asp:Label>
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
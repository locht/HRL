<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsInformations.ascx.vb"
    Inherits="Insurance.ctrlInsInformations" %>
<%@ Register Src="~/Modules/Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox"
    TagPrefix="Common" %>
<style>
    #ctrlInsInformations_btnwhereHealth
    {
        margin-top:-2px;
    }
</style>
<asp:HiddenField ID="hidWhereHealth" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Scrolling="None">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="Both" Width="100%">
        <tlk:RadToolBar ID="rtbMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <div style="display: none;">
            <tlk:RadTextBox ID="txtID" Text="0" runat="server">
            </tlk:RadTextBox>
            <tlk:RadTextBox ID="txtEMPID" Text="0" runat="server">
            </tlk:RadTextBox>
        </div>
        <div style="width: 100%;">
            <div style="margin-left: 30px; width: 100%;">
                <fieldset style="width: auto; height: auto">
                    <legend>
                        <%# Translate("Thông tin chung")%>
                    </legend>
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("MSNV")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtEMPLOYEE_ID" ReadOnly="true" runat="server">
                                </tlk:RadTextBox>
                                <tlk:RadButton EnableEmbeddedSkins="false" runat="server" ID="btnSearchEmp" SkinID="ButtonView"
                                    TabIndex="1" CausesValidation="false">
                                </tlk:RadButton>
                                <tlk:RadButton runat="server" ID="btnOrgSULP" SkinID="ButtonView" CausesValidation="false"
                                    Visible="false" />
                                <asp:RequiredFieldValidator ID="reqtxtEMPLOYEE_ID" ControlToValidate="txtEMPLOYEE_ID"
                                    runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Nhân viên. %>"
                                    ToolTip="<%$ Translate: Bạn phải nhập Nhân viên. %>"></asp:RequiredFieldValidator>
                            </td>
                            <td class="lb">
                                <%# Translate("Họ & tên")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtFULLNAME" ReadOnly="true" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Phòng ban/Đơn vị")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtDEP" ReadOnly="true" runat="server">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Chức danh")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtPOSITION" ReadOnly="true" runat="server">
                                </tlk:RadTextBox>
                            </td>
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
                                <tlk:RadDatePicker DateInput-DisplayDateFormat="dd/MM/yyyy" ReadOnly="true" runat="server"
                                    ID="txtDateIssue">
                                </tlk:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Nơi cấp CMND")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtBirthPlaceCmnd" ReadOnly="true" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày sinh")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker DateInput-DisplayDateFormat="dd/MM/yyyy" ReadOnly="true" runat="server"
                                    ID="txtDoB">
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
                        <tr>
                            <td class="lb">
                                <%# Translate("Năm thâm niên BHXH trước khi vào công ty")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="rtxtSeniortyYear" runat="server" AutoPostBack="true">
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Tháng thâm niên BHXH trước khi vào công ty")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="rtxtSeniortyMonth" runat="server" AutoPostBack="true">
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Năm thâm niên BHXH trong công ty")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="rtxtSeniortyYearCT" runat="server" ReadOnly="true">
                                </tlk:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Tháng thâm niên BHXH trong công ty")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="rtxtSeniortyMonthCT" runat="server" ReadOnly="true">
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Tổng thâm niên tham gia BHXH")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtSeniorityTotal" runat="server" ReadOnly="true">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Đơn vị đóng")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtINSORG" ReadOnly="true" runat="server">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Lương tham gia BH")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtSALARY" runat="server">
                                    <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Loại bảo hiểm")%>
                            </td>
                            <td colspan="2">
                                <tlk:RadButton ID="chkSI" AutoPostBack="false" Text='<%# Translate("BHXH")%>' ToggleType="CheckBox"
                                    TabIndex="3" ButtonType="ToggleButton" runat="server">
                                </tlk:RadButton>
                                <tlk:RadButton ID="chkHI" AutoPostBack="false" Text='<%# Translate("BHYT")%>' ToggleType="CheckBox"
                                    TabIndex="4" ButtonType="ToggleButton" runat="server">
                                </tlk:RadButton>
                                <tlk:RadButton ID="chkUI" AutoPostBack="false" Text='<%# Translate("BHTN")%>' ToggleType="CheckBox"
                                    TabIndex="5" ButtonType="ToggleButton" runat="server">
                                </tlk:RadButton>
                                <tlk:RadButton ID="chkBHTNLD_BNN" AutoPostBack="false" Text='<%# Translate("BHTNLD_BNN")%>'
                                    ToggleType="CheckBox" TabIndex="5" ButtonType="ToggleButton" runat="server">
                                </tlk:RadButton>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </div>
        <div style="width: 100%;">
            <div style="width: 50%; float: left;">
                <fieldset style="width: auto; height: auto">
                    <legend>
                        <%# Translate("BẢO HIỂM XÃ HỘI")%>
                    </legend>
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Từ tháng")%>
                            </td>
                            <td>
                                <tlk:RadMonthYearPicker runat="server" ID="txtSI_FROM_MONTH" DateInput-DisplayDateFormat="MM/yyyy"
                                    TabIndex="6" Culture="en-US">
                                </tlk:RadMonthYearPicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Đến tháng")%>
                            </td>
                            <td>
                                <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtSI_TO_MONTH"
                                    Culture="en-US" TabIndex="7">
                                </tlk:RadMonthYearPicker>
                                <tlk:RadButton runat="server" ID="RadButton1" OnClientClicked="clearRadDatePicker"
                                    Text="Xóa" Width="25px" Style="margin-top: 0px">
                                </tlk:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Số số")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtSOCIAL_NUMBER" runat="server" TabIndex="8">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Tình trạng sổ")%>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="ddlSOCIAL_STATUS" runat="server" TabIndex="9">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ngày nộp cho công ty")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker DateInput-DisplayDateFormat="dd/MM/yyyy" runat="server" ID="txtSOCIAL_SUBMIT_DATE"
                                    TabIndex="10">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày cấp")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker DateInput-DisplayDateFormat="dd/MM/yyyy" runat="server" ID="txtSOCIAL_GRANT_DATE"
                                    TabIndex="11">
                                </tlk:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Số lưu trữ")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtSOCIAL_SAVE_NUMBER" runat="server" TabIndex="12">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Người giao sổ")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtSOCIAL_SUBMIT" runat="server" TabIndex="13">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ngày trả sổ")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker DateInput-DisplayDateFormat="dd/MM/yyyy" runat="server" ID="txtSOCIAL_RETURN_DATE"
                                    TabIndex="14">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Người nhận")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtSOCIAL_RECEIVER" runat="server" TabIndex="15">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ghi chú")%>
                            </td>
                            <td colspan="3">
                                <tlk:RadTextBox ID="txtSOCIAL_NOTE" Width="420px" runat="server" TabIndex="16">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <div style="width: 50%; float: left;">
                <fieldset style="width: auto; height: auto">
                    <legend>
                        <%# Translate("BẢO HIỂM Y TẾ")%>
                    </legend>
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Từ tháng")%>
                            </td>
                            <td>
                                <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtHI_FROM_MONTH"
                                    Culture="en-US" TabIndex="17">
                                </tlk:RadMonthYearPicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Đến tháng")%>
                            </td>
                            <td>
                                <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtHI_TO_MONTH"
                                    Culture="en-US" TabIndex="18">
                                </tlk:RadMonthYearPicker>
                                <tlk:RadButton runat="server" ID="RadButton2" OnClientClicked="clearRadDatePicker1"
                                    Text="Xóa" Width="25px" Style="margin-top: 0px">
                                </tlk:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Số thẻ Y tế")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtHEALTH_NUMBER" runat="server" TabIndex="19">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Tình trạng thẻ")%>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="ddlHEALTH_STATUS" runat="server" TabIndex="20">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Hiệu lực Từ ngày")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker DateInput-DisplayDateFormat="dd/MM/yyyy" runat="server" ID="txtHEALTH_EFFECT_FROM_DATE"
                                    TabIndex="21">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Đến ngày")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker DateInput-DisplayDateFormat="dd/MM/yyyy" runat="server" ID="txtHEALTH_EFFECT_TO_DATE"
                                    TabIndex="22">
                                </tlk:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Nơi cấp thẻ")%>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboPlaceBHYT" runat="server" TabIndex="23">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Nơi khám chữa bệnh")%>
                            </td>
                            <td style="display: none">
                                <tlk:RadComboBox ID="ddlHEALTH_AREA_INS_ID" runat="server" TabIndex="23">
                                </tlk:RadComboBox>
                            </td>
                            <td colspan="6">
                                <tlk:RadButton EnableEmbeddedSkins="false" CssClass="tvc_ins_whereHealth" runat="server" ID="btnwhereHealth" SkinID="ButtonView"
                                    TabIndex="5" CausesValidation="false">
                                </tlk:RadButton>
                                <tlk:RadTextBox ID="txtWHCode" ReadOnly="true" Width="15%" runat="server">
                                </tlk:RadTextBox>
                                <tlk:RadTextBox ID="txtWHName" ReadOnly="true" runat="server" Width="75%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ngày nhận")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker DateInput-DisplayDateFormat="dd/MM/yyyy" runat="server" ID="txtHEALTH_RECEIVE_DATE"
                                    TabIndex="24">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày trả thẻ")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker DateInput-DisplayDateFormat="dd/MM/yyyy" runat="server" ID="txtHEALTH_RETURN_DATE"
                                    TabIndex="26">
                                </tlk:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Đã đóng bảo hiểm đủ 5 năm")%>
                            </td>
                            <td>
                                <tlk:RadButton ID="chkIS_HI_FIVE_YEAR" AutoPostBack="false" ToggleType="CheckBox"
                                    TabIndex="5" ButtonType="ToggleButton" runat="server">
                                </tlk:RadButton>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </div>
        <div style="width: 100%;">
            <div style="float: left; width: 50%;">
                <fieldset style="width: auto; height: auto">
                    <legend>
                        <%# Translate("BẢO HIỂM THẤT NGHIỆP")%>
                    </legend>
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Từ tháng")%>
                            </td>
                            <td>
                                <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtUNEMP_FROM_MONTH"
                                    Culture="en-US" TabIndex="27">
                                </tlk:RadMonthYearPicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Đến tháng")%>
                            </td>
                            <td>
                                <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtUNEMP_TO_MONTH"
                                    Culture="en-US" TabIndex="28">
                                </tlk:RadMonthYearPicker>
                                <tlk:RadButton runat="server" ID="RadButton3" OnClientClicked="clearRadDatePicker4"
                                    Text="Xóa" Width="25px" Style="margin-top: 0px">
                                </tlk:RadButton>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <div style="float: left; width: 50%;">
                <fieldset style="width: auto; height: auto">
                    <legend>
                        <%# Translate("BHTNLD-BNN")%>
                    </legend>
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Từ tháng")%>
                            </td>
                            <td>
                                <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtBHTNLD_BNN_FROM_MONTH"
                                    Culture="en-US" TabIndex="27">
                                </tlk:RadMonthYearPicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Đến tháng")%>
                            </td>
                            <td>
                                <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtBHTNLD_BNN_TO_MONTH"
                                    Culture="en-US" TabIndex="28">
                                </tlk:RadMonthYearPicker>
                                <tlk:RadButton runat="server" ID="RadButton4" OnClientClicked="clearRadDatePicker5"
                                    Text="Xóa" Width="25px" Style="margin-top: 0px">
                                </tlk:RadButton>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindWhereHealth" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="FindOrgSULP" runat="server"></asp:PlaceHolder>
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

        function clearRadDatePicker() {
            var datepicker = $find("<%= txtSI_TO_MONTH.ClientID%>");
            datepicker.clear();
        }

        function clearRadDatePicker1() {
            var datepicker = $find("<%= txtHI_TO_MONTH.ClientID%>");
            datepicker.clear();
        }

        function clearRadDatePicker4() {
            var datepicker = $find("<%= txtUNEMP_TO_MONTH.ClientID%>");
            datepicker.clear();
        }

        function clearRadDatePicker5() {
            var datepicker = $find("<%= txtBHTNLD_BNN_TO_MONTH.ClientID%>");
            datepicker.clear();
        }

    </script>
</tlk:RadCodeBlock>

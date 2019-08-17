<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_TerminateNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_TerminateNewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:radsplitter id="RadSplitter3" runat="server" width="100%" height="100%" orientation="Horizontal">
    <tlk:radpane id="RadPane1" runat="server" height="35px" scrolling="None">
        <tlk:radtoolbar id="tbarTerminate" runat="server" onclientbuttonclicking="clientButtonClicking" />
    </tlk:radpane>
    <tlk:radpane id="RadPane2" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidTitleID" runat="server" />
        <asp:HiddenField ID="hidOrgID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:HiddenField ID="hiSalbasic" runat="server" />
        <asp:HiddenField ID="hidWorkStatus" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin chung")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbEmployeeCode" Text="MSNV"></asp:Label>
                </td>
                <td>
                    <tlk:radtextbox id="txtEmployeeCode" runat="server" skinid="Readonly" readonly="true"
                        width="130px">
                    </tlk:radtextbox>
                    <tlk:radbutton id="btnFindEmployee" runat="server" skinid="ButtonView" causesvalidation="false">
                    </tlk:radbutton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="Bạn phải nhập nhân viên." ToolTip="Bạn phải nhập nhân viên."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbEmployeeName" Text="Họ tên nhân viên"></asp:Label>
                </td>
                <td>
                    <tlk:radtextbox id="txtEmployeeName" runat="server" skinid="Readonly" readonly="true">
                    </tlk:radtextbox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbOrgName" Text="Phòng ban"></asp:Label>
                </td>
                <td>
                    <tlk:radtextbox id="txtOrgName" runat="server" skinid="Readonly" readonly="true">
                    </tlk:radtextbox>
                </td>
            </tr>
            <%--<tr>
                
                <td class="lb">
                    <%# Translate("Cấp nhân sự")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtStaffRankName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>--%>
            <%--<td class="lb">
                    <%# Translate("Lương cơ bản")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSalBasic" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>--%>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTitleName" Text="Chức danh"></asp:Label>
                </td>
                <td>
                    <tlk:radtextbox id="txtTitleName" runat="server" skinid="Readonly" readonly="true">
                    </tlk:radtextbox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbJoinDateState" Text="Ngày vào làm"></asp:Label>
                </td>
                <td>
                    <tlk:raddatepicker id="rdJoinDateState" runat="server" enabled="False" readonly="true">
                    </tlk:raddatepicker>
                </td>
                <%--
                <td class="lb">
                    <%# Translate("Chi phí hỗ trợ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostSupport" runat="server" ReadOnly="true" SkinID="ReadOnly">
                    </tlk:RadNumericTextBox>
                </td>--%>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSeniority" Text="Thâm niên công tác"></asp:Label>
                </td>
                <td>
                    <tlk:radtextbox id="txtSeniority" runat="server" readonly="True" skinid="ReadOnly">
                    </tlk:radtextbox>
                </td>
                <%--<td class="lb">
                    <asp:Label runat ="server" ID ="Label1" Text ="Số hợp đồng" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContractNo" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>--%>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbContractNo" Text="Hợp đồng hiện tại"></asp:Label>
                </td>
                <td>
                    <tlk:radtextbox id="txtContractNo" runat="server" readonly="True" skinid="ReadOnly">
                    </tlk:radtextbox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbContractEffectDate" Text="Từ ngày"></asp:Label>
                </td>
                <td>
                    <tlk:raddatepicker id="rdContractEffectDate" runat="server" dateinput-enabled="False">
                    </tlk:raddatepicker>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbContractExpireDate" Text="Đến ngày"></asp:Label>
                </td>
                <td>
                    <tlk:raddatepicker id="rdContractExpireDate" runat="server" dateinput-enabled="False">
                    </tlk:raddatepicker>
                </td>
            </tr>
            <tr style="visibility: hidden">
                <td class="lb">
                    <tlk:radtextbox id="txtRemindLink" runat="server">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin nghỉ việc")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSendDate" Text="Ngày nộp đơn"></asp:Label>
                </td>
                <td>
                    <tlk:raddatepicker id="rdSendDate" runat="server">
                    </tlk:raddatepicker>
                    <asp:CustomValidator ID="cvaldpSendDate" runat="server" ErrorMessage="Ngày nộp đơn không nhỏ hơn ngày vào công ty."
                        ToolTip="Ngày nộp đơn không nhỏ hơn ngày vào công ty.">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbLastDate" Text="Ngày làm việc cuối cùng"></asp:Label>
                </td>
                <td>
                    <tlk:raddatepicker id="rdLastDate" runat="server" autopostback="true" dateinput-causesvalidation="false">
                    </tlk:raddatepicker>
                    <asp:RequiredFieldValidator ID="reqLastDate" ControlToValidate="rdLastDate" runat="server"
                        ErrorMessage="Bạn phải nhập ngày làm việc cuối cùng." ToolTip="Bạn phải nhập ngày làm việc cuối cùng."> </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cval_LastDate_SendDate" runat="server" ErrorMessage="Ngày làm việc cuối cùng phải lớn hơn ngày nộp đơn."
                        ToolTip="Ngày làm việc cuối cùng phải lớn hơn ngày nộp đơn.">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cval_LastDate_JoinDate" runat="server" ErrorMessage="Ngày làm việc cuối cùng không nhỏ hơn ngày vào công ty."
                        ToolTip="Ngày làm việc cuối cùng không nhỏ hơn ngày vào công ty.">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTerReason" Text="Lý do nghỉ"></asp:Label>
                </td>
                <td>
                    <tlk:radcombobox id="cboTerReason" runat="server">
                    </tlk:radcombobox>
                </td>
                <%--<td>
                    <tlk:RadDatePicker ID="rdApprovalDate" runat="server" AutoPostBack="true" DateInput-CausesValidation="false">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdApprovalDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày được phê duyệt nghỉ. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập ngày được phê duyệt nghỉ %>"> </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvaldpApproveDate" runat="server" ErrorMessage="<%$ Translate: Ngày được phê duyệt nghỉ phải lớn hơn ngày nộp đơn. %>"
                        ToolTip="<%$ Translate: Ngày được phê duyệt nghỉ phải lớn hơn ngày nộp đơn. %>">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvaldpApproveDateJoinDate" runat="server" ErrorMessage="<%$ Translate: Ngày phê duyệt không nhỏ hơn ngày vào công ty. %>"
                        ToolTip="<%$ Translate: Ngày phê duyệt không nhỏ hơn ngày vào công ty. %>">
                    </asp:CustomValidator>
                </td>--%>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTerReasonDetail" Text="Lý do chi tiết"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:radtextbox id="txtTerReasonDetail" runat="server" skinid="TextBox1023" width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbRemark" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:radtextbox id="txtRemark" runat="server" skinid="TextBox1023" width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin bàn giao")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="5">
                    <tlk:radgrid pagesize="50" id="rgHandoverContent" runat="server" height="200px" width="550px"
                        skinid="GridNotPaging" allowmultirowedit="true">
                        <mastertableview datakeynames="ID,TERMINATE_ID,IS_FINISH,CONTENT_ID,CONTENT_NAME,EMPLOYEE_ID"
                            clientdatakeynames="ID,TERMINATE_ID,IS_FINISH,CONTENT_ID,CONTENT_NAME,EMPLOYEE_ID"
                            editmode="InPlace">
                            <Columns>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung bàn giao %>" DataField="CONTENT_NAME"
                                    SortExpression="CONTENT_NAME" UniqueName="CONTENT_NAME" ReadOnly="true" />
                                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Hoàn thành %>" DataField="IS_FINISH"
                                    SortExpression="IS_FINISH" UniqueName="IS_FINISH">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </tlk:GridCheckBoxColumn>
                            </Columns>
                        </mastertableview>
                    </tlk:radgrid>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Danh sách công nợ")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDebtType" Text="Tên loại công nợ"></asp:Label>
                </td>
                <td>
                    <tlk:radcombobox id="cboDebtType" runat="server">
                    </tlk:radcombobox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDebtMoney" Text="Số tiền"></asp:Label>
                </td>
                <td>
                    <tlk:radnumerictextbox id="rntxtDebtMoney" minvalue="0" runat="server" skinid="Money">
                    </tlk:radnumerictextbox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDebtStatus" Text="Trạng thái"></asp:Label>
                </td>
                <td>
                    <tlk:radcombobox id="cboDebtStatus" runat="server">
                    </tlk:radcombobox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDebtNote" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:radtextbox id="txtDebtNote" runat="server" skinid="TextBox1023" width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <tlk:radgrid pagesize="50" id="rgDebt" runat="server" height="250px" width="100%"
                        skinid="GridNotPaging" allowmultirowedit="true" allowmultirowselection="false">
                        <clientsettings enablerowhoverstyle="true" enablepostbackonrowclick="True">
                        <Selecting AllowRowSelect="true" />
                    </clientsettings>
                        <mastertableview editmode="PopUp" datakeynames="DEBT_TYPE_ID,DEBT_TYPE_NAME,MONEY,ID,DEBT_STATUS,DEBT_STATUS_NAME,REMARK"
                            clientdatakeynames="DEBT_TYPE_ID,DEBT_TYPE_NAME,MONEY,ID,DEBT_STATUS,DEBT_STATUS_NAME,REMARK"
                            commanditemdisplay="Top" allowautomaticinserts="true">
                            <CommandItemStyle Height="28px" />
                            <CommandItemTemplate>
                                <div style="padding: 2px 0 0 0">
                                    <div style="float: left">
                                        <tlk:RadButton ID="btnAddDebt" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/add.png" 
                                            CommandName="btnAddDebt"
                                            CausesValidation="false" Width="70px" Text="<%$ Translate: Thêm %>" OnClientClicking="btnAddDebtsOnClientClicking">
                                        </tlk:RadButton>
                                    </div>
                                    <div style="float: left">
                                        <tlk:RadButton ID="btnEditDebt" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/edit.png" 
                                            CommandName="btnEditDebt"
                                            CausesValidation="false" Width="70px" Text="<%$ Translate: Sửa %>" OnClientClicking="btnEditDebtsOnClientClicking">
                                        </tlk:RadButton>
                                    </div>
                                    <div style="float: right">
                                        <tlk:RadButton ID="btnDeleteDebts" runat="server"
                                            CommandName="btnDeleteDebts"
                                            Icon-PrimaryIconUrl="~/Static/Images/Toolbar/delete.png" 
                                            CausesValidation="false" Width="70px" Text="<%$ Translate: Xóa %>" OnClientClicking="btnDeleteDebtsOnClientClicking">
                                        </tlk:RadButton>
                                    </div>
                                </div>
                            </CommandItemTemplate>

                            <Columns>
                                <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                </tlk:GridClientSelectColumn>--%>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên loại công nợ %>" DataField="DEBT_TYPE_NAME"
                                    SortExpression="DEBT_TYPE_NAME" UniqueName="DEBT_TYPE_NAME" ReadOnly="true" />
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="MONEY"
                                    SortExpression="MONEY" UniqueName="MONEY" DataFormatString="{0:n0}"
                                    ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="DEBT_STATUS_NAME"
                                    SortExpression="DEBT_STATUS_NAME" UniqueName="DEBT_STATUS_NAME" ReadOnly="true" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK"
                                    SortExpression="REMARK" UniqueName="REMARK" ReadOnly="true" />
                            </Columns>
                            <HeaderStyle Width="100px" />
                        </mastertableview>
                    </tlk:radgrid>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDebtTotal" Text="Tổng số tiền công nợ"></asp:Label>
                </td>
                <td>
                    <tlk:radnumerictextbox id="rntxtDebtTotal" minvalue="0" runat="server" skinid="Money"
                        readonly="true">
                    </tlk:radnumerictextbox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDebtTotalCollect" Text="Tổng số tiền công nợ phải thu"></asp:Label>
                </td>
                <td>
                    <tlk:radnumerictextbox id="rntxtDebtTotalCollect" minvalue="0" runat="server" skinid="Money"
                        readonly="true">
                    </tlk:radnumerictextbox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbCash" Text="Số tiền thanh toán bằng tiền mặt"></asp:Label>
                </td>
                <td>
                    <tlk:radnumerictextbox id="rntxtCash" minvalue="0" runat="server" skinid="Money"
                        autopostback="true">
                    </tlk:radnumerictextbox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbAmountWrongful" Text="Tiền bồi thường theo lý do nghỉ việc trái pháp luật"></asp:Label>
                </td>
                <td>
                    <tlk:radnumerictextbox id="rntxtAmountWrongful" minvalue="0" runat="server" skinid="Money">
                    </tlk:radnumerictextbox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbMoneyDeductFromSal" Text="Số tiền trừ vào lương"></asp:Label>
                </td>
                <td>
                    <tlk:radnumerictextbox id="rntxtMoneyDeductFromSal" minvalue="0" runat="server" skinid="Money"
                        readonly="true">
                    </tlk:radnumerictextbox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSalMonth" Text="Tháng lương"></asp:Label>
                </td>
                <td>
                    <tlk:radcombobox id="cboSalMonth" runat="server">
                    </tlk:radcombobox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbAmountViolations" Text="Tiền bồi thường khi vi phạm thời gian báo trước"></asp:Label>
                </td>
                <td>
                    <tlk:radnumerictextbox id="rntxtAmountViolations" minvalue="0" runat="server" skinid="Money">
                    </tlk:radnumerictextbox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbInsStatus" Text="Tình trạng sổ BHXH"></asp:Label>
                </td>
                <td>
                    <tlk:radcombobox id="cboInsStatus" runat="server">
                    </tlk:radcombobox>
                    <asp:RequiredFieldValidator ID="reqInsStatus" ControlToValidate="cboInsStatus" runat="server"
                        ErrorMessage="Bạn phải nhập tình trạng sổ BHXH." ToolTip="Bạn phải nhập tình trạng sổ BHXH"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin trợ cấp thôi việc")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox ID="cbIsAllowForTer" runat="server" Text="Tính trợ cấp thôi việc" />
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTimeAccidentIns_loss" Text="Thời gian tham gia BHTN"></asp:Label>
                </td>
                <td>
                    <tlk:radtextbox id="txtTimeAccidentIns_loss" runat="server">
                    </tlk:radtextbox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbyearforallow_loss" Text="Số năm tính trợ cấp thôi việc"></asp:Label>
                </td>
                <td>
                    <tlk:radnumerictextbox id="rntxtyearforallow_loss" minvalue="0" runat="server" skinid="Decimal"
                        autopostback="true">
                        <%-- <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />--%>
                        <%--<ClientEvents OnBlur="displayDecimalFormat" OnLoad="displayDecimalFormat" OnValueChanged="displayDecimalFormat" />--%>
                    </tlk:radnumerictextbox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbReserveSeniority" Text="Thâm niên bảo lưu (Tháng)"></asp:Label>
                </td>
                <td>
                    <tlk:radnumerictextbox id="rntxtReserveSeniority" readonly="true" runat="server"
                        skinid="Decimal">
                    </tlk:radnumerictextbox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSalaryMedium_loss" Text="Lương trung bình tính 6 tháng"></asp:Label>
                </td>
                <td>
                    <tlk:radnumerictextbox id="rntxtSalaryMedium_loss" runat="server" minvalue="0">
                        <numberformat allowrounding="false" decimaldigits="0" decimalseparator="." groupseparator=","
                            groupsizes="3" />
                    </tlk:radnumerictextbox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbAllowanceTerminate" Text="Tiền trợ cấp thôi việc"></asp:Label>
                </td>
                <td>
                    <tlk:radnumerictextbox id="rntxtAllowanceTerminate" runat="server">
                        <numberformat allowrounding="false" decimaldigits="0" decimalseparator="." groupseparator=","
                            groupsizes="3" />
                    </tlk:radnumerictextbox>
                </td>
            </tr>
            <%--<tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbRemainingLeave" Text ="Số phép còn lại" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtRemainingLeave" runat="server" SkinID="ReadonlyDecimal"
                        ReadOnly="true" NumberFormat-DecimalDigits="1">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbCompensatoryLeave" Text ="Số ngày nghỉ bù còn lại" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCompensatoryLeave" runat="server" ReadOnly="true"
                        SkinID="ReadonlyDecimal" NumberFormat-DecimalDigits="2">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbPaymentLeave" Text ="Tiền thanh toán phép"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtPaymentLeave" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbSalaryMedium_loss" Text ="Lương trung bình tính 6 tháng" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSalaryMedium_loss" runat="server" MinValue="0">
                        <NumberFormat AllowRounding="false" DecimalDigits="0" DecimalSeparator="." GroupSeparator=","
                            GroupSizes="3" />
                    </tlk:RadNumericTextBox>
                </td>

                <td class="lb">
                    <asp:Label runat ="server" ID ="lbTimeAccidentIns_loss" Text ="Thời gian tham gia BHTN" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTimeAccidentIns_loss" runat="server">
                    </tlk:RadTextBox>
                </td>

                <td class="lb">
                    <asp:Label runat ="server" ID ="lbyearforallow_loss" Text ="Số năm tính trợ cấp mất việc" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtyearforallow_loss" MinValue="0" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                    <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                                    <ClientEvents OnBlur="displayDecimalFormat" OnLoad="displayDecimalFormat" OnValueChanged="displayDecimalFormat" />
                    </tlk:RadNumericTextBox>
                </td>

            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbMoneyReturn" Text ="Số tiền còn lại" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtMoneyReturn" MinValue="0" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbAmountViolations" Text ="Phạt thời hạn báo trước" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAmountViolations" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbAmountWrongful" Text ="Phạt chấm dứt trái luật" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAmountWrongful" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbCompensatoryPayment" Text ="Tiền thanh toán nghỉ bù" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCompensatoryPayment" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbAllowanceTerminate" Text ="Trợ cấp thôi việc" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAllowanceTerminate" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbTrainingCosts" Text ="Chi phí đào tạo" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtTrainingCosts" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbOtherCompensation" Text ="Bồi thường khác" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtOtherCompensation" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>--%>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Phê duyệt")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox ID="cbIsNoHire" runat="server" Text="Không tuyển dụng lại" />
                </td>
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox ID="cbIsReplacePos" runat="server" Text="Tuyển thay thế" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDecisionType" Text="Loại quyết định"></asp:Label>
                </td>
                <td>
                    <tlk:radcombobox id="cboDecisionType" autopostback="true" causesvalidation="false"
                        runat="server">
                    </tlk:radcombobox>
                    <asp:RequiredFieldValidator ID="reqDecisionType" ControlToValidate="cboDecisionType"
                        runat="server" ErrorMessage="Bạn phải chọn loại quyết định." ToolTip="Bạn phải chọn loại quyết định."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDecisionNo" Text="Số quyết định"></asp:Label><span
                        style="color: red"> *</span>
                </td>
                <td>
                    <tlk:radtextbox id="txtDecisionNo" runat="server">
                    </tlk:radtextbox>
                    <asp:RequiredFieldValidator ID="reqDecisionNo" ControlToValidate="txtDecisionNo"
                        runat="server" ErrorMessage="Bạn phải nhập số quyết định." ToolTip="Bạn phải nhập số quyết định."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%>
                </td>
                <td>
                    <tlk:raddatepicker id="rdEffectDate" runat="server" autopostback="true" causesvalidation="false">
                    </tlk:raddatepicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" ControlToValidate="rdEffectDate" runat="server"
                        ErrorMessage="Bạn phải nhập ngày hiệu lực." ToolTip="Bạn phải nhập ngày hiệu lực."> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label4" Text="Tập tin đính kèm"></asp:Label>
                </td>
                <td>
                    <tlk:radtextbox id="txtUpload" readonly="true" runat="server">
                    </tlk:radtextbox>
                    <tlk:radtextbox id="txtUploadFile" runat="server" visible="false">
                    </tlk:radtextbox>
                    <tlk:radbutton runat="server" id="btnUploadFile" skinid="ButtonView" causesvalidation="false"
                        tabindex="3" />
                    <tlk:radbutton id="btnDownload" runat="server" text="<%$ Translate: Tải tập tin%>"
                        causesvalidation="false" onclientclicked="rbtClicked" tabindex="3" enableviewstate="false">
                    </tlk:radbutton>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSignDate" Text="Ngày ký"></asp:Label>
                </td>
                <td>
                    <tlk:raddatepicker id="rdSignDate" runat="server">
                    </tlk:raddatepicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSignerName" Text="Người phê duyệt"></asp:Label>
                </td>
                <td>
                    <tlk:radtextbox id="txtSignerName" runat="server" readonly="true" width="130px" skinid="ReadOnly">
                    </tlk:radtextbox>
                    <tlk:radbutton id="btnFindSinger" runat="server" skinid="ButtonView" causesvalidation="false">
                    </tlk:radbutton>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSignerTitle" Text="Chức danh người phê duyệt"></asp:Label>
                </td>
                <td>
                    <tlk:radtextbox id="txtSignerTitle" runat="server" readonly="true">
                    </tlk:radtextbox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbStatus" Text="Trạng thái phê duyệt"></asp:Label>
                </td>
                <td>
                    <tlk:radcombobox id="cboStatus" runat="server">
                    </tlk:radcombobox>
                    <asp:RequiredFieldValidator ID="reqStatus" ControlToValidate="cboStatus" runat="server"
                        ErrorMessage="Bạn phải chọn trạng tháỉ phê duyệt." ToolTip="Bạn phải chọn trạng tháỉ phê duyệt."> </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </tlk:radpane>
</tlk:radsplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:radcodeblock id="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusStatus(oSrc, args) {
            var cbo = $find("<%# cboStatus.ClientID%>");
            args.IsValid = (cbo.get_value().length != 0);
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
            //if (args.get_item().get_commandName() == 'CANCEL') {
            //    getRadWindow().close(null);
            //    args.set_cancel(true);
            //}
        }

        function btnDeleteReasonClick(sender, args) {
            var bCheck = $find('<%# rgHandoverContent.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                args.set_cancel(true);
            }
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function btnDeleteDebtsOnClientClicking(sender, args) {

        }
        function btnAddDebtsOnClientClicking(sender, args) {
            var grid = $find("<%# rgDebt.ClientID%>");
            // grid.MasterTableView.isItemInserted = true;
            // grid.MasterTableView.showInsertItem();
        }
        function btnEditDebtsOnClientClicking(sender, args) {

        }
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:radcodeblock>

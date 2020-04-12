<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_WageNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_WageNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<asp:HiddenField ID="hidEmp" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<asp:HiddenField ID="hidOrg" runat="server" />
<asp:HiddenField ID="hidTitle" runat="server" />
<asp:HiddenField ID="hidStaffRank" runat="server" />
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidExRate_Type_ID_Old" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Scrolling="None" Height="35px">
        <tlk:RadToolBar ID="tbarMassTransferSalary" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary"
            ValidationGroup="" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin chung")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="lbEmployeeCode" Text="Mã nhân viên"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" Width="130px" SkinID="Readonly"
                        ReadOnly="true">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="Bạn phải chọn Nhân viên " ToolTip="Bạn phải chọn Nhân viên"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="lbEmployeeName" Text="Họ tên"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="Bạn phải chọn Nhân viên " ToolTip="Bạn phải chọn Nhân viên"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="lbOrgName" Text="Phòng ban"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqOrgName" ControlToValidate="txtOrgName" runat="server"
                        ErrorMessage="Nhân viên chưa có phòng ban" ToolTip="Nhân viên chưa có phòng ban"> 
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTitleName" Text="Chức danh"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqTitleName" ControlToValidate="txtTitleName" runat="server"
                        ErrorMessage="Nhân viên chưa có chức danh" ToolTip="Nhân viên chưa có chức danh"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbEffectDate" Text="Ngày hiệu lực"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" AutoPostBack="true" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" runat="server" ControlToValidate="rdEffectDate"
                        ErrorMessage="Bạn phải nhập ngày hiệu lực" ToolTip="Bạn phải nhập ngày hiệu lực"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbExpireDate" Text="Ngày hết hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdExpireDate"
                        Type="Date" ControlToCompare="rdEffectDate" Operator="GreaterThanEqual" ErrorMessage="Ngày kết thúc phải lớn hơn ngày hiệu lực"
                        ToolTip="Ngày kết thúc phải lớn hơn ngày hiệu lực"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lý do điều chỉnh")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtREASON_EDIT_EFDATE" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDecisionNo" Text="Số tờ trình/QĐ"></asp:Label><span
                        class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionNo" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqDecisionNo" ControlToValidate="txtDecisionNo"
                        runat="server" ErrorMessage="Bạn phải nhập Số tờ trình/QĐ " ToolTip="Bạn phải nhập Số tờ trình/QĐ"> 
                    </asp:RequiredFieldValidator>
                </td>
                <%--<td class="lb">
                    <asp:Label ID="lbUpload" runat="server" Text="Tập tin đính kèm"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>--%>
                <td class="lb">
                </td>
                <td>
                </td>
                <td class="lb">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin lương")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbSalTYPE" runat="server" Text="Nhóm lương"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalTYPE" runat="server" AutoPostBack="true" CausesValidation="false"
                        OnClientItemsRequesting="OnClientItemsRequesting" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnSelectedIndexChanged="cboSalTYPE_SelectedIndexChanged" SkinID="LoadDemand">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqSalTYPE" ControlToValidate="cboSalTYPE" runat="server"
                        ErrorMessage="Bạn phải nhập chọn Nhóm lương" ToolTip="Bạn phải nhập chọn Nhóm lương"> 
                    </asp:RequiredFieldValidator>
                    <%--  <asp:CustomValidator ID="cusSalType" runat="server" ClientValidationFunction="cusSalType"
                        ErrorMessage="<%# GetYouMustChoseMsg(UI.Wage_WageGRoup) %>" ToolTip="<%# GetYouMustChoseMsg(UI.Wage_WageGRoup) %>">
                    </asp:CustomValidator>--%>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSalaryGroup" Text="Thang lương"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbSalaryGroup" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqSalaryGroup" ControlToValidate="cbSalaryGroup"
                        runat="server" ErrorMessage="Bạn phải nhập chọn Thang lương" ToolTip="Bạn phải nhập chọn Thang lương"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSalaryLevel" Text="Ngạch lương"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbSalaryLevel" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqSalaryLevel" ControlToValidate="cbSalaryLevel"
                        runat="server" ErrorMessage="Bạn phải nhập chọn Ngạch lương" ToolTip="Bạn phải nhập chọn Ngạch lương"> 
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSalaryRank" Text="Bậc lương"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbSalaryRank" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reSalaryRank" ControlToValidate="cbSalaryRank" runat="server"
                        ErrorMessage="Bạn phải nhập chọn Bậc lương" ToolTip="Bạn phải nhập chọn Bậc lương"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label1" Text="Mức lương tối thiểu"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnmtxtSalBas_Min" runat="server" Enabled="False" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label2" Text="Mức lương tối đa"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnmtxtSalBas_Max" runat="server" Enabled="False" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <%--<td class="lb">
                    <asp:Label runat="server" ID="lbFactorSalary" Text="Hệ số/mức tiền"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="rnFactorSalary" AutoPostBack="true" EnabledStyle-HorizontalAlign="Right">
                    </tlk:RadTextBox>
                </td>--%>
                <td class="lb">
                    <asp:Label ID="lbbasicSalary" runat="server" Text="Lương cơ bản"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="basicSalary" MinValue="0" runat="server" AutoPostBack="false"
                        CausesValidation="false" SkinID="Money">
                        <ClientEvents OnValueChanged="OnValueChanged" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqdBasicSalary" runat="server" ControlToValidate="basicSalary"
                        ErrorMessage="Bạn phải nhập Lương cơ bản" ToolTip="Bạn phải nhập Lương cơ bản"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbTaxTable" runat="server" Text="Biểu thuế"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTaxTable" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqTaxTable" runat="server" ControlToValidate="cboTaxTable"
                        ErrorMessage="Bạn phải chọn Biểu thuế" ToolTip="Bạn phải chọn Biểu thuế"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbAllowance_Total" runat="server" Text="Tổng phụ cấp"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAllowance_Total" runat="server" Enabled="False" SkinID="Money"
                        CausesValidation="false">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbPercentSalary" Text="% hưởng lương"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnPercentSalary" AutoPostBack="false" SkinID="Money"
                        MaxValue="100" MinValue="0">
                        <ClientEvents OnValueChanged="OnValueChanged" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqPercentSalary" runat="server" ControlToValidate="rnPercentSalary"
                        ErrorMessage="Bạn phải nhập % hưởng lương" ToolTip="Bạn phải nhập % hưởng lương"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="Label3" runat="server" Text="Tỷ giá bảo hiểm"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnmtxtSalRate" MinValue="0" runat="server" SkinID="Money"
                        AutoPostBack="true" CausesValidation="false">
                        <ClientEvents OnValueChanged="OnValueChanged" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqSalRate" runat="server" ControlToValidate="rnmtxtSalRate"
                        ErrorMessage="Bạn phải nhập Tỷ giá bảo hiểm" ToolTip="Bạn phải nhập Tỷ giá bảo hiểm"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbrntxtSalaryInsurance" runat="server" Text="Mức lương chính"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSalaryInsurance" runat="server" AutoPostBack="true"
                        SkinID="Money">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqSalaryInsurance" runat="server" ControlToValidate="rntxtSalaryInsurance"
                        ErrorMessage="Bạn phải nhập Mức lương chính" ToolTip="Bạn phải nhập Mức lương chính"> 
                    </asp:RequiredFieldValidator>
                    <%--<td class="lb">
                    <asp:Label ID="lbSalary_Total" runat="server" Text="Tổng mức lương"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="Salary_Total" MinValue="0" runat="server" Enabled="False" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>--%>
                </td>
            </tr>
            <%--<tr >
                <td class="lb">
                    <asp:Label runat="server" ID="lbOtherSalary1" Text="Thưởng hiệu quả công việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnOtherSalary1" AutoPostBack="true" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbOtherSalary2" Text="Phụ cấp kiêm nhiệm"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnOtherSalary2" AutoPostBack="true" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbOtherSalary3" Text="Chi phí hỗ trợ khác"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnOtherSalary3" AutoPostBack="true" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>--%>
            <tr style="display: none">
                <td class="lb">
                    <asp:Label runat="server" ID="lbOtherSalary4" Text="Lương khác 4"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnOtherSalary4" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbOtherSalary5" runat="server" Text="Lương khác 5"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnOtherSalary5" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbSaleCommision" runat="server" Text="Đối tượng Sale Commision"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSaleCommision" runat="server" AutoPostBack="true" CausesValidation="false"
                        OnClientItemsRequesting="OnClientItemsRequesting" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnSelectedIndexChanged="cboSalTYPE_SelectedIndexChanged" SkinID="LoadDemand">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin phụ cấp")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbAllowance" runat="server" Text="Phụ cấp"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboAllowance" runat="server" AutoPostBack="true" OnClientItemsRequesting="OnClientItemsRequesting"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" SkinID="LoadDemand"
                        ValidationGroup="Allowance">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbAmount" runat="server" Text="Số tiền"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAmount" runat="server" SkinID="Money" ValidationGroup="Allowance">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbAllowEffectDate" runat="server" Text="Ngày hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdAllowEffectDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr style="display: none">
                <%--<td class="lb">
                    <asp:Label ID="lbAllowExpireDate" runat="server" Text="Ngày hết hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdAllowExpireDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="rdEffectDate"
                        ControlToValidate="rdAllowExpireDate" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn Ngày hiệu lực %>"
                        Operator="GreaterThanEqual" ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn Ngày hiệu lực %>"
                        Type="Date"></asp:CompareValidator>
                </td>
                <td>
                    <tlk:RadButton ID="chkIsInsurrance" runat="server" AutoPostBack="false" ButtonType="ToggleButton"
                        CausesValidation="false" Enabled="false" Text=" Đóng bảo hiểm " ToggleType="CheckBox">
                    </tlk:RadButton>
                </td>--%>
            </tr>
            <tr>
                <td colspan="6">
                    <tlk:RadGrid ID="rgAllow" runat="server" Height="200px" PageSize="50" SkinID="GridNotPaging"
                        Width="100%">
                        <MasterTableView ClientDataKeyNames="ID,ALLOWANCE_LIST_ID,AMOUNT,IS_INSURRANCE,ALLOWANCE_LIST_NAME,EFFECT_DATE,EXPIRE_DATE,AMOUNT_EX"
                            CommandItemDisplay="Top" DataKeyNames="ID,ALLOWANCE_LIST_ID,AMOUNT,IS_INSURRANCE,ALLOWANCE_LIST_NAME,EFFECT_DATE,EXPIRE_DATE,AMOUNT_EX">
                            <CommandItemStyle Height="28px" />
                            <CommandItemTemplate>
                                <div style="padding: 2px 0 0 0">
                                    <div style="float: left">
                                        <tlk:RadButton ID="btnInsertAllowance" runat="server" CausesValidation="false" CommandName="InsertAllow"
                                            Icon-PrimaryIconUrl="~/Static/Images/Toolbar/add.png" Text="<%$ Translate: Thêm %>"
                                            Width="70px">
                                        </tlk:RadButton>
                                    </div>
                                    <div style="float: right">
                                        <tlk:RadButton ID="btnDeleteAllowance" runat="server" CausesValidation="false" CommandName="DeleteAllow"
                                            Icon-PrimaryIconUrl="~/Static/Images/Toolbar/delete.png" OnClientClicking="btnDeleteAllowanceClick"
                                            Text="<%$ Translate: Xóa %>" Width="70px">
                                        </tlk:RadButton>
                                    </div>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                </tlk:GridClientSelectColumn>
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền VNĐ %>" DataField="AMOUNT_EX"
                                    SortExpression="AMOUNT_EX" UniqueName="AMOUNT_EX" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="AMOUNT"
                                    SortExpression="AMOUNT" UniqueName="AMOUNT" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                            </Columns>
                        </MasterTableView>
                    </tlk:RadGrid>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label4" runat="server" Text="Loại tiền tệ"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td colspan="4">
                    <tlk:RadComboBox ID="cboExRate" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <%# Translate("(Lưu ý: loại tiền tệ sử dụng cho cả thông tin lương và thông tin phụ cấp)")%>
                    <asp:RequiredFieldValidator ID="reExRate" runat="server" ControlToValidate="cboExRate"
                        ErrorMessage="Bạn phải chọn loại tiền tệ" ToolTip="Bạn phải chọn loại tiền tệ"> 
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin phê duyệt")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trạng thái")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        SkinID="LoadDemand">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusStatus" runat="server" ClientValidationFunction="cusStatus"
                        ErrorMessage="<%$ Translate: Bạn phải chọn Trạng thái %>" ToolTip="<%$ Translate: Bạn phải chọn Trạng thái  %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày ký")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSignDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Người ký")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignName" runat="server" ReadOnly="true" SkinID="Readonly"
                        Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindSign" runat="server" CausesValidation="false" SkinID="ButtonView">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignTitle" runat="server" ReadOnly="true" SkinID="Readonly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

       <%-- function cusDecisionType(oSrc, args) {
            var cbo = $find("<%# cboDecisionType.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }--%>

        function cusStatus(oSrc, args) {
            var cbo = $find("<%# cboStatus.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusSalType(oSrc, args) {
            var cbo = $find("<%# cboSalTYPE.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
      <%--  function cusSalGroup(oSrc, args) {
            var cbo = $find("<%# cbSalaryGroup.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        } --%>
        function cusTaxTable(oSrc, args) {
            var cbo = $find("<%# cboTaxTable.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
      <%--   function cusSalLevel(oSrc, args) {
            var cbo = $find("<%# cbSalaryLevel.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        } --%>

       <%-- function cusSalRank(oSrc, args) {
            var cbo = $find("<%# cbSalaryRank.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }--%>

        function btnDeleteAllowanceClick(sender, args) {
            var bCheck = $find('<%# rgAllow.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                args.set_cancel(true);
            }
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
            if (args.get_item().get_commandName() == 'SAVE') {
                <%-- var objSalBasic = $find('<%= basicSalary.ClientID %>');
               --%>
                var valueSalBasic = 0;
                var valueSalTotal = 0;
                var valueCostSupport = 0;
                var valueHSDieuChinh = 0;
                var valueTGGiuBac = 0;
                var valueLuongDieuTiet = 0;
                <%-- if (objSalBasic.get_value()) {
                    valueSalBasic = objSalBasic.get_value();
                }
             if (objSalTotal.get_value()) {
                    valueSalTotal = objSalTotal.get_value();
                } 
                if (objCostSupport.get_value()) {
                    valueCostSupport = objCostSupport.get_value();
                }
                if (objHSDieuChinh.get_value()) {
                    valueHSDieuChinh = objHSDieuChinh.get_value();
                }
                if (objTGGiuBac.get_value()) {
                    valueTGGiuBac = objTGGiuBac.get_value();
                }
                if (objLuongDieuTiet.get_value()) {
                    valueLuongDieuTiet = objLuongDieuTiet.get_value();
                }--%>
                //                if (valueSalTotal - valueSalBasic - valueCostSupport - valueHSDieuChinh != 0) {
                //                    var m = 'Tổng lương phải bằng Lương cơ bản + Chi phí hỗ trợ + Hệ số điều chỉnh';
                //                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                //                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                //                    args.set_cancel(true);
                //                }
            }
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //            }
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
        }


        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
             case '<%= cbSalaryGroup.ClientID %>':
                cbo = $find('<%= cbSalaryLevel.ClientID %>');
                clearSelectRadcombo(cbo);
                cbo = $find('<%= cbSalaryRank.ClientID %>');
                clearSelectRadcombo(cbo);
                cbo = $find('<%= basicSalary.ClientID %>');
                clearSelectRadnumeric(cbo);
                
            break;
            case '<%= cbSalaryLevel.ClientID %>':
                cbo = $find('<%= cbSalaryRank.ClientID %>');
                clearSelectRadcombo(cbo);
                cbo = $find('<%= basicSalary.ClientID %>');
                clearSelectRadnumeric(cbo);
                
                break; 
                case '<%= cbSalaryRank.ClientID %>':
                    cbo = $find('<%= basicSalary.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    var item = eventArgs.get_item();
                    if (item) {
                        cbo.set_value(item.get_attributes().getAttribute("SALARY_BASIC"));
                    }
                    break;
                default:
                    break;
            }
        }


        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {
                case '<%= cboAllowance.ClientID %>':
                    break;
                case '<%= cboSalType.ClientID %>':
                    cbo = $find('<%= rdEffectDate.ClientID %>');
                    var date = cbo.get_selectedDate();
                    if (date) {
                        var day = cbo.get_selectedDate().getDate();
                        var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                        var month = months[cbo.get_selectedDate().getMonth()];
                        var year = cbo.get_selectedDate().getFullYear();
                        value = day + "/" + month + "/" + year;
                    }
                    break;
               <%-- case '<%= cbSalaryGroup.ClientID %>':
                    cbo = $find('<%= rdEffectDate.ClientID %>');
                    var date = cbo.get_selectedDate();
                    if (date) {
                        var day = cbo.get_selectedDate().getDate();
                        var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                        var month = months[cbo.get_selectedDate().getMonth()];
                        var year = cbo.get_selectedDate().getFullYear();
                        value = day + "/" + month + "/" + year;
                    }
                    break; 
                case '<%= cbSalaryLevel.ClientID %>':
                    cbo = $find('<%= cbSalaryGroup.ClientID %>');
                    value = cbo.get_value();
                    break;--%>
              case '<%= cbSalaryRank.ClientID %>':
                    cbo = $find('<%= cbSalaryLevel.ClientID %>');
                    value = cbo.get_value();
                    break; 
                default:
                    break;
            }

            if (!value) {
                value = null;
            }
            var context = eventArgs.get_context();
            context["valueCustom"] = value;

        }

        function clearSelectRadcombo(cbo) {
            if (cbo) {
                cbo.clearItems();
                cbo.clearSelection();
                cbo.set_text('');
            }
        }
        
         function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function clearSelectRadnumeric(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

        function OnValueChanged(sender, args) {
            var id = sender.get_id();
            var objSalaryInsurance=$find('<%= rntxtSalaryInsurance.ClientID %>');

            var valueSalBasic = 0;
            var valuePercentSalary = 0;
            var valueSalRate = 0;
            var valueAllowance_Total = 0;

            var objSalBasic = $find('<%= basicSalary.ClientID %>');
            var objPercentSalary = $find('<%= rnPercentSalary.ClientID %>');
            var objSalRate=$find('<%= rnmtxtSalRate.ClientID %>');
            var objAllowance_Total = $find('<%= rntxtAllowance_Total.ClientID %>');

            if (objSalBasic.get_value()) {
                valueSalBasic = objSalBasic.get_value();                
            }
             if (objPercentSalary.get_value()) {
                valuePercentSalary = objPercentSalary.get_value();                 
            } 
            if (objSalRate.get_value()) {
                valueSalRate = objSalRate.get_value();
            }
            if (objAllowance_Total.get_value()) {
                valueAllowance_Total = objAllowance_Total.get_value();                  
            }
            objSalaryInsurance.clear();       
           objSalaryInsurance.set_value((valueSalBasic*valuePercentSalary*valueSalRate)+valueAllowance_Total);
        }      
    </script>
</tlk:RadCodeBlock>

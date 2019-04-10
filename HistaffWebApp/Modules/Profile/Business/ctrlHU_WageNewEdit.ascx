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
                    <%# Translate("Mã nhân viên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" Width="130px" SkinID="Readonly"
                        ReadOnly="true">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nhân viên %>" ToolTip="<%$ Translate: Bạn phải chọn Nhân viên %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 180px">
                    <%# Translate("Họ tên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 180px">
                    <%# Translate("Đơn vị")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <%-- <td class="lb">
                    <%# Translate("Nhóm chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTitleGroup" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>--%>
                <%--<td class="lb">
                    <%# Translate("Cấp nhân sự")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtStaffRank" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>--%>
                <td class="lb">
                    <%# Translate("Số tờ trình/QĐ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionNo" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <%--  <tr>
                <td class="lb">
                    <%# Translate("Loại tờ trình/QĐ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDecisionType" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusDecisionType" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Loại tờ trình/QĐ %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Loại tờ trình/QĐ  %>" ClientValidationFunction="cusDecisionType">
                    </asp:CustomValidator>
                </td>
                
            </tr>--%>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                        <%--<DateInput CausesValidation="false" AutoPostBack="true">
                        </DateInput>--%>
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" runat="server" ControlToValidate="rdEffectDate"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                    <%--<asp:CustomValidator ID="cusExistEffectDate" runat="server" ErrorMessage="<%$ Translate: Ngày hiệu lực phải lớn hơn ngày hiệu lực gần nhất %>"
                        ToolTip="<%$ Translate: Ngày hiệu lực phải lớn hơn ngày hiệu lực gần nhất %>">
                    </asp:CustomValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdExpireDate"
                        Type="Date" ControlToCompare="rdEffectDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực %>"
                        ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực %>"></asp:CompareValidator>
                </td>
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
                    <%# UI.Wage_WageGRoup %><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalTYPE" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        AutoPostBack="true" OnClientItemsRequesting="OnClientItemsRequesting" OnSelectedIndexChanged="cboSalTYPE_SelectedIndexChanged"
                        CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusSalType" runat="server" ErrorMessage="<%# GetYouMustChoseMsg(UI.Wage_WageGRoup) %>"
                        ToolTip="<%# GetYouMustChoseMsg(UI.Wage_WageGRoup) %>" ClientValidationFunction="cusSalType">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# UI.Wage_TaxTable %><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTaxTable" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusTaxTable" runat="server" ErrorMessage="<%#  GetYouMustChoseMsg(UI.Wage_TaxTable) %>"
                        ToolTip="<%#  GetYouMustChoseMsg(UI.Wage_TaxTable) %>" ClientValidationFunction="cusTaxTable">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# UI.Wage_BasicSalary %><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="basicSalary" runat="server" SkinID="Money" ClientEvents-OnValueChanged="OnBasicSalaryChanged">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldBasicSalary" ControlToValidate="basicSalary"
                        runat="server" ErrorMessage="<%#  GetYouMustChoseMsg(UI.Wage_BasicSalary) %>"
                        ToolTip="<%#  GetYouMustChoseMsg(UI.Wage_BasicSalary)%>"> 
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# UI.Wage_Sal_Ins %>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="SalaryInsurance" runat="server" SkinID="Money" Enabled="False">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# UI.Wage_Allowance_total %>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="Allowance_Total" runat="server" SkinID="Money" Enabled="False">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# UI.Wage_Salary_Total %>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="Salary_Total" runat="server" SkinID="Money" Enabled="False">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đối tượng Sale Commision")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSaleCommision" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        AutoPostBack="true" OnClientItemsRequesting="OnClientItemsRequesting" OnSelectedIndexChanged="cboSalTYPE_SelectedIndexChanged"
                        CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <%-- <tr>
                <td class="lb">
                    <%# Translate("Hệ số phụ cấp chức vụ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtHSPCChucVu" runat="server" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Hệ số phụ cấp trách nhiệm")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtHSPCTrachNhiem" runat="server" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Hệ số lương cơ bản")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSalBasic" runat="server" SkinID="Readonly" ReadOnly="true">
                        <ClientEvents OnValueChanged="OnValueChanged" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Hệ số thâm niên vượt khung")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtHSTNVuotKhung" runat="server" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Lương định mức")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtLuongDinhMuc" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Hệ số điều chỉnh")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtHeSoDieuChinh" runat="server" ReadOnly="true" SkinID="Readonly">
                        <ClientEvents OnValueChanged="OnValueChanged" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Phụ cấp trách nhiệm định mức")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtPCTNDinhMuc" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Thời gian giữ bậc")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtThoiGianBac" runat="server" ReadOnly="true" SkinID="Readonly">
                        <ClientEvents OnValueChanged="OnValueChanged" />
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí hỗ trợ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostSupport" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lương điều tiết")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtLuongDieuTiet" runat="server" ReadOnly="true" SkinID="Readonly">
                        <ClientEvents OnValueChanged="OnValueChanged" />
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tổng số bảo hiểm")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtTongSoBH" runat="server" ReadOnly="true" SkinID="Readonly">
                        <ClientEvents OnValueChanged="OnValueChanged" />
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Hệ số doanh nghiệp")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtTongHeSo" runat="server" ReadOnly="true" SkinID="Readonly">
                        <ClientEvents OnValueChanged="OnValueChanged" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>--%>
            <tr>
                <td colspan="6">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Phụ cấp")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboAllowance" runat="server" ValidationGroup="Allowance" SkinID="LoadDemand"
                        AutoPostBack="true" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Số tiền")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAmount" runat="server" SkinID="Money" ValidationGroup="Allowance">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdAllowEffectDate" runat="server">
                    </tlk:RadDatePicker>
                    <%--  <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="rdAllowExpireDate"
                        Type="Date" ControlToCompare="rdEffectDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn Ngày hiệu lực %>"
                        ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn Ngày hiệu lực %>"></asp:CompareValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdAllowExpireDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="rdAllowExpireDate"
                        Type="Date" ControlToCompare="rdEffectDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn Ngày hiệu lực %>"
                        ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn Ngày hiệu lực %>"></asp:CompareValidator>
                </td>
                <td>
                </td>
                <td>
                    <tlk:RadButton runat="server" ID="chkIsInsurrance" ToggleType="CheckBox" ButtonType="ToggleButton"
                        Enabled="false" CausesValidation="false" Text=" <%$ Translate: Đóng bảo hiểm %>"
                        AutoPostBack="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <tlk:RadGrid PageSize="50" ID="rgAllow" runat="server" Height="200px" Width="100%"
                        SkinID="GridNotPaging">
                        <MasterTableView DataKeyNames="ID,ALLOWANCE_LIST_ID,AMOUNT,IS_INSURRANCE,ALLOWANCE_LIST_NAME,EFFECT_DATE,EXPIRE_DATE"
                            ClientDataKeyNames="ID,ALLOWANCE_LIST_ID,AMOUNT,IS_INSURRANCE,ALLOWANCE_LIST_NAME,EFFECT_DATE,EXPIRE_DATE"
                            CommandItemDisplay="Top">
                            <CommandItemStyle Height="28px" />
                            <CommandItemTemplate>
                                <div style="padding: 2px 0 0 0">
                                    <div style="float: left">
                                        <tlk:RadButton ID="btnInsertAllowance" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/add.png"
                                            CausesValidation="false" Width="70px" Text="<%$ Translate: Thêm %>" CommandName="InsertAllow">
                                        </tlk:RadButton>
                                    </div>
                                    <div style="float: right">
                                        <tlk:RadButton ID="btnDeleteAllowance" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/delete.png"
                                            CausesValidation="false" Width="70px" Text="<%$ Translate: Xóa %>" CommandName="DeleteAllow"
                                            OnClientClicking="btnDeleteAllowanceClick">
                                        </tlk:RadButton>
                                    </div>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                                <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                </tlk:GridClientSelectColumn>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên phụ cấp %>" DataField="ALLOWANCE_LIST_NAME"
                                    SortExpression="ALLOWANCE_LIST_NAME" UniqueName="ALLOWANCE_LIST_NAME" />
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="AMOUNT"
                                    SortExpression="AMOUNT" UniqueName="AMOUNT" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đóng bảo hiểm %>" DataField="IS_INSURRANCE"
                                    SortExpression="IS_INSURRANCE" UniqueName="IS_INSURRANCE" HeaderStyle-Width="100px">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </tlk:GridCheckBoxColumn>--%>
                            </Columns>
                        </MasterTableView>
                    </tlk:RadGrid>
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
                    <tlk:RadComboBox ID="cboStatus" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusStatus" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Trạng thái %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Trạng thái  %>" ClientValidationFunction="cusStatus">
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
                    <tlk:RadTextBox ID="txtSignName" runat="server" SkinID="Readonly" ReadOnly="true"
                        Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindSign" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignTitle" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" Width="100%" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
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
            var cbo = $find("<%# cboSalGroup.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        } --%>
        function cusTaxTable(oSrc, args) {
            var cbo = $find("<%# cboTaxTable.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
      <%--   function cusSalLevel(oSrc, args) {
            var cbo = $find("<%# cboSalLevel.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        } --%>

       <%-- function cusSalRank(oSrc, args) {
            var cbo = $find("<%# cboSalRank.ClientID %>");
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
                <%-- var objSalBasic = $find('<%= rntxtSalBasic.ClientID %>');
                var objSalTotal = $find('<%= rntxtSalTotal.ClientID %>');
                var objCostSupport = $find('<%= rntxtCostSupport.ClientID %>');
                var objHSDieuChinh = $find('<%= rntxtHeSoDieuChinh.ClientID %>');
                var objTGGiuBac = $find('<%= rntxtThoiGianBac.ClientID %>');
                var objLuongDieuTiet = $find('<%= rntxtLuongDieuTiet.ClientID %>');--%>
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
             <%--    case '<%= cboSalGroup.ClientID %>':
                    cbo = $find('<%= cboSalLevel.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboSalRank.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= rntxtSalBasic.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    cbo = $find('<%= rntxtHeSoDieuChinh.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    cbo = $find('<%= rntxtTongHeSo.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    cbo = $find('<%= rntxtThoiGianBac.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    cbo = $find('<%= rntxtLuongDieuTiet.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    break; --%>
              <%--   case '<%= cboSalLevel.ClientID %>':
                    cbo = $find('<%= cboSalRank.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= rntxtSalBasic.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    cbo = $find('<%= rntxtHeSoDieuChinh.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    cbo = $find('<%= rntxtTongHeSo.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    cbo = $find('<%= rntxtThoiGianBac.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    cbo = $find('<%= rntxtLuongDieuTiet.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    break; --%>
                <%--case '<%= cboSalRank.ClientID %>':
                    cbo = $find('<%= rntxtSalBasic.ClientID %>');
                    cboHSDC = $find('<%= rntxtHeSoDieuChinh.ClientID %>');
                    cboTongHS = $find('<%= rntxtTongHeSo.ClientID %>');
                    cboTGGiuBac = $find('<%= rntxtThoiGianBac.ClientID %>');
                    cboLuongDieuTiet = $find('<%= rntxtLuongDieuTiet.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    clearSelectRadnumeric(cboHSDC);
                    clearSelectRadnumeric(cboTongHS);
                    clearSelectRadnumeric(cboTGGiuBac);
                    clearSelectRadnumeric(cboLuongDieuTiet);
                    var item = eventArgs.get_item();
                    if (item) {
                        cbo.set_value(item.get_attributes().getAttribute("SALARY_BASIC"));
                        cboHSDC.set_value(item.get_attributes().getAttribute("HS_DC"));
                        cboTongHS.set_value(item.get_attributes().getAttribute("TONG_HS"));
                        cboTGGiuBac.set_value(item.get_attributes().getAttribute("THOIGIAN_BAC"));
                        cboLuongDieuTiet.set_value(item.get_attributes().getAttribute("LUONG_DT"));
                    }
                    break;--%>
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
               <%-- case '<%= cboSalGroup.ClientID %>':
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
                case '<%= cboSalLevel.ClientID %>':
                    cbo = $find('<%= cboSalGroup.ClientID %>');
                    value = cbo.get_value();
                    break;--%>
              <%--   case '<%= cboSalRank.ClientID %>':
                    cbo = $find('<%= cboSalLevel.ClientID %>');
                    value = cbo.get_value();
                    break; --%>
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

        function clearSelectRadnumeric(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

        function OnBasicSalaryChanged(sender, args) {
            var basicSalary = $find('<%= basicSalary.ClientID %>').get_value();
            var salaryTotal = $find('<%= Salary_Total.ClientID %>');
            var salIn = $find('<%= SalaryInsurance.ClientID %>');
            var dataItems = $find('<%= rgAllow.ClientID %>').get_masterTableView().get_dataItems();
            var allowanceIns = 0;
            var allowanceTotal = 0;
            for (var i = 0; i < dataItems.length; i++) {
                var allowAmount = parseFloat(dataItems[i].getDataKeyValue("AMOUNT"));
                if (dataItems[i].getDataKeyValue("IS_INSURRANCE") === "True") {
                    allowanceIns += allowAmount;
                }
                allowanceTotal += allowAmount;
            }
            salaryTotal.set_value(basicSalary + allowanceTotal);
            salIn.set_value(basicSalary + allowanceIns);
        }
        function OnValueChanged(sender, args) {
            var id = sender.get_id();
            var valueSalBasic = 0;
            var valueSalTotal = 0;
            var valueHSDieuChinh = 0;
            var valueTGGiuBac = 0;
            var valueLuongDieuTiet = 0;
            <%--    var objSalBasic = $find('<%= rntxtSalBasic.ClientID %>');
           var objSalTotal = $find('<%= rntxtSalTotal.ClientID %>');
            var objHSDieuChinh = $find('<%= rntxtHeSoDieuChinh.ClientID %>');
            var objTongHS = $find('<%= rntxtTongHeSo.ClientID %>');
            var objTGGiuBac = $find('<%= rntxtThoiGianBac.ClientID %>');
            var objLuongDieuTiet = $find('<%= rntxtLuongDieuTiet.ClientID %>');
            if (objSalBasic.get_value()) {
                valueSalBasic = objSalBasic.get_value();
            }
             if (objSalTotal.get_value()) {
                valueSalTotal = objSalTotal.get_value();
            } 
            if (objHSDieuChinh.get_value()) {
                valueHSDieuChinh = objHSDieuChinh.get_value();
            } --%>
            <%-- switch (id) {
                case '<%= rntxtSalBasic.ClientID %>':
                    valueSalBasic = 0;
                    if (args.get_newValue()) {
                        valueSalBasic = args.get_newValue();
                    }
                    break;
                case '<%= rntxtSalTotal.ClientID %>':
                    valueSalTotal = 0;
                    if (args.get_newValue()) {
                        valueSalTotal = args.get_newValue();
                    }
                    break;
                case '<%= rntxtHeSoDieuChinh.ClientID %>':
                    valueHSDieuChinh = 0;
                    if (args.get_newValue()) {
                        valueHSDieuChinh = args.get_newValue();
                    }
                    break;
                case '<%= rntxtThoiGianBac.ClientID %>':
                    valueTGGiuBac = 0;
                    if (args.get_newValue()) {
                        valueTGGiuBac = args.get_newValue();
                    }
                    break;
                case '<%= rntxtLuongDieuTiet.ClientID %>':
                    valueLuongDieuTiet = 0;
                    if (args.get_newValue()) {
                        valueLuongDieuTiet = args.get_newValue();
                    }
                    break;
                default:
                    break;
           
            } 

            objCostSupport = $find('<%= rntxtCostSupport.ClientID %>');
            objCostSupport.set_value(0);
            if (valueSalBasic < valueSalTotal) objCostSupport.set_value(valueSalTotal - valueSalBasic - valueHSDieuChinh);
        --%>
        }

    </script>
</tlk:RadCodeBlock>

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
                    <asp:Label runat ="server" ID ="lbEmployeeCode" Text ="Mã nhân viên" ></asp:Label> <span class="lbReq">*</span>
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
                    <asp:Label runat ="server" ID ="lbEmployeeName" Text ="Họ tên" ></asp:Label> 
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbTitleName" Text ="Chức danh" ></asp:Label> 
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 180px">
                    <asp:Label runat ="server" ID ="lbOrgName" Text ="Đơn vị" ></asp:Label> 
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbDecisionNo" Text ="Số tờ trình/QĐ" ></asp:Label> 
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionNo" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbEffectDate" Text ="Ngày hiệu lực" ></asp:Label> <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" runat="server" ControlToValidate="rdEffectDate"
                        ErrorMessage="Bạn phải nhập ngày hiệu lực" ToolTip="Bạn phải nhập ngày hiệu lực"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbExpireDate" Text ="Ngày hết hiệu lực" ></asp:Label> 
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdExpireDate"
                        Type="Date" ControlToCompare="rdEffectDate" Operator="GreaterThanEqual" ErrorMessage="Ngày kết thúc phải lớn hơn ngày hiệu lực"
                        ToolTip="Ngày kết thúc phải lớn hơn ngày hiệu lực"></asp:CompareValidator>
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
                    <asp:Label runat ="server" ID ="lbSalaryGroup" Text="Thang lương"></asp:Label>
                </td>
                <td >
                    <tlk:RadComboBox ID="cbSalaryGroup" runat="server" AutoPostBack ="true" CausesValidation="false" >
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbSalaryLevel" Text="Ngạch lương"></asp:Label>
                </td>
                <td >
                    <tlk:RadComboBox ID="cbSalaryLevel" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbSalaryRank" Text="Bậc lương"></asp:Label>
                </td>
                <td >
                    <tlk:RadComboBox ID="cbSalaryRank" runat="server" AutoPostBack="true" CausesValidation="false" >
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                 <td class="lb">
                    <asp:Label runat ="server" ID="lbPercentSalary" Text ="% hưởng lương"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat ="server" ID ="rdPercentSalary" SkinID ="Decimal" ></tlk:RadNumericTextBox>
                </td>
                 <td class="lb">
                    <asp:Label runat ="server" ID="lbFactorSalary" Text ="Hệ số lương"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat ="server" ID ="rdFactorSalary" SkinID ="Decimal" ReadOnly ="true"  ></tlk:RadNumericTextBox>
                </td>
                 <td class="lb">
                    <asp:Label runat ="server" ID="lbOtherSalary1" Text ="Lương khác 1"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat ="server" ID ="rdOtherSalary1" SkinID ="Decimal" ></tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                 <td class="lb">
                    <asp:Label runat ="server" ID="lbOtherSalary2" Text ="Lương khác 2"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat ="server" ID ="rdOtherSalary2" SkinID ="Decimal" ></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID="lbOtherSalary3" Text ="Lương khác 3"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat ="server" ID ="rdOtherSalary3" SkinID ="Decimal" ></tlk:RadNumericTextBox>
                </td>
                 <td class="lb">
                    <asp:Label runat ="server" ID="lbOtherSalary4" Text ="Lương khác 4"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat ="server" ID ="rdOtherSalary4" SkinID ="Decimal" ></tlk:RadNumericTextBox>
                </td>
            </tr>
                 <td class="lb">
                    <asp:Label runat ="server" ID="lbOtherSalary5" Text ="Lương khác 5"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat ="server" ID ="rdOtherSalary5" SkinID ="Decimal" ></tlk:RadNumericTextBox>
                </td>
            <tr>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbSalTYPE" Text="Nhóm lương"></asp:Label> <span class="lbReq">*</span>
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
                     <asp:Label runat ="server" ID ="lbTaxTable" Text ="Biểu thuế"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTaxTable" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusTaxTable" runat="server" ErrorMessage="<%#  GetYouMustChoseMsg(UI.Wage_TaxTable) %>"
                        ToolTip="<%#  GetYouMustChoseMsg(UI.Wage_TaxTable) %>" ClientValidationFunction="cusTaxTable">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                     <asp:Label runat ="server" ID ="lbbasicSalary" Text ="Lương cơ bản"></asp:Label><span class="lbReq">*</span>
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
                    <asp:Label runat ="server" ID ="lbSalaryInsurance" Text ="Mức lương đóng bảo hiểm"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="SalaryInsurance" runat="server" SkinID="Money" Enabled="False">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbAllowance_Total" Text ="Tổng phụ cấp"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="Allowance_Total" runat="server" SkinID="Money" Enabled="False">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                     <asp:Label runat ="server" ID ="lbSalary_Total" Text ="Tổng mức lương"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="Salary_Total" runat="server" SkinID="Money" Enabled="False">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbSaleCommision" Text ="Đối tượng Sale Commision"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSaleCommision" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        AutoPostBack="true" OnClientItemsRequesting="OnClientItemsRequesting" OnSelectedIndexChanged="cboSalTYPE_SelectedIndexChanged"
                        CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbAllowance" Text ="Phụ cấp"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboAllowance" runat="server" ValidationGroup="Allowance" SkinID="LoadDemand"
                        AutoPostBack="true" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbAmount" Text ="Số tiền"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAmount" runat="server" SkinID="Money" ValidationGroup="Allowance">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbAllowEffectDate" Text ="Ngày hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdAllowEffectDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbAllowExpireDate" Text ="Ngày hết hiệu lực"></asp:Label>
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
                        Enabled="false" CausesValidation="false" Text=" Đóng bảo hiểm "
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
                               <%-- <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
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

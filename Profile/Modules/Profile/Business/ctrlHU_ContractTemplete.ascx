<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ContractTemplete.ascx.vb"
    Inherits="Profile.ctrlHU_ContractTemplete" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidWorkingID" runat="server" />
<asp:HiddenField ID="hidEmployeeCode" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hiOrgIDEmp" runat="server" />
<asp:HiddenField ID="hidSalary" runat="server" />
<asp:HiddenField ID="hidPeriod" runat="server" />
<asp:HiddenField ID="hidLiquiDate" runat="server" />
<asp:HiddenField ID="hidContract_ID" runat="server" />
<asp:HiddenField ID="hidEmployeeID" runat="server" />
<asp:HiddenField ID="hidContractType_ID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarContract" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="NORMAL" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <fieldset>
            <legend>
                <%# Translate("Thông tin nhân viên")%>
            </legend>
            <table onkeydown="return (event.keyCode!=13)" class="table-form">
                <tr>
                    <td class="lb" style="width: 10%">
                        <%# Translate("MSNV")%>
                        <span class="lbReq">*</span>
                    </td>
                    <td style="width: 23%">
                        <tlk:RadTextBox ID="txtEmployeeCode" SkinID="Textbox15" runat="server" Width="130px">
                        </tlk:RadTextBox>
                        <tlk:RadButton EnableEmbeddedSkins="false" ID="btnEmployee" SkinID="ButtonView" runat="server"
                            CausesValidation="false" Width="40px">
                        </tlk:RadButton>
                        <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên. %>" ToolTip="<%$ Translate: Bạn phải chọn nhân viên. %>"> </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb" style="width: 10%">
                        <%# Translate("Họ và tên nhân viên")%>
                    </td>
                    <td style="width: 23%">
                        <tlk:RadTextBox ID="txtEmployeeName" runat="server">
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb" style="width: 10%">
                        <%# Translate("Phòng ban")%>
                    </td>
                    <td style="width: 23%">
                        <tlk:RadTextBox ID="txtOrg" runat="server">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Hợp đồng")%><span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboContract" runat="server" CausesValidation="false" AutoPostBack="true">
                        </tlk:RadComboBox>
                        <asp:RequiredFieldValidator ID="reqContract" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn hợp đồng %>"
                            ToolTip="<%$ Translate: Bạn phải chọn hợp đồng %>" ControlToValidate="cboContract">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <%# Translate("Loại phụ lục")%><span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboAppend_TypeID" runat="server" CausesValidation="false" AutoPostBack="true">
                        </tlk:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn loại phụ lục hợp đồng %>"
                            ToolTip="<%$ Translate: Bạn phải chọn loại phụ lục hợp đồng %>" ControlToValidate="cboAppend_TypeID">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <%# Translate("Chức danh")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtTitle" runat="server">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Số phụ lục HĐLĐ")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtContract_NumAppen" runat="server" ReadOnly="true">
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb">
                        <%# Translate("Ngày bắt đầu")%>
                        <span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadDatePicker ID="rdStartDate" runat="server" AutoPostBack = "true" >
                        </tlk:RadDatePicker>
                        <asp:RequiredFieldValidator ID="reqStartDate" ControlToValidate="rdStartDate" runat="server"
                            ErrorMessage="<%$ Translate: Bạn phải nhập ngày bắt đầu. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày bắt đầu. %>"> </asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CompareStartDate" runat="server" ErrorMessage="<%$ Translate: Ngày hiệu lực phải thuộc khoảng thời gian hiệu lực của hợp đồng. %>"
                            ToolTip="<%$ Translate: Ngày hiệu lực phải lớn hơn thời gian hiệu lực của hợp đồng. %>"></asp:CustomValidator>
                    </td>
                    <td class="lb">
                        <%# Translate("Ngày kết thúc")%><%--<span class="lbReq">*</span>--%>
                    </td>
                    <td>
                        <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                        </tlk:RadDatePicker>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rdExpireDate" runat="server"
                            ErrorMessage="<%$ Translate: Bạn phải nhập ngày kết thúc. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày kết thúc. %>"> </asp:RequiredFieldValidator>                        --%>
                        <asp:CustomValidator ID="cval_EffectDate_ExpireDate" runat="server" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực. %>"
                            ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực. %>">
                        </asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Ngày ký")%>
                    </td>
                    <td>
                        <tlk:RadDatePicker ID="rdSignDate" runat="server" DateInput-DisplayDateFormat="dd/MM/yyyy">
                        </tlk:RadDatePicker>
                        <%--<asp:RequiredFieldValidator ID="reqSignDate" ControlToValidate="rdSignDate" runat="server"
                            ErrorMessage="<%$ Translate: Bạn phải nhập ngày ký. %>" ToolTip="<%$ Translate: Bạn phải nhập
                        ngày ký. %>"> </asp:RequiredFieldValidator>--%>
                    </td>
                    <td class="lb">
                        <%# Translate("Người ký")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtSign" SkinID="Textbox15" runat="server" Width="130px" ReadOnly="true">
                        </tlk:RadTextBox>
                        <tlk:RadButton EnableEmbeddedSkins="false" ID="btnSign" SkinID="ButtonView" runat="server"
                            CausesValidation="false" Width="40px">
                        </tlk:RadButton>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtSign"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Người ký. %>" ToolTip="<%$ Translate: Bạn phải chọn Người ký %>"> 
                        </asp:RequiredFieldValidator>--%>
                    </td>
                    <td class="lb">
                        <%# Translate("Chức vụ người ký")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtSign_Title" runat="server" ReadOnly = "true" >
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Nội dung thay đổi")%><%--<span class="lbReq">*</span>--%>
                    </td>
                    <td colspan="5">
                        <tlk:RadTextBox ID="txtAppend_Content" runat="server" Width="95%">
                        </tlk:RadTextBox>
                       <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAppend_Content"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nội dung thay đổi. %>" ToolTip="<%$ Translate: Bạn phải nhập nội dung thay đổi. %>"> </asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Ghi chú")%>
                    </td>
                    <td colspan="5">
                        <tlk:RadTextBox ID="txtRemark" runat="server" Width="95%">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Trạng Thái")%><span class="lbReq">*</span>
                    </td>
                    <td colspan="5">
                        <tlk:RadComboBox ID="cboStatus_ID" runat="server">
                        </tlk:RadComboBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Trạng thái %>"
                            ToolTip="<%$ Translate: Bạn phải chọn Trạng thái %>" ControlToValidate="cboStatus_ID">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <%--THÔNG TIN LƯƠNG--%>
                <tr>
                    <td colspan="6">
                        <b>
                            <%# Translate("Thông tin lương")%></b>
                        <hr />
                    </td>
                </tr>
                <%--<tr>
                    <td class="lb">
                        <%# Translate("Chọn tờ trình/QĐ")%><span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtDesionNo" runat="server" ReadOnly="true" Width="130px">
                        </tlk:RadTextBox>
                        <tlk:RadButton ID="btnSalary" SkinID="ButtonView" runat="server" CausesValidation="false">
                        </tlk:RadButton>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtDesionNo"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải Chọn tờ trình/QĐ. %>" ToolTip="<%$ Translate: Bạn phải Chọn tờ trình/QĐ. %>"> </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <%# Translate("Mức lương")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtBasicSal" runat="server" MinValue="0" SkinID="Money"
                            ReadOnly="true">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                        <%# Translate("Thưởng hiệu quả công việc")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtMucBonus" runat="server" SkinID="Money">
                        </tlk:RadNumericTextBox>
                    </td>
                 </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("BHXH")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtSal_BHXH" runat="server" SkinID="Money" ReadOnly="True">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                        <%# Translate("BHYT")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtSal_BHYT" runat="server" SkinID="Money" ReadOnly="True">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                        <%# Translate("BHTN")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtSal_BHTN" runat="server" SkinID="Money" ReadOnly="True">
                        </tlk:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Tỷ lệ nghỉ hàng năm")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtPecentYear" runat="server" SkinID="Money" ReadOnly="True">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                        <%# Translate("Lương nghỉ hàng năm")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtLeaveYear" runat="server" SkinID="Money" ReadOnly="True">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                        <%# Translate("Tổng tiền lương")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtTotalSalary" runat="server" SkinID="Money" ReadOnly="True">
                        </tlk:RadNumericTextBox>
                    </td>
                </tr>--%>
                <tr>
                <td class="lb">
                    <%# Translate("Chọn Hồ Sơ Lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="Working_ID" runat="server" ReadOnly="true" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnSalary" SkinID="ButtonView" runat="server" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <%# UI.Wage_WageGRoup %><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalTYPE" runat="server" SkinID="LoadDemand" Enabled="False"></tlk:RadComboBox>
                   <%-- <asp:CustomValidator ID="cusSalType" runat="server" ErrorMessage="<%= groupWageWarning %>"
                                         ToolTip="<%= groupWageWarning %>" ClientValidationFunction="cusSalType">
                    </asp:CustomValidator>--%>
                </td>
                <td class="lb">
                    <%# UI.Wage_TaxTable %><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTaxTable" runat="server" SkinID="LoadDemand" Enabled="False"></tlk:RadComboBox>                  
                </td>               
            </tr>
            <tr>
                <td class="lb">
                    <%# UI.Wage_BasicSalary %><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtBasicSal" runat="server" MinValue="0" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# UI.Wage_Sal_Ins %>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="SalaryInsurance"  runat="server" SkinID="Money" Enabled="False" >
                    </tlk:RadNumericTextBox>                                     
                </td>
                <td class="lb">
                    <%# UI.Wage_Allowance_total %>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="Allowance_Total"  runat="server" SkinID="Money" Enabled="False">
                    </tlk:RadNumericTextBox> 
                </td>
                
            </tr>
            <tr>
                <td class="lb">
                    <%# UI.Wage_Salary_Total %>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="Salary_Total" runat="server" SkinID="Money" Enabled="False">
                    </tlk:RadNumericTextBox>                  
                </td>
            </tr>
                <tr>
                    <td colspan="6">
                        <tlk:RadGrid ID="rgAllow" runat="server" Height="160px" Width="98%" SkinID="GridNotPaging">
                            <MasterTableView DataKeyNames="ID,ALLOWANCE_LIST_ID,AMOUNT,IS_INSURRANCE,ALLOWANCE_LIST_NAME,EFFECT_DATE,EXPIRE_DATE"
                                ClientDataKeyNames="ID,ALLOWANCE_LIST_ID,AMOUNT,IS_INSURRANCE,ALLOWANCE_LIST_NAME,EFFECT_DATE,EXPIRE_DATE">
                                <Columns>
                                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên phụ cấp %>" DataField="ALLOWANCE_LIST_NAME"
                                        SortExpression="ALLOWANCE_LIST_NAME" UniqueName="ALLOWANCE_LIST_NAME" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="AMOUNT"
                                        SortExpression="AMOUNT" UniqueName="AMOUNT" DataFormatString="{0:n0}">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    </tlk:GridNumericColumn>
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                        ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                                        DataFormatString="{0:dd/MM/yyyy}" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                                        ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                                        DataFormatString="{0:dd/MM/yyyy}" Display="false" />
                                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đóng bảo hiểm %>" DataField="IS_INSURRANCE"
                                        SortExpression="IS_INSURRANCE" UniqueName="IS_INSURRANCE" HeaderStyle-Width="100px"
                                        Display="False">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </tlk:GridCheckBoxColumn>
                                </Columns>
                            </MasterTableView>
                        </tlk:RadGrid>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <%# Translate("Danh sách phụ lục hợp đồng")%>
            </legend>
            <tlk:RadGrid ID="rgContract" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                Height="230px" AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings AllowColumnsReorder="True" EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling UseStaticHeaders="true" AllowScroll="True" />
                    <Resizing AllowColumnResize="true" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,APPEND_NUMBER,START_DATE,EXPIRE_DATE">
                    <Columns>
                        <tlk:GridBoundColumn DataField="ID" Visible="false" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Số PLHĐ  %>" DataField="APPEND_NUMBER"
                            SortExpression="APPEND_NUMBER" UniqueName="APPEND_NUMBER">
                            <HeaderStyle Width="250px" />
                            <ItemStyle Width="250px" />
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày bắt đầu %>" DataField="START_DATE"
                            ItemStyle-HorizontalAlign="Center" SortExpression="START_DATE" UniqueName="START_DATE"
                            DataFormatString="{0:dd/MM/yyyy}" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="EXPIRE_DATE"
                            ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                            DataFormatString="{0:dd/MM/yyyy}" />
                    </Columns>
                </MasterTableView>
            </tlk:RadGrid>
        </fieldset>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
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

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSigner" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSalary" runat="server"></asp:PlaceHolder>

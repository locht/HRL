<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ContractNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_ContractNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgCode" runat="server" />
<asp:HiddenField ID="hidWorkingID" runat="server" />
<asp:HiddenField ID="hidWorkStatus" runat="server" />
<asp:HiddenField ID="hidEmployeeID" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<asp:HiddenField ID="hidPeriod" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarContract" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin hợp đồng")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb"  style="width: 200px">
                    <%# Translate("Mã nhân viên")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" SkinID="ReadOnly" runat="server" Width="130px"
                        ReadOnly="True">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 200px">
                    <%# Translate("Họ tên nhân viên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 200px">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTITLE" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đơn vị")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrg_Name" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <%--<td class="lb">
                    <%# Translate("Cấp nhân sự")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSTAFF_RANK" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>--%>
                <td class="lb">
                    <%# Translate("Loại hợp đồng")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboContractType" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusContractType" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Loại hợp đồng %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Loại hợp đồng %>" ClientValidationFunction="cusContractType">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cusvalContractType" ControlToValidate="cboContractType" runat="server" ErrorMessage="<%$ Translate: Loại hợp đồng không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Loại hợp đồng không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số hợp đồng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContractNo" runat="server" Enabled="false">
                    </tlk:RadTextBox>
<%--                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtContractNo"
                        runat="server" ErrorMessage="<%$ Translate: Số HĐ chưa được tạo %>" ToolTip="<%$ Translate: Số HĐ chưa được tạo %>"> </asp:RequiredFieldValidator>--%>
                      <%-- <asp:CustomValidator ID="cusContractNo" runat="server" ErrorMessage="<%$ Translate: Số hợp đồng đã tồn tại %>"
                        ToolTip="<%$ Translate: Số hợp đồng đã tồn tại %>">
                    </asp:CustomValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Ngày bắt đầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdStartDate" runat="server" AutoPostBack="True">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqStartDate" ControlToValidate="rdStartDate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày bắt đầu. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày bắt đầu. %>"> </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CompareStartDate" runat="server" ErrorMessage="<%$ Translate: Ngày bắt đầu phải lớn hơn ngày kết thúc gần nhất %>"
                        ToolTip="<%$ Translate: Ngày bắt đầu phải lớn hơn ngày kết thúc gần nhất %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày kết thúc")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdExpireDate"
                        Type="Date" ControlToCompare="rdStartDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn ngày bắt đầu %>"
                        ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn ngày bắt đầu %>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td>
                    <tlk:RadTextBox ID="rtAttachFile" runat="server" Width="300px" MaxLength="200" Height="22" />
                </td>
                <td class="lb" style="text-align:left">
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnUpload" CausesValidation="false" runat="server"  Text="<%$ Translate: CM_CTRLPROGRAMS_BROWSER %>">
                    </tlk:RadButton>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="rbtSQLLoaderDownload" runat="server"
                        CausesValidation="false" OnClientClicked="rbtClicked" Text="<%$ Translate: CM_CTRLPROGRAMS_SQLLOADER_DOWNLOAD %>">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trạng thái")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusStatus" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Trạng thái %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Trạng thái %>" ClientValidationFunction="cusStatus">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cusvalStatus" ControlToValidate="cboStatus" runat="server" ErrorMessage="<%$ Translate: Trạng thái không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Trạng thái không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <%--<td class="lb">
                    <%# Translate("Buổi sáng bắt đầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdMorning_Start">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                        </DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdMorning_Start"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập buổi sáng bắt đầu. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Buổi sáng kết thúc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdMorning_Stop">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                        </DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdMorning_Stop"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập buổi sáng kết thúc. %>"></asp:RequiredFieldValidator>
                </td>--%>
            </tr>
          <%--  <tr>
                <td class="lb">
                </td>
                <td>
                </td>
                <td class="lb">
                    <%# Translate("Buổi chiều bắt đầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdAfternoon_Start">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                        </DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdAfternoon_Start"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập buổi chiều bắt đầu. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Buổi chiều kết thúc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdAfternoon_Stop">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                        </DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rdAfternoon_Stop"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập buổi chiều kết thúc. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>--%>
            <tr>
                <td class="lb">
                    <%# Translate("Người ký")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSigner" runat="server" Width="130px" ReadOnly="true" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnSigner" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh ký")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignTitle" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày ký")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSignDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin lương")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chọn Hồ Sơ Lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="Working_ID" runat="server" ReadOnly="true" Width="130px" SkinID="ReadOnly">
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
                    <tlk:RadNumericTextBox ID="rntxtBasicSal" runat="server" MinValue="0" SkinID="ReadOnly" ReadOnly="true">
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
                   <tlk:RadGrid PageSize="50" ID="rgAllow" runat="server" 
                        SkinID="GridNotPaging">
                        <MasterTableView DataKeyNames="ID,ALLOWANCE_LIST_ID,AMOUNT,IS_INSURRANCE,ALLOWANCE_LIST_NAME,EFFECT_DATE,EXPIRE_DATE"
                            ClientDataKeyNames="ID,ALLOWANCE_LIST_ID,AMOUNT,IS_INSURRANCE,ALLOWANCE_LIST_NAME,EFFECT_DATE,EXPIRE_DATE"
                           >                            
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
                                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đóng bảo hiểm %>" DataField="IS_INSURRANCE"
                                    SortExpression="IS_INSURRANCE" UniqueName="IS_INSURRANCE" HeaderStyle-Width="100px">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </tlk:GridCheckBoxColumn>
                            </Columns>
                        </MasterTableView>
                    </tlk:RadGrid>
           </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_ContractNewEdit_RadSplitter1');
        });

        function cusContractType(oSrc, args) {
            var cbo = $find("<%# cboContractType.ClientID%>");
            args.IsValid = (cbo.get_value().length != 0);
        }
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


        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function cusSalType(oSrc, args) {
            var cbo = $find("<%# cboSalTYPE.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSigner" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSalary" runat="server"></asp:PlaceHolder>

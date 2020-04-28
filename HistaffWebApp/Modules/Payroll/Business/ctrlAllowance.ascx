<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAllowance.ascx.vb"
    Inherits="Payroll.ctrlAllowance" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidEmp" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane4" runat="server" Width="270px" Scrolling="None">
        <fieldset style="padding: 0">
            <legend>Thông tin tìm kiếm:</legend>
            <table class="table-form">
                <tr>
                    <td style="min-width: 70px" class="lb">
                        <%# Translate("Mã nhân viên")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtEmployeeCode" runat="server" Width="140px">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Từ ngày")%>
                    </td>
                    <td>
                        <tlk:RadDatePicker runat="server" ID="rdTuNgay">
                        </tlk:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Đến ngày")%>
                    </td>
                    <td>
                        <tlk:RadDatePicker runat="server" ID="rdDenNgay">
                        </tlk:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Loại phụ cấp")%>
                    </td>
                    <td>
                        <tlk:RadComboBox runat="server" ID="cbPhucap">
                        </tlk:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        NV nghỉ việc
                    </td>
                    <td>
                        <tlk:RadButton ID="chkNhanvienghiviec" AutoPostBack="false" ToggleType="CheckBox"
                            ButtonType="ToggleButton" runat="server" Text="" CausesValidation="False">
                        </tlk:RadButton>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                    </td>
                    <td>
                        <tlk:RadButton runat="server" Text="Tìm kiếm" ID="btnSearch" SkinID="ButtonFind"
                            CausesValidation="false">
                        </tlk:RadButton>
                    </td>
                </tr>
            </table>
        </fieldset>
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None" Height="100%">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPaneTop" runat="server" Height="30px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMenu" runat="server" OnClientButtonClicking="clientButtonClicking">
                </tlk:RadToolBar>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="150px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã nhân viên")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtCode" runat="server" ReadOnly="true">
                            </tlk:RadTextBox>
                            <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false"
                                Text="...">
                            </tlk:RadButton>
                            <asp:RequiredFieldValidator ID="rqCode" ControlToValidate="txtCode" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên %>" ToolTip="<%$ Translate: Bạn phải chọn nhân viên %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Họ và tên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtTennhanvien" runat="server" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Vị trí công việc")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtTitle" runat="server" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Đơn vị/phòng ban")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Loại phụ cấp, trợ cấp")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPhucap" runat="server" SkinID="dDropdownList">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="rqPhuCap" ControlToValidate="cboPhucap" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải chọn phụ cấp %>" ToolTip="<%$ Translate: Bạn phải chọn phụ cấp %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Số tiền")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="txtSotien" runat="server">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="rqSotien" ControlToValidate="txtSotien" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải nhập số tiền %>" ToolTip="<%$ Translate: Bạn phải nhập số tiền %>">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="dpTungay" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="rqTungay" ControlToValidate="dpTungay" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải nhập Ngày hiệu lực %>" ToolTip="<%$ Translate: Bạn phải nhập Ngày hiệu lực %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày kết hiệu lực")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="dpDenngay" runat="server">
                            </tlk:RadDatePicker>
                            <asp:CompareValidator ID="cpDenngay" runat="server" ToolTip="<%$ Translate: Ngày hiệu lưc phải nhỏ hơn ngày kết thúc %>"
                                ErrorMessage="<%$ Translate: Ngày hiệu lưc phải nhỏ hơn ngày kết thúc %>" Type="Date"
                                Operator="GreaterThan" ControlToCompare="dpTungay" ControlToValidate="dpDenngay"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="4">
                            <tlk:RadTextBox ID="txtGhichu" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPaneBotton" runat="server" Scrolling="None">
                <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
                    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                        <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%">
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,FULLNAME_VN,ALLOWANCE_TYPE,ALLOWANCE_TYPE_NAME,AMOUNT,EFFECT_DATE,EXP_DATE,ACTFLG,REMARK,TITLE_NAME,ORG_NAME">
                                <Columns>
                                    <tlk:GridClientSelectColumn UniqueName="CHK" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="100px" HeaderText="<%$ Translate: Mã nhân viên %>"
                                        DataField="EMPLOYEE_ID" SortExpression="EMPLOYEE_ID" UniqueName="EMPLOYEE_ID"
                                        Display="False" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="100px" HeaderText="<%$ Translate: Mã nhân viên %>"
                                        DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Họ và tên %>"
                                        DataField="FULLNAME_VN" SortExpression="FULLNAME_VN" UniqueName="FULLNAME_VN" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Vị trí công việc %>"
                                        DataField="TITLE_NAME" SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Đơn vị/phòng ban %>"
                                        DataField="ORG_NAME" SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Loại phụ cấp %>"
                                        DataField="ALLOWANCE_TYPE_NAME" SortExpression="ALLOWANCE_TYPE_NAME" UniqueName="ALLOWANCE_TYPE_NAME" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Tiền phụ cấp %>"
                                        DataField="AMOUNT" SortExpression="AMOUNT" UniqueName="AMOUNT" />
                                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                        SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}">
                                        <HeaderStyle Width="120px" />
                                        <ItemStyle Width="120px" />
                                    </tlk:GridDateTimeColumn>
                                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXP_DATE"
                                        SortExpression="EXP_DATE" UniqueName="EXP_DATE" DataFormatString="{0:dd/MM/yyyy}">
                                        <HeaderStyle Width="120px" />
                                        <ItemStyle Width="120px" />
                                    </tlk:GridDateTimeColumn>
                                    <%--      <tlk:GridBoundColumn HeaderStyle-Width="100px" HeaderText="<%$ Translate: Trạng thái %>"
                                        DataField="ACTFLG" SortExpression="ACTFLG" UniqueName="ACTFLG" />--%>
                                    <tlk:GridBoundColumn HeaderStyle-Width="100px" HeaderText="<%$ Translate: Ghi chú %>"
                                        DataField="REMARK" SortExpression="REMARK" UniqueName="REMARK" />
                                </Columns>
                            </MasterTableView>
                        </tlk:RadGrid>
                    </tlk:RadPane>
                </tlk:RadSplitter>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<script type="text/javascript">

    function clientButtonClicking(sender, args) {
        if (args.get_item().get_commandName() == 'EXPORT') {
            enableAjax = false;
        }
    }
    var enableAjax = true;
    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }

</script>

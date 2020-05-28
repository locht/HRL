<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_PlanNewEdit.ascx.vb"
    Inherits="Training.ctrlTR_PlanNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<style type="text/css">
    #ctl00_MainContent_ctrlTR_PlanNewEdit_txtWork_Relation
    {
        height: 80px !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin chung")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <%--  <td rowspan="5">
                            <tlk:RadGrid ID="rgISP" runat="server" Height="180px" Width="290px" Style="overflow: auto">
                                <MasterTableView DataKeyNames="CODE" ClientDataKeyNames="CODE,NAME_VN,MONEY">
                                    <Columns>
                                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã  %>" DataField="CODE"
                                            UniqueName="CODE" SortExpression="CODE" Visible="false" />
                                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên %>" DataField="NAME_VN"
                                            UniqueName="NAME_VN" SortExpression="NAME_VN" />
                                        <tlk:GridTemplateColumn HeaderText="<%$ Translate: Số tiền %>" SortExpression="MONEY_TEMP"
                                            UniqueName="MONEY_TEMP">
                                            <ItemTemplate>
                                                <tlk:RadNumericTextBox ID="MONEY" class="MoneyValue" runat="server" CausesValidation="false"
                                                    Width="100%" onchange="txtMoney_textChange()" Value='<%# Cint(Eval("MONEY")) %>'>
                                                    <NumberFormat AllowRounding="false" DecimalDigits="0" DecimalSeparator="." GroupSeparator=","
                                                        GroupSizes="3" />
                                                </tlk:RadNumericTextBox>
                                            </ItemTemplate>
                                        </tlk:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </tlk:RadGrid>
                        </td>--%>
                <td class="lb">
                    <%# Translate("Phòng ban")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindOrg" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Phòng ban %>" ToolTip="<%$ Translate: Bạn phải nhập Phòng ban %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Năm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtYear" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="" MinValue="1900" MaxLength="2999">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqYear" ControlToValidate="rntxtYear" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Năm đào tạo %>" ToolTip="<%$ Translate: Bạn phải nhập Năm đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Trạng thái")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboStatus">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhóm chương trình")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboGrProgram" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="rqGrProgram" ControlToValidate="cboGrProgram" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn Nhóm chương trình %>" ToolTip="<%$ Translate: Bạn phải chọn Nhóm chương trình %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Khóa đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCourse" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusCourse" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn khóa đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn khóa đào tạo %>" ClientValidationFunction="cusCourse">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkEvaluate" Text="Có đánh giá ứng dụng hay không?" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên chương trình đào tạo")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtName" Width="100%" SkinID="Textbox1023">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Tên chương trình đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Tên chương trình đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Mức độ ưu tiên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnPrioty" MaxLength="1">
                    </tlk:RadNumericTextBox>
                    <%# Translate("(Nhập từ 1 đến 5 theo thứ tự giảm dần)")%>
                    <asp:RequiredFieldValidator ID="rqPrioty" ControlToValidate="rnPrioty" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mức độ ưu tiên %>" ToolTip="<%$ Translate: Bạn phải nhập Mức độ ưu tiên %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Hình thức")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboHinhThuc" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusHinhThuc" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn hình thức đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn khóa đào tạo %>" ClientValidationFunction="cusHinhThuc">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tính chất nhu cầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboTinhchatnhucau" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusTinhchatnhucau" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Tính chất nhu cầu %>"
                        ToolTip="<%$ Translate: Bạn phải chọn tính chất nhu cầu %>" ClientValidationFunction="cusCourse">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị thời lượng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtDuration" runat="server" NumberFormat-GroupSeparator="."
                        NumberFormat-DecimalSeparator="," NumberFormat-DecimalDigits="1" MinValue="0"
                        Width="100px">
                    </tlk:RadNumericTextBox>
                    <tlk:RadComboBox runat="server" ID="cboDurationType">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqDuration" ControlToValidate="rntxtDuration" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn Thời lượng %>" ToolTip="<%$ Translate: Bạn phải chọn Thời lượng %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="display: none">
                    <%# Translate("Lĩnh vực đào tạo")%>
                </td>
                <td style="display: none">
                    <tlk:RadTextBox runat="server" ID="txtLinhvuc" ReadOnly="true" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td rowspan="6" class="lb">
                    <%# Translate("Thời gian dự kiến tổ chức")%>
                    <span class="lbReq">*</span>
                </td>
                <td rowspan="6">
                    <table style="border: 1px solid #abc1de">
                        <tr>
                            <td>
                                <tlk:RadButton ID="cbJan" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                    Text="<%$ Translate: Tháng 1%>" CausesValidation="false" AutoPostBack="false">
                                </tlk:RadButton>
                            </td>
                            <td>
                                <tlk:RadButton ID="cbFeb" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                    Text="<%$ Translate: Tháng 2%>" CausesValidation="false" AutoPostBack="false">
                                </tlk:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <tlk:RadButton ID="cbMar" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                    Text="<%$ Translate: Tháng 3%>" CausesValidation="false" AutoPostBack="false">
                                </tlk:RadButton>
                            </td>
                            <td>
                                <tlk:RadButton ID="cbApr" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                    Text="<%$ Translate: Tháng 4%>" CausesValidation="false" AutoPostBack="false">
                                </tlk:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <tlk:RadButton ID="cbMay" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                    Text="<%$ Translate: Tháng 5%>" CausesValidation="false" AutoPostBack="false">
                                </tlk:RadButton>
                            </td>
                            <td>
                                <tlk:RadButton ID="cbJun" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                    Text="<%$ Translate: Tháng 6%>" CausesValidation="false" AutoPostBack="false">
                                </tlk:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <tlk:RadButton ID="cbJul" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                    Text="<%$ Translate: Tháng 7%>" CausesValidation="false" AutoPostBack="false">
                                </tlk:RadButton>
                            </td>
                            <td>
                                <tlk:RadButton ID="cbAug" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                    Text="<%$ Translate: Tháng 8%>" CausesValidation="false" AutoPostBack="false">
                                </tlk:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <tlk:RadButton ID="cbSep" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                    Text="<%$ Translate: Tháng 9%>" CausesValidation="false" AutoPostBack="false">
                                </tlk:RadButton>
                            </td>
                            <td>
                                <tlk:RadButton ID="cbOct" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                    Text="<%$ Translate: Tháng 10%>" CausesValidation="false" AutoPostBack="false">
                                </tlk:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <tlk:RadButton ID="cbNov" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                    Text="<%$ Translate: Tháng 11%>" CausesValidation="false" AutoPostBack="false">
                                </tlk:RadButton>
                            </td>
                            <td>
                                <tlk:RadButton ID="cbDec" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                    Text="<%$ Translate: Tháng 12%>" CausesValidation="false" AutoPostBack="false">
                                </tlk:RadButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số lượng học viên dự kiến")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtStudents" runat="server" ReadOnly="true" NumberFormat-GroupSeparator="">
                    </tlk:RadNumericTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindMEmp" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboCourse"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số lượng học viên %>"
                        ToolTip="<%$ Translate: Bạn phải nhập số lượng học viên %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="display: none">
                    <%# Translate("Số lượng giảng viên")%>
                </td>
                <td style="display: none">
                    <tlk:RadNumericTextBox ID="rntxtTutors" runat="server" NumberFormat-GroupSeparator=""
                        NumberFormat-DecimalDigits="0" MinValue="0">
                    </tlk:RadNumericTextBox>
                </td>
                <td rowspan="6" colspan="2">
                    <tlk:RadGrid ID="rgISP" runat="server" Height="180px" Width="355px" Style="overflow: auto">
                        <MasterTableView DataKeyNames="CODE" ClientDataKeyNames="CODE,NAME_VN,MONEY">
                            <ColumnGroups>
                                <tlk:GridColumnGroup Name="GeneralInformation" HeaderText="Chi phí *" HeaderStyle-HorizontalAlign="Center" />
                            </ColumnGroups>
                            <Columns>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã  %>" DataField="CODE" UniqueName="CODE"
                                    SortExpression="CODE" Visible="false" ColumnGroupName="GeneralInformation" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi phí %>" DataField="NAME_VN" UniqueName="NAME_VN"
                                    SortExpression="NAME_VN" ColumnGroupName="GeneralInformation" />
                                <tlk:GridTemplateColumn HeaderText="<%$ Translate: Số tiền %>" SortExpression="MONEY_TEMP"
                                    UniqueName="MONEY_TEMP" ColumnGroupName="GeneralInformation">
                                    <ItemTemplate>
                                        <tlk:RadNumericTextBox ID="MONEY" class="MoneyValue" SkinID="Money" runat="server"
                                            CausesValidation="false" Width="100%" onchange="txtMoney_textChange()" NumberFormat-GroupSeparator=",">
                                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                            <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                        </tlk:RadNumericTextBox>
                                    </ItemTemplate>
                                </tlk:GridTemplateColumn>
                                <tlk:GridTemplateColumn HeaderText="<%$ Translate: Tiền tệ %>" SortExpression="MONEY_U_TEMP"
                                    UniqueName="MONEY_U_TEMP" ColumnGroupName="GeneralInformation">
                                    <HeaderStyle Width="80px" />
                                    <ItemTemplate>
                                        <tlk:RadComboBox runat="server" ID="RadMoneyU" AutoPostBack="true" CausesValidation="false"
                                            ChangeTextOnKeyBoardNavigation="false" AllowCustomText="False" Width="110%">
                                        </tlk:RadComboBox>
                                    </ItemTemplate>
                                </tlk:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </tlk:RadGrid>
                </td>
            </tr>
            <%--<tr>
                <td class="lb">
                    <%# Translate("Chi phí đào tạo")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtTrainingCost" MinValue="0" NumberFormat-GroupSeparator=".">
                        <ClientEvents OnValueChanged="OnClientValueChanged" />
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí phát sinh")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtInccurredCost" MinValue="0" NumberFormat-GroupSeparator=".">
                        <ClientEvents OnValueChanged="OnClientValueChanged" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chi phí khách sạn")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtTravleCost" MinValue="0" NumberFormat-GroupSeparator=".">
                        <ClientEvents OnValueChanged="OnClientValueChanged" />
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí khác")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtOtherCost" MinValue="0" NumberFormat-GroupSeparator=".">
                        <ClientEvents OnValueChanged="OnClientValueChanged" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>--%>
            <tr>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tổng chi phí (VNĐ)")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtTotal" SkinID="Money" ReadOnly="true"
                        NumberFormat-AllowRounding="False" NumberFormat-GroupSeparator=",">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                        <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            </tr> </tr> </tr> </tr>
            <td class="lb">
                <%# Translate("Chi phí 1 HV (VNĐ)")%>
            </td>
            <td>
                <tlk:RadNumericTextBox runat="server" ID="rntxtCostPerEmp" SkinID="Money" ReadOnly="true"
                    NumberFormat-GroupSeparator=",">
                    <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                    <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                </tlk:RadNumericTextBox>
            </td>
            </tr>
            <td class="lb" style="display: none">
                <%# Translate("Tổng chi phí (USD)")%>
            </td>
            <td style="display: none">
                <tlk:RadNumericTextBox ID="rntxtTotalUS" runat="server" SkinID="Money" ReadOnly="true"
                    NumberFormat-GroupSeparator=",">
                    <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                    <ClientEvents OnLoad="setDisplayValue" OnValueChanged="setDisplayValue" />
                </tlk:RadNumericTextBox>
            </td>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td class="lb" style="display: none">
                    <%# Translate("Chi phí 1 HV (USD)")%>
                </td>
                <td style="display: none">
                    <tlk:RadNumericTextBox ID="rntxtCostPerEmpUS" runat="server" SkinID="Money" ReadOnly="true"
                        NumberFormat-GroupSeparator=",">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                        <ClientEvents OnLoad="setDisplayValue" OnValueChanged="setDisplayValue" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Nhà cung cấp và đối tượng tham gia")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="display: none">
                    <%# Translate("Đơn vị chủ trì đào tạo")%>
                </td>
                <td style="display: none">
                    <tlk:RadComboBox ID="cboUnit" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <%-- <tr>
                <td class="lb">
                    <%# Translate("Đơn vị chủ trì đào tạo")%>
                </td>
                <td colspan="3">
                   <tlk:RadListBox ID="lstControlCenter" runat="server" CheckBoxes="true" Height="100px" OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging"
                        Width="100%" />
                </td>
            </tr>--%>
                <tr>
                    <td>
                    </td>
                    <td>
                        <%# Translate("Trung tâm đào tạo")%>
                    </td>
                    <td>
                    </td>
                    <%--<td>
                    <%# Translate("Đơn vị chủ trì đào tạo")%>
                </td>--%>
                    <td>
                        <%# Translate("Lý do đào tạo")%>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="2">
                        <%--  <tlk:RadListBox ID="lstCenter" runat="server" CheckBoxes="true" Height="100px" OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging"
                            Width="100%" />--%>
                        <tlk:RadComboBox rendermode="Lightweight" ID="cboCenter" AutoPostBack="true" CausesValidation="false"
                            runat="server" CheckBoxes="true" Filter="StartsWith" MarkFirstMatch="True" EnableCheckAllItemsCheckBox="true"
                            Width="100%" Label="">
                            <Items>
                            </Items>
                            <Localization CheckAllString="Chọn tât cả" AllItemsCheckedString="Tất cả Trung tâm"
                                ItemsCheckedString="Trung tâm đã chọn" />
                        </tlk:RadComboBox>
                    </td>
                    <td colspan="3">
                        <tlk:RadTextBox ID="txtTargetTrain" runat="server" Height="100px" SkinID="Textbox1023"
                            Width="100%">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Địa điểm tổ chức")%>
                    </td>
                    <td colspan="5">
                        <tlk:RadTextBox ID="txtVenue" runat="server" SkinID="Textbox1023" Width="100%">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <%# Translate("Đơn vị tham gia")%>
                    </td>
                    <td>
                    </td>
                    <td>
                        <%# Translate("Chức danh")%>
                    </td>
                    <td>
                    </td>
                    <td>
                        <%# Translate("Công việc liên quan")%>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="2">
                        <tlk:RadListBox ID="lstPartDepts" runat="server" Height="100px" Width="100%">
                        </tlk:RadListBox>
                    </td>
                    <td colspan="2">
                        <tlk:RadListBox ID="lstPositions" runat="server" Height="100px" OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging"
                            Width="100%">
                        </tlk:RadListBox>
                    </td>
                    <td>
                        <%-- <tlk:RadListBox ID="lstWork" runat="server" Height="100px" OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging"
                            Width="100%">
                        </tlk:RadListBox>--%>
                        <tlk:RadTextBox ID="txtWork_Relation" runat="server" Height="100px" Width="100%">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("File đính kèm")%>
                    </td>
                    <td>
                        <tlk:RadButton ID="btnUploadFile" runat="server" Text="<%$ Translate: Upload %>"
                            CausesValidation="false" Style="padding-right: 20px">
                        </tlk:RadButton>
                        <%--  <asp:Label id="lblFilename" runat="server" Text="">
                    </asp:Label>--%>
                    </td>
                    <td>
                        <asp:HyperLink ID="lblFilename" NavigateUrl="" Text="" Target="_new" runat="server" />
                    </td>
                    <td class="lb">
                        <%# Translate("Ghi chú")%>
                    </td>
                    <td colspan="3">
                        <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                        </tlk:RadTextBox>
                    </td>
                </tr>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="pgFindMultiEmp" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload2" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusCourse(oSrc, args) {
            var cbo = $find("<%# cboCourse.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusHinhThuc(oSrc, args) {
            var cbo = $find("<%# cboHinhThuc.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }


        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
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
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //            }
        }

        function OnClientValueChanged(sender, args) {
            //            var id = sender.get_id();
            //            var trainingCost = 0;
            //            var InccurredCost = 0;
            //            var TravelCost = 0;
            //            var OtherCost = 0;
            //            var TotalCost = 0;
            //            var EmpQuantity = 0;
            //            var cbo;

            //            cbo = $find('<%= rntxtStudents.ClientID %>');
            //            if (cbo.get_value()) {
            //                EmpQuantity = cbo.get_value();
            //            }
            //            switch (id) {
            //                case '<%= rntxtStudents.ClientID %>':
            //                    EmpQuantity = 0;
            //                    if (args.get_newValue()) {
            //                        EmpQuantity = args.get_newValue();
            //                    }
            //                    break;


            //                default:
            //                    break;
            //            }
            //            TotalCost = parseFloat(trainingCost) + parseFloat(InccurredCost) + parseFloat(TravelCost) + parseFloat(OtherCost);
            //            cbo = $find('<%= rntxtTotal.ClientID %>');
            //            cbo.set_value(TotalCost);
            //            cbo = $find('<%= rntxtCostPerEmp.ClientID %>');
            //            cbo.set_value(0);
            //            if (EmpQuantity != 0) {
            //                cbo.set_value(TotalCost / parseFloat(EmpQuantity));
            //            }
        }

        function setDisplayValue(sender, args) {
            sender.set_displayValue(sender.get_value());
        }


        function txtMoney_textChange() {
            var grid = $find("<%= rgISP.ClientID%>");
            var tonghv = 0;
            var tableView = grid.get_masterTableView();
            var items = tableView.get_dataItems();
            var iTotal = 0.00;
            var iTotalUSD = 0.00;
            for (var i = 0; i < items.length; i++) {
                var row = items[i];
                var itemValues = row.findElement("MONEY").value;
                var itemValues2 = row.findElement("RadMoneyU").value;
                if (parseFloat(itemValues)) {
                    if (itemValues2 == "USD") {
                        iTotalUSD += parseFloat(itemValues);
                    }
                    else {
                        iTotal += parseFloat(itemValues);
                    }
                }
            }

            var sohocvien = $find("<%= rntxtStudents.ClientID %>");
            if (sohocvien.get_value()) {
                tonghv = sohocvien.get_value();
            }

            //Find and set value to txtMoneyTotal
            var radTotalMomey = $find("<%= rntxtTotal.ClientID %>");
            var radTotalMomeyPer = $find("<%= rntxtCostPerEmp.ClientID %>");
            var radTotalMomeyUSD = $find("<%= rntxtTotalUS.ClientID %>");
            var radTotalMomeyPerUSD = $find("<%= rntxtCostPerEmpUS.ClientID %>");

            radTotalMomey.set_value(iTotal);
            radTotalMomeyUSD.set_value(iTotalUSD);

            if (tonghv != 0) {
                radTotalMomeyPer.set_value(iTotal / tonghv);
                radTotalMomeyPerUSD.set_value(iTotalUSD / tonghv);
            }

        };
    </script>
</tlk:RadCodeBlock>

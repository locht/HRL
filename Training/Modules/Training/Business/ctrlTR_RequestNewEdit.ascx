<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_RequestNewEdit.ascx.vb"
    Inherits="Training.ctrlTR_RequestNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidSenderID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="Y">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin chung")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đơn vị đề xuất")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindOrg" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Phòng ban %>" ToolTip="<%$ Translate: Bạn phải nhập Phòng ban %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Năm")%><%--<span>content</span> class="lbReq">*</span>--%>
                </td>

                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtYear" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="" MinValue="1900" MaxLength="2999" CausesValidation="false"
                        AutoPostBack="true" Width="80px">
                    </tlk:RadNumericTextBox>
                    <%--<div style="float: right">
                        <tlk:RadButton ID="cbIrregularly" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                            Text="<%$ Translate: Đột xuất%>" CausesValidation="false" AutoPostBack="true">
                        </tlk:RadButton>
                    </div>--%>
                    <%--<asp:RequiredFieldValidator ID="reqYear" ControlToValidate="rntxtYear" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Năm đào tạo %>" ToolTip="<%$ Translate: Bạn phải nhập Năm đào tạo %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Trạng thái")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboStatus">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqYear" ControlToValidate="cboStatus" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn trạng thái %>" ToolTip="<%$ Translate: Bạn phải chọn trạng thái %>">
                    </asp:RequiredFieldValidator>
                </td>

            </tr>
            <tr>
                <td></td>
                <td>
                    <tlk:RadButton ID="cbIrregularly" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                        Text="<%$ Translate: Đột xuất%>" CausesValidation="false" AutoPostBack="true">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <%# Translate("Nhóm chương trình")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cbGroupProgram" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Khóa đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Khóa đào tạo %>" ClientValidationFunction="cusPlan">
                    </asp:CustomValidator>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cbGroupProgram"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nhóm chương trình %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Nhóm chương trình %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Khóa đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboPlan" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusPlan" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Khóa đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Khóa đào tạo %>" ClientValidationFunction="cusPlan">
                    </asp:CustomValidator>
                </td>

            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhóm đối tượng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox rendermode="Lightweight" ID="cboObjGr" AutoPostBack="true" CausesValidation="false"
                        runat="server" CheckBoxes="true" Filter="StartsWith" MarkFirstMatch="True" EnableCheckAllItemsCheckBox="true"
                         Label="">
                        <Items>
                        </Items>
                        <Localization CheckAllString="Chọn tât cả" AllItemsCheckedString="Tất cả đối tượng"
                            ItemsCheckedString="Đối tượng đã chọn" />
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboObjGr"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nhóm đối tượng %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Nhóm đối tượng %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Hình thức đào tạo")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboTrainForm" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Tính chất nhu cầu")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboPropertiesNeed" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>

            </tr>
            <%--<tr>
                <td class="lb">
                    <%# Translate("Thời lượng")%><span class="lbReq">*</span>
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
            </tr>--%>
            <tr>
                <td class="lb"><%# Translate("Đánh giá khóa học: ")%></td>
                <td >
                    <tlk:RadButton ID="RadButton1" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                        Text="<%$ Translate: Khảo sát sau khóa học%>" CausesValidation="false" AutoPostBack="true">
                    </tlk:RadButton>
                </td>
                <td></td>
                <td >
                    <tlk:RadButton ID="RadButton2" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                        Text="<%$ Translate: Kiểm tra cuối khóa%>" CausesValidation="false" AutoPostBack="true">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <%# Translate("Thời gian đánh giá")%>
                </td>

                <td>
                    <tlk:RadNumericTextBox runat="server" ID="RadNumericTextBox1" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="" MinValue="0" MaxLength="100" CausesValidation="false"
                        AutoPostBack="true">
                    </tlk:RadNumericTextBox>
                    <%# Translate("(Tháng)")%>
                </td>
            </tr>
            
            <tr>
                <td class="lb" style="display: none">
                    <%# Translate("Lĩnh vực đào tạo")%>
                </td>
                <td style="display: none">
                    <tlk:RadTextBox runat="server" ID="txtTrainField" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày gửi yêu cầu")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdRequestDate">
                    </tlk:RadDatePicker>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdRequestDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày gửi yêu cầu %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Ngày gửi yêu cầu %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Thời gian dự kiến")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdExpectedDate">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdExpectedDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Thời gian dự kiến %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Thời gian dự kiến %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Thời gian bắt đầu")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdStartDate">
                    </tlk:RadDatePicker>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdStartDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Thời gian bắt đầu %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Thời gian bắt đầu %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
            </tr>




            <%--<tr>
                <td class="lb">
                    <%# Translate("Số lượng học viên dự kiến")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtStudents" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>--%>
            <tr>
                <td></td>                
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số lượng học viên dự kiến")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtStudents" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                
                <td rowspan="6" colspan="4">
                    <tlk:RadGrid ID="rgISP" runat="server" Height="205px" Width="550px" Style="overflow: auto;
                        margin-left: 20px">
                        <MasterTableView DataKeyNames="CODE" ClientDataKeyNames="CODE,NAME_VN,MONEY" AllowCustomPaging="false" AllowPaging="false">
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
                <td>
                </td>
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
            <tr>
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
            <%--k xoa khuc nay nhé--%>
            <tr>
                <td></td>                
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td></td>                
            </tr>
            <%--k xoa khuc nay nhé--%>

            <tr>
                <td class="lb">
                    <%# Translate("Trung tâm đào tạo")%>
                </td>
                <td>
                    <tlk:RadComboBox rendermode="Lightweight" ID="cboCenter" AutoPostBack="true" CausesValidation="false"
                        runat="server" CheckBoxes="true" Filter="StartsWith" MarkFirstMatch="True" EnableCheckAllItemsCheckBox="true"
                        Width="100%" Label="">
                        <Items>
                        </Items>
                        <Localization CheckAllString="Chọn tất cả" AllItemsCheckedString="Tất cả Trung tâm"
                            ItemsCheckedString="Trung tâm đã chọn" />
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Giảng viên")%>
                </td>
                <td>
                    <tlk:RadComboBox rendermode="Lightweight" ID="cboTeacher" AutoPostBack="true" CausesValidation="false"
                        runat="server" CheckBoxes="true" Filter="StartsWith" MarkFirstMatch="True" EnableCheckAllItemsCheckBox="true" Label="">
                        <Items>
                        </Items>
                        <Localization CheckAllString="Chọn tât cả" AllItemsCheckedString="Tất cả giảng viên"
                            ItemsCheckedString="Giảng viên đã chọn" />
                    </tlk:RadComboBox>
                </td>
                <td>
                    <%# Translate("Ngôn ngữ giảng dạy")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboLanguage">
                    </tlk:RadComboBox>
                </td>
            </tr>

            <tr>
                <td class="lb" style="display: none">
                    <%# Translate("Chi phí dự kiến")%>
                </td>
                <td style="display: none">
                    <tlk:RadNumericTextBox runat="server" ID="rntxtExpectedCost" MinValue="0" NumberFormat-GroupSeparator=",">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="display: none">
                    <%# Translate("Đơn vị tiền tệ")%>
                </td>
                <td style="display: none">
                    <tlk:RadComboBox runat="server" ID="cboCurrency">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <%--<tr>
                <td class="lb">
                    <%# Translate("Nội dung đào tạo")%><span class="lbReq">*</span>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtContent" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtContent"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Nội dung đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Nội dung đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>--%>
            <tr>
                <td class="lb">
                    <%# Translate("Nội dung đào tạo")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtContent" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Kỳ vọng đào tạo")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtKivong" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lý do đào tạo")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtTargetTrain" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Địa điểm tổ chức")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtVenue" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Người gửi")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtSender" ReadOnly="true" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindSender" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtSender"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Người gửi %>" ToolTip="<%$ Translate: Bạn phải chọn Người gửi %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Email người gửi")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtSenderMail" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Điện thoại người gửi")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtSenderMobile" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>

                <td class="lb">
                    <%# Translate("File đính kèm")%>
                </td>
                <td colspan="3">
                    <tlk:RadButton ID="btnUploadFile" runat="server" Text="<%$ Translate: Upload %>"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:Label ID="lblFilename" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtRemark" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin học viên")%>
                    <div style="float: right;">
                        <%# Translate("Số lượng học viên thực tế / theo kế hoạch : ")%>
                        <b>
                            <asp:Label runat="server" ID="lblNumOfRealTrainee" Text="0"></asp:Label>
                            /
                            <asp:Label runat="server" ID="lblNumOfPlanTrainee" Text="0"></asp:Label></b>
                    </div>
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
                <td colspan="4">
                    <tlk:RadButton ID="btnAdd" runat="server" Text="<%$ Translate: Thêm học viên %>"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnRemove" runat="server" Text="<%$ Translate: Xóa học viên %>"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnExport" runat="server" Text="<%$ Translate: Xuất file mẫu %>"
                        CausesValidation="false" OnClientClicking="btnExportClicking">
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnImport" runat="server" Text="<%$ Translate: Nhập file mẫu %>"
                        CausesValidation="false">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
        <div style="margin-left: 10px; height: 200px">
            <tlk:RadGrid ID="rgData" runat="server" Height="200px" Width="99%">
                <MasterTableView DataKeyNames="EMPLOYEE_ID" ClientDataKeyNames="EMPLOYEE_ID">
                    <Columns>
                        <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                        </tlk:GridClientSelectColumn>
                        <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                            SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                            SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" />
                        <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                            SortExpression="BIRTH_DATE" UniqueName="BIRTH_DATE" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </tlk:GridDateTimeColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                            SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="COM_NAME" SortExpression="COM_NAME"
                            UniqueName="COM_NAME" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                            SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Công việc liên quan %>" DataField="WORK_INVOLVE_NAME"
                            SortExpression="WORK_INVOLVE_NAME" UniqueName="WORK_INVOLVE_NAME" />
                    </Columns>
                </MasterTableView>
            </tlk:RadGrid>
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlUpload ID="ctrlUpload2" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function cusPlan(oSrc, args) {
            var cbo = $find("<%# cboPlan.ClientID %>");
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

        function btnExportClicking(sender, args) {
            enableAjax = false;
        }
        function clientButtonClicking(sender, args) {
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //                window.open('/Default.aspx?mid=Training&fid=ctrlTR_Request&group=Business', "_self");
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

            radTotalMomey.set_value(iTotal);
            radTotalMomeyUSD.set_value(iTotalUSD);

            if (tonghv != 0) {
                radTotalMomeyPer.set_value(iTotal / tonghv);
                radTotalMomeyPerUSD.set_value(iTotalUSD / tonghv);
            }

        }

    </script>
</tlk:RadCodeBlock>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_SafeLaborMngNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_SafeLaborMngNewEdit" %>
<tlk:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
<asp:HiddenField ID="hidIDEmp" runat="server" />
<asp:HiddenField ID="hidOrg" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Scrolling="None" Height="35px">
        <tlk:RadToolBar ID="tbarMenu" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Height="130px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary"
            ValidationGroup="" />
        <table class="table-form" style="margin-left: 100px">
            <tr>
                <td class="lb">
                    <asp:Label ID="lbCode" runat="server" Text="Mã vụ tai nạn"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCode">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rqCode" ControlToValidate="txtCode"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mã vụ tai nạn %>" ToolTip="<%$ Translate: Bạn phải nhập mã vụ tai nạn  %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbName" runat="server" Text="Tên vụ tai nạn"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtName">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtCode"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên vụ tai nạn %>" ToolTip="<%$ Translate: Bạn phải nhập Tên vụ tai nạn  %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbOrgName" runat="server" Text="Phòng ban"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" Width="130px" SkinID="Readonly" ReadOnly="true" />
                    <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                    <asp:RequiredFieldValidator ID="rqOrgName" ControlToValidate="txtOrgName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn Phòng ban %>" ToolTip="<%$ Translate: Bạn phải chọn Phòng ban %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbDateOccurAccident" runat="server" Text="Ngày xảy ra tai nạn"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDateOccurAccident">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="rqDateOccurAccident" ControlToValidate="rdDateOccurAccident"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày xảy ra tai nạn %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Ngày xảy ra tai nạn %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbHourOccur" runat="server" Text="<%$ Translate: Thời gian xảy ra tai nạn %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rtHourOccur">
                    </tlk:RadTimePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lb" runat="server" Text="<%$ Translate: Loại tai nạn %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboTypeAccident">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="rqTypeAccident" ControlToValidate="cboTypeAccident"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Loại tai nạn %>" ToolTip="<%$ Translate: Bạn phải chọn Loại tai nạn %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbPlaceAccident" runat="server" Text="<%$ Translate: Nơi xảy ra tai nạn %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtPlaceAccident">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbReasonAccident" runat="server" Text="<%$ Translate: Nguyên nhân tai nạn %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboReasonAccident">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="rqReasonAccident" ControlToValidate="cboReasonAccident"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nguyên nhân tai nạn %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Nguyên nhân tai nạn %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbReasonDetail" runat="server" Text="<%$ Translate: Nguyên nhân chi tiết tai nạn %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtReasonDetail">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbCost" runat="server" Text="<%$ Translate: Chi phí tai nạn %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnCost" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbRemark" runat="server" Text="<%$ Translate: Ghi chú %>"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtRemark">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgEmployee" AllowPaging="true" AllowMultiRowEdit="true" runat="server"
            PageSize="50" Height="100%">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView EditMode="InPlace" AllowPaging="true" AllowCustomPaging="true" DataKeyNames="EMPLOYEE_ID,EMPLOYEE_CODE,EMPLOYEE_NAME,ORG_NAME,TITLE_NAME,NUMBER_DATE,LEVEL_INJURED,LEVEL_DECLINE,MONEY_MEDICAL,COST_SALARY,MONEY_INDEMNIFY,COMPANY_PAY,DATE_INS_PAY,MONEY_INS_PAY,MONEY_DIFFERENCE,REMARK"
                ClientDataKeyNames="EMPLOYEE_ID,EMPLOYEE_CODE,EMPLOYEE_NAME,ORG_NAME,TITLE_NAME,NUMBER_DATE,LEVEL_INJURED,LEVEL_DECLINE,MONEY_MEDICAL,COST_SALARY,MONEY_INDEMNIFY,COMPANY_PAY,DATE_INS_PAY,MONEY_INS_PAY,MONEY_DIFFERENCE,REMARK"
                CommandItemDisplay="Top">
                <CommandItemStyle Height="25px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="150px" ID="btnEmployee" runat="server" Text="Chọn tất cả nhân viên"
                                CausesValidation="false" CommandName="FindEmployee" TabIndex="3">
                            </tlk:RadButton>
                        </div>
                        <div style="float: right">
                            <tlk:RadButton Width="100px" ID="btnDeleteEmp" runat="server" Text="Xóa" CausesValidation="false"
                                CommandName="DeleteEmployee" TabIndex="3">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="EMPLOYEE_ID" DataField="EMPLOYEE_ID" ReadOnly="true"
                        UniqueName="EMPLOYEE_ID" SortExpression="EMPLOYEE_ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="MSNV" DataField="EMPLOYEE_CODE" ReadOnly="true"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME"
                        ReadOnly="true" SortExpression="EMPLOYEE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        ReadOnly="true" SortExpression="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                        ReadOnly="true" SortExpression="TITLE_NAME" />
                    <tlk:GridTemplateColumn HeaderText="Số ngày nghỉ do tại nạn" DataField="NUMBER_DATE"
                        UniqueName="NUMBER_DATE">
                        <EditItemTemplate>
                            <tlk:RadNumericTextBox SkinID="Money" DataFormatString="{0:N2}" Width="90px" runat="server"
                                ID="r1DayNumber">
                            </tlk:RadNumericTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="Mức độ thương tật" DataField="LEVEL_INJURED"
                        UniqueName="LEVEL_INJURED">
                        <EditItemTemplate>
                            <tlk:RadTextBox runat="server" ID="r2LevelInjured" Width="70px">
                            </tlk:RadTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="Mức độ suy giảm LĐ" DataField="LEVEL_DECLINE"
                        UniqueName="LEVEL_DECLINE">
                        <EditItemTemplate>
                            <tlk:RadNumericTextBox SkinID="Money" DataFormatString="{0:N2}" runat="server" ID="r3LevelDecline">
                            </tlk:RadNumericTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="Tiền tạm ứng y tế" DataField="MONEY_MEDICAL"
                        UniqueName="MONEY_MEDICAL">
                        <EditItemTemplate>
                            <tlk:RadNumericTextBox SkinID="Money" DataFormatString="{0:N2}" Width="70px" runat="server"
                                ID="r4MoneyMedical">
                            </tlk:RadNumericTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="Chi phí lương" DataField="COST_SALARY" UniqueName="COST_SALARY">
                        <EditItemTemplate>
                            <tlk:RadNumericTextBox SkinID="Money" DataFormatString="{0:N2}" Width="70px" runat="server"
                                ID="r5CostSalary">
                            </tlk:RadNumericTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="Số tiền bồi thường" DataField="MONEY_INDEMNIFY"
                        UniqueName="MONEY_INDEMNIFY">
                        <EditItemTemplate>
                            <tlk:RadNumericTextBox SkinID="Money" DataFormatString="{0:N2}" Width="70px" runat="server"
                                ID="r6MoneyIndemnify">
                            </tlk:RadNumericTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="Số tiền công ty trả" DataField="COMPANY_PAY"
                        UniqueName="COMPANY_PAY">
                        <EditItemTemplate>
                            <tlk:RadNumericTextBox SkinID="Money" DataFormatString="{0:N2}" Width="70px" runat="server"
                                ID="r7CompanyPay" ReadOnly="true">
                            </tlk:RadNumericTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="Ngày bảo hiểm thanh toán" DataField="DATE_INS_PAY"
                        UniqueName="DATE_INS_PAY" HeaderStyle-Width="170px">
                        <EditItemTemplate>
                            <tlk:RadDatePicker runat="server" ID="r8DateInsPay">
                            </tlk:RadDatePicker>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="Tiền bảo hiểm thanh toán" DataField="MONEY_INS_PAY"
                        UniqueName="MONEY_INS_PAY">
                        <EditItemTemplate>
                            <tlk:RadNumericTextBox SkinID="Money" DataFormatString="{0:N2}" Width="70px" runat="server"
                                ID="r9MoneyInsPay">
                            </tlk:RadNumericTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="Tiền chênh lệch" HeaderStyle-Width="100px" DataField="MONEY_DIFFERENCE"
                        UniqueName="MONEY_DIFFERENCE">
                        <EditItemTemplate>
                            <tlk:RadNumericTextBox SkinID="Money" DataFormatString="{0:N2}" Width="70px" runat="server"
                                ID="rnMoneyDifference" ReadOnly="true">
                            </tlk:RadNumericTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="Ghi chú" DataField="REMARK" UniqueName="REMARK">
                        <EditItemTemplate>
                            <tlk:RadTextBox runat="server" ID="rtRemark">
                            </tlk:RadTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            registerOnfocusOut('RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_WelfareMngNewEdit_LeftPane');
        });



        var enableAjax = true;
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

        function clientButtonClicking(sender, args) {
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //            }
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'Export') {
                enableAjax = false;
            }
        }
        function btnExportClicking(sender, args) {
            enableAjax = false;
        }
        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                default:
                    break;
            }
        }

        function clearSelectRadnumeric(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

        function clearSelectRadtextbox(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

    </script>
</tlk:RadCodeBlock>

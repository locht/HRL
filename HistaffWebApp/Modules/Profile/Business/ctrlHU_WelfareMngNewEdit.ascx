<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_WelfareMngNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_WelfareMngNewEdit" %>
<tlk:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
<asp:HiddenField ID="hidIDEmp" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Scrolling="None" Height="35px">
        <tlk:RadToolBar ID="tbarMenu" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Height="100px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary"
            ValidationGroup="" />
        <table class="table-form">
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("Tên loại phúc lợi")%><span class="lbReq">*</span></b>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboWELFARE_ID" runat="server" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusWELFARE_ID" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn tên loại phúc lợi %>"
                        ToolTip="<%$ Translate: Bạn phải chọn tên loại phúc lợi  %>" ClientValidationFunction="cusWELFARE_ID">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cusValWELFARE_ID" ControlToValidate="cboWELFARE_ID" runat="server"
                        ErrorMessage="<%$ Translate: Loại phúc lợi không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Loại phúc lợi không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày thanh toán")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="dpEFFECT_DATE" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi Chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtSDESC" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgEmployee" AllowPaging="true" AllowMultiRowEdit="true" runat="server"
            PageSize="50" Height="100%">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView EditMode="InPlace" AllowPaging="true" AllowCustomPaging="true" DataKeyNames="ID,EMPLOYEE_CODE,ORG_ID,TITLE_ID"
                ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_NAME,TITLE_ID,ORG_ID,TITLE_NAME,ORG_NAME,EMPLOYEE_CODE,GENDER_NAME,CONTRACT_NAME,SENIORITY,TOTAL_CHILD,MONEY_PL,MONEY_TOTAL"
                CommandItemDisplay="Top">
                <CommandItemStyle Height="25px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="150px" ID="btnEmployee" runat="server" Text="Chọn tất cả nhân viên"
                                CausesValidation="false" CommandName="FindEmployee" TabIndex="3">
                            </tlk:RadButton>
                            <%--<tlk:RadButton Width="150px" ID="RadButton1" runat="server" Text="Chọn nhân viên từ import"
                                CausesValidation="false" CommandName="FindEmployeeImport" TabIndex="3">
                            </tlk:RadButton>--%>
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
                    <tlk:GridBoundColumn HeaderText="MSNV" DataField="EMPLOYEE_CODE" ReadOnly="true"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME"
                        ReadOnly="true" SortExpression="EMPLOYEE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        ReadOnly="true" SortExpression="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                        ReadOnly="true" SortExpression="TITLE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Giới tính" DataField="GENDER_NAME" UniqueName="GENDER_NAME" ReadOnly="true"
                        SortExpression="GENDER_NAME" />
                    <tlk:GridBoundColumn HeaderText="Loại hợp đồng gần nhất" DataField="CONTRACT_NAME" UniqueName="CONTRACT_NAME"
                        ReadOnly="true" SortExpression="CONTRACT_NAME" />
                    <tlk:GridBoundColumn HeaderText="Thâm niên" DataField="SENIORITY" UniqueName="SENIORITY" ReadOnly="true"
                        SortExpression="SENIORITY" />
                    <tlk:GridBoundColumn HeaderText="Tổng số con" DataField="TOTAL_CHILD" UniqueName="TOTAL_CHILD" ReadOnly="true"
                        SortExpression="TOTAL_CHILD" />
                    <tlk:GridBoundColumn HeaderText="Số tiền phúc lợi" DataField="MONEY_PL" UniqueName="MONEY_PL" ReadOnly="true"
                        SortExpression="MONEY_PL" />
                    <tlk:GridBoundColumn HeaderText="Tổng số tiền" DataField="MONEY_TOTAL" UniqueName="MONEY_TOTAL" ReadOnly="true"
                        SortExpression="MONEY_TOTAL" />
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
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            registerOnfocusOut('RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_WelfareMngNewEdit_LeftPane');
        });

        function cusWELFARE_ID(oSrc, args) {
            var cbo = $find("<%# cboWELFARE_ID.ClientID%>");
            args.IsValid = (cbo.get_value().length != 0);
        }

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
        }

        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                case '<%= cboWELFARE_ID.ClientID %>':


                    break;
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

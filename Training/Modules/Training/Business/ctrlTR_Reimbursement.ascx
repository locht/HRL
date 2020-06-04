<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_Reimbursement.ascx.vb"
    Inherits="Training.ctrlTR_Reimbursement" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="270px" Scrolling="None">
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Năm")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtSeachYear" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="" MinValue="1900" MaxLength="2999" AutoPostBack="true"
                        CausesValidation="false">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Khóa đào tạo")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboSearchCourse" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã nhân viên")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtSeachEmployee">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                </td>
                <td>
                    <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                        Text="<%$ Translate: Tìm kiếm %>">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
        <hr />
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:HiddenField runat="server" ID="hidEmployeeID" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Năm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtYear" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="" MinValue="1900" MaxLength="2999" AutoPostBack="true"
                        CausesValidation="false">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqYear" ControlToValidate="rntxtYear" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Năm đào tạo %>" ToolTip="<%$ Translate: Bạn phải nhập Năm đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Khóa đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCourse" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusCourse" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn khóa đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn khóa đào tạo %>" ClientValidationFunction="cusCourse">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí đào tạo 1 học viên")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostOfStudent" runat="server" NumberFormat-DecimalDigits="2"
                        NumberFormat-GroupSeparator="," MinValue="0" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>
                <td>
                    <tlk:RadButton ID="cbReserves" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                        Text="<%$ Translate: Trừ lương %>" CausesValidation="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày bắt đầu bồi hoàn")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdStartDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdStartDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày bắt đầu bồi hoàn %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Ngày bắt đầu bồi hoàn %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Mã nhân viên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" MaxLength="255" runat="server" Width="130px" ReadOnly="true">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindReimbursement" runat="server"
                        SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên %>" ToolTip="<%$ Translate: Bạn phải chọn nhân viên %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã nhân viên đã tồn tại %>"
                        ToolTip="<%$ Translate: Mã nhân viên đã tồn tại %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Họ tên nhân viên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
        <tlk:RadGrid ID="rgMain" runat="server" AllowPaging="True" Height="80%" AllowSorting="True"
            AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="5" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,EMPLOYEE_NAME,TITLE,ORG_NAME,YEAR,TR_PROGRAM_ID,TR_PROGRAM_NAME,FROM_DATE,TO_DATE,COST_OF_STUDENT,COMMIT_WORK,WORK_AFTER,COST_REIMBURSE,START_DATE,IS_RESERVES"
                ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,EMPLOYEE_NAME,TITLE,ORG_NAME,YEAR,TR_PROGRAM_ID,TR_PROGRAM_NAME,FROM_DATE,TO_DATE,COST_OF_STUDENT,COMMIT_WORK,WORK_AFTER,COST_REIMBURSE,START_DATE,IS_RESERVES">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                        UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE" UniqueName="TITLE"
                        SortExpression="TITLE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                        UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR" UniqueName="YEAR"
                        SortExpression="YEAR" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Khóa đào tạo %>" DataField="TR_PROGRAM_NAME"
                        UniqueName="TR_PROGRAM_NAME" SortExpression="TR_PROGRAM_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="FROM_DATE"
                        UniqueName="FROM_DATE" SortExpression="FROM_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="TO_DATE"
                        UniqueName="TO_DATE" SortExpression="TO_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Học phí đào tạo %>" DataField="COST_OF_STUDENT"
                        UniqueName="COST_OF_STUDENT" SortExpression="COST_OF_STUDENT" DataFormatString="{0:N0}" />
                    <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian cam kết sau đào tạo %>"
                        DataField="COMMIT_WORK" UniqueName="COMMIT_WORK" SortExpression="COMMIT_WORK" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian đã làm việc sau đào tạo %>"
                        DataField="WORK_AFTER" UniqueName="WORK_AFTER" SortExpression="WORK_AFTER" />--%>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng tiền bồi hoàn %>" DataField="COST_REIMBURSE"
                        UniqueName="COST_REIMBURSE" SortExpression="COST_REIMBURSE" DataFormatString="{0:N0}" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày bắt đầu bồi hoàn %>" DataField="START_DATE"
                        UniqueName="START_DATE" SortExpression="START_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                    <tlk:GridBoundColumn DataField="IS_RESERVES" Visible="false" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindReimbursement" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusCourse(oSrc, args) {
            var cbo = $find("<%# cboCourse.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        var enableAjax = true;
        var oldSize = 0;

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                //ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                //ResizeSplitterDefault();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        //        function ResizeSplitter() {
        //            setTimeout(function () {
        //                var splitter = $find("<%= RadSplitter3.ClientID%>");
        //                var pane = splitter.getPaneById('<%= LeftPane.ClientID %>');
        //                var height = pane.getContentElement().scrollHeight;
        //                splitter.set_height(splitter.get_height() + pane.get_height() - height);
        //                pane.set_height(height);
        //            }, 200);
        //        }

        //        // Hàm khôi phục lại Size ban đầu cho Splitter
        //        function ResizeSplitterDefault() {
        //            var splitter = $find("<%= RadSplitter3.ClientID%>");
        //            var pane = splitter.getPaneById('<%= LeftPane.ClientID %>');
        //            if (oldSize == 0) {
        //                oldSize = pane.getContentElement().scrollHeight;
        //            } else {
        //                var pane2 = splitter.getPaneById('<%= MainPane.ClientID %>');
        //                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
        //                pane.set_height(oldSize);
        //                pane2.set_height(splitter.get_height() - oldSize - 1);
        //            }
        //        }
    </script>
</tlk:RadCodeBlock>

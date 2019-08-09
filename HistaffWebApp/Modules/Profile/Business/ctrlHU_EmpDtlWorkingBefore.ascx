<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlWorkingBefore.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlWorkingBefore" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="hidEmployeeID" runat="server" />
<asp:HiddenField ID="hidWorkingID" runat="server" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="none" SkinID="Demo">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="195px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb" style="width: 130px">
                    <%--  <%# Translate("Tên công ty") %><span class="lbReq">*</span>--%>
                    <asp:Label runat="server" ID="lbCompanyName" Text="Tên công ty"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCompanyName">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCompanyName" ControlToValidate="txtCompanyName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên công ty %>" ToolTip="<%$ Translate: Bạn phải nhập tên công ty  %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 150px">
                    <%-- <%# ID="txt" Translate("Số điện thoại")%>--%>
                    <asp:Label runat="server" ID="lbsdt" Text="Số điện thoại"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTelephone">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%--    <%# Translate("Địa chỉ công ty") %>--%>
                    <asp:Label runat="server" ID="lbCompanyAddress" Text="Địa chỉ công ty"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtCompanyAddress" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%-- <%# Translate("Từ tháng/năm")%>--%>
                    <asp:Label runat="server" ID="lbJoinDate" Text="Từ tháng/năm"></asp:Label>
                </td>
                <td>
                    <tlk:RadMonthYearPicker ID="rdJoinDate" runat="server" Culture="en-US">
                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="MM/yyyy">
                        </DateInput>
                    </tlk:RadMonthYearPicker>
                </td>
                <td class="lb">
                    <%--  <%# Translate("Đến tháng/năm")%>--%>
                    <asp:Label runat="server" ID="lbEndDate" Text="Đến tháng/năm"></asp:Label>
                </td>
                <td>
                    <tlk:RadMonthYearPicker ID="rdEndDate" runat="server" Culture="en-US">
                        <DateInput ID="DateInput2" runat="server" DisplayDateFormat="MM/yyyy">
                        </DateInput>
                    </tlk:RadMonthYearPicker>
                    <%--<asp:CompareValidator ID="compare_JoinDate_EndDate" runat="server" ErrorMessage="<%$ Translate: Ngày nghỉ phải sau ngày vào %>"
                        ControlToCompare="rdJoinDate" ControlToValidate="rdEndDate" ToolTip="<%$ Translate: Ngày nghỉ phải sau ngày vào %>"
                        Type="Date" Operator="GreaterThan"></asp:CompareValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%--<%# Translate("Mức lương")%>--%>
                    <asp:Label runat="server" ID="lbSalary" Text="Mức lương"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="txtSalary" MinValue="0" MaxLength="9" NumberFormat-DecimalDigits="0">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%--   <%# Translate("Chức danh")%>--%>
                    <asp:Label runat="server" ID="lbTitleName" Text="Chức danh"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTitleName">
                    </tlk:RadTextBox>
                </td>
                <td style="display: none" class="lb">
                    <%-- <%# Translate("Cấp bậc")%>--%>
                    <asp:Label runat="server" ID="lbLevelName" Text="Cấp bậc"></asp:Label>
                </td>
                <td style="display: none">
                    <tlk:RadTextBox runat="server" ID="txtLevelName">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%-- <%# Translate("Lý do nghỉ việc") %>--%>
                    <asp:Label runat="server" ID="lbTerReason" Text="Lý do nghỉ việc"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtTerReason" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgGrid" runat="server" AllowMultiRowSelection="true"
            Height="100%">
            <MasterTableView DataKeyNames="ID,COMPANY_NAME,JOIN_DATE,END_DATE,COMPANY_ADDRESS,TELEPHONE,SALARY,TITLE_NAME,LEVEL_NAME,TER_REASON"
                ClientDataKeyNames="ID,COMPANY_NAME,JOIN_DATE,END_DATE,COMPANY_ADDRESS,TELEPHONE,SALARY,TITLE_NAME,LEVEL_NAME,TER_REASON">
                <NoRecordsTemplate>
                    Không có bản ghi nào
                </NoRecordsTemplate>
                <Columns>
                    <%--<tlk:GridClientSelectColumn>
                        <HeaderStyle Width="40px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên công ty %>" DataField="COMPANY_NAME"
                        UniqueName="COMPANY_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số điện thoại %>" DataField="TELEPHONE"
                        UniqueName="TELEPHONE">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ tháng/năm %>" DataField="JOIN_DATE"
                        UniqueName="JOIN_DATE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến tháng/năm %>" DataField="END_DATE"
                        UniqueName="END_DATE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridNumericColumn DataField="SALARY" HeaderText="<%$ Translate: Mức lương %>"
                        UniqueName="SALARY" DataFormatString="{0:n0}">
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp bậc %>" DataField="LEVEL_NAME"
                        UniqueName="LEVEL_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do nghỉ %>" DataField="TER_REASON"
                        UniqueName="TER_REASON">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Visible="False">
                    </tlk:GridBoundColumn>--%>
                </Columns>
                <HeaderStyle Width="120px" />
            </MasterTableView>
            <ClientSettings EnablePostBackOnRowClick="True">
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlWorkingBefore_RadSplitter2';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlWorkingBefore_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlWorkingBefore_RadPane4';
        var pane3ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlWorkingBefore_RadPane2';
        var pane4ID = 'ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlWorkingBefore_tbarMainToolBar';
        var validateID = 'MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlWorkingBefore_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;

        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgGrid', pane3ID, pane4ID);
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function clRadDatePicker() {
            $('#ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlWorkingBefore_rdJoinDate_dateInput').val('');
            $('#ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlWorkingBefore_rdEndDate_dateInput').val('');
        }
    </script>
</tlk:RadScriptBlock>

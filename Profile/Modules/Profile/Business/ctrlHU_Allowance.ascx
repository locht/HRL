<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Allowance.ascx.vb"
    Inherits="Profile.ctrlHU_Allowance" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    #ctl00_MainContent_ctrlHU_Allowance_ctrlOrg_trvOrgPostback {
        height: 58% !important;
    }
</style>
<asp:HiddenField runat="server" ID="hidEmployeeID" />
<asp:HiddenField runat="server" ID="hidEmployee_SID" />
<asp:PlaceHolder ID="phEMPR" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phEMPRS" runat="server"></asp:PlaceHolder>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="270px" Scrolling="None">
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb">
                    <%# Translate("Nhân viên")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtSeachEmployee">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFrom" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdTo" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("loại phụ cấp")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboLPC" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox ID="chkNewEmp" runat="server" Text="<%$ Translate: Tìm mới nhất %>" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Liệt kê cả nhân viên nghỉ việc %>" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                        Text="<%$ Translate: Tìm %>">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
        <hr />
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Height="200px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Tên nhân viên")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtEmployeeName" ReadOnly="true" />
                            <tlk:RadButton runat="server" ID="btnFindR" SkinID="ButtonView" CausesValidation="false">
                            </tlk:RadButton>
                            <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeName"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Phụ cấp")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboALLOWANCE_LIST" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboALLOWANCE_LIST"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn phụ cấp. %>" ToolTip="<%$ Translate: Bạn phải chọn phụ cấp. %>"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Số tiền")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtAmount" runat="server" SkinID="Money">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Hệ số")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtFactor" runat="server" SkinID="Money">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdEFFECT_DATE" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdEFFECT_DATE"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày hiệu lực %>" ToolTip="<%$ Translate: Bạn phải nhập Ngày hiệu lực %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày hết hiệu lực")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdEXPIRE_DATE" runat="server">
                            </tlk:RadDatePicker>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Ngày hiệu lực phải nhỏ hơn ngày hết hiệu lực %>"
                                ErrorMessage="<%$ Translate: Ngày hiệu lực phải nhỏ hơn ngày hết hiệu lực %>"
                                Type="Date" Operator="GreaterThan" ControlToCompare="rdEFFECT_DATE" ControlToValidate="rdEXPIRE_DATE"></asp:CompareValidator>
                        </td>
                    </tr>
                    
                    
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox runat="server" ID="txtRemark" Width="100%" SkinID="Textbox1023">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="50"
                    Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID" ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,
                    EMPLOYEE_NAME,ALLOWANCE_LIST_NAME,ALLOWANCE_LIST_ID,AMOUNT,
                    FACTOR,EXPIRE_DATE,EFFECT_DATE,EMPLOYEE_SIGNER_NAME,
                    EMPLOYEE_SIGNER_SR_NAME,REMARK,EMPLOYEE_SIGNER_TITLE_NAME,DATE_SIGNER,EMPLOYEE_SIGNER_ID,ORG_DESC,ORG_NAME1,ORG_NAME2,ORG_NAME3,PAYROLL,STAFF_RANK_NAME,TITLE_NAME_VN,TITLE_JOB_NAME,LEARNING_LEVEL_NAME">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="EMPLOYEE_SIGNER_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="120px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" HeaderStyle-Width="200px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phụ cấp %>" DataField="ALLOWANCE_LIST_NAME"
                                UniqueName="ALLOWANCE_LIST_NAME" SortExpression="ALLOWANCE_LIST_NAME" HeaderStyle-Width="200px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số tiền %>" DataField="AMOUNT" UniqueName="AMOUNT"
                                SortExpression="AMOUNT" DataFormatString="{0:n0}" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số %>" DataField="FACTOR" UniqueName="FACTOR"
                                SortExpression="FACTOR" HeaderStyle-Width="100px" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                                SortExpression="REMARK" HeaderStyle-Width="200px" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindReimbursement" runat="server"></asp:PlaceHolder>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlHU_Allowance_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_Allowance_RadPane2';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_Allowance_RadPane1';
        var validateID = 'MainContent_ctrlHU_Allowance_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }


        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgMain.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgMain');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else if (args.get_item().get_commandName() == "EDIT") {
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

    </script>
</tlk:RadCodeBlock>

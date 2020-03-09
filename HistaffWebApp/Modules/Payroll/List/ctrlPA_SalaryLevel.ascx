<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SalaryLevel.ascx.vb"
    Inherits="Payroll.ctrlPA_SalaryLevel" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="170px" Scrolling="None">
        <tlk:RadToolBar ID="tbarSalaryLevels" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">  
            <tr>
                <td class="lb">
                    <%# Translate("Nhóm ngạch bậc")%><span class="lbReq">*</span>
                </td>
                 <td>
                    <tlk:RadComboBox ID="cboGradeGroup" runat="server" >
                    </tlk:RadComboBox>                  
                      <asp:RequiredFieldValidator ID="reqGradeGroup" ControlToValidate="cboGradeGroup"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhóm ngạch bậc %>"
                        ToolTip="<%$ Translate: Bạn phải nhập nhóm ngạch bậc %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Mã ngạch lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server" SkinID="Textbox50">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mã ngạch lương %>" ToolTip="<%$ Translate: Bạn phải nhập Mã ngạch lương %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã ngạch lương đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã ngạch lương đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên ngạch lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên nhóm lương %>" ToolTip="<%$ Translate: Bạn phải nhập Tên nhóm lương %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>  
            <tr>
               <td class="lb" id="tdSLlbSalaryFr">
                    <label id="lbSalaryFr">
                        <%# Translate("Mức lương từ")%></label>
                        <span class="lbReq">*</span>
                </td>              
                  <td >
                    <tlk:RadNumericTextBox runat="server" ID="rntxtSalaryFr" MinValue="1" Width="60px"
                        ShowSpinButtons="True" ReadOnly="true" CausesValidation="false">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rntxtSalaryFr"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập mức lương từ. %>" ToolTip="<%$ Translate: Bạn phải nhập mức từ. %>"></asp:RequiredFieldValidator>
                </td>
                 <td class="lb" id="tdSLlbSalaryTo">
                    <label id="lbSalaryTo">
                        <%# Translate("Mức lương đến")%></label>
                        <span class="lbReq">*</span>
                </td>                
                 <td >
                    <tlk:RadNumericTextBox runat="server" ID="rntxtSalaryTo" MinValue="1" Width="60px"
                         ShowSpinButtons="True" ReadOnly="true" CausesValidation="false">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rntxtOrders"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập mức lương đến. %>" ToolTip="<%$ Translate: Bạn phải nhập mức đến. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb" id="tdSLlbOrders">
                    <label id="lbOrders">
                        <%# Translate("Thứ tự")%></label>
                        <span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadNumericTextBox runat="server" ID="rntxtOrders" MinValue="1" Width="60px"
                        Value="1" ShowSpinButtons="True" ReadOnly="true" CausesValidation="false">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rntxtOrders"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số thứ tự. %>" ToolTip="<%$ Translate: Bạn phải nhập số thứ tự. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Thang lương")%><span class="lbReq">*</span>
                </td>
                <td >
                    <tlk:RadComboBox ID="cboSalaryGroup" runat="server" >
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalSalaryGroup" ControlToValidate="cboSalaryGroup" runat="server" ErrorMessage="<%$ Translate: Thang lương không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Thang lương không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>            
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%">
            <ClientSettings EnableRowHoverStyle="true" >
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="GRADE_GROUP,SAL_FR,SAL_TO,SAL_GROUP_ID, CODE,NAME,REMARK,ORDERS">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm ngạch bậc %>" DataField="GRADE_GROUP_NAME"
                        SortExpression="GRADE_GROUP_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        AndCurrentFilterFunction="Contains" UniqueName="GRADE_GROUP_NAME" />
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã %>" DataField="CODE" SortExpression="CODE"
                        AutoPostBackOnFilter="true" ShowFilterIcon="false" AndCurrentFilterFunction="Contains"
                        UniqueName="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên %>" DataField="NAME" SortExpression="NAME"
                        AutoPostBackOnFilter="true" ShowFilterIcon="false" AndCurrentFilterFunction="Contains"
                        UniqueName="NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thang lương %>" DataField="SAL_GROUP_NAME"
                        SortExpression="SAL_GROUP_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        AndCurrentFilterFunction="Contains" UniqueName="SAL_GROUP_NAME" /> 
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức lương từ %>" DataField="SAL_FR" SortExpression="SAL_FR"
                        UniqueName="SAL_FR">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức lương đến %>" DataField="SAL_TO" SortExpression="SAL_TO"
                        UniqueName="SAL_TO">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Thứ tự %>" DataField="ORDERS" SortExpression="ORDERS"
                        UniqueName="ORDERS">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                        UniqueName="REMARK" />                    
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        SortExpression="ACTFLG" UniqueName="ACTFLG" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;

        var splitterID = 'ctl00_MainContent_ctrlPA_SalaryLevel_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_SalaryLevel_RadPane2';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_SalaryLevel_RadPane1';
        var validateID = 'MainContent_ctrlPA_SalaryLevel_valSum';
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
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize);
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadCodeBlock>

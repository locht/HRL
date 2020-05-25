<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAT_Formula.ascx.vb"
    Inherits="Attendance.ctrlAT_Formula" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
   @media screen and (-webkit-min-device-pixel-ratio:0) {
        #ctl00_MainContent_ctrlAT_Formula_txtDesc    
        {       
            height: 56px;
        } 
    }
    .RadGrid_Metro .rgRow>td:first-child 
    {
        padding :0 !important;
    }
    .RadGrid_Metro .rgAltRow>td:first-child 
    {
        padding :0 !important;
    }
    .RadGrid_Metro .rgHeader
    {
        padding :0 !important;
    }
    #ctl00_MainContent_ctrlAT_Formula_rgData_ctl00_ctl02_ctl02_FilterTextBox_STATUS
    {
        display : none;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarSalaryGroups" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table style="display: none" class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Đối tượng lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="rcboObjSalary" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqObjSalary" ControlToValidate="rcboObjSalary"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Đối tượng lương %>" ToolTip="<%$ Translate: Bạn phải chọn Đối tượng lương %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalObjSalary" runat="server" ControlToValidate="rcboObjSalary"
                        ErrorMessage="<%$ Translate: Đối tượng lương không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Đối tượng lương không tồn tại hoặc đã ngừng áp dụng. %>" >
                    </asp:CustomValidator>
                </td>
                <td> <%# Translate("Nhóm lương")%><span class="lbReq">*</span></td>
                <td><tlk:RadTextBox ID="rtxtNameVN" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqNameVN" ControlToValidate="rtxtNameVN"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhóm lương %>" ToolTip="<%$ Translate: Bạn phải nhập nhóm lương %>">
                    </asp:RequiredFieldValidator>
                </td>                    
            </tr>
            <tr>
              <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdpStartDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqStartDate" ControlToValidate="rdpStartDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày kết thúc")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdpEndDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="rdpEndDate"
                        ControlToCompare="rdpStartDate" Operator="GreaterThanEqual" Type="Date" 
                        ErrorMessage="Ngày kết thúc phải lớn hơn ngày hiệu lực" />
                </td>
            </tr>            
            <tr>
                <td class="lb">
                    <%# Translate("Thứ tự")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnmIdx" runat="server" Width="60px" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="1" NumberFormat-AllowRounding="false">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtDesc" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Sao chép tới")%>
                </td>
                <td colspan="3">
                    <tlk:RadComboBox ID="rcboCopyObjSalary" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true">
             <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
             </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,FML_NAME,EFFECT_DATE,EXPIRE_DATE,STATUS,CFDESC">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">                        
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhóm công thức %>" DataField="FML_NAME" SortExpression="FML_NAME" UniqueName="FML_NAME" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE" UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="EXPIRE_DATE" UniqueName="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EXPIRE_DATE">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>   
                    <tlk:GridTemplateColumn UniqueName="STATUS" DataField="STATUS" HeaderText="<%$ Translate: Trạng thái %>"
                        SortExpression="STATUS" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="ChkSTATUS" runat="server" Readonly="true" Checked='<%# IIf(DataBinder.Eval(Container.DataItem, "STATUS").ToString() <> "0", True, False) %>' />
                        </ItemTemplate>
                    </tlk:GridTemplateColumn>          
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnRowDblClick="gridRowDblClick" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
                <KeyboardNavigationSettings AllowSubmitOnEnter="true" EnableKeyboardShortcuts="true" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px"  EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin thiết lập công thức công%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlAT_Formula_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlAT_Formula_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlAT_Formula_RadPane2';
        var validateID = 'MainContent_ctrlAT_Formula_valSum';
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

        function cusObjSalary(oSrc, args) {
            var cbo = $find("<%# rcboObjSalary.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function CheckValidate() {
            var bCheck = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            return 0;
        }

        function OpenEdit() {
            var id = $find('<%= rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Attendance&fid=ctrlAT_FormulaDetail&group=Setting&gUID=' + id + '', "_self"); 
        }

        function gridRowDblClick(sender, eventArgs) {
            var bCheck = CheckValidate();
            var n;
            var m;
            if (bCheck == 0) {
                OpenEdit();
            }
            if (bCheck == 1) {
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }
            args.set_cancel(true);

        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                var grid = $find("<%=rgData.ClientID %>");
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                if (rows.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }

            if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData', 0, 0, 7);
                }
                else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clRadDatePicker() {
            $('#ctl00_MainContent_ctrlAT_Formula_rdpStartDate_dateInput').val('');
            $('#ctl00_MainContent_ctrlAT_Formula_rdpEndDate_dateInput').val('');
        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData', 0, 0, 7);
        }

    </script>
</tlk:RadCodeBlock>

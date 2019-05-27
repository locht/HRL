<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_WelfareList.ascx.vb"
    Inherits="Profile.ctrlHU_WelfareList" %>
    <link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
 
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarWelfareLists" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="175px" Scrolling="None">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td colspan="4">
                </td>
                <td>                    
                    <asp:Label ID="lbGender" runat="server" Text="Giới tính"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbContractType" runat="server" Text="Loại hợp đồng"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbCode" runat="server" Text="Mã chế độ phúc lợi"></asp:Label>                    
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" SkinID="ReadOnly" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server" ValidationGroup="WelfareList"
                        CausesValidation="false" ErrorMessage="Bạn phải nhập mã chế độ phúc lợi"
                        ToolTip="Bạn phải nhập mã chế độ phúc lợi.">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="Mã chế độ phúc lợi đã tồn tại."
                        ToolTip="Mã chế độ phúc lợi đã tồn tại." ValidationGroup="WelfareList">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="WelfareList" ErrorMessage="Mã không được chứa ký tự đặc biệt và khoảng trắng"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbName" runat="server" Text="Tên chế độ phúc lợi"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server" ValidationGroup="WelfareList"
                        CausesValidation="false" ErrorMessage="Bạn phải nhập tên chế độ phúc lợi."
                        ToolTip="Bạn phải nhập tên chế độ phúc lợi."></asp:RequiredFieldValidator>
                </td>
                <td rowspan="5" style="vertical-align: top;">
                    <tlk:RadListBox ID="lstbGender" CheckBoxes="true" runat="server" Height="60px" Width="100px"
                        OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging">
                    </tlk:RadListBox>
                </td>
                <td rowspan="5" style="vertical-align: top;">
                    <tlk:RadListBox ID="lstCONTRACT_TYPE" CheckBoxes="true" runat="server" Height="135px"
                        Width="290px">
                    </tlk:RadListBox>
                    <asp:CustomValidator ID="cvalContractType" runat="server" ValidationGroup="WelfareList" ControlToValidate="lstCONTRACT_TYPE"
                        ErrorMessage="Loại hợp đồng không tồn tại hoặc đã ngừng áp dụng."
                        ToolTip="Loại hợp đồng không tồn tại hoặc đã ngừng áp dụng.">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbMoney" runat="server" Text="Số tiền"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmMONEY" MinValue="1" MaxLength="38" runat="server">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="cvalMONEY" ControlToValidate="nmMONEY" runat="server" ValidationGroup="WelfareList"
                        ErrorMessage="Bạn phải nhập số tiền." ToolTip="Bạn phải nhập số tiền.">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbSENIORITY" runat="server" Text="Thâm niên (tháng)"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmSENIORITY" SkinID="Number" MaxLength="38" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbCHILD_OLD_FROM" runat="server" Text="Tuổi con nhỏ từ"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmCHILD_OLD_FROM" MinValue="0" MaxLength="38" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbCHILD_OLD_TO" runat="server" Text="đến"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmCHILD_OLD_TO" MinValue="0" MaxLength="38" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbSTART_DATE" runat="server" Text="Ngày hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="dpSTART_DATE">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="dpSTART_DATE" ValidationGroup="WelfareList"
                        runat="server" ErrorMessage="Bạn phải nhập ngày hiệu lực." ToolTip="Bạn phải nhập ngày hiệu lực.">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbEND_DATE" runat="server" Text="Ngày hết hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="dpEND_DATE">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="WelfareList" ToolTip="Ngày hết hiệu lực phải lớn hơn ngày hiệu lực."
                        ErrorMessage="Ngày hết hiệu lực phải lớn hơn ngày hiệu lực."
                        Type="Date" Operator="GreaterThan" ControlToCompare="dpSTART_DATE" ControlToValidate="dpEND_DATE"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                </td>
                <td>
                    <tlk:RadButton ID="chkIS_AUTO" AutoPostBack="false" ToggleType="CheckBox" ButtonType="ToggleButton" ValidateRequestMode="Disabled"
                    ValidationGroup="a" runat="server" Text="Tự động áp dụng.">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="100%">
        <tlk:RadGrid PageSize=50 ID="rgWelfareList" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,CODE,NAME,CONTRACT_TYPE_NAME,CONTRACT_TYPE,GENDER,SENIORITY,MONEY,START_DATE,END_DATE,IS_AUTO,CHILD_OLD_FROM,CHILD_OLD_TO">
                <Columns>
                   <%--<tlk:gridclientselectcolumn uniquename="cbStatus" headerstyle-horizontalalign="center"
                        headerstyle-width="30px" itemstyle-horizontalalign="center">
                    </tlk:gridclientselectcolumn>
                    <tlk:gridboundcolumn datafield="id" visible="false" />
                    <tlk:gridboundcolumn headertext="Mã chế độ phúc lợi" datafield="code"
                        sortexpression="code" uniquename="code" />
                    <tlk:gridboundcolumn headertext="Tên chế độ phúc lợi" datafield="name"
                        sortexpression="name" uniquename="name">
                        <headerstyle width="200px" />
                    </tlk:gridboundcolumn>
                    <tlk:gridboundcolumn headertext="Loại hợp đồng" datafield="contract_type_name"
                        sortexpression="contract_type_name" uniquename="contract_type_name">
                        <headerstyle width="250px" />
                    </tlk:gridboundcolumn>
                    <tlk:gridboundcolumn headertext="Giới tính" datafield="gender_name"
                        sortexpression="gender_name" uniquename="gender_name" />
                    <tlk:gridnumericcolumn headertext="Thâm niên" datafield="seniority"
                        sortexpression="seniority" uniquename="seniority" />
                    <tlk:gridnumericcolumn headertext="Tuổi con nhỏ từ" datafield="child_old_from"
                        sortexpression="child_old_from" uniquename="child_old_from" />
                    <tlk:gridnumericcolumn headertext="Tuổi con nhỏ đến" datafield="child_old_to"
                        sortexpression="child_old_to" uniquename="child_old_to" />
                    <tlk:gridnumericcolumn headertext="Số tiền" datafield="money" sortexpression="money"
                        dataformatstring="{0:n0}" uniquename="money" />
                    <tlk:griddatetimecolumn headertext="Ngày bắt đầu" datafield="start_date"
                        dataformatstring="{0:dd/mm/yyyy}" sortexpression="start_date" uniquename="start_date" />
                    <tlk:griddatetimecolumn headertext="Ngày kết thúc" datafield="end_date"
                        dataformatstring="{0:dd/mm/yyyy}" sortexpression="end_date" uniquename="end_date" />
                    <tlk:gridcheckboxcolumn headertext="Tự động áp dụng" datafield="is_auto"
                        allowfiltering="false"  sortexpression="is_auto" uniquename="is_auto" />
                    <tlk:gridboundcolumn headertext="Trạng thái" datafield="actflg"
                        sortexpression="actflg" uniquename="actflg" />--%>
                </Columns>
                <HeaderStyle Width="120px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }
        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function StandardConfirm(sender, args) { 
        
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }
        // Resize when save success
        function ResizeSplitterSaveSuccess() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - 175);
                pane.set_height(175);
            }, 200);
        }
        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - 175);
                pane.set_height(175);
                pane2.set_height(splitter.get_height() - 175 - 1);
            }
        }
      
    </script>
</tlk:RadCodeBlock>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDMKieuCong.ascx.vb"
    Inherits="Attendance.ctrlDMKieuCong" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="200px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbCode" Text ="Mã ký hiệu công" ></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" SkinID="Textbox50" runat="server" Width="250px">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCode"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập mã ký hiệu công. %>"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã kiểu công đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã kiểu công đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbIsLeave" Text ="Công nghỉ"  ></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkIsLeave" runat="server" onclick="enableTextbox(this.id) "/>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbIS_REG_SHIFT" Text ="Đăng ký nghỉ theo ca" ></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkIS_REG_SHIFT" runat="server" Checked = "false"/>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbIsCalHoliday" Text ="Tính ngày nghỉ, lễ/tết " ></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="ckIsCalHoliday" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbNameVN" Text ="Tên ký hiệu công" ></asp:Label><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNameVN" runat="server"  SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNameVN"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tên kiểu công. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbNote" Text ="Mô tả" ></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgDanhMuc" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME_VN,NAME_EN,EFFECT_DATE,IS_LEAVE,ACTFLG,NOTE,IS_CALHOLIDAY,IS_REG_SHIFT">
                <Columns>
                    <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Mã ký hiệu công " DataField="CODE"
                        UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="Tên ký hiệu công" DataField="NAME_VN"
                        UniqueName="NAME_VN" SortExpression="NAME_VN" />
                    <tlk:GridCheckBoxColumn HeaderText="Công nghỉ" AllowFiltering="false"
                        DataField="IS_LEAVE" SortExpression="IS_LEAVE" UniqueName="IS_LEAVE" />
                    <tlk:GridCheckBoxColumn HeaderText="Tính ngày nghỉ, lễ/tết" AllowFiltering="false"
                        DataField="IS_CALHOLIDAY" SortExpression="IS_CALHOLIDAY" UniqueName="IS_CALHOLIDAY" />
                    <tlk:GridBoundColumn HeaderText="Mô tả" DataField="NOTE" UniqueName="NOTE"
                        SortExpression="NOTE" />
                    <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG">
                        <HeaderStyle Width="100px" />
                    </tlk:GridBoundColumn>--%>
                </Columns>
                <HeaderStyle Width="100px" />
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
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
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
        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
        function enableTextbox(checkbox) {
            document.getElementById('<%= chkIS_REG_SHIFT.ClientID %>').disabled = !document.getElementById(checkbox).checked;
        }
    </script>
</tlk:RadCodeBlock>

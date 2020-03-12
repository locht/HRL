<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SalaryExRate.ascx.vb"
    Inherits="Payroll.ctrlPA_SalaryExRate" %>
<%@ Import Namespace="Framework.UI" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="165px" Scrolling="None">
        <tlk:RadToolBar ID="tbarSalaryGroups" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <asp:Label ID="lbFrCuType" runat="server" Text="Loại ngoại tệ"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                     <tlk:RadComboBox ID="cboFrCuType" runat="server" >
                    </tlk:RadComboBox>    
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="cboFrCuType" runat="server"
                        ErrorMessage="Bạn phải nhập loại ngoại tệ" ToolTip="Bạn phải nhập loại ngoại tệ">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="cboFrCuType" runat="server" ErrorMessage="Loại ngoại tệ đã tồn tại."
                        ToolTip="Loại ngoại tệ đã tồn tại.">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Loại ngoại tệ không được chứa ký tự đặc biệt và khoảng trắng"
                        ControlToValidate="cboFrCuType" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>

                <td class="lb">
                    <asp:Label ID="lbEffectDate" runat="server" Text="Ngày hiệu lực"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" ControlToValidate="rdEffectDate" runat="server"
                        ErrorMessage="Bạn phải nhập ngày hiệu lực." ToolTip="Bạn phải nhập ngày hiệu lực.">
                    </asp:RequiredFieldValidator>
                </td>  
            </tr>           
            <tr>
               <td class="lb">
                    <%# Translate("Tỷ giá")%>
                     <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtExRate" MaxLength="255" runat="server">
                    </tlk:RadNumericTextBox>                    
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbRemark"  Text ="Ghi chú"></asp:Label>
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
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="NAME,EFFECT_DATE,REMARK,FOREIGN_CURRENCY_TYPE,ORDERS">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />              
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại ngoại tệ %>" DataField="FOREIGN_CURRENCY_TYPE"
                        UniqueName="FOREIGN_CURRENCY_TYPE" SortExpression="FOREIGN_CURRENCY_TYPE" />     
                    <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>   
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỷ giá %>" DataField="NAME"
                        UniqueName="NAME" SortExpression="NAME" /> 
                      <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                        SortExpression="REMARK" />
                </Columns>
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
    </script>
</tlk:RadCodeBlock>

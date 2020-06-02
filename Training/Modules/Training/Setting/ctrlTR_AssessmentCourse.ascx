<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_AssessmentCourse.ascx.vb"
    Inherits="Training.ctrlTR_AssessmentCourse" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="165px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <asp:Label ID="Label1" runat="server" Text="Năm"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                     <tlk:RadNumericTextBox ID="rtxtYear" runat="server" SkinId="NUMBER">
                    </tlk:RadNumericTextBox>    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rtxtYear" runat="server"
                        ErrorMessage="Bạn phải nhập năm" ToolTip="Bạn phải nhập năm">
                    </asp:RequiredFieldValidator>                      
                </td>

                <td class="lb">
                    <asp:Label ID="lbFrCuType" runat="server" Text="Khóa đạo tạo"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                     <tlk:RadComboBox ID="cboCourse" runat="server" >
                    </tlk:RadComboBox>    
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="cboCourse" runat="server"
                        ErrorMessage="Bạn phải chọn Khóa đạo tạo" ToolTip="Bạn phải chọn Khóa đạo tạo">
                    </asp:RequiredFieldValidator>                      
                </td>
                <td class="lb">
                    <%# Translate("Mẫu biểu")%><span class="lbReq">*</span>
                </td>
                <td>
                     <tlk:RadComboBox ID="cboAssessment" runat="server" >
                    </tlk:RadComboBox>  
                    <asp:RequiredFieldValidator ID="reqNameVN" ControlToValidate="cboAssessment" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn Mẫu biểu %>" ToolTip="<%$ Translate: Bạn phải chọn Mẫu biểu %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True" Height="100%" >          
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="YEAR,REMARK,COURSE_ID,ASSESSMENT_FROM_ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                      <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR"
                        UniqueName="YEAR" SortExpression="YEAR" HeaderStyle-Width="30%"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Khóa đạo tạo %>" DataField="COURSE_NAME"
                        UniqueName="COURSE_NAME" SortExpression="COURSE_NAME" HeaderStyle-Width="30%"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mẫu biểu %>" DataField="ASSESSMENT_NAME"
                        UniqueName="ASSESSMENT_NAME" SortExpression="ASSESSMENT_NAME" HeaderStyle-Width="30%"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                        SortExpression="REMARK" HeaderStyle-Width="30%" />
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

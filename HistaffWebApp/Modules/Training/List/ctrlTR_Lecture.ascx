<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_Lecture.ascx.vb"
    Inherits="Training.ctrlTR_Lecture" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="180px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:HiddenField runat="server" ID="hidLectureID" />
        <table class="table-form">
            <tr>
                <td>
                </td>
                <td>
                    <tlk:RadButton ButtonType="ToggleButton" ToggleType="CheckBox" Checked="true" runat="server"
                        CausesValidation="false" ID="chkIsLocal" Text="<%$ Translate: Giảng viên nội bộ %>">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <asp:CheckBox ID="chkJoined" runat="server" Text="<%$ Translate: Đã từng tham gia đào tạo ở ACV %>" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trung tâm đào tạo")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCenter" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusCenter" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Trung tâm đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Trung tâm đào tạo %>" ClientValidationFunction="cusCenter">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Mã giảng viên")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" MaxLength="255" runat="server" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindLecture" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mã giảng viên %>" ToolTip="<%$ Translate: Bạn phải nhập Mã giảng viên %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên giảng viên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên giảng viên %>" ToolTip="<%$ Translate: Bạn phải nhập Tên giảng viên %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số điện thoại")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPhone" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Email")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmail" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Lĩnh vực đào tạo")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboFieldTrain">
                    </tlk:RadComboBox>          
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" SkinID="TextBox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="LECTURE_CODE,LECTURE_NAME,TR_CENTER_ID,LECTURE_ID,PHONE,EMAIL,REMARK,IS_LOCAL,FIELD_TRAIN_ID,IS_JOINED">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã giảng viên %>" DataField="LECTURE_CODE"
                        UniqueName="LECTURE_CODE" SortExpression="LECTURE_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên giảng viên %>" DataField="LECTURE_NAME"
                        UniqueName="LECTURE_NAME" SortExpression="LECTURE_NAME" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đã từng tham gia đào tạo ở ACV %>" DataField="IS_JOINED"
                        UniqueName="IS_JOINED" SortExpression="IS_JOINED" AllowFiltering ="false"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Lĩnh vực đào tạo %>" DataField="FIELD_TRAIN_NAME"
                        UniqueName="FIELD_TRAIN_NAME" SortExpression="FIELD_TRAIN_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trung tâm đào tạo %>" DataField="TR_CENTER_NAME"
                        UniqueName="TR_CENTER_NAME" SortExpression="TR_CENTER_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số điện thoại %>" DataField="PHONE"
                        UniqueName="PHONE" SortExpression="PHONE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Email %>" DataField="EMAIL" UniqueName="EMAIL"
                        SortExpression="EMAIL" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" ShowFilterIcon="true" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK"
                        UniqueName="REMARK" SortExpression="REMARK" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindLecture" runat="server"></asp:PlaceHolder>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusCenter(oSrc, args) {
            var cbo = $find("<%# cboCenter.ClientID%>");
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

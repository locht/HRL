<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_ProgramNewEdit.ascx.vb"
    Inherits="Training.ctrlTR_ProgramNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidRequestID" runat="server" />
<asp:HiddenField ID="hidCourseID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<style type="text/css">
    div.RadComboBox_Office2007
    {
        height: 22px;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Khai báo yêu cầu đào tạo chi tiết")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Năm đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtYear" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="" MinValue="1900" MaxLength="2999" CausesValidation="false"
                        AutoPostBack="true" ShowSpinButtons="true">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqYear" ControlToValidate="rntxtYear" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Năm đào tạo %>" ToolTip="<%$ Translate: Bạn phải nhập Năm đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Phòng ban")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindOrg" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Phòng ban %>" ToolTip="<%$ Translate: Bạn phải nhập Phòng ban %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Khóa đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboPlan" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>                    
                    <asp:RequiredFieldValidator ID="reqcboPlan" ControlToValidate="cboPlan" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn Khóa đào tạo %>" ToolTip="<%$ Translate: Bạn phải chọn Khóa đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên chương trình đào tạo")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtName" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Tên chương trình đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Tên chương trình đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Nhóm chương trình")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtNhomCT" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Hình thức")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboHinhThuc" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>           
                    <asp:RequiredFieldValidator ID="reqcboHinhThuc" ControlToValidate="cboHinhThuc" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn hình thức %>" ToolTip="<%$ Translate: Bạn phải chọn hình thức %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tính chất nhu cầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboTinhchatnhucau" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqcboTinhchatnhucau" ControlToValidate="cboTinhchatnhucau" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn tính chất nhu cầu %>" ToolTip="<%$ Translate: Bạn phải chọn tính chất nhu cầu %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Lĩnh vực đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtLinhvuc" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thời gian đào tạo")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Thời lượng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtDuration" runat="server" NumberFormat-GroupSeparator="."
                        NumberFormat-DecimalSeparator="," NumberFormat-DecimalDigits="1" MinValue="0"
                        Width="75px">
                    </tlk:RadNumericTextBox>
                    <tlk:RadComboBox runat="server" ID="cboDurationType" Width="80px">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqDuration" ControlToValidate="rntxtDuration" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn Thời lượng %>" ToolTip="<%$ Translate: Bạn phải chọn Thời lượng %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdStartDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdStartDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Từ ngày %>" ToolTip="<%$ Translate: Bạn phải nhập Từ ngày %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEndDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdEndDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Đến ngày %>" ToolTip="<%$ Translate: Bạn phải nhập Đến ngày %>">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdEndDate"
                        ControlToCompare="rdStartDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Đến ngày phải lớn hơn Từ ngày %>"
                        ToolTip="<%$ Translate: Đến ngày phải lớn hơn Từ ngày %>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Thời gian học")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDurationStudy" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Trong giờ HC")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtDurationHC" runat="server" NumberFormat-GroupSeparator="."
                        NumberFormat-DecimalSeparator="," NumberFormat-DecimalDigits="1" MinValue="0">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngoài giờ HC")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtDurationOT" runat="server" NumberFormat-GroupSeparator="."
                        NumberFormat-DecimalSeparator="," NumberFormat-DecimalDigits="1" MinValue="0">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Chi phí đào tạo")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số lượng học viên")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtNumberStudent" runat="server" SkinID="Number" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>
                <td colspan="4" valign="middle">
                    <tlk:RadButton ID="btnFindOrgCost" runat="server" CausesValidation="false" Text="Thêm phòng ban" />
                    <tlk:RadButton ID="btnDel" runat="server" CausesValidation="false" Text="Xóa phòng ban" />
                    <tlk:RadButton ID="btnCalCost" runat="server" CausesValidation="false" Text="Tính chi phí chi tiết" />
                    <asp:Label ID="lblDVT" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:CheckBox ID="chkThilai" runat="server" Text="<%$ Translate: Có thi lại? %>" />
                </td>
                <td>
                    <asp:CheckBox ID="chkIsReimburse" runat="server" Text="<%$ Translate: Có bồi hoàn? %>" />
                </td>
                <td colspan="4" rowspan="5">
                    <div id="divGrid" runat="server" style="float: left; width: 460px;">
                        <tlk:RadGrid ID="rgChiPhi" runat="server" AutoGenerateColumns="true" AllowPaging="false"
                            AllowSorting="false" AllowMultiRowSelection="false" CellSpacing="0" GridLines="Vertical"
                            Width="100%" Height="150px">
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <PagerStyle Visible="false" />
                            <MasterTableView DataKeyNames="ORG_ID,ORG_NAME,COST_COMPANY" ClientDataKeyNames="ORG_ID,ORG_NAME,COST_COMPANY">
                                <Columns>
                                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn DataField="ORG_ID" Visible="false" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                        SortExpression="ORG_NAME" UniqueName="ORG_NAME">
                                    </tlk:GridBoundColumn>
                                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: Chi phí đào tạo %>" DataField="COST_COMPANY"
                                        SortExpression="COST_COMPANY" UniqueName="COST_COMPANY" HeaderStyle-Width="150px">
                                        <ItemTemplate>
                                            <tlk:RadNumericTextBox ID="ValueTS" runat="server" CausesValidation="false" Width="120px"
                                                Value='<%# Cint(Eval("COST_COMPANY")) %>'>
                                                <NumberFormat AllowRounding="false" DecimalDigits="0" DecimalSeparator="." GroupSeparator=","
                                                    GroupSizes="3" />
                                            </tlk:RadNumericTextBox>
                                        </ItemTemplate>
                                    </tlk:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </tlk:RadGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tổng chi phí đào tạo (USD)")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostCompanyUS" runat="server" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="," MinValue="0" ReadOnly="true" CausesValidation="false">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chi phí/1 học viên (USD)")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostOfStudentUS" runat="server" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="," MinValue="0" ReadOnly="true" CausesValidation="false">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tổng chi phí đào tạo (VNĐ)")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostCompany" runat="server" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="," MinValue="0" ReadOnly="true" CausesValidation="false">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chi phí/1 học viên (VNĐ)")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostOfStudent" runat="server" NumberFormat-DecimalDigits="2"
                        NumberFormat-GroupSeparator="," MinValue="0" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Đối tượng tham gia")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <%# Translate("Đơn vị tham gia")%>
                </td>
                <td>
                </td>
                <td>
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                </td>
                <td>
                    <%# Translate("Công việc liên quan")%>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="2">
                    <tlk:RadListBox ID="lstPartDepts" runat="server" Height="100px" Width="100%">
                    </tlk:RadListBox>
                </td>
                <td colspan="2">
                    <tlk:RadListBox ID="lstPositions" runat="server" Height="100px" Width="100%">
                    </tlk:RadListBox>
                </td>
                <td>
                    <tlk:RadListBox ID="lstWork" runat="server" Height="100px" Width="100%">
                    </tlk:RadListBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin nhà cung cấp")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngôn ngữ giảng dạy")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboLanguage" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị chủ trì đào tạo")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboUnit" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="3">
                    <%# Translate("Trung tâm đào tạo")%>
                </td>
                <td>
                    <%# Translate("Giảng viên")%>
                </td>
                <%--<td class="lb">
                    <asp:CheckBox ID="chkIsLocal" runat="server" Text="<%$ Translate: Trong công ty %>"
                        AutoPostBack="true" CausesValidation="false" />
                </td>--%>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="3">
                    <tlk:RadListBox runat="server" ID="lstCenter" Width="100%" Height="100px" CheckBoxes="true"
                        OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging" />
                </td>
                <td colspan="2">
                    <tlk:RadListBox runat="server" ID="lstLecture" Width="100%" Height="100px" CheckBoxes="true"
                        OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging" />
                </td>
            </tr>
            <%--<tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Nội dung")%></b>
                    <hr />
                </td>
            </tr>--%>
            <tr>
                <td class="lb">
                    <%# Translate("Nội dung đào tạo")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtContent" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mục tiêu")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtTargetTrain" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Địa điểm tổ chức")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtVenue" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtRemark" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">


        function cusPlan(oSrc, args) {
            var cbo = $find("<%# cboPlan.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }


        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
        }

        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }

        function clientButtonClicking(sender, args) {
//            if (args.get_item().get_commandName() == 'CANCEL') {
//                getRadWindow().close(null);
//                args.set_cancel(true);
//            }
        }

    </script>
</tlk:RadCodeBlock>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDeclareEntitlementNBNewEdit.ascx.vb"
    Inherits="Attendance.ctrlDeclareEntitlementNBNewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb">
                 <asp:Label ID="lbCode" runat="server" Text="Mã nhân viên"></asp:Label>
                   <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server" SkinID="ReadOnly" ReadOnly="True" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="Bạn phải nhập nhân viên." ToolTip="Bạn phải nhập nhân viên."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbChucDanh" runat="server" Text="Chức danh"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtChucDanh" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                  <asp:Label ID="lbName" runat="server" Text="Họ tên"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                     <asp:Label ID="lbDonVi" runat="server" Text="Phòng ban"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDonVi" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                 <asp:Label ID="lbYear" runat="server" Text="Năm khai báo"></asp:Label>
                  <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtYear" MaxLength="4" SkinID="Number" runat="server">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtYear"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Năm khai báo %>" ToolTip="<%$ Translate: Bạn phải nhập Năm khai báo %>"> </asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="rvyear" MinimumValue="1901" MaximumValue="2999" ControlToValidate="txtYear"
                        runat="server" ErrorMessage="<%$ Translate: Năm khai báo phải nằm trong khoảng từ 1901 đến 2999 %>"
                        ToolTip="<%$ Translate: Năm khai báo phải nằm trong khoảng từ 1901 đến 2999 %>"> </asp:RangeValidator>
                </td>
                 <td class="lb">
                    <asp:Label ID="lbStartDate" runat="server" Text="Ngày vào công ty"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdStartDate" SkinID="ReadOnly" runat="server" AutoPostBack="True">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="4">
                 <asp:Label ID="lbInfo" runat="server" Text="Thông tin thâm niên"></asp:Label>
                        <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                   <asp:Label ID="lbStartMonth" runat="server" Text="Tháng điều chỉnh"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStartMonth" runat="server" SkinID="dDropdownList">
                        <Items>
                            <tlk:RadComboBoxItem Text="" Value="" />
                            <tlk:RadComboBoxItem Text="1" Value="1" />
                            <tlk:RadComboBoxItem Text="2" Value="2" />
                            <tlk:RadComboBoxItem Text="3" Value="3" />
                            <tlk:RadComboBoxItem Text="4" Value="4" />
                            <tlk:RadComboBoxItem Text="5" Value="5" />
                            <tlk:RadComboBoxItem Text="6" Value="6" />
                            <tlk:RadComboBoxItem Text="7" Value="7" />
                            <tlk:RadComboBoxItem Text="8" Value="8" />
                            <tlk:RadComboBoxItem Text="9" Value="9" />
                            <tlk:RadComboBoxItem Text="10" Value="10" />
                            <tlk:RadComboBoxItem Text="11" Value="11" />
                            <tlk:RadComboBoxItem Text="12" Value="12" />
                        </Items>
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                <asp:Label ID="lbADJUST_MONTH_TN2" runat="server" Text="Số tháng điều chỉnh"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtADJUST_MONTH_TN2" runat="server">
                    <NumberFormat DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                 <asp:Label ID="lbREMARK_TN" runat="server" Text="Lý do điều chỉnh"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtREMARK_TN" Width="100%" SkinID="Textbox1023" TextMode="MultiLine"
                        runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <b style="color: Red;">Chú ý: </b>
                    <br />
                    - Nhập số dương hệ thống sẽ (+)
                    <br />
                    - Nhập số âm hệ thống sẽ (-)
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            registerOnfocusOut('ctl00_MainContent_RadSplitter1');
        });
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

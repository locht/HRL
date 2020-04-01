<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_TrainingForeignNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_TrainingForeignNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgCode" runat="server" />
<asp:HiddenField ID="hidWorkingID" runat="server" />
<asp:HiddenField ID="hidWorkStatus" runat="server" />
<asp:HiddenField ID="hidEmployeeID" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<asp:HiddenField ID="hidPeriod" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarContract" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin đi công tác")%>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 200px">
                    <asp:Label ID="lbEmployeeCode" runat="server" Text="Mã nhân viên"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" SkinID="ReadOnly" runat="server" Width="130px"
                        ReadOnly="True">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="Bạn phải nhập nhân viên." ToolTip="Bạn phải nhập nhân viên."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 200px">
                    <asp:Label ID="lbEmployeeName" runat="server" Text="Họ tên nhân viên"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 200px">
                    <asp:Label ID="lbTITLE" runat="server" Text="Chức danh"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTITLE" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbBranch" runat="server" Text="Chi nhánh / Khối"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtBranch" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbOrg_Name" runat="server" Text="Phòng"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrg_Name" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbBan" runat="server" Text="Ban"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtBan" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbDecisionNo" runat="server" Text="Số quyết định"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionNo" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="rqDecisionNo" ControlToValidate="txtDecisionNo" runat="server"
                        ErrorMessage="Bạn phải nhập Số quyết định." ToolTip="Bạn phải nhập Số quyết định."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbStartDate" runat="server" Text="Từ ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdStartDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqStartDate" ControlToValidate="rdStartDate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày bắt đầu. %>" ToolTip="Bạn phải nhập ngày bắt đầu."> </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CompareStartDate" runat="server" ErrorMessage="Ngày bắt đầu phải lớn hơn ngày kết thúc gần nhất"
                        ToolTip="Ngày bắt đầu phải lớn hơn ngày kết thúc gần nhất">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbExpireDate" runat="server" Text="Đến ngày"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="rqExpireDate" ControlToValidate="rdExpireDate" runat="server"
                        ErrorMessage="Bạn phải nhập kết thúc." ToolTip="Bạn phải nhập ngày kết thúc."> </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdExpireDate"
                        Type="Date" ControlToCompare="rdStartDate" Operator="GreaterThanEqual" ErrorMessage="Ngày kết thúc phải lớn hơn ngày bắt đầu"
                        ToolTip="Ngày kết thúc phải lớn hơn ngày bắt đầu"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbPlaceFrom" runat="server" Text="Nơi đi"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtPlaceFrom">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="rqPlaceFrom" ControlToValidate="txtPlaceFrom" runat="server"
                        ErrorMessage="Bạn phải nhập Nơi đi." ToolTip="Bạn phải nhập Nơi đi."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbPlaceTo" runat="server" Text="Nơi đến"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtPlaceTo">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="rqPlaceTo" ControlToValidate="txtPlaceTo" runat="server"
                        ErrorMessage="Bạn phải nhập kết thúc." ToolTip="Bạn phải nhập ngày kết thúc."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbTypeVisa" runat="server" Text="Loại Visa"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboTypeVisa">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lvNumberVisa" runat="server" Text="Số Visa"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtNumberVisa">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDateCap" runat="server" Text="Ngày cấp visa"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDateCap">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDateHH" runat="server" Text="Ngày hết hạn visa"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDateHH">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbPlace" runat="server" Text="Nơi cấp"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtPlace">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbCost" runat="server" Text="Chi phí xin Visa"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnCost">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbNumberDate" runat="server" Text="Số ngày ở lại"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnNumberDate" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="rqNumberDate" ControlToValidate="rnNumberDate" runat="server"
                        ErrorMessage="Bạn phải nhập Số ngày ở lại." ToolTip="Bạn phải nhập Số ngày ở lại."> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbCostKH" runat="server" Text="Chi phí tiếp khách"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnCostKH">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbCostWork" runat="server" Text="Công tác phí"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnCostWork">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbCostHotel" runat="server" Text="Chi phí khách sạn"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnCostHotel">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbCostAnother" runat="server" Text="Chi phí khác"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnCostAnother">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbCostGo" runat="server" Text="Chi phí đi lại"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnCostGo">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkCostWork" Text="Có tính công tác phí hay không" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbSum" runat="server" Text="Tổng chi phí"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnSumCost" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbContent" runat="server" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtContent" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_ContractNewEdit_RadSplitter1');
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


        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //            }
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSigner" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSalary" runat="server"></asp:PlaceHolder>

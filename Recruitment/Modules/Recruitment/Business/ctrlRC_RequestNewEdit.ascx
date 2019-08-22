<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_RequestNewEdit.ascx.vb"
    Inherits="Recruitment.ctrlRC_RequestNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<style type="text/css">
    .ages
    {
        width: 39% !important;
        float: left;
    }
    .LevelLanguage
    {
        width: 44% !important;
        float: left;
    }
    .ages span
    {
        width: 100% !important;
    }
    .LevelLanguage div
    {
        width: 100% !important;
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
                <td class="lb">
                    <%# Translate("Phòng ban")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindOrg" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập phòng ban %>" ToolTip="<%$ Translate: Bạn phải nhập phòng ban %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 150px">
                    <%# Translate("Vị trí tuyển dụng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTitle" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusTitle" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn chức danh %>"
                        ToolTip="<%$ Translate: Bạn phải chọn chức danh %>" ClientValidationFunction="cusTitle">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày gửi yêu cầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSendDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdSendDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày gửi kế hoạch %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Ngày gửi kế hoạch %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 150px">
                    <%# Translate("Loại hợp đồng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboContractType" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb" style="width: 150px">
                    <%# Translate("Tính chất tuyển dụng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboRecruitProperty" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lý do tuyển dụng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboRecruitReason" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusRecruitReason" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Lý do tuyển dụng%>"
                        ToolTip="<%$ Translate: Bạn phải chọn Lý do tuyển dụng %>" ClientValidationFunction="cusRecruitReason">
                    </asp:CustomValidator>
                </td>
                <td>
                </td>
                <td style="width: 150px">
                    <asp:CheckBox ID="chkIsSupport" runat="server" Text="<%$ Translate: TNG hỗ trợ triển khai %>" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số lượng hiện có")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtCurrentNumber" ReadOnly="true" runat="server" SkinID="number">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số lượng định biên")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtPayrollLimit" ReadOnly="true" runat="server" SkinID="number">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số lượng tăng/giảm")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtDifferenceNumber" ReadOnly="true" runat="server" SkinID="number">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Diễn giải chi tiết")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRecruitReason" runat="server" TextMode="MultiLine" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td>
                </td>
                <td class="lb">
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindEmployee" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <%# Translate("Chọn người được thay thế")%>
                </td>
            </tr>
            <tr style="display: none">
                <td>
                </td>
                <td colspan="5">
                    <tlk:RadListBox ID="lstEmployee" runat="server" Width="100%" Height="100px">
                    </tlk:RadListBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trình độ học vấn")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboLearningLevel" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="cboLearningLevel"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Trình độ học vấn %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Trình độ học vấn %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Độ tuổi từ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAgeFrom" runat="server" NumberFormat-DecimalDigits="1"
                        NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="18"
                        MaxValue="100">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="rntxtAgeFrom"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Độ tuổi từ %>" ToolTip="<%$ Translate: Bạn phải nhập Độ tuổi từ %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Độ tuổi đến")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAgeTo" runat="server" NumberFormat-DecimalDigits="1"
                        NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="18"
                        MaxValue="100">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="rntxtAgeTo"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Độ tuổi đến %>" ToolTip="<%$ Translate: Bạn phải nhập Độ tuổi đến %>"> 
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rntxtAgeTo"
                        ControlToCompare="rntxtAgeFrom" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Độ tuổi đến phải lớn hơn Độ tuổi từ %>"
                        ToolTip="<%$ Translate: Độ tuổi đến phải lớn hơn Độ tuổi từ %>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nghiệp vụ chuyên môn")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboQualification" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="cboQualification"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nghiệp vụ chuyên môn %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Nghiệp vụ chuyên môn %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="display: none;">
                    <%# Translate("Đính kèm mô tả")%>
                </td>
                <td style="display: none;">
                    <tlk:RadButton ID="btnUploadFileDescription" runat="server" Text="Tải lên" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:HyperLink ID="hypFile" Visible="false" Target="_blank" runat="server"></asp:HyperLink>
                    <asp:HiddenField ID="hddFile" runat="server" />
                    <asp:LinkButton ID="btnDeleteFile" runat="server" Visible="false" CausesValidation="false"
                        OnClientClick="return confirm('Bạn có chắc chắn muốn xóa tệp tin này');">Xóa</asp:LinkButton>
                </td>
                <td class="lb">
                    <%# Translate("Kỹ năng đặc biệt")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSpecialSkills" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngoại ngữ")%><br>
                </td>
                <td style="width: 1px">
                    <tlk:RadComboBox ID="cboLanguage" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Trình độ ngoại ngữ")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboLanguageLevel" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Điểm ngoại ngữ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtScores" runat="server" SkinID="number">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Khả năng ngoại ngữ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtForeignAbility" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày cần đáp ứng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpectedJoinDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdExpectedJoinDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày đi làm dự kiến %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Ngày đi làm dự kiến %>"> 
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="rdExpectedJoinDate"
                        ControlToCompare="rdSendDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày đi làm dự kiến phải lớn hơn Ngày gửi yêu cầu %>"
                        ToolTip="<%$ Translate: Ngày đi làm dự kiến phải lớn hơn Ngày gửi yêu cầu %>"></asp:CompareValidator>
                </td>
                <td class="lb">
                    <%# Translate("Số năm kinh nghiệm tối thiểu")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtExperienceNumber" runat="server" NumberFormat-DecimalDigits="1"
                        NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="0"
                        MaxValue="100">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Trình độ tin học văn phòng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboComputerLevel" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trình độ tin học ứng dụng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtComputerAppLevel" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số lượng cần tuyển")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtRecruitNumber" runat="server" NumberFormat-DecimalDigits="1"
                        NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="0"
                        MaxValue="100" AutoPostBack="true">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rntxtRecruitNumber"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số lượng cần tuyển %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Số lượng cần tuyển %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                </td>
                <td style="width: 150px">
                    <asp:CheckBox ID="chkIsOver" runat="server" Text="<%$ Translate: Vượt định biên %>"
                        Checked="false" />
                </td>
                <td class="lb">
                    <%# Translate("Ưu tiên giới tính")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboGenderPriority" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả công việc")%><span class="lbReq">*</span>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtDescription"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Mô tả công việc %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Mô tả công việc %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Yêu cầu chính")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtMainTask" runat="server" TextMode="MultiLine" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mức độ ưu tiên")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtRequestExperience" runat="server" TextMode="MultiLine" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Đính kèm tập tin")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUpload" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Yêu cầu khác")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRequestOther" runat="server" TextMode="MultiLine" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlUpload ID="ctrlUpload2" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusTitle(oSrc, args) {
            var cbo = $find("<%# cboTitle.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusContractType(oSrc, args) {
            var cbo = $find("<%# cboContractType.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusRecruitReason(oSrc, args) {
            var cbo = $find("<%# cboRecruitReason.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
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

        function OnValueChanged(sender, args) {

        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>

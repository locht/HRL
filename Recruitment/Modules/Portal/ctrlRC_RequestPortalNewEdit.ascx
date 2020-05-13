<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_RequestPortalNewEdit.ascx.vb"
    Inherits="Recruitment.ctrlRC_RequestPortalNewEdit" %>
<%@ Import Namespace="Common" %>
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
<tlk:RadPane ID="RadPane2" runat="server" Height="200px">
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
    <tlk:RadMultiPage ID="mitiple" runat="server" Width="100%" ScrollBars="None">
        <tlk:RadPageView ID="rpvID" runat="server" Selected="true">
            <fieldset>
                <legend>
                        <%# Translate("Thông tin chung")%>
                    </legend>
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Phòng ban")%><span class="lbReq">*</span>
                </td>
                <td colspan="2">
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindOrg" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập phòng ban %>" ToolTip="<%$ Translate: Bạn phải nhập phòng ban %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td style="width: 150px">
                    <asp:CheckBox ID="chkIsInPlan" runat="server" Text="<%$ Translate: Trong kế hoạch %>"
                        AutoPostBack="True" CausesValidation="false" />
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
                    <%# Translate("Loại hợp đồng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboContractType" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusContractType" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Loại hợp đồng%>"
                        ToolTip="<%$ Translate: Bạn phải chọn Loại hợp đồng %>" ClientValidationFunction="cusContractType">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Lý do tuyển dụng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboRecruitReason" runat="server" OnSelectedIndexChanged="cboRecruitReason_SelectedIndexChanged">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusRecruitReason" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Lý do tuyển dụng%>"
                        ToolTip="<%$ Translate: Bạn phải chọn Lý do tuyển dụng %>" ClientValidationFunction="cusRecruitReason">
                    </asp:CustomValidator>
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
                    <%# Translate("Lý do tuyển dụng chi tiết")%>
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
            </table>
            </fieldset>
            <fieldset>
                    <legend>
                        <%# Translate("Chi tiết yêu cầu tuyển dụng")%>
                    </legend>
                    <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Trình độ học vấn")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboLearningLevel" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Độ tuổi từ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAgeFrom" runat="server" NumberFormat-DecimalDigits="1"
                        NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="18"
                        MaxValue="100">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Độ tuổi đến")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAgeTo" runat="server" NumberFormat-DecimalDigits="1"
                        NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="18"
                        MaxValue="100">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rntxtAgeTo"
                        ControlToCompare="rntxtAgeFrom" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Độ tuổi đến phải lớn hơn Độ tuổi từ %>"
                        ToolTip="<%$ Translate: Độ tuổi đến phải lớn hơn Độ tuổi từ %>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nghiệp vụ chuyên môn")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboQualification" runat="server">
                    </tlk:RadComboBox>
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
                    <%# Translate("Ngày đi làm dự kiến")%><span class="lbReq">*</span>
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
                    <%# Translate("Số năm kinh nghiệm")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtExperienceNumber" runat="server" NumberFormat-DecimalDigits="1"
                        NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="0"
                        MaxValue="100">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Trình độ tin học")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboComputerLevel" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số lượng tuyển nam")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtMaleNumber" runat="server" NumberFormat-DecimalDigits="1"
                        NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="0"
                        MaxValue="100">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                        <ClientEvents OnValueChanged="OnValueChanged"/>
                    </tlk:RadNumericTextBox>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="rntxtMaleNumber"
                        Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Vui lòng nhập số lượng nam %>"
                        ToolTip="<%$ Translate: Vui lòng nhập số lượng nam %>"></asp:CompareValidator>
                </td>
                <td class="lb">
                    <%# Translate("Số lượng tuyển nữ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtFemaleNumber" runat="server" NumberFormat-DecimalDigits="1"
                        NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="0"
                        MaxValue="100">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                        <ClientEvents OnValueChanged="OnValueChanged"/>
                    </tlk:RadNumericTextBox>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="rntxtFemaleNumber"
                        Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Vui lòng nhập số lượng nữ %>"
                        ToolTip="<%$ Translate: Vui lòng nhập số lượng nữ %>"></asp:CompareValidator>
                </td>
                <td class="lb">
                    <%# Translate("Số lượng cần tuyển")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtRecruitNumber" runat="server" NumberFormat-DecimalDigits="1"
                        NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="0"
                        MaxValue="100">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rntxtRecruitNumber"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số lượng tuyển nam hoặc Số lượng tuyển nữ %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Số lượng tuyển nam hoặc Số lượng tuyển nữ %>"> 
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả công việc")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhiệm vụ chính")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtMainTask" runat="server" TextMode="MultiLine" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Yêu cầu kinh nghiệm")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRequestExperience" runat="server" TextMode="MultiLine" Width="100%">
                    </tlk:RadTextBox>
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
        </fieldset> 
        </tlk:RadPageView>
    </tlk:RadMultiPage>
</tlk:RadPane>
 <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Enabled="true" Height="200px">
        <tlk:RadGrid ID="rdgDanhSachNhanVien" AllowPaging="true" AllowMultiRowEdit="true" runat="server"
            PageSize="50" Height="100%">
            <GroupingSettings CaseSensitive="false" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView EditMode="InPlace" AllowPaging="true" AllowCustomPaging="true" DataKeyNames="EMPLOYEE_CODE,EMPLOYEE_ID,EMPLOYEE_NAME,ORG_NAME,TITLE_NAME"
                ClientDataKeyNames="EMPLOYEE_CODE,EMPLOYEE_ID,EMPLOYEE_NAME,ORG_NAME,TITLE_NAME"
                CommandItemDisplay="Top">
                <CommandItemStyle Height="25px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="150px" ID="btnEmployee" runat="server" Text="Chọn tất cả nhân viên"
                                CausesValidation="false" CommandName="FindEmployee" TabIndex="3">
                            </tlk:RadButton>
                        </div>
                        <div style="float: right">
                            <tlk:RadButton Width="100px" ID="btnDeleteEmp" runat="server" Text="Xóa" CausesValidation="false"
                                CommandName="DeleteEmployee" TabIndex="3">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="ID" DataField="EMPLOYEE_ID" UniqueName="EMPLOYEE_ID"
                        ReadOnly="true" SortExpression="EMPLOYEE_ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="MSNV" DataField="EMPLOYEE_CODE" ReadOnly="true"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME"
                        ReadOnly="true" SortExpression="EMPLOYEE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME"
                        UniqueName="ORG_NAME" ReadOnly="true" SortExpression="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" ReadOnly="true" SortExpression="TITLE_NAME" />
                    <tlk:GridDateTimeColumn HeaderText="Ngày nghỉ việc" DataField="TER_LAST_DATE" ItemStyle-HorizontalAlign="Center"
                        SortExpression="TER_LAST_DATE" UniqueName="TER_LAST_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function cusTitle(oSrc, args) {
            var cbo = $find("<%# cboTitle.ClientID %>");
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

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>

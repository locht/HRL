﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_DisciplineNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_DisciplineNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidDecisionID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarDiscipline" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="350px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form"  onkeydown="return (event.keyCode!=13)">
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin quyết định")%>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server" TabIndex="1" AutoPostBack="true">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" ControlToValidate="rdEffectDate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDate" runat="server" TabIndex="2">
                    </tlk:RadDatePicker>
                    <asp:CustomValidator ID="cval_EffectDate_ExpireDate" runat="server" ErrorMessage="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực. %>"
                        ToolTip="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Trạng thái")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server" TabIndex="3">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqStatus" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn trạng thái %>"
                        ToolTip="<%$ Translate: Bạn phải chọn trạng thái  %>" ControlToValidate="cboStatus">
                    </asp:RequiredFieldValidator>
                     <asp:CustomValidator ID="cusStatus" ControlToValidate="cboStatus" runat="server" ErrorMessage="<%$ Translate: Trạng thái không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Trạng thái không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số quyết định")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionNo" runat="server" TabIndex="4">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqDecisionNo" ControlToValidate="txtDecisionNo"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số quyết định. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập số quyết định. %>"> 
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusDecisionNo" runat="server" ErrorMessage="<%$ Translate: Số quyết định đã tồn tại %>"
                        ToolTip="<%$ Translate: Số quyết định đã tồn tại %>">
                    </asp:CustomValidator>
                </td>
                <%--<td class="lb">
                    <%# Translate("Ngày ban hành")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdIssueDate" runat="server" TabIndex="5">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqIssueDate" ControlToValidate="rdIssueDate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày ban hành. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày ban hành. %>"> 
                    </asp:RequiredFieldValidator>
                </td>--%>

                 <td class="lb">
                    <%# Translate("Ngày ký")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSignDate" runat="server" TabIndex="6">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Người ký")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                   <%-- <tlk:RadTextBox ID = "txtIDEmp" runat = "server" Visible="false"></tlk:RadTextBox>--%>
                    <tlk:RadTextBox ID="txtSignerName" Width="130px" runat="server" ReadOnly="true" SkinID="ReadOnly" TabIndex="7">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindSinger" runat="server" SkinID="ButtonView" CausesValidation="false" TabIndex="8">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqSignerName" ControlToValidate="txtSignerName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập người ký. %>" ToolTip="<%$ Translate: Bạn phải nhập người ký. %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignerTitle" runat="server" TabIndex="9" ReadOnly="true" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>

                <%-- <td class="lb">
                    <%# Translate("Tên quyết định")%>
                </td>--%>
                <td>
                    <tlk:RadTextBox ID="txtDecisionName" runat="server" TabIndex="10" Visible="false">
                    </tlk:RadTextBox>
                </td>
                  <td class="lb">
                    <%# Translate("Tập tin đính kèm")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>              
                </td>
            </tr>
                <tr style="visibility:hidden">
                <td class="lb">
                    <tlk:RadTextBox ID="txtRemindLink" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin kỷ luật")%>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đối tượng")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDisciplineObj" runat="server" AutoPostBack="True" TabIndex="11"
                        CausesValidation="False">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqDisciplineObj" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập đối tượng %>"
                        ToolTip="<%$ Translate: Bạn phải nhập đối tượng  %>" ControlToValidate="cboDisciplineObj">
                    </asp:RequiredFieldValidator>
                     <asp:CustomValidator ID="cvalDisciplineObj" ControlToValidate="cboDisciplineObj" runat="server" ErrorMessage="<%$ Translate: Đối tượng không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Đối tượng không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Cấp kỷ luật")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDisciplineLevel" runat="server" TabIndex="12" CausesValidation="False">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqDisciplineLevel" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập cấp kỷ luật %>"
                        ToolTip="<%$ Translate: Bạn phải nhập cấp kỷ luật  %>" ControlToValidate="cboDisciplineLevel">
                    </asp:RequiredFieldValidator>
                     <asp:CustomValidator ID="cvalDisciplineLevel" ControlToValidate="cboDisciplineLevel" runat="server" ErrorMessage="<%$ Translate: Cấp kỷ luật không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Cấp kỷ luật không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Hình thức")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDisciplineType" runat="server" TabIndex="13" CausesValidation="False">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqDisciplineType" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập hình thức kỷ luật %>"
                        ToolTip="<%$ Translate: Bạn phải nhập cấp kỷ luật  %>" ControlToValidate="cboDisciplineType">
                    </asp:RequiredFieldValidator>
                     <asp:CustomValidator ID="cvalDisciplineType" ControlToValidate="cboDisciplineType" runat="server" ErrorMessage="<%$ Translate: Hình thức kỷ luật không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Hình thức kỷ luật không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:CheckBox ID="chkPhatTien" runat="server" Checked="false" CausesValidation="false" TextAlign="Right" AutoPostBack="true" />
                    <%# Translate("Số tiền")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtMoney" runat="server" SkinID="Money" TabIndex="14">
                    </tlk:RadNumericTextBox>
                    <asp:CustomValidator ID="cvalMoney" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số tiền phải >= 0. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập số tiền phải >= 0. %>">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalTotal" runat="server" ErrorMessage="<%$ Translate: Tổng số tiền của nhân viên phải bằng số tiền phạt %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tổng giá trị thiệt hại")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtIndemnifyMoney" runat="server" SkinID="Money" TabIndex="15">
                    </tlk:RadNumericTextBox>
                    <asp:CustomValidator ID="cvalIndemnifyTotal" runat="server" ErrorMessage="<%$ Translate: Tổng số tiền bồi thường của nhân viên phải bằng số tiền bồi thường %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Thanh toán tiền")%>
                </td>
                <td>
                    <tlk:RadButton ID="PaidInMoeny" ButtonType="ToggleButton" runat="server"></tlk:RadButton>
                </td>
                 <td class="lb">
                    <%# Translate("Lý do kỷ luật")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDisciplineReason" runat="server" TabIndex="16" CausesValidation="False" >
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqDisciplineReason" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập lý do kỷ luật %>"
                        ToolTip="<%$ Translate: Bạn phải nhập lý do kỷ luật  %>" ControlToValidate="cboDisciplineReason">
                    </asp:RequiredFieldValidator>
                     <asp:CustomValidator ID="cvalDisciplineReason" ControlToValidate="cboDisciplineReason" runat="server" ErrorMessage="<%$ Translate: Lý do kỷ luật không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Lý do kỷ luật không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Hành vi vi phạm")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" TextMode="MultiLine" TabIndex="17"
                        Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <asp:CheckBox ID="chkDeductFromSalary" runat="server" Text="<%$ Translate: Trừ vào lương %>"
                            Checked="false" CausesValidation="false" TextAlign="Right" AutoPostBack="true" />
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Năm")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmYear" runat="server" AutoPostBack="true" Enabled="false" TabIndex="18"
                        MaxLength="4" MinValue="1900" SkinID="Number">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Kỳ lương")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPeriod" runat="server" Enabled="false" TabIndex="19">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalPeriod" runat="server" ErrorMessage="<%$ Translate: Bạn chưa chọn kỳ lương thanh toán. %>"
                        ToolTip="<%$ Translate: Bạn chưa chọn kỳ lương thanh toán. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane runat="server" Scrolling="none" ID="PanelEmployee" Height="100%">
        <tlk:RadGrid PageSize=50 ID="rgEmployee" AllowPaging="false" AllowMultiRowEdit="true"
            runat="server" Height="100%" ShowFooter="True">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView EditMode="InPlace" AllowPaging="false" AllowCustomPaging="false"
                DataKeyNames="HU_DISCIPLINE_ID,HU_EMPLOYEE_ID,EMPLOYEE_CODE,MONEY" ClientDataKeyNames="HU_DISCIPLINE_ID,HU_EMPLOYEE_ID,EMPLOYEE_CODE,FULLNAME,TITLE_NAME,ORG_NAME,MONEY"
                CommandItemDisplay="Top">
                <CommandItemStyle Height="28px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="100px" ID="btnEmployee" runat="server" Text="<%$ Translate: Chọn nhân viên %>"
                                CausesValidation="false" CommandName="FindEmployee">
                            </tlk:RadButton>
                            <tlk:RadButton Width="100px" ID="btnShare" runat="server" Text="<%$ Translate: Chia đều %>"
                                CausesValidation="false" CommandName="ShareEmployee">
                            </tlk:RadButton>
                            <tlk:RadButton Width="100px" ID="btnCalc" runat="server" Text="<%$ Translate: Tính toán %>"
                                CausesValidation="false" CommandName="CalcEmployee">
                            </tlk:RadButton>
                        </div>
                        <div style="float: right;">
                            <tlk:RadButton Width="100px" ID="btnDeleteEmp" runat="server" Text="<%$ Translate: Xóa %>"
                                CausesValidation="false" CommandName="DeleteEmployee">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                        ReadOnly="true" UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" Aggregate="Count"
                        FooterText="Tổng: ">
                        <HeaderStyle Width="100px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULLNAME" UniqueName="FULLNAME"
                        ReadOnly="true" SortExpression="FULLNAME">
                        <HeaderStyle Width="120px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        ReadOnly="true" UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        ReadOnly="true" SortExpression="ORG_NAME">
                        <HeaderStyle Width="200px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền phạt %>" DataField="MONEY"
                        UniqueName="MONEY" DataFormatString="{0:n0}" SortExpression="MONEY" Aggregate="Sum">
                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền bồi thường %>" DataField="INDEMNIFY_MONEY"
                        UniqueName="INDEMNIFY_MONEY" DataFormatString="{0:n0}" SortExpression="INDEMNIFY_MONEY"
                        Aggregate="Sum">
                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                    </tlk:GridNumericColumn>
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" Width="120px" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
    <script type="text/javascript">

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
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                //var radWindow = $find('rwPopup');
                //radWindow.close();
                args.set_cancel(true);
            }
        }

        var enableAjax = true;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function RedirectTerminate(empID) {
            document.location.href = 'Dialog.aspx?mid=Profile&fid=ctrlHU_TerminateNewEdit&group=Business&noscroll=1&FormType=3&ter_reason=4350&empid=' + empID;
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>

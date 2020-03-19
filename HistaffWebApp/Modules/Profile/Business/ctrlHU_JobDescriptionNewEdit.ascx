<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_JobDescriptionNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_JobDescriptionNewEdit" %>
<%@ Import Namespace="Common" %>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<style type="text/css">
    div .rlbItem
    {
        float: left;
        width: 250px;
    }
    .setLineHeight
    {
        line-height: 30px;
    }
    .lb3
    {
        text-align: right;
        padding-right: 5px;
        padding-left: 5px;
        vertical-align: middle;
        width: 14%;
    }
    .control3
    {
        width: 20%;
    }
    .RadListBox_Metro .rlbGroup, .RadListBox_Metro .rlbTemplateContainer
    {
        border: none !important;
    }
</style>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal"
    SkinID="Demo">
    <tlk:RadPane ID="ToolbarPane" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking"
            ValidationGroup="EmpProfile" />
    </tlk:RadPane>
    <tlk:RadPane ID="DetailPane" runat="server" Scrolling="None" Height="73%">
        <tlk:RadTabStrip ID="rtabProfileInfo" runat="server" CausesValidation="false" MultiPageID="RadMultiPage1"
            AutoPostBack="false">
            <Tabs>
                <tlk:RadTab runat="server" ID="rtIdEmpInfo" PageViewID="rpvEmpInfo" Text="Mô tả công việc"
                    Selected="True">
                </tlk:RadTab>
                <tlk:RadTab runat="server" ID="rtJobRequest" PageViewID="rpvJobRequest" Text="<%$ Translate: Yêu cầu công việc %>">
                </tlk:RadTab>
            </Tabs>
        </tlk:RadTabStrip>
        <tlk:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" runat="server" Width="100%"
            ScrollBars="Auto" Height="90%">
            <tlk:RadPageView ID="rpvEmpInfo" runat="server" Width="100%">
                <table style="width: 99%" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td colspan="6">
                            <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary"
                                ValidationGroup="EmpProfile" />
                        </td>
                    </tr>
                </table>
                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                    <tr class="setLineHeight">
                        <td class="lb">
                            <asp:Label runat="server" ID="lbJobCode" Text="Mã công việc"></asp:Label>
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtJobCode" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbJobName" Text="Tên công việc"></asp:Label>
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtJobName" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbJobPowew" Text="Quyền hạn tương ứng"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtJobPower" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr class="setLineHeight">
                        <td class="lb">
                            <asp:Label runat="server" ID="lbOrg" Text="Đơn vị"></asp:Label>
                            <span class="lbReq">*</span>
                        </td>
                        <td class="control3">
                            <tlk:RadTextBox runat="server" ID="txtOrgName2" ReadOnly="true" />
                            <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator1"
                                ControlToValidate="txtOrgName2" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>"
                                ToolTip="<%$ Translate: Bạn phải chọn bộ phận %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbTitle" Text="Chức danh"></asp:Label>
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTitle" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ValidationGroup="JobDescription" ID="reqTitle" ControlToValidate="cboTitle"
                                runat="server" ErrorMessage="Bạn phải chọn chức danh" ToolTip="Bạn phải chọn chức danh">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbAttachFile" Text="File đính kèm"></asp:Label>
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
                    <tr class="setLineHeight">
                        <td class="lb">
                            <asp:Label runat="server" ID="lbTimeWorking" Text="Thời gian làm việc"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboTimeWorking" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbJobCondition" Text="Điều kiện làm việc"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtJobCondition" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbNote" Text="Ghi chú"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtNote" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr class="setLineHeight">
                        <td class="lb">
                            <asp:Label runat="server" ID="lbJobDes" Text="Mô tả công việc"></asp:Label>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtJobDescription" runat="server" TextMode="MultiLine" TabIndex="18"
                                Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr class="setLineHeight">
                        <td class="lb">
                            <asp:Label runat="server" ID="lbJobPar" Text="Đặc thù công việc"></asp:Label>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtJobParticularties" runat="server" TextMode="MultiLine" TabIndex="18"
                                Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr class="setLineHeight">
                        <td class="lb">
                            <asp:Label runat="server" ID="lbJobEnv" Text="Môi trường làm việc"></asp:Label>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtJobEnvironment" runat="server" TextMode="MultiLine" TabIndex="18"
                                Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr class="setLineHeight">
                        <td class="lb">
                            <asp:Label runat="server" ID="lbJobRep" Text="Trách nhiệm công việc"></asp:Label>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtJobReponsibility" runat="server" TextMode="MultiLine" TabIndex="18"
                                Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpvJobRequest" runat="server" Width="100%">
                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                    <tr class="setLineHeight">
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbLearningLV" Text="Trình độ học vấn"></asp:Label>
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboLearningLV" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator2"
                                ControlToValidate="cboLearningLV" runat="server" ErrorMessage="Bạn phải chọn trình độ học vấn"
                                ToolTip="Bạn phải chọn trình độ học vấn">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="setLineHeight">
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbTrainingForm" Text="Trình độ đào tạo chính quy"></asp:Label>
                        </td>
                        <td class="control3">
                            <tlk:RadComboBox runat="server" ID="cboTrainingForm" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbGraduateSchool" Text="Trường đào tạo"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboGraduateSchool" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbMajorRank" Text="Loại tốt nghiệp"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboMajorRank" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr class="setLineHeight">
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbTrainingForm_2" Text="Trình độ đào tạo phụ"></asp:Label>
                        </td>
                        <td class="control3">
                            <tlk:RadComboBox runat="server" ID="cboTrainingForm2" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbGradSchool2" Text="Trường đào tạo phụ"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboGraduateSchool2" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbMajor2" Text="Loại tốt nghiệp phụ"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboMajorRank2" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr class="setLineHeight">
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbComputer" Text="Trình độ tin học"></asp:Label>
                        </td>
                        <td class="control3">
                            <tlk:RadComboBox runat="server" ID="cboComputerRank" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr class="setLineHeight">
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbLanguage1" Text="Ngoại ngữ 1"></asp:Label>
                        </td>
                        <td class="control3">
                            <tlk:RadComboBox runat="server" ID="cboLanguage1" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbLanguage2" Text="Ngoại ngữ 2"></asp:Label>
                        </td>
                        <td class="control3">
                            <tlk:RadComboBox runat="server" ID="cboLanguage2" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbLanguage3" Text="Ngoại ngữ 3"></asp:Label>
                        </td>
                        <td class="control3">
                            <tlk:RadComboBox runat="server" ID="cboLanguage3" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr class="setLineHeight">
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbLanguageRank1" Text="Trình độ ngoại ngữ 1"></asp:Label>
                        </td>
                        <td class="control3">
                            <tlk:RadComboBox runat="server" ID="cboLanguageRank1" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbLanguageRank2" Text="Trình độ ngoại ngữ 2"></asp:Label>
                        </td>
                        <td class="control3">
                            <tlk:RadComboBox runat="server" ID="cboLanguageRank2" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbLanguageRank3" Text="Trình độ ngoại ngữ 3"></asp:Label>
                        </td>
                        <td class="control3">
                            <tlk:RadComboBox runat="server" ID="cboLanguageRank3" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr class="setLineHeight">
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbSoftSkill" Text="Kỹ năng mềm"></asp:Label>
                        </td>
                        <td class="control3">
                            <tlk:RadComboBox runat="server" ID="cboSoftSkill" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbCharacter" Text="Tính cách"></asp:Label>
                        </td>
                        <td class="control3">
                            <tlk:RadTextBox runat="server" ID="txtCharacter">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPageView>
        </tlk:RadMultiPage>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phPopupOrg" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock runat="server" ID="ScriptBlock">
    <script type="text/javascript">

        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
        }

        function clearSelectRadcombo(cbo) {
            if (cbo) {
                cbo.clearItems();
                cbo.clearSelection();
                cbo.set_text('');
            }
        }
        function clearSelectRadtextbox(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;

        }

        function OnClientClose(oWnd, args) {
            var arg = args.get_argument();
            if (arg == '1') {
                location.reload();
            }
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }

        function clientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "PRINT") {
                enableAjax = false;
            }
        }
    </script>
</tlk:RadScriptBlock>

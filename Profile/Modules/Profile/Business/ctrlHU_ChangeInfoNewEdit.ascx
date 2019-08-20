<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ChangeInfoNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_ChangeInfoNewEdit" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<%@ Import Namespace="Profile" %>
<asp:HiddenField ID="hidEmp" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<asp:HiddenField ID="hidOrg" runat="server" />
<asp:HiddenField ID="hidStaffRank" runat="server" />
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidManager" runat="server" />
<style type="text/css">
    .borderRight
    {
        border-right: 1px solid #C1C1C1;
    }
    
    .RadButton_Metro.rbSkinnedButton, .RadButton_Metro .rbDecorated
    {
        height: 22px;
    }
    
    @media screen and (-webkit-min-device-pixel-ratio:0)
    {
        .RadButton_Metro.rbSkinnedButton, .RadButton_Metro .rbDecorated
        {
            height: 21px;
        }
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Scrolling="None" Height="35px">
        <tlk:RadToolBar ID="tbarMassTransferSalary" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="item-head" colspan="8">
                    <%# Translate("Thông tin chung")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <asp:Label ID="lbEmployeeCode" runat="server" Text="Mã nhân viên"></asp:Label><span
                        class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" SkinID="Readonly" ReadOnly="true"
                        Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên %>" ToolTip="<%$ Translate: Bạn phải chọn nhân viên %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 130px">
                    <asp:Label ID="lbEmployeeName" runat="server" Text="Họ tên"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 130px">
                </td>
                <td>
                </td>
                <td class="lb" style="width: 130px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="item-head">
                    <%# Translate("Thông tin hiện tại")%>
                    <hr />
                </td>
                <td colspan="4" class="item-head">
                    <%# Translate("Thông tin điều chỉnh")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                </td>
                <td >
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbDecisionTypeOld" runat="server" Text="Loại quyết định"></asp:Label>
                </td>
                <td class="lb">
                    <tlk:RadTextBox ID="txtDecisionTypeOld" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDecisionold" runat="server" Text="Số quyết định"></asp:Label>
                </td>
                <td class="borderRight">
                    <tlk:RadTextBox ID="txtDecisionold" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDecisionType" runat="server" Text="Loại quyết định"></asp:Label><span
                        class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDecisionType" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusDecisionType" runat="server" ErrorMessage="<%# GetYouMustChoseMsg(UI.DecisionType) %>"
                        ToolTip="<%# GetYouMustChoseMsg(UI.DecisionType) %>" ClientValidationFunction="cusDecisionType"> 
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDecision" runat="server" Text="Số quyết định"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecision" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbEffectDateOld" runat="server" Text="Ngày hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDateOld" runat="server" SkinID="Readonly">
                        <DateInput Enabled="false">
                        </DateInput>
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbExpireDateOld" runat="server" Text="Ngày hết hiệu lực"></asp:Label>
                </td>
                <td class="borderRight">
                    <tlk:RadDatePicker ID="rdExpireDateOld" runat="server" SkinID="Readonly">
                        <DateInput Enabled="false">
                        </DateInput>
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbEffectDate" runat="server" Text="Ngày hiệu lực"></asp:Label><span
                        class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                        <DateInput CausesValidation="false">
                        </DateInput>
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" runat="server" ControlToValidate="rdEffectDate"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusExistEffectDate" runat="server" ErrorMessage="<%$ Translate: Ngày hiệu lực phải lớn hơn ngày hiệu lực gần nhất %>"
                        ToolTip="<%$ Translate: Ngày hiệu lực phải lớn hơn ngày hiệu lực gần nhất %>"> </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbExpireDate" runat="server" Text="Ngày hết hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdExpireDate"
                        Type="Date" ControlToCompare="rdEffectDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực %>"
                        ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực %>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbOrgNameOld" runat="server" Text="Đơn vị"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgNameOld" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbTitleNameOld" runat="server" Text="Chức danh"></asp:Label>
                </td>
                <td class="borderRight">
                    <tlk:RadTextBox ID="txtTitleNameOld" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <%--  <td class="lb">
                    <asp:Label runat="server" ID="lbOBJECT_ATTENDANCE_OLD" Text="Đối tượng chấm công"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="rtOBJECT_ATTENDANCE_OLD" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>--%>
                <%-- <td class="lb">
                    <asp:Label runat="server" ID="lbFILING_DATE_OLD" Text="Ngày nộp đơn"></asp:Label>
                </td>
                <td class="borderRight">
                    <tlk:RadDatePicker runat="server" ID="rdFILING_DATE_OLD" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadDatePicker>
                </td>--%>
                <%-- <td class="lb">
                    <asp:Label runat="server" ID="lbOBJECT_ATTENDANCE" Text="Đối tượng chấm công"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cbOBJECT_ATTENDANCE">
                    </tlk:RadComboBox>
                </td>--%>
                <%--  <td class="lb">
                    <asp:Label  runat ="server" ID="lbFILING_DATE" Text ="Ngày nộp đơn"></asp:Label>
                </td>--%>
                <%--  <td>
                    <tlk:RadDatePicker runat="server" ID="rdFILING_DATE">
                    </tlk:RadDatePicker>
                </td>--%>
                <td class="lb">
                    <asp:Label ID="lbOrgName" runat="server" Text="Đơn vị"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" Width="130px" SkinID="Readonly" ReadOnly="true" />
                    <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>" ToolTip="<%$ Translate: Bạn phải chọn đơn vị %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbTitle" runat="server" Text="Chức danh"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboTitle" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusTitle" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Chức danh %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Chức danh %>" ClientValidationFunction="cusTitle">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbStaffRankOld" runat="server" Text="Bậc nhân sự"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtStaffRankOld" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                      <asp:Label ID="lbObjectLaborOld" runat="server" Text="Đối tượng lao động"></asp:Label><span class="lbReq">*</span>
                </td>
                <td class="borderRight">
                    <tlk:RadTextBox ID="txtObjectLaborOld" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbStaffRank" runat="server" Text="Bậc nhân sự"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboStaffRank" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                       <asp:Label ID="lbObjectLaborNew" runat="server" Text="Đối tượng lao động"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboObjectLaborNew" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                       <asp:CustomValidator ID="cusObjectLaborNew" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Đối tượng lao động %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Đối tượng lao động %>" ClientValidationFunction="cusObjectLaborNew">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbManagerOld" runat="server" Text="Quản lý trực tiếp"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtManagerOld" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbFileAttach_Link" runat="server" Text="Tập tin đính kèm"></asp:Label>
                </td>
                <td class="borderRight">
                    <asp:Label ID="lbFileAttach" runat="server" Visible="false" />
                    <tlk:RadTextBox ID="txtFileAttach_Link" runat="server" ReadOnly="true" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtFileAttach_Link1" runat="server" ReadOnly="true" Visible="false"></tlk:RadTextBox>
                    <tlk:RadButton ID="btnDownloadOld" runat="server" Width="160px" Text="<%$ Translate: Tải xuống%>"
                        CausesValidation="false" OnClientClicked="rbtOldClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <asp:Label ID="lbManagerNew" runat="server" Text="Quản lý trực tiếp"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtManagerNew" ReadOnly="true" Width="130px" />
                    <tlk:RadButton runat="server" ID="btnFindDirect" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <asp:Label ID="lbUpload" runat="server" Text="Tập tin đính kèm"></asp:Label>
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
            <tr>
                <td colspan="4" class="borderRight">
                </td>
                <td>
                </td>
                <td colspan="3">
                    <asp:CheckBox ID="chkIsProcess" runat="server" Checked="true" Text="<%$ Translate: Có lưu dữ liệu sang Quá trình công tác %>" />
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="8">
                    <%# Translate("Thông tin phê duyệt")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbStatus" runat="server" Text="Trạng thái"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusStatus" runat="server" ErrorMessage="<%$Translate: Bạn phải chọn trạng thái %>"
                        ToolTip="<%$ Translate: Bạn phải chọn trạng thái %>" ClientValidationFunction="cusStatus"> </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalStatus" ControlToValidate="cboStatus" runat="server"
                        ErrorMessage="<%$ Translate: Trạng thái không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Trạng thái không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbSignDate" runat="server" Text="Ngày ký"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSignDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbSignName" runat="server" Text="Người ký"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignName" runat="server" SkinID="Readonly" ReadOnly="true"
                        Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindSign" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <asp:Label ID="lbSignTitle" runat="server" Text="Chức danh"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignTitle" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbRemark" runat="server" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="7" style="padding-bottom: 8px">
                    <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" Width="100%" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr style="visibility: hidden">
                <td class="lb">
                    <tlk:RadTextBox ID="txtRemindLink" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

//        $(document).ready(function () {
//            registerOnfocusOut('RAD_SPLITTER_PANE_TR_ctl00_MainContent_ctrlHU_ChangeInfoNewEdit_LeftPane');
//        });

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function cusDecisionType(oSrc, args) {
            var cbo = $find("<%# cboDecisionType.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusTitle(oSrc, args) {
            var cbo = $find("<%# cboTitle.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
          function cusObjectLaborNew(oSrc, args) {
            var cbo = $find("<%# cboObjectLaborNew.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusStatus(oSrc, args) {
            var cbo = $find("<%# cboStatus.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }


        function cusStaffRank(oSrc, args) {
            var cbo = $find("<%# cboStaffRank.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

       <%-- function cusSalLevel(oSrc, args) {
            var cbo = $find("<%# cboSalLevel.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusSalRank(oSrc, args) {
            var cbo = $find("<%# cboSalRank.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }--%>

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

        function OnDateSelected(sender, e) {
            var datePicker = $find("<%= rdEffectDate.ClientID %>");
            var date = datePicker.get_selectedDate();
        }

        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                <%--   case '<%= cboSalGroup.ClientID %>':
                    cbo = $find('<%= cboSalLevel.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboSalRank.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= rntxtSalBasic.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    break;
                   
                case '<%= cboSalLevel.ClientID %>':
                    cbo = $find('<%= cboSalRank.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= rntxtSalBasic.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    break;
                case '<%= cboSalRank.ClientID %>':
                    cbo = $find('<%= rntxtSalBasic.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    var item = eventArgs.get_item();
                    if (item) {
                        cbo.set_value(item.get_attributes().getAttribute("SALARY_BASIC"));
                    }
                    break;
         --%>
               <%-- case '<%= cboTitle.ClientID %>':
                    cbo = $find('<%= txtTitleGroup.ClientID %>');
                    clearSelectRadtextbox(cbo);
                    var item = eventArgs.get_item();
                    if (item) {
                        cbo.set_value(item.get_attributes().getAttribute("GROUP_NAME"));
                    }
                    break; --%>
                default:
                    break;
            }
        }

        function clearSelectRadcombo(cbo) {
            if (cbo) {
                cbo.clearItems();
                cbo.clearSelection();
                cbo.set_text('');
            }
        }
        function rbtOldClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function clearSelectRadnumeric(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

        function clearSelectRadtextbox(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

        function getGridAllowInfo(gridClientId) {
            var allowanceIns = 0;
            var allowanceTotal = 0;
            var rgAllowItems = $find(gridClientId).get_masterTableView().get_dataItems();
            for (var i = 0; i < rgAllowItems.length; i++) {
                var allowAmount = parseFloat(dataItems[i].getDataKeyValue("AMOUNT"));
                if (dataItems[i].getDataKeyValue("IS_INSURRANCE") === "True") {
                    allowanceIns += allowAmount;
                }
                allowanceTotal += allowAmount;
            }
            return { allowanceIns: allowanceIns, allowanceTotal: allowanceTotal };
        }

        function toggleControls(clientIds, status) {
            $.each(clientIds, function (index, value) {
                toggleControl(value);
            });
        }
        function toggleControl(id, status) {
            var control = $find(id);
            if (control) {
                if (status) {
                    control.enable();
                } else {
                    control.disable();
                }
            }
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
        }

        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;

            if (!value) {
                value = null;
            }
            var context = eventArgs.get_context();
            context["valueCustom"] = value;

        }

    </script>
</tlk:RadCodeBlock>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_TitleConcurrent.ascx.vb"
    Inherits="Profile.ctrlHU_TitleConcurrent" %>
<asp:PlaceHolder ID="phPopupOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopupDirect" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopupLevel" runat="server"></asp:PlaceHolder>
<tlk:RadToolBar ID="tbarMain" runat="server" ValidationGroup="TitleConcurrent" />
<asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="TitleConcurrent" />
<asp:HiddenField runat="server" ID="hidReload" />
<table class="table-form">
    <tr>
        <td class="lb">
            <%# Translate("Đơn vị")%><span class="lbReq">*</span>
        </td>
        <td>
            <asp:HiddenField ID="hidOrgID" runat="server" />
            <tlk:RadTextBox runat="server" ID="txtOrgName2" ReadOnly="true" />
            <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
            <asp:RequiredFieldValidator ValidationGroup="TitleConcurrent" ID="RequiredFieldValidator1"
                ControlToValidate="txtOrgName2" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>"
                ToolTip="<%$ Translate: Bạn phải chọn đơn vị %>">
            </asp:RequiredFieldValidator>
        </td>
        <td class="lb" style="width: 150px">
            <%# Translate("Chức danh")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadComboBox runat="server" ID="cboTitle" SkinID="LoadDemand" OnClientItemsRequesting="OnClientItemsRequesting">
            </tlk:RadComboBox>
            <asp:CustomValidator ValidationGroup="TitleConcurrent" ID="cusTitle" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Chức danh %>"
                ToolTip="<%$ Translate: Bạn phải chọn Chức danh  %>" ClientValidationFunction="cusTitle">
            </asp:CustomValidator>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdEffectDate" runat="server">
            </tlk:RadDatePicker>
            <asp:RequiredFieldValidator ID="reqEffectDate" runat="server" ControlToValidate="rdEffectDate"
                ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"
                ValidationGroup="TitleConcurrent"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="cusExistEffectDate" runat="server" ErrorMessage="<%$ Translate: Ngày hiệu lực phải lớn hơn ngày hiệu lực gần nhất %>"
                ToolTip="<%$ Translate: Ngày hiệu lực phải lớn hơn ngày hiệu lực gần nhất %>"
                ValidationGroup="TitleConcurrent">
            </asp:CustomValidator>
        </td>
        <td class="lb">
            <%# Translate("Ngày hết hiệu lực")%>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdExpireDate" runat="server">
            </tlk:RadDatePicker>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdExpireDate"
                Type="Date" ControlToCompare="rdEffectDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực %>"
                ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực %>" ValidationGroup="TitleConcurrent"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ghi chú")%>
        </td>
        <td colspan="3">
            <tlk:RadTextBox ID="txtNote" runat="server" Width="100%">
            </tlk:RadTextBox>
        </td>
    </tr>
</table>
<tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="False"
    AllowPaging="True" Height="250px" AllowSorting="True" AllowMultiRowSelection="true"
    Width="99.8%">
    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="True">
        <Selecting AllowRowSelect="true" />
    </ClientSettings>
    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="NAME,EFFECT_DATE,EXPIRE_DATE,NOTE,ORG_ID,ORG_NAME,TITLE_ID">
        <Columns>
            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
            </tlk:GridClientSelectColumn>
            <tlk:GridBoundColumn DataField="ID" Visible="false" />
            <tlk:GridBoundColumn DataField="ORG_ID" Visible="false" />
            <tlk:GridBoundColumn DataField="ORG_NAME" Visible="false" />
            <tlk:GridBoundColumn DataField="TITLE_ID" Visible="false" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="NAME" UniqueName="NAME"
                SortExpression="NAME" />
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" />
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE" />
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" UniqueName="NOTE"
                SortExpression="NOTE" />
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        var oldSize = 0;

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function ResizeSplitter() {
        }

        function cusTitle(oSrc, args) {
            var cbo = $find("<%# cboTitle.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {
                case '<%= cboTitle.ClientID %>':
                    value = $get("<%= hidOrgID.ClientID %>").value;
                    break;
                default:
                    break;
            }

            if (!value) {
                value = 0;
            }
            var context = eventArgs.get_context();
            context["valueCustom"] = value;
            context["value"] = sender.get_value();

        }

    </script>
</tlk:RadCodeBlock>

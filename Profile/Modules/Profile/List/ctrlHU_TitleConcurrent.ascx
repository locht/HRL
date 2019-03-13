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
        <%--    <td class="lb">
            <%# Translate("Chức danh-Công ty")%><span class="lbReq">*</span>
        </td>
        <td colspan="3">
            <tlk:RadTextBox ID="txtTitle" runat="server" Width="100%">
            </tlk:RadTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                ErrorMessage="<%$ Translate: Bạn phải nhập Chức danh %>" ToolTip="<%$ Translate: Bạn phải nhập Chức danh %>"
                ValidationGroup="TitleConcurrent"></asp:RequiredFieldValidator>
        </td>--%>
        <td class="lb3">
            <%# Translate("Đơn vị")%><span class="lbReq">*</span>
        </td>
        <td class="control3">
            <asp:HiddenField ID="hidOrgID" runat="server" />
            <tlk:RadTextBox runat="server" ID="txtOrgName2" ReadOnly="true" />
            <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator1"
                ControlToValidate="txtOrgName2" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>"
                ToolTip="<%$ Translate: Bạn phải chọn đơn vị %>">
            </asp:RequiredFieldValidator>
        </td>
        <td class="lb3">
            <%# Translate("Chức danh")%><span class="lbReq">*</span>
        </td>
        <td class="control3">
            <tlk:RadComboBox runat="server" ID="cboTitle" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                OnClientItemsRequesting="OnClientItemsRequesting">
            </tlk:RadComboBox>
            <asp:CustomValidator ValidationGroup="EmpProfile" ID="cusTitle" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Chức danh %>"
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
    <ClientSettings EnableRowHoverStyle="true">
        <Selecting AllowRowSelect="true" />
    </ClientSettings>
    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="NAME,EFFECT_DATE,EXPIRE_DATE,NOTE">
        <Columns>
            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
            </tlk:GridClientSelectColumn>
            <tlk:GridBoundColumn DataField="ID" Visible="false" />
            <tlk:GridBoundColumn DataField="ORG_ID" Visible="false" />
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

    </script>
</tlk:RadCodeBlock>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlLocation.ascx.vb"
    Inherits="Profile.ctrlLocation" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI" %>
<asp:PlaceHolder ID="phPopupOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopupDirect" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopupLevel" runat="server"></asp:PlaceHolder>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="PaneToolBar" runat="server" Scrolling="None" Height="33px">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="PaneMain" runat="server" Scrolling="None" Height="330px">
        <asp:HiddenField ID="hfID" runat="server" />
        <asp:HiddenField ID="hfLawAgent" runat="server" />
        <asp:HiddenField ID="hfSignUpAgent" runat="server" />
        <asp:HiddenField ID="hfOrg" runat="server" />
        <asp:HiddenField ID="hfBank" runat="server" />
        <asp:HiddenField ID="hfBank_Branch" runat="server" />
        <div style="width: 100%; height: 93%; overflow: auto;">
            <table class="table-form">
                <tr>
                    <td colspan="8">
                        <asp:ValidationSummary ID="valSum" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 100px">
                        <%# Translate("CTRLLOCATION_LBLLOCATIONCODE")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" SkinID="readonly" ReadOnly="true" ID="txtLocationCODE" />
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 100px">
                        <%# Translate("CTRLLOCATION_LBLLOCATIONVN")%><span class="lbReq">*</span>
                    </td>
                    <td colspan="3">
                        <tlk:RadTextBox runat="server" ID="txtLocationVN" Width="92%" ReadOnly="true" />
                        <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                        <asp:RequiredFieldValidator ID="reqSDTC" ControlToValidate="txtLocationVN" runat="server"
                            ErrorMessage="<%$ Translate: Bạn phải chọn sơ đồ tổ chức. %>" ToolTip="<%$ Translate: Bạn phải chọn sơ đồ tổ chức. %>"> </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb" style="width: 100px">
                        <%# Translate("CTRLLOCATION_LBLLOCATIONEN")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtLocationEn" />
                    </td>
                    <td class="lb" style="width: 100px">
                        <%# Translate("CTRLLOCATION_LBLLOCATIONSHORT")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtLocationShort" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLADDRESS")%><span class="lbReq">*</span>
                    </td>
                    <td colspan="3">
                        <tlk:RadTextBox runat="server" ID="txtAddress" Width="98%" />
                        <asp:RequiredFieldValidator ID="reqAddress" ControlToValidate="txtAddress" runat="server"
                            ErrorMessage="<%$ Translate: Bạn phải nhập Địa chỉ theo GPKD. %>" ToolTip="<%$ Translate: Bạn phải nhập Địa chỉ theo GPKD. %>"> </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb" style="width: 100px">
                        <%# Translate("CTRLLOCATION_LBLPHONE")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtPhone" />
                    </td>
                    <td class="lb" style="width: 100px">
                        <%# Translate("CTRLLOCATION_LBLFAX")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtFax" />
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 100px">
                        <%# Translate("CTRLLOCATION_LBLADDRESSEMP")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtAddress_Emp" />
                    </td>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLOFFICEPLACE")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtOfficePlace" />
                    </td>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLWEBSITE")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtWebsite" />
                    </td>
                    
                    <td class="lb" style="width: 100px">
                        <%# Translate("CTRLLOCATION_ISSIGNCONTRACT")%>
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="ckIsSignContract" />
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_PROVINCE")%>
                    </td>
                    <td>
                        <tlk:RadComboBox runat="server" SkinID="LoadDemand" ID="cboProvince" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                            OnClientItemsRequesting="OnClientItemsRequesting" AutoPostBack="true" CausesValidation="false">
                        </tlk:RadComboBox>
                    </td>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_DISTRICT")%>
                    </td>
                    <td>
                        <tlk:RadComboBox runat="server" SkinID="LoadDemand" ID="cboDistrict" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                            OnClientItemsRequesting="OnClientItemsRequesting" AutoPostBack="true" CausesValidation="false">
                        </tlk:RadComboBox>
                    </td>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_WARD")%>
                    </td>
                    <td>
                        <tlk:RadComboBox runat="server" SkinID="LoadDemand" ID="cboWard" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                            OnClientItemsRequesting="OnClientItemsRequesting" CausesValidation="false">
                        </tlk:RadComboBox>
                    </td>
                    <td class="lb">
                        <asp:Label runat="server" ID="lbUploadFile_LG" Text="<%$ Translate: CTRLLOCATION_LBLOGO %>"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtUpload_LG" ReadOnly="true" runat="server">
                        </tlk:RadTextBox>
                        <tlk:RadButton runat="server" ID="btnUploadFile_LG" SkinID="ButtonView" CausesValidation="false"
                            TabIndex="3" />
                        <tlk:RadButton ID="btnDownload_LG" runat="server" Text="Tải xuống" CausesValidation="false"
                            OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                        </tlk:RadButton>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLACCOUNTNUMBER")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtAccountNumber" CausesValidation="false" />
                        <asp:CustomValidator ID="CustomValidator1" ControlToValidate="txtAccountNumber" runat="server"
                            ErrorMessage="<%$ Translate: Số tài khoản đã tồn tại %>" ToolTip="<%$ Translate: Số tài khoản đã tồn tại %>"> </asp:CustomValidator>
                    </td>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLBANKID")%>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cbBank" runat="server" AutoPostBack="true" CausesValidation="false">
                        </tlk:RadComboBox>
                    </td>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLBANK_BRANCH_ID")%>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboRank_Banch" runat="server" CausesValidation="false">
                        </tlk:RadComboBox>
                    </td>
                    <td class="lb">
                        <asp:Label runat="server" ID="lbUploadFile_HD" Text="<%$ Translate: CTRLLOCATION_LBHD %>"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtUpload_HD" ReadOnly="true" runat="server">
                        </tlk:RadTextBox>
                        <tlk:RadButton runat="server" ID="btnUploadFile_HD" SkinID="ButtonView" CausesValidation="false"
                            TabIndex="3" />
                        <tlk:RadButton ID="btnDownload_HD" runat="server" Text="Tải xuống" CausesValidation="false"
                            OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                        </tlk:RadButton>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLTAXCODE")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtTaxCode" />
                    </td>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLTAXDATE")%>
                    </td>
                    <td>
                        <tlk:RadDatePicker ID="rdpTaxDate" runat="server">
                        </tlk:RadDatePicker>
                    </td>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLTAXPLACE")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtTaxPlace" />
                    </td>
                    
                    <td class="lb">
                        <asp:Label runat="server" ID="lbUploadFile_FT" Text="<%$ Translate: CTRLLOCATION_LBFT %>"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtUpload_FT" ReadOnly="true" runat="server">
                        </tlk:RadTextBox>
                        <tlk:RadButton runat="server" ID="btnUploadFile_FT" SkinID="ButtonView" CausesValidation="false"
                            TabIndex="3" />
                        <tlk:RadButton ID="btnDownload_FT" runat="server" Text="Tải xuống" CausesValidation="false"
                            OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                        </tlk:RadButton>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLLAWAGENT")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" SkinID="readonly" ID="txtLawAgentId" ReadOnly="true" />
                        <tlk:RadButton runat="server" ID="btnLawAgentId" SkinID="ButtonView" CausesValidation="false" />
                    </td>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLLAWAGENTTITLE")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" SkinID="readonly" ID="txtLawAgentTitle" ReadOnly="true" />
                    </td>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLLAWAGENTNATIONALITY")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" SkinID="readonly" ID="txtLawAgentNationality" ReadOnly="true" />
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLSIGNUPAGENT")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" SkinID="readonly" ID="txtSignupAgent" ReadOnly="true" />
                        <tlk:RadButton runat="server" ID="btnSignupAgent" SkinID="ButtonView" CausesValidation="false" />
                    </td>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLSIGNUPAGENTTITLE")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" SkinID="readonly" ID="txtSignupAgentTitle" ReadOnly="true" />
                    </td>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLSIGNUPAGENTNATIONALITY")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" SkinID="readonly" ID="txtSigupAgentNationality" ReadOnly="true" />
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLNUMBERBUSINESS")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtNumberBusiness" />
                    </td>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLREGISTERDATE")%>
                    </td>
                    <td>
                        <tlk:RadDatePicker ID="rdRegisterDate" runat="server">
                        </tlk:RadDatePicker>
                    </td>
                    <td class="lb" style="visibility:hidden">
                        <%# Translate("CTRLLOCATION_LBLNAMEBUSINESS")%>
                    </td>
                    <td style="visibility:hidden">
                        <tlk:RadTextBox runat="server" ID="txtNameBusiness" />
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("CTRLLOCATION_LBLNOTE")%>
                    </td>
                    <td colspan="7">
                        <tlk:RadTextBox runat="server" ID="txtNote" Width="100%" TextMode="MultiLine" />
                    </td>
                </tr>
            </table>
        </div>
    </tlk:RadPane>
    <tlk:RadPane ID="PaneGrid" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgLocation" runat="server" AllowFilteringByColumn="True" AllowMultiRowSelection="True"
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellSpacing="0"
            GridLines="None" Height="100%" PageSize="20" ShowStatusBar="True">
            <ClientSettings AllowColumnsReorder="True" EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling UseStaticHeaders="true" AllowScroll="True" />
                <Resizing AllowColumnResize="true" />
            </ClientSettings>
            <MasterTableView CommandItemDisplay="None" EditMode="InPlace" DataKeyNames="ID,LOCATION_VN_NAME,LOCATION_EN_NAME,ADDRESS,PHONE, ACTFLG">
                <CommandItemSettings ExportToPdfText="Export to PDF" />
                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                </RowIndicatorColumn>
                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                </ExpandCollapseColumn>
                <Columns>
                    <tlk:GridBoundColumn DataField="ID" FilterControlAltText="Filter ID column" HeaderText="<%$ Translate: CTRLLOCATION_LBLID %>"
                        ReadOnly="true" SortExpression="ID" UniqueName="ID" Visible="False">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="LOCATION_VN_NAME" FilterControlAltText="Filter NAME_VN column"
                        HeaderText="<%$ Translate: CTRLLOCATION_LBLLOCATIONVI %>" ReadOnly="true" SortExpression="NAME_VN"
                        UniqueName="NAME_VN">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="LOCATION_EN_NAME" FilterControlAltText="Filter NAME_EN column"
                        HeaderText="<%$ Translate: CTRLLOCATION_LBLLOCATIONEN %>" ReadOnly="true" SortExpression="NAME_EN"
                        UniqueName="NAME_EN">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ADDRESS" FilterControlAltText="Filter ADDRESS column"
                        HeaderText="<%$ Translate: CTRLLOCATION_LBLADDRESS %>" ReadOnly="true" SortExpression="ADDRESS"
                        UniqueName="ADDRESS">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="PHONE" FilterControlAltText="Filter PHONE column"
                        HeaderText="<%$ Translate: CTRLLOCATION_LBLPHONE %>" ReadOnly="true" SortExpression="PHONE"
                        UniqueName="PHONE">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ACTFLG" FilterControlAltText="Filter ACTFLG column"
                        HeaderText="<%$ Translate: Trạng thái %>" ReadOnly="true" SortExpression="ACTFLG"
                        HeaderStyle-Width="70px" UniqueName="ACTFLG">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                </Columns>
                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
            <FilterMenu EnableImageSprites="False">
            </FilterMenu>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindEmployee_Contract" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindOrganization" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
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

        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {
                case '<%= cboDistrict.ClientID %>':
                    cbo = $find('<%= cboProvince.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboWard.ClientID %>':
                    cbo = $find('<%= cboDistrict.ClientID %>');
                    value = cbo.get_value();
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

        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                case '<%= cboProvince.ClientID %>':
                    cbo = $find('<%= cboDistrict.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboWard.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
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

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />

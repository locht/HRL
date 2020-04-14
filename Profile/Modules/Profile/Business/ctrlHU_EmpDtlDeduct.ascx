<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlDeduct.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlDeduct" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="hidFamilyID" runat="server" />
<asp:HiddenField ID="hidEmployeeid" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal"
    SkinID="Demo">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RightPane" runat="server" Height="200px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                 <td class="lb" style="width: 130px">
                    <asp:Label ID="lbFullName" runat="server" Text="Họ tên"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtFullName">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqFullname" ControlToValidate="txtFullName" runat="server"
                        ErrorMessage="Bạn phải nhập họ tên" ToolTip="Bạn phải nhập họ tên">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 150px">
                    <asp:Label ID="lbRelationship" runat="server" Text="Mối quan hệ"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboRelationship">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboRelationship"
                        runat="server" ErrorMessage="Bạn phải chọn Mối quan hệ" ToolTip="Bạn phải chọn Mối quan hệ">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalRelationship" Enabled="false" runat="server" ErrorMessage="Bạn phải chọn Mối quan hệ"
                        ToolTip="Bạn phải chọn Mối quan hệ">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbBirthDate" runat="server" Text="Ngày sinh"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdBirthDate">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdBirthDate"
                        runat="server" ErrorMessage="Bạn phải nhập Ngày sinh" ToolTip="Bạn phải nhập Ngày sinh">
                    </asp:RequiredFieldValidator>
                </td> 
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbDeductReg" runat="server" Text="Ngày bắt đầu giảm trừ thuế TNCN"></asp:Label> <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDeductReg">
                    </tlk:RadDatePicker>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdDeductReg"
                        runat="server" ErrorMessage="Bạn phải nhậpNgày bắt đầu giảm trừ thuế TNCN" ToolTip="Bạn phải nhập Ngày bắt đầu giảm trừ thuế TNCN">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDeductFrom" runat="server" Text="Ngày bắt đầu giảm trừ"></asp:Label> <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDeductFrom">
                    </tlk:RadDatePicker>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rdDeductFrom"
                        runat="server" ErrorMessage="Bạn phải nhập Ngày bắt đầu giảm trừ" ToolTip="Bạn phải nhập Ngày bắt đầu giảm trừ">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDeductTo" runat="server" Text="Ngày kết thúc giảm trừ"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDeductTo">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbAdresss" runat="server" Text="Địa chỉ"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtAdress" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label6" Text="Tỉnh/TP"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboProvince_City1" SkinID="LoadDemand" runat="server" Width="160px"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label5" Text="Quận/Huyện"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDistrict1" SkinID="LoadDemand" runat="server" Width="160px"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbBangCap" Text="Phường/Xã"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCommune1" SkinID="LoadDemand" runat="server" Width="160px"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>          
            </tr>
             <tr>
                 <td class="lb">
                    <%# Translate("Giới tính")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboGender" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboGender"
                        runat="server" ErrorMessage="Bạn phải chọn giới tính" ToolTip="Bạn phải chọn giới tính">
                    </asp:RequiredFieldValidator>
                </td>
                 <td class="lb">
                    <asp:Label ID="lbIDNO" runat="server" Text="Số CMND"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtIDNO" SkinID="Textbox15">
                    </tlk:RadTextBox>
                </td>
                 <td class="lb">
                    <%# Translate("Ngày cấp CMND")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdIDDate">
                    </tlk:RadDatePicker>
                </td>
             </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nơi cấp CMND")%>
                </td>
                <td>
                     <tlk:RadComboBox runat="server" ID="cboIDPlace" SkinID="LoadDemand" Width="160px"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbTax" runat="server" Text="Mã số thuế"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTax">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày cấp MST")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdMSTDate">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nơi cấp mã số thuế")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txt_MSTPLACE">
                    </tlk:RadTextBox>
                </td>
                 <td class="lb">
                    <%# Translate("Giấy khai sinh")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtBIRTH_CODE">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Quyển")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtQuyen">
                    </tlk:RadTextBox>
                </td>
            </tr>
             <tr> 
                <td class="lb">
                    <asp:Label ID="Label4" runat="server" Text="Địa chỉ nơi đăng ký giấy khai sinh"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtHamlet1" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Quốc tịch")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboNATIONALITYFAMILY" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting" AutoPostBack="true">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Tỉnh/Thành phố")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cbTempKtPROVINCE_ID" SkinID="LoadDemand" Width="160px"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Quận/Huyện")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cbTempKtDISTRICT_ID" SkinID="LoadDemand" Width="160px"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Xã/Phường")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cbTempKtWARD_ID" SkinID="LoadDemand" Width="160px"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                 <td class="lb">
                    <asp:Label ID="lbDieDate" runat="server" Text="Ngày mất"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDieDate">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbRemark" runat="server" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtRemark" SkinID="Textbox1023" Width="100%" Height="35px" />
                </td>
            </tr>  
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgDeduct" runat="server" AllowMultiRowSelection="true"
            Height="100%">
            <MasterTableView DataKeyNames="ID,ID_NO,FULLNAME,RELATION_ID,DEDUCT_FROM,DEDUCT_TO,ADDRESS,BIRTH_DATE,REMARK,TAXTATION,PROVINCE_ID,DISTRICT_ID,WARD_ID,
            BIRTH_CODE,QUYEN,BIRTH_NATION_ID,BIRTH_PROVINCE_ID,BIRTH_DISTRICT_ID,BIRTH_WARD_ID,DIE_DATE,DEDUCT_REG,ID_NO_DATE,TAXTATION_DATE,TAXTATION_PLACE,GENDER,ID_NO_PLACE,BIRTH_ADDRESS"
                ClientDataKeyNames="ID,ID_NO,FULLNAME,RELATION_ID,DEDUCT_FROM,DEDUCT_TO,ADDRESS,BIRTH_DATE,REMARK,TAXTATION,PROVINCE_ID,DISTRICT_ID,WARD_ID,
            BIRTH_CODE,QUYEN,BIRTH_NATION_ID,BIRTH_PROVINCE_ID,BIRTH_DISTRICT_ID,BIRTH_WARD_ID,DIE_DATE,DEDUCT_REG,ID_NO_DATE,TAXTATION_DATE,TAXTATION_PLACE,GENDER,ID_NO_PLACE,BIRTH_ADDRESS">
                <NoRecordsTemplate>
                    Không có bản ghi nào
                </NoRecordsTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn HeaderStyle-Width="40px">
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="FULLNAME" HeaderText="<%$ Translate: Họ và tên%>"
                        UniqueName="FULLNAME" Visible="True">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn UniqueName="BIRTH_DATE" HeaderText="<%$ Translate: Ngày sinh%>"
                        ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="BIRTH_DATE">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn UniqueName="DIE_DATE" HeaderText="<%$ Translate: Ngày mất%>"
                        ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="DIE_DATE">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn DataField="RELATION_NAME" HeaderText="<%$ Translate: Quan hệ %>"
                        UniqueName="RELATION_NAME" Visible="True">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="GENDER_NAME" HeaderText="<%$ Translate: Giới tính %>"
                        UniqueName="GENDER_NAME" Visible="True">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ID_NO" HeaderText="<%$ Translate: CMND %>" UniqueName="ID_NO"
                        Visible="True" EmptyDataText="">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    </tlk:GridBoundColumn>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="FindIsEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlDeduct_RadSplitter2';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlDeduct_RadPane3';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlDeduct_RadPane4';
        var pane3ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlDeduct_RadPane2';
        var pane4ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlDeduct_RadPane1';
        var validateID = 'MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlDeduct_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;

        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter();
                else
                    ResizeSplitterDefault();
            }
            else if (item.get_commandName() == "NEW") {
                enableAjax = false;
            }
            else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }

        function clRadDatePicker() {
            $('#ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlDeduct_rdBirthDate_dateInput').val('');
            $('#ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlDeduct_rdDeductReg_dateInput').val('');
            $('#ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlDeduct_rdDeductFrom_dateInput').val('');
            $('#ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlDeduct_rdDeductTo_dateInput').val('');
        }
        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter2.ClientID%>");
                var pane = splitter.getPaneById('<%= RightPane.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter2.ClientID%>");
            var pane = splitter.getPaneById('<%= RightPane.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane4.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                case '<%= cboProvince_City1.ClientID %>':
                    cbo = $find('<%= cboDistrict1.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboCommune1.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboDistrict1.ClientID %>':
                    cbo = $find('<%= cboCommune1.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;

                case '<%= cbTempKtPROVINCE_ID.ClientID%>':
                    cbo = $find('<%= cbTempKtDISTRICT_ID.ClientID%>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cbTempKtWARD_ID.ClientID%>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cbTempKtDISTRICT_ID.ClientID%>':
                    cbo = $find('<%= cbTempKtWARD_ID.ClientID%>');
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
        function clearSelectRadtextbox(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }
        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {

                case '<%= cboDistrict1.ClientID %>':
                    cbo = $find('<%= cboProvince_City1.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboCommune1.ClientID %>':
                    cbo = $find('<%= cboDistrict1.ClientID %>');
                    value = cbo.get_value();
                    break;

                case '<%= cbTempKtDISTRICT_ID.ClientID%>':
                    cbo = $find('<%= cbTempKtPROVINCE_ID.ClientID%>');
                    value = cbo.get_value();
                    break;
                case '<%= cbTempKtWARD_ID.ClientID%>':
                    cbo = $find('<%= cbTempKtDISTRICT_ID.ClientID%>');
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

    </script>
</tlk:RadScriptBlock>

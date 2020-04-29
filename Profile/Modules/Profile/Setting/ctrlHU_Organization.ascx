<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Organization.ascx.vb"
    Inherits="Profile.ctrlHU_Organization" %>
<style type="text/css">
    /*#btnSaveImage
    {
        display: none;
    }*/
    
    .RadUpload .ruFakeInput
    {
        display: none;
    }
    
    .RadUpload .ruBrowse
    {
        width: 120px !important;
        _width: 120px !important;
        width: 120px;
        _width: 120px;
        background-position: 0 -46px !important;
    }
    
    .hide
    {
        display: none !important;
    }
    
    .btnChooseImage
    {
        margin-left: -5px;
    }
    
    .ruInputs
    {
        width: 0px;
        text-align: center;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="400px">
        <tlk:RadTreeView ID="treeOrgFunction" runat="server" CausesValidation="False" Height="93%">
        </tlk:RadTreeView>
        <asp:CheckBox ID="cbDissolve" runat="server" Text="<%$ Translate: Hiển thị các đơn vị giải thể %>"
            AutoPostBack="True" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidParentID" runat="server" />
        <asp:HiddenField ID="hidListDistrict" runat="server" />
        <asp:HiddenField ID="hidListbankBrach" runat="server" />
        <%--<asp:HiddenField ID="hidRepresentative" runat="server" />--%>
        <tlk:RadToolBar ID="tbarOrgFunctions" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <asp:Label ID="lbParent_Name" runat="server" Text="Đơn vị cha"></asp:Label><span
                        class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtParent_Name" runat="server" ReadOnly="true" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbNameVN" runat="server" Text="Tên đơn vị (VN)"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNameVN" runat="server" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqNameVN" ControlToValidate="txtNameVN" runat="server"
                        ErrorMessage="Bạn phải nhập Tên đơn vị" ToolTip="Bạn phải nhập Tên đơn vị"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbNameEN" runat="server" Text="Tên đơn vị (EN)"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNameEN" runat="server" Width="100%">
                    </tlk:RadTextBox>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtNameVN"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Tên đơn vị %>" ToolTip="<%$ Translate: Bạn phải nhập Tên đơn vị %>"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <%-- <tr>
                <td class="lb">
                   <asp:Label ID="lbRepresentativeName" runat="server" Text="Quản lý đơn vị"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:radtextbox id="txtRepresentativeName" runat="server" width="130px" readonly="True">
                    </tlk:radtextbox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtRepresentativeName" runat="server"
                        ErrorMessage="Bạn phải chọn Quản lý đơn vị" ToolTip="Bạn phải chọn Quản lý đơn vị"></asp:RequiredFieldValidator>
                    <tlk:radbutton id="btnFindRepresentative" skinid="ButtonView" runat="server" causesvalidation="false"
                        width="40px">
                    </tlk:radbutton>
                </td>
                <td class="lb">
                   <asp:Label ID="lbUNIT_LEVEL" runat="server" Text="Bậc đơn vị"></asp:Label>
                </td>
                <td colspan ="2">
                    <tlk:RadComboBox  id="cbUNIT_LEVEL" runat="server" width="100%">
                    </tlk:RadComboBox>
                </td>
            </tr>--%>
            <tr>
                <td colspan="4">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã đơn vị")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server" SkinID="Textbox50">
                    </tlk:RadTextBox>
                    <%-- <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mã đơn vị %>" ToolTip="<%$ Translate: Bạn phải nhập Mã đơn vị %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Tên viết tắt")%>
                    <%--<asp:Label ID="lbOrg_level" runat="server" Text="Cấp phòng ban"></asp:Label>
                    <span class="lbReq">*</span>--%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtSHORT_NAME" runat="server" SkinID="Textbox50" />
                    <%--<tlk:radcombobox id="cboOrg_level" runat="server">
                    </tlk:radcombobox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboOrg_level"
                        runat="server" ErrorMessage="Bạn phải nhập cấp phòng ban" ToolTip="Bạn phải nhập cấp phòng ban">
                    </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đơn vị kí hợp đồng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkIsSignContract" />
                </td>
                <td class="lb">
                    <%# Translate("Mã hợp đồng")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtCONTRACT_CODE" runat="server" SkinID="Textbox50" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trực thuộc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboUnder" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="cboUnder"
                        runat="server" ErrorMessage="Bạn phải chọn thông tin trực thuộc" ToolTip="Bạn phải chọn thông tin trực thuộc">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhóm chịu phí")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboGROUP_PAID_ID" runat="server" />
                </td>
                <td class="lb">
                    <%# Translate("Cấp đơn vị")%>
                </td>
                <td colspan="3">
                    <tlk:RadComboBox ID="cboUNIT_RANK_ID" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Vùng bảo hiểm")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboRegion" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqRegion" ControlToValidate="cboRegion" runat="server"
                        ErrorMessage="Vui lòng chọn vùng lương" ToolTip="Vui lòng chọn vùng lương">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Mã chi phí không được trùng"
                        ToolTip="Mã chi phí không được trùng">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbU_insurance" runat="server" Text="Đơn vị đóng bảo hiểm"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadComboBox ID="cboU_insurance" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cboU_insurance"
                        runat="server" ErrorMessage="Bạn phải nhập đơn vị đóng bảo hiểm" ToolTip="Bạn phải nhập đơn vị đóng bảo hiểm">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <%--<tr>
                <td class="lb">
                   <asp:Label ID="lbNUMBER_BUSINESS" runat="server" Text="Giấy phép ĐKKD (Mã số thuế)" Visible="false"></asp:Label>
                </td>
                <td>
                    
                </td>
                <td class="lb">
                  <asp:Label ID="lbDATE_BUSINESS" runat="server" Text="Ngày cấp giấy phép ĐKKD" Visible="false"></asp:Label>
                   
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdDATE_BUSINESS" runat="server" Visible="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>--%>
            <tr>
                <td class="lb">
                    <%# Translate("Tỉnh/Thành")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPROVINCE_ID" runat="server" OnClientSelectedIndexChanged="OnClientSelectedIndexChangedPROVINCE_ID" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboPROVINCE_ID"
                        runat="server" ErrorMessage="Bạn phải chọn Tỉnh/Thành" ToolTip="Bạn phải chọn Tỉnh/Thành">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Quận/Huyện")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadComboBox ID="cboDISTRICT_ID" runat="server" EnableLoadOnDemand="true" OnClientItemsRequested="OnClientItemsRequesting" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboDISTRICT_ID"
                        runat="server" ErrorMessage="Bạn phải chọn Quận/Huyện" ToolTip="Bạn phải chọn Quận/Huyện" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbADDRESS" runat="server" Text="Địa chỉ"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="rtADDRESS" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Điện thọai/Fax")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtFAX" runat="server" SkinID="Textbox50" />
                </td>
                <td class="lb">
                    <%# Translate("Website")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtWEBSITE_LINK" runat="server" SkinID="Textbox50" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số tài khoản")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtBANK_NO" runat="server" SkinID="Textbox50" />
                </td>
                <td class="lb">
                    <%# Translate("Mã số thuế")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtPIT_NO" runat="server" SkinID="Textbox50">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tại ngân hàng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboBANK_ID" runat="server" OnClientSelectedIndexChanged="OnCSIC_BANK_ID" />
                </td>
                <td class="lb">
                    <%# Translate("Chi nhánh ngân hàng")%>
                </td>
                <td colspan="3">
                    <tlk:RadComboBox ID="cboBANK_BRACH_ID" runat="server" OnClientItemsRequested="OnCIR_BANK_BRACH_ID" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Người đại diện")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtREPRESENTATIVE_ID" Width="128px" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnREPRESENTATIVE_ID" runat="server" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="5">
                    </tlk:RadButton>
                    <asp:HiddenField ID="hidREPRESENTATIVE_ID" runat="server" />
                </td>
                <td class="lb">
                    <%# Translate("Kế toán trưởng")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtACCOUNTING_ID" Width="128px" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnACCOUNTING_ID" runat="server" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="5">
                    </tlk:RadButton>
                    <asp:HiddenField ID="hidACCOUNTING_ID" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Phụ trách nhân sự")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtHR_ID" Width="128px" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnHR_ID" runat="server" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="5">
                    </tlk:RadButton>
                    <asp:HiddenField ID="hidHR_ID" runat="server" />
                </td>
                <td class="lb">
                    <%# Translate("Giấy ủy quyền")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtAUTHOR_LETTER" runat="server" SkinID="Textbox50">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tỉnh/thành nơi ký hợp đồng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPROVINCE_CONTRACT_ID" runat="server" OnClientSelectedIndexChanged="OnCSIC_PROVINCE_CONTRACT_ID" />
                </td>
                <td class="lb">
                    <%# Translate("Quận huyện nơi ký hợp đồng ")%>
                </td>
                <td colspan="3">
                    <tlk:RadComboBox ID="cboDISTRICT_CONTRACT_ID" runat="server" OnClientItemsRequested="OnCIR_DISTRICT_CONTRACT_ID" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số giấy phép kinh doanh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNUMBER_BUSINESS" runat="server" SkinID="Textbox50" />
                </td>
                <td class="lb">
                    <%# Translate("Tên đăng ký GPKD")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtBUSS_REG_NAME" runat="server" SkinID="Textbox50" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên đơn vị chủ quản")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMAN_UNI_NAME" runat="server" SkinID="Textbox50" />
                </td>
                <td class="lb">
                    <%# Translate("Số thứ tự")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rdOrdNo" runat="server" SkinID="NUMBER">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdOrdNo"
                        runat="server" ErrorMessage="Bạn phải nhập số thứ tự" ToolTip="Bạn phải nhập số thứ tự">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <%--<tr style = "display : none">
                <td class="lb">
                    <asp:Label ID="lbLocationWork" runat="server" Text="Địa điểm làm việc"></asp:Label>
                  <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtLocationWork" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>--%>
            <%--<tr  style = "display : none">
                <td class="lb">
                  <asp:Label ID="lbTypeDecision" runat="server" Text="Loại quyết định"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtTypeDecision" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>--%>
            <%--<tr  style = "display : none">
                <td class="lb">
                 <asp:Label ID="lbNumberDecision" runat="server" Text="Số quyết định"></asp:Label>
                 <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNumberDecision" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                   <asp:Label ID="lbEffectDate" runat="server" Text="Ngày hiệu lực của quyết định"></asp:Label>
                  
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>--%>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbFOUNDATION_DATE" runat="server" Text="Ngày thành lập"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFOUNDATION_DATE" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDicision_Date" runat="server" Text="Ngày giải thể"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdDISSOLVE_DATE" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbREMARK" runat="server" Text="Ghi chú"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td colspan="4">
                    <tlk:RadTextBox ID="txtREMARK" runat="server" SkinID="Textbox9999" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <%--<tr>
                <td>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkOrgChart" />
                  <asp:Label ID="lbOrgChart" runat="server" Text="Hiển thị org chart"></asp:Label>
                  <span class="lbReq"></span>
                    <tlk:RadButton CausesValidation="false" Text="Hiển thị org chart" ToggleType="CheckBox"
                        runat="server" ID="chkOrgChart" ButtonType="ToggleButton">
                    </tlk:RadButton>
                </td>
                <td  style = "display : none">
                    <asp:Label ID="lbFile" runat="server" Text="Tập tin đính kèm"></asp:Label>
                    <span class="lbReq"></span>
                </td>
               
                <td colspan="2"  style = "display : none">
                    <tlk:RadListBox ID="lstFile" runat="server" CheckBoxes="true" Height="100px" OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging"
                        Width="80%" />
                </td>
                 <td  style = "display : none">
                    <tlk:RadButton ID="btnUploadFile" runat="server" Text="<%$ Translate: Upload %>"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnDownloadFile" runat="server" Text="<%$ Translate: Tải %>"
                        CausesValidation="false" OnClientClicked="rbtClicked">
                    </tlk:RadButton>
                   
                </td>
            </tr>--%>
        </table>
        <%--<div style="display: none;">
            <asp:Button runat="server" ID="btnSaveImage" CausesValidation="false" />
            <asp:Button runat="server" ID="btnSaveFile" CausesValidation="false" />
        </div>--%>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="PlaceHolder1" runat="server">
    <Common:ctrlFindEmployeePopup ID="ctrlREPRESENTATIVE_ID" runat="server" IsHideTerminate="false"
        MultiSelect="false" LoadAllOrganization="false" />
</asp:PlaceHolder>
<asp:PlaceHolder ID="PlaceHolder2" runat="server">
    <Common:ctrlFindEmployeePopup ID="CtrlhidACCOUNTING_ID" runat="server" IsHideTerminate="false"
        MultiSelect="false" LoadAllOrganization="false" />
</asp:PlaceHolder>
<asp:PlaceHolder ID="PlaceHolder3" runat="server">
    <Common:ctrlFindEmployeePopup ID="CtrlHR_ID" runat="server" IsHideTerminate="false"
        MultiSelect="false" LoadAllOrganization="false" />
</asp:PlaceHolder>
<tlk:RadScriptBlock ID="rscriptblock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        //function OnClientItemSelectedIndexChanging(sender, args) {
        //    var item = args.get_item();
        //    item.set_checked(!item.get_checked());
        //    args.set_cancel(true);
        //}
        //function rbtClicked(sender, eventArgs) {
        //    enableAjax = false;
        //}
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function loadcombobox(id_pro, id_cbo, list_id) {
            var combo = $find(id_cbo);
            
            if (id_pro > 0) {
                var n = document.getElementById(list_id)
                var n1 = JSON.parse(n.value)

                var filterObj = n1.filter(function (e) {
                    return e.PROVINCE_ID == id_pro;
                });

                combo.trackChanges();
                combo.clearItems()

                for (i in filterObj) {
                    var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                    comboItem.set_text(filterObj[i].NAME_VN);
                    comboItem.set_value(filterObj[i].ID);
                    combo.get_items().add(comboItem);
                }

                combo.commitChanges();
            } else {
                combo.trackChanges();
                combo.clearItems()
                combo.commitChanges();
            }
        }
        function OnClientSelectedIndexChangedPROVINCE_ID(sender, eventArgs) {
            cbo = $find('<%= cboPROVINCE_ID.ClientID %>');
            var cbo2 = '<%= cboDISTRICT_ID.ClientID %>';
            value = cbo.get_value();
            if (!value) {
                value = 0;
            }
            loadcombobox(value, cbo2, "<%= hidListDistrict.ClientID %>")

            var txttemp = document.getElementById(cbo2 + '_Input');
            txttemp.value = '';
        }

        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {
                case '<%= cboDISTRICT_ID.ClientID %>':
                    cbo = $find('<%= cboPROVINCE_ID.ClientID %>');
                    value = cbo.get_value();
                    break;
                default:
                    break;
            }

            if (!value) {
                value = 0;
            }
            loadcombobox(value, id)
        }
        function OnCSIC_PROVINCE_CONTRACT_ID(sender, eventArgs) {
            cbo = $find('<%= cboPROVINCE_CONTRACT_ID.ClientID %>');
            var cbo2 = '<%= cboDISTRICT_CONTRACT_ID.ClientID %>';
            value = cbo.get_value();
            if (!value) {
                value = 0;
            }
            loadcombobox(value, cbo2, "<%= hidListDistrict.ClientID %>")

            var txttemp = document.getElementById(cbo2 + '_Input');
            txttemp.value = '';
        }
        function OnCIR_DISTRICT_CONTRACT_ID(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {
                case '<%= cboDISTRICT_CONTRACT_ID.ClientID %>':
                    cbo = $find('<%= cboPROVINCE_CONTRACT_ID.ClientID %>');
                    value = cbo.get_value();
                    break;
                default:
                    break;
            }

            if (!value) {
                value = 0;
            }
            loadcombobox(value, id, "<%= hidListDistrict.ClientID %>")
        }
        function OnCSIC_BANK_ID(sender, eventArgs) {
            cbo = $find('<%= cboBANK_ID.ClientID %>');
            var cbo2 = '<%= cboBANK_BRACH_ID.ClientID %>';
            value = cbo.get_value();
            if (!value) {
                value = 0;
            }
            loadcombobox(value, cbo2, "<%= hidListbankBrach.ClientID %>")

            var txttemp = document.getElementById(cbo2 + '_Input');
            txttemp.value = '';
        }
        function OnCIR_BANK_BRACH_ID(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {
                case '<%= cboDISTRICT_CONTRACT_ID.ClientID %>':
                    cbo = $find('<%= cboBANK_BRACH_ID.ClientID %>');
                    value = cbo.get_value();
                    break;
                default:
                    break;
            }

            if (!value) {
                value = 0;
            }
            loadcombobox(value, id, "<%= hidListbankBrach.ClientID %>")
        }



        //function btnDownFile_Clicking(sender, args) {
        //    enableAjax = false;
        //}
        <%--function fileUploaded(sender, args) {
            $get('<%= btnSaveImage.ClientID %>').click();
        }--%>
        <%--function fileUploaded1(sender, args) {
            $get('<%= btnSaveFile.ClientID %>').click();
        }--%>
        <%--function validationFailed(sender, args) {
            sender.deleteFileInputAt(0);
            var message = '<%=Translate("Dung lượng ảnh phải < 4MB") %>';
            var notify104126 = noty({ text: message, dismissQueue: true, type: 'error' });
            setTimeout(function () { $.noty.close(notify104126.options.id); }, 2000);
        }--%>
    </script>
</tlk:RadScriptBlock>

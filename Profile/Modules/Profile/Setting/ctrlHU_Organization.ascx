<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Organization.ascx.vb"
    Inherits="Profile.ctrlHU_Organization" %>
<style type="text/css">
    #btnSaveImage
    {
        display: none;
    }
    
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
<tlk:radsplitter id="RadSplitter1" runat="server" width="100%" height="100%">
    <tlk:radpane id="LeftPane" runat="server" minwidth="200" width="400px">
        <tlk:radtreeview id="treeOrgFunction" runat="server" causesvalidation="False" height="93%">
        </tlk:radtreeview>
        <asp:CheckBox ID="cbDissolve" runat="server" Text="<%$ Translate: Hiển thị các đơn vị giải thể %>"
            AutoPostBack="True" />
    </tlk:radpane>
    <tlk:radsplitbar id="RadSplitBar1" runat="server" collapsemode="Forward">
    </tlk:radsplitbar>
    <tlk:radpane id="MainPane" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidParentID" runat="server" />
        <asp:HiddenField ID="hidRepresentative" runat="server" />
        <tlk:radtoolbar id="tbarOrgFunctions" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Đơn vị cha")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="txtParent_Name" runat="server" readonly="true" width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên đơn vị (VN)")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="txtNameVN" runat="server" width="100%">
                    </tlk:radtextbox>
                    <asp:RequiredFieldValidator ID="reqNameVN" ControlToValidate="txtNameVN" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên đơn vị %>" ToolTip="<%$ Translate: Bạn phải nhập Tên đơn vị %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên đơn vị (EN)")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="RadTextBox1" runat="server" width="100%">
                    </tlk:radtextbox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtNameVN"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Tên đơn vị %>" ToolTip="<%$ Translate: Bạn phải nhập Tên đơn vị %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Quản lý đơn vị")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:radtextbox id="txtRepresentativeName" runat="server" width="130px" readonly="True">
                    </tlk:radtextbox>
                    <tlk:radbutton id="btnFindRepresentative" skinid="ButtonView" runat="server" causesvalidation="false"
                        width="40px">
                    </tlk:radbutton>
                </td>
                <td class="lb">
                    <%# Translate("CostCenter")%>
                </td>
                <td>
                    <tlk:radtextbox id="txtCostCenter" runat="server" width="130px">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã đơn vị")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:radtextbox id="txtCode" runat="server" skinid="Textbox50">
                    </tlk:radtextbox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mã đơn vị %>" ToolTip="<%$ Translate: Bạn phải nhập Mã đơn vị %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Cấp phòng ban")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:radcombobox id="cboOrg_level" runat="server">
                    </tlk:radcombobox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboOrg_level"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập cấp đơn vị %>" ToolTip="<%$ Translate: Bạn phải nhập cấp đơn vị %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Vùng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:radcombobox id="cboRegion" runat="server">
                    </tlk:radcombobox>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Translate: Mã chi phí không được trùng %>"
                        ToolTip="<%$ Translate: Mã chi phí không được trùng %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị đóng bảo hiểm")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:radcombobox id="cboU_insurance" runat="server">
                    </tlk:radcombobox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cboU_insurance"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập đơn vị đóng bảo hiểm %>"
                        ToolTip="<%$ Translate: Bạn phải nhập đơn vị đóng bảo hiểm %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <div style="display: none;">
            <asp:Button runat="server" ID="btnSaveImage" CausesValidation="false" />
            <asp:Button runat="server" ID="btnSaveFile" CausesValidation="false" />
        </div>
    </tlk:radpane>
</tlk:radsplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<tlk:radscriptblock id="rscriptblock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function btnDownFile_Clicking(sender, args) {
            enableAjax = false;
        }
        function fileUploaded(sender, args) {
            $get('<%= btnSaveImage.ClientID %>').click();
        }
        function fileUploaded1(sender, args) {
            $get('<%= btnSaveFile.ClientID %>').click();
        }
        function validationFailed(sender, args) {
            sender.deleteFileInputAt(0);
            var message = '<%=Translate("Dung lượng ảnh phải < 4MB") %>';
            var notify104126 = noty({ text: message, dismissQueue: true, type: 'error' });
            setTimeout(function () { $.noty.close(notify104126.options.id); }, 2000);
        }
    </script>
</tlk:radscriptblock>

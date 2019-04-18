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
                  <asp:Label ID="lblParent_Name" runat="server" Text="<%$ Translate :Đơn vị cha %>"></asp:Label>
             <span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="txtParent_Name" runat="server" readonly="true" width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                   <asp:Label ID="lbNameVN" runat="server" Text="<%$ Translate:Tên đơn vị (VN) %>"></asp:Label>
                   <span class="lbReq">*</span>
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
                    <asp:Label ID="lbNameEN" runat="server" Text="<%$ Translate:Tên đơn vị (EN)%>"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="txtNameEN" runat="server" width="100%">
                    </tlk:radtextbox>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtNameVN"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Tên đơn vị %>" ToolTip="<%$ Translate: Bạn phải nhập Tên đơn vị %>"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                   <asp:Label ID="lbRepresentativeName" runat="server" Text="<%$ Translate:Quản lý đơn vị %>"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:radtextbox id="txtRepresentativeName" runat="server" width="130px" readonly="True">
                    </tlk:radtextbox>
                    <tlk:radbutton id="btnFindRepresentative" skinid="ButtonView" runat="server" causesvalidation="false"
                        width="40px">
                    </tlk:radbutton>
                </td>
                <td class="lb">
                   <asp:Label ID="lbUNIT_LEVEL" runat="server" Text="<%$ Translate: Bậc đơn vị %>"></asp:Label>
                </td>
                <td colspan ="3">
                    <tlk:RadComboBox  id="cbUNIT_LEVEL" runat="server" width="100%">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbCode" runat="server" Text="<%$ Translate: Mã đơn vị %>"></asp:Label>
                   <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:radtextbox id="txtCode" runat="server" skinid="Textbox50">
                    </tlk:radtextbox>
                   <%-- <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mã đơn vị %>" ToolTip="<%$ Translate: Bạn phải nhập Mã đơn vị %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                     <asp:Label ID="lbOrg_level" runat="server" Text="<%$ Translate: Cấp phòng ban %>"></asp:Label>
                    <span class="lbReq"></span>
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
                   <asp:Label ID="lbRegion" runat="server" Text="<%$ Translate: Vùng %>"></asp:Label>
                  <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:radcombobox id="cboRegion" runat="server">
                    </tlk:radcombobox>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Translate: Mã chi phí không được trùng %>"
                        ToolTip="<%$ Translate: Mã chi phí không được trùng %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbinsurance" runat="server" Text="<%$ Translate: Đơn vị đóng bảo hiểm%>"></asp:Label>
                 <span class="lbReq"></span>
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
            <tr>
                <td class="lb">
                   <asp:Label ID="lbNUMBER_BUSINESS" runat="server" Text="<%$ Translate: Giấy phép ĐKKD (Mã số thuế)%>"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:radtextbox id="rtNUMBER_BUSINESS" runat="server" skinid="Textbox50">
                    </tlk:radtextbox>
                </td>
                <td class="lb">
                  <asp:Label ID="lbDATE_BUSINESS" runat="server" Text="<%$ Translate: Ngày cấp giấy phép ĐKKD %>"></asp:Label>
                   
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdDATE_BUSINESS" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
           
            <tr>
                <td class="lb">
                    <asp:Label ID="lbADDRESS" runat="server" Text="<%$ Translate: Địa chỉ %>"></asp:Label>
                  <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="rtADDRESS" runat="server" skinid="Textbox9999"  width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbLocationWork" runat="server" Text="<%$ Translate: Địa điểm làm việc %>"></asp:Label>
                  <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="rtxtLocationWork" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                  <asp:Label ID="lbTypeDecision" runat="server" Text="<%$ Translate: Loại quyết định %>"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="rtxtTypeDecision" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                 <asp:Label ID="lbNumberDecision" runat="server" Text="<%$ Translate:Số quyết định%>"></asp:Label>
                 <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="rtxtNumberDecision" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                   <asp:Label ID="lbFOUNDATION_DATE" runat="server" Text="<%$ Translate: Ngày thành lập %>"></asp:Label>
                  
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFOUNDATION_DATE" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                <asp:Label ID="lbDicision_Date" runat="server" Text="<%$ Translate:Ngày giải thể%>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdDicision_Date" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                  <asp:Label ID="lbREMARK" runat="server" Text="<%$ Translate: Ghi chú %>"></asp:Label>
                   <span class="lbReq"></span>
                </td>
                <td colspan="4">
                    <tlk:RadTextBox ID="rtREMARK" runat="server" SkinID="Textbox9999" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkOrgChart" />
                  <asp:Label ID="lblOrgChart" runat="server" Text="<%$ Translate: Hiển thị org chart %>"></asp:Label>
                  <span class="lbReq"></span>
                 <%--   <tlk:RadButton CausesValidation="false" Text="Hiển thị org chart" ToggleType="CheckBox"
                        runat="server" ID="chkOrgChart" ButtonType="ToggleButton">
                    </tlk:RadButton>--%>
                </td>
                <td>
                  <asp:Label ID="lblFile" runat="server" Text="<%$ Translate: Tập tin đính kèm %>"></asp:Label>
                <span class="lbReq"></span>
                </td>
               
                <td colspan="2">
                    <tlk:RadListBox ID="lstFile" runat="server" CheckBoxes="true" Height="100px" OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging"
                        Width="80%" />
                </td>
                 <td>
                    <tlk:RadButton ID="btnUploadFile" runat="server" Text="<%$ Translate: Upload %>"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnDownloadFile" runat="server" Text="<%$ Translate: Tải %>"
                        CausesValidation="false" OnClientClicked="rbtClicked">
                    </tlk:RadButton>
                   
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
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<tlk:radscriptblock id="rscriptblock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
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

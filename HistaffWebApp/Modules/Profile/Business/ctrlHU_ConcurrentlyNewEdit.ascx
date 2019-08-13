<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ConcurrentlyNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_ConcurrentlyNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<asp:HiddenField ID="hidOrgCOn" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<asp:HiddenField ID="hidSign2" runat="server" />
<asp:HiddenField ID="hidOrgId" runat="server" />
<asp:HiddenField ID="hidSignStop" runat="server" />
<asp:HiddenField ID="hidSignStop2" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCon" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Width="100%">
        <div style="width: 100%">
            <div style="width: 50%; float: left">
                <fieldset>
                    <legend>
                        <%# Translate("Thông tin chính")%>
                    </legend>
                    <table onkeydown="return (event.keyCode!=13)" style="height: 140px" class="table-form">
                        <tr>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Mã nhân viên")%>
                                <span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtEmpCode" Width="128px" runat="server" ReadOnly="true">
                                </tlk:RadTextBox>
                                <tlk:RadButton ID="btnFindEmp" Enabled="False" runat="server" SkinID="ButtonView"
                                    CausesValidation="false" TabIndex="5">
                                </tlk:RadButton>
                                <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmpCode" runat="server"
                                    ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Họ và tên")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtEmpName" runat="server" ReadOnly="true">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb" style="text-align: left">
                                <%# Translate("Đơn vị công tác")%>
                            </td>
                            <td>
                                   <tlk:RadToolTip ID="EmpOrgNameToolTip" runat="server" Width="100" Height="10" Position="BottomRight" 
            Text='' TargetControlID="txtEmpOrgName"> </tlk:RadToolTip> 
                                <tlk:RadTextBox Width="128px" ID="txtEmpOrgName" runat="server" ReadOnly="true">
                                </tlk:RadTextBox>
                                <tlk:RadButton runat="server" ID="btnOrgId" Width="8px" SkinID="ButtonView" CausesValidation="false" ReadOnly="true"/>
                            </td>
                            <td class="lb" style="text-align: left">
                                <%# Translate("Chức danh")%>
                            </td>
                            <td>
                                 <tlk:RadComboBox  ID="cboTitleId" runat="server" ReadOnly="true" Enabled="false"
                                    CausesValidation="false">
                                </tlk:RadComboBox>
                               <%-- <tlk:RadTextBox ID="txtEmpTitle" Width="100%" runat="server" ReadOnly="True">
                                </tlk:RadTextBox>--%>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <div style="width: 50%; float: left">
                <fieldset>
                    <legend>
                        <%# Translate("Thông tin kiêm nhiệm")%>
                    </legend>
                    <table onkeydown="return (event.keyCode!=13)" class="table-form" style="height: 140px">
                        <tr>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Đơn vị kiêm nhiệm")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadToolTip ID="ORG_CONNameToolTip" runat="server" Width="100" Height="10" Position="BottomRight" 
                                 Text='' TargetControlID="txtORG_CONName"> </tlk:RadToolTip> 
                                <tlk:RadTextBox runat="server" ID="txtORG_CONName" Width="130px" ReadOnly="true" />
                                <tlk:RadButton runat="server" ID="btnORG_CON" SkinID="ButtonView" CausesValidation="false" />
                                <asp:RequiredFieldValidator ID="reqOrgName" ControlToValidate="txtORG_CONName" runat="server"
                                    ErrorMessage="<%$ Translate: Bạn phải nhập đơn vị kiêm nhiệm. %>" ToolTip="<%$ Translate: Bạn phải nhập đơn vị kiêm nhiệm. %>"> 
                                </asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Chức danh kiêm nhiệm")%>
                            </td>
                            <td>
                                <tlk:RadComboBox  ID="cboTITLE_CON" runat="server"
                                    CausesValidation="false">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr> 
                            <td style="display:none">
                                <%# Translate("Phụ cấp kiêm nhiệm")%>
                            </td>
                            <td style="display:none">
                                <tlk:RadNumericTextBox ID="rntxtALLOW_MONEY" MinValue="0" runat="server">
                                    <NumberFormat DecimalDigits="0" ZeroPattern="n" />
                                </tlk:RadNumericTextBox>
                            </td> 
                            <td></td>
                        <td style="display:none">
                            <asp:CheckBox ID="chkALLOW" runat="server" Text="<%$ Translate: Đơn vị kiêm nhiệm trả phụ cấp? %>"
                                AutoPostBack="false"/>
                        </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Ngày hiệu lực hưởng phụ cấp kiêm nhiệm")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdEFFECT_DATE_CON" runat="server">
                                </tlk:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdEFFECT_DATE_CON"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày hưởng PC. %>"
                                    ToolTip="<%$ Translate: Bạn phải nhập Ngày hưởng PC. %>"> </asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Ngày hết hiệu lực")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdEXPIRE_DATE_CON" runat="server">
                                </tlk:RadDatePicker>
                                <asp:CustomValidator ID="cval_EffectDate_ExpireDate" runat="server" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực. %>"
                                    ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực. %>">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                             <td style="text-align: left" class="lb">
                                <%# Translate("Tập tin đính kèm")%>
                            </td>
                            <td>
                                <div>
                                    <tlk:RadComboBox ID="cboUpload" runat="server" SkinID="LoadDemand" EnableCheckAllItemsCheckBox="true"
                                        CheckBoxes="true" DropDownAutoWidth="Enabled">
                                    </tlk:RadComboBox>
                                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                                    </tlk:RadTextBox>
                                    
                                </div>
                            </td>
                            <td>
                                <tlk:RadButton Width="35px" runat="server" ID="btnUploadFile" Text="<%$ Translate: Đăng %>"
                                        CausesValidation="false" />
                                    <tlk:RadButton Width="35px" ID="btnDownload" runat="server" Text="<%$ Translate: Tải %>"
                                        CausesValidation="false" OnClientClicked="rbtClicked">
                                    </tlk:RadButton>
                            </td>
                             <td style="display:none">
                            <asp:CheckBox ID="chkIsChuyen" runat="server" Text="<%$ Translate: Tạo nhân viên mới %>"
                                AutoPostBack="false" />
                             </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </div>
        <div style="width: 100%">
            <div style="width: 50%; float: left">
                <fieldset>
                    <legend>
                        <%# Translate("Thông tin phê duyệt kiêm nhiệm")%>
                    </legend>
                    <table onkeydown="return (event.keyCode!=13)" class="table-form" style="height: 200px">
                        <tr>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Số quyết định")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtCON_NO" runat="server">
                                </tlk:RadTextBox>
                                <asp:CustomValidator ID="curDecisionNo" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn số quyết định %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn số quyết định %>">
                                </asp:CustomValidator>
                                <asp:CustomValidator ID="CusDecisionNoSame" runat="server" ErrorMessage="<%$ Translate: Mã số bị trùng  %>"
                                    ToolTip="<%$ Translate: Mã số bị trùng %>">
                                </asp:CustomValidator>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Ngày ký")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdSIGN_DATE" runat="server">
                                </tlk:RadDatePicker>
                                <asp:CustomValidator ID="curStartDate" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Ngày ký %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn Ngày ký %>">
                                </asp:CustomValidator>
                            </td>
                            <td class="lb" style="text-align: left">
                                <%# Translate("Trạng thái của quyết định")%>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboStatus" runat="server" CausesValidation="False">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb" style="text-align: left;">
                                <%# Translate("Người ký 1")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server" ID="txtSIGN" ReadOnly="true" Width="128px" />
                                <tlk:RadButton runat="server" ID="btnSIGN" SkinID="ButtonView" CausesValidation="false" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtSIGN" runat="server"
                                    ErrorMessage="<%$ Translate: Bạn phải nhập người phê duyệt. %>" ToolTip="<%$ Translate: Bạn phải nhập người phê duyệt. %>"> 
                                </asp:RequiredFieldValidator>
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                            </td>
                            <td class="lb" style="text-align: left">
                                <%# Translate("Chức danh người ký 1")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtSIGN_TITLE" ReadOnly="True" runat="server">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb" style="text-align: left;">
                                <%# Translate("Người ký 2")%><%--<span class="lbReq">*</span>--%>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server" ID="txtSIGN2" ReadOnly="true" Width="128px" />
                                <tlk:RadButton runat="server" ID="btnSIGN2" SkinID="ButtonView" CausesValidation="false" />
                               <%-- <asp:RequiredFieldValidator ID="reqApplyName1" ControlToValidate="txtSIGN2" runat="server"
                                    ErrorMessage="<%$ Translate: Bạn phải nhập người phê duyệt. %>" ToolTip="<%$ Translate: Bạn phải nhập người phê duyệt. %>"> 
                                </asp:RequiredFieldValidator>--%>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </td>
                            <td class="lb" style="text-align: left">
                                <%# Translate("Chức danh người ký 2")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtSIGN_TITLE2" ReadOnly="True" runat="server">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb" style="text-align: left">
                                <%# Translate("Ghi chú")%>
                            </td>
                            <td colspan="3">
                                <tlk:RadTextBox ID="txtRemark" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <div style="width: 50%; float: left">
                <fieldset>
                    <legend>
                        <%# Translate("Thông tin thôi kiêm nhiệm")%>
                    </legend>
                    <table onkeydown="return (event.keyCode!=13)" class="table-form" style="height: 200px">
                        <tr>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Số quyết định thôi kiêm nhiệm")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtCON_NO_STOP" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Ngày hiệu lực của quyết định thôi kiêm nhiệm")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdEFFECT_DATE_STOP" runat="server">
                                </tlk:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Ngày ký quyết định thôi kiêm nhiệm")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdSIGN_DATE_STOP" runat="server">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb" style="text-align: left">
                                <%# Translate("Trạng thái của quyết định thôi kiêm nhiệm")%>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cbSTATUS_STOP" runat="server" CausesValidation="False">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Người ký quyết định thôi kiêm nhiệm 1")%>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server" ID="txtSIGN_STOP" Width="130px" ReadOnly="true" />
                                <tlk:RadButton runat="server" ID="btnSIGN_STOP" SkinID="ButtonView" CausesValidation="false" />
                            </td>
                            <td class="lb" style="text-align: left">
                                <%# Translate("Chức danh người ký quyết định thôi kiêm nhiệm 1")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtSIGN_STOP_TITLE" ReadOnly="True" runat="server">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Người ký quyết định thôi kiêm nhiệm 2")%>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server" ID="txtSIGN_STOP2" Width="130px" ReadOnly="true" />
                                <tlk:RadButton runat="server" ID="btnSIGN_STOP2" SkinID="ButtonView" CausesValidation="false" />
                            </td>
                            <td class="lb" style="text-align: left">
                                <%# Translate("Chức danh người ký quyết định thôi kiêm nhiệm 2")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtSIGN_STOP_TITLE2" ReadOnly="True" runat="server">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb" style="text-align: left">
                                <%# Translate("Ghi chú")%>
                            </td>
                            <td >
                                <tlk:RadTextBox Width="100%" ID="txtREMARK_STOP" runat="server">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                             <td style="text-align: left" class="lb">
                                <%# Translate("Tập tin đính kèm")%>
                            </td>
                            <td>
                                <div>
                                    <tlk:RadComboBox ID="cboUpload1" runat="server" SkinID="LoadDemand" EnableCheckAllItemsCheckBox="true"
                                        CheckBoxes="true" DropDownAutoWidth="Enabled">
                                    </tlk:RadComboBox>
                                    <tlk:RadTextBox ID="txtUploadFile1" runat="server" Visible="false">
                                    </tlk:RadTextBox>
                                   
                                </div>
                            </td>
                            <td> <tlk:RadButton Width="35px" runat="server" ID="btnUploadFile1" Text="<%$ Translate: Đăng %>"
                                        CausesValidation="false" />
                                    <tlk:RadButton Width="35px" ID="btnDownload1" runat="server" Text="<%$ Translate: Tải %>"
                                        CausesValidation="false" OnClientClicked="rbtClicked">
                                    </tlk:RadButton>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </div>
        <div style="float: left; width: 100%">
            <tlk:RadGrid ID="rgConcurrently" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                Height="270px" AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings AllowColumnsReorder="True" EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling UseStaticHeaders="true" AllowScroll="True" />
                    <Resizing AllowColumnResize="true" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,STATUS">
                    <Columns>
                    </Columns>
                </MasterTableView>
            </tlk:RadGrid>
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrgCon" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign2" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSignStop" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSignStop2" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindEmployeeGoc" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlUpload ID="ctrlUpload2" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">


        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

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
                //getRadWindow().close(null);
                //args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "REJECT") {
                enableAjax = false;
            }
        }
        function getFileName(box, id, name) {
            var gotopage = "/GetFileMLG.aspx?fp=" + box + "&fid=" + id + "&fname=" + name;
            window.open(gotopage);
        }
        function OpenEditTransfer(e) {
        }
        function OnClientItemsRequesting(sender, eventArgs) {

        }
        function OnClientSelectedIndexChanged(sender, eventArgs) {


        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>

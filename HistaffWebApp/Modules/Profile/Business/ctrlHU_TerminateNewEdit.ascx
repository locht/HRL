<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_TerminateNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_TerminateNewEdit" %>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:radsplitter id="RadSplitter3" runat="server" width="100%" height="100%" orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarTerminate" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidTitleID" runat="server" />
        <asp:HiddenField ID="hidOrgID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:HiddenField ID="hiSalbasic" runat="server" />
        <asp:HiddenField ID="hidWorkStatus" runat="server" />
        <asp:HiddenField ID="hidCheckDelete" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin chung")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbEmployeeCode" Text ="MSNV" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" SkinID="Readonly" ReadOnly="true"
                        Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="Bạn phải nhập nhân viên." ToolTip="Bạn phải nhập nhân viên."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbEmployeeName" Text ="Họ tên nhân viên" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbOrgName" Text ="Phòng ban" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
           
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbTitleName" Text ="Chức danh" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>

                <td class="lb">
                    <asp:Label runat ="server" ID ="lbJoinDateState" Text ="Ngày vào làm" ></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdJoinDateState" runat="server" Enabled="False" ReadOnly="true">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbContractNo" Text ="Hợp đồng hiện tại" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContractNo" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbContractEffectDate" Text ="Từ ngày" ></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdContractEffectDate" runat="server" Enabled="False">
                    </tlk:RadDatePicker> 
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbContractExpireDate" Text ="Đến ngày" ></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdContractExpireDate" runat="server" Enabled="False">
                    </tlk:RadDatePicker>
                </td>                
            </tr>
            <tr style="visibility:hidden">
                <td class="lb">
                    <tlk:RadTextBox ID="txtRemindLink" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin nghỉ việc")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbSendDate" Text ="Ngày nộp đơn" ></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSendDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CustomValidator ID="cvaldpSendDate" runat="server" ErrorMessage="Ngày nộp đơn không nhỏ hơn ngày vào công ty."
                        ToolTip="Ngày nộp đơn không nhỏ hơn ngày vào công ty.">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                  <asp:Label runat ="server" ID ="lbEffectDate" Text ="Ngày hiệu lực" > </asp:Label><span style="color: red"></span>
             
                </td> 
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" ControlToValidate="rdEffectDate" runat="server"
                        ErrorMessage="Bạn phải nhập ngày hiệu lực." ToolTip="Bạn phải nhập ngày hiệu lực."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbTerReason" Text ="Lý do nghỉ" ></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTerReason" runat="server">
                    </tlk:RadComboBox>
                </td>         
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbTerReasonDetail" Text ="Lý do chi tiết" ></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtTerReasonDetail" runat="server" SkinID="TextBox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbLastDate" Text ="Ngày nghỉ thực tế" ></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdLastDate" runat="server" AutoPostBack="true" DateInput-CausesValidation="false">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqLastDate" ControlToValidate="rdLastDate" runat="server"
                        ErrorMessage="Bạn phải nhập ngày làm việc cuối cùng." ToolTip="Bạn phải nhập ngày làm việc cuối cùng."> </asp:RequiredFieldValidator>

                    <asp:CustomValidator ID="cval_LastDate_SendDate" runat="server" ErrorMessage="Ngày làm việc cuối cùng phải lớn hơn ngày nộp đơn."
                        ToolTip="Ngày làm việc cuối cùng phải lớn hơn ngày nộp đơn.">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cval_LastDate_JoinDate" runat="server" ErrorMessage="Ngày làm việc cuối cùng không nhỏ hơn ngày vào công ty."
                        ToolTip="Ngày làm việc cuối cùng không nhỏ hơn ngày vào công ty.">
                    </asp:CustomValidator>
                </td>
                <td></td>
                <td>
                    <asp:CheckBox runat="server" ID="chkDSDen" Text="Danh sách đen" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbRemark" Text ="Ghi chú" ></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="TextBox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin bàn giao")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="5">
                    <tlk:RadGrid PageSize="50" ID="rgHandoverContent" runat="server" Height="200px" Width="550px" SkinID="GridNotPaging"
                        AllowMultiRowEdit="true">
                        <MasterTableView DataKeyNames="CHECKCOUNT" ClientDataKeyNames="CHECKCOUNT"
                            EditMode="InPlace">
                            <Columns>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung bàn giao %>" DataField="NAME"
                                    SortExpression="NAME" UniqueName="NAME" ReadOnly="true" />
<%--                                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Hoàn thành %>" DataField="STATUS"
                                    SortExpression="STATUS" UniqueName="STATUS">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </tlk:GridCheckBoxColumn>--%>
                                <tlk:GridTemplateColumn  HeaderText="<%$ Translate: Hoàn thành %>"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="STATUS" runat="server" Enabled="False" />
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </tlk:RadGrid>
                </td>
            </tr>
            
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Phê duyệt")%></b>
                    <hr />
                </td>
            </tr>
            <tr>                
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbDecisionNo" Text ="Số quyết định" ></asp:Label><span style="color:red"> *</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionNo" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqDecisionNo" ControlToValidate="txtDecisionNo" runat="server"
                        ErrorMessage="Bạn phải nhập số quyết định." ToolTip="Bạn phải nhập số quyết định."> </asp:RequiredFieldValidator>
                </td>
                 <td class="lb">
                    <asp:Label runat ="server" ID ="lbStatus" Text ="Trạng thái phê duyệt" ></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqStatus" ControlToValidate="cboStatus" runat="server"
                        ErrorMessage= "Bạn phải chọn trạng tháỉ phê duyệt." ToolTip="Bạn phải chọn trạng tháỉ phê duyệt."> </asp:RequiredFieldValidator>
                </td>
                 <td class="lb">
                    <asp:Label runat ="server" ID ="lbSignDate" Text ="Ngày ký" ></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSignDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                 <td class="lb" style="display:none">
                    <asp:Label runat ="server" ID ="lbDecisionType" Text ="Loại quyết định" ></asp:Label> 
                </td>
                <td style="display:none">
                    <tlk:RadComboBox ID="cboDecisionType" AutoPostBack="true" CausesValidation="false" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb" style="display:none">
                    <asp:Label runat ="server" ID ="Label4" Text ="Tập tin đính kèm"></asp:Label>
                </td>
                <td style="display:none">
                    <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3"  Enabled="true"/>
                    <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải tập tin%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
               
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbSignerName" Text ="Người ký" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignerName" runat="server" ReadOnly="true" Width="130px" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindSinger" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbSignerTitle" Text ="Chức danh người phê duyệt" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignerTitle" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>            
        </table>
    </tlk:RadPane>
</tlk:radsplitter>
<common:ctrlupload id="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:radcodeblock id="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusStatus(oSrc, args) {
            var cbo = $find("<%# cboStatus.ClientID%>");
            args.IsValid = (cbo.get_value().length != 0);
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
            //if (args.get_item().get_commandName() == 'CANCEL') {
            //    getRadWindow().close(null);
            //    args.set_cancel(true);
            //}
        }

        function btnDeleteReasonClick(sender, args) {
            var bCheck = $find('<%# rgHandoverContent.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                args.set_cancel(true);
            }
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function btnDeleteDebtsOnClientClicking(sender, args) {

        }


        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:radcodeblock>

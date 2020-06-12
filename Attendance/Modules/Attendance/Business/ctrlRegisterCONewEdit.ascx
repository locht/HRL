<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRegisterCONewEdit.ascx.vb"
    Inherits="Attendance.ctrlRegisterCONewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidValid" runat="server" />
<asp:HiddenField ID="hidStatus" runat="server" />
<asp:Label runat="server" ID="lbStatus" ForeColor="Red"></asp:Label>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Orientation="Horizontal" Width="100%" Height="100%">    
  <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
    <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="35px">
       <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
    <tlk:RadPane ID="RadPane1" runat="server" Height="235px" Scrolling="None">
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td colspan="4">
                    <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <b style="color: red">
                        <%# Translate("Thông tin đăng ký nghỉ phép")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Loại nghỉ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboMANUAL_ID" Width="250px" DataTextField="NAME_VN"
                        DataValueField="ID" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Diễn giải")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="rtMANUAL_NOTE" Width="100%" Enabled="false">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server"  ID="rdLEAVE_FROM" >
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Từ buổi")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server"  ID="cboFROM_SESSION" Enabled="false" DataTextField="NAME_VN" DataValueField="ID">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server"  ID="rdLEAVE_TO" >
                    </tlk:RadDatePicker>
                    <input id="btnDetail" value="<%# Translate("Chi tiết")%>" type="button" style="display: none;"
                        onclick="showDetail('')">
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdLEAVE_TO"
                        Type="Date" ControlToCompare="rdLEAVE_FROM" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày kết thúc nghỉ phải lớn hơn ngày bắt đầu nghỉ %>"
                        ToolTip="<%$ Translate: Ngày kết thúc nghỉ phải lớn hơn ngày bắt đầu nghỉ %>"></asp:CompareValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến buổi")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server"  Enabled="false" ID="cboTO_SESSION" DataTextField="NAME_VN" DataValueField="ID">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lý do nghỉ phép")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboREASON_LEAVE" DataTextField="NAME_VN" DataValueField="ID">
                    </tlk:RadComboBox>
                </td>
                <td class="lb" style="display:none">
                    <%# Translate("Số ngày đăng ký nghỉ")%>
                </td>
                <td style="display:none">
                    <tlk:RadNumericTextBox runat="server" ID="rnDAY_NUM" ReadOnly="true" Width="50px">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lý do chi tiết")%><span class="lbReq">*</span>
                    
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="rtNote" CausesValidation="false" rtNOTE="MultiLine"
                        Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reNOTE" ControlToValidate="rtNote" runat="server"
                        ErrorMessage="<%$ Translate: Chưa nhập lý do nghỉ %>" ToolTip="<%$ Translate: Chưa nhập lý do nghỉ  %>"> </asp:RequiredFieldValidator>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnIS_APP" Value="0" Visible="false">
                    </tlk:RadNumericTextBox>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnSTATUS" Value="0" Visible="false">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
        </table>
      </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgEmployee" AllowPaging="false" AllowMultiRowEdit="true" runat="server" Height="100%">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView EditMode="InPlace" EditFormSettings-EditFormType="AutoGenerated"
                AllowPaging="false" AllowCustomPaging="false" DataKeyNames="ID,EMPLOYEE_CODE,ORG_ID,ORG_NAME,TITLE_ID,FULLNAME_VN,TITLE_NAME_VN,EMPLOYEE_OBJECT_CODE,EMPLOYEE_OBJECT,EMP_ENTITLEMENT"
                ClientDataKeyNames="ID,EMPLOYEE_CODE,ORG_ID,TITLE_ID,FULLNAME_VN,TITLE_NAME_VN,ORG_NAME,EMPLOYEE_OBJECT_CODE,EMPLOYEE_OBJECT,EMP_ENTITLEMENT"
                CommandItemDisplay="Top">
                <CommandItemStyle Height="25px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="150px" ID="btnEmployee" runat="server" Text="Chọn tất cả nhân viên"
                                CausesValidation="false" CommandName="FindEmployee" TabIndex="3">
                            </tlk:RadButton>
                        </div>
                        <div style="float: right">
                            <tlk:RadButton Width="100px" ID="btnDeleteEmp" runat="server" Text="Xóa" CausesValidation="false"
                                CommandName="DeleteEmployee" TabIndex="3">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" UniqueName="ID" SortExpression="ID" Visible="false"
                        ReadOnly="true" />
                    <tlk:GridBoundColumn HeaderText="MSNV" DataField="EMPLOYEE_CODE" HeaderStyle-Width="100px"
                        ReadOnly="true" UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="FULLNAME_VN" UniqueName="FULLNAME_VN"
                        HeaderStyle-Width="200px" ReadOnly="true" SortExpression="FULLNAME_VN" ItemStyle-HorizontalAlign="Left" />
                    <tlk:GridBoundColumn DataField="ORG_ID" UniqueName="ORG_ID" HeaderStyle-Width="200px"
                        ReadOnly="true" SortExpression="ORG_ID" Visible="false" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn DataField="EMPLOYEE_OBJECT_CODE" UniqueName="EMPLOYEE_OBJECT_CODE" HeaderStyle-Width="200px"
                        ReadOnly="true" SortExpression="EMPLOYEE_OBJECT_CODE" Visible="false" ItemStyle-HorizontalAlign="Left" />
                    <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        HeaderStyle-Width="200px" ReadOnly="true" SortExpression="ORG_NAME" ItemStyle-HorizontalAlign="Left" />
                    <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME_VN" UniqueName="TITLE_NAME_VN"
                        HeaderStyle-Width="200px" ReadOnly="true" SortExpression="TITLE_NAME_VN" ItemStyle-HorizontalAlign="Left" />
                    <tlk:GridBoundColumn HeaderText="Đối tượng nhân viên" DataField="EMPLOYEE_OBJECT_NAME" UniqueName="EMPLOYEE_OBJECT_NAME"
                        HeaderStyle-Width="200px" ReadOnly="true" SortExpression="EMPLOYEE_OBJECT_NAME" ItemStyle-HorizontalAlign="Left" />
                    <tlk:GridNumericColumn HeaderText="Phép năm còn lại" DataField="EMP_ENTITLEMENT" UniqueName="EMP_ENTITLEMENT"
                        HeaderStyle-Width="200px" ReadOnly="true" SortExpression="EMP_ENTITLEMENT" ItemStyle-HorizontalAlign="Right" />
                    <tlk:GridBoundColumn HeaderText="Quỹ nghỉ bù còn lại" DataField="A" UniqueName="A"
                        HeaderStyle-Width="200px" ReadOnly="true" SortExpression="A" ItemStyle-HorizontalAlign="Right" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
    </tlk:RadSplitter>
  </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
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
            //if (args.get_item().get_commandName() == "CANCEL") {
            //    OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlLeaveRegistration');
            //    args.set_cancel(true);
            //}
        }
        function OpenInNewTab(url) {
            window.location.href = url;
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
        function showDetail(value) {
            if (value == "")
                if ($("#divLeaveDetail").css("display") == "block")
                    $("#divLeaveDetail").css("display", "none");
                else
                    $("#divLeaveDetail").css("display", "block");
            else
                $("#divLeaveDetail").css("display", value);
        }
        function valueChange(sender, args) {
        }

        function IsBlock() {
            $("#divLeaveDetail").css("display", "block");
        }
    </script>
</tlk:RadCodeBlock>
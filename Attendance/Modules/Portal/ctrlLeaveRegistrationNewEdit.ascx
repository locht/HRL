<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlLeaveRegistrationNewEdit.ascx.vb"
    Inherits="Attendance.ctrlLeaveRegistrationNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidValid" runat="server" />
<asp:HiddenField ID="hidStatus" runat="server" />
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<asp:Label runat="server" ID="lbStatus" ForeColor="Red"></asp:Label>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Orientation="Horizontal" Width="100%">
    <tlk:RadPane ID="RadPane1" runat="server" Height="100%" Scrolling="Y">
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
                    <tlk:RadTextBox runat="server" ID="txtManual_Note" Width="100%" Enabled="false">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" AutoPostBack="true" ID="rdLEAVE_FROM" CausesValidation="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Từ buổi")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" AutoPostBack="true" CausesValidation="false" ID="cboFROM_SESSION" Enabled="false" DataTextField="NAME_VN" DataValueField="ID">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" AutoPostBack="true" ID="rdLEAVE_TO" CausesValidation="false">
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
                    <tlk:RadComboBox runat="server" AutoPostBack="true" CausesValidation="false" Enabled="false" ID="cboTO_SESSION" DataTextField="NAME_VN" DataValueField="ID">
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
                <td class="lb">
                    <%# Translate("Số ngày đăng ký nghỉ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnDAY_NUM" ReadOnly="true" Width="50px">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lý do chi tiết")%>
                    
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
        <div id="divLeaveDetail" style="display: none">
            <tlk:RadGrid PageSize="500" runat="server" ID="rgData" AllowMultiRowEdit="true" Width="70%">
                <MasterTableView DataKeyNames="MANUAL_ID,STATUS_SHIFT,DAY_NUM,SHIFT_NAME,SHIFT_DAY,LEAVE_DAY,EMPLOYEE_ID"
                    ClientDataKeyNames="MANUAL_ID,STATUS_SHIFT,DAY_NUM,SHIFT_NAME,SHIFT_DAY,LEAVE_DAY,EMPLOYEE_ID"
                    EditMode="InPlace" EditFormSettings-EditFormType="AutoGenerated">
                    <CommandItemStyle Height="28px" />
                    <Columns>
                        <tlk:GridBoundColumn HeaderText='<%$ Translate: EMPLOYEE_ID %>' DataField="EMPLOYEE_ID"
                            Visible="false" UniqueName="EMPLOYEE_ID" SortExpression="EMPLOYEE_ID" ReadOnly="true"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="145px" />
                            <HeaderStyle Width="150px" />
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText='<%$ Translate: Ngày nghỉ %>' DataField="LEAVE_DAY"
                            UniqueName="LEAVE_DAY" SortExpression="LEAVE_DAY" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="145px" />
                            <HeaderStyle Width="150px" />
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại nghỉ%>" DataField="MANUAL_ID"
                            AllowSorting="false" ColumnGroupName="MANUAL_ID" UniqueName="MANUAL_ID" SortExpression="MANUAL_ID"
                            ReadOnly="true" Visible="false" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại nghỉ%>" DataField="MANUAL_NAME"
                            AllowSorting="false" ColumnGroupName="MANUAL_NAME" UniqueName="MANUAL_NAME" SortExpression="MANUAL_NAME"
                            ReadOnly="true" />
                        <tlk:GridTemplateColumn HeaderText="Đầu ca/cuối ca" HeaderStyle-Width="150px" UniqueName="STATUS_SHIFT">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "STATUS_SHIFT_NAME")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <tlk:RadComboBox Width="125px" runat="server" ID="cboSTATUS_SHIFT" ReadOnly="true"
                                    AutoPostBack="true" CausesValidation="false">
                                </tlk:RadComboBox>
                            </EditItemTemplate>
                        </tlk:GridTemplateColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Số ngày nghỉ%>" DataField="DAY_NUM"
                            AllowSorting="false" ColumnGroupName="DAY_NUM" UniqueName="DAY_NUM" SortExpression="DAY_NUM"
                            ReadOnly="true" DataFormatString="{0:N2}" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca làm việc%>" DataField="SHIFT_NAME"
                            AllowSorting="false" ColumnGroupName="SHIFT_NAME" UniqueName="SHIFT_NAME" SortExpression="SHIFT_NAME"
                            ReadOnly="true" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên NV%>" DataField="SHIFT_ID"
                            AllowSorting="false" ColumnGroupName="SHIFT_ID" UniqueName="SHIFT_ID" SortExpression="SHIFT_ID"
                            ReadOnly="true" Visible="false" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày công ca%>" DataField="SHIFT_DAY"
                            AllowSorting="false" ColumnGroupName="SHIFT_DAY" UniqueName="SHIFT_DAY" SortExpression="SHIFT_DAY"
                            ReadOnly="true" DataFormatString="{0:N2}" />
                    </Columns>
                </MasterTableView>
            </tlk:RadGrid>
        </div>
        <tlk:RadGrid ID="rgEmployee" AllowPaging="false" AllowMultiRowEdit="true" runat="server">
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
                        HeaderStyle-Width="200px" ReadOnly="true" SortExpression="FULLNAME_VN" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn DataField="ORG_ID" UniqueName="ORG_ID" HeaderStyle-Width="200px"
                        ReadOnly="true" SortExpression="ORG_ID" Visible="false" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn DataField="EMPLOYEE_OBJECT_CODE" UniqueName="EMPLOYEE_OBJECT_CODE" HeaderStyle-Width="200px"
                        ReadOnly="true" SortExpression="EMPLOYEE_OBJECT_CODE" Visible="false" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        HeaderStyle-Width="200px" ReadOnly="true" SortExpression="ORG_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME_VN" UniqueName="TITLE_NAME_VN"
                        HeaderStyle-Width="200px" ReadOnly="true" SortExpression="TITLE_NAME_VN" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Đối tượng nhân viên" DataField="EMPLOYEE_OBJECT_NAME" UniqueName="EMPLOYEE_OBJECT_NAME"
                        HeaderStyle-Width="200px" ReadOnly="true" SortExpression="EMPLOYEE_OBJECT_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridNumericColumn HeaderText="Phép năm còn lại" DataField="EMP_ENTITLEMENT" UniqueName="EMP_ENTITLEMENT"
                        HeaderStyle-Width="200px" ReadOnly="true" SortExpression="EMP_ENTITLEMENT" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Quỹ nghỉ bù còn lại" DataField="A" UniqueName="A"
                        HeaderStyle-Width="200px" ReadOnly="true" SortExpression="A" ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
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

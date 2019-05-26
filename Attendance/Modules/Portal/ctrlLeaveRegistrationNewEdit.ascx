<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlLeaveRegistrationNewEdit.ascx.vb"
    Inherits="Attendance.ctrlLeaveRegistrationNewEdit" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidValid" runat="server" />
<asp:HiddenField ID="hidStatus" runat="server" />
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<asp:Label runat="server" ID="lbStatus" ForeColor="Red"></asp:Label>
<table class="table-form">
    <tr>
        <td colspan="4">
            <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <b style="color: red">
                <%# Translate("Thông tin nhân viên")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Họ tên")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtFullName" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Phòng ban")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtDepartment" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Mã nhân viên")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtEmpCode" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Chức danh")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtTitle" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <b style="color: red">
                <%# Translate("Thông tin phép năm")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Phép chế độ")%>
        </td>
        <td>
            <tlk:RadNumericTextBox SkinID="Decimal" runat="server"
                ID="rntEntitlement" ReadOnly="true">
            </tlk:RadNumericTextBox>
        </td>
        <td class="lb">
            <%# Translate("Phép đã nghĩ")%>
        </td>
        <td>
            <tlk:RadNumericTextBox SkinID="Decimal" runat="server"
             ID="rntSeniority" ReadOnly="true">
            </tlk:RadNumericTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Phép thâm niên")%>
        </td>
        <td>
            <tlk:RadNumericTextBox SkinID="Decimal" runat="server"
                 ID="rntBrought" ReadOnly="true">
            </tlk:RadNumericTextBox>
        </td>
        <td class="lb">
            <%# Translate("Phép trừ quy đổi từ số phút ngoài cơ quan")%>
        </td>
        <td>
            <tlk:RadNumericTextBox SkinID="Decimal" runat="server" 
              ID="rntTotal" ReadOnly="true">
            </tlk:RadNumericTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Phép năm trước còn lại")%>
        </td>
        <td>
            <tlk:RadNumericTextBox SkinID="Decimal" runat="server" 
               ID="rntTotalTaken" ReadOnly="true">
            </tlk:RadNumericTextBox>
        </td>
        <td class="lb">
            <%# Translate("Phép còn lại")%>
        </td>
        <td>
            <tlk:RadNumericTextBox SkinID="Decimal" runat="server" 
                 ID="rntBalance" ReadOnly="true">
            </tlk:RadNumericTextBox>
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
            <tlk:RadComboBox runat="server" ID="cboleaveType" Width="250px" DataTextField="NAME_VN"
                DataValueField="ID" AutoPostBack="TRUE">
            </tlk:RadComboBox>
      
        </td>
        <td class="lb">
            <%# Translate("Số ngày")%>
        </td>
        <td>
            <tlk:RadNumericTextBox runat="server" ID="rntxDayRegist" ReadOnly="true" Visible="false">
            </tlk:RadNumericTextBox>
            <tlk:RadTextBox runat="server" ID="txtDayRegist" ReadOnly="true">
            </tlk:RadTextBox>
            <input id="btnDetail" value="<%# Translate("Chi tiết")%>" type="button" onclick="showDetail('')">
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Thời gian bắt đầu nghỉ")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdFromDate" AutoPostBack="true">
            </tlk:RadDatePicker>
          
        </td>
        <td class="lb">
            <%# Translate("Thời gian kết thúc nghỉ")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdToDate" AutoPostBack="true">
            </tlk:RadDatePicker>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdToDate"
                Type="Date" ControlToCompare="rdFromDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày kết thúc nghỉ phải lớn hơn ngày bắt đầu nghỉ %>"
                ToolTip="<%$ Translate: Ngày kết thúc nghỉ phải lớn hơn ngày bắt đầu nghỉ %>"></asp:CompareValidator>
        </td>
        <td>
            <asp:CheckBox runat="server" ID="chkWorkday" Text="Ngày làm việc" AutoPostBack="true" />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Số ngày trong kế hoạch")%>
        </td>
        <td>
            <tlk:RadNumericTextBox SkinID="NUMBER" runat="server" 
                Culture="en-GB" ID="rtxtdayinkh" ReadOnly="true">
            </tlk:RadNumericTextBox>
        </td>
        <td class="lb">
            <%# Translate("Số ngày ngoài kế hoạch")%>
        </td>
        <td>
            <tlk:RadNumericTextBox SkinID="NUMBER" runat="server" 
                Culture="en-GB" ID="rtxtdayoutkh" ReadOnly="true">
            </tlk:RadNumericTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Lý do nghỉ phép")%>
            <span class="lbReq">*</span>
        </td>
        <td colspan="3">
            <tlk:RadTextBox runat="server" ID="txtNote" TextMode="MultiLine" Width="100%">
            </tlk:RadTextBox>
        </td>
    </tr>
</table>
<div id="divLeaveDetail" style="display: none">
    <tlk:RadGrid PageSize="500" runat="server" ID="rgData" AllowMultiRowEdit="true" Width="65%">
        <MasterTableView DataKeyNames="EFFECTIVEDATE,LEAVE_VALUE,IS_UPDATE,IS_OFF" ClientDataKeyNames="EFFECTIVEDATE,LEAVE_NAME,IS_UPDATE,IS_OFF"
            EditMode="InPlace" CommandItemDisplay="Top">
            <CommandItemStyle Height="28px" />
            <CommandItemTemplate>
                <div style="padding: 2px 0 0 0">
                    <div style="float: left">
                        <tlk:RadButton ID="btnEditDetail" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/add.png"
                            CausesValidation="false" Width="70px" Text="<%$ Translate: Edit %>" CommandName="EditDetail"
                            Visible="false">
                        </tlk:RadButton>
                    </div>
                </div>
            </CommandItemTemplate>
            <Columns>
                <tlk:GridBoundColumn HeaderText='<%$ Translate: Ngày nghỉ %>' DataField="EFFECTIVEDATE"
                    UniqueName="EFFECTIVEDATE" SortExpression="EFFECTIVEDATE" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemStyle Width="50%" />
                    <HeaderStyle Width="50%" />
                </tlk:GridBoundColumn>
                <%--<tlk:GridNumericColumn HeaderText='<%$ Translate: Giá trị nghỉ %>' DataField="LEAVE_VALUE" UniqueName="LEAVE_VALUE" SortExpression="LEAVE_VALUE" >
                    <ItemStyle Width="30%" />
                    <HeaderStyle Width="30%" />
                </tlk:GridNumericColumn>--%>
                <tlk:GridTemplateColumn UniqueName="LEAVE_VALUE" HeaderText="<%$ Translate: Giá trị nghỉ %>"
                    SortExpression="LEAVE_VALUE">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "LEAVE_NAME")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <tlk:RadComboBox runat="server" ID="cboLeaveValue" Width="100%" OnClientSelectedIndexChanged="valueChange">
                        </tlk:RadComboBox>
                    </EditItemTemplate>
                </tlk:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </tlk:RadGrid>
</div>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

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
            debugger;
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
    </script>
</tlk:RadCodeBlock>

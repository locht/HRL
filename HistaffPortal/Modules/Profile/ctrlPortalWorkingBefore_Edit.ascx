<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalWorkingBefore_Edit.ascx.vb"
    Inherits="Profile.ctrlPortalWorkingBefore_Edit" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidWorkingID" runat="server" />
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<div style="overflow: auto">
    <table class="table-form">
        <tr>
            <td class="lb" style="width: 130px">
                <%# Translate("Đơn vị công tác") %><span class="lbReq">*</span>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtCompanyName">
                </tlk:RadTextBox>
                <asp:RequiredFieldValidator ID="reqCompanyName" ControlToValidate="txtCompanyName"
                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên công ty %>" ToolTip="<%$ Translate: Bạn phải nhập tên công ty  %>">
                </asp:RequiredFieldValidator>
            </td>
            <td class="lb" style="width: 130px">
                <%# Translate("Số điện thoại")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtTelephone">
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Địa chỉ công ty") %>
            </td>
            <td colspan="3">
                <tlk:RadTextBox runat="server" ID="txtCompanyAddress" Width="100%">
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Ngày vào")%>
            </td>
            <td>
                <tlk:RadDatePicker ID="rdJoinDate" runat="server">
                </tlk:RadDatePicker>
            </td>
            <td class="lb">
                <%# Translate("Ngày nghỉ")%>
            </td>
            <td>
                <tlk:RadDatePicker ID="rdEndDate" runat="server">
                </tlk:RadDatePicker>
                <asp:CompareValidator ID="compare_JoinDate_EndDate" runat="server" ErrorMessage="<%$ Translate: Ngày nghỉ phải sau ngày vào %>"
                    ControlToCompare="rdJoinDate" ControlToValidate="rdEndDate" ToolTip="<%$ Translate: Ngày nghỉ phải sau ngày vào %>"
                    Type="Date" Operator="GreaterThan"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Mức lương")%>
            </td>
            <td>
                <tlk:RadNumericTextBox runat="server" ID="txtSalary" MinValue="0" MaxLength="9" NumberFormat-DecimalDigits="0">
                </tlk:RadNumericTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Vị trí công việc")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtTitleName">
                </tlk:RadTextBox>
            </td>
            <td class="lb">
                <%# Translate("Công việc chính")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtMainJobName" >
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Lý do nghỉ việc") %>
            </td>
            <td colspan="3">
                <tlk:RadTextBox runat="server" ID="txtTerReason" SkinID="Textbox1023" Width="100%">
                </tlk:RadTextBox>
            </td>
        </tr>
    </table>
    <tlk:RadGrid PageSize=50 ID="rgWorkingBeforeEdit" runat="server" Height="250px" Width="99%">
        <MasterTableView DataKeyNames="ID,COMPANY_NAME,JOIN_DATE,END_DATE,COMPANY_ADDRESS,MAIN_JOB,TELEPHONE,SALARY,TITLE_NAME,LEVEL_NAME,TER_REASON,FK_PKEY,REASON_UNAPROVE,STATUS,REASON_UNAPROVE"
                         ClientDataKeyNames = "ID,COMPANY_NAME,JOIN_DATE,END_DATE,MAIN_JOB,COMPANY_ADDRESS,TELEPHONE,SALARY,TITLE_NAME,LEVEL_NAME,TER_REASON,FK_PKEY,REASON_UNAPROVE,STATUS,REASON_UNAPROVE"
            Caption="<%$ Translate: Thông tin chỉnh sửa %>">
            <Columns>
                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                </tlk:GridClientSelectColumn>
                <tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>
                <tlk:GridBoundColumn HeaderText="Đơn vị công tác" DataField="COMPANY_NAME" UniqueName="COMPANY_NAME"
                SortExpression="COMPANY_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Số điện thoại" DataField="TELEPHONE"
                UniqueName="TELEPHONE" SortExpression="TELEPHONE" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
           <tlk:GridBoundColumn HeaderText="Địa chỉ công ty" DataField="COMPANY_ADDRESS"
                UniqueName="COMPANY_ADDRESS" SortExpression="COMPANY_ADDRESS" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày vào công ty" DataField="JOIN_DATE" UniqueName="JOIN_DATE"
                DataFormatString="{0:MM/yyyy}" SortExpression="JOIN_DATE" ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày nghỉ việc" DataField="END_DATE" UniqueName="END_DATE"
                DataFormatString="{0:MM/yyyy}" SortExpression="END_DATE" ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
              <tlk:GridBoundColumn HeaderText="Mức lương" DataField="SALARY" UniqueName="SALARY"
                SortExpression="SALARY" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
              <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                SortExpression="TITLE_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Công việc chính" DataField="MAIN_JOB" UniqueName="MAIN_JOB"
                SortExpression="MAIN_JOB" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Lý do nghỉ việc" DataField="TER_REASON" UniqueName="TER_REASON"
                SortExpression="TER_REASON" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Lý do không duyệt" DataField="REASON_UNAPROVE" UniqueName="REASON_UNAPROVE"
                SortExpression="REASON_UNAPROVE" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="STATUS_NAME" UniqueName="STATUS_NAME"
                SortExpression="STATUS_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn> 
            </Columns>
        </MasterTableView>
    </tlk:RadGrid>
    <br />
    <tlk:RadGrid PageSize=50 ID="rgWorkingBefore" runat="server" Height="250px" Width="99%">
        <MasterTableView DataKeyNames="ID,COMPANY_NAME,JOIN_DATE,END_DATE,COMPANY_ADDRESS,TELEPHONE,SALARY,TITLE_NAME,LEVEL_NAME,TER_REASON"
                         ClientDataKeyNames ="ID,COMPANY_NAME,JOIN_DATE,END_DATE,COMPANY_ADDRESS,TELEPHONE,SALARY,TITLE_NAME,LEVEL_NAME,TER_REASON"
            Caption="<%$ Translate: Thông tin hiện tại %>">
            <Columns>
                <tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow">
                    <HeaderStyle Wrap="False" Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>
                 <tlk:GridBoundColumn HeaderText="Đơn vị công tác" DataField="COMPANY_NAME" UniqueName="COMPANY_NAME"
                SortExpression="COMPANY_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Số điện thoại" DataField="TELEPHONE"
                UniqueName="TELEPHONE" SortExpression="TELEPHONE" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
           <tlk:GridBoundColumn HeaderText="Địa chỉ công ty" DataField="COMPANY_ADDRESS"
                UniqueName="COMPANY_ADDRESS" SortExpression="COMPANY_ADDRESS" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày vào công ty" DataField="JOIN_DATE" UniqueName="JOIN_DATE"
                DataFormatString="{0:MM/yyyy}" SortExpression="JOIN_DATE" ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày nghỉ việc" DataField="END_DATE" UniqueName="END_DATE"
                DataFormatString="{0:MM/yyyy}" SortExpression="END_DATE" ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
              <tlk:GridBoundColumn HeaderText="Mức lương" DataField="SALARY" UniqueName="SALARY"
                SortExpression="SALARY" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
              <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                SortExpression="TITLE_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Công việc chính" DataField="MAIN_JOB" UniqueName="MAIN_JOB"
                SortExpression="MAIN_JOB" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Lý do nghỉ việc" DataField="TER_REASON" UniqueName="TER_REASON"
                SortExpression="TER_REASON" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Lý do không duyệt" DataField="REASON_UNAPROVE" UniqueName="REASON_UNAPROVE"
                SortExpression="REASON_UNAPROVE" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="STATUS_NAME" UniqueName="STATUS_NAME"
                SortExpression="STATUS_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn> 
            </Columns>
        </MasterTableView>
    </tlk:RadGrid>
    <tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function clientButtonClicking(sender, args)
            {
               
            }

           
        </script>
    </tlk:RadCodeBlock>
</div>


<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />

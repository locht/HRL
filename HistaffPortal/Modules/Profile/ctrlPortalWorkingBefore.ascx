<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalWorkingBefore.ascx.vb"
    Inherits="Profile.ctrlPortalWorkingBefore" %>
<tlk:RadGrid PageSize="50" ID="rgWorkingBefore" runat="server" Height="350px" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,COMPANY_NAME,COMPANY_ADDRESS,JOIN_DATE,END_DATE,TITLE_NAME,LEVEL_NAME">
        <Columns>
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
            <tlk:GridBoundColumn HeaderText="Công việc chính" DataField="LEVEL_NAME" UniqueName="LEVEL_NAME"
                SortExpression="LEVEL_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Lý do nghỉ việc" DataField="TER_REASON" UniqueName="TER_REASON"
                SortExpression="TER_REASON" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Lý do không duyệt" DataField="NOT_APPROVE_REASON" UniqueName="NOT_APPROVE_REASON"
                SortExpression="NOT_APPROVE_REASON" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="STATUS" UniqueName="STATUS"
                SortExpression="STATUS" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>            
            <%-- <tlk:GridBoundColumn HeaderText="Cấp bậc" DataField="LEVEL_NAME"
                UniqueName="LEVEL_NAME" SortExpression="LEVEL_NAME"
                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                FilterControlWidth="100%">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>--%>
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
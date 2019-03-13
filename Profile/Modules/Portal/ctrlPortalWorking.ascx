<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalWorking.ascx.vb"
    Inherits="Profile.ctrlPortalWorking" %>
<tlk:RadGrid PageSize=50 ID="rgWorking" runat="server" Height="350px" Width="100%" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="ID">
        <Columns>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại quyết định %>" DataField="DECISION_TYPE_NAME"
                UniqueName="DECISION_TYPE_NAME" SortExpression="DECISION_TYPE_NAME" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE"
                ShowFilterIcon="true ">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="EXPIRE_DATE"
                UniqueName="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EXPIRE_DATE"
                ShowFilterIcon="true ">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" DataField="STAFF_RANK_NAME"
                UniqueName="STAFF_RANK_NAME" SortExpression="STAFF_RANK_NAME" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                SortExpression="ORG_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày ký %>" DataField="SIGN_DATE"
                UniqueName="SIGN_DATE" SortExpression="SIGN_DATE" ShowFilterIcon="true" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người ký %>" DataField="SIGN_NAME"
                UniqueName="SIGN_NAME" SortExpression="SIGN_NAME" ShowFilterIcon="true" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh người ký %>" DataField="SIGN_TITLE"
                UniqueName="SIGN_TITLE" SortExpression="SIGN_TITLE" ShowFilterIcon="true" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
<tlk:RadGrid PageSize=50 ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True"
    Height="250px" AllowSorting="True" AllowMultiRowSelection="true" Width="100%">
    <ClientSettings EnableRowHoverStyle="true">
        <Selecting AllowRowSelect="true" />
    </ClientSettings>
    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="NAME,EFFECT_DATE,EXPIRE_DATE"
        Caption="<%$ Translate: Chức danh kiêm nhiệm %>">
        <Columns>
            <tlk:GridBoundColumn DataField="ID" Visible="false" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="NAME" UniqueName="NAME"
                SortExpression="NAME" ItemStyle-HorizontalAlign="Center" />
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}"
                ItemStyle-HorizontalAlign="Center" />
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}"
                ItemStyle-HorizontalAlign="Center" />
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalContract.ascx.vb"
    Inherits="Profile.ctrlPortalContract" %>
<tlk:RadGrid PageSize=50 ID="rgContract" runat="server" Height="350px" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="ID">
        <Columns>
            <%--<tlk:GridBoundColumn HeaderText="Loại hợp đồng" DataField="CONTRACTTYPE_NAME"
                UniqueName="CONTRACTTYPE_NAME" SortExpression="CONTRACTTYPE_NAME" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Số hợp đồng" DataField="CONTRACT_NO"
                UniqueName="CONTRACT_NO" SortExpression="CONTRACT_NO" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày bắt đầu" DataField="START_DATE"
                UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="START_DATE"
                ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày kết thúc" DataField="EXPIRE_DATE"
                UniqueName="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EXPIRE_DATE"
                ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridBoundColumn HeaderText="Người ký" DataField="SIGNER_NAME"
                UniqueName="SIGNER_NAME" SortExpression="SIGNER_NAME" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày ký" DataField="SIGN_DATE"
                UniqueName="SIGN_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="SIGN_DATE"
                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                FilterControlWidth="100%">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridBoundColumn HeaderText="Chức danh người ký" DataField="SIGNER_TITLE"
                UniqueName="SIGNER_TITLE" SortExpression="SIGNER_TITLE" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>--%>
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
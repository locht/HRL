<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalCommend.ascx.vb"
    Inherits="Profile.ctrlPortalCommend" %>
<tlk:RadGrid PageSize=50 ID="rgCommend" runat="server" Height="350px" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="ID">
        <Columns>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số quyết định %>" DataField="DECISION_NO"
                UniqueName="DECISION_NO" SortExpression="DECISION_NO" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE"
                ShowFilterIcon="true">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
          <%--  <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>--%>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp khen thưởng %>" DataField="COMMEND_LEVEL_NAME"
                UniqueName="COMMEND_LEVEL_NAME" SortExpression="COMMEND_LEVEL_NAME" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức khen thưởng %>" DataField="COMMEND_TYPE_NAME"
                UniqueName="COMMEND_TYPE_NAME" SortExpression="COMMEND_TYPE_NAME" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung khen thưởng %>" DataField="REMARK"
                UniqueName="REMARK" SortExpression="REMARK" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="MONEY" UniqueName="MONEY"
                SortExpression="MONEY" DataFormatString="{0:###,###,###,##0}" ShowFilterIcon="true">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
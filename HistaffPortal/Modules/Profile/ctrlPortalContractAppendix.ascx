<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalContractAppendix.ascx.vb"
    Inherits="Profile.ctrlPortalContractAppendix" %>
<tlk:RadGrid ID="rgGrid" runat="server" Height="350px" AllowFilteringByColumn="true" Scrolling="both">
    <MasterTableView DataKeyNames="ID">
        <Columns>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME1"
                UniqueName="ORG_NAME1" SortExpression="ORG_NAME1" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số HĐLĐ %>" DataField="CONTRACT_NO"
                UniqueName="CONTRACT_NO" SortExpression="CONTRACT_NO" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại hợp đồng %>" DataField="CONTRACTTYPE_NAME"
                UniqueName="CONTRACTTYPE_NAME" SortExpression="CONTRACTTYPE_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số phụ lục hợp đồng %>" DataField="APPEND_NUMBER"
                UniqueName="APPEND_NUMBER" SortExpression="APPEND_NUMBER" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại phụ lục hợp đồng %>" DataField="APPEND_TYPE_NAME"
                UniqueName="APPEND_TYPE_NAME" SortExpression="APPEND_TYPE_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày bắt đầu %>" DataField="START_DATE" UniqueName="START_DATE"
                ReadOnly="true" SortExpression="START_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                ReadOnly="true" SortExpression="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày ký %>" DataField="SIGN_DATE" UniqueName="SIGN_DATE"
                ReadOnly="true" SortExpression="SIGN_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh người ký %>" DataField="SIGN_TITLE_NAME"
                UniqueName="SIGN_TITLE_NAME" SortExpression="SIGN_TITLE_NAME" />

            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Loại quyết định %>" DataField="DECISION_TYPE_NAME"
                UniqueName="DECISION_TYPE_NAME" SortExpression="DECISION_TYPE_NAME" />--%>
            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Người ký %>" DataField="SIGN_NAME"
                UniqueName="SIGN_NAME" SortExpression="SIGN_NAME" />--%>
        </Columns>
    </MasterTableView>
</tlk:RadGrid>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalWorking.ascx.vb"
    Inherits="Profile.ctrlPortalWorking" %>
<tlk:RadGrid PageSize="50" ID="rgWorking" runat="server" Height="350px" Width="100%" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="ID">
        <Columns>
            <tlk:GridBoundColumn HeaderText="Loại quyết định" DataField="DECISION_TYPE_NAME"
                UniqueName="DECISION_TYPE_NAME" SortExpression="DECISION_TYPE_NAME">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME" UniqueName="ORG_NAME"
                SortExpression="ORG_NAME">
            </tlk:GridBoundColumn>
           <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME"
                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Loại hình lao động" DataField="OBJECT_LABOR_TITLE"
                UniqueName="OBJECT_LABOR_TITLE" SortExpression="OBJECT_LABOR_TITLE">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Người quản lý trực tiếp" DataField="DIRECT_MANAGER_NAME"
                UniqueName="DIRECT_MANAGER_NAME" SortExpression="DIRECT_MANAGER_NAME">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO"
                UniqueName="DECISION_NO" SortExpression="DECISION_NO">
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}">
            </tlk:GridDateTimeColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày hết hiệu lực" DataField="EXPIRE_DATE"
                UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}">
            </tlk:GridDateTimeColumn>
            <tlk:GridBoundColumn HeaderText="Đơn vị ký quyết định " DataField="SIGN_ORG_NAME"
                UniqueName="SIGN_ORG_NAME" SortExpression="SIGN_ORG_NAME">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Người ký" DataField="SIGN_NAME"
                UniqueName="SIGN_NAME" SortExpression="SIGN_NAME">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Chức danh người ký" DataField="SIGN_TITLE"
                UniqueName="SIGN_TITLE" SortExpression="SIGN_TITLE">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE"
                UniqueName="NOTE" SortExpression="NOTE">
            </tlk:GridBoundColumn>
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
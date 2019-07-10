<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalWorking.ascx.vb"
    Inherits="Profile.ctrlPortalWorking" %>
<tlk:RadGrid PageSize="50" ID="rgWorking" runat="server" Height="350px" Width="100%" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="ID">
        <Columns>
            <tlk:GridBoundColumn HeaderText="Loại quyết định" DataField="DECISION_TYPE_NAME"
                UniqueName="DECISION_TYPE_NAME" SortExpression="DECISION_TYPE_NAME">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" UniqueName="ORG_NAME"
                SortExpression="ORG_NAME">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME"
                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Đối tượng lao động" DataField="OBJECT_LABOR_TITLE"
                UniqueName="OBJECT_LABOR_TITLE" SortExpression="OBJECT_LABOR_TITLE">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO"
                UniqueName="DECISION_NO" SortExpression="DECISION_NO">
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE">
            </tlk:GridDateTimeColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày hết hiệu lực" DataField="EXPIRE_DATE"
                UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE">
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
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
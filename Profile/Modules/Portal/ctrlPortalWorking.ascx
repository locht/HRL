<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalWorking.ascx.vb"
    Inherits="Profile.ctrlPortalWorking" %>
<tlk:RadGrid PageSize="50" ID="rgWorking" runat="server" Height="350px" Width="100%" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="ID">
        <Columns>
            <tlk:GridBoundColumn HeaderText="Loại quyết định" DataField="DECISION_TYPE_NAME"
                UniqueName="DECISION_TYPE_NAME" SortExpression="DECISION_TYPE_NAME">
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}">
            </tlk:GridDateTimeColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày hết hiệu lực" DataField="EXPIRE_DATE"
                UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}">
            </tlk:GridDateTimeColumn>
            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME"
                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME" UniqueName="ORG_NAME"
                SortExpression="ORG_NAME">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO"
                UniqueName="DECISION_NO" SortExpression="DECISION_NO">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Ngày ký" DataField="Sign_Date"
                UniqueName="Sign_Date" SortExpression="Sign_Date">
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
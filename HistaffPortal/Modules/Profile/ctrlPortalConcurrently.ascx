<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalConcurrently.ascx.vb"
    Inherits="Profile.ctrlPortalConcurrently" %>
<tlk:RadGrid PageSize="50" ID="rgDiscipline" runat="server" Height="350px" AllowFilteringByColumn="true" Scrolling="Both">
    <MasterTableView DataKeyNames="ID">
          <Columns>
              <%-- <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="ORG_ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="ORG_NAME" Visible="false" />
                    <tlk:GridBoundColumn DataField="TITLE_ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        SortExpression="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="Chức danh" DataField="NAME" UniqueName="NAME" SortExpression="NAME" />
                    <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO" UniqueName="DECISION_NO"
                        SortExpression="DECISION_NO" />
                    <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE" UniqueName="EFFECT_DATE"
                        SortExpression="EFFECT_DATE" />
                    <tlk:GridDateTimeColumn HeaderText="Ngày hết hiệu lực" DataField="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                        SortExpression="EXPIRE_DATE" />
                    <tlk:GridDateTimeColumn HeaderText="Ghi chú" DataField="NOTE" UniqueName="NOTE" SortExpression="NOTE" />--%>
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
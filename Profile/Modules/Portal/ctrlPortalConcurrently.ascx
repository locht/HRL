<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalConcurrently.ascx.vb"
    Inherits="Profile.ctrlPortalConcurrently" %>
<tlk:RadGrid PageSize="50" ID="rgDiscipline" runat="server" Height="350px" AllowFilteringByColumn="true" Scrolling="Both">
    <MasterTableView DataKeyNames="ID">
        <Columns>
            <tlk:GridBoundColumn DataField="ID" Visible="false" />
            <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false" />
            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px"   Visible="false"/>
            <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="FULLNAME_VN"
                SortExpression="FULLNAME_VN" UniqueName="FULLNAME_VN" HeaderStyle-Width="150px"  Visible="false" />
            <tlk:GridBoundColumn HeaderText="Đơn vị kiêm nhiệm" DataField="ORG_CON_NAME"
                SortExpression="ORG_CON_NAME" UniqueName="ORG_CON_NAME" HeaderStyle-Width="150px" />
            <tlk:GridBoundColumn HeaderText="Chức danh kiêm nhiệm" DataField="TITLE_CON_NAME"
                SortExpression="TITLE_CON_NAME" UniqueName="TITLE_CON_NAME" HeaderStyle-Width="150px" />
            <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE_CON" HeaderStyle-Width="150px"
                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE_CON" UniqueName="EFFECT_DATE_CON"
                DataFormatString="{0:dd/MM/yyyy}">
            </tlk:GridDateTimeColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày hết hiệu lực" DataField="EXPIRE_DATE_CON" HeaderStyle-Width="150px"
                ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE_CON" UniqueName="EXPIRE_DATE_CON"
                DataFormatString="{0:dd/MM/yyyy}">
            </tlk:GridDateTimeColumn>
            <tlk:GridBoundColumn HeaderText="Phụ cấp kiêm nhiệm" DataField="ALLOW_MONEY_NUMBER" HeaderStyle-Width="150px"
                SortExpression="ALLOW_MONEY_NUMBER" UniqueName="ALLOW_MONEY_NUMBER" DataFormatString="{0:N0}" />
            <tlk:GridBoundColumn HeaderText="Người ký quyết định kiêm nhiệm 1" DataField="SIGN_NAME"
                SortExpression="SIGN_NAME" UniqueName="SIGN_NAME" HeaderStyle-Width="120px" />
            <tlk:GridBoundColumn HeaderText="Chức danh quyết định kiêm nhiệm 1" DataField="SIGN_TITLE_NAME"
                SortExpression="SIGN_TITLE_NAME" UniqueName="SIGN_TITLE_NAME" HeaderStyle-Width="120px" />
            <tlk:GridBoundColumn HeaderText="Người ký quyết định kiêm nhiệm 2" DataField="SIGN_NAME2"
                SortExpression="SIGN_NAME2" UniqueName="SIGN_NAME2" HeaderStyle-Width="120px" />
            <tlk:GridBoundColumn HeaderText="Chức danh quyết định kiêm nhiệm 2" DataField="SIGN_TITLE_NAME2"
                SortExpression="SIGN_TITLE_NAME2" UniqueName="SIGN_TITLE_NAME2" HeaderStyle-Width="120px" />
            <tlk:GridDateTimeColumn HeaderText="Ngày thôi kiêm nhiệm" DataField="EFFECT_DATE_STOP" HeaderStyle-Width="150px"
                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE_STOP" UniqueName="EFFECT_DATE_STOP"
                DataFormatString="{0:dd/MM/yyyy}">
            </tlk:GridDateTimeColumn>
            <tlk:GridBoundColumn HeaderText="Người ký quyết định thôi kiêm nhiệm 1" DataField="SIGN_NAME_STOP"
                SortExpression="SIGN_NAME_STOP" UniqueName="SIGN_NAME_STOP" HeaderStyle-Width="120px" />
            <tlk:GridBoundColumn HeaderText="Chức danh quyết định thôi kiêm nhiệm 1" DataField="SIGN_TITLE_NAME_STOP"
                SortExpression="SIGN_TITLE_NAME_STOP" UniqueName="SIGN_TITLE_NAME_STOP" HeaderStyle-Width="120px" />
            <tlk:GridBoundColumn HeaderText="Người ký quyết định thôi kiêm nhiệm 2" DataField="SIGN_NAME_STOP2"
                SortExpression="SIGN_NAME_STOP2" UniqueName="SIGN_NAME_STOP2" HeaderStyle-Width="120px" />
            <tlk:GridBoundColumn HeaderText="Chức danh quyết định thôi kiêm nhiệm 2" DataField="SIGN_TITLE_NAME_STOP2"
                SortExpression="SIGN_TITLE_NAME_STOP2" UniqueName="SIGN_TITLE_NAME_STOP2" HeaderStyle-Width="120px" />
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
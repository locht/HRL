<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalSalary.ascx.vb"
    Inherits="Profile.ctrlPortalSalary" %>
<tlk:RadGrid PageSize=50 ID="rgSalary" runat="server" Height="300px" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="ID,EFFECT_DATE,EMPLOYEE_ID">
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
            <tlk:GridNumericColumn HeaderText="<%$ Translate: Lương cơ bản %>" DataField="SAL_BASIC"
                UniqueName="SAL_BASIC" DataFormatString="{0:n0}" SortExpression="SAL_BASIC"
                ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridNumericColumn HeaderText="<%$ Translate: % lương được hưởng %>" DataField="PERCENT_SALARY"
                UniqueName="PERCENT_SALARY" SortExpression="PERCENT_SALARY" ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí hỗ trợ  %>" DataField="COST_SUPPORT"
                UniqueName="COST_SUPPORT" SortExpression="COST_SUPPORT" ShowFilterIcon="true" DataFormatString="{0:n0}">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng thu nhập  %>" DataField="SAL_TOTAL"
                UniqueName="SAL_TOTAL" SortExpression="SAL_TOTAL" ShowFilterIcon="true" DataFormatString="{0:n0}">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
<br />
<tlk:RadGrid PageSize=50 ID="rgAllow" runat="server" Height="200px">
    <MasterTableView Caption="<%$ Translate: Phụ cấp theo tờ trình/QĐ %>">
        <Columns>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên phụ cấp %>" DataField="ALLOWANCE_LIST_NAME"
                SortExpression="ALLOWANCE_LIST_NAME" UniqueName="ALLOWANCE_LIST_NAME" />
            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="AMOUNT"
                SortExpression="AMOUNT" UniqueName="AMOUNT" DataFormatString="{0:n0}">
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
            </tlk:GridNumericColumn>
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}"/>
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}"/>
            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đóng bảo hiểm %>" DataField="IS_INSURRANCE"
                SortExpression="IS_INSURRANCE" UniqueName="IS_INSURRANCE" HeaderStyle-Width="100px">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </tlk:GridCheckBoxColumn>
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
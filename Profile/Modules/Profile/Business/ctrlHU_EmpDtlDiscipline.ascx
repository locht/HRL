<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlDiscipline.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlDiscipline" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal" SkinID="Demo">
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgGrid" runat="server" AllowFilteringByColumn="true" Height="100%">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số quyết định %>" DataField="DECISION_NO"
                        UniqueName="DECISION_NO" SortExpression="DECISION_NO" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        FilterControlWidth="100%">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp kỷ luật %>" DataField="DISCIPLINE_LEVEL_NAME"
                        UniqueName="DISCIPLINE_LEVEL_NAME" SortExpression="DISCIPLINE_LEVEL_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="30%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức kỷ luật %>" DataField="DISCIPLINE_TYPE_NAME"
                        UniqueName="DISCIPLINE_TYPE_NAME" SortExpression="DISCIPLINE_TYPE_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tiền phạt %>" DataField="MONEY" UniqueName="MONEY"
                        SortExpression="MONEY" DataFormatString="{0:###,###,###,##0}" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" FilterControlWidth="100%">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
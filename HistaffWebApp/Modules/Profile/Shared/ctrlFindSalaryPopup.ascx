<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindSalaryPopup.ascx.vb"
    Inherits="Profile.ctrlFindSalaryPopup" %>
    <link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
   
<tlk:RadWindow runat="server" ID="rwMessage" Behaviors="Close" VisibleStatusbar="false"
    Height="400px" Width="600px" Modal="true" Title="<%$ Translate: Quá trình lương %>"
   OnClientClose="popupclose" VisibleOnPageLoad ="true" 
    ViewStateMode="Enabled">
    <ContentTemplate>
    <asp:HiddenField ID="hidEmployeeID" runat="server" />
    <asp:TextBox ID="txtEmployeeID" runat="server" style="display:none;"></asp:TextBox>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <p>
                
                <tlk:RadGrid  ID="rgSalary" runat="server" AutoGenerateColumns="False" AllowPaging="False" Height="300px">
                    <MasterTableView DataKeyNames="ID,DECISION_NO,EFFECT_DATE,EXPIRE_DATE,SAL_GROUP_NAME,SAL_LEVEL_NAME,SAL_RANK_NAME,SAL_BASIC,PERCENT_SALARY,ALLOWANCE_TOTAL">
                        <Columns>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: ID %>" DataField="ID"
                                                 SortExpression="ID" UniqueName="ID"  Visible="false"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số quyết định %>" DataField="DECISION_NO"
                                SortExpression="DECISION_NO" UniqueName="DECISION_NO" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />

                            <tlk:GridBoundColumn HeaderText="" DataField="SAL_TYPE_NAME" SortExpression="SAL_TYPE_NAME" UniqueName="SAL_TYPE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thang lương %>" DataField="SAL_GROUP_NAME" SortExpression="SAL_GROUP_NAME" UniqueName="SAL_GROUP_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngạch lương %>" DataField="SAL_LEVEL_NAME" SortExpression="SAL_LEVEL_NAME" UniqueName="SAL_LEVEL_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Bậc lương %>" DataField="SAL_RANK_NAME" SortExpression="SAL_RANK_NAME" UniqueName="SAL_RANK_NAME" />
                            <tlk:GridNumericColumn HeaderText="" DataField="TAX_TABLE_Name" SortExpression="TAX_TABLE_Name" UniqueName="TAX_TABLE_Name"/>
                            <tlk:GridNumericColumn HeaderText="" DataField="SAL_BASIC" SortExpression="SAL_BASIC" UniqueName="SAL_BASIC" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="" DataField="SAL_INS" SortExpression="SAL_INS" UniqueName="SAL_INS" DataFormatString="{0:n0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng phụ cấp %>" DataField="ALLOWANCE_TOTAL" SortExpression="ALLOWANCE_TOTAL" UniqueName="ALLOWANCE_TOTAL"  DataFormatString="{0:n0}"/>
                            <tlk:GridNumericColumn HeaderText="" DataField="SAL_TOTAL" SortExpression="SAL_TOTAL" UniqueName="SAL_TOTAL" DataFormatString="{0:n0}" />

                        </Columns>
                        <HeaderStyle Width="120px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </p>
            <div style="margin: 0px 10px 10px 10px; text-align: right;">
                <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Chọn %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
                <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
            </div>
        </tlk:RadAjaxPanel>
    </ContentTemplate>
</tlk:RadWindow>
<tlk:RadScriptBlock runat="server" ID="ScriptBlock">
    <script type="text/javascript">
        function popupclose(sender, args) {
            $get("<%= btnNO.ClientId %>").click();
        }
    </script>
</tlk:RadScriptBlock>

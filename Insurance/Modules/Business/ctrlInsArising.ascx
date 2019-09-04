<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsArising.ascx.vb"
    Inherits="Insurance.ctrlInsArising" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="300" Width="300px" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">            
            <tlk:RadPane ID="RadPane3" runat="server" Height="180px" Scrolling="None">         
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnFind">        
                <div style="display: none;">
                    <tlk:RadTextBox ID="txtID" Text="0" runat="server">
                    </tlk:RadTextBox>
                    <%-- <%# Translate("Tên loại biến động khai báo")%>--%>
                </div>
                <div>
                    <fieldset style="width: auto; height: auto">
                        <legend>
                            <%# Translate("Thông tin tìm kiếm")%>
                        </legend>
                        <table width="100%" class="td-padding">
                            <tr>
                                <td class="lb">
                                    <%# Translate("Từ tháng")%>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker runat="server" ID="txtFROMDATE" TabIndex="4" Culture="en-US">
                                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="MM/yyyy">
                                        </DateInput>
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Đến tháng")%>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker runat="server" ID="txtTODATE" TabIndex="5" Culture="en-US">
                                            <DateInput ID="DateInput2" runat="server" DisplayDateFormat="MM/yyyy"></DateInput>
                                     </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Đơn vị BH")%>
                                </td>
                                <td>
                                    <tlk:RadComboBox DataValueField="ID" DataTextField="NAME" ID="ddlINSORG" runat="server"
                                        TabIndex="2" ToolTip="<%$ Translate: Đơn vị BH %>" AutoPostBack="false" EmptyMessage="<%$ Translate: Đơn vị BH %>">
                                    </tlk:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Nhóm biến động")%>
                                </td>
                                <td>
                                    <tlk:RadComboBox DataValueField="CODE" DataTextField="NAME" ID="ddlGROUP_ARISING_TYPE_ID"
                                        runat="server" TabIndex="3" EmptyMessage="<%$ Translate: Nhóm biến động %>" CheckBoxes="false"
                                        EnableCheckAllItemsCheckBox="false" DropDownAutoWidth="Enabled" Height="100px">
                                    </tlk:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                </td>
                                <td>
                                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFind" TabIndex="8" runat="server"
                                        Text="<%$ Translate: Tìm kiếm %>" ValidationGroup="Search" SkinID="ButtonFind"
                                        OnClick="btnFIND_Click">
                                    </tlk:RadButton>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </asp:Panel>
            </tlk:RadPane>
            <tlk:RadSplitBar ID="RadSplitBar3" runat="server">
            </tlk:RadSplitBar>
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                <Common:ctrlOrganization ID="ctrlOrg" runat="server" AutoPostBack="false"/>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="120px">
                <tlk:RadToolBar ID="rtbMain" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Tháng khai báo cơ quan BH")%>
                        </td>
                        <td>
                            <%--<tlk:RadDatePicker runat="server" DateInput-DateFormat="MM/yyyy" ID="txtMONTH" 
                                TabIndex="5">
                            </tlk:RadDatePicker>--%>
                            <tlk:RadMonthYearPicker runat="server" ID="txtMONTH" TabIndex="5" Culture="en-US">
                                 <DateInput ID="DateInput3" runat="server" DisplayDateFormat="MM/yyyy"></DateInput>
                            </tlk:RadMonthYearPicker>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtMONTH"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn tháng khai báo cơ quan BH. %>"
                                ToolTip="<%$ Translate: Bạn phải chọn tháng khai báo cơ quan BH. %>"></asp:RequiredFieldValidator>--%>
                        </td>
                        <td class="lb">
                            <%# Translate("Loại biến động")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox DataValueField="ID" DataTextField="NAME" ID="ddlINS_ARISING_TYPE_ID"
                                runat="server" TabIndex="2" ToolTip="<%$ Translate: Tên loại biến động khai báo %>"
                                AutoPostBack="true" EmptyMessage="<%$ Translate: Chọn Tên loại biến động khai báo %>">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqddlINS_ARISING_TYPE_ID" ControlToValidate="ddlINS_ARISING_TYPE_ID"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Loại biến động. %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Loại biến động. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgGridData" runat="server" Height="100%" AllowPaging="True" AllowSorting="True" EnableLinqExpressions="false"
                    CellSpacing="0" ShowStatusBar="true" AllowMultiRowSelection="true" GridLines="None"
                    PageSize="20" AutoGenerateColumns="false" AllowFilteringByColumn="true">
                    <ClientSettings AllowColumnsReorder="True" EnableRowHoverStyle="true" EnablePostBackOnRowClick="True" 
                                    Scrolling-AllowScroll="true" Scrolling-SaveScrollPosition="true"  Scrolling-UseStaticHeaders="true">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" FrozenColumnsCount="5" />
                        <Selecting AllowRowSelect="true" />
                        <Scrolling UseStaticHeaders="true" />
                        <Resizing AllowColumnResize="true" />
                    </ClientSettings>
                    <MasterTableView TableLayout="Fixed" CommandItemDisplay="None" DataKeyNames="ID"
                        ClientDataKeyNames="ID,EMPLOYEE_CODE,EMPID,FULL_NAME,DEP_NAME,TITLE_NAME,EFFECT_DATE,ARISING_TYPE_NAME,ARISING_GROUP_TYPE,NOTE,ORG_DESC">
                        <GroupByExpressions>
                            <tlk:GridGroupByExpression>
                                <SelectFields>
                                    <tlk:GridGroupByField FieldName="ARISING_GROUP_TYPE" HeaderText="Nhóm Biến động">
                                    </tlk:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <tlk:GridGroupByField FieldName="ARISING_GROUP_TYPE"></tlk:GridGroupByField>
                                </GroupByFields>
                            </tlk:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã NV %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" FilterControlWidth="99%" HeaderStyle-Width="70px"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULL_NAME"
                                UniqueName="FULL_NAME" SortExpression="FULL_NAME" HeaderStyle-Width="150px" FilterControlWidth="99%"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="DEP_NAME"
                                UniqueName="DEP_NAME" HeaderStyle-Width="150px" SortExpression="DEP_NAME" FilterControlWidth="99%"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị BH %>" DataField="U_INSURANCE_NAME"
                                UniqueName="U_INSURANCE_NAME" HeaderStyle-Width="100px" SortExpression="U_INSURANCE_NAME" FilterControlWidth="99%"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />

                             <%--<tlk:GridTemplateColumn HeaderText="Đơn vị" DataField="DEP_NAME" SortExpression="ORG_NAME"
                                UniqueName="DEP_NAME">
                                <HeaderStyle Width="200px" />
                                <ItemTemplate>
                                 <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEP_NAME")%>'>
                                </asp:Label>
                                <tlk:RadToolTip RenderMode="Lightweight" ID="RadToolTip1" runat="server" TargetControlID="Label1"
                                                    RelativeTo="Element" Position="BottomCenter">
                                <%# DrawTreeByString(DataBinder.Eval(Container, "DataItem.ORG_DESC"))%>
                                </tlk:RadToolTip>
                            </ItemTemplate>
                            </tlk:GridTemplateColumn>--%>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" HeaderStyle-Width="150px" SortExpression="TITLE_NAME"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE"
                                HeaderStyle-Width="85px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại biến động %>" DataField="ARISING_TYPE_NAME"
                                UniqueName="ARISING_TYPE_NAME" SortExpression="ARISING_TYPE_NAME" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lương cũ %>" DataFormatString="{0:N0}"
                                DataField="OLD_SAL" UniqueName="OLD_SAL" SortExpression="OLD_SAL" HeaderStyle-Width="80px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lương mới %>" DataFormatString="{0:N0}"
                                DataField="NEW_SAL" UniqueName="NEW_SAL" SortExpression="NEW_SAL" HeaderStyle-Width="80px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHXH %>" DataField="SI" DataType="System.Boolean"
                                ItemStyle-VerticalAlign="Middle" UniqueName="SI" SortExpression="SI" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHYT %>" DataField="HI" DataType="System.Boolean"
                                ItemStyle-VerticalAlign="Middle" UniqueName="HI" SortExpression="HI" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHTN %>" DataField="UI" DataType="System.Boolean"
                                ItemStyle-VerticalAlign="Middle" UniqueName="UI" SortExpression="UI" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHTNLD-BNN %>" DataField="BHTNLD_BNN" DataType="System.Boolean"
                                ItemStyle-VerticalAlign="Middle" UniqueName="BHTNLD_BNN" SortExpression="BHTNLD_BNN" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Sử dụng %>" DataField="ARISING_TYPE_ID"
                                UniqueName="ARISING_TYPE_ID" SortExpression="ARISING_TYPE_ID" Visible="false" />

                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Số sổ bảo hiểm %>" DataField="SOCIAL_NUMBER"
                                UniqueName="SOCIAL_NUMBER" HeaderStyle-Width="150px" SortExpression="SOCIAL_NUMBER"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />


                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do %>" DataField="REASONS" UniqueName="REASONS"
                                SortExpression="REASONS" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã NV%>" DataField="EMPID" UniqueName="EMPID"
                                SortExpression="EMPID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: ID%>" DataField="ID" UniqueName="ID"
                                SortExpression="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: ID%>" DataField="ARISING_GROUP_TYPE"
                                UniqueName="ARISING_GROUP_TYPE" SortExpression="ARISING_GROUP_TYPE" Visible="false" />                                                        
                            <%--<tlk:GridTemplateColumn Display="false" HeaderText="<%$ Translate: Ghi chú %>" UniqueName="NOTE" SortExpression="NOTE" HeaderStyle-Width="200px">
                              <ItemTemplate>
                                <tlk:RadTextBox ID="rtbNote" runat="server" Text='<%# Bind("NOTE") %>'></tlk:RadTextBox>
                              </ItemTemplate>
                            </tlk:GridTemplateColumn>--%>                           
                        </Columns>
                    </MasterTableView>
                    <GroupingSettings ShowUnGroupButton="true"></GroupingSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>
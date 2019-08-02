<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_Manning.ascx.vb"
    Inherits="Recruitment.ctrlRC_Manning" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidID" Value="" runat="server" />
<asp:HiddenField ID="hidMannOrgId" Value="" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Height="100%" Width="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="Y">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="rtbMain" runat="server" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="validationsummary"
                    DisplayMode="BulletList" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Height="200px" Scrolling="None">
                <table class="table-form">
                    <%--         <tr>
                        <td class="item-head" colspan="6">
                            <%# Translate("Tìm")%>
                            <hr />
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>:
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" runat="server" AutoPostBack="true" CausesValidation="false">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Tên định biên")%>:
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboListManning" runat="server" AutoPostBack="false" CausesValidation="false">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                Text="<%$ Translate: Tìm %>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
                <table class="table-form">
                    <tr>
                        <td class="item-head" colspan="6">
                            <%# Translate("Thông tin định biên")%>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Đơn vị / Phòng ban")%>:
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblOrgName" runat="server" Font-Bold="true">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Tên định biên")%>:<span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="rbtManName" runat="server" MaxLength="50" ToolTip="<%$ Translate: RC_MANNING_NAME %>"
                                Height="22" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rbtManName"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên định biên. %>"
                                Text="<%$ Translate: Bạn phải nhập tên định biên. %>" ToolTip="<%$ Translate: Bạn phải nhập tên định biên. %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực")%>:<span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdEffectDate"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"
                                Text="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                        </td>
                        <td>
                            <asp:CheckBox ID="cbStatus" runat="server" Text="<%$ Translate: Phê duyệt %>" />
                        </td>
                    </tr>
                    <tr>
                        <%--   <td>
                                <%# Translate("Định biên cũ")%>:
                            </td>
                            <td>
                                <tlk:RadTextBox ID="rbtManningOld" runat="server" Width="250px" MaxLength="50" ToolTip="<%$ Translate: RC_MANNING_OLD %>"
                                    Height="22" Enabled="false" />
                            </td>--%>
                        <td class="lb">
                            <%# Translate("Số lượng hiện tại")%>:
                        </td>
                        <td>
                            <tlk:RadTextBox ID="rbtManningCurrentCount" ClientIDMode="Static" runat="server"
                                 MaxLength="50" ToolTip="<%$ Translate: RC_MANNING_CURRENT_COUNT %>"
                                Height="22" Enabled="false" />
                        </td>
                        <td class="lb">
                            <%# Translate("Định biên")%>:
                        </td>
                        <td>
                            <tlk:RadTextBox ID="rbtManningNew" ClientIDMode="Static" runat="server" MaxLength="50"
                                ToolTip="<%$ Translate: RC_MANNING_NEW %>" Height="22" Enabled="false">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Số lượng tăng giảm")%>:
                        </td>
                        <td>
                            <tlk:RadTextBox ID="rbtManningCountMobilize" ClientIDMode="Static" runat="server"
                                MaxLength="50" ToolTip="<%$ Translate: RC_MANNING_COUNT_MOBILIZE %>" Height="22"
                                Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>:
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="rbtNote" runat="server" MaxLength="50" ToolTip="<%$ Translate: RC_MANNING_NOTE %>"
                                Height="22" Width="100%" />
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgManning" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    PageSize="50" AllowSorting="True" AllowMultiRowSelection="false" CellSpacing="0"
                    GridLines="None" Height="100%" Width="100%">
                    <ClientSettings>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
                    <MasterTableView DataKeyNames="ID" EditMode="InPlace">
                        <Columns>
                            <tlk:GridBoundColumn DataField="ID" UniqueName="ID" Visible="false">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="NAME" EmptyDataText="" FilterControlWidth="90%" HeaderText="<%$ Translate: Tên định biên %>"
                                SortExpression="NAME" UniqueName="NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="ORG_NAME" FilterControlWidth="90%" HeaderText="<%$ Translate: Phòng ban %>"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="TITLE_NAME" FilterControlWidth="90%" HeaderText="<%$ Translate: Chức danh %>"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>                              
                            <tlk:GridBoundColumn DataField="EFFECT_DATE" EmptyDataText="" FilterControlWidth="90%"
                                HeaderText="<%$ Translate: Ngày hiệu lực %>" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            </tlk:GridBoundColumn>
                            <%-- <tlk:GridBoundColumn DataField="EmployeeCount" EmptyDataText="" FilterControlWidth="90%"
                                HeaderText="<%$ Translate: SLNV hiện tại %>" SortExpression="EmployeeCount" UniqueName="EmployeeCount">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            </tlk:GridBoundColumn>--%>
                            <tlk:GridBoundColumn DataField="OLD_MANNING" EmptyDataText="" FilterControlWidth="90%"
                                HeaderText="<%$ Translate: Định biên cũ %>" SortExpression="OLD_MANNING" UniqueName="OLD_MANNING"
                                Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="CURRENT_MANNING" EmptyDataText="" HeaderText="<%$ Translate: Số lượng hiện tại %>"
                                SortExpression="CURRENT_MANNING" UniqueName="CURRENT_MANNING">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridTemplateColumn DataField="NEW_MANNING" HeaderText="<%$ Translate: Định biên %>"
                                SortExpression="NEW_MANNING" UniqueName="NEW_MANNING">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="txtNewManning" ShowSpinButtons="false" MinValue="0" DataType="Interger"
                                        runat="server" CausesValidation="false" Text='<%# (Eval("NEW_MANNING")) %>' Width="78px">
                                        <ClientEvents OnBlur="OnChanged" />
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridBoundColumn DataField="MOBILIZE_COUNT_MANNING" EmptyDataText="" HeaderText="<%$ Translate: Số lượng tăng/giảm %>"
                                SortExpression="MOBILIZE_COUNT_MANNING" UniqueName="MOBILIZE_COUNT_MANNING">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridTemplateColumn DataField="NOTE" HeaderText="<%$ Translate: Ghi chú %>" SortExpression="NOTE"
                                UniqueName="NOTE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <tlk:RadTextBox ID="txtNote" runat="server" CausesValidation="false" Text='<%# (Eval("NOTE")) %>'>
                                        <ClientEvents OnBlur="OnChanged" />
                                    </tlk:RadTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                        </Columns>
                        <HeaderStyle Width="150px" />
                    </MasterTableView></tlk:RadGrid><Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
            </tlk:RadPane>
        </tlk:RadSplitter></tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server" OnAjaxRequest="RadAjaxPanel1_AjaxRequest">
</tlk:RadAjaxPanel>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }


        function OnClientClose(oWnd, args) {
            postBack(oWnd.get_navigateUrl());
        }

        function OnFocus(index) {
            var grid = $find('<%# rgManning.ClientID%>').get_masterTableView();
            grid.clearSelectedItems();
            grid.get_dataItems()[index].set_selected(true);
        }

        function OnChanged(sender, eventArgs) {
            $find("<%= RadAjaxPanel1.ClientID%>").ajaxRequestWithTarget("<%= RadAjaxPanel1.UniqueID %>", sender.get_id());
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

    </script>
</tlk:RadScriptBlock>

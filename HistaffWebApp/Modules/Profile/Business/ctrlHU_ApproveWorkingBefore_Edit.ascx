<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ApproveWorkingBefore_Edit.ascx.vb"
    Inherits="Profile.ctrlHU_ApproveWorkingBefore_Edit" %>
<%@ Import Namespace="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarWorkings" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="53px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Lý do không phê duyệt")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" runat="server" Width="100%" MaxLength="250" Rows="1" Height="38px">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 500px;">
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgData" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,STATUS" ClientDataKeyNames="ID,STATUS">
                        <Columns>
                         <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên công ty %>" DataField="COMPANY_NAME"
                                UniqueName="COMPANY_NAME" SortExpression="COMPANY_NAME" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số điện thoại %>" DataField="TELEPHONE"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                UniqueName="TELEPHONE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ công ty %>" DataField="COMPANY_ADDRESS"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                UniqueName="COMPANY_ADDRESS">
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày vào %>" DataField="JOIN_DATE"
                                UniqueName="JOIN_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="JOIN_DATE"
                                ShowFilterIcon="true">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày nghỉ %>" DataField="END_DATE"
                                UniqueName="END_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="END_DATE"
                                ShowFilterIcon="true">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn DataField="SALARY" HeaderText="<%$ Translate: Mức lương %>"
                                UniqueName="SALARY" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                Visible="true" DataFormatString="{0:n0}">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                UniqueName="TITLE_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do nghỉ %>" DataField="TER_REASON"
                                UniqueName="TER_REASON" SortExpression="TER_REASON" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Width="120px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_ApproveWorkingBefore_Edit_RadSplitter3');
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs)
        {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args)
        {
           
        }

    </script>
</tlk:RadCodeBlock>

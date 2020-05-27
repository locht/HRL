<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindEmployee2GridPopupDialog.ascx.vb"
    Inherits="Common.ctrlFindEmployee2GridPopupDialog" %>
    <%@ Import Namespace="Framework.UI.Utilities" %>
<asp:HiddenField runat="server" ID="hidPageId" Value="ctrlFindEmployee2GridPopupDialog" />
<asp:HiddenField runat="server" ID="hidOrg" />
<asp:HiddenField ID="hidSelected" runat="server"/>
<%@ Register Src="ctrlOrganization.ascx" TagName="ctrlOrganization" TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="250" Width="250px" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter4" runat="server" Orientation="Horizontal" Width="100%"
            Height="100%">
            <tlk:RadPane ID="Pane1" runat="server" Width="100%" Height="50%" Scrolling="None">
                <tlk:RadSplitter ID="RadSplitter5" runat="server" Orientation="Horizontal" Width="100%"
                    Height="100%">
                    <tlk:RadPane ID="RadPane6" runat="server" Width="100%" Scrolling="None">
                        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
                    </tlk:RadPane>
                </tlk:RadSplitter>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar2" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane runat="server" ID="RightPane" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane runat="server" ID="RadPane7" Scrolling="None" Height="100%">
                <tlk:RadMultiPage runat="server" ID="pageImportSalary" SelectedIndex="0" Height="100%">
                    <tlk:RadPageView ID="RadPageView1" runat="server">
                        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
                            <tlk:RadPane runat="server" ID="RadPane2" Scrolling="None" Height="50%">
                                <tlk:RadGrid ID="rgvEmployees" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    Height="100%" AllowSorting="True" AllowMultiRowSelection="True" CellSpacing="0"
                                    GridLines="None" AllowFilteringByColumn="True" HierarchyLoadMode="ServerOnDemand">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" />
                                    </ClientSettings>
                                    <MasterTableView DataKeyNames="EMPLOYEE_ID,EMPLOYEE_CODE,FULLNAME_VN,ORG_ID,ORG_NAME,TITLE_NAME,JOIN_DATE" ClientDataKeyNames="EMPLOYEE_ID,EMPLOYEE_CODE,FULLNAME_VN,ORG_ID,ORG_NAME,JOIN_DATE">
                                        <Columns>
                                            <tlk:GridClientSelectColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px"
                                                ItemStyle-HorizontalAlign="Center" UniqueName="cbCheck">
                                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </tlk:GridClientSelectColumn>
                                            <tlk:GridBoundColumn DataField="EMPLOYEE_ID" UniqueName="EMPLOYEE_ID" Visible="false">
                                            </tlk:GridBoundColumn>
                                            <tlk:GridBoundColumn DataField="EMPLOYEE_CODE" HeaderText="<%$ Translate: Mã nhân viên %>"
                                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE">
                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                            </tlk:GridBoundColumn>
                                            <tlk:GridBoundColumn DataField="FULLNAME_VN" HeaderText="<%$ Translate: Tên nhân viên %>"
                                                UniqueName="FULLNAME_VN"  SortExpression="FULLNAME_VN">
                                                <HeaderStyle HorizontalAlign="Center" Width="30%" />
                                            </tlk:GridBoundColumn>
                                            <tlk:GridBoundColumn DataField="ORG_ID" HeaderText="<%$ Translate: Mã phòng ban %>"
                                                UniqueName="ORG_ID" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            </tlk:GridBoundColumn>
                                            <tlk:GridBoundColumn DataField="ORG_NAME" HeaderText="<%$ Translate: Tên phòng ban %>"
                                                UniqueName="ORG_NAME" SortExpression="ORG_NAME"/>

                                            <tlk:GridBoundColumn DataField="JOIN_DATE" HeaderText="<%$ Translate: Ngày vào TCT/cty con %>"
                                                UniqueName="JOIN_DATE" SortExpression="JOIN_DATE" DataFormatString="{0:dd/MM/yyyy}"/>

                                                  <tlk:GridBoundColumn DataField="TITLE_NAME" HeaderText="<%$ Translate: Chức vụ %>"
                                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                            </tlk:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </tlk:RadGrid>
                            </tlk:RadPane>
                            <tlk:RadSplitBar ID="RadSplitBar3" runat="server" CollapseMode="Forward">
                            </tlk:RadSplitBar>
                            <tlk:RadPane runat="server" ID="RadPane4" Scrolling="None" Height="10%">
                                <div>
                                    <table>
                                        <tr>
                                            <tlk:RadToolBar runat="server" ID="tbarExport"></tlk:RadToolBar>
                                        </tr>
                                    </table>
                                </div>
                                </tlk:RadPane> 
                                 <tlk:RadPane runat="server" ID="RadPane1" Scrolling="None" Height="40%">
                                <tlk:RadGrid ID="rgvDataPrepare" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize =50
                                    Height="100%" AllowSorting="True" AllowMultiRowSelection="True" CellSpacing="0"
                                    GridLines="None"  HierarchyLoadMode="ServerOnDemand">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" />
                                    </ClientSettings>
                                
                                    <MasterTableView DataKeyNames="EMPLOYEE_ID,EMPLOYEE_CODE,FULLNAME_VN,ORG_ID,ORG_NAME,TITLE_NAME,JOIN_DATE" ClientDataKeyNames="EMPLOYEE_ID,EMPLOYEE_CODE,FULLNAME_VN,ORG_ID,ORG_NAME,JOIN_DATE">
                                        <Columns>
                                            <tlk:GridClientSelectColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px"
                                                ItemStyle-HorizontalAlign="Center" UniqueName="cbCheck">
                                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </tlk:GridClientSelectColumn>
                                            <tlk:GridBoundColumn DataField="EMPLOYEE_ID" UniqueName="EMPLOYEE_ID" Visible="false">
                                            </tlk:GridBoundColumn>
                                            <tlk:GridBoundColumn DataField="EMPLOYEE_CODE" HeaderText="<%$ Translate: Mã nhân viên %>"
                                                UniqueName="EMPLOYEE_CODE">
                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                            </tlk:GridBoundColumn>
                                            <tlk:GridBoundColumn DataField="FULLNAME_VN" HeaderText="<%$ Translate: Tên nhân viên %>"
                                                UniqueName="FULLNAME_VN">
                                                <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                            </tlk:GridBoundColumn>
                                            <tlk:GridBoundColumn DataField="ORG_ID" HeaderText="<%$ Translate: Mã phòng ban %>"
                                                UniqueName="ORG_ID" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            </tlk:GridBoundColumn>
                                            <tlk:GridBoundColumn DataField="ORG_NAME" HeaderText="<%$ Translate: Tên phòng ban %>"
                                                UniqueName="ORG_NAME"/>
                                                  <tlk:GridBoundColumn DataField="TITLE_NAME" HeaderText="<%$ Translate: Chức vụ %>"
                                                UniqueName="TITLE_NAME">
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                            </tlk:GridBoundColumn>
                                        </Columns>
                                       
                                    </MasterTableView>
                                </tlk:RadGrid>
                            </tlk:RadPane>
                        </tlk:RadSplitter>
                    </tlk:RadPageView>
                </tlk:RadMultiPage>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EDIT') {
                OpenEdit();
                args.set_cancel(true);
            }

            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
        }

        function btnYesClick() {
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();

            oArg.ID = $("#<%=hidSelected.ClientID %>").val();
            //Close the RadWindow and send the argument to the parent page
            oWnd.close(oArg);
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientClose(oWnd, args) {
            oWnd = $find('<%=popupId %>');
            oWnd.remove_close(OnClientClose);
            var arg = args.get_argument();
            if (arg) {
                postBack(arg);
            }
        }

        function postBack(arg) {
            var ajaxManager = $find("<%= AjaxManagerId %>");
            ajaxManager.ajaxRequest(arg); //Making ajax request with the argument
        }
    </script>
</tlk:RadCodeBlock>

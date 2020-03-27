<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_JobPosition.ascx.vb"
    Inherits="Profile.ctrlHU_JobPosition" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
                <tlk:RadToolBar ID="tbarOrgTitles" runat="server" onclientbuttonclicking="clientButtonClicking"/>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgOrgTitle" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    AllowFilteringByColumn="true" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                         <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,TITLE_ID,ACTFLG,JOB_NAME,ORG_ID" ClientDataKeyNames="ID,TITLE_ID,ACTFLG,JOB_NAME,ORG_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="Mã vị trí công việc" DataField="CODE"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" SortExpression="CODE"
                                UniqueName="CODE" />
                            <tlk:GridBoundColumn HeaderText="Tên vị trí công việc" DataField="JOB_NAME"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" SortExpression="JOB_NAME"
                                UniqueName="JOB_NAME" />
                            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" SortExpression="TITLE_NAME"
                                UniqueName="TITLE_NAME" />
                             <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Trưởng đơn vị %>" DataField="IS_LEADER" 
                                     AllowFiltering ="false" FooterStyle-HorizontalAlign="Center"
                                    SortExpression="IS_LEADER" UniqueName="IS_LEADER">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                     </tlk:GridCheckBoxColumn>
                            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="ACTFLG"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" SortExpression="ACTFLG"
                                UniqueName="ACTFLG" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:radwindowmanager id="RadWindowManager1" runat="server">
    <windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"
            OnClientClose="popupclose" Height="600px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Tạo hợp đồng %>">
        </tlk:RadWindow>
    </windows>
</tlk:radwindowmanager>
<tlk:radcodeblock id="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function OpenNew() {
            var extented = '';
            var bCheck = $find('<%= rgOrgTitle.ClientID%>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 1) {
                empID = $find('<%= rgOrgTitle.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
                if (empID)
                    extented = '&empid=' + empID;
            } else if (bCheck > 1) {
                var m = '<%= Translate("Bạn không thể copy dữ liệu từ nhiều bản ghi! Không thể thực hiện thao tác này") %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            window.open('/Default.aspx?mid=Organize&fid=ctrlHU_JobPositionNewEdit&group=Business' + extented, "_self");
        }

        function OpenEdit() {
            var bCheck = $find('<%= rgOrgTitle.ClientID%>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            } else if (bCheck > 1) {
                return 2;
            }
            var id = $find('<%= rgOrgTitle.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');

            window.open('/Default.aspx?mid=Organize&fid=ctrlHU_JobPositionNewEdit&group=Business&IDSelect=' + id, "_self");

            return 0;
        }


        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'REFRESH') {
                OPENTHANHLY();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT") {
                var bCheck = $find('<%= rgOrgTitle.ClientID%>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }

            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "EDIT") {
                if (OpenEdit() == 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                } else if (OpenEditContract() == 2) {
                    var m = '<%= Translate("Bạn không thể sửa nhiều bản ghi! Không thể thực hiện thao tác này") %>';
                        var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    }
                args.set_cancel(true);
            }
        }
        function gridRowDblClick(sender, eventArgs) {
             OpenEdit();
        }
        function popupclose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }
        }



        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

  </script>
</tlk:radcodeblock>
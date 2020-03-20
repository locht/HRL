<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_JobDescription.ascx.vb"
    Inherits="Profile.ctrlHU_JobDescription" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
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
                <tlk:RadToolBar ID="tbarWorkings" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="50px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbKeyWord" runat="server" Text="Chức danh"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTitle" runat="server" AutoPostBack="true" >
                            </tlk:RadComboBox>
                        </td>
                        <td style="display:none">
                            <tlk:RadButton ID="btnSearch" runat="server" Text="Tìm" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgJobDes" runat="server" AllowMultiRowSelection="true"
                    Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <ClientEvents OnCommand="ValidateFilter" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã công việc %>" DataField="CODE"
                                SortExpression="CODE" UniqueName="CODE" HeaderStyle-Width="60px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên công việc %>" DataField="NAME"
                                SortExpression="NAME" UniqueName="NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả công việc %>" DataField="JOB_DESCRIPTION"
                                SortExpression="JOB_DESCRIPTION" UniqueName="JOB_DESCRIPTION" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Điều kiện làm việc %>" DataField="JOB_CONDITION" SortExpression="JOB_CONDITION"
                                UniqueName="JOB_CONDITION" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đặc thù công việc %>" DataField="JOB_PARTICULARTIES"
                                SortExpression="JOB_PARTICULARTIES" UniqueName="JOB_PARTICULARTIES" /> 

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Môi trường làm việc %>" DataField="WORK_ENVIRONMENT"
                                ItemStyle-HorizontalAlign="Center" SortExpression="WORK_ENVIRONMENT" UniqueName="WORK_ENVIRONMENT" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trách nhiệm công việc %>" DataField="JOB_RESPONSIBILITY"
                                SortExpression="JOB_RESPONSIBILITY" UniqueName="JOB_RESPONSIBILITY" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Quyền hạn được hưởng %>" DataField="JOB_POWER"
                                SortExpression="JOB_POWER" UniqueName="JOB_POWER" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức lương từ %>" DataField="SAL_FROM"
                                SortExpression="SAL_FROM" UniqueName="SAL_FROM" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức lương đến %>" DataField="SAL_TO" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}" SortExpression="SAL_TO" UniqueName="SAL_TO" />
                        </Columns>
                        <HeaderStyle Width="120px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="900px"
            OnClientClose="popupclose" Height="640px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
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


        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenWage() {
            var extented = '';
            var bCheck = $find('<%= rgJobDes.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck > 1) {
                var n = noty({ text: 'Không thể chọn nhiều dòng để thực hiện thao tác này', dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            if (bCheck == 1) {
                ID = $find('<%= rgJobDes.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                if (empID)
                    extented = '&ID=' + ID;
            }

            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_JobDescriptionNewEdit&group=Business' + extented, "_self"); /*
            //var pos = $("html").offset();
            //oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
        }

        function OpenWageEdit(e) {
            var bCheck = $find('<%= rgJobDes.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            var id = $find('<%= rgJobDes.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');

            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_JobDescriptionNewEdit&group=Business&ID=' + id, "_self"); /*
            //var pos = $("html").offset();
            //oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
            return 0;
        }

        function OnClientButtonClicking(sender, args) {
            var m;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenWage();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'EDIT') {
                if (OpenWageEdit(false) == 1) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                }
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "NEXT") {
                enableAjax = false;
            }
        }

        function gridRowDblClick(sender, eventArgs) {
            if (OpenWageEdit(true) == 1) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
            }
            args.set_cancel(true);
        }

        function popupclose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $get("<%= btnSearch.ClientId %>").click();
            }

        }

        function getUrlVars() {
            var vars = {};
            var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (m, key, value) {
                vars[key] = value;
            });
            return vars;
        }

    </script>
</tlk:RadCodeBlock>

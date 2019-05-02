<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ChangeInfoMng.ascx.vb"
    Inherits="Profile.ctrlHU_ChangeInfoMng" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .rcbAutoWidthResizer .rcbScroll
    {
        width: 397px !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">            
            <tlk:RadPane ID="RadPane1" runat="server" Height="72px" Scrolling="None">
                <tlk:RadToolBar ID="tbarWorkings" runat="server" OnClientButtonClicking="clientButtonClicking" />
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />                                 
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkTerminate" runat="server" Text="<%$ Translate: Liệt kê cả nhân viên nghỉ việc %>" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>        
                     
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">                   
                <tlk:RadGrid PageSize="50" ID="rgWorking" runat="server"
                    Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,STATUS_ID,DECISION_TYPE_ID,EMPLOYEE_CODE,DECISION_TYPE_NAME,CODE" ClientDataKeyNames="ID,EMPLOYEE_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px" />
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME">
                                <HeaderStyle Width="200px" />
                                <ItemTemplate>
                                 <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ORG_NAME") %>'>
                                </asp:Label>
                                <tlk:RadToolTip RenderMode="Lightweight" ID="RadToolTip1" runat="server" TargetControlID="Label1"
                                                    RelativeTo="Element" Position="BottomCenter">
                                <%# DrawTreeByString(DataBinder.Eval(Container, "DataItem.ORG_DESC"))%>
                                </tlk:RadToolTip>
                            </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" HeaderStyle-Width="200px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" DataField="STAFF_RANK_NAME"
                                SortExpression="STAFF_RANK_NAME" UniqueName="STAFF_RANK_NAME" HeaderStyle-Width="80px" />
                            <tlk:GridBoundColumn HeaderText="" DataField="DECISION_TYPE_NAME"
                                SortExpression="DECISION_TYPE_NAME" UniqueName="DECISION_TYPE_NAME" HeaderStyle-Width="200px" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                            <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC"
                                SortExpression="ORG_DESC" Visible="false" />
                        </Columns>
                        <HeaderStyle Width="120px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>

<%--            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                
            </tlk:RadPane>--%>
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None" Height="45px" Visible="false">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb" Visible="false">
                            <%# Translate("Biễu mẫu hỗ trợ")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" Width="400px" ID="cboPrintSupport" Visible="false">
                            </tlk:RadComboBox>
                            <asp:CustomValidator ID="cvalPrintSupport" ControlToValidate="cboPrintSupport" runat="server"
                                ErrorMessage="<%$ Translate: Biểu mẫu hỗ trợ không tồn tại hoặc đã ngừng áp dụng. %>"
                                ToolTip="<%$ Translate: Biểu mẫu hỗ trợ không tồn tại hoặc đã ngừng áp dụng. %>">
                            </asp:CustomValidator>      
                        </td>
                        <td>
                            <tlk:RadButton ID="btnPrintSupport" runat="server" Text="<%$ Translate: Hỗ trợ in %>"
                                OnClientClicking="btnPrintSupportClick" AutoPostBack="true" CausesValidation="true"
                                OnClientClicked="btnResize" Visible="false" >
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
           <%-- <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None" Height="45px">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Biễu mẫu hỗ trợ")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" Width="400px" ID="cboPrintSupport">
                            </tlk:RadComboBox>
                            <asp:CustomValidator ID="cvalPrintSupport" ControlToValidate="cboPrintSupport" runat="server"
                                ErrorMessage="<%$ Translate: Biểu mẫu hỗ trợ không tồn tại hoặc đã ngừng áp dụng. %>"
                                ToolTip="<%$ Translate: Biểu mẫu hỗ trợ không tồn tại hoặc đã ngừng áp dụng. %>">
                            </asp:CustomValidator>      
                        </td>
                        <td>
                            <tlk:RadButton ID="btnPrintSupport" runat="server" Text="<%$ Translate: Hỗ trợ in %>"
                                OnClientClicking="btnPrintSupportClick" AutoPostBack="true" CausesValidation="true"
                                OnClientClicked="btnResize" >
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>--%>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
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

        //        function GridCreated(sender, eventArgs) {
        //            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_ChangeInfoMng_RadSplitter3');
        //        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenWage() {
            var extented = '';
            var bCheck = $find('<%= rgWorking.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck > 1) {
                var n = noty({ text: 'Không thể chọn nhiều dòng để thực hiện thao tác này', dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            if (bCheck == 1) {
                empID = $find('<%= rgWorking.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
                if (empID)
                    extented = '&empid=' + empID;
            }

            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_ChangeInfoNewEdit&group=Business' + extented, "_self"); /*
            //var pos = $("html").offset();
            //oWindow.moveTo(pos.left, pos.top);

            var iWidtd = 1300;
            if ($(window).width() < 1300) {
                iWidtd = $(window).width();
            }
            oWindow.setSize(iWidtd, $(window).height());
            oWindow.center(); */
        }

        function OpenWageEdit(e) {
            var bCheck = $find('<%= rgWorking.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            var id = $find('<%= rgWorking.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');

            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_ChangeInfoNewEdit&group=Business&ID=' + id, "_self"); /*
            //var pos = $("html").offset();
            //oWindow.moveTo(pos.left, pos.top);
            var iWidtd = 1300;
            if ($(window).width() < 1300) {
                iWidtd = $(window).width();
            }
            oWindow.setSize(iWidtd, $(window).height());
            oWindow.center(); */
            return 0;
        }

        function clientButtonClicking(sender, args) {
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

            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "PRINT") {
                var bCheck = $find('<%= rgWorking.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                    return;
                }
                if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }

            if (args.get_item().get_commandName() == "EXPIRE_TEMP") {

                var extented = '';
                var bCheck = $find('<%= rgWorking.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                    return;
                }
                empID = $find('<%= rgWorking.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
                if (empID)
                    extented = '&empid=' + empID;


                window.open('/Default.aspx?mid=Profile&fid=ctrlHU_WorkingAllowance&group=Business' + extented, "_self"); /*
                oWindow.setSize(800, 600);
                oWindow.center(); */
                args.set_cancel(true);
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

        function btnPrintSupportClick(sender, args) {
            var bCheck = $find('<%= rgWorking.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                args.set_cancel(true);
                return;
            }
            if (bCheck > 1) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                args.set_cancel(true);
                return;
            }
            enableAjax = false;
        }

        var space = 0;
        var oldSize = 72;

        function pageLoad(sender, args) {
            $(document).ready(function () {
                ResizeSplitter();
            });
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                var pane4 = splitter.getPaneById('<%= RadPane4.ClientID %>');

                var height = pane.getContentElement().scrollHeight;
                var splHeight = splitter.get_height();
                var pane1Height = pane.get_height();
                var pane2Height = pane2.get_height();

                var validateHeight = $("#<%= valSum.ClientID%>").height();
                if (height > oldSize + 1) {
                    space = height - validateHeight - oldSize - 18;
                }

                if (validateHeight != 0) {
                    pane2.set_height(pane2Height - validateHeight - 18);
                }

                pane.set_height(height - space);
                pane4.set_height(45);
            }, 200);
        }

        function btnResize(sender, args) {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
            var validateHeight = $("#<%= valSum.ClientID%>").height();

            var pane2Height = pane2.get_height();

            if (validateHeight != 0) {
                pane2.set_height(pane2Height + validateHeight + 18);
            }

            pane.set_height(oldSize);
        }

    </script>
</tlk:RadCodeBlock>

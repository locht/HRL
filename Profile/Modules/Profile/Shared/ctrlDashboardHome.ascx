<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDashboardHome.ascx.vb" 
Inherits="Profile.ctrlDashboardHome" %>

<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal"
    SkinID="Demo">
     
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid  ID="rgContract" runat="server" Height="100%" AllowFilteringByColumn="true">
            <MasterTableView CommandItemSettings-ExportToExcelText="Xuất dữ liệu"
            CommandItemSettings-ExportToCsvText="Chuyển" CommandItemSettings-ExportToPdfText="Báo tăng thai sản đi làm lại" CommandItemDisplay="Top"
             ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,FULLNAME,REMIND_DATE,GENDER,REMIND_TYPE,REMIND_NAME,USERNAME,TITLE_NAME,ORG_NAME,JOIN_DATE,VALUE,LINK_POPUP,WORK_EMAIL">
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton ID="btnSendMail" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/send_email.png"
                                CausesValidation="false" Width="85px" Text="<%$ Translate: Gửi mail %>" ToolTip="MAIL"
                                CommandName="SendMail" OnClientClicking="SelectGridClick">
                            </tlk:RadButton>
                        </div>
                        <div style="float: left">
                            <tlk:RadButton ID="btnExport" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/export1.png"
                                CausesValidation="false" Width="70px" Text="<%$ Translate: Excel %>" ToolTip="Export"
                                CommandName="EXPORT" OnClientClicking="SelectGridClick">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <CommandItemSettings  ShowAddNewRecordButton="false" ShowExportToCsvButton="false"
                    ShowRefreshButton="false" ShowExportToExcelButton="true"></CommandItemSettings>
                <Columns>
                    <tlk:GridClientSelectColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px"
                        ItemStyle-HorizontalAlign="Center" UniqueName="cbStatus">
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridClientSelectColumn>
                    <tlk:GridTemplateColumn UniqueName="LINK_POPUP" AllowFiltering="false" HeaderText="<%$ Translate: Liên kết nhanh %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnLink" runat="server" Text="Xem" Style="text-decoration: underline !important;
                                color: Blue">
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle Width="90px" />
                    </tlk:GridTemplateColumn>
                    <tlk:GridBoundColumn DataField="REMIND_TYPE" Visible="false" UniqueName="REMIND_TYPE"
                        SortExpression="REMIND_TYPE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại nhắc nhở %>" DataField="REMIND_NAME"
                        UniqueName="REMIND_NAME" SortExpression="REMIND_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hết hạn %>" DataField="REMIND_DATE"
                        UniqueName="REMIND_DATE" SortExpression="REMIND_DATE" DataFormatString="{0:dd/MM/yyyy}">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="FULLNAME"
                        UniqueName="FULLNAME" SortExpression="FULLNAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                        UniqueName="ORG_NAME" SortExpression="ORG_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITILE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Email %>" DataField="WORK_EMAIL"
                        UniqueName="WORK_EMAIL" SortExpression="WORK_EMAIL">
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <ClientEvents OnCommand="RaiseCommand" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oWnd1;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function RaiseCommand(sender, eventArgs) {
            var item = eventArgs.get_commandName();
            if (item == "ExportToExcel") {
                enableAjax = false;
            }
        }

        function POPUP(value) {
            var RadWinManager = GetRadWindow().get_windowManager();
            RadWinManager.set_visibleTitlebar(true);
            var oWindow = RadWinManager.open(value);
            var pos = $("html").offset();
            oWindow.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close); //set the desired behavioursvar pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize(parent.document.body.clientWidth, parent.document.body.clientHeight);
            setTimeout(function () { oWindow.setActive(true); }, 0);
            return false;
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function GetRadWindow() {
            var oWindow = null;

            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;

            return oWindow;
        }
        function SelectGridClick(sender, args) {
            var item = args.get_commandName();
            if (item == "SendMail") {
                var bCheck = $find('<%# rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var notifyMgs = Notify("Vui lòng chọn dữ liệu thao tác", "warning");
                    args.set_cancel(true);
                    enableAjax = true;
                }
                else {
                    enableAjax = false;
                }
            }
            else if (item == "EXPORT") {
                enableAjax = false;
            }
            else
                enableAjax = true;
        }
    </script>
</tlk:RadScriptBlock>

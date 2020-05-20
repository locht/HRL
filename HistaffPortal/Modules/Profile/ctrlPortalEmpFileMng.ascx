<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalEmpFileMng.ascx.vb"
    Inherits="Profile.ctrlPortalEmpFileMng" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Height="100%"
        Scrolling="None">
        <Common:ctrlFolders ID="ctrlFD" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Width="100%" Scrolling="None">
                <tlk:RadToolBar ID="tbarContracts" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" Height="100%" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgHealth" Width="100%" runat="server" Height="350px"
                    AllowFilteringByColumn="false" Scrolling="both">
                    <MasterTableView DataKeyNames="ID">
                        <Columns>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên file %>" DataField="NAME" AllowFiltering="false"
                                ReadOnly="true" UniqueName="NAME" HeaderStyle-Width="200px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="DESCRIPTION"
                                AllowFiltering="false" ReadOnly="true" UniqueName="DESCRIPTION" HeaderStyle-Width="200px" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày tạo %>" DataField="CREATED_DATE"
                                AllowFiltering="false" ItemStyle-HorizontalAlign="Center" SortExpression="CREATED_DATE"
                                UniqueName="CREATED_DATE" DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người tạo %>" DataField="CREATED_BY"
                                AllowFiltering="false" ReadOnly="true" UniqueName="CREATED_BY" HeaderStyle-Width="200px" />
                            <tlk:GridTemplateColumn AllowFiltering="false">
                                <ItemTemplate>
                                    <tlk:RadButton runat="server" Text="Download" ID="btnDownload" OnClientClicked="rbtClicked" CommandName="DownloadFile"
                                        CommandArgument='<%# Eval("ID") %>'>
                                    </tlk:RadButton>
                                    <tlk:RadButton runat="server" Text="Delete" ID="btnDelete" CommandName="DeleteFile"
                                        CommandArgument='<%# Eval("ID") %>'>
                                    </tlk:RadButton>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="500px"
            OnClientClose="OnClientClose" Height="300px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Tạo hợp đồng %>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OpenNew(_id) {
            var url = 'Dialog.aspx?mid=Profile&fid=ctrlFoldersNewEdit&PrID=' + _id + '&noscroll=1&reload=1&FormType=0';
            var oWindow = radopen(url, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.center);
            oWindow.setSize("500px", "200px");
            oWindow.center();
            return 0;
        }


        function OpenEditFolder(_id) {
            var url = 'Dialog.aspx?mid=Profile&fid=ctrlFoldersNewEdit&ID=' + _id + '&noscroll=1&reload=1&FormType=0';
            var oWindow = radopen(url, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.center);
            oWindow.setSize("500px", "200px");
        }

        function OpenAddFile(_id) {
            var url = "Default.aspx?mid=Profile&fid=ctrlUserFileNewEdit&FoID=" + _id;
            window.location.href = url;
        }
        function clientButtonClicking(sender, args) {
            var ctrFolder = document.getElementById('ctl00_MainContent_ctrlPortalEmpFileMng_ctrlFD_trvOrgPostback').value;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew(ctrFolder);
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'SUBMIT') {
                OpenAddFile(ctrFolder);
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "EDIT") {
                OpenEditFolder(ctrFolder);
                args.set_cancel(true);
            }
        }
        function OnClientClose(oWnd, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgHealth.ClientID %>").get_masterTableView().rebind();
            }
            if (arg == '2') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                location.reload();
            }
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>

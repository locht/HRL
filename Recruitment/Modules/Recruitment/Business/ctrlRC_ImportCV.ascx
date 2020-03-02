<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ImportCV.ascx.vb"
    Inherits="Recruitment.ctrlRC_ImportCV" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidProgramID" runat="server" />
<asp:HiddenField ID="lstID" runat="server" />
<asp:HiddenField ID="CandidateID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,ID_NO,FIRST_NAME_VN,LAST_NAME_VN,ERROR">
                <Columns>
                     <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <%--<tlk:GridBoundColumn DataField="IS_CMND" Display="false" SortExpression="IS_CMND" UniqueName="IS_CMND" HeaderStyle-Width="120px" />--%>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: CMND %>" DataField="ID_NO"
                        SortExpression="ID_NO" UniqueName="ID_NO" HeaderStyle-Width="120px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                        SortExpression="BIRTH_DATE" UniqueName="BIRTH_DATE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên lót %>" DataField="FIRST_NAME_VN" SortExpression="FIRST_NAME_VN"
                        UniqueName="FIRST_NAME_VN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên %>" DataField="LAST_NAME_VN"
                        SortExpression="LAST_NAME_VN" UniqueName="LAST_NAME_VN" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Chi tiết lỗi/Cảnh báo %>" DataField="S_ERROR"
                        UniqueName="S_ERROR" SortExpression="S_ERROR" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Tên file %>" DataField="FILE_NAME"
                        SortExpression="FILE_NAME" UniqueName="FILE_NAME" />
                </Columns>
                <HeaderStyle Width="150px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<asp:HiddenField ID="hddLinkPopup" runat="server" Value="Dialog.aspx?mid=Recruitment&fid=ctrlRC_CandidateReasonChange&group=Business&noscroll=1" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            OnClientBeforeClose="OnClientBeforeClose" Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<common:ctrlupload id="ctrlUpload" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }


        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;

            if (args.get_item().get_commandName() == "SAVE") {
                var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return;
                }
                if (bCheck > 1) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return;
                }
                var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                getRadWindow().close('TRANSFER;' + id);
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }

    </script>
</tlk:RadCodeBlock>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSwipeDataDownload.ascx.vb"
    Inherits="Attendance.ctrlSwipeDataDownload" %>
    <%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="75px" Scrolling="None">
        <tlk:RadToolBar ID="rtbMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdStartDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEndDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td>
                    <tlk:RadButton ID="btnSearchEmp" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rglSwipeDataDownload" runat="server" Height="100%" AllowSorting="True"
            AllowMultiRowSelection="true" AllowPaging="True" AutoGenerateColumns="False">
            <ClientSettings>
                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ITIME_ID" ClientDataKeyNames="VALTIME,ITIME_ID,TERMINAL_CODE">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="Mã CC" DataField="ITIME_ID_S" SortExpression="ITIME_ID_S"
                        UniqueName="ITIME_ID_S">
                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày quẹt thẻ %>" DataField="WORKINGDAY"
                        SortExpression="WORKINGDAY" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}"
                        UniqueName="WORKINGDAY">
                        <HeaderStyle Width="30%" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn AllowFiltering="false" HeaderText="<%$ Translate:Thời gian quẹt thẻ %>"
                        DataField="VALTIME" UniqueName="VALTIME" DataFormatString="{0:HH:mm}" DataType="System.DateTime"
                        PickerType="TimePicker" SortExpression="VALTIME">
                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<script type="text/javascript">
    var enableAjax = true;
    var splitterID = 'ctl00_MainContent_ctrlSwipeDataDownload_RadSplitter3';
    var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlSwipeDataDownload_RadPane1';
    var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlSwipeDataDownload_RadPane2';
    var validateID = 'MainContent_ctrlSwipeDataDownload_valSum';
    var oldSize = $('#' + pane1ID).height();
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
        registerOnfocusOut(splitterID);
    }
    function OnClientButtonClicking(sender, args) {
        var item = args.get_item();
        if (args.get_item().get_commandName() == 'EXPORT') {
            var rows = $find('<%= rglSwipeDataDownload.ClientID %>').get_masterTableView().get_dataItems().length;
            if (rows == 0) {
                var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
                return;
            }
            enableAjax = false;
        } 
    }
   
    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }
</script>

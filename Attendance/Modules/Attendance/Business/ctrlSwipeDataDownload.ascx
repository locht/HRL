<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSwipeDataDownload.ascx.vb"
    Inherits="Attendance.ctrlSwipeDataDownload" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="rtbMain" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb">
                    <asp:Label ID="lbMachine_Type" runat ="server"  Text ="Hệ thống chấm công"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat ="server" ID="cbMachine_Type">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbStartDate" runat ="server"  Text ="Từ ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdStartDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbEndDate" runat ="server"  Text ="Đến ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEndDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td>
                    <tlk:RadButton ID="btnSearchEmp" runat="server" Text="<%$ Translate: Tìm kiếm %>">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rglSwipeDataDownload" runat="server" Height="100%" AllowSorting="True"
            AllowMultiRowSelection="true" AllowPaging="True" AutoGenerateColumns="False">
            <ClientSettings>
                <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ITIME_ID" ClientDataKeyNames="VALTIME,ITIME_ID">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="Mã CC" DataField="ITIME_ID_S" SortExpression="ITIME_ID_S"
                        UniqueName="ITIME_ID_S">
                        <HeaderStyle HorizontalAlign="Center" Width="300px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày quẹt thẻ %>" DataField="WORKINGDAY"
                        SortExpression="WORKINGDAY" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}"
                        UniqueName="WORKINGDAY">
                        <HeaderStyle Width="300px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn AllowFiltering="false" HeaderText="<%$ Translate:Thời gian quẹt thẻ %>"
                        DataField="VALTIME" UniqueName="VALTIME" DataFormatString="{0:HH:mm}" DataType="System.DateTime"
                        PickerType="TimePicker" SortExpression="VALTIME">
                        <HeaderStyle HorizontalAlign="Center" />
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
    var oldSize = 0;
    function OnClientButtonClicking(sender, args) {
        var item = args.get_item();
        if (item.get_commandName() == "EXPORT") {
            enableAjax = false;
        } else if (item.get_commandName() == "SAVE") {
            // Nếu nhấn nút SAVE thì resize
            ResizeSplitter();
        } else {
            // Nếu nhấn các nút khác thì resize default
            ResizeSplitterDefault();
        }
    }

    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }
    // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
    function ResizeSplitter() {
        setTimeout(function () {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
            var height = pane.getContentElement().scrollHeight;
            splitter.set_height(splitter.get_height() + pane.get_height() - height);
            pane.set_height(height);
        }, 200);
    }
    // Hàm khôi phục lại Size ban đầu cho Splitter
    function ResizeSplitterDefault() {
        var splitter = $find("<%= RadSplitter3.ClientID%>");
        var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
        if (oldSize == 0) {
            oldSize = pane.getContentElement().scrollHeight;
        } else {
            var pane2 = splitter.getPaneById('<%= RadPane1.ClientID %>');
            splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
            pane.set_height(oldSize);
            pane2.set_height(splitter.get_height() - oldSize - 1);
        }
    }
</script>

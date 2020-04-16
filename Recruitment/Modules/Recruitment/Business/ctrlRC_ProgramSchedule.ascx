<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ProgramSchedule.ascx.vb"
    Inherits="Recruitment.ctrlRC_ProgramSchedule" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="150px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <asp:HiddenField ID="hidOrg" runat="server" />
        <asp:HiddenField ID="hidTitle" runat="server" />
        <asp:HiddenField ID="hidProgramID" runat="server" />
        <asp:Panel ID="Panel1" runat="server">
            <table class="table-form padding-10">
                <tr>
                    <td colspan="8">
                        <b>
                            <%# Translate("Thông tin chương trình tuyển dụng")%>
                        </b>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Phòng ban yêu cầu")%>:
                    </td>
                    <td>
                        <asp:Label ID="lblOrgName" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="lb">
                        <%# Translate("Ngày gửi yêu cầu")%>:
                    </td>
                    <td>
                        <asp:Label ID="lblSendDate" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="lb">
                        <%# Translate("Mã tuyển dụng")%>:
                    </td>
                    <td>
                        <asp:Label ID="lblCode" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="lb"  style="display : none">
                        <%# Translate("Tên công việc")%>:
                    </td>
                    <td  style="display : none">
                        <asp:Label ID="lblJobName" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                                   
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Vị trí tuyển dụng")%>:
                    </td>
                    <td>
                        <asp:Label ID="lblTitle" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="lb">
                        <%# Translate("Hồ sơ đã nhận")%>:
                    </td>
                    <td>
                        <asp:Label ID="lblCandidate" runat="server" Font-Bold="true"></asp:Label>
                    </td>     
                    <td class="lb">
                        <%# Translate("Số lượng cần tuyển")%>:
                    </td>
                    <td>
                        <asp:Label ID="lblRequestNo" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="lb">
                        <%# Translate("Số lượng đã tuyển")%>:
                    </td>
                    <td>
                        <asp:Label ID="lblCanReceivedCount" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày phỏng vấn %>" DataField="SCHEDULE_DATE" HeaderStyle-Width="120px"
                        UniqueName="SCHEDULE_DATE" SortExpression="SCHEDULE_DATE" DataFormatString="{0:dd/MM/yyyy}">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Vòng phỏng vấn%>" DataField="EXAMS_NAME"
                        UniqueName="EXAMS_NAME" SortExpression="EXAMS_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: SL ứng viên đã lên lịch phỏng vấn %>" DataField="CANDIDATE_COUNT" HeaderStyle-Width="200px"
                        UniqueName="CANDIDATE_COUNT" SortExpression="CANDIDATE_COUNT" ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            OnClientBeforeClose="OnClientBeforeClose" Modal="true" ShowContentDuringLoad="false"
            Title="<%$ Translate: Thông tin lên lịch thi tuyển %>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

        function OpenEditWindow(states) {
            var gUId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');

            var programID = $get("<%=hidProgramID.ClientID %>").value;

            var oWindow = radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_ProgramScheduleNewEdit&group=Business&PROGRAM_ID='
            + programID + '&SCHEDULE_ID=' + gUId + '&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());

        }

        function OnClientBeforeClose(sender, eventArgs) {
            var arg = eventArgs.get_argument();
            if (!arg) {
                if (!confirm("Bạn có muốn đóng màn hình không?")) {
                    //if cancel is clicked prevent the window from closing
                    eventArgs.set_cancel(true);
                }
            }
        }

        function OpenInsertWindow() {
            var programID = $get("<%=hidProgramID.ClientID %>").value;
            var oWindow = radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_ProgramScheduleNewEdit&group=Business&PROGRAM_ID='
            + programID + '&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow("Edit");
                    args.set_cancel(true);
                }

            } else if (args.get_item().get_commandName() == 'EXPORT' || args.get_item().get_commandName() == 'PRINT' || args.get_item().get_commandName() == 'UNLOCK' || args.get_item().get_commandName() == 'PREVIOUS') {
                enableAjax = false;
            }
        }
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientClose(oWnd, args) {
            //window.location.reload(true);
            $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
        }

        function postBack(url) {
            var ajaxManager = $find("<%# AjaxManagerId %>");
            ajaxManager.ajaxRequest(url); //Making ajax request with the argument
        }

    </script>
</tlk:RadScriptBlock>

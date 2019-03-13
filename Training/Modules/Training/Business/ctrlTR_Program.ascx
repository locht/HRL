<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_Program.ascx.vb"
    Inherits="Training.ctrlTR_Program" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="40px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                Text="<%$ Translate: Tìm %>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true" Width="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowClick="gridRowClick" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR" SortExpression="YEAR"
                                UniqueName="YEAR" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Khóa đào tạo %>" DataField="TR_COURSE_NAME"
                                SortExpression="TR_COURSE_NAME" UniqueName="TR_COURSE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên chương trình đào tạo %>" DataField="NAME"
                                SortExpression="NAME" UniqueName="NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm chương trình %>" DataField="PROGRAM_GROUP"
                                SortExpression="PROGRAM_GROUP" UniqueName="PROGRAM_GROUP" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lĩnh vực đào tạo %>" DataField="TRAIN_FIELD"
                                SortExpression="TRAIN_FIELD" UniqueName="TRAIN_FIELD" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức đào tạo %>" DataField="TRAIN_FORM_NAME"
                                SortExpression="TRAIN_FORM_NAME" UniqueName="TRAIN_FORM_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị tham gia %>" DataField="Departments_NAME"
                                SortExpression="Departments_NAME" UniqueName="Departments_NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="Titles_NAME"
                                SortExpression="Titles_NAME" UniqueName="Titles_NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời lượng %>" DataField="DURATION"
                                SortExpression="DURATION" UniqueName="DURATION" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="START_DATE"
                                SortExpression="START_DATE" UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="END_DATE"
                                SortExpression="END_DATE" UniqueName="END_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số buổi học trong giờ hành chính %>"
                                DataField="DURATION_HC" SortExpression="DURATION_HC" UniqueName="DURATION_HC" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số buổi học ngoài giờ hành chính %>"
                                DataField="DURATION_OT" SortExpression="DURATION_OT" UniqueName="DURATION_OT" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số học viên theo kế hoạch %>" DataField="PLAN_STUDENT_NUMBER"
                                SortExpression="PLAN_STUDENT_NUMBER" UniqueName="PLAN_STUDENT_NUMBER" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng chi phí theo kế hoạch (VNĐ) %>"
                                DataField="PLAN_COST_TOTAL" SortExpression="PLAN_COST_TOTAL" UniqueName="PLAN_COST_TOTAL"
                                DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng chi phí theo kế hoạch (USD) %>"
                                DataField="PLAN_COST_TOTAL_US" SortExpression="PLAN_COST_TOTAL_US" UniqueName="PLAN_COST_TOTAL_US"
                                DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số học viên thực tế %>" DataField="STUDENT_NUMBER"
                                SortExpression="STUDENT_NUMBER" UniqueName="STUDENT_NUMBER" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng chi phí thực tế (VNĐ) %>" DataField="COST_TOTAL"
                                SortExpression="COST_TOTAL" UniqueName="COST_TOTAL" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi phí 1 học viên (VNĐ) %>" DataField="COST_STUDENT"
                                SortExpression="COST_STUDENT" UniqueName="COST_STUDENT" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng chi phí thực tế (USD) %>" DataField="COST_TOTAL_US"
                                SortExpression="COST_TOTAL_US" UniqueName="COST_TOTAL_US" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi phí 1 học viên (USD) %>" DataField="COST_STUDENT_US"
                                SortExpression="COST_STUDENT_US" UniqueName="COST_STUDENT_US" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Có thi lại %>" DataField="IS_RETEST"
                                SortExpression="IS_RETEST" UniqueName="IS_RETEST" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngôn ngữ %>" DataField="TR_LANGUAGE_NAME"
                                SortExpression="TR_LANGUAGE_NAME" UniqueName="TR_LANGUAGE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trung tâm đào tạo %>" DataField="Centers_NAME"
                                SortExpression="Centers_NAME" UniqueName="Centers_NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị chủ trì đào tạo %>" DataField="TR_UNIT_NAME"
                                SortExpression="TR_UNIT_NAME" UniqueName="TR_UNIT_NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung %>" DataField="CONTENT"
                                SortExpression="CONTENT" UniqueName="CONTENT" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mục đích %>" DataField="TARGET_TRAIN"
                                SortExpression="TARGET_TRAIN" UniqueName="TARGET_TRAIN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa điểm %>" DataField="VENUE" SortExpression="VENUE"
                                UniqueName="VENUE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                                UniqueName="REMARK" />
                        </Columns>
                        <HeaderStyle Width="150px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="50px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnPrepare" runat="server" Text="<%$ Translate: Chuẩn bị khóa học %>"
                                OnClientClicking="btnPrepareClick" AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnClass" runat="server" Text="<%$ Translate: Thông tin lớp học %>"
                                OnClientClicking="btnClassClick" AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnCommit" runat="server" Text="<%$ Translate: Cam kết đào tạo %>"
                                OnClientClicking="btnCommitClick" AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnResult" runat="server" Text="<%$ Translate: Cập nhật kết quả %>"
                                OnClientClicking="btnResultClick" AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var selectedID = -1;
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenNew() {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 1)
                window.open('/Default.aspx?mid=Training&fid=ctrlTR_ProgramNewEdit&group=Business&ID=' + selectedID, "_self");
            else
                window.open('/Default.aspx?mid=Training&fid=ctrlTR_ProgramNewEdit&group=Business&ID=' + selectedID, "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
        }

        function OpenEdit() {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Training&fid=ctrlTR_ProgramNewEdit&group=Business&ID=' + id, "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
            return 2;
        }

        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = OpenEdit();
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck == 1) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }

                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'SENDMAIL') {
                window.open('/Default.aspx?mid=Training&fid=ctrlTR_ProgramNotify&group=Business', "_self"); /*
                var pos = $("html").offset();
                oWindow.moveTo(pos.left, pos.top);
                oWindow.setSize(300, 50);
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'DELETE' ||
            args.get_item().get_commandName() == 'APROVE' ||
            args.get_item().get_commandName() == 'REJECT') {
                bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
            }
            if (args.get_item().get_commandName() == "CREATE_CP") {
                bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck > 1) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                window.open('/Default.aspx?mid=Training&fid=ctrlTR_ProgramCost&group=Business&PROGRAM_ID=' + id, "_self"); /*
                var pos = $("html").offset();
                oWindow.moveTo(pos.left, pos.top);
                oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */

                args.set_cancel(true);
            }
        }

        function gridRowClick(sender, eventArgs) {
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            selectedID = id;
            return 0;
        }

        function gridRowDblClick(sender, eventArgs) {
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');

            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_ProgramNewEdit&group=Business&ID=' + id + '&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30);
            oWindow.center();

            return 0;
        }

        function OnClientClose(oWnd, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgData.ClientID %>").get_masterTableView().rebind();

                oWnd = $find('<%#popupId %>');
                oWnd.setSize(screen.width - 250, screen.height - 300);
                oWnd.remove_close(OnClientClose);
                var arg = args.get_argument();
                if (arg) {
                    postBack(arg);
                }
            }
        }


        function btnPrepareClick(sender, args) {
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
            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_ProgramPrepare&group=Business&PROGRAM_ID=' + id + '&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30);
            oWindow.center();
        }
        function btnClassClick(sender, args) {

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
            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_ProgramClass&group=Business&PROGRAM_ID=' + id + '&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30);
            oWindow.center();
        }

        function btnCommitClick(sender, args) {
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

            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_ProgramCommit&group=Business&PROGRAM_ID=' + id + '&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30);
            oWindow.center();
        }
        function btnResultClick(sender, args) {
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

            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_ProgramResult&group=Business&PROGRAM_ID=' + id + '&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30);
            oWindow.center();
        }

        function OpenInNewTab(url) {
            var win = window.open(url, '_blank');
            win.focus();
        }
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
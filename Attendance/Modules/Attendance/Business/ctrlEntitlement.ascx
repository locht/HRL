<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlEntitlement.ascx.vb"
    Inherits="Attendance.ctrlEntitlement" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="50px">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="true"
                                TabIndex="12" Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ công")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" Width="150px" MaxLength="80"
                                runat="server" ToolTip="">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Liệt kê cả nhân viên nghỉ việc %>" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgEntitlement" runat="server" Height="100%" Scrolling="None">
                   
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,ORG_DESC">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULLNAME_VN"
                                UniqueName="FULLNAME_VN" HeaderStyle-Width="120px" SortExpression="FULLNAME_VN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME_VN"
                                UniqueName="TITLE_NAME_VN" HeaderStyle-Width="120px" SortExpression="TITLE_NAME_VN" />
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" DataField="STAFF_RANK_NAME"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" SortExpression="STAFF_RANK_NAME"
                                UniqueName="STAFF_RANK_NAME" />--%>
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
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày vào công ty %>" DataField="JOIN_DATE_STATE"
                                UniqueName="JOIN_DATE_STATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="JOIN_DATE_STATE">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian thâm niên %>"
                                DataField="SENIORITY" ItemStyle-HorizontalAlign="Center"
                                SortExpression="SENIORITY" UniqueName="SENIORITY" HeaderStyle-Width="120px">
                            </tlk:GridBoundColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tháng thâm niên điều chỉnh %>"
                                DataField="MONTH_SENIORITY_CHANGE" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="MONTH_SENIORITY_CHANGE" UniqueName="MONTH_SENIORITY_CHANGE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Thời gian thâm niên sau điều chỉnh %>"
                                DataField="TIME_SENIORITY_AFTER_CHANGE" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="TIME_SENIORITY_AFTER_CHANGE" UniqueName="TIME_SENIORITY_AFTER_CHANGE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Phép năm trước còn lại chuyển sang %>"
                                DataField="PREV_HAVE" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="PREV_HAVE" UniqueName="PREV_HAVE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Phép năm trước đã nghỉ %>"
                                DataField="PREV_USED" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="PREV_USED" UniqueName="PREV_USED" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                             <tlk:GridNumericColumn HeaderText="<%$ Translate:Phép năm trước còn lại được sử dụng %>"
                                DataField="PREVTOTAL_HAVE" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="PREVTOTAL_HAVE" UniqueName="PREVTOTAL_HAVE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                             <tlk:GridNumericColumn HeaderText="<%$ Translate:Phép thâm niên %>"
                                DataField="SENIORITYHAVE" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="SENIORITYHAVE" UniqueName="SENIORITYHAVE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Phép chuẩn năm nay %>"
                                DataField="TOTAL_HAVE1" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="TOTAL_HAVE1" UniqueName="TOTAL_HAVE1" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                           
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Quỹ phép năm nay %>"
                                DataField="TOTAL_CUR_HAVE" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="TOTAL_CUR_HAVE" UniqueName="TOTAL_CUR_HAVE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Phép năm nay đã sử dụng %>"
                                DataField="CUR_USED" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="CUR_USED" UniqueName="CUR_USED" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số phép năm còn lại %>" DataField="CUR_HAVE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_HAVE"
                                UniqueName="CUR_HAVE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Phép trừ từ số ngày ngoài cơ quan %>" DataField="TIME_OUTSIDE_COMPANY"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="TIME_OUTSIDE_COMPANY"
                                UniqueName="TIME_OUTSIDE_COMPANY" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép đã nghỉ trong tháng 01 %>" DataField="CUR_USED1"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED1"
                                UniqueName="CUR_USED1">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép đã nghỉ trong tháng 02 %>" DataField="CUR_USED2"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED2"
                                UniqueName="CUR_USED2">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép đã nghỉ trong tháng 03 %>" DataField="CUR_USED3"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED3"
                                UniqueName="CUR_USED3">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép đã nghỉ trong tháng 04 %>" DataField="CUR_USED4"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED4"
                                UniqueName="CUR_USED4">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép đã nghỉ trong tháng 05 %>" DataField="CUR_USED5"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED5"
                                UniqueName="CUR_USED5">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép đã nghỉ trong tháng 06 %>" DataField="CUR_USED6"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED6"
                                UniqueName="CUR_USED6">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép đã nghỉ trong tháng 07 %>" DataField="CUR_USED7"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED7"
                                UniqueName="CUR_USED7">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép đã nghỉ trong tháng 08 %>" DataField="CUR_USED8"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED8"
                                UniqueName="CUR_USED8">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép đã nghỉ trong tháng 09 %>" DataField="CUR_USED9"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED9"
                                UniqueName="CUR_USED9">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép đã nghỉ trong tháng 10 %>" DataField="CUR_USED10"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED10"
                                UniqueName="CUR_USED10">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép đã nghỉ trong tháng 11 %>" DataField="CUR_USED11"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED11"
                                UniqueName="CUR_USED11">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép đã nghỉ trong tháng 12 %>" DataField="CUR_USED12"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED12"
                                UniqueName="CUR_USED12">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép bị trừ từ số ngày ngoài cơ quan tháng 01 %>" DataField="AL_T1"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="AL_T1"
                                UniqueName="AL_T1">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép bị trừ từ số ngày ngoài cơ quan tháng 02 %>" DataField="AL_T2"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="AL_T2"
                                UniqueName="AL_T2">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép bị trừ từ số ngày ngoài cơ quan tháng 03 %>" DataField="AL_T3"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="AL_T3"
                                UniqueName="AL_T3">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép bị trừ từ số ngày ngoài cơ quan tháng 04 %>" DataField="AL_T4"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="AL_T4"
                                UniqueName="AL_T4">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép bị trừ từ số ngày ngoài cơ quan tháng 05 %>" DataField="AL_T5"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="AL_T5"
                                UniqueName="AL_T5">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép bị trừ từ số ngày ngoài cơ quan tháng 06 %>" DataField="AL_T6"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="AL_T6"
                                UniqueName="AL_T6">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép bị trừ từ số ngày ngoài cơ quan tháng 07 %>" DataField="AL_T7"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="AL_T7"
                                UniqueName="AL_T7">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép bị trừ từ số ngày ngoài cơ quan tháng 08 %>" DataField="AL_T8"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="AL_T8"
                                UniqueName="AL_T8">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép bị trừ từ số ngày ngoài cơ quan tháng 09 %>" DataField="AL_T9"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="AL_T9"
                                UniqueName="AL_T9">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép bị trừ từ số ngày ngoài cơ quan tháng 10 %>" DataField="AL_T10"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="AL_T10"
                                UniqueName="AL_T10">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép bị trừ từ số ngày ngoài cơ quan tháng 11 %>" DataField="AL_T11"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="AL_T11"
                                UniqueName="AL_T11">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày phép bị trừ từ số ngày ngoài cơ quan tháng 12 %>" DataField="AL_T12"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="AL_T12"
                                UniqueName="AL_T12">
                            </tlk:GridNumericColumn>
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var splitterID = 'ctl00_MainContent_ctrlEntitlement_RadSplitter3';
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
        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }

        function OpenEditWindow(states) {


        }

        function OpenInsertWindow() {


        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgENTITLEMENT.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow("Edit");
                    args.set_cancel(true);
                }
            } else if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgEntitlement.ClientID %>");
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                if (rows.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }
}

function OnClientClose(oWnd, args) {
    postBack(oWnd.get_navigateUrl());
}

function postBack(url) {
    var ajaxManager = $find("<%= AjaxManagerId %>");
            ajaxManager.ajaxRequest(url); //Making ajax request with the argument
        }

    </script>
</tlk:RadScriptBlock>

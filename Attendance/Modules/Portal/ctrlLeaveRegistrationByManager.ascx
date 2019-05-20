﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlLeaveRegistrationByManager.ascx.vb"
    Inherits="Attendance.ctrlLeaveRegistrationByManager
    " %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidValid" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="40px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="txtYear" runat="server" SkinID="Number" NumberFormat-GroupSeparator=""
                                ShowSpinButtons="true" MaxLength="4" MinValue="2000" NumberFormat-DecimalDigits="0"
                                Width="60px">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trạng thái")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboStatus">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm kiếm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgMain" runat="server" Height="100%" Width="100%" AllowPaging="true"
                    AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID, ID_EMPLOYEE, EMPLOYEE_CODE, EMPLOYEE_NAME, DEPARTMENT, JOBTITLE, 
                 YEAR,FROM_DATE, TO_DATE, ID_SIGN, SIGN_CODE,SIGN_NAME, TOTAL_LEAVE, NOTE, 
                 STATUS, STATUS_NAME" ClientDataKeyNames="ID, ID_EMPLOYEE, EMPLOYEE_CODE, EMPLOYEE_NAME, DEPARTMENT, JOBTITLE, 
                 YEAR,FROM_DATE, TO_DATE, ID_SIGN, SIGN_CODE,SIGN_NAME, TOTAL_LEAVE, NOTE, 
                 STATUS, STATUS_NAME">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" HeaderStyle-Width="130px"
                                ItemStyle-Width="130px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="80px"
                                ItemStyle-Width="80px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" HeaderStyle-Width="140px"
                                ItemStyle-Width="140px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="DEPARTMENT"
                                UniqueName="DEPARTMENT" SortExpression="DEPARTMENT" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="200px" ItemStyle-Width="200px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="JOBTITLE"
                                UniqueName="JOBTITLE" SortExpression="JOBTITLE" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="270px" ItemStyle-Width="270px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại nghỉ %>" DataField="SIGN_NAME"
                                UniqueName="SIGN_NAME" SortExpression="SIGN_NAME" HeaderStyle-Width="200px" ItemStyle-Width="200px" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Thời gian bắt đầu nghỉ phép %>"
                                DataField="FROM_DATE" UniqueName="FROM_DATE" SortExpression="FROM_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Thời gian kết thúc nghỉ phép %>"
                                DataField="TO_DATE" UniqueName="TO_DATE" SortExpression="TO_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày %>" DataField="TOTAL_LEAVE"
                                UniqueName="TOTAL_LEAVE" SortExpression="TOTAL_LEAVE" ItemStyle-HorizontalAlign="Right"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày trong kế hoạch %>" DataField=""
                                UniqueName="" SortExpression="" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày ngoài kế hoạch %>" DataField=""
                                UniqueName="" SortExpression="" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridNumericColumn>
                               <tlk:GridBoundColumn HeaderText="<%$ Translate: Lí do nghỉ phép %>" DataField="NOTE"
                                UniqueName="NOTE" SortExpression="NOTE" ItemStyle-Width="200px">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người cập nhật %>" DataField="MODIFIED_BY"
                                UniqueName="MODIFIED_BY" SortExpression="MODIFIED_BY" ItemStyle-Width="140px">
                                <HeaderStyle Width="140px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cập nhật %>" DataField="MODIFIED_DATE"
                                UniqueName="MODIFIED_DATE" SortExpression="MODIFIED_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do không duyệt %>" DataField="REASON"
                                UniqueName="REASON" SortExpression="REASON" HeaderStyle-Width="200px" ItemStyle-Width="200px" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlCommon_Reject ID="ctrlCommon_Reject" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function OnClientClose(oWnd, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgMain.ClientID %>").get_masterTableView().rebind();
            }
        }

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

        function gridRowDblClick(sender, args) {
            debugger;
            var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                //args.set_cancel(true);
            }
            else if (bCheck > 1) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                //args.set_cancel(true);
            }
            else {

                var id = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                var empId = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
                OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlLeaveRegistrationNewEdit&id=' + id + '&typeUser=LM&empId=' + empId);
                //args.set_cancel(true);
            }
        }

        function OpenInNewTab(url) {
            window.location.href = url;
        }
    </script>
</tlk:RadCodeBlock>

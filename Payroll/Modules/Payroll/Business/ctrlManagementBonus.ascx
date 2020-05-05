﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlManagementBonus.ascx.vb"
    Inherits="Payroll.ctrlManagementBonus" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane4" runat="server" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None" Height="100%">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPaneTop" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMenu" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="50px" Scrolling="None">
                <table class="table-form" style="margin-top: 10px">
                    <tr>
                        <td>
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" runat="server" SkinID="dDropdownList" AutoPostBack="true"
                                Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ lương")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" runat="server" SkinID="dDropdownList">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb" style="display: none">
                            <%# Translate("SALTYPE")%>
                        </td>
                        <td style="display: none">
                            <tlk:RadComboBox ID="cboSalaryType" runat="server" SkinID="dDropdownList" AutoPostBack="True">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb" style="display: none">
                            <%# Translate("Nhóm thưởng")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboGrBonus" runat="server" SkinID="dDropdownList">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <%# Translate("Mã nhân viên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="rtxtEmployee" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSeach" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPaneBotton" runat="server" Scrolling="None">
                <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
                    <tlk:RadPane ID="RadPane1" runat="server" Width="250px" Scrolling="None" Visible="false">
                        <tlk:RadTreeView runat="server" CausesValidation="false" ID="ctrlListSalary" CheckBoxes="true"
                            CheckChildNodes="true" Height="100%" TriStateCheckBoxes="true">
                        </tlk:RadTreeView>
                    </tlk:RadPane>
                    <tlk:RadSplitBar ID="RadSplitBar2" runat="server" CollapseMode="Forward">
                    </tlk:RadSplitBar>
                    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                        <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%">
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,EMPLOYEE_CODE,FULLNAME_VN,ORG_NAME,JOB_NAME">
                                <Columns>
                                    <tlk:GridBoundColumn HeaderStyle-Width="100px" HeaderText="<%$ Translate: Mã nhân viên %>"
                                        DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Họ và tên %>"
                                        DataField="FULLNAME_VN" SortExpression="FULLNAME_VN" UniqueName="FULLNAME_VN" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="200px" HeaderText="<%$ Translate: Đơn vị %>"
                                        DataField="ORG_NAME" SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Vị trí công việc %>"
                                        DataField="JOB_NAME" SortExpression="JOB_NAME" UniqueName="JOB_NAME" />
                                </Columns>
                            </MasterTableView>
                        </tlk:RadGrid>
                    </tlk:RadPane>
                </tlk:RadSplitter>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<script type="text/javascript">
    var enableAjax = true;

    function clientButtonClicking(sender, args) {
        if (args.get_item().get_commandName() == 'NEXT') {
            var grid = $find("<%=rgData.ClientID %>");
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

    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }

</script>

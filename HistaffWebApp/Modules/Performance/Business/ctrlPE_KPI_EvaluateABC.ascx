﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPE_KPI_EvaluateABC.ascx.vb"
    Inherits="Performance.ctrlPE_KPI_EvaluateABC" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidIDEmp" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="RadPane3" Width="100%" Height="100%" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="95%" Height="95%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="120px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking" />
                <asp:ValidationSummary ID="valSum" runat="server" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboYear" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ đánh giá")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboPeriodEvaluate" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdFrom" Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdTo" Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                Text="<%$ Translate: Tìm kiếm %>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgEmployeeList" runat="server" AllowPaging="True"
                    Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderText="CheckBox" HeaderStyle-HorizontalAlign="center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="id" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE"
                                ReadOnly="true" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="FULLNAME" SortExpression="FULLNAME" UniqueName="FULLNAME"
                                ReadOnly="true" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" SortExpression="ORG_NAME" UniqueName="ORG_NAME"
                                ReadOnly="true" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME" SortExpression="TITLE_NAME" UniqueName="TITLE_NAME"
                                ReadOnly="true" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Ngạch lương" DataField="SALARY_LEVEL" SortExpression="SALARY_LEVEL" UniqueName="SALARY_LEVEL"
                                ReadOnly="true" HeaderStyle-Width="100px" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày nhận việc" DataField="JOIN_DATE" ItemStyle-HorizontalAlign="Center"
                                SortExpression="JOIN_DATE" UniqueName="JOIN_DATE" DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Tinh thần trách nhiệm" DataField="RESPONSIBILITY" ReadOnly="true"
                                SortExpression="RESPONSIBILITY" UniqueName="RESPONSIBILITY" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Tính hợp tác" DataField="COOPERATION" SortExpression="COOPERATION"
                                UniqueName="COOPERATION" ReadOnly="true" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Tính chuyên cần" DataField="DILIGENCE" SortExpression="DILIGENCE"
                                UniqueName="DILIGENCE" ReadOnly="true" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Tổng cộng" DataField="TOTAL" SortExpression="TOTAL"
                                UniqueName="TOTAL" ReadOnly="true" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Xếp loại" DataField="CLASSIFICATION_NAME" SortExpression="CLASSIFICATION_NAME"
                                UniqueName="CLASSIFICATION_NAME" ReadOnly="true" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Nhận xét" DataField="COMMENT1"
                                SortExpression="COMMENT1" UniqueName="COMMENT1" ReadOnly="true" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE" SortExpression="NOTE"
                                UniqueName="NOTE" ReadOnly="true" HeaderStyle-Width="100px" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"  />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
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
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlPE_KPI_Evaluate_RadSplitter3');
        }
        function clientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT" || item.get_commandName() == "NEXT") {
                enableAjax = false;
            }
        }
    </script>
</tlk:RadCodeBlock>
﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ApproveEmployee_Edit.ascx.vb"
    Inherits="Profile.ctrlHU_ApproveEmployee_Edit" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarWorkings" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="55px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Lý do không phê duyệt")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" runat="server" Width="100%" Height = "37px" MaxLength="250">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 500px;">
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgData" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,STATUS" ClientDataKeyNames="ID,STATUS">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tình trạng hôn nhân %>" DataField="MARITAL_STATUS_NAME"
                                UniqueName="MARITAL_STATUS_NAME" SortExpression="MARITAL_STATUS_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ thường trú %>" DataField="PER_ADDRESS"
                                UniqueName="PER_ADDRESS" SortExpression="PER_ADDRESS">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thành phố %>" DataField="PER_PROVINCE_NAME"
                                UniqueName="PER_PROVINCE_NAME" SortExpression="PER_PROVINCE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Quận huyện %>" DataField="PER_DISTRICT_NAME"
                                UniqueName="PER_DISTRICT_NAME" SortExpression="PER_DISTRICT_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Xã phường %>" DataField="PER_WARD_NAME"
                                UniqueName="PER_WARD_NAME" SortExpression="PER_WARD_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ tạm trú %>" DataField="NAV_ADDRESS"
                                UniqueName="NAV_ADDRESS" SortExpression="NAV_ADDRESS">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thành phố %>" DataField="NAV_PROVINCE_NAME"
                                UniqueName="NAV_PROVINCE_NAME" SortExpression="NAV_PROVINCE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Quận huyện %>" DataField="NAV_DISTRICT_NAME"
                                UniqueName="NAV_DISTRICT_NAME" SortExpression="NAV_DISTRICT_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Xã phường %>" DataField="NAV_WARD_NAME"
                                UniqueName="NAV_WARD_NAME" SortExpression="NAV_WARD_NAME">
                            </tlk:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Width="120px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
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
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_ApproveEmployee_Edit_RadSplitter1');
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {
            
        }


    </script>
</tlk:RadCodeBlock>

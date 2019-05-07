﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlConcurrently.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlConcurrently" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal"
    SkinID="Demo">
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgGrid" runat="server" AllowFilteringByColumn="true"
            Height="100%">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                  <%--  <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="ORG_ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="ORG_NAME" Visible="false" />
                    <tlk:GridBoundColumn DataField="TITLE_ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        SortExpression="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="Chức danh" DataField="NAME" UniqueName="NAME" SortExpression="NAME" />
                    <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO" UniqueName="DECISION_NO"
                        SortExpression="DECISION_NO" />
                    <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE" UniqueName="EFFECT_DATE"
                        SortExpression="EFFECT_DATE" />
                    <tlk:GridDateTimeColumn HeaderText="Ngày hết hiệu lực" DataField="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                        SortExpression="EXPIRE_DATE" />
                    <tlk:GridDateTimeColumn HeaderText="Ghi chú" DataField="NOTE" UniqueName="NOTE" SortExpression="NOTE" />--%>
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
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

        //        function GridCreated(sender, eventArgs) {
        //            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlDiscipline_RadSplitter2');
        //        }

    </script>
</tlk:RadScriptBlock>

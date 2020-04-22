<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlHealthMng.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlHealthMng" %>
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
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                        ReadOnly="true" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="60px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên nhân viên %>" DataField="EMPLOYEE_NAME"
                        ReadOnly="true" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                        ReadOnly="true" UniqueName="ORG_NAME" HeaderStyle-Width="200px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        ReadOnly="true" UniqueName="TITLE_NAME" HeaderStyle-Width="200px" />
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <Scrolling AllowScroll="true" />
                <ClientEvents OnGridCreated="GridCreated" />
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

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlHealthMng_RadSplitter2');
        }

    </script>
</tlk:RadScriptBlock>

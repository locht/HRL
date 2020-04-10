<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlQuantumSal.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlQuantumSal" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:radsplitter id="RadSplitter2" runat="server" height="100%" width="100%" orientation="Horizontal"
    skinid="Demo">
    <tlk:radpane id="RadPane2" runat="server" height="40px" scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:radpane>
    <tlk:radpane id="RadPane1" runat="server" scrolling="None">
        <tlk:radgrid pagesize="50" id="rgGrid" runat="server" allowfilteringbycolumn="true"
            height="100%">
            <mastertableview datakeynames="ID">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="Chi nhánh/ Khối/ Trung tâm" DataField="ORG_NAME2"
                        UniqueName="ORG_NAME2" SortExpression="ORG_NAME2" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="100%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>

                    <tlk:GridBoundColumn HeaderText="Nhà máy / Phòng /Ban" DataField="ORG_NAME3"
                        UniqueName="ORG_NAME3" SortExpression="ORG_NAME3" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="30%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Ngạch" DataField="LEVEL_NAME"
                        UniqueName="LEVEL_NAME" SortExpression="LEVEL_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="30%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Bậc" DataField="RANK_NAME"
                        UniqueName="RANK_NAME" SortExpression="RANK_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="30%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>

                   <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE"
                        ShowFilterIcon="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        FilterControlWidth="100%">
                        <HeaderStyle Width="100%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>

                </Columns>
            </mastertableview>
            <clientsettings>
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </clientsettings>
        </tlk:radgrid>
    </tlk:radpane>
</tlk:radsplitter>
<tlk:radscriptblock id="scriptBlock" runat="server">
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
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlQuantumSal_RadSplitter2');
        }

    </script>
</tlk:radscriptblock>

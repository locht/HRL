<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlContract.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlContract" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal"
    SkinID="Demo">
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgGrid" runat="server" AllowFilteringByColumn="true"
            Height="100%">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi nhánh/ Khối/ Trung tâm %>" DataField="ORG_NAME2"
                        UniqueName="ORG_NAME2" SortExpression="ORG_NAME2" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhà máy / Phòng /Ban %>" DataField="ORG_NAME3"
                        UniqueName="ORG_NAME3" SortExpression="ORG_NAME3" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại hợp đồng %>" DataField="CONTRACTTYPE_NAME"
                        UniqueName="CONTRACTTYPE_NAME" SortExpression="CONTRACTTYPE_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số hợp đồng %>" DataField="CONTRACT_NO"
                        UniqueName="CONTRACT_NO" SortExpression="CONTRACT_NO" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày bắt đầu %>" DataField="START_DATE"
                        UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="START_DATE"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        FilterControlWidth="100%">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="EXPIRE_DATE"
                        UniqueName="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EXPIRE_DATE"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        FilterControlWidth="100%">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Người ký %>" DataField="SIGNER_NAME"
                        UniqueName="SIGNER_NAME" SortExpression="SIGNER_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày ký %>" DataField="SIGN_DATE"
                        UniqueName="SIGN_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="SIGN_DATE"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        FilterControlWidth="100%">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh người ký %>" DataField="SIGNER_TITLE"
                        UniqueName="SIGNER_TITLE" SortExpression="SIGNER_TITLE" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                        SortExpression="REMARK" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        FilterControlWidth="100%">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
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
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlContract_RadSplitter2');
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {
            var item = args.get_item();

            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }

        }

    </script>
</tlk:RadScriptBlock>

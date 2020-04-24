<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsAccidentRisk.ascx.vb"
    Inherits="Insurance.ctrlInsAccidentRisk" %>
<%@ Import Namespace="Common" %>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="260px" Scrolling="None">
        <common:ctrlorganization id="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMenu" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgData" runat="server" Height="100%" AllowSorting="True" AllowMultiRowSelection="true"
                    AllowPaging="True" AutoGenerateColumns="False">
                    <ClientSettings EnableRowHoverStyle="true">
                         <Scrolling AllowScroll="True" UseStaticHeaders="True" FrozenColumnsCount="3" />
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="60px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh%>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi nhánh/Khối/Trung tâm%>" DataField="ORG_NAME2"
                                SortExpression="ORG_NAME2" UniqueName="ORG_NAME2">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhà máy/Phòng/Ban%>" DataField="ORG_NAME3"
                                SortExpression="ORG_NAME3" UniqueName="ORG_NAME3">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngành/văn phòng đại diện/Bộ phận%>" DataField="ORG_NAME4"
                                SortExpression="ORG_NAME4" UniqueName="ORG_NAME4">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="BIRTH_DATE" UniqueName="BIRTH_DATE">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới tính%>" DataField="GENDER_NAME"
                                SortExpression="GENDER_NAME" UniqueName="GENDER_NAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ thường trú%>" DataField="ADDRESS"
                                SortExpression="ADDRESS" UniqueName="ADDRESS">
                                <HeaderStyle Width="250px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Số hợp đồng%>" DataField="CONTRACT_NO"
                                SortExpression="CONTRACT_NO" UniqueName="CONTRACT_NO">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số thứ tự HD%>" DataField="ROWNUM_NO"
                                SortExpression="ROWNUM_NO" UniqueName="ROWNUM_NO">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày ký hợp đồng %>" DataField="CONTRACT_SIGN_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="CONTRACT_SIGN_DATE" UniqueName="CONTRACT_SIGN_DATE">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực hợp đồng %>" DataField="CONTRACT_START_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="CONTRACT_START_DATE" UniqueName="CONTRACT_START_DATE">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực hợp đồng %>" DataField="CONTRACT_EXPIRE_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="CONTRACT_EXPIRE_DATE" UniqueName="CONTRACT_EXPIRE_DATE">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Phụ lục hợp đồng %>" DataField="PLHD_CONTRACT_NO"
                                SortExpression="PLHD_CONTRACT_NO" UniqueName="PLHD_CONTRACT_NO">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày ký phụ lục hợp đồng %>" DataField="PLHD_SIGN_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="PLHD_SIGN_DATE" UniqueName="PLHD_SIGN_DATE">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                           <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực của phụ lục %>" DataField="PLHD_START_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="PLHD_START_DATE" UniqueName="PLHD_START_DATE">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc của phụ lục %>" DataField="PLHD_EXPIRE_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="PLHD_EXPIRE_DATE" UniqueName="PLHD_EXPIRE_DATE">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị bảo hiểm %>" DataField="INS_ORG_NAME"
                                SortExpression="INS_ORG_NAME" UniqueName="INS_ORG_NAME" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK"
                                SortExpression="REMARK" UniqueName="REMARK" />
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="500" Height="500px"
            OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }

        function OpenInsertWindow() {
            var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsAccidentRiskNewEdit&group=Business&FormType=0&noscroll=1', "rwPopup");
            oWindow.setSize(800, 500);
            oWindow.center();
        }
        function OpenEditWindow() {
            var grid = $find('<%# rgData.ClientID %>');
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            var id = 0
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            if (gridSelected != "") {
                id = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            }
            if (id > 0) {
                var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsAccidentRiskNewEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id + '&noscroll=1', "rwPopup");
                oWindow.setSize(800, 500);
                oWindow.center();
            }
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnClientClose(sender, args) {
            $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
        }
        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow();
                    args.set_cancel(true);
                }
            } if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
    }
    function OpenInNewTab(url) {
        var win = window.open(url, '_blank');
        win.focus();
    }
    </script>
</tlk:RadScriptBlock>

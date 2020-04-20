﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Contract.ascx.vb"
    Inherits="Profile.ctrlHU_Contract" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:radsplitter id="RadSplitter1" runat="server" width="100%" height="100%">
    <tlk:radpane id="LeftPane" runat="server" minwidth="200" width="250px" scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:radpane>
    <tlk:radsplitbar id="RadSplitBar1" runat="server" collapsemode="Forward">
    </tlk:radsplitbar>
    <tlk:radpane id="MainPane" runat="server" scrolling="None">
        <tlk:radsplitter id="RadSplitter3" runat="server" width="100%" height="100%" orientation="Horizontal">
            <tlk:radpane id="RadPane3" runat="server" height="35px" scrolling="None">
                <tlk:radtoolbar id="tbarContracts" runat="server" onclientbuttonclicking="clientButtonClicking" />
            </tlk:radpane>
            <tlk:radpane id="RadPane1" runat="server" height="38px" scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbFromDate" runat="server" Text="Ngày bắt đầu từ"></asp:Label>
                        </td>
                        <td>
                            <tlk:raddatepicker id="rdFromDate" runat="server">
                            </tlk:raddatepicker>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbToDate" runat="server" Text="Đến"></asp:Label>
                        </td>
                        <td>
                            <tlk:raddatepicker id="rdToDate" runat="server">
                            </tlk:raddatepicker>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkTerminate" runat="server" Text="Liệt kê cả nhân viên nghỉ việc" />
                        </td>
                        <td>
                            <tlk:radbutton id="btnSearch" runat="server" text="Tìm" skinid="ButtonFind">
                            </tlk:radbutton>
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:radpane>
            <tlk:radpane id="RadPane2" runat="server" scrolling="None">
                <tlk:radgrid pagesize="50" id="rgContract" runat="server" height="100%" allowpaging="True"
                    allowsorting="True" allowmultirowselection="true">
                    <clientsettings enablerowhoverstyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                          <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3"/>
                    </clientsettings>
                    <mastertableview datakeynames="ID,ORG_ID,EMPLOYEE_ID,EMPLOYEE_CODE,STATUS_CODE,STATUS_ID,CONTRACTTYPE_ID"
                        clientdatakeynames="ID,ORG_ID,EMPLOYEE_ID,STATUS_CODE,CONTRACTTYPE_CODE,STATUS_ID,CONTRACTTYPE_ID,EMPLOYEE_CODE">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="STATUS_CODE" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viêngFormatCurrencyVN" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="200px" />  
                            <tlk:GridTemplateColumn HeaderText="Đơn vị" DataField="ORG_NAME" SortExpression="ORG_NAME"
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
                            <tlk:GridBoundColumn HeaderText="Chức danhgFormatCurrencyVN" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Số HĐLĐgFormatCurrencyVN" DataField="CONTRACT_NO"
                                SortExpression="CONTRACT_NO" UniqueName="CONTRACT_NO" />
                            <tlk:GridBoundColumn HeaderText="Loại hợp đồnggFormatCurrencyVN" DataField="CONTRACTTYPE_NAME"
                                SortExpression="CONTRACTTYPE_NAME" UniqueName="CONTRACTTYPE_NAME" HeaderStyle-Width="250px" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày bắt đầugFormatCurrencyVN" DataField="START_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="START_DATE" UniqueName="START_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="Ngày kết thúcgFormatCurrencyVN" DataField="EXPIRE_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Người kýgFormatCurrencyVN" DataField="SIGNER_NAME"
                                SortExpression="SIGNER_NAME" UniqueName="SIGNER_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày kýgFormatCurrencyVN" DataField="SIGN_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="SIGN_DATE" UniqueName="SIGN_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Trạng tháigFormatCurrencyVN" DataField="STATUS_NAME"
                                SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                            <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC"
                                SortExpression="ORG_DESC" Visible="false" />--%>
                        </Columns>
                    </mastertableview>
                    <headerstyle width="120px" />
                </tlk:radgrid>
            </tlk:radpane>
        </tlk:radsplitter>
    </tlk:radpane>
</tlk:radsplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox1" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:radwindowmanager id="RadWindowManager1" runat="server">
    <windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"
            OnClientClose="popupclose" Height="600px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Tạo hợp đồng %>">
        </tlk:RadWindow>
    </windows>
</tlk:radwindowmanager>
<tlk:radcodeblock id="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function OpenNew() {
            var extented = '';
            var bCheck = $find('<%= rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 1) {
                empID = $find('<%= rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
                if (empID)
                    extented = '&empid=' + empID;
            } else if (bCheck > 1) {
                var m = '<%= Translate("Bạn không thể copy dữ liệu từ nhiều bản ghi! Không thể thực hiện thao tác này") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
                return;
            }
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_ContractNewEdit&group=Business' + extented, "_self");
        }

        function OpenEditContract() {
            var bCheck = $find('<%= rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            } else if (bCheck > 1) {
                return 2;
            }
            var id = $find('<%= rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var emp_id = $find('<%= rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');


            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_ContractNewEdit&group=Business&IDSelect=' + id + '&empid=' + emp_id, "_self"); /*

            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
            return 0;
        }
        function OPENTHANHLY() {
            var grid = $find('<%= rgContract.ClientID%>')
            var emp_id = $find('<%= rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
            var idCT = $find('<%= rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlContract_Liquidate&group=Business&noscroll=1&empid=' + emp_id + '&idCT=' + idCT, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize(800, 300);
            oWindow.center();
        }
        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'REFRESH') {
                OPENTHANHLY();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT") {
                var bCheck = $find('<%= rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }
            //if (args.get_item().get_commandName() == "NEXT") {
                //var bCheck = $find('<%= rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
                //if (bCheck == 0) {
                    //var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    //var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    //setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    //args.set_cancel(true);
                    //return;
                //}
                //enableAjax = false;
            //}
            if (args.get_item().get_commandName() == 'NEXT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "EDIT") {
                if (OpenEditContract() == 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                } else if (OpenEditContract() == 2) {
                    var m = '<%= Translate("Bạn không thể sửa nhiều bản ghi! Không thể thực hiện thao tác này") %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                args.set_cancel(true);
            }
        }

        function gridRowDblClick(sender, eventArgs) {
            OpenEditContract();
        }


        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function popupclose(sender, args) {

            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $get("<%= btnSearch.ClientId %>").click();
            }

        }


        function btnPrintSupportClick(sender, args) {
            alert(1);
            var bCheck = $find('<%= rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
                return;
            }
            //            if (bCheck > 1) {
            //                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
            //                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
            //                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            //                args.set_cancel(true);
            //                return;
            //            }
            enableAjax = false;
        }
        //        function Liquidation_Click() {
        //            var grid = $find('<%= rgContract.ClientID%>')
        //            var emp_id = $find('<%= rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
        //            var idCT = $find('<%= rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
        //            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlContract_Liquidate&group=Business&noscroll=1&empid=' + emp_id + '&idCT=' + idCT, "rwPopup");
        //            var pos = $("html").offset();
        //            oWindow.moveTo(pos.left, pos.top);
        //            oWindow.setSize($(window).width(), $(window).height());
        //        }
    </script>
</tlk:radcodeblock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />

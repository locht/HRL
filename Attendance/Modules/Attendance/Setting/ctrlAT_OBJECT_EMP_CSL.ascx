<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAT_OBJECT_EMP_CSL.ascx.vb"
    Inherits="Attendance.ctrlAT_OBJECT_EMP_CSL" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane3" runat="server" MinWidth="300" Width="300px" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane4" runat="server" Height="185px" Scrolling="None" MinHeight="400">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnFind">
                    <div>
                        <fieldset style="width: auto; height: auto">
                            <legend>
                                <%# Translate("Thông tin tìm kiếm")%>
                            </legend>
                            <table width="100%" class="td-padding">
                                <tr>
                                    <td class="lb">
                                        <%# Translate("Mã/Tên NV")%>
                                    </td>
                                    <td>
                                        <tlk:RadTextBox ID="txtEMPLOYEEID_SEARCH" MaxLength="250" runat="server" TabIndex="3">
                                        </tlk:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lb">
                                        <%# Translate("Đối tượng nhân viên")%>
                                    </td>
                                    <td>
                                        <tlk:RadComboBox runat="server" ID="cbo_OBJ_EMP_Search" Width="100%">
                                        </tlk:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lb">
                                        <%# Translate("Đối tượng nghỉ bù")%>
                                    </td>
                                    <td>
                                        <tlk:RadComboBox runat="server" ID="cbo_OBJ_CSL_Search" Width="100%">
                                        </tlk:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lb">
                                        <%# Translate("Trạng thái làm việc")%>
                                    </td>
                                    <td>
                                        <tlk:RadComboBox runat="server" ID="cboStatus" Width="100%">
                                        </tlk:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:CheckBox ID="chkSTATUS" runat="server" Text='<%# Translate("liệt kê cả nhân viên đã nghỉ việc")%>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <tlk:RadButton EnableEmbeddedSkins="true" ID="btnFind" TabIndex="4" runat="server"
                                            Text="<%$ Translate: Tìm kiếm %>" ValidationGroup="Search" SkinID="ButtonFind">
                                        </tlk:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </asp:Panel>
            </tlk:RadPane>
            <tlk:RadSplitBar ID="RadSplitBar3" runat="server">
            </tlk:RadSplitBar>
            <tlk:RadPane ID="RadPane5" runat="server">
                <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane6" runat="server" Height="80px" Scrolling="None">
                <asp:Panel ID="Panel2" runat="server" DefaultButton="btnFind">
                    <div>
                        <fieldset style="max-width:570px; width: auto; height: auto">
                            <legend>
                                <%# Translate("Thông tin chỉnh sửa hàng loạt")%>
                            </legend>
                            <table width="100%" class="td-padding">
                                <tr>
                                    <td class="lb" style="width:120px">
                                        <%# Translate("Đối tượng nhân viên")%>
                                    </td>
                                    <td style="width:160px">
                                        <tlk:RadComboBox runat="server" ID="cbo_OBJ_EMP_updateAll" Width="100%">
                                        </tlk:RadComboBox>
                                    </td>
                                    <td class="lb" style="width:120px">
                                        <%# Translate("Đối tượng nghỉ bù")%>
                                    </td>
                                    <td style="width:160px">
                                        <tlk:RadComboBox runat="server" ID="cbo_OBJ_CSL_updateAll" Width="100%">
                                        </tlk:RadComboBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </asp:Panel>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true"
                    AllowMultiRowEdit="true" Width="100%">
                    <ClientSettings>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="STT,ID,EMPLOYEE_CODE,FULLNAME_VN,TITLE_ID,TITLE_NAME,ORG_ID,ORG_NAME,SAL_LEVEL_ID,SAL_LEVEL_NAME,SAL_RANK_ID,SAL_RANK_NAME,OBJ_EMP_ID,OBJ_EMP_NAME,OBJ_CSL_ID,OBJ_CSL_NAME"
                        ClientDataKeyNames="STT,ID,EMPLOYEE_CODE,FULLNAME_VN,TITLE_ID,TITLE_NAME,ORG_ID,ORG_NAME,SAL_LEVEL_ID,SAL_LEVEL_NAME,SAL_RANK_ID,SAL_RANK_NAME,OBJ_EMP_ID,OBJ_EMP_NAME,OBJ_CSL_ID,OBJ_CSL_NAME"
                        EditMode="InPlace">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />                            
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" ReadOnly="true" HeaderStyle-Width="60"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="FULLNAME_VN"
                                SortExpression="FULLNAME_VN" UniqueName="FULLNAME_VN" ReadOnly="true"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Công việc %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" ReadOnly="true"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngạch Lương %>" DataField="SAL_LEVEL_NAME"
                                SortExpression="SAL_LEVEL_NAME" UniqueName="SAL_LEVEL_NAME" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Bậc lương %>" DataField="SAL_RANK_NAME"
                                SortExpression="SAL_RANK_NAME" UniqueName="SAL_RANK_NAME" ReadOnly="true" />
                            <tlk:GridTemplateColumn UniqueName="OBJ_EMP_NAME" HeaderText="<%$ Translate: Đối tượng nhân viên %>"
                                SortExpression="OBJ_EMP_NAME" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "OBJ_EMP_NAME")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <tlk:RadComboBox runat="server" ID="cbo_OBJ_EMP_NAME" Width="80%">
                                    </tlk:RadComboBox>
                                </EditItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn UniqueName="OBJ_CSL_NAME" HeaderText="<%$ Translate: Đối tượng nghỉ bù %>"
                                SortExpression="OBJ_CSL_NAME" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "OBJ_CSL_NAME")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <tlk:RadComboBox runat="server" ID="cbo_OBJ_CSL_NAME" Width="80%">
                                    </tlk:RadComboBox>
                                </EditItemTemplate>
                            </tlk:GridTemplateColumn>
                        </Columns>
                        <HeaderStyle Width="150px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var selectedID = -1;
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "APPROVE") {
                enableAjax = false;
            }
        }

        function OpenInNewTab(url) {
            var win = window.open(url, '_blank');
            win.focus();
        }
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />

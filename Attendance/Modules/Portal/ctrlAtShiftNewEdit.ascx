﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAtShiftNewEdit.ascx.vb"
    Inherits="Attendance.ctrlAtShiftNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<style>
    #ctrlAtShiftNewEdit_rgEmployee
    {
        position: fixed;
        bottom: 0;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Width="100%" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" Height="100%" runat="server" Scrolling="None">
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form">
                    <tr>
                        <td class="item-head" colspan="6">
                            <asp:Label runat="server" ID="DecisionInfo" Text="Thông tin đăng ký"></asp:Label>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label1" Text="Ca làm việc"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboShift" runat="server" TabIndex="3">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboShift"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn ca làm việc %>" ToolTip="<%$ Translate: Bạn phải chọn ca làm việc %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbEffectDate" Text="Từ ngày"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDateFrom" runat="server" TabIndex="3">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="rqDateFrom" ControlToValidate="rdDateFrom" runat="server"
                                ErrorMessage="Bạn phải nhập ngày bắt đầu." ToolTip="Bạn phải nhập ngày bắt đầu."> 
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbExpireDate" Text="Đến ngày"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDateTo" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="rqDateTo" ControlToValidate="rdDateTo" runat="server"
                                ErrorMessage="Bạn phải nhập ngày kết thúc." ToolTip="Bạn phải nhập ngày kết thúc."> 
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ValidationGroup="EmpProfile" ID="cvalPassDate" runat="server"
                                ErrorMessage="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>" ToolTip="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"
                                ControlToValidate="rdDateTo" ControlToCompare="rdDateFrom" Operator="GreaterThanEqual"
                                Type="Date">
                            </asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbReason" Text="Lý do"></asp:Label>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtReason" runat="server" Width="100%">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator5"
                                ControlToValidate="txtReason" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập lý do %>"
                                ToolTip="<%$ Translate:  Bạn phải nhập lý do %>">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <tlk:RadGrid ID="rgEmployee" AllowPaging="false" AllowMultiRowEdit="true" runat="server">
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView EditMode="InPlace" EditFormSettings-EditFormType="AutoGenerated"
                        AllowPaging="false" AllowCustomPaging="false" DataKeyNames="ID,EMPLOYEE_CODE,ORG_ID,ORG_NAME,TITLE_ID,FULLNAME_VN,TITLE_NAME_VN"
                        ClientDataKeyNames="ID,EMPLOYEE_CODE,ORG_ID,TITLE_ID,FULLNAME_VN,TITLE_NAME_VN,ORG_NAME"
                        CommandItemDisplay="Top">
                        <CommandItemStyle Height="25px" />
                        <CommandItemTemplate>
                            <div style="padding: 2px 0 0 0">
                                <div style="float: left">
                                    <tlk:RadButton Width="150px" ID="btnEmployee" runat="server" Text="Chọn tất cả nhân viên"
                                        CausesValidation="false" CommandName="FindEmployee" TabIndex="3">
                                    </tlk:RadButton>
                                </div>
                                <div style="float: right">
                                    <tlk:RadButton Width="100px" ID="btnDeleteEmp" runat="server" Text="Xóa" CausesValidation="false"
                                        CommandName="DeleteEmployee" TabIndex="3">
                                    </tlk:RadButton>
                                </div>
                            </div>
                        </CommandItemTemplate>
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" UniqueName="ID" SortExpression="ID" Visible="false"
                                ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="MSNV" DataField="EMPLOYEE_CODE" HeaderStyle-Width="100px"
                                ReadOnly="true" UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="FULLNAME_VN" UniqueName="FULLNAME_VN"
                                HeaderStyle-Width="200px" ReadOnly="true" SortExpression="FULLNAME_VN" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn DataField="ORG_ID" UniqueName="ORG_ID" HeaderStyle-Width="200px"
                                ReadOnly="true" SortExpression="ORG_ID" Visible="false" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" UniqueName="ORG_NAME"
                                HeaderStyle-Width="200px" ReadOnly="true" SortExpression="ORG_NAME" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME_VN" UniqueName="TITLE_NAME_VN"
                                HeaderStyle-Width="200px" ReadOnly="true" SortExpression="TITLE_NAME_VN" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(0);
            }
        }
        var enableAjax = true;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadScriptBlock>

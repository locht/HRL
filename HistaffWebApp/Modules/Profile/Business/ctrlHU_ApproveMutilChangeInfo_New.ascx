﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ApproveMutilChangeInfo_New.ascx.vb"
    Inherits="Profile.ctrlHU_ApproveMutilChangeInfo_New" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<%@ Import Namespace="Profile" %>
<asp:HiddenField ID="hidIDEmp" runat="server" />
<asp:HiddenField ID="hidOrg" runat="server" />
<asp:HiddenField ID="hidManager" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Scrolling="None" Height="35px">
        <tlk:RadToolBar ID="tbarMenu" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Height="310px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary"
            ValidationGroup="" />
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="8">
                    <%# Translate("Thông tin điều chỉnh")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <asp:Label ID="lbDecisionType" runat="server" Text="Loại quyết định"></asp:Label><span
                        class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDecisionType" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboDecisionType"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Loại quyết định %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Loại quyết định %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbOrgName" runat="server" Text="Đơn vị"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" Width="130px" SkinID="Readonly" ReadOnly="true" />
                    <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>" ToolTip="<%$ Translate: Bạn phải chọn đơn vị %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDecision" runat="server" Text="Số quyết định"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecision" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbTitle" runat="server" Text="Chức danh"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboTitle">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbEffectDate" runat="server" Text="Ngày hiệu lực"></asp:Label><span
                        class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                        <DateInput CausesValidation="false">
                        </DateInput>
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" runat="server" ControlToValidate="rdEffectDate"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusExistEffectDate" runat="server" ErrorMessage="<%$ Translate: Ngày hiệu lực phải lớn hơn ngày hiệu lực gần nhất %>"
                        ToolTip="<%$ Translate: Ngày hiệu lực phải lớn hơn ngày hiệu lực gần nhất %>"> </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbExpireDate" runat="server" Text="Ngày hết hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdExpireDate"
                        Type="Date" ControlToCompare="rdEffectDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực %>"
                        ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực %>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbManagerNew" runat="server" Text="Quản lý trực tiếp"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtManagerNew" ReadOnly="true" Width="130px" />
                    <tlk:RadButton runat="server" ID="btnFindDirect" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <asp:Label ID="lbStaffRank" runat="server" Text="Bậc nhân sự"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboStaffRank" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox ID="chkIsProcess" runat="server" Checked="true" Text="<%$ Translate: Có lưu dữ liệu sang Quá trình công tác %>" />
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="8">
                    <%# Translate("Thông tin phê duyệt")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbStatus" runat="server" Text="Trạng thái"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbSignDate" runat="server" Text="Ngày ký"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSignDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbUpload" runat="server" Text="Tập tin đính kèm"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbSignName" runat="server" Text="Người ký"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignName" runat="server" SkinID="Readonly" ReadOnly="true"
                        Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindSign" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <asp:Label ID="lbSignTitle" runat="server" Text="Chức danh"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignTitle" runat="server" SkinID="Readonly" AutoPostBack="true"
                        ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi Chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtSDESC" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr style="visibility: hidden">
                <td class="lb">
                    <tlk:RadTextBox ID="txtRemindLink" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtFileAttach_Link" runat="server" ReadOnly="true" Visible="false">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgEmployee" AllowPaging="true" AllowMultiRowEdit="true" runat="server"
            PageSize="50" Height="100%">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView EditMode="InPlace" AllowPaging="true" AllowCustomPaging="true" DataKeyNames="EMPLOYEE_ID,EMPLOYEE_NAME,TITLE_ID,ORG_ID,TITLE_NAME,ORG_NAME,EMPLOYEE_CODE,REMARK,OBJECT_ATTENDANCE_NAME,OBJECT_LABORNAME,STAFF_RANK_NAME,OBJECT_ATTENDANCE,OBJECT_LABOR,STAFF_RANK_ID"
                ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_NAME,TITLE_ID,ORG_ID,TITLE_NAME,ORG_NAME,EMPLOYEE_CODE,REMARK,OBJECT_ATTENDANCE_NAME,OBJECT_LABORNAME,STAFF_RANK_NAME,OBJECT_ATTENDANCE,OBJECT_LABOR,STAFF_RANK_ID"
                CommandItemDisplay="Top">
                <CommandItemStyle Height="25px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="150px" ID="btnEmployee" runat="server" Text="Chọn tất cả nhân viên"
                                CausesValidation="false" CommandName="FindEmployee" TabIndex="3">
                            </tlk:RadButton>
                            <%--<tlk:RadButton Width="150px" ID="RadButton1" runat="server" Text="Chọn nhân viên từ import"
                                CausesValidation="false" CommandName="FindEmployeeImport" TabIndex="3">
                            </tlk:RadButton>--%>
                        </div>
                        <%-- <div style="float: left">
                            <tlk:RadButton Width="150px" ID="btnExport" runat="server" Text="Xuất Excel"   
                                CausesValidation="false" CommandName="Export" TabIndex="3">
                            </tlk:RadButton>
                        </div>--%>
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
                    <tlk:GridBoundColumn HeaderText="MSNV" DataField="EMPLOYEE_CODE" ReadOnly="true"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME"
                        ReadOnly="true" SortExpression="EMPLOYEE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        ReadOnly="true" SortExpression="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                        ReadOnly="true" SortExpression="TITLE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Đối tượng chấm công" DataField="OBJECT_ATTENDANCE_NAME"
                        UniqueName="OBJECT_ATTENDANCE_NAME" ReadOnly="true" SortExpression="OBJECT_ATTENDANCE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Loại hình lao động" DataField="OBJECT_LABORNAME"
                        UniqueName="OBJECT_LABORNAME" ReadOnly="true" SortExpression="OBJECT_LABORNAME" />
                    <tlk:GridBoundColumn HeaderText="Bậc nhân sự" DataField="STAFF_RANK_NAME" UniqueName="STAFF_RANK_NAME"
                        ReadOnly="true" SortExpression="STAFF_RANK_NAME" />
                    <tlk:GridBoundColumn HeaderText="NHÂN VIÊN" DataField="EMPLOYEE_ID" UniqueName="EMPLOYEE_ID"
                        ReadOnly="true" SortExpression="EMPLOYEE_ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="CHỨC DANH" DataField="TITLE_ID" UniqueName="TITLE_ID"
                        ReadOnly="true" SortExpression="TITLE_ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="TÊN PHÒNG BAN" DataField="ORG_ID" UniqueName="ORG_ID"
                        ReadOnly="true" SortExpression="ORG_ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="ID THIẾT LẬP" DataField="WELFARE_ID" UniqueName="WELFARE_ID"
                        ReadOnly="true" SortExpression="WELFARE_ID" Visible="false" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSignManager" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            registerOnfocusOut('RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_ApproveMutilChangeInfo_New_LeftPane');
        });



        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        //mandatory for the RadWindow dialogs functionality
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
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //            }
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'Export') {
                enableAjax = false;
            }
        }
        function btnExportClicking(sender, args) {
            enableAjax = false;
        }
        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {

                default:
                    break;
            }
        }

        function clearSelectRadnumeric(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

        function clearSelectRadtextbox(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function OnClientItemsRequesting() { }
    </script>
</tlk:RadCodeBlock>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_AssessmentResult.ascx.vb"
    Inherits="Training.ctrlTR_AssessmentResult" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="LeftPane" runat="server" Height="240px">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Năm")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtYear" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="" MinValue="1900" MaxLength="2999" AutoPostBack="true"
                        CausesValidation="false">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Khóa đào tạo")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCourse" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trung tâm")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtCenters" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Giảng viên")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtLectures" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdStartDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEndDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Địa điểm")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtAddress" SkinID="Textbox1023" Width="100%"
                        ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" CellSpacing="0" 
                    GridLines="None">
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView Caption="<%$ Translate: Danh sách nhân viên %>" 
                        DataKeyNames="EMPLOYEE_ID">
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                            Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>
                            <tlk:GridBoundColumn DataField="EMPLOYEE_CODE" 
                                HeaderText="<%$ Translate: Mã nhân viên %>" SortExpression="EMPLOYEE_CODE" 
                                UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn DataField="EMPLOYEE_NAME" 
                                HeaderText="<%$ Translate: Tên nhân viên%>" SortExpression="EMPLOYEE_NAME" 
                                UniqueName="EMPLOYEE_NAME" />
                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgResult" runat="server" Height="100%" SkinID="GridNotPaging" AllowSorting="false"
                    AllowMultiRowSelection="false" AllowMultiRowEdit="true">
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,TR_CRITERIA_GROUP_ID,TR_CRITERIA_ID,POINT_ASS,REMARK"
                        Caption="<%$ Translate: Kết quả đánh giá %>" EditMode="InPlace">
                        <GroupByExpressions>
                            <tlk:GridGroupByExpression>
                                <SelectFields>
                                    <tlk:GridGroupByField FieldName="TR_CRITERIA_GROUP_NAME" HeaderText="<%$ Translate: Nhóm tiêu chí %>">
                                    </tlk:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <tlk:GridGroupByField FieldName="TR_CRITERIA_GROUP_NAME" SortOrder="Ascending"></tlk:GridGroupByField>
                                </GroupByFields>
                            </tlk:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã tiêu chí %>" DataField="TR_CRITERIA_CODE"
                                UniqueName="TR_CRITERIA_CODE" SortExpression="TR_CRITERIA_CODE" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiêu chí %>" DataField="TR_CRITERIA_NAME"
                                UniqueName="TR_CRITERIA_NAME" SortExpression="TR_CRITERIA_NAME" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Điểm tối đa %>" DataField="TR_CRITERIA_POINT_MAX"
                                UniqueName="TR_CRITERIA_POINT_MAX" SortExpression="TR_CRITERIA_POINT_MAX" DataFormatString="{0:n0}"
                                ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Điểm đánh giá %>" DataField="POINT_ASS"
                                UniqueName="POINT_ASS" SortExpression="POINT_ASS" DataFormatString="{0:n0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                                SortExpression="REMARK" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

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


        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'PRINT_STUDENT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'PRINT_COMPLETE') {
                enableAjax = false;
            }
        }
    </script>
</tlk:RadCodeBlock>

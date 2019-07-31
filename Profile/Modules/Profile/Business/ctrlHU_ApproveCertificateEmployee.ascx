<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ApproveCertificateEmployee.ascx.vb"
    Inherits="Profile.ctrlHU_ApproveCertificateEmployee" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarWorkings" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="55px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbRemark" runat="server" Text="Lý do không phê duyệt"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" runat="server" Width="100%" MaxLength="250"
                                Rows="1" Height="38px">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 500px;">
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,STATUS" ClientDataKeyNames="ID,STATUS">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lĩnh vực đào tạo %>" DataField="FIELD_NAME"
                                UniqueName="FIELD_NAME" SortExpression="FIELD_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn UniqueName="FROM_DATE" HeaderText="<%$ Translate: Thời gian đào tạo từ tháng %>"
                                ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="FROM_DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn UniqueName="TO_DATE" HeaderText="<%$ Translate: Đến tháng %>"
                                ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="TO_DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên trường %>" DataField="SCHOOL_NAME"
                                UniqueName="SCHOOL_NAME" SortExpression="SCHOOL_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chuyên ngành %>" DataField="MAJOR_NAME"
                                UniqueName="MAJOR_NAME" SortExpression="MAJOR_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trình độ %>" DataField="LEVEL_NAME"
                                UniqueName="LEVEL_NAME" SortExpression="LEVEL_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm số %>" DataField="MARK" UniqueName="MARK"
                                SortExpression="MARK">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung đào tạo %>" DataField="CONTENT_NAME"
                                UniqueName="CONTENT_NAME" SortExpression="CONTENT_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại hình đào tạo %>" DataField="TYPE_NAME"
                                UniqueName="TYPE_NAME" SortExpression="TYPE_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã số chứng chỉ %>" DataField="CODE_CERTIFICATE"
                                UniqueName="CODE_CERTIFICATE" SortExpression="CODE_CERTIFICATE">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn UniqueName="EFFECT_FROM" HeaderText="<%$ Translate: Hiệu lực chứng chỉ từ %>"
                                ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="EFFECT_FROM">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn UniqueName="EFFECT_TO" HeaderText="<%$ Translate: Hiệu lực chứng chỉ đến %>"
                                ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="EFFECT_TO">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Xếp loại tốt nghiệp %>" DataField="CLASSIFICATION"
                                UniqueName="CLASSIFICATION" SortExpression="CLASSIFICATION">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm tốt nghiệp %>" DataField="YEAR"
                                UniqueName="YEAR" SortExpression="YEAR">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tệp tin %>" DataField="FILENAME"
                                UniqueName="FILENAME" SortExpression="FILENAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                                SortExpression="REMARK">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                UniqueName="STATUS_NAME" SortExpression="STATUS_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Width="120px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {

        }


    </script>
</tlk:RadCodeBlock>

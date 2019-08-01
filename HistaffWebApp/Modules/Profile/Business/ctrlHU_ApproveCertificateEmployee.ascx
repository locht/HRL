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
                            <tlk:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Display="false">
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn DataField="FROM_DATE" HeaderText="Từ tháng" UniqueName="FROM_DATE"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                Visible="true" DataFormatString="{0:MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn DataField="TO_DATE" HeaderText="Tới tháng" UniqueName="TO_DATE"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:MM/yyyy}"
                                CurrentFilterFunction="EqualTo" Visible="true">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn DataField="YEAR_GRA" HeaderText="Năm tốt nghiệp" UniqueName="YEAR_GRA"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="NAME_SHOOLS" HeaderText="Tên trường" UniqueName="NAME_SHOOLS"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="FORM_TRAIN_NAME" HeaderText="Hình thức đào tạo" UniqueName="FORM_TRAIN_NAME"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="SPECIALIZED_TRAIN" HeaderText="Chuyên ngành" UniqueName="SPECIALIZED_TRAIN"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="TYPE_TRAIN_NAME" HeaderText="Loại hình đào tạo" UniqueName="TYPE_TRAIN_NAME"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="RESULT_TRAIN" HeaderText="Kết quả" UniqueName="RESULT_TRAIN"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="CERTIFICATE" HeaderText="Bằng cấp" UniqueName="CERTIFICATE"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="RECEIVE_DEGREE_DATE" HeaderText="Ngày nhận bằng"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_FROM"
                                UniqueName="RECEIVE_DEGREE_DATE">
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECTIVE_DATE_FROM"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_FROM"
                                UniqueName="EFFECTIVE_DATE_FROM">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="Ngày hết hiệu lực" DataField="EFFECTIVE_DATE_TO"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_TO"
                                UniqueName="EFFECTIVE_DATE_TO">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="STATUS_NAME" UniqueName="STATUS_NAME"
                                SortExpression="STATUS_NAME" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="TRUE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="UPLOAD_FILE" UniqueName="UPLOAD_FILE"
                                SortExpression="UPLOAD_FILE" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="false">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="FILE_NAME" UniqueName="FILE_NAME"
                                SortExpression="FILE_NAME" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="TRUE">
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

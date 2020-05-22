<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ApproveTrainingOutCompany_Edit.ascx.vb"
    Inherits="Profile.ctrlHU_ApproveTrainingOutCompany_Edit" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
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
                            <%# Translate("Lý do không phê duyệt")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" runat="server" Width="100%" MaxLength="250" Rows="1" Height="38px">
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
                <tlk:RadGrid PageSize=50 ID="rgData" runat="server" Height="100%" >
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,STATUS" ClientDataKeyNames="ID,STATUS">
                        <Columns>
                         <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="EMPLOYEE_CODE" HeaderText="Mã nhân viên"
                        UniqueName="EMPLOYEE_CODE" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FULL_NAME_VN" HeaderText="Họ và tên"
                        UniqueName="FULL_NAME_VN" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ORG_NAME" HeaderText="Phòng ban"
                        UniqueName="ORG_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TITLE_NAME" HeaderText="Chức danh"
                        UniqueName="TITLE_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                           <tlk:GridDateTimeColumn DataField="FROM_DATE" HeaderText="Từ ngày"
                        UniqueName="FROM_DATE" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                        Visible="true" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn DataField="TO_DATE" HeaderText="Tới ngày"
                        UniqueName="TO_DATE" ShowFilterIcon="false" DataFormatString="{0:dd/MM/yyyy}"
                        CurrentFilterFunction="EqualTo" Visible="true">
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn DataField="SCHOOLS_NAME" HeaderText="Tên trường"
                        UniqueName="SCHOOLS_NAME" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                        Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="SPECIALIZED_TRAIN_NAME" HeaderText="Ngành học"
                        UniqueName="SPECIALIZED_TRAIN_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="LEVEL_NAME" HeaderText="Trình độ"
                        UniqueName="LEVEL_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="CERTIFICATE" HeaderText="Bằng cấp/Chứng chỉ"
                        UniqueName="CERTIFICATE" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="RESULT_TRAIN_NAME" HeaderText="Xếp loại"
                        UniqueName="RESULT_TRAIN_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains" Visible="true">
                    </tlk:GridBoundColumn> 
                    <tlk:GridBoundColumn DataField="YEAR_GRA" HeaderText="Năm tốt nghiệp"
                        UniqueName="YEAR_GRA" ShowFilterIcon="false" Visible="true">
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn DataField="CONTENT_TRAIN" HeaderText="Nội dung đào tạo"
                        UniqueName="CONTENT_TRAIN" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains" Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FORM_TRAIN_NAME" HeaderText="Hình thức đào tạo"
                        UniqueName="FORM_TRAIN_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Width="120px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
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
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_ApproveTrainingOutCompany_Edit_RadSplitter3');
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {

        }

    </script>
</tlk:RadCodeBlock>

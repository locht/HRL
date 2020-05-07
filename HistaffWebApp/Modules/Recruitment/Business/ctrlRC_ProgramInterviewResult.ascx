<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ProgramInterviewResult.ascx.vb"
    Inherits="Recruitment.ctrlRC_ProgramInterviewResult" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hdProgramID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="600px" Scrolling="None">
        <fieldset style="height: 90%; padding-right: 5px">
            <legend></legend>
            <tlk:RadButton ID="cmdSendEmail" runat="server" CausesValidation="false" Text="<%$ Translate: Gửi thư cảm ơn %>">
            </tlk:RadButton>
            <tlk:RadButton ID="btnSuggestIntern" runat="server" CausesValidation="false" Text="<%$ Translate: DS đề nghị thử việc %>">
            </tlk:RadButton>
            <tlk:RadButton ID="btnExport" runat="server" CausesValidation="false" Text="<%$ Translate: Xuất file mẫu %>">
            </tlk:RadButton>
            <tlk:RadButton ID="btnImport" runat="server" CausesValidation="false" Text="<%$ Translate: Nhập file mẫu %>">
            </tlk:RadButton>
             <table class="table-form">
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbTemplatePrint" runat="server" Text="Biễu mẫu hỗ trợ DS đề nghị thử việc"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" Width="260px" ID="cboSuggestIntern">
                            </tlk:RadComboBox>
                        </td>                       
                    </tr>
                </table>
            <tlk:RadGrid ID="gridCadidate" runat="server" Height="360px" AllowMultiRowEdit="false"
                OnSelectedIndexChanged="gridCadidate_SelectedIndexChanged" AllowSorting="false">
                <MasterTableView DataKeyNames="ID" SkinID="GridSingleSelect" ClientDataKeyNames="ID">
                    <Columns>
                        <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                        </tlk:GridClientSelectColumn>
                        <tlk:GridBoundColumn DataField="ID" Visible="false" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ứng viên %>" DataField="Code"
                            UniqueName="Code" SortExpression="Code" ReadOnly="true" HeaderStyle-Width="70px" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FullName" UniqueName="FullName"
                            SortExpression="FullName" ReadOnly="true" HeaderStyle-Width="120px" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới tính %>" DataField="Gender"
                            UniqueName="Gender" SortExpression="Gender" ReadOnly="true" HeaderStyle-Width="60px" />
                        <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="DOB"
                            UniqueName="DOB" SortExpression="DOB" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px"
                            ReadOnly="true">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </tlk:GridDateTimeColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi sinh %>" DataField="BIRTH_PROVINCE"
                            UniqueName="BIRTH_PROVINCE" SortExpression="BIRTH_PROVINCE" ReadOnly="true" HeaderStyle-Width="60px" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="Status"
                            UniqueName="Status" SortExpression="Status" ReadOnly="true" HeaderStyle-Width="60px" />
                    </Columns>
                </MasterTableView><ClientSettings EnablePostBackOnRowClick="true">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <HeaderStyle HorizontalAlign="Center" Width="150px" />
            </tlk:RadGrid>

            </fieldset>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" MinWidth="200" Width="730px" Scrolling="None">
        <fieldset style="height: 90%">
            <legend></legend>
            <table class="table-form" width="100%">
                <tr>
                    <td style="width: 120px">
                        <%# Translate("Vòng phỏng vấn")%>:
                    </td>
                    <td colspan="3">
                        <tlk:RadTextBox ID="lblExamName_Interview" runat="server" ReadOnly="true" Width="200px"
                            Font-Bold="true" BorderWidth="0">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%# Translate("Người phỏng vấn")%>:
                    </td>
                    <td>
                        <asp:Label ID="lblProctor" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td>
                        <%# Translate("Kết quả")%>:
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cbbStatus" runat="server">
                            <Items>
                                <tlk:RadComboBoxItem runat="server" Selected="True" Text="Chưa có kết quả" Value="-1" />
                                <tlk:RadComboBoxItem runat="server" Text="Không đạt phỏng vấn" Value="0" />
                                <tlk:RadComboBoxItem runat="server" Text="Đạt phỏng vấn" Value="1" />
                            </Items>
                        </tlk:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: text-top" colspan="2">
                        <%# Translate("Nhận xét")%>:
                    </td>
                    <td style="vertical-align: text-top; display: none" colspan="2">
                        <%# Translate("Đánh giá")%>:
                    </td>
                    <td colspan="2">
                        <tlk:RadTextBox ID="txtComment" runat="server" SkinID="Textbox1023" Width="300px">
                        </tlk:RadTextBox>
                    </td>
                    <td colspan="2" style="display: none">
                        <tlk:RadTextBox ID="txtAssessment" runat="server" SkinID="Textbox1023" Width="300px">
                        </tlk:RadTextBox>
                    </td>
                </tr>
            </table>
            <tlk:RadToolBar ID="tbarMain" runat="server" />
            <tlk:RadGrid ID="rgDataInterview" runat="server" Height="230px" AllowMultiRowEdit="false"
                AllowSorting="false">
                <%-- <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                </ClientSettings>--%>
                <MasterTableView DataKeyNames="ID" SkinID="GridSingleSelect" ClientDataKeyNames="ID,EXAM_NAME,ISPASS,IS_PASS,COMMENT_INFO,ASSESSMENT_INFO,PV_PERSON,EXAMS_ORDER">
                    <Columns>
                        <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                        </tlk:GridClientSelectColumn>
                        <tlk:GridBoundColumn DataField="ID" Visible="false" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ứng viên %>" DataField="CANDIDATE_CODE"
                            UniqueName="CANDIDATE_CODE" SortExpression="CANDIDATE_CODE" ReadOnly="true" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên ứng viên %>" DataField="FULLNAME_VN"
                            UniqueName="FULLNAME_VN" SortExpression="FULLNAME_VN" ReadOnly="true" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Vòng phỏng vấn %>" DataField="EXAM_NAME"
                            UniqueName="EXAM_NAME" SortExpression="EXAM_NAME" ReadOnly="true" />
                        <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày phỏng vấn %>" DataField="SCHEDULE_DATE"
                            UniqueName="SCHEDULE_DATE" SortExpression="SCHEDULE_DATE" DataFormatString="{0:dd/MM/yyyy}"
                            ReadOnly="true">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </tlk:GridDateTimeColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhận xét %>" DataField="COMMENT_INFO"
                            SortExpression="COMMENT_INFO" UniqueName="COMMENT_INFO" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                            SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Người phỏng vấn %>" DataField="PV_PERSON"
                            UniqueName="PV_PERSON" SortExpression="PV_PERSON" ReadOnly="true" />
                        <%--  <tlk:GridBoundColumn HeaderText="<%$ Translate: Vòng phỏng vấn %>" DataField="EXAM_NAME"
                            UniqueName="EXAM_NAME" SortExpression="EXAM_NAME" ReadOnly="true" HeaderStyle-Width="200px" />
                        <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày thi %>" DataField="SCHEDULE_DATE"
                            UniqueName="SCHEDULE_DATE" SortExpression="SCHEDULE_DATE" DataFormatString="{0:dd/MM/yyyy}"
                            HeaderStyle-Width="70px" ReadOnly="true">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </tlk:GridDateTimeColumn>
                        <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đạt %>" DataField="ISPASS" HeaderStyle-Width="30px"
                            UniqueName="ISPASS" SortExpression="ISPASS" ShowFilterIcon="true" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhận xét %>" DataField="COMMENT_INFO"
                            SortExpression="COMMENT_INFO" UniqueName="COMMENT_INFO" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Đánh giá %>" DataField="ASSESSMENT_INFO"
                            SortExpression="ASSESSMENT_INFO" UniqueName="ASSESSMENT_INFO" />--%>
                    </Columns>
                </MasterTableView></tlk:RadGrid></fieldset>
    </tlk:RadPane>
</tlk:RadSplitter>
<style type="text/css">
    #ctl00_MainContent_ctrlRC_ProgramUpdateResult_ctrlRC_ProgramInterviewResult_tbarMain
    {
     width: 95% !important;
    }
    #ctl00_MainContent_ctrlRC_ProgramUpdateResult_ctrlRC_ProgramInterviewResult_rgDataInterview
    {
           width: 95% !important;
    }
<</style>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function btnExportClicking(sender, args) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>

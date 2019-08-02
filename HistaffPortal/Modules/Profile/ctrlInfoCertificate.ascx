<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInfoCertificate.ascx.vb"
    Inherits="Profile.ctrlInfoCertificate" %>
<%@ Import Namespace="Common" %>
<style type="text/css">
  .rgDataDiv
  {
      height: 140px !important
      }
}
</style>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidFamilyID" runat="server" />
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<div style="overflow: auto">
    <table class="table-form">
        <tr>
            <%--       <td class="lb" style="width: 150px">
                <%# Translate("Lĩnh vực đào tạo")%><span class="lbReq">*</span>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cbField">
                </tlk:RadComboBox>
                <asp:RequiredFieldValidator ID="reqField" ControlToValidate="cbField" runat="server"
                    ErrorMessage="<%$ Translate: Bạn phải chọn lĩnh vực %>" ToolTip="<%$ Translate: Bạn phải chọn lĩnh vực %>">
                </asp:RequiredFieldValidator>
            </td>--%>
            <td class="lb" style="width: 150px">
                <%# Translate("Thời gian đào tạo từ tháng")%>
            </td>
            <td>
                <tlk:RadMonthYearPicker ID="rdFromDate" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                    DateInput-DateFormat="dd/MM/yyyy">
                </tlk:RadMonthYearPicker>
            </td>
            <td class="lb">
                <%# Translate("Đến tháng")%>
            </td>
            <td>
                <tlk:RadMonthYearPicker ID="rdToDate" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                    DateInput-DateFormat="dd/MM/yyyy">
                </tlk:RadMonthYearPicker>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Năm tốt nghiệp")%>
            </td>
            <td>
                <tlk:RadNumericTextBox ID="txtYear" runat="server" NumberFormat-DecimalDigits="1"
                    NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="4" MinValue="1900"
                    MaxValue="9999" SkinID="Number" CausesValidation="false">
                    <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                </tlk:RadNumericTextBox>
            </td>
            <td class="lb">
                <%# Translate("Tên trường")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtSchool">
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <asp:Label runat="server" ID="lbTrainingForm" Text="Hình thức đào tạo"></asp:Label>
            </td>
            <td>
                <tlk:RadComboBox ID="cboTrainingForm" runat="server">
                </tlk:RadComboBox>
            </td>
            <td class="lb">
                <asp:Label runat="server" ID="lbChuyenNganh" Text="Chuyên ngành"></asp:Label>
            </td>
            <td>
                <tlk:RadTextBox ID="txtChuyenNganh" SkinID="Textbox250" runat="server">
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <asp:Label runat="server" ID="lbTrainingType" Text="Loại hình đào tạo"></asp:Label>
            </td>
            <td>
                <tlk:RadComboBox ID="cboTrainingType" runat="server">
                </tlk:RadComboBox>
            </td>
            <td class="lb">
                <asp:Label runat="server" ID="lbBangCap" Text="Bằng cấp/Chứng chỉ"></asp:Label>
            </td>
            <td>
                <tlk:RadComboBox ID="cboBangCap" runat="server" Width="160px" AutoPostBack="true"
                    CausesValidation="false">
                </tlk:RadComboBox>
            </td>
            <td>
                <asp:Label runat="server" ID="lbRenew" Text="Cần gia hạn"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="is_Renew" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Kết quả đào tạo")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtResultTrain">
                </tlk:RadTextBox>
            </td>
            <td class="lb">
                <%# Translate("Ngày nhận bằng")%>
            </td>
            <td>
                <tlk:RadDatePicker runat="server" ID="rdDayGra">
                </tlk:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Hiệu lực chứng chỉ từ")%>
                <span class="lbReq">*</span>
            </td>
            <td>
                <tlk:RadDatePicker runat="server" ID="rdEffectFrom">
                </tlk:RadDatePicker>
                <asp:RequiredFieldValidator ID="rqEffectFrom" ControlToValidate="rdEffectFrom" runat="server"
                    ErrorMessage="<%$ Translate: Bạn phải chọn ngày hiệu lực %>" ToolTip="<%$ Translate: Bạn phải chọn ngày hiệu lực %>">
                </asp:RequiredFieldValidator>
            </td>
            <td class="lb">
                <%# Translate("Hiệu lực chứng chỉ đến")%>
                <span class="lbReq">*</span>
            </td>
            <td>
                <tlk:RadDatePicker runat="server" ID="rdEffectTo">
                </tlk:RadDatePicker>
                <asp:RequiredFieldValidator ID="rqEffectTo" ControlToValidate="rdEffectTo" runat="server"
                    ErrorMessage="<%$ Translate: Bạn phải chọn ngày hiệu lực %>" ToolTip="<%$ Translate: Bạn phải chọn ngày hiệu lực %>">
                </asp:RequiredFieldValidator>
            </td>
            <td class="lb">
                <%# Translate("Tập tin đính kèm")%>
            </td>
            <td>
                <tlk:RadTextBox ID="txtUploadFile" runat="server">
                </tlk:RadTextBox>
            </td>
            <td>
                <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false" />
                <tlk:RadButton runat="server" ID="btnDownload" Text="<%$ Translate: Tải xuống%>"
                    CausesValidation="false" OnClientClicked="rbtClicked" EnableViewState="false">
                </tlk:RadButton>
            </td>
        </tr>
        <tr style="display: none">
            <td class="lb" style="display: none">
                <%# Translate("Ghi chú")%>
            </td>
            <td style="display: none">
                <tlk:RadTextBox runat="server" ID="txtRemark" SkinID="Textbox1023">
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr style="visibility: hidden">
            <td class="lb">
                <tlk:RadTextBox ID="txtRemindLink" runat="server">
                </tlk:RadTextBox>
            </td>
        </tr>
    </table>
    <tlk:RadGrid PageSize="50" ID="rgCetificateEdit" runat="server" Height="250px" Width="99%">
        <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
        <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,FROM_DATE,TO_DATE,YEAR_GRA,NAME_SHOOLS,FORM_TRAIN_NAME,SPECIALIZED_TRAIN,TYPE_TRAIN_NAME,RESULT_TRAIN,CERTIFICATE,RECEIVE_DEGREE_DATE,EFFECTIVE_DATE_FROM,EFFECTIVE_DATE_TO,UPLOAD_FILE,FILE_NAME,FK_PKEY,FORM_TRAIN_ID,TYPE_TRAIN_ID,CERTIFICATE_ID,IS_RENEWED,STATUS"
            ClientDataKeyNames="ID,EMPLOYEE_ID,FROM_DATE,TO_DATE,YEAR_GRA,NAME_SHOOLS,FORM_TRAIN_NAME,SPECIALIZED_TRAIN,TYPE_TRAIN_NAME,RESULT_TRAIN,CERTIFICATE,RECEIVE_DEGREE_DATE,EFFECTIVE_DATE_FROM,EFFECTIVE_DATE_TO,UPLOAD_FILE,FILE_NAME,FK_PKEY,FORM_TRAIN_ID,TYPE_TRAIN_ID,CERTIFICATE_ID,IS_RENEWED,STATUS"
            Caption="<%$ Translate: Thông tin chỉnh sửa %>">
            <Columns>
                <tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow">
                    <HeaderStyle Wrap="False" Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>
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
               <%-- <tlk:GridNumericColumn DataField="YEAR_GRA" HeaderText="Năm tốt nghiệp" UniqueName="YEAR_GRA"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                    Visible="true">
                </tlk:GridNumericColumn>--%>
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
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="RECEIVE_DEGREE_DATE"
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
                    Visible="false">
                </tlk:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </tlk:RadGrid>
    <br />
    <tlk:RadGrid PageSize="50" ID="rgCetificate" runat="server" Height="248px" Width="99%">
        <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
        <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,FROM_DATE,TO_DATE,YEAR_GRA,NAME_SHOOLS,FORM_TRAIN_NAME,SPECIALIZED_TRAIN,TYPE_TRAIN_NAME,RESULT_TRAIN,CERTIFICATE,RECEIVE_DEGREE_DATE,EFFECTIVE_DATE_FROM,EFFECTIVE_DATE_TO,UPLOAD_FILE,FILE_NAME,FORM_TRAIN_ID,TYPE_TRAIN_ID,CERTIFICATE_ID,IS_RENEWED" 
        ClientDataKeyNames="ID,EMPLOYEE_ID,FROM_DATE,TO_DATE,YEAR_GRA,NAME_SHOOLS,FORM_TRAIN_NAME,SPECIALIZED_TRAIN,TYPE_TRAIN_NAME,RESULT_TRAIN,CERTIFICATE,RECEIVE_DEGREE_DATE,EFFECTIVE_DATE_FROM,EFFECTIVE_DATE_TO,UPLOAD_FILE,FILE_NAME,FORM_TRAIN_ID,TYPE_TRAIN_ID,CERTIFICATE_ID,IS_RENEWED" 
        Caption="<%$ Translate: Thông tin hiện tại %>">
            <Columns>
                <tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow">
                    <HeaderStyle Wrap="False" Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>
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
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="RECEIVE_DEGREE_DATE"
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
                
                <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="UPLOAD_FILE" UniqueName="UPLOAD_FILE"
                    SortExpression="UPLOAD_FILE" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="false">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="FILE_NAME" UniqueName="FILE_NAME"
                    SortExpression="FILE_NAME" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="false">
                </tlk:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </tlk:RadGrid>
    <Common:ctrlUpload ID="ctrlUpload1" runat="server" />
    <tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function clientButtonClicking(sender, args) {

            }

            var enableAjax = true;
            function onRequestStart(sender, eventArgs) {
                eventArgs.set_enableAjax(enableAjax);
                enableAjax = true;
            }

            function OnClientSelectedIndexChanged(sender, eventArgs) {
                var id = sender.get_id();
                var cbo;

            }

            function clearSelectRadcombo(cbo) {
                if (cbo) {
                    cbo.clearItems();
                    cbo.clearSelection();
                    cbo.set_text('');
                }
            }
            function clearSelectRadtextbox(cbo) {
                if (cbo) {
                    cbo.clear();
                }
            }

            function OnClientItemsRequesting(sender, eventArgs) {

            }
            function rbtClicked(sender, eventArgs) {
                enableAjax = false;
            }
            function rgCetificateEditRadGridDeSelecting() { }
            function rgCetificateRadGridDeSelecting() { }
            function rgCetificateEditOnClientRowSelected() { }
            function rgCetificateEditRadGridSelecting() { }
            function rgCetificateOnClientRowSelected() { }
            function rgCetificateRadGridSelecting() { }
             
             
        </script>
    </tlk:RadCodeBlock>
</div>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />

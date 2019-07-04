<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInfoCertificate.ascx.vb"
    Inherits="Profile.ctrlInfoCertificate" %>
<%@ Import Namespace="Common" %>
<style type="text/css">
  .rgDataDiv
  {
      height: 150px !important
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
            <td class="lb" style="width: 150px">
                <%# Translate("Lĩnh vực đào tạo")%><span class="lbReq">*</span>
            </td>
            <td>
                <%--  <tlk:RadTextBox runat="server" ID="txtFullName">
                </tlk:RadTextBox>--%>
                <tlk:RadComboBox runat="server" ID="cbField">
                </tlk:RadComboBox>
                <asp:RequiredFieldValidator ID="reqField" ControlToValidate="cbField" runat="server"
                    ErrorMessage="<%$ Translate: Bạn phải chọn lĩnh vực %>" ToolTip="<%$ Translate: Bạn phải chọn lĩnh vực %>">
                </asp:RequiredFieldValidator>
            </td>
            <td class="lb" style="width: 150px">
                <%# Translate("Thời gian đào tạo từ tháng")%>
            </td>
            <td>
                <tlk:RadDatePicker runat="server" ID="rdFromDate">
                </tlk:RadDatePicker>
            </td>
            <td class="lb">
                <%# Translate("Đến tháng")%>
            </td>
            <td>
                <tlk:RadDatePicker runat="server" ID="rdToDate">
                </tlk:RadDatePicker>
                <%--  <asp:CheckBox runat="server" ID="chkIs_Owner" AutoPostBack="true" Text="<%$ Translate : Là chủ hộ %>" />--%>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Tên trường")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtSchool">
                </tlk:RadTextBox>
            </td>
            <td class="lb">
                <%# Translate("Chuyên ngành")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cbMajor">
                </tlk:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Trình độ")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cbLevel">
                </tlk:RadComboBox>
            </td>
            <td class="lb">
                <%# Translate("Điểm số")%>
            </td>
            <td>
                <tlk:RadNumericTextBox runat="server" ID="txtMark">
                </tlk:RadNumericTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Nội dung đào tạo")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtContentTrain" SkinID="Textbox1023">
                </tlk:RadTextBox>
            </td>
            <td class="lb">
                <%# Translate("Loại hình đào tạo")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtTypeTrain" SkinID="Textbox1023">
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Mã số chứng chỉ")%>
            </td>
            <td colspan="2">
                <tlk:RadTextBox runat="server" ID="txtCodeCertificate">
                </tlk:RadTextBox>
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
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Xếp loại tốt nghiệp")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtClassification" SkinID="Textbox1023">
                </tlk:RadTextBox>
            </td>
            <td class="lb">
                <%# Translate("Năm tốt nghiệp")%>
            </td>
            <td>
                <tlk:RadNumericTextBox runat="server" ID="txtYear">
                </tlk:RadNumericTextBox>
            </td>
            <td class="lb">
            </td>
            <td>
                <asp:CheckBox runat="server" ID="is_Renew" Text="<%$ Translate : Có gia hạn %>" />
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Ghi chú")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtRemark" SkinID="Textbox1023">
                </tlk:RadTextBox>
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
        <tr style="visibility: hidden">
            <td class="lb">
                <tlk:RadTextBox ID="txtRemindLink" runat="server">
                </tlk:RadTextBox>
            </td>
        </tr>
    </table>
    <tlk:RadGrid PageSize="50" ID="rgCetificateEdit" runat="server"  Width="99%">
        <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
        <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,FIELD,FIELD_NAME,FROM_DATE,TO_DATE,SCHOOL_NAME,MAJOR,MAJOR_NAME,LEVEL,LEVEL_NAME,MARK,CONTENT_NAME,TYPE_NAME,CODE_CERTIFICATE,EFFECT_FROM,EFFECT_TO,CLASSIFICATION,YEAR,FK_PKEY,RENEW,REMARK,FILENAME,UPLOAD,STATUS"
            ClientDataKeyNames="ID,EMPLOYEE_ID,FIELD,FIELD_NAME,FROM_DATE,TO_DATE,SCHOOL_NAME,MAJOR,MAJOR_NAME,LEVEL,LEVEL_NAME,MARK,CONTENT_NAME,TYPE_NAME,CODE_CERTIFICATE,EFFECT_FROM,EFFECT_TO,CLASSIFICATION,YEAR,FK_PKEY,RENEW,REMARK,FILENAME,UPLOAD,STATUS"
            Caption="<%$ Translate: Thông tin chỉnh sửa %>">
            <Columns>
                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                </tlk:GridClientSelectColumn>
                <tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>
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
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tệp tin %>" DataField="FILENAME" UniqueName="FILENAME"
                    SortExpression="FILENAME">
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
        </MasterTableView>
    </tlk:RadGrid>
    <br />
    <tlk:RadGrid PageSize="50" ID="rgCetificate" runat="server"  Width="99%">
        <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
        <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID" Caption="<%$ Translate: Thông tin hiện tại %>">
            <Columns>
                <tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow">
                    <HeaderStyle Wrap="False" Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Lĩnh vực đào tạo %>" DataField=""
                    UniqueName="" SortExpression="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridDateTimeColumn UniqueName="" HeaderText="<%$ Translate: Thời gian đào tạo từ tháng %>"
                    ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn UniqueName="" HeaderText="<%$ Translate: Đến tháng %>" ReadOnly="True"
                    DataFormatString="{0:dd/MM/yyyy}" DataField="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên trường %>" DataField="" UniqueName=""
                    SortExpression="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Chuyên ngành %>" DataField="" UniqueName=""
                    SortExpression="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Trình độ %>" DataField="" UniqueName=""
                    SortExpression="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm số %>" DataField="" UniqueName=""
                    SortExpression="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung đào tạo %>" DataField=""
                    UniqueName="" SortExpression="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại hình đào tạo %>" DataField=""
                    UniqueName="" SortExpression="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã số chứng chỉ %>" DataField=""
                    UniqueName="" SortExpression="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridDateTimeColumn UniqueName="" HeaderText="<%$ Translate: Hiệu lực chứng chỉ từ %>"
                    ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn UniqueName="" HeaderText="<%$ Translate: Hiệu lực chứng chỉ đến %>"
                    ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Xếp loại tốt nghiệp %>" DataField=""
                    UniqueName="" SortExpression="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm tốt nghiệp %>" DataField="" UniqueName=""
                    SortExpression="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tệp tin %>" DataField="" UniqueName=""
                    SortExpression="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="" UniqueName=""
                    SortExpression="">
                    <HeaderStyle HorizontalAlign="Center" />
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

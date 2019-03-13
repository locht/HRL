<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlFile.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlFile" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<asp:HiddenField ID="hidHuFileID" runat="server" />
<asp:HiddenField ID="txtFileNameSys" runat="server" />
<asp:HiddenField ID="txtDownloadFile" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal"
    SkinID="Demo">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPaneLeft" runat="server" Height="210px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Tên văn bản")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtFullName">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqFullname" ControlToValidate="txtFullName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên văn bản %>" ToolTip="<%$ Translate: Bạn phải nhập Tên văn bản  %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Số hiệu ban hành")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtNumberCode" SkinID="Textbox15">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tệp tin")%>
                </td>
                <td style="padding-left: 12px;" colspan="3">
                    <tlk:RadAsyncUpload Width="120px" Height="20px" runat="server" ID="_radAsynceUpload1"
                        ControlObjectsVisibility="None" OnClientFileUploaded="fileUploaded1" OnClientValidationFailed="validationFailed"
                        EnableAjaxSkinRendering="true" MaxFileSize="4096000" CssClass="btnChooseImage"
                        HideFileInput="True" DisablePlugins="True" MaxFileInputsCount="1" MultipleFileSelection="Disabled">
                        <Localization Select="<%$ Translate: Chọn file %>" />
                    </tlk:RadAsyncUpload>
                    <asp:LinkButton ID="txtFileName" runat="server" OnClientClick="txtFileName_Click()">
                    </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nơi ban hành")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtAdress" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdFromDate">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdToDate">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="compareFromDate_DeductToDate" runat="server" ErrorMessage="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực %>"
                        ToolTip="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực %>" ControlToValidate="rdToDate"
                        ControlToCompare="rdFromDate" Operator="GreaterThanEqual" Type="Date">
                    </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtNote" SkinID="Textbox1023" Width="100%" />
                </td>
            </tr>
        </table>
        <div style="display: none;">
            <asp:Button runat="server" ID="btnSaveFile" CausesValidation="false" />
            <asp:Button runat="server" ID="btnExportFile" CausesValidation="false" />
            <asp:Button runat="server" ID="btnDownload" CausesValidation="false" />
        </div>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="20" ID="rgHuFile" runat="server" AllowMultiRowSelection="true"
            Height="100%">
            <MasterTableView DataKeyNames="ID,TYPE_ID,TYPE_NAME,NAME,NUMBER_CODE,ADDRESS,FROM_DATE,TO_DATE,FILENAME,FILENAME_SYS,NOTE,ACTFLG"
                ClientDataKeyNames="ID,TYPE_ID,TYPE_NAME,NAME,NUMBER_CODE,ADDRESS,FROM_DATE,TO_DATE,FILENAME,FILENAME_SYS,NOTE,ACTFLG">
                <NoRecordsTemplate>
                    Không có bản ghi nào
                </NoRecordsTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn HeaderStyle-Width="40px">
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="NUMBER_CODE" HeaderText="<%$ Translate: Số hiệu văn bản %>"
                        UniqueName="NUMBER_CODE" Visible="True">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="NAME" HeaderText="<%$ Translate: Tên văn bản %>"
                        UniqueName="NAME" Visible="True">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TYPE_NAME" HeaderText="<%$ Translate: Loại văn bản %>"
                        UniqueName="TYPE_NAME" Visible="True">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn DataField="FROM_DATE" HeaderText="<%$ Translate: Ngày hiệu lực%>"
                        UniqueName="FROM_DATE" Visible="true" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" Width="65px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn DataField="TO_DATE" HeaderText="<%$ Translate: Ngày hết hiệu lực %>"
                        UniqueName="TO_DATE" Visible="true" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" Width="65px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn DataField="ADDRESS" HeaderText="<%$ Translate: Nơi ban hành %>"
                        UniqueName="ADDRESS" Visible="True" EmptyDataText="">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="NOTE" HeaderText="<%$ Translate: Ghi chú %>" UniqueName="NOTE"
                        Visible="True" EmptyDataText="">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    </tlk:GridBoundColumn>
                    <%--<asp:LinkButton ID ="LinkButton1" runat="server" OnClientClick="download(<%$ Eval("FILENAME_SYS").ToString() %>)" Text="Tải tệp">
                    </asp:LinkButton>   --%>
                    <%--<tlk:GridHyperLinkColumn UniqueName="FILENAME_SYS" 
                            FilterControlAltText="Filter column column" DataNavigateUrlFields="FILENAME_SYS" 
                            Text="Tải tệp" >
                    </tlk:GridHyperLinkColumn>--%>
                    <tlk:GridTemplateColumn DataField="FILENAME_SYS" HeaderText="FILENAME_SYS" UniqueName="FILENAME_SYS">
                        <ItemTemplate>
                            <asp:LinkButton ID="txtDownload" runat="server" Text=' Tải tệp' CausesValidation="false"
                                OnClientClick='<%# "txtDownload_Click(""" & Eval("FILENAME_SYS").ToString() &  """)" %>'></asp:LinkButton>
                        </ItemTemplate>
                    </tlk:GridTemplateColumn>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
            <ClientSettings EnablePostBackOnRowClick="True">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<script type="text/javascript">
    var enableAjax = true;
    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }
    function fileUploaded1(sender, args) {
        $get('<%= btnSaveFile.ClientID %>').click();
    }
    function validationFailed(sender, args) {
        sender.deleteFileInputAt(0);
        var message = '<%=Translate("Dung lượng ảnh phải < 4MB") %>';
        var notify104126 = noty({ text: message, dismissQueue: true, type: 'error' });
        setTimeout(function () { $.noty.close(notify104126.options.id); }, 2000);
    }
    function txtFileName_Click(val) {
        enableAjax = false;
        $get('<%= btnExportFile.ClientID %>').click();
    }
    function txtDownload_Click(val) {
        document.getElementById('<%= txtDownloadFile.ClientID %>').value = val;
        //$("#txtDownloadFile").val(value);
        //alert($("#txtDownloadFile").val());
        enableAjax = false;
        $get('<%= btnDownload.ClientID %>').click();
    }
</script>

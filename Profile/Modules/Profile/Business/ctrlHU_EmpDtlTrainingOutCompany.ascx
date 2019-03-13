<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlTrainingOutCompany.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlTrainingOutCompany" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Orientation="Horizontal" Scrolling="None">
    <tlk:RadPane ID="ToolbarPane" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="180px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Từ tháng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadMonthYearPicker ID="rdTuThang" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                        DateInput-DateFormat="dd/MM/yyyy">
                    </tlk:RadMonthYearPicker>
                    <asp:RequiredFieldValidator ID="rqTuThang" ControlToValidate="rdTuThang" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập từ tháng. %>" ToolTip="<%$ Translate: Bạn phải nhập từ tháng. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 200px">
                    <%# Translate("Đến tháng")%>
                </td>
                <td>
                    <tlk:RadMonthYearPicker ID="rdToiThang" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                        DateInput-DateFormat="dd/MM/yyyy" >
                    </tlk:RadMonthYearPicker>
                    <asp:CustomValidator ID="cvalToiThang" runat="server" ErrorMessage="<%$ Translate: Từ tháng không đươc lớn hơn tới tháng. %>"
                        ToolTip="<%$ Translate: Từ tháng không đươc lớn hơn tới tháng. %>">
                    </asp:CustomValidator>
                    <%--<asp:RequiredFieldValidator ID="rqToiThang" ControlToValidate="rdToiThang" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tới tháng. %>" ToolTip="<%$ Translate: Bạn phải nhập tới tháng. %>"> </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Năm tốt nghiệp")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <%--<tlk:RadNumericTextBox runat="server" ID="rntGraduateYear1" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSizes="4" MinValue="1990" MaxValue="9999">
                    </tlk:RadNumericTextBox>--%>

                     <tlk:RadNumericTextBox ID="rntGraduateYear" runat="server" NumberFormat-DecimalDigits="1"
                    NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="4" MinValue="1900"
                    MaxValue="9999" SkinID="Number" CausesValidation="false">
                    <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                </tlk:RadNumericTextBox>


                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntGraduateYear"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập năm tốt nghiệp. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập năm tốt nghiệp. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên trường")%>
                    <span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtTrainingSchool" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtTrainingSchool"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên trường. %>" ToolTip="<%$ Translate: Bạn phải nhập tên trường. %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Hình thức đào tạo")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTrainingForm" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Chuyên ngành")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtChuyenNganh" SkinID="Textbox250" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Kết quả đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtKetQua" SkinID="Textbox250" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Bằng cấp/Chứng chỉ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtBangCap" SkinID="Textbox250" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực chứng chỉ")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFrom" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hiệu lực chứng chỉ")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdTo" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Ngày hiệu lực chứng chỉ phải nhỏ hơn ngày hết hiệu lực chứng chỉ %>"
                        ErrorMessage="<%$ Translate: Ngày hiệu lực chứng chỉ phải nhỏ hơn ngày hết hiệu lực chứng chỉ %>"
                        Type="Date" Operator="GreaterThan" ControlToCompare="rdFrom" ControlToValidate="rdTo"></asp:CompareValidator>
                </td>
                <td class="lb">
                    <%# Translate("Mô tả công việc")%>
                </td>
                <td>
                    <%-- <tlk:RadComboBox ID="cboRemark" runat="server" SkinID="number" Width="160px" >
                    </tlk:RadComboBox>--%>
                    <tlk:RadTextBox ID="txtRemark" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td colspan="3">
                    <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <tlk:RadButton runat="server" ID="btnDownload" Text="<%$ Translate: Tải xuống%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr style="visibility:hidden">
                <td class="lb">
                    <tlk:RadTextBox ID="txtRemindLink" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="RadPane4" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgEmployeeTrain" runat="server" AllowMultiRowSelection="true" Height="100%"
            AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID,UPLOAD_FILE,FILE_NAME" ClientDataKeyNames="ID, FROM_DATE,TO_DATE, YEAR_GRA, NAME_SHOOLS, FORM_TRAIN_ID, SPECIALIZED_TRAIN, RESULT_TRAIN, CERTIFICATE, EFFECTIVE_DATE_FROM, EFFECTIVE_DATE_TO,UPLOAD_FILE,FILE_NAME">
                <NoRecordsTemplate>
                    Không có bản ghi nào
                </NoRecordsTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Display="false"></tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn DataField="FROM_DATE" HeaderText="<%$ Translate: Từ tháng%>"
                        UniqueName="FROM_DATE" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        Visible="true" DataFormatString="{0:MM/yyyy}">
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn DataField="TO_DATE" HeaderText="<%$ Translate: Tới tháng%>"
                        UniqueName="TO_DATE" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:MM/yyyy}"
                        CurrentFilterFunction="EqualTo" Visible="true">
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridNumericColumn DataField="YEAR_GRA" HeaderText="<%$ Translate: Năm tốt nghiệp%>"
                        UniqueName="YEAR_GRA" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        Visible="true">
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn DataField="NAME_SHOOLS" HeaderText="<%$ Translate: Tên trường%>"
                        UniqueName="NAME_SHOOLS" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FORM_TRAIN_NAME" HeaderText="<%$ Translate: Hình thức đào tạo%>"
                        UniqueName="FORM_TRAIN_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="SPECIALIZED_TRAIN" HeaderText="<%$ Translate: Chuyên ngành%>"
                        UniqueName="SPECIALIZED_TRAIN" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="RESULT_TRAIN" HeaderText="<%$ Translate: Kết quả%>"
                        UniqueName="RESULT_TRAIN" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="CERTIFICATE" HeaderText="<%$ Translate: Bằng cấp%>"
                        UniqueName="CERTIFICATE" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECTIVE_DATE_FROM"
                        ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_FROM"
                        UniqueName="EFFECTIVE_DATE_FROM">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EFFECTIVE_DATE_TO"
                        ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_TO"
                        UniqueName="EFFECTIVE_DATE_TO">
                    </tlk:GridDateTimeColumn>
                      <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="UPLOAD_FILE"
                        UniqueName="UPLOAD_FILE" SortExpression="UPLOAD_FILE" Visible="false"/>
                         <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="FILE_NAME"
                        UniqueName="FILE_NAME" SortExpression="FILE_NAME" Visible="false"/>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane4.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>

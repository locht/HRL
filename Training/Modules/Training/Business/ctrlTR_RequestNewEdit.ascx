<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_RequestNewEdit.ascx.vb"
    Inherits="Training.ctrlTR_RequestNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidSenderID" runat="server" />
<tlk:radsplitter id="RadSplitter3" runat="server" width="100%" height="100%" orientation="Horizontal">
    <tlk:radpane id="RadPane1" runat="server" height="30px" scrolling="None">
        <tlk:radtoolbar id="tbarMain" runat="server" onclientbuttonclicking="clientButtonClicking" />
    </tlk:radpane>
    <tlk:radpane id="RadPane2" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin chung")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Phòng ban")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:radtextbox runat="server" id="txtOrgName" readonly="true" width="130px">
                    </tlk:radtextbox>
                    <tlk:radbutton enableembeddedskins="false" id="btnFindOrg" runat="server" skinid="ButtonView"
                        causesvalidation="false">
                    </tlk:radbutton>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Phòng ban %>" ToolTip="<%$ Translate: Bạn phải nhập Phòng ban %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Năm")%><%--<span>content</span> class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:radnumerictextbox runat="server" id="rntxtYear" numberformat-decimaldigits="0"
                        numberformat-groupseparator="" minvalue="1900" maxlength="2999" causesvalidation="false"
                        autopostback="true" width="80px">
                    </tlk:radnumerictextbox>
                    <div style="float: right">
                        <tlk:radbutton id="cbIrregularly" runat="server" toggletype="CheckBox" buttontype="ToggleButton"
                            text="<%$ Translate: Đột xuất%>" causesvalidation="false" autopostback="true">
                        </tlk:radbutton>
                    </div>
                    <%--<asp:RequiredFieldValidator ID="reqYear" ControlToValidate="rntxtYear" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Năm đào tạo %>" ToolTip="<%$ Translate: Bạn phải nhập Năm đào tạo %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Khóa đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:radcombobox runat="server" id="cboPlan" autopostback="true" causesvalidation="false">
                    </tlk:radcombobox>
                    <asp:CustomValidator ID="cusPlan" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Khóa đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Khóa đào tạo %>" ClientValidationFunction="cusPlan">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhóm chương trình")%>
                </td>
                <td>
                    <tlk:radtextbox runat="server" id="txtProgramGroup" readonly="true">
                    </tlk:radtextbox>
                </td>
                <td class="lb">
                    <%# Translate("Hình thức đào tạo")%>
                </td>
                <td>
                    <tlk:radcombobox runat="server" id="cboTrainForm" causesvalidation="false">
                    </tlk:radcombobox>
                </td>
                <td class="lb">
                    <%# Translate("Tính chất nhu cầu")%>
                </td>
                <td>
                    <tlk:radcombobox runat="server" id="cboPropertiesNeed" causesvalidation="false">
                    </tlk:radcombobox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lĩnh vực đào tạo")%>
                </td>
                <td>
                    <tlk:radtextbox runat="server" id="txtTrainField" readonly="true">
                    </tlk:radtextbox>
                </td>
                <td class="lb">
                    <%# Translate("Thời gian dự kiến")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:raddatepicker runat="server" id="rdExpectedDate">
                    </tlk:raddatepicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdExpectedDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Thời gian dự kiến %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Thời gian dự kiến %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Thời gian bắt đầu")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:raddatepicker runat="server" id="rdStartDate">
                    </tlk:raddatepicker>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdStartDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Thời gian bắt đầu %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Thời gian bắt đầu %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nội dung đào tạo")%><span class="lbReq">*</span>
                </td>
                <td colspan="5">
                    <tlk:radtextbox runat="server" id="txtContent" skinid="Textbox1023" width="100%">
                    </tlk:radtextbox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtContent"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Nội dung đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Nội dung đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb" rowspan="4">
                    <%# Translate("Trung tâm đào tạo")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td rowspan="4">
                    <tlk:radlistbox runat="server" id="lstCenter" width="100%" height="100px" checkboxes="true"
                        autopostback="true">
                    </tlk:radlistbox>
                    <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Khóa đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Khóa đào tạo %>" ClientValidationFunction="cusPlan">
                    </asp:CustomValidator>--%>
                </td>
                <td class="lb" rowspan="4">
                    <%# Translate("Giảng viên")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td rowspan="4">
                    <tlk:radlistbox runat="server" id="lstTeacher" width="100%" height="100px" checkboxes="true">
                    </tlk:radlistbox>
                    <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Khóa đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Khóa đào tạo %>" ClientValidationFunction="cusPlan">
                    </asp:CustomValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị chủ trì đào tạo")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:radcombobox runat="server" id="cboUnits" causesvalidation="false">
                    </tlk:radcombobox>
                    <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Khóa đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Khóa đào tạo %>" ClientValidationFunction="cusPlan">
                    </asp:CustomValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chi phí dự kiến")%>
                </td>
                <td>
                    <tlk:radnumerictextbox runat="server" id="rntxtExpectedCost" minvalue="0" numberformat-groupseparator=",">
                    </tlk:radnumerictextbox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đơn vị tiền tệ")%>
                </td>
                <td>
                    <tlk:radcombobox runat="server" id="cboCurrency">
                    </tlk:radcombobox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trạng thái")%>
                </td>
                <td>
                    <tlk:radcombobox runat="server" id="cboStatus">
                    </tlk:radcombobox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mục tiêu")%>
                </td>
                <td colspan="5">
                    <tlk:radtextbox runat="server" id="txtTargetTrain" skinid="Textbox1023" width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Địa điểm tổ chức")%>
                </td>
                <td colspan="5">
                    <tlk:radtextbox runat="server" id="txtVenue" skinid="Textbox1023" width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Người gửi")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:radtextbox runat="server" id="txtSender" readonly="true" width="130px">
                    </tlk:radtextbox>
                    <tlk:radbutton enableembeddedskins="false" id="btnFindSender" runat="server" skinid="ButtonView"
                        causesvalidation="false">
                    </tlk:radbutton>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtSender"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Người gửi %>" ToolTip="<%$ Translate: Bạn phải chọn Người gửi %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Email người gửi")%>
                </td>
                <td>
                    <tlk:radtextbox runat="server" id="txtSenderMail" readonly="true">
                    </tlk:radtextbox>
                </td>
                <td class="lb">
                    <%# Translate("Điện thoại người gửi")%>
                </td>
                <td>
                    <tlk:radtextbox runat="server" id="txtSenderMobile" readonly="true">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày gửi yêu cầu")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:raddatepicker runat="server" id="rdRequestDate">
                    </tlk:raddatepicker>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdRequestDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày gửi yêu cầu %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Ngày gửi yêu cầu %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("File đính kèm")%>
                </td>
                <td colspan="3">
                    <tlk:radbutton id="btnUploadFile" runat="server" text="<%$ Translate: Upload %>"
                        causesvalidation="false" style="padding-right: 20px">
                    </tlk:radbutton>
                    <asp:Label ID="lblFilename" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:radtextbox runat="server" id="txtRemark" skinid="Textbox1023" width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin học viên")%></b>
                    <div style="float: right;">
                        <%# Translate("Số lượng học viên thực tế / theo kế hoạch : ")%>
                        <b>
                            <asp:Label runat="server" ID="lblNumOfRealTrainee" Text="0"></asp:Label>
                            /
                            <asp:Label runat="server" ID="lblNumOfPlanTrainee" Text="0"></asp:Label></b>
                    </div>
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
                <td colspan="4">
                    <tlk:radbutton id="btnAdd" runat="server" text="<%$ Translate: Thêm học viên %>"
                        causesvalidation="false" style="padding-right: 20px">
                    </tlk:radbutton>
                    <tlk:radbutton id="btnRemove" runat="server" text="<%$ Translate: Xóa học viên %>"
                        causesvalidation="false" style="padding-right: 20px">
                    </tlk:radbutton>
                    <tlk:radbutton id="btnExport" runat="server" text="<%$ Translate: Xuất file mẫu %>"
                        causesvalidation="false" onclientclicking="btnExportClicking" style="padding-right: 20px">
                    </tlk:radbutton>
                    <tlk:radbutton id="btnImport" runat="server" text="<%$ Translate: Nhập file mẫu %>"
                        causesvalidation="false" style="padding-right: 20px">
                    </tlk:radbutton>
                </td>
            </tr>
        </table>
        <div style="margin-left: 10px">
            <tlk:radgrid id="rgData" runat="server" height="200px" width="99%">
                <mastertableview datakeynames="EMPLOYEE_ID" clientdatakeynames="EMPLOYEE_ID">
                    <Columns>
                        <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                        </tlk:GridClientSelectColumn>
                        <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                            SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                            SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" />
                        <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                            SortExpression="BIRTH_DATE" UniqueName="BIRTH_DATE" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </tlk:GridDateTimeColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                            SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi nhánh (khối) %>" DataField="COM_NAME" SortExpression="COM_NAME"
                            UniqueName="COM_NAME" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban (đơn vị) %>" DataField="ORG_NAME"
                            SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Công việc liên quan %>" DataField="WORK_INVOLVE_NAME"
                            SortExpression="WORK_INVOLVE_NAME" UniqueName="WORK_INVOLVE_NAME" Visible="false" />
                    </Columns>
                </mastertableview>
            </tlk:radgrid>
        </div>
    </tlk:radpane>
</tlk:radsplitter>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlUpload ID="ctrlUpload2" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:radcodeblock id="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function cusPlan(oSrc, args) {
            var cbo = $find("<%# cboPlan.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
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

        function btnExportClicking(sender, args) {
            enableAjax = false;
        }
        function clientButtonClicking(sender, args) {
//            if (args.get_item().get_commandName() == 'CANCEL') {
//                                getRadWindow().close(null);
//                                args.set_cancel(true);
//            }
        }

    </script>
</tlk:radcodeblock>
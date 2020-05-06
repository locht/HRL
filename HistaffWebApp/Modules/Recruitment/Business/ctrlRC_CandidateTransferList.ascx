<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_CandidateTransferList.ascx.vb"
    Inherits="Recruitment.ctrlRC_CandidateTransferList" %>
<%@ Import Namespace="Common" %>
<style type="text/css">
    .horizontalListbox .rlbItem
    {
        float: left !important;
    }
    .horizontalListbox .rlbGroup, .RadListBox
    {
        width: auto !important;
        border: none !important;
    }
    .RadListBox_Default .rlbGroup
    {
        border: none !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane3" runat="server" Height="230px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <asp:HiddenField ID="hidOrg" runat="server" />
        <asp:HiddenField ID="hidTitle" runat="server" />
        <asp:HiddenField ID="hidProgramID" runat="server" />
        <%--   <fieldset style="height: 100px">
            <legend>
                <%# Translate("Thông tin về yêu cầu tuyển dụng")%></legend>--%>
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin chương trình tuyển dụng")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày gửi yêu cầu")%>
                </td>
                <td>
                    <asp:Label ID="lblSendDate" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td class="lb">
                    <%# Translate("Phòng ban")%>
                </td>
                <td>
                    <asp:Label ID="lblOrgName" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td class="lb">
                    <%# Translate("Vị trí tuyển dụng")%>
                </td>
                <td>
                    <asp:Label ID="lblTitleName" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã tuyển dụng")%>
                </td>
                <td>
                    <asp:Label ID="lblCode" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td class="lb">
                    <%# Translate("Số lượng cần tuyển")%>
                </td>
                <td>
                    <asp:Label ID="lblRequestNumber" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td class="lb">
                    <%# Translate("Số lượng đã tuyển")%>
                </td>
                <td>
                    <asp:Label ID="lblNumberHaveRecruit" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lý do tuyển dụng")%>
                </td>
                <td>
                    <asp:Label ID="lblRecruitReason" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td class="lb">
                    <%# Translate("Loại tuyển dụng")%>
                </td>
                <td>
                    <asp:Label ID="lblTypeRecruit" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td class="lb">
                    <%# Translate("Trạng thái chương trình")%>
                </td>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
        <%--       </fieldset>
        <fieldset style="height: 70px">
            <legend>
                <%# Translate("Chọn các tiêu chí để lọc dữ liệu")%></legend>--%>
        <table class="table-form">
            <tr style="height: 5px">
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Chọn các tiêu chí để lọc dữ liệu")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    <tlk:RadListBox runat="server" CssClass="horizontalListbox" ID="rlbStatus" CheckBoxes="true"
                        AllowReorder="true" ShowCheckAll="true" OnClientItemChecked="onItemChecked">
                        <ButtonSettings ShowReorder="false" />
                        <Items>
                        </Items>
                        <%--            <Items>
                <tlk:RadListBoxItem Text="Ứng viên thi đạt"  Value="DAT"/>
                <tlk:RadListBoxItem Text="Ứng viên trúng tuyển" Value="TRUNGTUYEN"  />
                <tlk:RadListBoxItem Text="Ứng viên nội bộ" Value="NOIBO" />
                <tlk:RadListBoxItem Text="Ứng viên tiềm năng" Value="TIEMNANG" />
                 <tlk:RadListBoxItem Text="Đã gửi thư mời thử việc" Value="THUMOI" />
                <tlk:RadListBoxItem Text="Ứng viên đã là nhân viên"  Value="NHANVIEN" />
            </Items>--%>
                    </tlk:RadListBox>
                </td>
                <td>
                    <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm %>">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
        <%--   </fieldset>--%>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadTabStrip ID="rtabCandidateTransfer" runat="server" CausesValidation="false"
            MultiPageID="RadMultiPage1" AutoPostBack="false">
            <Tabs>
                <tlk:RadTab runat="server" ID="rtIdElect" PageViewID="rpvElect" Text="<%$ Translate: Xác nhận trúng tuyển %>"
                    Selected="True">
                </tlk:RadTab>
                <tlk:RadTab runat="server" ID="rtIdResult" PageViewID="rpvResult" Text="<%$ Translate: Kết quả môn thi %>">
                </tlk:RadTab>
                <tlk:RadTab runat="server" ID="rtIdAspiration" PageViewID="rpvAspiration" Text="<%$ Translate: Nguyện vọng %>">
                </tlk:RadTab>
            </Tabs>
        </tlk:RadTabStrip>
        <tlk:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" runat="server" Width="100%"
            Height="380px">
            <tlk:RadPageView ID="rpvElect" runat="server" Width="100%" Height="380px">
                <tlk:RadGrid ID="rgCandidateList" runat="server" Width="100%" Height="200px">
                    <MasterTableView DataKeyNames="ID,CANDIDATE_CODE,ID_CANDIDATE" ClientDataKeyNames="ID,CANDIDATE_CODE,STATUS_CODE,ID_CANDIDATE">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="STATUS_CODE" Visible="false" />
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="ID_CANDIDATE" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ứng viên %>" DataField="CANDIDATE_CODE"
                                UniqueName="CANDIDATE_CODE" SortExpression="CANDIDATE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULLNAME_VN"
                                UniqueName="FULLNAME_VN" SortExpression="FULLNAME_VN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới tính %>" DataField="GENDER_NAME"
                                UniqueName="GENDER_NAME" SortExpression="GENDER_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                                UniqueName="BIRTH_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="BIRTH_DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: CMND %>" DataField="ID_NO" UniqueName="ID_NO"
                                SortExpression="ID_NO" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp %>" DataField="ID_DATE"
                                UniqueName="ID_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="ID_DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi sinh %>" DataField="BIRTH_PLACE_NAME"
                                UniqueName="BIRTH_PLACE_NAME" SortExpression="BIRTH_PLACE_NAME" />--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate:Địa chỉ hiện tại %>" DataField="PER_ADDRESS"
                                UniqueName="PER_ADDRESS" SortExpression="PER_ADDRESS" />
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate:Số điện thoại %>" DataField="CONTACT_MOBILE"
                                UniqueName="CONTACT_MOBILE" SortExpression="CONTACT_MOBILE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate:Thông tin đánh giá %>" DataField="ASSESS_NAME"
                                UniqueName="ASSESS_NAME" SortExpression="ASSESS_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người phỏng vấn %>" DataField="ASSESSOR"
                                UniqueName="ASSESSOR" SortExpression="ASSESSOR" />--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Email %>" DataField="PER_EMAIL" UniqueName="PER_EMAIL"
                                SortExpression="PER_EMAIL" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpvResult" runat="server" Width="100%" Height="360px">
                <tlk:RadGrid ID="rgResult" runat="server" Height="200px" AllowFilteringByColumn="true"
                    AllowPaging="true" PageSize="50">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ứng viên %>" DataField="CANDIDATE_CODE"
                                UniqueName="CANDIDATE_CODE" SortExpression="CANDIDATE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULLNAME_VN"
                                UniqueName="FULLNAME_VN" SortExpression="FULLNAME_VN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vòng phỏng vấn %>" DataField="EXAMS_NAME"
                                UniqueName="EXAMS_NAME" SortExpression="EXAMS_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate:Nhận xét %>" DataField="COMMENT_INFO"
                                UniqueName="COMMENT_INFO" SortExpression="COMMENT_INFO" />
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Thang điểm %>" DataField="POINT_LADDER"
                                UniqueName="POINT_LADDER" SortExpression="POINT_LADDER" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm đạt %>" DataField="POINT_PASS"
                                UniqueName="POINT_PASS" SortExpression="POINT_PASS" />--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả %>" DataField="STATUS_NAME"
                                UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" />
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate:Thông tin đánh giá %>" DataField="ASSESSMENT_INFO"
                                UniqueName="ASSESSMENT_INFO" SortExpression="ASSESSMENT_INFO" />--%>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày phỏng vấn %>" DataField="SCHEDULE_DATE"
                                UniqueName="SCHEDULE_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="SCHEDULE_DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người phỏng vấn %>" DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpvAspiration" runat="server" Width="100%" Height="360px">
                <tlk:RadGrid ID="rgAspiration" runat="server" Height="200px" Width="100%" AllowScroll="true">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,CANDIDATE_CODE,ID_CANDIDATE,PLACE_WORK,RECEIVE_FROM,RECEIVE_TO,PROBATION_FROM,PROBATION_TO,TIME_WORK,STARTDATE_WORK,PROBATION_SALARY,OFFICAL_SALARY,OTHER_SUGGESTIONS">
                        <Columns>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="ID_CANDIDATE" Visible="false" />
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ứng viên %>" DataField="CANDIDATE_CODE"
                                UniqueName="CANDIDATE_CODE" SortExpression="CANDIDATE_CODE" HeaderStyle-Width="70px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULLNAME_VN"
                                UniqueName="FULLNAME_VN" SortExpression="FULLNAME_VN" HeaderStyle-Width="150px" />
                            <tlk:GridTemplateColumn HeaderStyle-Width="180px" HeaderText="<%$ Translate: Thời gian làm việc %>"
                                DataField="TIME_WORK" UniqueName="TIME_WORK" SortExpression="TIME_WORK">
                                <ItemTemplate>
                                    
                                    <tlk:RadComboBox runat="server" ID="TIME_WORK" Width="180px" > 
                                    </tlk:RadComboBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderStyle-Width="180px" HeaderText="<%$ Translate: Nơi làm việc %>"
                                DataField="PLACE_WORK" SortExpression="PLACE_WORK" UniqueName="PLACE_WORK">
                                <ItemTemplate>
                                    <tlk:RadTextBox ID="PLACE_WORK" runat="server" CausesValidation="false" Width="170px"
                                        Text='<%# (Eval("PLACE_WORK")) %>'>
                                    </tlk:RadTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderStyle-Width="125px" HeaderText="<%$ Translate: Nhận việc từ ngày %>"
                                DataField="RECEIVE_FROM" SortExpression="RECEIVE_FROM" UniqueName="RECEIVE_FROM">
                                <ItemTemplate>
                                    <tlk:RadDatePicker ID="RECEIVE_FROM" runat="server" Width="120px" DbSelectedDate='<%# (Eval("RECEIVE_FROM")) %>'>
                                    </tlk:RadDatePicker>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderStyle-Width="125px" HeaderText="<%$ Translate: Nhận việc đến ngày %>"
                                DataField="RECEIVE_TO" SortExpression="RECEIVE_TO" UniqueName="RECEIVE_TO">
                                <ItemTemplate>
                                    <tlk:RadDatePicker ID="RECEIVE_TO" runat="server" Width="120px" DbSelectedDate='<%# (Eval("RECEIVE_TO")) %>'>
                                    </tlk:RadDatePicker>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderStyle-Width="125px" HeaderText="<%$ Translate: Thử việc từ ngày %>"
                                DataField="PROBATION_FROM" SortExpression="PROBATION_FROM" UniqueName="PROBATION_FROM">
                                <ItemTemplate>
                                    <tlk:RadDatePicker ID="PROBATION_FROM" runat="server" Width="120px" DbSelectedDate='<%# (Eval("PROBATION_FROM")) %>'>
                                    </tlk:RadDatePicker>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderStyle-Width="125px" HeaderText="<%$ Translate: Thử việc đến ngày %>"
                                DataField="PROBATION_TO" SortExpression="PROBATION_TO" UniqueName="PROBATION_TO">
                                <ItemTemplate>
                                    <tlk:RadDatePicker ID="PROBATION_TO" runat="server" Width="120px" DbSelectedDate='<%# (Eval("PROBATION_TO")) %>'>
                                    </tlk:RadDatePicker>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderStyle-Width="125px" HeaderText="<%$ Translate: Ngày bắt đầu làm việc %>"
                                DataField="STARTDATE_WORK" UniqueName="STARTDATE_WORK" SortExpression="STARTDATE_WORK">
                                <ItemTemplate>
                                    <tlk:RadDatePicker ID="STARTDATE_WORK" runat="server" Width="120px" DbSelectedDate='<%# (Eval("STARTDATE_WORK")) %>'>
                                    </tlk:RadDatePicker>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderStyle-Width="125px" HeaderText="<%$ Translate: Mức lương thử việc %>"
                                DataField="PROBATION_SALARY" UniqueName="PROBATION_SALARY" SortExpression="PROBATION_SALARY">
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="PROBATION_SALARY" runat="server" CausesValidation="false"
                                        Width="120px" Text='<%# (Eval("PROBATION_SALARY")) %>'>
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderStyle-Width="125px" HeaderText="<%$ Translate: Mức lương chính thức %>"
                                DataField="OFFICAL_SALARY" UniqueName="OFFICAL_SALARY" SortExpression="OFFICAL_SALARY">
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="OFFICAL_SALARY" runat="server" CausesValidation="false"
                                        Width="120px" Text='<%# (Eval("OFFICAL_SALARY")) %>'>
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Đề nghị khác %>" DataField="OTHER_SUGGESTIONS"
                                UniqueName="OTHER_SUGGESTIONS" SortExpression="OTHER_SUGGESTIONS" HeaderStyle-Width="125px">
                                <ItemTemplate>
                                    <tlk:RadTextBox ID="OTHER_SUGGESTIONS" runat="server" CausesValidation="false" Width="120px"
                                        Text='<%# (Eval("OTHER_SUGGESTIONS")) %>'>
                                    </tlk:RadTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPageView>
        </tlk:RadMultiPage>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane4" runat="server" Height="70px" Scrolling="None">
        <table class="table-form">
            <tr>
                <td>
                    <tlk:RadButton ID="btnTrungTuyen" runat="server" Text="<%$ Translate: Xác nhận offer %>">
                    </tlk:RadButton>
                </td>
                <td>
                    <tlk:RadButton ID="btnKhongTrungTuyen" runat="server" Text="<%$ Translate: Từ chối trúng tuyển %>">
                    </tlk:RadButton>
                </td>
                <td>
                    <tlk:RadButton ID="btnPontential" runat="server" Text="<%$ Translate: Lưu ứng viên tiềm năng %>">
                    </tlk:RadButton>
                </td>
                <td>
                    <tlk:RadButton ID="btnBlacklist" runat="server" Text="<%$ Translate: Blacklist %>">
                    </tlk:RadButton>
                </td>
                <td>
                    <tlk:RadButton ID="btnSendEmailReceive" runat="server" Text="<%$ Translate: Gửi email thư mời nhận việc %>"
                        OnClientClicked="DisableAjax" Visible="false">
                    </tlk:RadButton>
                </td>
                <%-- <td>
                    <tlk:RadButton ID="btnReceive" runat="server" Text="<%$ Translate: Thư mời nhận việc %>"
                        OnClientClicked="DisableAjax">
                    </tlk:RadButton>
                </td>--%>
                <%--  <td>
                    <tlk:RadButton ID="btnThankLetter" runat="server" Text="<%$ Translate: Thư cảm ơn %>"
                        OnClientClicked="DisableAjax">
                    </tlk:RadButton>
                </td>--%>
                <td>
                    <tlk:RadButton ID="btnTransfer" runat="server" Text="<%$ Translate: Chuyển sang HSNV %>">
                    </tlk:RadButton>
                </td>
                <%-- <td colspan="2">
                    <tlk:RadButton ID="cmdYCTDKhac" runat="server" Width="100%" Text="<%$ Translate: Chuyển sang vị trí tuyển dụng khác %>">
                    </tlk:RadButton>
                </td>--%>
                <td>
                    <tlk:RadButton ID="btnExportContract" runat="server" Text="<%$ Translate: Xuất tờ trình ký HĐLĐ thử việc %>"
                        OnClientClicked="DisableAjax" Visible="false">
                    </tlk:RadButton>
                </td>
                <td>
                    <tlk:RadButton ID="btnLĐ" runat="server" Text="<%$ Translate: Gửi email thông báo tiếp nhận LĐ thử việc  %>"
                        OnClientClicked="DisableAjax" Visible="false">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBoxTransferProgram" runat="server" />
<asp:PlaceHolder ID="FindOrgTitle" runat="server"></asp:PlaceHolder>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            OnClientBeforeClose="OnClientBeforeClose" Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }

        function OpenEditWindow(states) {
            var empId = $find('<%# rgCandidateList.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('CANDIDATE_CODE');
            var gUId = $find('<%# rgCandidateList.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');

            var orgID = $get("<%=hidOrg.ClientID %>").value;
            var titleID = $get("<%=hidTitle.ClientID %>").value;
            var programID = $get("<%=hidProgramID.ClientID %>").value;

            window.open('/Default.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&gUID=' + gUId + '&Can=' +
            empId + '&state=' + states + '&ORGID=' + orgID + '&TITLEID=' + titleID + '&PROGRAM_ID=' + programID + '', "_self"); /*
         var pos = $("html").offset();
         oWindow.moveTo(pos.left, pos.top);
         oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */

        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT' || args.get_item().get_commandName() == 'PRINT' || args.get_item().get_commandName() == 'UNLOCK' || args.get_item().get_commandName() == 'PREVIOUS') {
                enableAjax = false;
            }
        }

        function DisableAjax(sender, args) {
            enableAjax = false;
        }
        function btnTransferClick(sender, args) {

            var bCheck = $find('<%# rgCandidateList.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }
        }

        function btnTrungTuyenClick(sender, args) {
            var bCheck = $find('<%# rgCandidateList.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }
        }
        function btnKhongTrungTuyenClick(sender, args) {
            var bCheck = $find('<%# rgCandidateList.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }
        }
        function btnBlacklistClick(sender, args) {
            var bCheck = $find('<%# rgCandidateList.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }
        }

        function btnPontentialClick(sender, args) {
            var bCheck = $find('<%# rgCandidateList.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function onItemChecked(sender, e) {
            var item = e.get_item();
            var items = sender.get_items();
            var checked = item.get_checked();
            var firstItem = sender.getItem(0);
            if (item.get_value() == "All") {
                items.forEach(function (itm) { itm.set_checked(checked); });
            }
            else {
                if (sender.get_checkedItems().length == items.get_count() - 1) {
                    firstItem.set_checked(!firstItem.get_checked());
                }
            }
        }
        function OnClientClose(oWnd, args) {
            $find("<%= rgCandidateList.ClientID %>").get_masterTableView().rebind();
        }

        function OnClientBeforeClose(sender, eventArgs) {
            var arg = eventArgs.get_argument();
            if (!arg) {
                if (!confirm("Bạn có muốn đóng màn hình không?")) {
                    //if cancel is clicked prevent the window from closing
                    args.set_cancel(true);
                }
            }
        }
    </script>
</tlk:RadCodeBlock>

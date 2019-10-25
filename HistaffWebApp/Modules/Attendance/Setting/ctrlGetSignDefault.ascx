<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGetSignDefault.ascx.vb"
    Inherits="Attendance.ctrlGetSignDefault" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<asp:HiddenField ID="hidTitleID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:ValidationSummary ID="valSum" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:radpane id="LeftPane" runat="server" minwidth="200" width="250px" scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:radpane>
    <tlk:radsplitbar id="RadSplitBar1" runat="server" collapsemode="Forward">
    </tlk:radsplitbar>
    <tlk:radpane id="MainPane" runat="server" scrolling="None">
        <tlk:radsplitter id="RadSplitter2" runat="server" width="100%" height="100%" orientation="Horizontal">
            <tlk:radpane id="RadPane3" runat="server" height="35px" scrolling="None">
                <tlk:radtoolbar id="tbarOT" runat="server" onclientbuttonclicking="clientButtonClicking" />
            </tlk:radpane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="150px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã nhân viên")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmpCode" SkinID="ReadOnly" runat="server" Width="130px">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEmpCode"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên. %>"></asp:RequiredFieldValidator>
                            <tlk:RadButton Width="100" ID="btnChooseEmployee" runat="server" CausesValidation="false"
                                SkinID="ButtonView">
                            </tlk:RadButton>
                        </td>
                        <td class="lb">
                            <%# Translate("Họ tên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmpName" SkinID="ReadOnly" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Đơn vị")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtOrg" SkinID="ReadOnly" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Chức danh")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtTitle" SkinID="ReadOnly" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Ca mặc định T2-T6")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSign" runat="server"></tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqSign" ControlToValidate="cboSign"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ca mặc định. %>" 
                                ToolTip="<%$ Translate: Bạn phải chọn ca mặc định. %>"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvalSign" runat="server" ControlToValidate="cboSign"
                                ErrorMessage="<%$ Translate: Ca mặc định không tồn tại hoặc đã ngừng áp dụng. %>"
                                ToolTip="<%$ Translate: Ca mặc định không tồn tại hoặc đã ngừng áp dụng. %>" >
                            </asp:CustomValidator>
                        </td>

                         <td class="lb">
                            <%# Translate("Ca T7")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSignSat" runat="server"></tlk:RadComboBox>
                             <asp:RequiredFieldValidator ID="reqSignSat" ControlToValidate="cboSignSat"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ca này %>" 
                                ToolTip="<%$ Translate: Bạn phải chọn ca này. %>"></asp:RequiredFieldValidator>
                        </td>

                         <td class="lb">
                            <%# Translate("Ca CN")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSignSun" runat="server"></tlk:RadComboBox>
                             <asp:RequiredFieldValidator ID="reqSignSun" ControlToValidate="cboSignSun"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ca này %>" 
                                ToolTip="<%$ Translate: Bạn phải chọn ca này. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực từ")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdFromDate"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực đến")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdToDate"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hết hiệu lực. %>"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực %>"
                                ErrorMessage="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực %>"
                                Type="Date" Operator="GreaterThan" ControlToCompare="rdFromDate" ControlToValidate="rdToDate"></asp:CompareValidator>
                            <asp:CustomValidator ID="cvalEffedate" runat="server" ErrorMessage="<%$ Translate: Khoảng thời gian hiệu lực bị trùng. %>"
                                ToolTip="<%$ Translate: Khoảng thời gian hiệu lực bị trùng. %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mô tả")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                        <tlk:RadGrid PageSize=50 ID="rgWorkschedule" runat="server" Height="100%">
                            <MasterTableView DataKeyNames="ID,EMPLOYEE_CODE,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,EMPLOYEE_NAME,
                                TITLE_ID,TITLE_NAME,ORG_ID,ORG_NAME,EFFECT_DATE_FROM,EFFECT_DATE_TO,SINGDEFAULE,NOTE,SING_SAT,SING_SUN">
                                <Columns>
                                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="EMPLOYEE_NAME"
                                        UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME">
                                        <HeaderStyle Width="150px" />
                                        <ItemStyle Width="150px" />
                                    </tlk:GridBoundColumn>
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
                                        <HeaderStyle Width="150px" />
                                        <ItemStyle Width="150px" />
                                    </tlk:GridBoundColumn>
                                    <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                                        SortExpression="ORG_NAME">
                                        <HeaderStyle Width="200px" />
                                        <ItemStyle Width="200px" />
                                    </tlk:GridBoundColumn>--%>
                                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                        UniqueName="ORG_NAME">
                                        <HeaderStyle Width="200px" />
                                        <ItemTemplate>
                                         <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ORG_NAME") %>'>
                                        </asp:Label>
                                        <tlk:RadToolTip RenderMode="Lightweight" ID="RadToolTip1" runat="server" TargetControlID="Label1"
                                                            RelativeTo="Element" Position="BottomCenter">
                                        <%# DrawTreeByString(DataBinder.Eval(Container, "DataItem.ORG_DESC"))%>
                                        </tlk:RadToolTip>
                                    </ItemTemplate>
                                    </tlk:GridTemplateColumn>
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca T2-T6%>" DataField="SINGDEFAULF_NAME"
                                        UniqueName="SINGDEFAULF_NAME" SortExpression="SINGDEFAULF_NAME" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca  T7%>" DataField="SING_SAT_NAME"
                                        UniqueName="SING_SAT_NAME" SortExpression="SING_SAT_NAME" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca CN%>" DataField="SING_SUN_NAME"
                                        UniqueName="SING_SUN_NAME" SortExpression="SING_SUN_NAME" />

                                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực từ %>" DataField="EFFECT_DATE_FROM"
                                        UniqueName="EFFECT_DATE_FROM" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE_FROM">
                                        <HeaderStyle Width="120px" />
                                    </tlk:GridDateTimeColumn>
                                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực đến %>" DataField="EFFECT_DATE_TO"
                                        UniqueName="EFFECT_DATE_TO" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE_TO">
                                        <HeaderStyle Width="120px" />
                                    </tlk:GridDateTimeColumn>
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                                        UniqueName="ACTFLG" SortExpression="ACTFLG">
                                        <HeaderStyle Width="70px" />
                                    </tlk:GridBoundColumn>
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="NOTE" UniqueName="NOTE"
                                        SortExpression="NOTE">
                                        <HeaderStyle Width="200px" />
                                        <ItemStyle Width="200px" />
                                    </tlk:GridBoundColumn>
                                </Columns>
                                <HeaderStyle Width="100px" />
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                        <Selecting AllowRowSelect="True" />
                                        <ClientEvents OnGridCreated="GridCreated" />
                                        <ClientEvents OnCommand="ValidateFilter" />
                            </ClientSettings>
                            <HeaderStyle HorizontalAlign="Center" />
                        </tlk:RadGrid>
                    </tlk:RadPane>
       </tlk:radsplitter>
    </tlk:radpane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlGetSignDefault_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlGetSignDefault_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlGetSignDefault_RadPane2';
        var validateID = 'MainContent_ctrlGetSignDefault_valSum';
        var oldSize = $('#' + pane1ID).height();

        var txtBoxName = 'ctl00_MainContent_ctrlGetSignDefault_txtEmpName_wrapper';
        var txtBoxTitle = 'ctl00_MainContent_ctrlGetSignDefault_txtTitle_wrapper';

        var enableAjax = true;

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
            registerOnfocusOut(splitterID);
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'SAVE') {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    $('#' + txtBoxName + ',' + '#' + txtBoxTitle).css("width", "100%");
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule', 0, 0, 7);
                }
                else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            }
            if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgWorkschedule.ClientID %>");
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                if (rows.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function cusSign(oSrc, args) {
            var cbo = $find("<%# cboSign.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule', 0, 0, 7);
        }

        function clRadDatePicker() {
            $('#ctl00_MainContent_ctrlGetSignDefault_rdFromDate_dateInput').val('');
            $('#ctl00_MainContent_ctrlGetSignDefault_rdToDate_dateInput').val('');
        }

    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />

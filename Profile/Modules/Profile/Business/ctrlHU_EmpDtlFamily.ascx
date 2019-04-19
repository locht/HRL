<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlFamily.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlFamily" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    #ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_rgFamily_ctl00_ctl02_ctl02_FilterCheckBox_IS_DEDUCT
    {
        display: none;
    }
</style>
<asp:HiddenField ID="hidFamilyID" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal"
    SkinID="Demo">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RightPane" runat="server" Height="200px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb" style="width: 130px">
                    <asp:Label ID="lbFullName" runat="server" Text="Họ tên"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtFullName">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqFullname" ControlToValidate="txtFullName" runat="server"
                        ErrorMessage="Bạn phải nhập họ tên" ToolTip="Bạn phải nhập họ tên">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 150px">
                    <asp:Label ID="lbRelationship" runat="server" Text="Mối quan hệ"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboRelationship" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboRelationship"
                        runat="server" ErrorMessage="Bạn phải chọn Mối quan hệ" ToolTip="Bạn phải chọn Mối quan hệ">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalRelationship" runat="server" ErrorMessage="Bạn phải chọn Mối quan hệ"
                        ToolTip="Bạn phải chọn Mối quan hệ">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbBirthDate" runat="server" Text="Ngày sinh"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdBirthDate">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdBirthDate"
                        runat="server" ErrorMessage="Bạn phải nhập Ngày sinh" ToolTip="Bạn phải nhập Ngày sinh">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbIDNO" runat="server" Text="Số CMND"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtIDNO" SkinID="Textbox15">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbAdress" runat="server" Text="Địa chỉ thường trú"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtAdress" Width="100%" />
                </td>                
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbNguyenQuan" runat="server" Text="Nguyên quán"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboNguyenQuan">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbCareer" runat="server" Text="Nghề nghiệp"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCareer" SkinID="Textbox15">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbTitle" runat="server" Text="Chức danh"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTitle" SkinID="Textbox15">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbTax" runat="server" Text="Mã số thuế"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTax" SkinID="Textbox15">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">                    
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkIsDeduct" AutoPostBack="true" Text="Đối tượng giảm trừ" />
                </td>
                <td class="lb">
                    <asp:Label ID="lbDeductReg" runat="server" Text="Ngày đăng ký giảm trừ"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDeductReg">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbDeductFrom" runat="server" Text="Ngày giảm trừ"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDeductFrom">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDeductTo" runat="server" Text="Ngày kết thúc"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDeductTo">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="compareDeductTo_DeductFrom" runat="server" ErrorMessage="Ngày kết thúc giảm trừ phải lớn hơn ngày bắt đầu"
                        ToolTip="Ngày kết thúc giảm trừ phải lớn hơn ngày bắt đầu"
                        ControlToValidate="rdDeductTo" ControlToCompare="rdDeductFrom" Operator="GreaterThanEqual"
                        Type="Date">
                    </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbRemark" runat="server" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtRemark" SkinID="Textbox1023" Width="100%" Height="35px" />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgFamily" runat="server" AllowMultiRowSelection="true"
            Height="100%">
            <MasterTableView DataKeyNames="ID,ID_NO,FULLNAME,RELATION_ID,IS_DEDUCT,DEDUCT_FROM,DEDUCT_TO,ADDRESS,BIRTH_DATE,REMARK,DEDUCT_REG,TAXTATION,PROVINCE_ID,CAREER,TITLE_NAME"
                ClientDataKeyNames="ID,ID_NO,FULLNAME,RELATION_ID,IS_DEDUCT,DEDUCT_FROM,DEDUCT_TO,ADDRESS,BIRTH_DATE,REMARK,DEDUCT_REG,TAXTATION,PROVINCE_ID,CAREER,TITLE_NAME">
                <NoRecordsTemplate>
                    Không có bản ghi nào
                </NoRecordsTemplate>
                <Columns>
                    <%--<tlk:GridClientSelectColumn HeaderStyle-Width="40px">
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="RELATION_NAME" HeaderText="<%$ Translate: Quan hệ %>"
                        UniqueName="RELATION_NAME" Visible="True">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FULLNAME" HeaderText="<%$ Translate: Họ và tên%>"
                        UniqueName="FULLNAME" Visible="True">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn UniqueName="BIRTH_DATE" HeaderText="<%$ Translate: Ngày sinh%>"
                        ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="BIRTH_DATE">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn DataField="ID_NO" HeaderText="<%$ Translate: CMND %>" UniqueName="ID_NO"
                        Visible="True" EmptyDataText="">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TAXTATION" HeaderText="<%$ Translate: Mã thuế %>" UniqueName="TAXTATION"
                        Visible="True" EmptyDataText="">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn DataField="DEDUCT_REG" HeaderText="<%$ Translate: Ngày đăng ký giảm trừ%>"
                        UniqueName="DEDUCT_REG" Visible="true" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" Width="65px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridCheckBoxColumn DataField="IS_DEDUCT" UniqueName="IS_DEDUCT" HeaderText="<%$ Translate: Giảm trừ%>">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridDateTimeColumn DataField="DEDUCT_FROM" HeaderText="<%$ Translate: Ngày giảm trừ%>"
                        UniqueName="DEDUCT_FROM" Visible="true" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" Width="65px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn DataField="DEDUCT_TO" HeaderText="<%$ Translate: Ngày kết thúc %>"
                        UniqueName="DEDUCT_TO" Visible="true" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" Width="65px" />
                    </tlk:GridDateTimeColumn>--%>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_RadSplitter2';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_RadPane3';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_RadPane4';
        var pane3ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_RadPane2';
        var pane4ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_RadPane1';
        var validateID = 'MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_valSum';
        var oldSize = $('#' + pane1ID).height();
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
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgFamily', pane3ID, pane4ID);
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_RadSplitter2');
        }

        function clRadDatePicker() {
            $('#ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_rdBirthDate_dateInput').val('');
            $('#ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_rdDeductReg_dateInput').val('');
            $('#ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_rdDeductFrom_dateInput').val('');
            $('#ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_rdDeductTo_dateInput').val('');
        }

    </script>
</tlk:RadScriptBlock>

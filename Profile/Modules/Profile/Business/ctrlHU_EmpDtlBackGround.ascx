<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlBackGround.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlBackGround" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="hidEmployeeID" runat="server" />
<asp:HiddenField ID="hidBackGroundID" runat="server" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="none" SkinID="Demo">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="240px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label1" Text="Ngày hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdEffectiveDate">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <asp:Label runat="server" ID="Label2" Text="Số CMND"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtIdNo">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label3" Text="Ngày cấp"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdLicenseDate">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lb" Text="Nơi cấp"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboLicensePlace" SkinID="LoadDemand">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <asp:Label runat="server" ID="Label4" Text="Điện thoại cố định"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtFixedPhone">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 130px">
                    <asp:Label runat="server" ID="Label5" Text="Điện thoại di động"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtMobilePhone">
                    </tlk:RadTextBox>
                </td>
            </tr>

            <tr>
                <td class="lb3">
                    <asp:Label runat="server" ID="lbPerAddress" Text="Địa chỉ thường trú"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtPermanentAddress" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label6" Text="Quốc gia"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboNation_Per" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label7" Text="Thành phố"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboProvince_Per" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label8" Text="Quận/Huyện"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboDistrict_Per" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label9" Text="Xã Phường"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboWard_Per" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
            </tr>

            <tr>
                <td class="lb3">
                    <asp:Label runat="server" ID="Label10" Text="Địa chỉ hiện tại"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtCurrentAddress" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label11" Text="Quốc gia"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboNation_Cur" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label12" Text="Thành phố"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboProvince_Cur" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label13" Text="Quận/Huyện"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboDistrict_Cur" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label14" Text="Xã Phường"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboWard_Cur" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
            </tr>


        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgGrid" runat="server" AllowMultiRowSelection="true"
            Height="100%">
            <%--COMPANY_NAME,JOIN_DATE,END_DATE,COMPANY_ADDRESS,TELEPHONE,SALARY,TITLE_NAME,LEVEL_NAME,TER_REASON--%>
            <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,EFFECTIVE_DATE,ID_NO,LICENSE_DATE,LICENSE_PLACE,LICENSE_PLACE_ID,MOBILE_PHONE,FIXED_PHONE,PERMANENT_ADDRESS_F,PERMANENT_NATION_NAME,PERMANENT_PROVINCE_NAME,PERMANENT_DISTRICT_NAME,PERMANENT_WARD_NAME,CURRENT_ADDRESS_F,CURRENT_NATION_NAME,CURRENT_PROVINCE_NAME,CURRENT_DISTRICT_NAME,CURRENT_WARD_NAME,
                CURRENT_NATION_ID,CURRENT_PROVINCE_ID,CURRENT_DISTRICT_ID,CURRENT_WARD_ID,PERMANENT_NATION_ID,PERMANENT_PROVINCE_ID,PERMANENT_DISTRICT_ID,PERMANENT_WARD_ID"

                ClientDataKeyNames="ID,EMPLOYEE_ID,EFFECTIVE_DATE,ID_NO,LICENSE_DATE,LICENSE_PLACE,LICENSE_PLACE_ID,MOBILE_PHONE,FIXED_PHONE,PERMANENT_ADDRESS_F,PERMANENT_NATION_NAME,PERMANENT_PROVINCE_NAME,PERMANENT_DISTRICT_NAME,PERMANENT_WARD_NAME,CURRENT_ADDRESS_F,CURRENT_NATION_NAME,CURRENT_PROVINCE_NAME,CURRENT_DISTRICT_NAME,CURRENT_WARD_NAME,
                CURRENT_NATION_ID,CURRENT_PROVINCE_ID,CURRENT_DISTRICT_ID,CURRENT_WARD_ID,PERMANENT_NATION_ID,PERMANENT_PROVINCE_ID,PERMANENT_DISTRICT_ID,PERMANENT_WARD_ID">
                <NoRecordsTemplate>
                    Không có bản ghi nào
                </NoRecordsTemplate>
                <Columns>
                    <%--<tlk:GridClientSelectColumn>
                        <HeaderStyle Width="40px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên công ty %>" DataField="COMPANY_NAME"
                        UniqueName="COMPANY_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số điện thoại %>" DataField="TELEPHONE"
                        UniqueName="TELEPHONE">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ tháng/năm %>" DataField="JOIN_DATE"
                        UniqueName="JOIN_DATE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến tháng/năm %>" DataField="END_DATE"
                        UniqueName="END_DATE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridNumericColumn DataField="SALARY" HeaderText="<%$ Translate: Mức lương %>"
                        UniqueName="SALARY" DataFormatString="{0:n0}">
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp bậc %>" DataField="LEVEL_NAME"
                        UniqueName="LEVEL_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do nghỉ %>" DataField="TER_REASON"
                        UniqueName="TER_REASON">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Visible="False">
                    </tlk:GridBoundColumn>--%>
                </Columns>
                <HeaderStyle Width="120px" />
            </MasterTableView>
            <ClientSettings EnablePostBackOnRowClick="True">
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlBackGround_RadSplitter2';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlBackGround_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlBackGround_RadPane4';
        var pane3ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlBackGround_RadPane2';
        var pane4ID = 'ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlBackGround_tbarMainToolBar';
        var validateID = 'MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlBackGround_valSum';
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
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            //} else if (item.get_commandName() == "SAVE") {
            //    // Nếu nhấn nút SAVE thì resize
            //    if (!Page_ClientValidate(""))
            //        ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgGrid', pane3ID, pane4ID);
            //    else
            //        ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {
                case '<%= cboProvince_Per.ClientID%>':
                    cbo = $find('<%= cboNation_Per.ClientID%>');
                    value = cbo.get_value();
                    break;
                case '<%= cboDistrict_Per.ClientID%>':
                    cbo = $find('<%= cboProvince_Per.ClientID%>');
                    value = cbo.get_value();
                    break;
                case '<%= cboWard_Per.ClientID%>':
                    cbo = $find('<%= cboDistrict_Per.ClientID%>');
                    value = cbo.get_value();
                    break;

                case '<%= cboProvince_Cur.ClientID%>':
                    cbo = $find('<%= cboNation_Cur.ClientID%>');
                    value = cbo.get_value();
                    break;
                case '<%= cboDistrict_Cur.ClientID%>':
                    cbo = $find('<%= cboProvince_Cur.ClientID%>');
                    value = cbo.get_value();
                    break;
                case '<%= cboWard_Cur.ClientID%>':
                    cbo = $find('<%= cboDistrict_Cur.ClientID%>');
                    value = cbo.get_value();
                    break;

                default:
                    break;
            }
            if (!value) {
                value = 0;
            }
            var context = eventArgs.get_context();
            context["valueCustom"] = value;
            context["value"] = sender.get_value();
        }

        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                case '<%= cboProvince_Per.ClientID%>':
                    cbo = $find('<%= cboDistrict_Per.ClientID%>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboWard_Per.ClientID%>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboDistrict_Per.ClientID%>':
                    cbo = $find('<%= cboWard_Per.ClientID%>');
                    clearSelectRadcombo(cbo);
                    break;

                case '<%= cboProvince_Cur.ClientID%>':
                    cbo = $find('<%= cboDistrict_Cur.ClientID%>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboWard_Cur.ClientID%>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboDistrict_Cur.ClientID%>':
                    cbo = $find('<%= cboWard_Cur.ClientID%>');
                    clearSelectRadcombo(cbo);
                    break;
                
                default:
                    break;
            }
        }

        function clRadDatePicker() {
            $('#ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlBackGround_rdEffectiveDate_dateInput').val('');
            $('#ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlBackGround_rdLicenseDate_dateInput').val('');
        }
    </script>
</tlk:RadScriptBlock>

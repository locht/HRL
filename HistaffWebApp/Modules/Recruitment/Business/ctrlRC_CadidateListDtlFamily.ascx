<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_CadidateListDtlFamily.ascx.vb"
    Inherits="Recruitment.ctrlRC_CadidateListDtlFamily" %>
<%@ Register Src="~/Modules/Profile/Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    #ctl00_MainContent_ctrlRC_CadidateListDtl_ctrlRC_CadidateListDtlFamily_rgFamily_ctl00_ctl02_ctl02_FilterCheckBox_IS_DEDUCT
    {
        display: none;
    }
</style>
<asp:HiddenField ID="hidFamilyID" runat="server" />
<asp:HiddenField ID="hidEmployeeid" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal"
    SkinID="Demo">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
          <Profile:ctrlEmpBasicInfo  runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RightPane" runat="server" Height="200px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <asp:CheckBox runat="server" ID="chkIsEmployee" AutoPostBack="true" />
                </td>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="Người thân đang là CBNV trong công ty"></asp:Label>
                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="Working_ID"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải Chọn Hồ Sơ Lương. %>" ToolTip="<%$ Translate: Bạn phải Chọn Hồ Sơ Lương. %>"> </asp:RequiredFieldValidator>--%>
                </td>
                <td>
                    <tlk:RadButton ID="btnIsEmployee" runat="server" CausesValidation="false" ToolTip=""
                        SkinID="ButtonView" Width="26px">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <asp:Label ID="lbFullName" runat="server" Text="Họ tên"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtFullName">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqFullname" ControlToValidate="txtFullName" runat="server"
                        ErrorMessage="Bạn phải nhập họ tên" ToolTip="Bạn phải nhập họ tên">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbBirthDate" runat="server" Text="Ngày sinh"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdBirthDate">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdBirthDate"
                        runat="server" ErrorMessage="Bạn phải nhập Ngày sinh" ToolTip="Bạn phải nhập Ngày sinh">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDieDate" runat="server" Text="Ngày mất"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDieDate">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb" style="width: 150px">
                    <asp:Label ID="lbRelationship" runat="server" Text="Mối quan hệ"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboRelationship">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboRelationship"
                        runat="server" ErrorMessage="Bạn phải chọn Mối quan hệ" ToolTip="Bạn phải chọn Mối quan hệ">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalRelationship" Enabled="false" runat="server" ErrorMessage="Bạn phải chọn Mối quan hệ"
                        ToolTip="Bạn phải chọn Mối quan hệ">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label3" Text="Chủ hộ"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkHousehold" runat="server" AutoPostBack="true" />
                </td>
                <td class="lb">
                    <asp:Label ID="Label1" runat="server" Text="Số hộ khẩu"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSoHoKhau" SkinID="Textbox250" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkIsDeduct" Text="Đối tượng giảm trừ" />
                </td>
                <td class="lb">
                    <%# Translate("Giới tính")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboGender" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboGender"
                        runat="server" ErrorMessage="Bạn phải chọn giới tính" ToolTip="Bạn phải chọn giới tính">
                    </asp:RequiredFieldValidator>
                </td>
                <td style="display: none">
                    <asp:Label ID="lbNguyenQuan" runat="server" Text="Nguyên quán"></asp:Label>
                </td>
                <td style="display: none">
                    <tlk:RadComboBox runat="server" ID="cboNguyenQuan">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbIDNO" runat="server" Text="Số CMND"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtIDNO" SkinID="Textbox15">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số điện thoại")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtPhone" />
                </td>
                <td class="lb">
                    <asp:Label ID="lbCareer" runat="server" Text="Nghề nghiệp"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCareer">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbCollecttion" runat="server" Text="Thu nhập"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnCollecttion">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label11" runat="server" Text="Đơn vị công tác"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCompanyWork">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbAdresss" runat="server" Text="Địa chỉ"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtAdress1" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label6" Text="Tỉnh/TP"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboProvince_City1" SkinID="LoadDemand" runat="server" Width="160px"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label5" Text="Quận/Huyện"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDistrict1" SkinID="LoadDemand" runat="server" Width="160px"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbBangCap" Text="Phường/Xã"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCommune1" SkinID="LoadDemand" runat="server" Width="160px"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label4" runat="server" Text="Địa chỉ nơi đăng ký giấy khai sinh"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtHamlet1" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Quốc gia")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboNationlity" SkinID="LoadDemand" AutoPostBack="true">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label8" Text="Tỉnh/TP"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboProvince_City2" SkinID="LoadDemand" runat="server" Width="160px"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label9" Text="Quận/Huyện"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDistrict2" SkinID="LoadDemand" runat="server" Width="160px"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label10" Text="Phường/Xã"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCommune2" SkinID="LoadDemand" runat="server" Width="160px"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbRemark" runat="server" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="7">
                    <tlk:RadTextBox runat="server" ID="txtRemark" SkinID="Textbox1023" Width="100%" Height="35px" />
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <%# Translate("Ngày cấp CMND")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdIDDate">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Nơi cấp CMND")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtIDPlace" SkinID="Textbox15">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <%--  -------------------------------------------------------------%>
            <tr style="display: none">
                <td class="lb">
                    <asp:Label ID="Label2" runat="server" Text="Mã hộ gia đình"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMaHoGiaDinh" SkinID="Textbox250" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td style="display: none">
                    <asp:Label ID="lbTitle" runat="server" Text="Chức danh"></asp:Label>
                </td>
                <td style="display: none">
                    <tlk:RadTextBox runat="server" ID="txtTitle">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <%--  -------------------------------------------------------------%>
            <%---------------------------------------------------------------%>
            <tr style="display: none">
                <td class="lb">
                    <asp:Label ID="Label7" runat="server" Text="Địa chỉ tạm trú: "></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtAdress_TT" Width="100%" />
                </td>
            </tr>
            <%--  -------------------------------------------------------------%>
            <tr style="display: none">
                <td class="lb">
                    <asp:Label ID="Label12" runat="server" AutoPostBack="true" Text="Đã mất"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkDaMat" AutoPostBack="true" />
                </td>
                <td class="lb">
                    <asp:Label ID="lbTax" runat="server" Text="Mã số thuế"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTax">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày cấp MST")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdMSTDate">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <%# Translate("Nơi cấp mã số thuế")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txt_MSTPLACE" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <asp:Label ID="lbDeductReg" runat="server" Text="Ngày đăng ký giảm trừ"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDeductReg" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDeductFrom" runat="server" Text="Ngày giảm trừ"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDeductFrom" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDeductTo" runat="server" Text="Ngày kết thúc"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDeductTo" Enabled="false">
                    </tlk:RadDatePicker>
                    <%--<asp:CompareValidator ID="compareDeductTo_DeductFrom" runat="server" ErrorMessage="Ngày kết thúc giảm trừ phải lớn hơn ngày bắt đầu"
                        ToolTip="Ngày kết thúc giảm trừ phải lớn hơn ngày bắt đầu"
                        ControlToValidate="rdDeductTo" ControlToCompare="rdDeductFrom" Operator="GreaterThanEqual"
                        Type="Date">
                    </asp:CompareValidator>--%>
                </td>
            </tr>
            <tr style="display: none">
                <td colspan="7" style="color: red">
                    <asp:Label runat="server" ID="lbNKS" Text="Nơi đăng ký khai sinh:"></asp:Label>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <%# Translate("Giấy khai sinh")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtBIRTH_CODE">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Quyển")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtQuyen">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Quốc tịch")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboNATIONALITYFAMILY" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting" AutoPostBack="true">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <%# Translate("Tỉnh/Thành phố")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cbTempKtPROVINCE_ID" SkinID="LoadDemand" Width="160px"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Quận/Huyện")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cbTempKtDISTRICT_ID" SkinID="LoadDemand" Width="160px"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Xã/Phường")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cbTempKtWARD_ID" SkinID="LoadDemand" Width="160px"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgFamily" runat="server" AllowMultiRowSelection="true"
            Height="100%">
            <MasterTableView DataKeyNames="ID,ID_NO,FULLNAME,RELATION_ID,IS_DEDUCT,DEDUCT_FROM,DEDUCT_TO,ADDRESS,ADDRESS_TT,BIRTH_DATE,REMARK,DEDUCT_REG,TAXTATION,PROVINCE_ID,CAREER,TITLE_NAME,CERTIFICATE_CODE,CERTIFICATE_NUM,AD_VILLAGE,IS_OWNER,IS_PASS,AD_PROVINCE_ID,AD_PROVINCE_NAME,AD_DISTRICT_ID,AD_DISTRICT_NAME,AD_WARD_ID,AD_WARD_NAME,TT_PROVINCE_ID,TT_PROVINCE_NAME,TT_DISTRICT_ID,TT_DISTRICT_NAME,TT_WARD_NAME,
            BIRTH_CODE,QUYEN,BIRTH_NATION_ID,BIRTH_PROVINCE_ID,BIRTH_DISTRICT_ID,BIRTH_WARD_ID,DIE_DATE,COMPANY_WORK,IS_EMPLOYEE,SALARY_EARN"
                ClientDataKeyNames="ID,ID_NO,FULLNAME,RELATION_ID,IS_DEDUCT,DEDUCT_FROM,DEDUCT_TO,ADDRESS,ADDRESS_TT,BIRTH_DATE,REMARK,DEDUCT_REG,TAXTATION,PROVINCE_ID,CAREER,TITLE_NAME,CERTIFICATE_CODE,CERTIFICATE_NUM,AD_VILLAGE,IS_OWNER,IS_PASS,AD_PROVINCE_ID,AD_PROVINCE_NAME,AD_DISTRICT_ID,AD_DISTRICT_NAME,AD_WARD_ID,AD_WARD_NAME,TT_PROVINCE_ID,TT_PROVINCE_NAME,TT_DISTRICT_ID,TT_DISTRICT_NAME,TT_WARD_NAME,NATION_ID,ID_NO_DATE,ID_NO_PLACE_NAME,PHONE,TAXTATION_DATE,TAXTATION_PLACE,
            BIRTH_CODE,QUYEN,BIRTH_NATION_ID,BIRTH_PROVINCE_ID,BIRTH_DISTRICT_ID,BIRTH_WARD_ID,TT_WARD_ID,TT_WARD_NAME,GENDER,DIE_DATE,COMPANY_WORK,IS_EMPLOYEE,SALARY_EARN">
                <NoRecordsTemplate>
                    Không có bản ghi nào
                </NoRecordsTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn HeaderStyle-Width="40px">
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="FULLNAME" HeaderText="<%$ Translate: Họ và tên%>"
                        UniqueName="FULLNAME" Visible="True">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn UniqueName="BIRTH_DATE" HeaderText="<%$ Translate: Ngày sinh%>"
                        ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="BIRTH_DATE">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn UniqueName="DIE_DATE" HeaderText="<%$ Translate: Ngày mất%>"
                        ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="DIE_DATE">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn DataField="RELATION_NAME" HeaderText="<%$ Translate: Quan hệ %>"
                        UniqueName="RELATION_NAME" Visible="True">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridCheckBoxColumn AllowFiltering="false" DataField="IS_OWNER" UniqueName="IS_OWNER"
                        HeaderText="<%$ Translate: Chủ hộ%>">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridBoundColumn DataField="CERTIFICATE_NUM" HeaderText="<%$ Translate: Số hộ khẩu%>"
                        UniqueName="CERTIFICATE_NUM" Visible="True">
                    </tlk:GridBoundColumn>
                    <tlk:GridCheckBoxColumn DataField="IS_DEDUCT" UniqueName="IS_DEDUCT" HeaderText="<%$ Translate: Đối tượng giảm trừ%>">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridBoundColumn DataField="GENDER_NAME" HeaderText="<%$ Translate: Giới tính %>"
                        UniqueName="GENDER_NAME" Visible="True">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ID_NO" HeaderText="<%$ Translate: CMND %>" UniqueName="ID_NO"
                        Visible="True" EmptyDataText="">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="PHONE" HeaderText="<%$ Translate: Số điện thoại %>"
                        UniqueName="PHONE" Visible="True">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="CAREER" HeaderText="<%$ Translate: Nghề nghiệp %>"
                        UniqueName="CAREER" Visible="True">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridNumericColumn DataField="SALARY_EARN" HeaderText="<%$ Translate: Thu nhập %>"
                        UniqueName="SALARY_EARN">
                    </tlk:GridNumericColumn>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="FindIsEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlRC_CadidateListDtl_ctrlRC_CadidateListDtlFamily_RadSplitter2';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlRC_CadidateListDtl_ctrlRC_CadidateListDtlFamily_RadPane3';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlRC_CadidateListDtl_ctrlRC_CadidateListDtlFamily_RadPane4';
        var pane3ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlRC_CadidateListDtl_ctrlRC_CadidateListDtlFamily_RadPane2';
        var pane4ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlRC_CadidateListDtl_ctrlRC_CadidateListDtlFamily_RadPane1';
        var validateID = 'MainContent_ctrlRC_CadidateListDtl_ctrlRC_CadidateListDtlFamily_valSum';
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

        //        function GridCreated(sender, eventArgs) {
        //            registerOnfocusOut(splitterID);
        //        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter();
                else
                    ResizeSplitterDefault();
            }
            else if (item.get_commandName() == "NEW") {
                enableAjax = false;
            }
            else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }
        //        function GridCreated(sender, eventArgs) {
        //            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlRC_CadidateListDtl_ctrlRC_CadidateListDtlFamily_RadSplitter2');
        //        }

        function clRadDatePicker() {
            $('#ctl00_MainContent_ctrlRC_CadidateListDtl_ctrlRC_CadidateListDtlFamily_rdBirthDate_dateInput').val('');
            $('#ctl00_MainContent_ctrlRC_CadidateListDtl_ctrlRC_CadidateListDtlFamily_rdDeductReg_dateInput').val('');
            $('#ctl00_MainContent_ctrlRC_CadidateListDtl_ctrlRC_CadidateListDtlFamily_rdDeductFrom_dateInput').val('');
            $('#ctl00_MainContent_ctrlRC_CadidateListDtl_ctrlRC_CadidateListDtlFamily_rdDeductTo_dateInput').val('');
        }
        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter2.ClientID%>");
                var pane = splitter.getPaneById('<%= RightPane.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter2.ClientID%>");
            var pane = splitter.getPaneById('<%= RightPane.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane4.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                case '<%= cboProvince_City1.ClientID %>':
                    cbo = $find('<%= cboDistrict1.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboCommune1.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboDistrict1.ClientID %>':
                    cbo = $find('<%= cboCommune1.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;

                case '<%= cboProvince_City2.ClientID %>':
                    cbo = $find('<%= cboDistrict2.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboCommune2.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboDistrict2.ClientID %>':
                    cbo = $find('<%= cboCommune2.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;

                case '<%= cbTempKtPROVINCE_ID.ClientID%>':
                    cbo = $find('<%= cbTempKtDISTRICT_ID.ClientID%>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cbTempKtWARD_ID.ClientID%>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cbTempKtDISTRICT_ID.ClientID%>':
                    cbo = $find('<%= cbTempKtWARD_ID.ClientID%>');
                    clearSelectRadcombo(cbo);
                    break;
                default:
                    break;
            }
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
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {

                case '<%= cboDistrict1.ClientID %>':
                    cbo = $find('<%= cboProvince_City1.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboCommune1.ClientID %>':
                    cbo = $find('<%= cboDistrict1.ClientID %>');
                    value = cbo.get_value();
                    break;

                case '<%= cboDistrict2.ClientID %>':
                    cbo = $find('<%= cboProvince_City2.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboCommune2.ClientID %>':
                    cbo = $find('<%= cboDistrict2.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cbTempKtDISTRICT_ID.ClientID%>':
                    cbo = $find('<%= cbTempKtPROVINCE_ID.ClientID%>');
                    value = cbo.get_value();
                    break;
                case '<%= cbTempKtWARD_ID.ClientID%>':
                    cbo = $find('<%= cbTempKtDISTRICT_ID.ClientID%>');
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

    </script>
</tlk:RadScriptBlock>

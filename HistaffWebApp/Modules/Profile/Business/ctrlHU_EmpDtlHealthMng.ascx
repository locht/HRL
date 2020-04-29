﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlHealthMng.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlHealthMng" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal"
    SkinID="Demo">
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgGrid" runat="server" AllowFilteringByColumn="true"
            Height="100%">
            <MasterTableView DataKeyNames="">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                        ReadOnly="true" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="60px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên nhân viên %>" DataField="EMPLOYEE_NAME"
                        ReadOnly="true" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                        ReadOnly="true" UniqueName="ORG_NAME" HeaderStyle-Width="200px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        ReadOnly="true" UniqueName="TITLE_NAME" HeaderStyle-Width="200px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR_KHAMBENH"
                        ReadOnly="true" UniqueName="YEAR_KHAMBENH" HeaderStyle-Width="100px" DataFormatString="{0:yyyy}" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đợt khám %>" DataField="DATE_KHAMBENH"
                        ReadOnly="true" UniqueName="DATE_KHAMBENH" HeaderStyle-Width="100px" DataFormatString="{0:dd/MM/yyyy}" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Cân nặng %>" DataField="CAN_NANG"
                        ReadOnly="true" UniqueName="CAN_NANG" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Huyết áp %>" DataField="HUYET_AP"
                        ReadOnly="true" UniqueName="HUYET_AP" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội khoa %>" DataField="NOI_KHOA"
                        ReadOnly="true" UniqueName="NOI_KHOA" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngoại khoa %>" DataField="NGOAI_KHOA"
                        ReadOnly="true" UniqueName="NGOAI_KHOA" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phụ khoa %>" DataField="PHU_KHOA"
                        ReadOnly="true" UniqueName="PHU_KHOA" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mắt %>" DataField="MAT" ReadOnly="true"
                        UniqueName="MAT" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: TMH %>" DataField="TMH" ReadOnly="true"
                        UniqueName="TMH" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: RHM %>" DataField="RHM" ReadOnly="true"
                        UniqueName="RHM" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Da liễu %>" DataField="DA_LIEU" ReadOnly="true"
                        UniqueName="DA_LIEU" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Công thức máu %>" DataField="CONG_THUC_MAU"
                        ReadOnly="true" UniqueName="CONG_THUC_MAU" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đường máu %>" DataField="DUONG_MAU"
                        ReadOnly="true" UniqueName="DUONG_MAU" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Xét nghiệm nước tiểu %>" DataField="XN_NUOC_TIEU"
                        ReadOnly="true" UniqueName="XN_NUOC_TIEU" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: X Quang %>" DataField="X_QUANG" ReadOnly="true"
                        UniqueName="X_QUANG" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điện tim %>" DataField="DIEN_TIM"
                        ReadOnly="true" UniqueName="DIEN_TIM" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Siêu âm %>" DataField="SIEU_AM" ReadOnly="true"
                        UniqueName="SENIORITY" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: XN Phân %>" DataField="XN_PHAN" ReadOnly="true"
                        UniqueName="XN_PHAN" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Siêu vi A %>" DataField="SIEU_VI_A"
                        ReadOnly="true" UniqueName="SIEU_VI_A" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Siêu vi E %>" DataField="SIEU_VI_E"
                        ReadOnly="true" UniqueName="SIEU_VI_E" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Siêu vi B %>" DataField="SIEU_VI_B"
                        ReadOnly="true" UniqueName="SIEU_VI_B" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Khán thể viêm gan B %>" DataField="KT_VIEN_GAN_B"
                        ReadOnly="true" UniqueName="KT_VIEN_GAN_B" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức năng gan %>" DataField="CHUC_NANG_GAN"
                        ReadOnly="true" UniqueName="CHUC_NANG_GAN" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức năng thận %>" DataField="CHUC_NANG_THAN"
                        ReadOnly="true" UniqueName="CHUC_NANG_THAN" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mỡ máu %>" DataField="MO_MAU" ReadOnly="true"
                        UniqueName="MO_MAU" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: KQ1 %>" DataField="KQ1" ReadOnly="true"
                        UniqueName="KQ1" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: KQ2 %>" DataField="KQ2" ReadOnly="true"
                        UniqueName="KQ2" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: KQ3 %>" DataField="KQ3" ReadOnly="true"
                        UniqueName="KQ3" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: KQ4 %>" DataField="KQ4" ReadOnly="true"
                        UniqueName="KQ4" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: KQ5 %>" DataField="KQ5" ReadOnly="true"
                        UniqueName="KQ5" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: KQ6 %>" DataField="KQ6" ReadOnly="true"
                        UniqueName="KQ6" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: KQ7 %>" DataField="KQ7" ReadOnly="true"
                        UniqueName="KQ7" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: KQ8 %>" DataField="KQ8" ReadOnly="true"
                        UniqueName="KQ8" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: KQ9 %>" DataField="KQ9" ReadOnly="true"
                        UniqueName="KQ9" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: KQ10 %>" DataField="KQ10" ReadOnly="true"
                        UniqueName="KQ10" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại sức khỏe %>" DataField="HEALTH_TYPE"
                        ReadOnly="true" UniqueName="HEALTH_TYPE" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm bệnh %>" DataField="NHOM_BENH"
                        ReadOnly="true" UniqueName="NHOM_BENH" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên bệnh %>" DataField="TEN_BENH"
                        ReadOnly="true" UniqueName="TEN_BENH" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết luận %>" DataField="KET_LUAB"
                        ReadOnly="true" UniqueName="KET_LUAB" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="GHI_CHU" ReadOnly="true"
                        UniqueName="GHI_CHU" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đo thính thị lực sơ bộ %>" DataField="DO_THINH_LUC_SB"
                        ReadOnly="true" UniqueName="DO_THINH_LUC_SB" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đo thính thị lực hoàn chỉnh %>" DataField="DO_THINH_LUC_HC"
                        ReadOnly="true" UniqueName="DO_THINH_LUC_HC" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đo chức năng hô hấp %>" DataField="DO_CN_HO_HAP"
                        ReadOnly="true" UniqueName="DO_CN_HO_HAP" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: XN hàm lượng TOLUEN %>" DataField="XN_HAMLUONG_TOLUEN"
                        ReadOnly="true" UniqueName="XN_HAMLUONG_TOLUEN" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Bệnh NN 1 %>" DataField="BENH_NN1"
                        ReadOnly="true" UniqueName="BENH_NN1" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Bệnh NN 2 %>" DataField="BENH_NN2"
                        ReadOnly="true" UniqueName="BENH_NN2" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Bệnh TN, NN cần theo dõi %>" DataField="BENH_TN_NN"
                        ReadOnly="true" UniqueName="BENH_TN_NN" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày điểu trị %>" DataField="NGAY_DIEU_TRI"
                        ReadOnly="true" UniqueName="NGAY_DIEU_TRI" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phương pháp điều trị %>" DataField="PP_DIEU_TRI"
                        ReadOnly="true" UniqueName="PP_DIEU_TRI" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả điều trị %>" DataField="KQ_DIEU_TRI"
                        ReadOnly="true" UniqueName="KQ_DIEU_TRI" HeaderStyle-Width="100px" />
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <Scrolling AllowScroll="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

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
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlHealthMng_RadSplitter2');
        }

    </script>
</tlk:RadScriptBlock>
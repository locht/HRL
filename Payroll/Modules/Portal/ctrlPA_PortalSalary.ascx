<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_PortalSalary.ascx.vb"
    Inherits="Payroll.ctrlPA_PortalSalary" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<table class="table-form">
    <tr>
        <td class="lb">
            <%# Translate("Năm")%>
        </td>
        <td>
            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="true"
                TabIndex="12" Width="80px">
            </tlk:RadComboBox>
        </td>
        <td class="lb">
            <%# Translate("Kỳ lương")%>
        </td>
        <td>
            <tlk:RadComboBox ID="rcboPeriod" runat="server" AutoPostBack="false">
            </tlk:RadComboBox>
        </td>
        <td>
            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Xem %>" SkinID="ButtonFind">
            </tlk:RadButton>
        </td>
    </tr>
</table>
<table style="width: 98%" border="0" cellpadding="0" cellspacing="0" class="show-data ui-widget ui-widget-content">
    <tr>
        <td style="padding-left: 50px">
            <asp:Label ID="Label23" runat="server"></asp:Label>
        </td>
        <td>
            <h2>THÔNG TIN LƯƠNG</h2>
        </td>
    </tr>
</table>
<br />
<div class="all-pl">
    <div class="all-l" >
        <table style="width: 100%;">
            <tr>
                <td colspan="2">Họ tên</td>
                <td style="font-size: medium;">
                    <asp:Label ID="lblFULLNAME_VN" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2">Phòng ban</td>
                <td>
                    <asp:Label ID="lblORG_NAME" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2">Số ngày làm việc đầy đủ</td>
                <td>
                    <asp:Label ID="lblBL_3" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2">Số ngày nghỉ phép</td>
                <td>
                    <asp:Label ID="lblBL_4" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2">Ngày nghỉ hưởng lương</td>
                <td>
                    <asp:Label ID="lblBL_5" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2">Mức lương chính:</td>
                <td>
                    <asp:Label ID="lblBL_2" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">Hệ số PA:</td>
                <td>
                    <asp:Label ID="lblADD1" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 150px" rowspan="6"><strong>Tổng tiền lương và các  khoản thu nhập khác</strong></td>
                <td style="width: 260px">Lương tính theo ngày công  trong tháng</td>
                <td>
                    <asp:Label ID="lblBL_14" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Phụ cấp ăn trưa</td>
                <td>
                    <asp:Label ID="lblBL_17" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Tiền làm thêm giờ</td>
                <td>
                <td>
                    <asp:Label ID="lblBL_15_BL_16" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Thu nhập chịu thuế khác</td>
                <td>
                    <asp:Label ID="lblBL_18" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Thu nhập không chịu thuế  khác</td>
                <td>
                    <asp:Label ID="lblBL_19" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Thưởng sales-commission</td>
                <td>
                    <asp:Label ID="lblBL_20" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td rowspan="5"><strong>Các khoản phải khấu trừ vào lương</strong></td>
                <td height="28">BHXH do người lao động nộp  (8%)</td>
                <td>
                    <asp:Label ID="lblTT_BHXH_NV" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>BHYT do người lao động nộp (1.5%)</td>
                <td>
                    <asp:Label ID="lblTT_BHYT_NV" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>BHTN do người lao động nộp  (1%)</td>
                <td>
                    <asp:Label ID="lblTT_BHTN_NV" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Thuế TNCN tạm thu</td>
                <td>
                    <asp:Label ID="lblBL_25" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Tạm ứng từ Kế toán</td>
                <td>
                    <asp:Label ID="lblBL_27" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" align="center"><strong>Thực nhận</strong></td>
                <td>
                    <asp:Label ID="lblBL_29" runat="server"></asp:Label></td>
            </tr>
        </table>
    </div>
    <div class="all-r">
       <%-- <img src="../../Static/Images/logo_luong.png" />--%>
        <img src="../../Static/Images/logo_note.png" />
    </div>
</div>
<style>
    .all-pl {
    float: left;
    width: 100%;
    min-height: 500px;
    }

    .all-l {
    padding-left: 50px;
    font-size: larger;
    border: 1px;
    width: 50%;
    float: left;
    }

    .all-r {
    float: right;
    width: 45%;
    }

        .all-r img {
        width: 100%;
        margin-bottom: 50px;
        }
    #WindowMainRegion {
        min-height:500px;
    }
</style>
<script type="text/javascript">

    function popupclose(sender, args) {
        var m;
        var arg = args.get_argument();
        if (arg == '1') {
            m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
            var n = noty({ text: m, dismissQueue: true, type: 'success' });
            setTimeout(function () { $.noty.close(n.options.id); }, 5000);

        }

    }

    var enableAjax = true;
    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }
</script>

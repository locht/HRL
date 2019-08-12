<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtl.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtl" %>
<script type="text/javascript" language="javascript">
    function findGetParameter(parameterName) {
        var result = null,
        tmp = [];
        var items = location.search.substr(1).split("&");
        for (var index = 0; index < items.length; index++) {
            tmp = items[index].split("=");
            if (tmp[0] === parameterName) result = decodeURIComponent(tmp[1]);
        }
        return result;
    }
    $(document).ready(function () {
        if (findGetParameter("state") == "Edit" || findGetParameter("state") == "New") {
            var links = $('#ctl00_MainContent_ctrlHU_EmpDtl_rpbProfile').find('a');

            for (var i = 0; i < links.length; i++) {
                links[i].setAttribute("data-href", links[i].getAttribute("href"));
                links[i].removeAttribute("href");
                links[i].onclick = function () {
                    window.location = "#";
                };
            }
        }

    });
</script>
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%">
    <tlk:RadPane ID="RadPaneLeft" runat="server" MinWidth="230" MaxWidth="230" Width="230px">
        <div class="box_img">
            <asp:PlaceHolder ID="ViewPlaceHoderImage" runat="server"></asp:PlaceHolder>
        </div>
        <div class="fullname">
            <asp:Label ID="lblFullName" runat="server"></asp:Label>
        </div>
        <tlk:RadPanelBar ID="rpbProfile" runat="server" Width="230px" Enabled="false">
            <Items>
                <tlk:RadPanelItem Expanded="True">
                    <Items>
                        <tlk:RadPanelItem Value="ctrlHU_EmpDtlProfile" Text="<%$ Translate:Hồ sơ nhân viên%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlProfile&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlHU_EmpDtlWorking" Text="<%$ Translate:Quá trình công tác tại công ty %>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlWorking&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlHU_EmpDtlWorkingBefore" Text="<%$ Translate:Quá trình công tác trước đây%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlWorkingBefore&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlHU_EmpDtlSalary" Text="<%$ Translate:Quá trình lương - phụ cấp%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlSalary&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlHU_EmpDtlContract" Text="<%$ Translate:Quá trình ký hợp đồng lao động%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlContract&state=Normal", EmployeeID) %>' />
                        <%-- <tlk:RadPanelItem Value="ctrlHU_EmpDtlTraining" Text="<%$ Translate:Quá trình đào tạo trong công ty%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlTraining&state=Normal", EmployeeID) %>' />--%>
                        <tlk:RadPanelItem Value="ctrlHU_EmpDtlTrainingOutCompany" Text="<%$ Translate:Quản lý bằng cấp/chứng chỉ%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlTrainingOutCompany&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlHU_EmpDtlCommend" Text="<%$ Translate:Quá trình khen thưởng%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlCommend&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlHU_EmpDtlConcurrently" Text="<%$ Translate:Quá trình kiêm nhiệm%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlConcurrently&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlHU_EmpDtlDiscipline" Text="<%$ Translate:Quá trình kỷ luật%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlDiscipline&state=Normal", EmployeeID) %>' />
                        <%-- <tlk:RadPanelItem Value="ctrlHU_EmpDtlInsurance" Text="<%$ Translate:Quá trình tham gia bảo hiểm%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlInsurance&state=Normal", EmployeeID) %>' />--%>
                        <tlk:RadPanelItem Value="ctrlHU_EmpDtlFamily" Text="<%$ Translate:Gia cảnh người thân%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlFamily&state=Normal", EmployeeID) %>' />
                        <%--  <tlk:RadPanelItem Value="ctrlHU_EmpDtlHistory" Text="<%$ Translate:Lịch sử chỉnh sửa thông tin %>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlHistory&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlHU_EmpDtlViewKPI" Text="<%$ Translate:Quá trình đánh giá %>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlViewKPI&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlHU_EmpDtlCompetency" Text="<%$ Translate:Quá trình năng lực %>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlCompetency&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlHU_EmpDtlFile" Text="<%$ Translate:Quản lý tập tin văn bản %>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlFile&state=Normal", EmployeeID) %>' />--%>
                    </Items>
                </tlk:RadPanelItem>
            </Items>
        </tlk:RadPanelBar>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <asp:PlaceHolder ID="phProfile" runat="server"></asp:PlaceHolder>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />

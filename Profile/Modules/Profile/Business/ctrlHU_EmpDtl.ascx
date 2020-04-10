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
<tlk:radsplitter id="RadSplitter2" runat="server" height="100%" width="100%">
    <tlk:radpane id="RadPaneLeft" runat="server" minwidth="230" maxwidth="230" width="230px">
        <div class="box_img">
            <asp:PlaceHolder ID="ViewPlaceHoderImage" runat="server"></asp:PlaceHolder>
        </div>
        <div class="fullname">
            <asp:Label ID="lblFullName" runat="server"></asp:Label>
        </div>
        <tlk:radpanelbar id="rpbProfile" runat="server" width="230px" enabled="false">
            <items>
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
                        <tlk:RadPanelItem Value="ctrlHU_EmpDtlAppendix" Text="<%$ Translate:Quá trình ký phụ lục hợp đồng %>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlAppendix&state=Normal", EmployeeID) %>' />
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
                  <%--  <tlk:RadPanelItem Value="ctrlHU_EmpDtlFamily" Text="<%$ Translate:Quan hệ nhân thân%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlFamily&state=Normal", EmployeeID)%>' />--%>
                    <tlk:RadPanelItem Value="ctrlHU_EmpDtlQuantumSal" Text="<%$ Translate:Quá trình ngạch bậc%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&Place=ctrlHU_EmpDtlQuantumSal&state=Normal", EmployeeID) %>' />
                    </Items>
                </tlk:RadPanelItem>
            </items>
        </tlk:radpanelbar>
    </tlk:radpane>
    <tlk:radsplitbar id="RadSplitBar1" runat="server" collapsemode="Forward">
    </tlk:radsplitbar>
    <tlk:radpane id="RadPane2" runat="server" scrolling="None">
        <asp:PlaceHolder ID="phProfile" runat="server"></asp:PlaceHolder>
    </tlk:radpane>
</tlk:radsplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />

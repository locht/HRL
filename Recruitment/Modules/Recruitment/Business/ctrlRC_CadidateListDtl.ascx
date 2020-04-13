<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_CadidateListDtl.ascx.vb"
    Inherits="Recruitment.ctrlRC_CadidateListDtl" %>
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
            var links = $('#ctl00_MainContent_ctrlRC_CadidateListDtl_rpbRecruitment').find('a');

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
<tlk:radsplitter id="RadSplitter2" runat="server" height="100%" width="100%" visible="false">
    <tlk:radpane id="RadPaneLeft" runat="server" minwidth="230" maxwidth="230" width="230px" visible="false">
        <div class="box_img">
            <asp:PlaceHolder ID="ViewPlaceHoderImage" runat="server"></asp:PlaceHolder>
        </div>
        <div class="fullname">
            <asp:Label ID="lblFullName" runat="server"></asp:Label>
        </div>
        <tlk:radpanelbar id="rpbRecruitment" runat="server" width="230px" enabled="false">
            <items>
                <tlk:RadPanelItem Expanded="True">
                    <Items>                    
                      <%--  <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlRecruitment" Text="<%$ Translate:Hồ sơ nhân viên%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlRecruitment&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlWorking" Text="<%$ Translate:Quá trình công tác tại công ty %>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlWorking&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlWorkingBefore" Text="<%$ Translate:Quá trình công tác trước đây%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlWorkingBefore&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlSalary" Text="<%$ Translate:Quá trình lương - phụ cấp%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlSalary&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlContract" Text="<%$ Translate:Quá trình ký hợp đồng lao động%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlContract&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlAppendix" Text="<%$ Translate:Quá trình ký phụ lục hợp đồng %>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlAppendix&state=Normal", EmployeeID) %>' />--%>
                        <%-- <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlTraining" Text="<%$ Translate:Quá trình đào tạo trong công ty%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlTraining&state=Normal", EmployeeID) %>' />--%>
                        <%--<tlk:RadPanelItem Value="ctrlRC_CadidateListDtlTrainingOutCompany" Text="<%$ Translate:Quản lý bằng cấp/chứng chỉ%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlTrainingOutCompany&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlCommend" Text="<%$ Translate:Quá trình khen thưởng%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlCommend&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlConcurrently" Text="<%$ Translate:Quá trình kiêm nhiệm%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlConcurrently&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlDiscipline" Text="<%$ Translate:Quá trình kỷ luật%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlDiscipline&state=Normal", EmployeeID) %>' />--%>
                        <%-- <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlInsurance" Text="<%$ Translate:Quá trình tham gia bảo hiểm%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlInsurance&state=Normal", EmployeeID) %>' />--%>
                        <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlFamily" Text="<%$ Translate:Gia cảnh người thân%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlFamily&state=Normal", EmployeeID) %>' />
                        <%--  <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlHistory" Text="<%$ Translate:Lịch sử chỉnh sửa thông tin %>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlHistory&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlViewKPI" Text="<%$ Translate:Quá trình đánh giá %>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlViewKPI&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlCompetency" Text="<%$ Translate:Quá trình năng lực %>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlCompetency&state=Normal", EmployeeID) %>' />
                        <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlFile" Text="<%$ Translate:Quản lý tập tin văn bản %>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlFile&state=Normal", EmployeeID) %>' />--%>
                  <%--  <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlFamily" Text="<%$ Translate:Quan hệ nhân thân%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlFamily&state=Normal", EmployeeID)%>' />--%>
                   <%-- <tlk:RadPanelItem Value="ctrlRC_CadidateListDtlQuantumSal" Text="<%$ Translate:Quá trình ngạch bậc%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CadidateListDtl&group=Business&emp={0}&Place=ctrlRC_CadidateListDtlQuantumSal&state=Normal", EmployeeID) %>' />--%>
                    </Items>
                </tlk:RadPanelItem>
            </items>
        </tlk:radpanelbar>
    </tlk:radpane>
    <tlk:radsplitbar id="RadSplitBar1" runat="server" collapsemode="Forward">
    </tlk:radsplitbar>
    <tlk:radpane id="RadPane2" runat="server" scrolling="None">
        <asp:PlaceHolder ID="phRecruitment" runat="server"></asp:PlaceHolder>
    </tlk:radpane>
</tlk:radsplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />

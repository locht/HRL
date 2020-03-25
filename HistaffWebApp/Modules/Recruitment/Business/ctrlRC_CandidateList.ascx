<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_CandidateList.ascx.vb"
    Inherits="Recruitment.ctrlRC_CandidateList" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <asp:HiddenField ID="hidOrg" runat="server" />
        <asp:HiddenField ID="hidTitle" runat="server" />
        <asp:HiddenField ID="hidProgramID" runat="server" />
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
            <fieldset style="width: auto; height: auto">
                <legend><b>
                    <%# Translate("Thông tin chương trình tuyển dụng")%>
                </b></legend>
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày gửi yêu cầu")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblSendDate" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb">
                            <%# Translate("Mã tuyển dụng")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblCode" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb" style="display: none">
                            <%# Translate("Tên công việc")%>:
                        </td>
                        <td style="display: none">
                            <asp:Label ID="lblJobName" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb">
                            <%# Translate("Yêu cầu kinh nghiệm")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblExperienceRequired" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Phòng ban")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblOrgName" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb">
                            <%# Translate("Vị trí tuyển dụng")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblTitleName" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb">
                            <%# Translate("Số lượng cần tuyển")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblRequestNumber" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb">
                            <%# Translate("Số lượng đã tuyển")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblQuantityHasRecruitment" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Trạng thái yêu cầu")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblStatusRequest" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb">
                            <%# Translate("Lý do tuyển dụng")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblReasonRecruitment" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb">
                            <%# Translate("Yêu cầu khác")%>:
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblOtherRequest" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset style="width: auto; height: auto">
                <legend><b>
                    <%# Translate("Thông tin tìm kiếm")%></b> </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Ứng viên không đủ điều kiện")%>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkCandidateUnsatisfactory" runat="server" />
                            <%--<tlk:RadTextBox ID="txtCandidateCode" runat="server" Style="display: none">
                            </tlk:RadTextBox>--%>
                        </td>
                        <td class="lb">
                            <%# Translate("Ứng viên đạt")%>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkCandidateQualified" runat="server" />
                        </td>
                        <td class="lb">
                            <%# Translate("Ứng viên trúng tuyển")%>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkElectCandidate" runat="server" />
                        </td>
                        <td class="lb" style="display: none">
                            <%# Translate("Ứng viên tiềm năng")%>
                        </td>
                        <td style="display: none">
                            <asp:CheckBox ID="chkCandidatePotential" runat="server" />
                        </td>
                        <td class="lb" style="display: none">
                            <%# Translate("Ứng viên đã từ chối")%>
                        </td>
                        <td style="display: none">
                            <asp:CheckBox ID="chkCandidateCancel" runat="server" />
                        </td>
                        <td class="lb" style="display: none">
                            <%# Translate("Gửi thư mời")%>
                        </td>
                        <td style="display: none">
                            <asp:CheckBox ID="chkCandidateHavesentmail" runat="server" />
                        </td>
                        <td class="lb">
                            <%# Translate("Đã là nhân viên")%>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkCandiateIsEmp" runat="server" />
                        </td>
                        <td class="lb">
                            <%# Translate("Ứng viên nội bộ")%>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkCandidateIsLocaltion" runat="server" />
                        </td>
                        <td>
                            <tlk:RadButton Width="100px" runat="server" ID="btnSearch" SkinID="ButtonFind" Text="<%$ Translate: Tìm kiếm %>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgCandidateList" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID,Candidate_CODE,STATUS_ID,FULLNAME_VN" ClientDataKeyNames="ID,Candidate_CODE,STATUS_ID,FULLNAME_VN">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ứng viên %>" DataField="Candidate_CODE"
                        UniqueName="Candidate_CODE" SortExpression="Candidate_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULLNAME_VN"
                        UniqueName="FULLNAME_VN" SortExpression="FULLNAME_VN" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                        UniqueName="BIRTH_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="BIRTH_DATE">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: CMND %>" DataField="ID_NO" UniqueName="ID_NO"
                        SortExpression="ID_NO" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi cấp %>" DataField="ID_PLACE_NAME" UniqueName="ID_PLACE_NAME"
                        SortExpression="ID_PLACE_NAME" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp %>" DataField="ID_DATE"
                        UniqueName="ID_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="ID_DATE">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <%--<tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Black list %>" DataField="IS_BLACKLIST"
                        UniqueName="IS_BLACKLIST" SortExpression="IS_BLACKLIST" ShowFilterIcon="true" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: ReHire %>" DataField="IS_REHIRE"
                        UniqueName="IS_REHIRE" SortExpression="IS_REHIRE" ShowFilterIcon="true" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Tiềm năng %>" DataField="IS_PONTENTIAL"
                        UniqueName="IS_PONTENTIAL" SortExpression="IS_PONTENTIAL" ShowFilterIcon="true" />--%>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại ứng viên %>" DataField="STATUS_NAME"
                        UniqueName="STATUS_NAME" />

                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" Width="100px" />
            <ItemStyle Width="100px" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnRowDblClick="gridRowDblClick" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane4" runat="server" Height="50px" Scrolling="None">
        <table class="table-form">
            <tr>
                <td>
                    <tlk:RadButton ID="btnDuDieuKien" runat="server" Text="<%$ Translate: Đủ điều kiện %>"
                        OnClientClicking="btnDuDieuKienClick">
                    </tlk:RadButton>
                </td>
                <td>
                    <tlk:RadButton ID="btnKhongDuDieuKien" runat="server" Text="<%$ Translate: Không đủ điều kiện %>"
                        OnClientClicking="btnKhongDuDieuKienClick">
                    </tlk:RadButton>
                </td>
                <td>
                    <tlk:RadButton ID="btnBlacklist" runat="server" Text="<%$ Translate: Black list %>"
                        OnClientClicking="btnBlacklistClick">
                    </tlk:RadButton>
                </td>
                 <td>
                    <tlk:RadButton ID="btnHSNVTransfer" runat="server" Text="<%$ Translate: [...] Ứng viên nội bộ %>">
                    </tlk:RadButton>
                </td>
                <td>
                    <tlk:RadButton ID="btnPontential" runat="server" Text="<%$ Translate: [...] Ứng viên tiềm năng %>"
                        OnClientClicking="btnPontentialClick" AutoPostBack="false">
                    </tlk:RadButton>
                </td>
                <td>
                    <tlk:RadButton ID="cmdYCTDKhac" runat="server" Text="<%$ Translate: Chuyển sang vị trí tuyển dụng khác %>">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            OnClientBeforeClose="OnClientBeforeClose" Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindOrgTitle" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }

        function OpenEditWindow(states) {
            var empId = $find('<%# rgCandidateList.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('Candidate_CODE');
            var gUId = $find('<%# rgCandidateList.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');

            var orgID = $get("<%=hidOrg.ClientID %>").value;
            var titleID = $get("<%=hidTitle.ClientID %>").value;
            var programID = $get("<%=hidProgramID.ClientID %>").value;

            var oWindow = radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&gUID=' + gUId + '&Can=' +
            empId + '&state=' + states + '&ORGID=' + orgID + '&TITLEID=' + titleID + '&PROGRAM_ID=' + programID + '&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            var desiredHeight = $(window).height() - 50;
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), desiredHeight);

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

        function OpenInsertWindow() {
            var orgID = $get("<%=hidOrg.ClientID %>").value;
            var titleID = $get("<%=hidTitle.ClientID %>").value;
            var programID = $get("<%=hidProgramID.ClientID %>").value;
            var oWindow = radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&state=New&ORGID=' +
            orgID + '&TITLEID=' + titleID + '&PROGRAM_ID=' + programID + '&noscroll=1&reload=1&FormType=0', "rwPopup");
            var pos = $("html").offset();
            var desiredHeight = $(window).height() - 50;
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), desiredHeight);
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%# rgCandidateList.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow("Edit");
                    // OpenEditWindow("Normal");
                    args.set_cancel(true);
                }

            } else if (args.get_item().get_commandName() == 'DELETE') {
                var bCheck = $find('<%# rgCandidateList.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }

            } else if (args.get_item().get_commandName() == 'EXPORT' || args.get_item().get_commandName() == 'PRINT' || args.get_item().get_commandName() == 'UNLOCK' || args.get_item().get_commandName() == 'PREVIOUS' || args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            }
            else if (args.get_item().get_commandName() == "NEXT") {
                var orgID = $get("<%=hidOrg.ClientID %>").value;
                var titleID = $get("<%=hidTitle.ClientID %>").value;
                var programID = $get("<%=hidProgramID.ClientID %>").value;
                var oWindow = radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_ImportCV&group=Business&noscroll=1&ORGID=' + orgID + '&TITLEID=' + titleID + '&PROGRAMID=' + programID, "rwPopup");
                var pos = $("html").offset();
                oWindow.moveTo(pos.left, pos.top);
                oWindow.setSize($(window).width(), $(window).height());
                args.set_cancel(true);
            }
        }

        function btnTransferClick(sender, args) {
            var bCheck = $find('<%# rgCandidateList.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            var oWindow = radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_FindProgram&group=Business&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            var desiredHeight = $(window).height() - 50;
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), desiredHeight);
        }

        function btnPontentialClick(sender, args) {
            var oWindow = radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_FindCandidate&group=Business&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            var desiredHeight = $(window).height() - 50;
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), desiredHeight);
        }

        function btnDuDieuKienClick(sender, args) {
            var bCheck = $find('<%# rgCandidateList.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }
        }
        function btnKhongDuDieuKienClick(sender, args) {
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
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientClose(oWnd, args) {
            $find("<%= rgCandidateList.ClientID %>").get_masterTableView().rebind();
        }

        function postBack(url) {
            var ajaxManager = $find("<%# AjaxManagerId %>");
            ajaxManager.ajaxRequest(url); //Making ajax request with the argument
        }

    </script>
</tlk:RadScriptBlock>

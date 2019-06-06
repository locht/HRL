<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRemindConfig.ascx.vb"
    Inherits="Profile.ctrlRemindConfig" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane1" runat="server">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
         <asp:Panel ID="radProbation" runat="server" CssClass = "Pane"> 
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkProbation" runat="server" Text="<%$ Translate: Nhân viên hết hạn hợp đồng thử việc%>"
                        onclick="CheckChangeProbation(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtProbation" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>
                            (<%# Translate("ngày")%>)
                            <asp:CustomValidator ID="cvalProbation" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên hết hạn hợp đồng thử việc. %>"
                                ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên hết hạn hợp đồng thử việc. %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
         <asp:Panel ID="radCONTRACT" runat="server" CssClass = "Pane"> 
            <fieldset>
                        <legend>
                            <asp:CheckBox ID="chkCONTRACT" runat="server" Text="<%$ Translate: Nhân viên hết hạn hợp đồng %>"
                                onclick="CheckChangeContract(this)" />
                        </legend>
                        <table class="table-form">
                            <tr>
                                <td class="lb lbRemind">
                                    <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="rntxtCONTRACT" runat="server" SkinID="Number">
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    (<%# Translate("ngày")%>)
                                    <asp:CustomValidator ID="cvalCONTRACT" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên hết hạn hợp đồng. %>"
                                        ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên hết hạn hợp đồng. %>">
                                    </asp:CustomValidator>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
         </asp:Panel>
         <asp:Panel ID="radBIRTHDAY" runat="server"  CssClass = "Pane"> 
            <fieldset>
                        <legend>
                            <asp:CheckBox ID="chkBIRTHDAY" runat="server" Text="<%$ Translate: Nhân viên sắp đến sinh nhật %>"
                                onclick="CheckChangeBirthday(this)" />
                        </legend>
                        <table class="table-form">
                            <tr>
                                <td class="lb lbRemind">
                                    <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="rntxtBIRTHDAY" runat="server" SkinID="Number">
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    (<%# Translate("ngày")%>)
                                    <asp:CustomValidator ID="cvalBIRTHDAY" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên sắp đến sinh nhật. %>"
                                        ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên sắp đến sinh nhật. %>">
                                    </asp:CustomValidator>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
         </asp:Panel>
         <asp:Panel ID="radVISA" runat="server" CssClass = "Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkVisa" runat="server" Text="<%$ Translate: Nhân viên hết hạn Visa%>"
                        onclick="CheckChangeVisa(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtVISA" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>
                            (<%# Translate("ngày")%>)
                            <asp:CustomValidator ID="valnmVISA" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên hết hạn visa, hộ chiếu. %>"
                                ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên hết hạn visa, hộ chiếu. %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
         </asp:Panel>
         <asp:Panel ID="radWORKING" runat="server" CssClass = "Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkWorking" runat="server" Text="<%$ Translate: Nhân viên hết hạn tờ trình %>"
                        onclick="CheckChangeWorking(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtWORKING" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>
                            (<%# Translate("ngày")%>)
                        </td>
                    </tr>
                </table>
            </fieldset>
         </asp:Panel>
         <asp:Panel ID="radLABOR" runat="server" CssClass = "Pane">
         <fieldset>
                <legend>
                    <asp:CheckBox ID="chkLabor" runat="server" Text="<%$ Translate: Nhân viên hết hạn giấy phép lao động  %>"
                        onclick="CheckChangeLabor(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtLABOR" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>
                            (<%# Translate("ngày")%>)
                        </td>
                    </tr>
                </table>
            </fieldset>
         </asp:Panel>
         <asp:Panel ID="radCERTIFICATE" runat="server" CssClass = "Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkCertificate" runat="server" Text="<%$ Translate: Nhân viên hết hạn chứng chỉ lao động  %>"
                        onclick="CheckChangeCertificate(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtCERTIFICATE" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>
                            (<%# Translate("ngày")%>)
                        </td>
                    </tr>
                </table>
            </fieldset>
         </asp:Panel>
         <asp:Panel ID="radTERMINATE" runat="server" CssClass = "Pane"> 
            <fieldset>
                        <legend>
                            <asp:CheckBox ID="chkTerminate" runat="server" Text="<%$ Translate: Nhân viên nghỉ việc trong tháng %>"
                                onclick="CheckChangeTerminate(this)" />
                        </legend>
                        <table class="table-form">
                            <tr>
                                <td class="lb lbRemind">
                                    <%# Translate("Thời gian báo trước")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="rntxtTERMINATE" runat="server" SkinID="Number">
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    (<%# Translate("ngày")%>)
                                </td>
                            </tr>
                        </table>
                    </fieldset>
         </asp:Panel>
         <asp:Panel ID="radTERMINATEDEBT" runat="server" CssClass = "Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkTerminateDebt" runat="server" Text="<%$ Translate: Nhân viên nghỉ việc chưa bàn giao hoặc còn thiếu công nợ %>"
                        onclick="CheckChangeTerminateDebt(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtTERMINATEDEBT" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>
                            (<%# Translate("ngày")%>)
                        </td>
                    </tr>
                </table>
            </fieldset>
         </asp:Panel> 
         <asp:Panel ID="radNOPAPER" runat="server" CssClass = "Pane"> 
            <fieldset>
                        <legend>
                            <asp:CheckBox ID="chkNoPaper" runat="server" Text="<%$ Translate: Nhân viên chưa nộp đủ giấy tờ khi tiếp nhận %>" />
                        </legend>
                        <table class="table-form" style="visibility:hidden; ">
                            <tr>
                                <td class="lb lbRemind">
                                    <%# Translate("Thời gian báo sau")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="rntxtNOPAPER" runat="server" SkinID="Number">
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    (<%# Translate("ngày")%>)
                                </td>
                            </tr>
                        </table>
                    </fieldset>
         </asp:Panel>
         <asp:Panel ID="radApprove" runat="server" CssClass = "Pane"> 
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkApprove" runat="server" Text="<%$ Translate: Nhân viên đến hạn bổ nhiệm lại chức vụ%>"
                        onclick="CheckChangeApprove(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtApprove" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>
                            (<%# Translate("ngày")%>)
                            <asp:CustomValidator ID="cvalApprove" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên đến hạn bổ nhiệm lại chức vụ . %>"
                                ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên đến hạn bổ nhiệm lại chức vụ. %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radApproveHDLD" runat="server" CssClass = "Pane"> 
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkApproveHDLD" runat="server" Text="<%$ Translate: Nhân viên đến hạn ký lại HĐLĐ%>"
                        onclick="CheckChangeApproveHDLD(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtApproveHDLD" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>
                            (<%# Translate("ngày")%>)
                            <asp:CustomValidator ID="cvalApproveHDLD" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên đến hạn ký lại HĐLĐ . %>"
                                ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên đến hạn ký lại HĐLĐ. %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radApproveTHHD" runat="server" CssClass = "Pane"> 
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkApproveTHHD" runat="server" Text="<%$ Translate: Nhân viên hết hạn tạm hoãn HĐ. %>"
                        onclick="CheckChangeApproveTHHD(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtApproveTHHD" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>
                            (<%# Translate("ngày")%>)
                            <asp:CustomValidator ID="cvalApproveTHHD" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên hết hạn tạm hoãn HĐ. %>"
                                ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên hết hạn tạm hoãn HĐ. %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>       
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function CheckChangeContract(chk) {
            if (chk.checked) {
                $find("<%=rntxtCONTRACT.ClientID %>").enable();
                $find("<%=rntxtCONTRACT.ClientID %>").focus();
            } else {
                $find("<%=rntxtCONTRACT.ClientID %>").clear();
                $find("<%=rntxtCONTRACT.ClientID %>").disable();
            }
        }
        function CheckChangeBirthday(chk) {
            if (chk.checked) {
                $find("<%=rntxtBIRTHDAY.ClientID %>").enable();
                $find("<%=rntxtBIRTHDAY.ClientID %>").focus();
            } else {
                $find("<%=rntxtBIRTHDAY.ClientID %>").clear();
                $find("<%=rntxtBIRTHDAY.ClientID %>").disable();
            }
        }


        function CheckChangeVisa(chk) {
            if (chk.checked) {
                $find("<%=rntxtVISA.ClientID %>").enable();
                $find("<%=rntxtVISA.ClientID %>").focus();
            } else {
                $find("<%=rntxtVISA.ClientID %>").clear();
                $find("<%=rntxtVISA.ClientID %>").disable();
            }
        }
        function CheckChangeWorking(chk) {
            if (chk.checked) {
                $find("<%=rntxtWORKING.ClientID %>").enable();
                $find("<%=rntxtWORKING.ClientID %>").focus();
            } else {
                $find("<%=rntxtWORKING.ClientID %>").clear();
                $find("<%=rntxtWORKING.ClientID %>").disable();
            }
        }
        function CheckChangeTerminate(chk) {
            if (chk.checked) {
                $find("<%=rntxtTERMINATE.ClientID %>").enable();
                $find("<%=rntxtTERMINATE.ClientID %>").focus();
            } else {
                $find("<%=rntxtTERMINATE.ClientID %>").clear();
                $find("<%=rntxtTERMINATE.ClientID %>").disable();
            }
        }
        function CheckChangeTerminateDebt(chk) {
            if (chk.checked) {
                $find("<%=rntxtTERMINATEDEBT.ClientID %>").enable();
                $find("<%=rntxtTERMINATEDEBT.ClientID %>").focus();
            } else {
                $find("<%=rntxtTERMINATEDEBT.ClientID %>").clear();
                $find("<%=rntxtTERMINATEDEBT.ClientID %>").disable();
            }
        }

//        function CheckChangeNoPaper(chk) {
//            if (chk.checked) {
//                $find("<%=rntxtNOPAPER.ClientID %>").enable();
//                $find("<%=rntxtNOPAPER.ClientID %>").focus();
//            } else {
//                $find("<%=rntxtNOPAPER.ClientID %>").clear();
//                $find("<%=rntxtNOPAPER.ClientID %>").disable();
//            }
//        }

        function CheckChangeProbation(chk) {
            if (chk.checked) {
                $find("<%=rntxtProbation.ClientID %>").enable();
                $find("<%=rntxtProbation.ClientID %>").focus();
            } else {
                $find("<%=rntxtProbation.ClientID %>").clear();
                $find("<%=rntxtProbation.ClientID %>").disable();
            }
        }
        
        function CheckChangeCertificate(chk) {
            if (chk.checked) {
                $find("<%=rntxtCERTIFICATE.ClientID %>").enable();
                $find("<%=rntxtCERTIFICATE.ClientID %>").focus();
            } else {
                $find("<%=rntxtCERTIFICATE.ClientID %>").clear();
                $find("<%=rntxtCERTIFICATE.ClientID %>").disable();
            }
        }

        function CheckChangeLabor(chk) {
            if (chk.checked) {
                $find("<%=rntxtLABOR.ClientID %>").enable();
                $find("<%=rntxtLABOR.ClientID %>").focus();
            } else {
                $find("<%=rntxtLABOR.ClientID %>").clear();
                $find("<%=rntxtLABOR.ClientID %>").disable();
            }
        }

        function CheckChangeApprove(chk) {
            if (chk.checked) {
                $find("<%=rntxtApprove.ClientID %>").enable();
                $find("<%=rntxtApprove.ClientID %>").focus();
            } else {
                $find("<%=rntxtApprove.ClientID %>").clear();
                $find("<%=rntxtApprove.ClientID %>").disable();
            }
        }
        function CheckChangeApproveHDLD(chk) {
            if (chk.checked) {
                $find("<%=rntxtApproveHDLD.ClientID %>").enable();
                $find("<%=rntxtApproveHDLD.ClientID %>").focus();
            } else {
                $find("<%=rntxtApproveHDLD.ClientID %>").clear();
                $find("<%=rntxtApproveHDLD.ClientID %>").disable();
            }
        }

        function CheckChangeApproveTHHD(chk) {
            if (chk.checked) {
                $find("<%=rntxtApproveTHHD.ClientID %>").enable();
                $find("<%=rntxtApproveTHHD.ClientID %>").focus();
            } else {
                $find("<%=rntxtApproveTHHD.ClientID %>").clear();
                $find("<%=rntxtApproveTHHD.ClientID %>").disable();
            }
        }

    </script>
    <style>
        .Pane
        {
            width:33%;
            float:left
        }
    </style>
</tlk:RadCodeBlock>

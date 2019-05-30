<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_TerminateNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_TerminateNewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarTerminate" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidTitleID" runat="server" />
        <asp:HiddenField ID="hidOrgID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:HiddenField ID="hiSalbasic" runat="server" />
        <asp:HiddenField ID="hidWorkStatus" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin chung")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbEmployeeCode" Text ="Mã nhân viên" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" SkinID="Readonly" ReadOnly="true"
                        Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="Bạn phải nhập nhân viên." ToolTip="Bạn phải nhập nhân viên."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbEmployeeName" Text ="Họ tên" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbOrgName" Text ="Phòng ban" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>


            </tr>
            <%--<tr>
                
                <td class="lb">
                    <%# Translate("Cấp nhân sự")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtStaffRankName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>--%>
            <%--<td class="lb">
                    <%# Translate("Lương cơ bản")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSalBasic" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>--%>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbTitleName" Text ="Chức danh" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>

                <td class="lb">
                    <asp:Label runat ="server" ID ="lbJoinDateState" Text ="Ngày vào làm" ></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdJoinDateState" runat="server" Enabled="False" ReadOnly="true">
                    </tlk:RadDatePicker>
                </td>
                <%--
                <td class="lb">
                    <%# Translate("Chi phí hỗ trợ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostSupport" runat="server" ReadOnly="true" SkinID="ReadOnly">
                    </tlk:RadNumericTextBox>
                </td>--%>

                <td class="lb">
                    <asp:Label runat ="server" ID ="Label1" Text ="Số hợp đồng" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContractNo" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>

            </tr>
            <tr>

                <td class="lb">
                    <asp:Label runat ="server" ID ="Label2" Text ="Ngày hiệu lực" ></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdContractEffectDate" runat="server" DateInput-Enabled="False">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="Label3" Text ="Ngày hết hiệu lực" ></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdContractExpireDate" runat="server" DateInput-Enabled="False">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="Label4" Text ="Tập tin đính kèm"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr style="visibility:hidden">
                <td class="lb">
                    <tlk:RadTextBox ID="txtRemindLink" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin nghỉ việc")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbSendDate" Text ="Ngày nộp đơn" ></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSendDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CustomValidator ID="cvaldpSendDate" runat="server" ErrorMessage="Ngày nộp đơn không nhỏ hơn ngày vào công ty."
                        ToolTip="Ngày nộp đơn không nhỏ hơn ngày vào công ty.">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbEffectDate" Text ="Ngày thôi việc" ></asp:Label>                    
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <%--
                <td>
                    <tlk:RadDatePicker ID="rdApprovalDate" runat="server" AutoPostBack="true" DateInput-CausesValidation="false">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdApprovalDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày được phê duyệt nghỉ. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập ngày được phê duyệt nghỉ %>"> </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvaldpApproveDate" runat="server" ErrorMessage="<%$ Translate: Ngày được phê duyệt nghỉ phải lớn hơn ngày nộp đơn. %>"
                        ToolTip="<%$ Translate: Ngày được phê duyệt nghỉ phải lớn hơn ngày nộp đơn. %>">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvaldpApproveDateJoinDate" runat="server" ErrorMessage="<%$ Translate: Ngày phê duyệt không nhỏ hơn ngày vào công ty. %>"
                        ToolTip="<%$ Translate: Ngày phê duyệt không nhỏ hơn ngày vào công ty. %>">
                    </asp:CustomValidator>
                </td>--%>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbLastDate" Text ="Ngày nghỉ thực tế" ></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdLastDate" runat="server" AutoPostBack="true" DateInput-CausesValidation="false">
                    </tlk:RadDatePicker>
                    <%--<asp:RequiredFieldValidator ID="reqLastDate" ControlToValidate="rdLastDate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày nghỉ thực tế. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày nghỉ thực tế. %>"> </asp:RequiredFieldValidator>
                    --%>
                    <asp:CustomValidator ID="cval_LastDate" runat="server" ControlToValidate="rdLastDate" ErrorMessage="Bạn phải nhập ngày nghỉ thực tế."
                        ToolTip="Bạn phải nhập ngày nghỉ thực tế.">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cval_LastDate_SendDate" runat="server" ErrorMessage="Ngày nghỉ thực tế phải lớn hơn ngày nộp đơn."
                        ToolTip="Ngày nghỉ thực tế phải lớn hơn ngày nộp đơn.">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cval_LastDate_JoinDate" runat="server" ErrorMessage="Ngày nghỉ thực tế không nhỏ hơn ngày vào công ty."
                        ToolTip="Ngày nghỉ thực tế không nhỏ hơn ngày vào công ty.">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbSeniority" Text ="Thâm niên công tác" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSeniority" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>

                <td class="lb"></td>
                <td>
                    <asp:CheckBox ID="cbIsNoHire" runat="server" Text="Danh sách đen" />
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbTYPE_TERMINATE" Text ="Loại nghỉ" ></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTYPE_TERMINATE" runat="server" ReadOnly="False" SkinID="ReadOnly">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="5">
                    <tlk:RadGrid PageSize="50" ID="rgReason" runat="server" Height="200px" Width="550px" SkinID="GridNotPaging"
                        AllowMultiRowEdit="true">
                        <MasterTableView DataKeyNames="ID,TER_REASON_ID,TER_REASON_NAME,DENSITY" ClientDataKeyNames="ID,TER_REASON_ID,TER_REASON_NAME,DENSITY"
                            EditMode="InPlace">
                            <Columns>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Ly do nghỉ %>" DataField="TER_REASON_NAME"
                                    SortExpression="TER_REASON_NAME" UniqueName="TER_REASON_NAME" ReadOnly="true" />
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Tỷ trọng (%) %>" DataField="DENSITY"
                                    SortExpression="DENSITY" UniqueName="DENSITY" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                            </Columns>
                        </MasterTableView>
                    </tlk:RadGrid>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbTerReasonDetail" Text ="Lý do chi tiết" ></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtTerReasonDetail" runat="server" SkinID="TextBox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbInsStatus" Text ="Tình trạng sổ BHXH" ></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboInsStatus" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbRemark" Text ="Ghi chú" ></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="TextBox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Danh mục tài sản bàn giao")%></b>
                    <hr />
                </td>
            </tr>
            <%-- <tr>
                <td>
                </td>
                <td colspan="5">
                    <asp:CheckBox ID="chkIdenitifiCard" runat="server" /><%# Translate("Thẻ gửi xe, thẻ nhân viên")%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày trả")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdIdentifiDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Tình trạng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtIdentifiStatus" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số tiền")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtIdentifiMoney" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="5">
                    <asp:CheckBox ID="chkSunCard" runat="server" Text="Thẻ Sun Group, thẻ Sun Care, huy hiệu" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày trả")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSunDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Tình trạng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSunStatus" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số tiền")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSunMoney" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="5">
                    <asp:CheckBox ID="chkInsuranceCard" runat="server" Text="Thẻ BHYT" AutoPostBack="true"
                        CausesValidation="false" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày trả")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdInsuranceDate" AutoPostBack="true" CausesValidation="false"
                        runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Tình trạng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtInsuranceStatus" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số tiền")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtInsuranceMoney" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>--%>
            <%-- <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Các tài sản, Bảo hộ lao động (đồng phục) đã giao cho người lao động")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <tlk:RadGrid PageSize=50 ID="rgLabourProtect" runat="server" Height="200px" Width="900px" SkinID="GridNotPaging"
                        AllowMultiRowEdit="true">
                        <MasterTableView EditMode="InPlace" Caption="Bảo hộ lao động">
                            <Columns>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên bảo hộ %>" DataField="LABOURPROTECTION_NAME"
                                    SortExpression="LABOURPROTECTION_NAME" UniqueName="LABOURPROTECTION_NAME" ReadOnly="true" />
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lượng %>" DataField="QUANTITY"
                                    SortExpression="QUANTITY" UniqueName="QUANTITY" DataFormatString="{0:n0}" ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="UNIT_PRICE"
                                    SortExpression="UNIT_PRICE" UniqueName="UNIT_PRICE" DataFormatString="{0:n0}"
                                    ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày cấp phát %>" DataField="DAYS_ALLOCATED"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="DAYS_ALLOCATED" UniqueName="DAYS_ALLOCATED"
                                    ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}" />
                                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Thu hồi %>" DataField="RETRIEVED"
                                    SortExpression="RETRIEVED" UniqueName="RETRIEVED" HeaderStyle-Width="100px">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </tlk:GridCheckBoxColumn>
                            </Columns>
                            <HeaderStyle Width="100px" />
                        </MasterTableView>
                    </tlk:RadGrid>
                </td>
            </tr>--%>
            <tr>
                <td colspan="6">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbAssetCode" Text ="Tài sản" ></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="txtAssetCode" runat="server"  EnableLoadOnDemand="true"  OnItemsRequested="txtAssetCode_ItemsRequested">
                    </tlk:RadComboBox>

                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbAssetAmount" Text ="Giá trị tài sản" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="AssetAmount" runat="server" SkinID="Money" TabIndex="6">
                    </tlk:RadNumericTextBox>
                </td>
                 <td class="lb">
                    <asp:Label runat ="server" ID ="lbAssetQuantity" Text ="Số lượng" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="AssetQuantity" runat="server" SkinID="Money" TabIndex="6">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>               
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbAssetStatus" Text ="Tình trạng" ></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="AssetStatus" runat="server" TabIndex="6"></tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbAssetNote" Text ="Ghi chú" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="AssetNote" runat="server" TabIndex="6">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <tlk:RadGrid PageSize="50" ID="rgAsset" runat="server" Height="250px" Width="100%" SkinID="GridNotPaging"
                        AllowMultiRowEdit="true">
                        <MasterTableView EditMode="PopUp" DataKeyNames="ASSET_ID,ASSET_DECLARE_ID,EMPLOYEE_ID,ID,ASSET_NAME,EMPLOYEE_CODE,ASSET_VALUE,QUANTITY,STATUS_NAME,STATUS_ID,REMARK" CommandItemDisplay="Top" AllowAutomaticInserts="true">
                            <CommandItemStyle Height="28px" />
                            <CommandItemTemplate>
                                <div style="padding: 2px 0 0 0">
                                    <div style="float: left">
                                        <tlk:RadButton ID="btnAddAsset" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/add.png" 
                                            CommandName="btnAddAsset"
                                            CausesValidation="false" Width="70px" Text="<%$ Translate: Thêm %>" OnClientClicking="btnAddAssetsOnClientClicking">
                                        </tlk:RadButton>
                                    </div>
                                    <div style="float: right">
                                        <tlk:RadButton ID="btnDeleteAssets" runat="server"
                                            CommandName="btnDeleteAssets"
                                            Icon-PrimaryIconUrl="~/Static/Images/Toolbar/delete.png" 
                                            CausesValidation="false" Width="70px" Text="<%$ Translate: Xóa %>" OnClientClicking="btnDeleteAssetsOnClientClicking">
                                        </tlk:RadButton>
                                    </div>

                                </div>
                            </CommandItemTemplate>

                            <Columns>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tài sản %>" DataField="ASSET_NAME"
                                    SortExpression="ASSET_NAME" UniqueName="ASSET_NAME" ReadOnly="true" />
                                <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Mã vạch %>" DataField="ASSET_BARCODE"
                                    SortExpression="ASSET_BARCODE" UniqueName="ASSET_BARCODE" ReadOnly="true" />--%>
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Giá trị tài sản %>" DataField="ASSET_VALUE"
                                    SortExpression="ASSET_VALUE" UniqueName="ASSET_VALUE" DataFormatString="{0:n0}"
                                    ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày cấp phát %>" DataField="ISSUE_DATE"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="ISSUE_DATE" UniqueName="ISSUE_DATE"
                                    ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày thu hồi %>" DataField="RETURN_DATE"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="RETURN_DATE" UniqueName="RETURN_DATE"
                                    ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}" />--%>
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lượng %>" DataField="QUANTITY"
                                    SortExpression="QUANTITY" UniqueName="QUANTITY" DataFormatString="{0:n0}" ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tình trạng %>" DataField="STATUS_NAME"
                                    SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" ReadOnly="true" />

                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK"
                                    SortExpression="REMARK" UniqueName="REMARK" ReadOnly="true" />
                            </Columns>
                            <HeaderStyle Width="100px" />
                        </MasterTableView>
                    </tlk:RadGrid>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin thanh lý hợp đồng")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbRemainingLeave" Text ="Số phép còn lại" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtRemainingLeave" runat="server" SkinID="ReadonlyDecimal"
                        ReadOnly="true" NumberFormat-DecimalDigits="1">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbCompensatoryLeave" Text ="Số ngày nghỉ bù còn lại" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCompensatoryLeave" runat="server" ReadOnly="true"
                        SkinID="ReadonlyDecimal" NumberFormat-DecimalDigits="2">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbPaymentLeave" Text ="Tiền thanh toán phép"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtPaymentLeave" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbSalaryMedium_loss" Text ="Lương trung bình tính 6 tháng" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSalaryMedium_loss" runat="server" MinValue="0">
                        <NumberFormat AllowRounding="false" DecimalDigits="0" DecimalSeparator="." GroupSeparator=","
                            GroupSizes="3" />
                    </tlk:RadNumericTextBox>
                </td>

                <td class="lb">
                    <asp:Label runat ="server" ID ="lbTimeAccidentIns_loss" Text ="Thời gian tham gia BHTN" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTimeAccidentIns_loss" runat="server">
                    </tlk:RadTextBox>
                </td>

                <td class="lb">
                    <asp:Label runat ="server" ID ="lbyearforallow_loss" Text ="Số năm tính trợ cấp mất việc" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtyearforallow_loss" MinValue="0" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                    <%-- <%--<NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                                    <ClientEvents OnBlur="displayDecimalFormat" OnLoad="displayDecimalFormat" OnValueChanged="displayDecimalFormat" />
                    </tlk:RadNumericTextBox>--%>
                </td>

            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbMoneyReturn" Text ="Số tiền còn lại" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtMoneyReturn" MinValue="0" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbAmountViolations" Text ="Phạt thời hạn báo trước" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAmountViolations" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbAmountWrongful" Text ="Phạt chấm dứt trái luật" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAmountWrongful" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbCompensatoryPayment" Text ="Tiền thanh toán nghỉ bù" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCompensatoryPayment" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbAllowanceTerminate" Text ="Trợ cấp thôi việc" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAllowanceTerminate" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbTrainingCosts" Text ="Chi phí đào tạo" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtTrainingCosts" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbOtherCompensation" Text ="Bồi thường khác" ></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtOtherCompensation" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Phê duyệt")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbStatus" Text ="Trạng thái" ></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusStatus" runat="server" ErrorMessage="Bạn phải nhập Trạng tháỉ"
                        ToolTip="Bạn phải nhập Trạng tháỉ" ClientValidationFunction="cusStatus">
                    </asp:CustomValidator>
                   <%-- <asp:CustomValidator ID="cvalStatus" ControlToValidate="cboStatus" runat="server" ErrorMessage="<%$ Translate: Trạng thái không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Trạng thái không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>--%>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbDecisionNo" Text ="Số quyết định" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionNo" runat="server">
                    </tlk:RadTextBox>
                </td>
               <%-- <td class="lb">
                    <%# Translate("Ngày thôi việc")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>--%>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbSignDate" Text ="Ngày ký" ></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSignDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbSignerName" Text ="Người ký" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignerName" runat="server" ReadOnly="true" Width="130px" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindSinger" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbSignerTitle" Text ="Chức danh" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignerTitle" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusStatus(oSrc, args) {
            var cbo = $find("<%# cboStatus.ClientID%>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }

        function clientButtonClicking(sender, args) {
            //if (args.get_item().get_commandName() == 'CANCEL') {
            //    getRadWindow().close(null);
            //    args.set_cancel(true);
            //}
        }

        function btnDeleteReasonClick(sender, args) {
            var bCheck = $find('<%# rgReason.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                args.set_cancel(true);
            }
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function btnDeleteAssetsOnClientClicking(sender, args) {

        }
        function btnAddAssetsOnClientClicking(sender, args) {
            var grid = $find("<%# rgAsset.ClientID%>");
            // grid.MasterTableView.isItemInserted = true;
            // grid.MasterTableView.showInsertItem();
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>

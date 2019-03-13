<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsTotalSalary.ascx.vb"
    Inherits="Insurance.ctrlInsTotalSalary" %>
<%@ Register Src="~/Modules/Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox"
    TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Width="100%" Height="100%">
        <tlk:RadToolBar ID="tbarOtherLists" runat="server" Visible="true" />
        <asp:HiddenField ID="hidID" runat="server" />
        <div style="display: none;">
            <tlk:RadTextBox ID="txtID" Text="0" runat="server">
            </tlk:RadTextBox>
        </div>
        <div style="padding-top: 10px; float: left">
            <table>
                <tr>
                    <td class="lb">
                        <%# Translate("Đơn vị bảo hiểm")%>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="ddlINS_ORG_ID" runat="server" AutoPostBack="true" Width="250px">
                        </tlk:RadComboBox>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divCal" runat="server" visible="true">
            <table class="table-form">
                <tr>
                    <td class="lb">
                        <%# Translate("Năm")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox MinValue="1900" MaxValue="9999" ID="txtYear" Width="50px"
                            runat="server" AutoPostBack="true">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                        <%# Translate("Tháng")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox MinValue="1" MaxValue="12" ID="txtMonth" Width="50px" runat="server"
                            AutoPostBack="true">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb" style="visibility: hidden">
                        <%# Translate("Đợt khai báo")%>
                    </td>
                    <td style="visibility: hidden">
                        <tlk:RadComboBox ID="ddlPeriod" runat="server" AutoPostBack="true">
                        </tlk:RadComboBox>
                        <%-- <tlk:RadDatePicker  DateInput-DateFormat="MM/yyyy"  runat="server" ID="txtPeriod" TabIndex="5" >
								</tlk:RadDatePicker>--%>
                    </td>
                    <td>
                        <tlk:RadButton runat="server" ID="btnCAL" Visible="false" Text="<%$ Translate: Tổng hợp %>"
                            CausesValidation="false" />
                        <tlk:RadButton runat="server" ID="btnExport" Visible="false" Text="<%$ Translate: Xuất dữ liệu %>"
                            CausesValidation="false" />
                        <tlk:RadButton runat="server" ID="btnLock" Visible="false" Text="<%$ Translate: Khóa %>"
                            CausesValidation="false" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divCalBatch" runat="server" visible="false">
            <table class="table-form">
                <tr>
                    <td class="lb">
                        <%# Translate("Từ ngày")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                    </td>
                    <td>
                        <tlk:RadDatePicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ID="txtFROMDATE"
                            TabIndex="5">
                        </tlk:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtFROMDATE"
                            runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn từ ngày. %>"
                            ToolTip="<%$ Translate: Bạn phải chọn từ ngày. %>"></asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <%# Translate("Đến ngày")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                    </td>
                    <td>
                        <tlk:RadDatePicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ID="txtTODATE"
                            TabIndex="5">
                        </tlk:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtTODATE"
                            runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập đến ngày. %>"
                            ToolTip="<%$ Translate: Bạn phải nhập đến ngày. %>"></asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        &nbsp;
                    </td>
                    <td>
                        <tlk:RadButton ID="btnCalBatch" AutoPostBack="true" runat="server" Text="<%$ Translate: Tổng hợp hàng loạt %>"
                            CausesValidation="false" />
                    </td>
                </tr>
            </table>
        </div>
        <tlk:RadGrid ID="rgGridData" runat="server" Height="78%" AllowPaging="True" AllowSorting="True"
            CellSpacing="0" ShowStatusBar="true" GridLines="None"
            PageSize="500" AutoGenerateColumns="false" AllowFilteringByColumn="true">
            <ClientSettings>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
            <MasterTableView CommandItemDisplay="None" DataKeyNames="ID" ClientDataKeyNames="ID,EMPLOYEE_CODE,FULL_NAME,TITLE,SI_EMP,SI_COM,HI_EMP,HI_COM,UI_EMP,UI_COM,OLD_SAL,NEW_SAL,PERIOD,SI_ADJUST,HI_ADJUST,UI_ADJUST,HI_ADD,UI_ADD,RATE_SI_COM,RATE_SI_EMP,RATE_HI_COM,RATE_HI_EMP,RATE_UI_COM,RATE_UI_EMP">
                <GroupByExpressions>
                    <tlk:GridGroupByExpression>
                        <SelectFields>
                            <tlk:GridGroupByField FieldName="ARISINGGROUP" HeaderText="Nhóm Biến động"></tlk:GridGroupByField>
                        </SelectFields>
                        <GroupByFields>
                            <tlk:GridGroupByField FieldName="ARISINGGROUP"></tlk:GridGroupByField>
                        </GroupByFields>
                    </tlk:GridGroupByExpression>
                </GroupByExpressions>
                <Columns>
                    <tlk:GridBoundColumn DataField="ID" UniqueName="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: MSNV %>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" CurrentFilterFunction="Contains"
                        HeaderStyle-Width="80px" FilterControlWidth="60%" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="FULL_NAME"
                        CurrentFilterFunction="Contains" HeaderStyle-Width="150px" FilterControlWidth="60%"
                        UniqueName="FULL_NAME" SortExpression="FULL_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE" UniqueName="TITLE"
                        CurrentFilterFunction="Contains" HeaderStyle-Width="170px" FilterControlWidth="60%"
                        SortExpression="TITLE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: BHXH công ty %>" DataFormatString="{0:N0}"
                        HeaderStyle-Width="110px" DataField="SI_COM" UniqueName="SI_COM" SortExpression="SI_COM" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: BHYT công ty %>" DataFormatString="{0:N0}"
                        HeaderStyle-Width="110px" DataField="HI_COM" UniqueName="HI_COM" SortExpression="HI_COM" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: BHTN công ty %>" DataFormatString="{0:N0}"
                        HeaderStyle-Width="110px" DataField="UI_COM" UniqueName="UI_COM" SortExpression="UI_COM" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: BHXH nhân viên %>" DataFormatString="{0:N0}"
                        HeaderStyle-Width="110px" DataField="SI_EMP" UniqueName="SI_EMP" SortExpression="SI_EMP" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: BHYT nhân viên %>" DataFormatString="{0:N0}"
                        HeaderStyle-Width="110px" DataField="HI_EMP" UniqueName="HI_EMP" SortExpression="HI_EMP" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: BHTN nhân viên %>" DataFormatString="{0:N0}"
                        HeaderStyle-Width="110px" DataField="UI_EMP" UniqueName="UI_EMP" SortExpression="UI_EMP" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức lương cũ %>" DataFormatString="{0:N0}"
                        HeaderStyle-Width="110px" DataField="OLD_SAL" UniqueName="OLD_SAL" SortExpression="OLD_SAL" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức lương mới %>" DataFormatString="{0:N0}"
                        HeaderStyle-Width="110px" DataField="NEW_SAL" UniqueName="NEW_SAL" SortExpression="NEW_SAL" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đợt khai báo %>" DataFormatString="{0:dd/MM/yyyy}"
                        HeaderStyle-Width="110px" DataField="PERIOD" UniqueName="PERIOD" SortExpression="PERIOD" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điều chỉnh BHXH NV  %>" DataFormatString="{0:N0}"
                        HeaderStyle-Width="110px" DataField="SI_ADJUST" UniqueName="SI_ADJUST" SortExpression="SI_ADJUST" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điều chỉnh BHYT NV %>" DataFormatString="{0:N0}"
                        HeaderStyle-Width="110px" DataField="HI_ADJUST" UniqueName="HI_ADJUST" SortExpression="HI_ADJUST" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điều chỉnh BHTN NV %>" DataFormatString="{0:N0}"
                        HeaderStyle-Width="110px" DataField="UI_ADJUST" UniqueName="UI_ADJUST" SortExpression="UI_ADJUST" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điều chỉnh BHXH CTY  %>" DataFormatString="{0:N0}"
                        HeaderStyle-Width="110px" DataField="SI_ADJUST_COM" UniqueName="SI_ADJUST_COM" SortExpression="SI_ADJUST_COM" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điều chỉnh BHYT CTY %>" DataFormatString="{0:N0}"
                        HeaderStyle-Width="110px" DataField="HI_ADJUST_COM" UniqueName="HI_ADJUST_COM" SortExpression="HI_ADJUST_COM" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điều chỉnh BHTN CTY %>" DataFormatString="{0:N0}"
                        HeaderStyle-Width="110px" DataField="UI_ADJUST_COM" UniqueName="UI_ADJUST_COM" SortExpression="UI_ADJUST_COM" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Bổ sung BHYT  %>" DataFormatString="{0:N0}"
                        HeaderStyle-Width="110px" DataField="HI_ADD" UniqueName="HI_ADD" SortExpression="HI_ADD" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Bổ sung BHTN %>" DataFormatString="{0:N0}"
                        HeaderStyle-Width="110px" DataField="UI_ADD" UniqueName="UI_ADD" SortExpression="UI_ADD" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: %BHXH công ty %>" DataFormatString="{0:#,##0.#}%"
                        HeaderStyle-Width="110px" DataField="RATE_SI_COM" UniqueName="RATE_SI_COM" SortExpression="RATE_SI_COM" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: %BHYT công ty %>" DataFormatString="{0:#,##0.#}%"
                        HeaderStyle-Width="110px" DataField="RATE_HI_COM" UniqueName="RATE_HI_COM" SortExpression="RATE_HI_COM" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: %BHTN công ty %>" DataFormatString="{0:#,##0.#}%"
                        HeaderStyle-Width="110px" DataField="RATE_UI_COM" UniqueName="RATE_UI_COM" SortExpression="RATE_UI_COM" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: %BHXH nhân viên %>" DataFormatString="{0:#,##0.#}%"
                        HeaderStyle-Width="120px" DataField="RATE_SI_EMP" UniqueName="RATE_SI_EMP" SortExpression="RATE_SI_EMP" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: %BHYT nhân viên %>" DataFormatString="{0:#,##0.#}%"
                        HeaderStyle-Width="120px" DataField="RATE_HI_EMP" UniqueName="RATE_HI_EMP" SortExpression="RATE_HI_EMP" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: %BHTN nhân viên %>" DataFormatString="{0:#,##0.#}%"
                        HeaderStyle-Width="120px" DataField="RATE_UI_EMP" UniqueName="RATE_UI_EMP" SortExpression="RATE_UI_EMP" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="200px" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="TopPanel" runat="server" MinHeight="30" Height="30px" Scrolling="None">
                <tlk:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage1" CausesValidation="false">
                    <Tabs>
                        <tlk:RadTab runat="server" PageViewID="rpvInfor1" Text="<%$ Translate: Thông tin tổng hợp trong kỳ %>"
                            Selected="True">
                        </tlk:RadTab>
                        <tlk:RadTab runat="server" PageViewID="rpvInfor2" Text="<%$ Translate: Thông tin tổng hợp cuối kỳ %>">
                        </tlk:RadTab>
                    </Tabs>
                </tlk:RadTabStrip>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server">
                <tlk:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Width="100%">
                    <tlk:RadPageView ID="rpvInfor1" runat="server">
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                </td>
                                <td colspan="2" align="center">
                                    <b>
                                        <%# Translate("BẢO HIỂM XÃ HỘI")%></b>
                                </td>
                                <td colspan="2" align="center">
                                    <b>
                                        <%# Translate("BẢO HIỂM Y TẾ")%></b>
                                </td>
                                <td colspan="2" align="center">
                                    <b>
                                        <%# Translate("BẢO HIỂM THẤT NGHIỆP")%></b>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                </td>
                                <td align="center">
                                    <%# Translate("Tăng")%>
                                </td>
                                <td align="center">
                                    <%# Translate("Giảm")%>
                                </td>
                                <td align="center">
                                    <%# Translate("Tăng")%>
                                </td>
                                <td align="center">
                                    <%# Translate("Giảm")%>
                                </td>
                                <td align="center">
                                    <%# Translate("Tăng")%>
                                </td>
                                <td align="center">
                                    <%# Translate("Giảm")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("1. Số lao động")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtHC_1_SI_T" MinValue="0" Value="0" ReadOnly="true" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td class="lb">
                                    <tlk:RadNumericTextBox ID="txtHC_1_SI_G" MinValue="0" Value="0" ReadOnly="true" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtHC_1_HI_T" MinValue="0" Value="0" ReadOnly="true" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtHC_1_HI_G" MinValue="0" Value="0" ReadOnly="true" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtHC_1_UI_T" MinValue="0" Value="0" ReadOnly="true" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtHC_1_UI_G" MinValue="0" Value="0" ReadOnly="true" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("2. Tổng quỹ lương")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtTotalSal_1_SI_T" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td class="lb">
                                    <tlk:RadNumericTextBox ID="txtTotalSal_1_SI_G" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtTotalSal_1_HI_T" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtTotalSal_1_HI_G" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtTotalSal_1_UI_T" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtTotalSal_1_UI_G" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("3. Số phải đóng")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtSumit_1_SI_T" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td class="lb">
                                    <tlk:RadNumericTextBox ID="txtSumit_1_SI_G" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtSumit_1_HI_T" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtSumit_1_HI_G" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtSumit_1_UI_T" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtSumit_1_UI_G" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("4. Điều chỉnh")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtAdjust_1_SI_T" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td class="lb">
                                    <tlk:RadNumericTextBox ID="txtAdjust_1_SI_G" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtAdjust_1_HI_T" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtAdjust_1_HI_G" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtAdjust_1_UI_T" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtAdjust_1_UI_G" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </tlk:RadPageView>
                    <tlk:RadPageView ID="rpvInfor2" runat="server" Width="100%">
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                </td>
                                <td colspan="2" align="center">
                                    <b>
                                        <%# Translate("BẢO HIỂM XÃ HỘI")%></b>
                                </td>
                                <td colspan="2" align="center">
                                    <b>
                                        <%# Translate("BẢO HIỂM Y TẾ")%></b>
                                </td>
                                <td colspan="2" align="center">
                                    <b>
                                        <%# Translate("BẢO HIỂM THẤT NGHIỆP")%></b>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                </td>
                                <td align="center">
                                    <%# Translate("Kỳ trước")%>
                                </td>
                                <td align="center">
                                    <%# Translate("Kỳ này")%>
                                </td>
                                <td align="center">
                                    <%# Translate("Kỳ trước")%>
                                </td>
                                <td align="center">
                                    <%# Translate("Kỳ này")%>
                                </td>
                                <td align="center">
                                    <%# Translate("Kỳ trước")%>
                                </td>
                                <td align="center">
                                    <%# Translate("Kỳ này")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("1. Số lao động")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtHC_2_SI_T" MinValue="0" Value="0" ReadOnly="true" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td class="lb">
                                    <tlk:RadNumericTextBox ID="txtHC_2_SI_G" MinValue="0" Value="0" ReadOnly="true" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtHC_2_HI_T" MinValue="0" Value="0" ReadOnly="true" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtHC_2_HI_G" MinValue="0" Value="0" ReadOnly="true" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtHC_2_UI_T" MinValue="0" Value="0" ReadOnly="true" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtHC_2_UI_G" MinValue="0" Value="0" ReadOnly="true" runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("2. Tổng quỹ lương")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtTotalSal_2_SI_T" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td class="lb">
                                    <tlk:RadNumericTextBox ID="txtTotalSal_2_SI_G" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtTotalSal_2_HI_T" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtTotalSal_2_HI_G" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtTotalSal_2_UI_T" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtTotalSal_2_UI_G" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("3. Số phải đóng")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtSumit_2_SI_T" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td class="lb">
                                    <tlk:RadNumericTextBox ID="txtSumit_2_SI_G" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtSumit_2_HI_T" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtSumit_2_HI_G" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtSumit_2_UI_T" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtSumit_2_UI_G" MinValue="0" Value="0" ReadOnly="true"
                                        runat="server">
                                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <%--<tr>
                                                    <td class="lb">
                                                        <%# Translate("4. Điều chỉnh")%>
                                                    </td>
                                                    <td>
                                                        <tlk:RadNumericTextBox ID="txtAdjust_2_SI_T"  runat="server"></tlk:RadNumericTextBox>                          
                                                    </td>
                                                    <td class="lb">
                                                        <tlk:RadNumericTextBox ID="txtAdjust_2_SI_G"  runat="server"></tlk:RadNumericTextBox> 
                                                    </td>
                                                    <td>
                                                        <tlk:RadNumericTextBox ID="txtAdjust_2_HI_T"  runat="server"></tlk:RadNumericTextBox>                          
                                                    </td>
                                                    <td>
                                                        <tlk:RadNumericTextBox ID="txtAdjust_2_HI_G"  runat="server"></tlk:RadNumericTextBox>                          
                                                    </td>
                                                    <td>
                                                        <tlk:RadNumericTextBox ID="txtAdjust_2_UI_T"  runat="server"></tlk:RadNumericTextBox>                          
                                                    </td>
                                                    <td>
                                                        <tlk:RadNumericTextBox ID="txtAdjust_2_UI_G"  runat="server"></tlk:RadNumericTextBox>                          
                                                    </td>
                                                </tr>--%>
                        </table>
                    </tlk:RadPageView>
                </tlk:RadMultiPage>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            //            if (item.get_commandName() == "CREATE") {
            //                OpenCalInsBatch(sender, args);
            //                args.set_cancel(true);
            //                enableAjax = false;
            //            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        //        function popupclose(oWnd, args) {
        //            //window.location.reload();
        //        }


        //        function OnClientBeforeClose(sender, eventArgs) {
        //            if (!confirm("Bạn có muốn đóng màn hình không?")) {
        //                //if cancel is clicked prevent the window from closing
        //                args.set_cancel(true);
        //            }
        //        }

        //        function OnClientClose(oWnd, args) {
        //            oWnd = $find('<%#popupId %>');
        //            oWnd.setSize(screen.width - 250, screen.height - 300);
        //            oWnd.remove_close(OnClientClose);
        //            var arg = args.get_argument();
        //            if (arg) {
        //                postBack(arg);
        //            }
        //        }

        //        function OpenCalInsBatch(sender, eventArgs) {
        //            var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsTotalSalaryBatch&group=Business', "rwPopup");
        //            var pos = $("html").offset();
        //            //oWindow.setSize($(window).width(), $(window).height());
        //            oWindow.moveTo(pos.left, pos.top);
        //            oWindow.setSize(550, 250);
        //            return 0;
        //        }
		
    </script>
</tlk:RadCodeBlock>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalTrainingOutCompany_Edit.ascx.vb"
    Inherits="Profile.ctrlPortalTrainingOutCompany_Edit" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidProcessTrainID" runat="server" />
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<div style="overflow: auto">
    <table class="table-form">
        <tr>
                <td class="lb" style="width: 130px">
                    <asp:Label runat="server" ID="lbTuNgay" Text="Từ ngày"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>                    
                     <tlk:RadDatePicker ID="rdTuNgay" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="rqTuNgay" ControlToValidate="rdTuNgay" runat="server"
                        ErrorMessage="Bạn phải nhập từ ngày." ToolTip="Bạn phải nhập từ ngày."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 200px">
                    <asp:Label runat="server" ID="lbToiNgay" Text="Đến ngày"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                <tlk:RadDatePicker ID="rdToiNgay" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CustomValidator ID="cvalToiNgay" runat="server" ErrorMessage="Từ ngày không được lớn hơn tới ngày."
                        ToolTip="Từ ngày không được lớn hơn tới ngày.">
                    </asp:CustomValidator>
                    <%--<asp:RequiredFieldValidator ID="rqToiThang" ControlToValidate="rdToiThang" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tới tháng. %>" ToolTip="<%$ Translate: Bạn phải nhập tới tháng. %>"> </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
        <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTrainingSchool" Text="Tên trường"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTrainingSchool" runat="server" Width="160px" CausesValidation="false">
                     </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboTrainingSchool"
                        runat="server" ErrorMessage="Bạn phải nhập tên trường." ToolTip="Bạn phải nhập tên trường."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbChuyenNganh" Text="Ngành học"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboChuyenNganh" runat="server" Width="160px" CausesValidation="false">
                     </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label1" Text="Trình độ"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboLevelId" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbBangCap" Text="Bằng cấp/Chứng chỉ"></asp:Label>
                </td>
                <td>
                     <tlk:RadComboBox ID="cboRemark" runat="server" Width="160px" CausesValidation="false">
                     </tlk:RadComboBox>
                </td>
               <%-- <td class="lb">
                    <asp:Label runat="server" ID="Label2" Text="Điểm số"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtxtPointLevel" runat="server">
                    </tlk:RadTextBox>
                </td>--%>
            </tr>       
        <tr>
              <td class="lb">
                    <asp:Label runat="server" ID="lbKetQua" Text="Xếp loại"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboKetQua" runat="server">
                    </tlk:RadComboBox>
                </td>
                
                <td class="lb">
                    <asp:Label runat="server" ID="lbGraduateYear" Text="Năm tốt nghiệp"></asp:Label>                  
                </td>
                <td>
                    <%--<tlk:RadNumericTextBox runat="server" ID="rntGraduateYear1" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSizes="4" MinValue="1990" MaxValue="9999">
                    </tlk:RadNumericTextBox>--%>

                     <tlk:RadNumericTextBox ID="rntGraduateYear" runat="server" NumberFormat-DecimalDigits="1"
                    NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="4" MinValue="1900"
                    MaxValue="9999" SkinID="Number" CausesValidation="false">
                    <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                </tlk:RadNumericTextBox>


                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntGraduateYear"
                        runat="server" ErrorMessage="Bạn phải nhập năm tốt nghiệp."
                        ToolTip="Bạn phải nhập năm tốt nghiệp."> </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <%-- <td class="lb">
                    <asp:Label runat="server" ID="lbTrainingType" Text="Loại hình đào tạo"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTrainingType" runat="server">
                    </tlk:RadTextBox>
                </td>--%>
                 <td class="lb">
                    <asp:Label runat="server" ID="Label4" Text="Nội dung đào tạo"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtxtContentLevel" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTrainingForm" Text="Hình thức đào tạo"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTrainingForm" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
    </table>
    <tlk:RadGrid PageSize=50 ID="rgTrainingOutCompanyEdit" runat="server" Height="250px" Width="99%">
        <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,FROM_DATE,TO_DATE,YEAR_GRA,NAME_SHOOLS,FORM_TRAIN_ID,SPECIALIZED_TRAIN,RESULT_TRAIN,CERTIFICATE,
                          EFFECTIVE_DATE_FROM,EFFECTIVE_DATE_TO,STATUS,FK_PKEY,REASON_UNAPROVE "
            ClientDataKeyNames="ID,EMPLOYEE_ID,FROM_DATE,TO_DATE,YEAR_GRA,NAME_SHOOLS,FORM_TRAIN_ID,SPECIALIZED_TRAIN,RESULT_TRAIN,CERTIFICATE,
                          EFFECTIVE_DATE_FROM,EFFECTIVE_DATE_TO,STATUS,FK_PKEY,REASON_UNAPROVE"
            Caption="<%$ Translate: Thông tin chỉnh sửa %>">
            <Columns>
                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                </tlk:GridClientSelectColumn>
                <tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>
                <tlk:GridDateTimeColumn DataField="FROM_DATE" HeaderText="<%$ Translate: Từ tháng%>"
                    UniqueName="FROM_DATE" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                    Visible="true" DataFormatString="{0:MM/yyyy}">
                    <HeaderStyle Width="120px" />
                    <ItemStyle Width="120px" />
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn DataField="TO_DATE" HeaderText="<%$ Translate: Tới tháng%>"
                    UniqueName="TO_DATE" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:MM/yyyy}"
                    CurrentFilterFunction="EqualTo" Visible="true">
                    <HeaderStyle Width="120px" />
                    <ItemStyle Width="120px" />
                </tlk:GridDateTimeColumn>
                <tlk:GridNumericColumn HeaderText="<%$ Translate: Năm tốt nghiệp %>" DataField="YEAR_GRA"
                    UniqueName="YEAR_GRA" SortExpression="YEAR_GRA" ShowFilterIcon="true">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </tlk:GridNumericColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên trường %>" DataField="NAME_SHOOLS"
                    UniqueName="NAME_SHOOLS" SortExpression="NAME_SHOOLS" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức đào tạo %>" DataField="FORM_TRAIN_NAME"
                    UniqueName="FORM_TRAIN_NAME" SortExpression="FORM_TRAIN_NAME" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Chuyên ngành %>" DataField="SPECIALIZED_TRAIN"
                    UniqueName="SPECIALIZED_TRAIN" SortExpression="SPECIALIZED_TRAIN" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả đào tạo %>" DataField="RESULT_TRAIN"
                    UniqueName="RESULT_TRAIN" SortExpression="RESULT_TRAIN" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Bằng cấp/chứng chỉ %>" DataField="CERTIFICATE"
                    UniqueName="CERTIFICATE" SortExpression="CERTIFICATE" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECTIVE_DATE_FROM"
                    UniqueName="EFFECTIVE_DATE_FROM" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_FROM"
                    ShowFilterIcon="true">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EFFECTIVE_DATE_TO"
                    UniqueName="EFFECTIVE_DATE_TO" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_TO"
                    ShowFilterIcon="true">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái phê duyệt%>" DataField="STATUS_NAME"
                    UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do không phê duyệt %>" DataField="REASON_UNAPROVE"
                    UniqueName="REASON_UNAPROVE" SortExpression="REASON_UNAPROVE">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </tlk:RadGrid>
    <br />
    <tlk:RadGrid PageSize=50 ID="rgTrainingOutCompany" runat="server" Height="250px" Width="99%">
        <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,FROM_DATE,TO_DATE,YEAR_GRA,NAME_SHOOLS,FORM_TRAIN_ID,SPECIALIZED_TRAIN,RESULT_TRAIN,CERTIFICATE,
                            EFFECTIVE_DATE_FROM,EFFECTIVE_DATE_TO " ClientDataKeyNames="ID,EMPLOYEE_ID,FROM_DATE,TO_DATE,YEAR_GRA,NAME_SHOOLS,FORM_TRAIN_ID,SPECIALIZED_TRAIN,RESULT_TRAIN,CERTIFICATE,
                            EFFECTIVE_DATE_FROM,EFFECTIVE_DATE_TO" Caption="<%$ Translate: Thông tin hiện tại %>">
            <Columns>
                <tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow">
                    <HeaderStyle Wrap="False" Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>
                <tlk:GridDateTimeColumn DataField="FROM_DATE" HeaderText="<%$ Translate: Từ tháng%>"
                    UniqueName="FROM_DATE" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                    Visible="true" DataFormatString="{0:MM/yyyy}">
                    <HeaderStyle Width="120px" />
                    <ItemStyle Width="120px" />
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn DataField="TO_DATE" HeaderText="<%$ Translate: Tới tháng%>"
                    UniqueName="TO_DATE" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:MM/yyyy}"
                    CurrentFilterFunction="EqualTo" Visible="true">
                    <HeaderStyle Width="120px" />
                    <ItemStyle Width="120px" />
                </tlk:GridDateTimeColumn>
                <tlk:GridNumericColumn HeaderText="<%$ Translate: Năm tốt nghiệp %>" DataField="YEAR_GRA"
                    UniqueName="YEAR_GRA" SortExpression="YEAR_GRA" ShowFilterIcon="true">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </tlk:GridNumericColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên trường %>" DataField="NAME_SHOOLS"
                    UniqueName="NAME_SHOOLS" SortExpression="NAME_SHOOLS" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức đào tạo %>" DataField="FORM_TRAIN_NAME"
                    UniqueName="FORM_TRAIN_NAME" SortExpression="FORM_TRAIN_NAME" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Chuyên ngành %>" DataField="SPECIALIZED_TRAIN"
                    UniqueName="SPECIALIZED_TRAIN" SortExpression="SPECIALIZED_TRAIN" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả đào tạo %>" DataField="RESULT_TRAIN"
                    UniqueName="RESULT_TRAIN" SortExpression="RESULT_TRAIN" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Bằng cấp/chứng chỉ %>" DataField="CERTIFICATE"
                    UniqueName="CERTIFICATE" SortExpression="CERTIFICATE" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECTIVE_DATE_FROM"
                    UniqueName="EFFECTIVE_DATE_FROM" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_FROM"
                    ShowFilterIcon="true">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EFFECTIVE_DATE_TO"
                    UniqueName="EFFECTIVE_DATE_TO" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_TO"
                    ShowFilterIcon="true">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
            </Columns>
        </MasterTableView>
    </tlk:RadGrid>
    <tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function clientButtonClicking(sender, args) {

            }

           
        </script>
    </tlk:RadCodeBlock>
</div>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />

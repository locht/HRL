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
        <MasterTableView DataKeyNames="ID,FROM_DATE,TO_DATE,SCHOOLS_ID,SCHOOLS_NAME,SPECIALIZED_TRAIN_ID,SPECIALIZED_TRAIN_NAME,LEVEL_ID,LEVEL_NAME,CERTIFICATE_ID,CERTIFICATE,RESULT_TRAIN_ID,RESULT_TRAIN_NAME,YEAR_GRA,CONTENT_TRAIN,FORM_TRAIN_ID,FORM_TRAIN_NAME,STATUS,STATUS_NAME,FK_PKEY,REASON_UNAPROVE"
            ClientDataKeyNames="ID,FROM_DATE,TO_DATE,SCHOOLS_ID,SCHOOLS_NAME,SPECIALIZED_TRAIN_ID,SPECIALIZED_TRAIN_NAME,LEVEL_ID,LEVEL_NAME,CERTIFICATE_ID,CERTIFICATE,RESULT_TRAIN_ID,RESULT_TRAIN_NAME,YEAR_GRA,CONTENT_TRAIN,FORM_TRAIN_ID,FORM_TRAIN_NAME,STATUS,STATUS_NAME,FK_PKEY,REASON_UNAPROVE"
            Caption="<%$ Translate: Thông tin chỉnh sửa %>">
            <Columns>
                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                </tlk:GridClientSelectColumn>
                <tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>
                 <tlk:GridDateTimeColumn DataField="FROM_DATE" HeaderText="Từ ngày"
                        UniqueName="FROM_DATE" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                        Visible="true" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn DataField="TO_DATE" HeaderText="Tới ngày"
                        UniqueName="TO_DATE" ShowFilterIcon="false" DataFormatString="{0:dd/MM/yyyy}"
                        CurrentFilterFunction="EqualTo" Visible="true">
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn DataField="SCHOOLS_NAME" HeaderText="Tên trường"
                        UniqueName="SCHOOLS_NAME" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                        Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="SPECIALIZED_TRAIN_NAME" HeaderText="Ngành học"
                        UniqueName="SPECIALIZED_TRAIN_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="LEVEL_NAME" HeaderText="Trình độ"
                        UniqueName="LEVEL_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="CERTIFICATE" HeaderText="Bằng cấp/Chứng chỉ"
                        UniqueName="CERTIFICATE" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="RESULT_TRAIN_NAME" HeaderText="Xếp loại"
                        UniqueName="RESULT_TRAIN_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains" Visible="true">
                    </tlk:GridBoundColumn> 
                    <tlk:GridBoundColumn DataField="YEAR_GRA" HeaderText="Năm tốt nghiệp"
                        UniqueName="YEAR_GRA" ShowFilterIcon="false" Visible="true">
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn DataField="CONTENT_TRAIN" HeaderText="Nội dung đào tạo"
                        UniqueName="CONTENT_TRAIN" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains" Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FORM_TRAIN_NAME" HeaderText="Hình thức đào tạo"
                        UniqueName="FORM_TRAIN_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
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
        <MasterTableView DataKeyNames="ID,FROM_DATE,TO_DATE,SCHOOLS_ID,SCHOOLS_NAME,SPECIALIZED_TRAIN_ID,SPECIALIZED_TRAIN_NAME,LEVEL_ID,LEVEL_NAME,CERTIFICATE_ID,CERTIFICATE,RESULT_TRAIN_ID,RESULT_TRAIN_NAME,YEAR_GRA,CONTENT_LEVEL,FORM_TRAIN_ID,FORM_TRAIN_NAME" ClientDataKeyNames="ID,FROM_DATE,TO_DATE,SCHOOLS_ID,SCHOOLS_NAME,SPECIALIZED_TRAIN_ID,SPECIALIZED_TRAIN_NAME,LEVEL_ID,LEVEL_NAME,CERTIFICATE_ID,CERTIFICATE,RESULT_TRAIN_ID,RESULT_TRAIN_NAME,YEAR_GRA,CONTENT_LEVEL,FORM_TRAIN_ID,FORM_TRAIN_NAME" Caption="<%$ Translate: Thông tin hiện tại %>">
            <Columns>
                <tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow">
                    <HeaderStyle Wrap="False" Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>
                <tlk:GridDateTimeColumn DataField="FROM_DATE" HeaderText="Từ ngày"
                        UniqueName="FROM_DATE" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                        Visible="true" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn DataField="TO_DATE" HeaderText="Tới ngày"
                        UniqueName="TO_DATE" ShowFilterIcon="false" DataFormatString="{0:dd/MM/yyyy}"
                        CurrentFilterFunction="EqualTo" Visible="true">
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn DataField="SCHOOLS_NAME" HeaderText="Tên trường"
                        UniqueName="SCHOOLS_NAME" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                        Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="SPECIALIZED_TRAIN_NAME" HeaderText="Ngành học"
                        UniqueName="SPECIALIZED_TRAIN_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="LEVEL_NAME" HeaderText="Trình độ"
                        UniqueName="LEVEL_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="CERTIFICATE" HeaderText="Bằng cấp/Chứng chỉ"
                        UniqueName="CERTIFICATE" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="RESULT_TRAIN_NAME" HeaderText="Xếp loại"
                        UniqueName="RESULT_TRAIN_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains" Visible="true">
                    </tlk:GridBoundColumn> 
                    <tlk:GridBoundColumn DataField="YEAR_GRA" HeaderText="Năm tốt nghiệp"
                        UniqueName="YEAR_GRA" ShowFilterIcon="false" Visible="true">
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn DataField="CONTENT_LEVEL" HeaderText="Nội dung đào tạo"
                        UniqueName="CONTENT_LEVEL" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains" Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FORM_TRAIN_NAME" HeaderText="Hình thức đào tạo"
                        UniqueName="FORM_TRAIN_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
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

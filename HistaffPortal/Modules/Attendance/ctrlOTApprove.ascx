﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOTApprove.ascx.vb"
    Inherits="Attendance.ctrlOTApprove" %>
<tlk:RadSplitter runat="server" ID="splitAll" Orientation="Horizontal" Width="100%"
    Height="100%">
    <tlk:RadPane runat="server" ID="paneTop" Scrolling="None" Height="80px">
        <div style="display: block; padding: 5px">
            <table class="table-form">
                <tr>
                    <td class="lb">
                        <%# Translate("Đăng ký từ ngày")%>
                    </td>
                    <td>
                        <tlk:RadDatePicker runat="server" ID="rdpFromDateSearch">
                        </tlk:RadDatePicker>
                    </td>
                    <td class="lb">
                        <%# Translate("Đến ngày")%>
                    </td>
                    <td>
                        <tlk:RadDatePicker runat="server" ID="rdpToDateSearch">
                        </tlk:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Hệ số làm thêm")%>
                    </td>
                    <td>
                        <tlk:RadComboBox runat="server" ID="cboLeaveTypeSearch">
                            <Items>
                                <tlk:RadComboBoxItem Text='<%$ Translate: Tất cả %>' Value="0" />
                            </Items>
                        </tlk:RadComboBox>
                    </td>
                    <td class="lb">
                        <%# Translate("Nhân viên") %>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtEmployee">
                        </tlk:RadTextBox>
                    </td>
                    <td>
                        <tlk:RadButton runat="server" ID="btnSearch" Text='<%$ Translate: Tìm kiếm %>'>
                        </tlk:RadButton>
                    </td>
                </tr>
            </table>
        </div>
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="RadPane1" Height="320px">
        <tlk:RadTabStrip runat="server" ID="tabTypeApprove" SelectedIndex="0" MultiPageID="pageTypeApprove">
            <Tabs>
                <tlk:RadTab Text='<%$ Translate: Đăng ký chưa duyệt %>'>
                </tlk:RadTab>
                <tlk:RadTab Text='<%$ Translate: Đăng ký đã duyệt %>'>
                </tlk:RadTab>
                <tlk:RadTab Text='<%$ Translate: Đăng ký không duyệt %>'>
                </tlk:RadTab>
            </Tabs>
        </tlk:RadTabStrip>
        <tlk:RadMultiPage runat="server" ID="pageTypeApprove" SelectedIndex="0">
            <tlk:RadPageView ID="RadPageView1" runat="server">
                <div style="display: block; padding: 5px">
                    <asp:ValidationSummary runat="server" ID="valSum" ValidationGroup="Approve" />
                    <%# Translate("Ý kiến:")%>(<span class="lbReg">*</span>)
                    <tlk:RadTextBox runat="server" ID="txtNote" EmptyMessage='<%$ Translate: Ý kiến phê duyệt %>'
                        Width="300px">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator runat="server" ID="valNote" ControlToValidate="txtNote"
                        ValidationGroup="Approve" ErrorMessage='<%$ Translate: Chưa nhập ý kiến phê duyệt %>'></asp:RequiredFieldValidator>
                    <tlk:RadButton runat="server" ID="btnApprove" Text='<%$ Translate: Duyệt %>' ForeColor="Blue"
                        ValidationGroup="Approve">
                    </tlk:RadButton>
                    <tlk:RadButton runat="server" ID="btnDeny" Text='<%$ Translate: Không duyệt %>' ForeColor="Red"
                        ValidationGroup="Approve">
                    </tlk:RadButton>
                </div>
                <tlk:RadGrid runat="server" ID="grvWaiting" Width="99%">
                    <MasterTableView ClientDataKeyNames="ID" DataKeyNames="ID,ID_REGGROUP">
                        <Columns>
                             <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Mã nhân viên %>' DataField="EMPLOYEE_CODE">
                                <ItemStyle Width="75" />
                                <HeaderStyle Width="75" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Tên nhân viên %>' DataField="EMPLOYEE_NAME">
                                <ItemStyle Width="150" />
                                <HeaderStyle Width="150" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Ngày đăng ký %>' DataField="REGDATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle HorizontalAlign="Center" Width="70" />
                                <HeaderStyle Width="70" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Từ ngày %>' DataField="FROM_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle HorizontalAlign="Center" Width="70" />
                                <HeaderStyle Width="70" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Đến ngày %>' DataField="TO_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle HorizontalAlign="Center" Width="70" />
                                <HeaderStyle Width="70" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Đăng ký nghỉ bù %>' DataField="NGHI_BU">
                                <ItemStyle Width="120" />
                                <HeaderStyle Width="120" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Hệ số làm thêm %>' DataField="SIGN_NAME">
                                <ItemStyle Width="120" />
                                <HeaderStyle Width="120" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Chi tiết đăng ký %>' DataField="DISPLAY">
                                <ItemStyle Width="150" />
                                <HeaderStyle Width="150" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Tổng số giờ %>' DataField="HOURCOUNT">
                                <ItemStyle HorizontalAlign="Right" Width="70" />
                                <HeaderStyle Width="70" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Ghi chú %>' DataField="NOTE">
                                <ItemStyle Width="200" />
                                <HeaderStyle Width="200" />
                            </tlk:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPageView>
            <tlk:RadPageView ID="RadPageView2" runat="server">
                <tlk:RadGrid runat="server" ID="grvApproved" Width="99%">
                    <MasterTableView ClientDataKeyNames="ID" DataKeyNames="ID">
                        <Columns>
                             <tlk:GridBoundColumn HeaderText='<%$ Translate: Mã nhân viên %>' DataField="EMPLOYEE_CODE">
                                <ItemStyle Width="75" />
                                <HeaderStyle Width="75" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Tên nhân viên %>' DataField="EMPLOYEE_NAME">
                                <ItemStyle Width="120" />
                                <HeaderStyle Width="120" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Ngày đăng ký %>' DataField="REGDATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle HorizontalAlign="Center" Width="70" />
                                <HeaderStyle Width="70" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Từ ngày %>' DataField="FROM_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle HorizontalAlign="Center" Width="70" />
                                <HeaderStyle Width="70" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Đến ngày %>' DataField="TO_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle HorizontalAlign="Center" Width="70" />
                                <HeaderStyle Width="70" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Đăng ký nghỉ bù %>' DataField="NGHI_BU">
                                <ItemStyle Width="120" />
                                <HeaderStyle Width="120" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Hệ số làm thêm %>' DataField="SIGN_NAME">
                                <ItemStyle Width="120" />
                                <HeaderStyle Width="120" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Chi tiết đăng ký %>' DataField="DISPLAY">
                                <ItemStyle Width="120" />
                                <HeaderStyle Width="120" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Tổng số giờ %>' DataField="HOURCOUNT">
                                <ItemStyle HorizontalAlign="Right" Width="70" />
                                <HeaderStyle Width="70" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Ghi chú %>' DataField="NOTE">
                                <ItemStyle Width="150" />
                                <HeaderStyle Width="150" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Cấp duyệt %>' DataField="APP_LEVEL">
                                <ItemStyle HorizontalAlign="Right" Width="60" />
                                <HeaderStyle Width="60" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Ngày duyệt %>' DataField="APP_DATE"
                                DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                <ItemStyle HorizontalAlign="Center" Width="70" />
                                <HeaderStyle Width="70" />
                            </tlk:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPageView>
            <tlk:RadPageView ID="RadPageView3" runat="server">
                <tlk:RadGrid runat="server" ID="grvDenied" Width="99%">
                    <MasterTableView ClientDataKeyNames="ID" DataKeyNames="ID">
                        <Columns>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Mã nhân viên %>' DataField="EMPLOYEE_CODE">
                                <ItemStyle Width="75" />
                                <HeaderStyle Width="75" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Tên nhân viên %>' DataField="EMPLOYEE_NAME">
                                <ItemStyle Width="120" />
                                <HeaderStyle Width="120" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Ngày đăng ký %>' DataField="REGDATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle HorizontalAlign="Center" Width="70" />
                                <HeaderStyle Width="70" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Từ ngày %>' DataField="FROM_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle HorizontalAlign="Center" Width="70" />
                                <HeaderStyle Width="70" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Đến ngày %>' DataField="TO_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle HorizontalAlign="Center" Width="70" />
                                <HeaderStyle Width="70" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Đăng ký nghỉ bù %>' DataField="NGHI_BU">
                                <ItemStyle Width="120" />
                                <HeaderStyle Width="120" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Hệ số làm thêm %>' DataField="SIGN_NAME">
                                <ItemStyle Width="120" />
                                <HeaderStyle Width="120" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Chi tiết đăng ký %>' DataField="DISPLAY">
                                <ItemStyle Width="120" />
                                <HeaderStyle Width="120" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Tổng số giờ %>' DataField="HOURCOUNT">
                                <ItemStyle HorizontalAlign="Right" Width="70" />
                                <HeaderStyle Width="70" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Ghi chú %>' DataField="NOTE">
                                <ItemStyle Width="150" />
                                <HeaderStyle Width="150" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Cấp duyệt %>' DataField="APP_LEVEL">
                                <ItemStyle HorizontalAlign="Right" Width="60" />
                                <HeaderStyle Width="60" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Ngày duyệt %>' DataField="APP_DATE"
                                DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                <ItemStyle HorizontalAlign="Center" Width="70" />
                                <HeaderStyle Width="70" />
                            </tlk:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPageView>
        </tlk:RadMultiPage>
    </tlk:RadPane>
</tlk:RadSplitter>
﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ApproveEmployee_Edit.ascx.vb"
    Inherits="Profile.ctrlHU_ApproveEmployee_Edit" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarWorkings" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="80px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Lý do không phê duyệt")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" runat="server" Width="100%" MaxLength="250">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 500px;">
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgData" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,STATUS,EMPLOYEE_CODE,EMPLOYEE_NAME,MARITAL_STATUS_NAME,PER_ADDRESS,PER_PROVINCE_NAME,PER_DISTRICT_NAME,PER_WARD_NAME,NAV_ADDRESS,NAV_PROVINCE_NAME,NAV_DISTRICT_NAME,NAV_WARD_NAME,ID_NO,ID_DATE,ID_PLACE_NAME,ID_PLACE,
                    EXPIRE_DATE_IDNO,CONTACT_PER,RELATION_PER_CTR,RELATION_PER_CTR_NAME,CONTACT_PER_MBPHONE,VILLAGE,HOME_PHONE,MOBILE_PHONE,WORK_EMAIL,PER_EMAIL,PERSON_INHERITANCE,BANK_NO,BANK_ID,BANK_NAME,BANK_BRANCH_NAME" 
                    ClientDataKeyNames="ID,STATUS,EMPLOYEE_CODE,EMPLOYEE_NAME,MARITAL_STATUS_NAME,PER_ADDRESS,PER_PROVINCE_NAME,PER_DISTRICT_NAME,PER_WARD_NAME,NAV_ADDRESS,NAV_PROVINCE_NAME,NAV_DISTRICT_NAME,NAV_WARD_NAME,ID_NO,ID_DATE,ID_PLACE_NAME,ID_PLACE,
                    EXPIRE_DATE_IDNO,CONTACT_PER,RELATION_PER_CTR,RELATION_PER_CTR_NAME,CONTACT_PER_MBPHONE,VILLAGE,HOME_PHONE,MOBILE_PHONE,WORK_EMAIL,PER_EMAIL,PERSON_INHERITANCE,BANK_NO,BANK_ID,BANK_NAME,BANK_BRANCH_NAME" >
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tình trạng hôn nhân %>" DataField="MARITAL_STATUS_NAME"
                                UniqueName="MARITAL_STATUS_NAME" SortExpression="MARITAL_STATUS_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số CMND %>" DataField="ID_NO"
                                UniqueName="ID_NO" SortExpression="ID_NO">
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn UniqueName="ID_DATE" HeaderText="<%$ Translate: Ngày cấp %>"
                                ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="ID_DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn UniqueName="EXPIRE_DATE_IDNO" HeaderText="<%$ Translate: Ngày hết hiệu lực %>"
                                ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="EXPIRE_DATE_IDNO">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi cấp %>" DataField="ID_PLACE_NAME"
                                UniqueName="ID_PLACE_NAME" SortExpression="ID_PLACE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người liên hệ %>" DataField="CONTACT_PER"
                                UniqueName="CONTACT_PER" SortExpression="CONTACT_PER">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mối quan hệ %>" DataField="RELATION_PER_CTR_NAME"
                                UniqueName="RELATION_PER_CTR_NAME" SortExpression="RELATION_PER_CTR_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số điện thoại người liên hệ %>" DataField="CONTACT_PER_MBPHONE"
                                UniqueName="CONTACT_PER_MBPHONE" SortExpression="CONTACT_PER_MBPHONE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ thường trú %>" DataField="PER_ADDRESS"
                                UniqueName="PER_ADDRESS" SortExpression="PER_ADDRESS">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thành phố %>" DataField="PER_PROVINCE_NAME"
                                UniqueName="PER_PROVINCE_NAME" SortExpression="PER_PROVINCE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Quận huyện %>" DataField="PER_DISTRICT_NAME"
                                UniqueName="PER_DISTRICT_NAME" SortExpression="PER_DISTRICT_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Xã phường %>" DataField="VILLAGE"
                                UniqueName="VILLAGE" SortExpression="VILLAGE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thôn/Ấp/ Khu phố %>" DataField="PER_WARD_NAME"
                                UniqueName="PER_WARD_NAME" SortExpression="PER_WARD_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ tạm trú %>" DataField="NAV_ADDRESS"
                                UniqueName="NAV_ADDRESS" SortExpression="NAV_ADDRESS">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thành phố %>" DataField="NAV_PROVINCE_NAME"
                                UniqueName="NAV_PROVINCE_NAME" SortExpression="NAV_PROVINCE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Quận huyện %>" DataField="NAV_DISTRICT_NAME"
                                UniqueName="NAV_DISTRICT_NAME" SortExpression="NAV_DISTRICT_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Xã phường %>" DataField="NAV_WARD_NAME"
                                UniqueName="NAV_WARD_NAME" SortExpression="NAV_WARD_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Điện thoại cố định %>" DataField="HOME_PHONE"
                                UniqueName="HOME_PHONE" SortExpression="HOME_PHONE">
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Điện thoại di động %>" DataField="MOBILE_PHONE"
                                UniqueName="MOBILE_PHONE" SortExpression="MOBILE_PHONE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Email công ty %>" DataField="WORK_EMAIL"
                                UniqueName="WORK_EMAIL" SortExpression="WORK_EMAIL">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Email cá nhân %>" DataField="PER_EMAIL"
                                UniqueName="PER_EMAIL" SortExpression="PER_EMAIL">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên người thụ hưởng %>" DataField="PERSON_INHERITANCE"
                                UniqueName="PERSON_INHERITANCE" SortExpression="PERSON_INHERITANCE">
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Số TK chuyển khoản %>" DataField="BANK_NO"
                                UniqueName="BANK_NO" SortExpression="BANK_NO">
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngân hàng %>" DataField="BANK_NAME"
                                UniqueName="BANK_NAME" SortExpression="BANK_NAME">
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi nhánh ngân hàng %>" DataField="BANK_BRANCH_NAME"
                                UniqueName="BANK_BRANCH_NAME" SortExpression="BANK_BRANCH_NAME">
                            </tlk:GridBoundColumn>                            
                        </Columns>
                        <HeaderStyle Width="120px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {

        }


    </script>
</tlk:RadCodeBlock>

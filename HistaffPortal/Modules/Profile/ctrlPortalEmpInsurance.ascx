﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalEmpInsurance.ascx.vb"
    Inherits="Profile.ctrlPortalEmpInsurance" %>
<tlk:RadGrid PageSize="50" ID="rgIns" runat="server" Height="350px" AllowFilteringByColumn="true"
    Scrolling="both">
    <MasterTableView DataKeyNames="ID">
        <Columns>
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đợt khai báo %>" DataField="DECLARE_DATE"
                DataFormatString="{0:MM/yyyy}" UniqueName="DECLARE_DATE" SortExpression="DECLARE_DATE"
                HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                AutoPostBackOnFilter="true" />
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECTIVE_DATE"
                DataFormatString="{0:dd/MM/yyyy}" UniqueName="EFFECTIVE_DATE" SortExpression="EFFECTIVE_DATE"
                HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                AutoPostBackOnFilter="true" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị đóng bảo hiểm %>" DataField="INS_ORG_NAME"
                UniqueName="INS_ORG_NAME" SortExpression="INS_ORG_NAME" ShowFilterIcon="false"
                HeaderStyle-Width="200px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại biến động %>" DataField="ARISING_TYPE_NM"
                UniqueName="ARISING_TYPE_NM" SortExpression="ARISING_TYPE_NM" ShowFilterIcon="false"
                HeaderStyle-Width="150px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức lương cũ %>" DataFormatString="{0:N0}"
                DataField="SALARY_PRE_PERIOD" UniqueName="SALARY_PRE_PERIOD" SortExpression="SALARY_PRE_PERIOD"
                HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                AutoPostBackOnFilter="true" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức lương mới %>" DataFormatString="{0:N0}"
                DataField="SALARY_NOW_PERIOD" UniqueName="SALARY_NOW_PERIOD" SortExpression="SALARY_NOW_PERIOD"
                HeaderStyle-Width="90px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                AutoPostBackOnFilter="true" />


            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHXH %>" AllowFiltering="false" DataField="SI_CHK" DataType="System.Boolean"
                FilterControlWidth="20px" ShowFilterIcon="false" SortExpression="SI_CHK" UniqueName="SI_CHK">
                <HeaderStyle HorizontalAlign="Center" Width="40px" />
            </tlk:GridCheckBoxColumn>
            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHYT %>" AllowFiltering="false" DataField="HI_CHK" DataType="System.Boolean"
                FilterControlWidth="20px" ShowFilterIcon="false" SortExpression="HI_CHK" UniqueName="HI_CHK">
                <HeaderStyle HorizontalAlign="Center" Width="40px" />
            </tlk:GridCheckBoxColumn>
            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHTN %>" AllowFiltering="false" DataField="UI_CHK" DataType="System.Boolean"
                FilterControlWidth="20px" ShowFilterIcon="false" SortExpression="UI_CHK" UniqueName="UI_CHK">
                <HeaderStyle HorizontalAlign="Center" Width="40px" />
            </tlk:GridCheckBoxColumn>
            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHTNLD - BNN %>" AllowFiltering="false" DataField="BHTNLDBNN_CHK"
                DataType="System.Boolean" FilterControlWidth="20px" ShowFilterIcon="false" SortExpression="BHTNLDBNN_CHK"
                UniqueName="BHTNLDBNN_CHK">
                <HeaderStyle HorizontalAlign="Center" Width="40px" />
            </tlk:GridCheckBoxColumn>


            <tlk:GridBoundColumn HeaderText="<%$ Translate: Công việc %>" DataField="TITLE_NAME"
                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" ShowFilterIcon="false" HeaderStyle-Width="150px"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                UniqueName="ORG_NAME" SortExpression="ORG_NAME" ShowFilterIcon="false" HeaderStyle-Width="150px"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridNumericColumn HeaderText="Truy thu BHXH" DataFormatString="{0:###,##0.##}"
                DataField="A_SI" UniqueName="A_SI" HeaderStyle-Width="100px" FilterControlWidth="99%"
                ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true"
                SortExpression="A_SI" />
            <tlk:GridNumericColumn HeaderText="Truy thu BHYT" DataFormatString="{0:###,##0.##}"
                DataField="A_HI" UniqueName="A_HI" HeaderStyle-Width="100px" FilterControlWidth="99%"
                ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true"
                SortExpression="A_HI" />
            <tlk:GridNumericColumn HeaderText="Truy thu BHTN" DataFormatString="{0:###,##0.##}"
                DataField="A_UI" UniqueName="A_UI" HeaderStyle-Width="100px" FilterControlWidth="99%"
                ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true"
                SortExpression="A_UI" />
            <tlk:GridNumericColumn HeaderText="Truy thu BHTNLĐ – BNN" DataFormatString="{0:###,##0.##}"
                DataField="A_TNLD_BNN" UniqueName="A_TNLD_BNN" HeaderStyle-Width="100px" FilterControlWidth="99%"
                ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true"
                SortExpression="A_TNLD_BNN" />
            <tlk:GridNumericColumn HeaderText="Thoái thu BHXH" DataFormatString="{0:###,##0.##}"
                DataField="R_SI" UniqueName="R_SI" HeaderStyle-Width="100px" FilterControlWidth="99%"
                ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true"
                SortExpression="R_SI" />
            <tlk:GridNumericColumn HeaderText="Thoái thu BHYT" DataFormatString="{0:###,##0.##}"
                DataField="R_HI" UniqueName="R_HI" HeaderStyle-Width="100px" FilterControlWidth="99%"
                ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true"
                SortExpression="R_HI" />
            <tlk:GridNumericColumn HeaderText="Thoái thu BHTN" DataFormatString="{0:###,##0.##}"
                DataField="R_UI" UniqueName="R_UI" HeaderStyle-Width="100px" FilterControlWidth="99%"
                ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true"
                SortExpression="R_UI" />
            <tlk:GridNumericColumn HeaderText="Thoái thu BHTNLĐ – BNN" DataFormatString="{0:###,##0.##}"
                DataField="R_TNLD_BNN" UniqueName="R_TNLD_BNN" HeaderStyle-Width="100px" FilterControlWidth="99%"
                ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true"
                SortExpression="R_TNLD_BNN" />
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
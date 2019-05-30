<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlWorking.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlWorking" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal" SkinID="Demo">
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgGrid" runat="server" AllowFilteringByColumn="true" Height="100%">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <%--<tlk:GridBoundColumn HeaderText="Loại quyết định" DataField="DECISION_TYPE_NAME"
                        UniqueName="DECISION_TYPE_NAME" SortExpression="DECISION_TYPE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="Ngày hết hiệu lực" DataField="EXPIRE_DATE"
                        UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Cấp nhân sự" DataField="STAFF_RANK_NAME"
                        UniqueName="STAFF_RANK_NAME" SortExpression="STAFF_RANK_NAME">
                    </tlk:GridBoundColumn>
                   
                   <tlk:GridBoundColumn HeaderText="Đối tượng chấm công" DataField="OBJECT_ATTENDANCE_NAME"
                        UniqueName="OBJECT_ATTENDANCE_NAME" SortExpression="OBJECT_ATTENDANCE_NAME">
                    </tlk:GridBoundColumn>


                    <tlk:GridDateTimeColumn HeaderText=" Ngày nộp đơn" DataField="FILING_DATE"
                        UniqueName="FILING_DATE" SortExpression="FILING_DATE" DataFormatString="{0:dd/MM/yyyy}">
                    </tlk:GridDateTimeColumn>

                    <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        SortExpression="ORG_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="Ngày ký" DataField="SIGN_DATE"
                        UniqueName="SIGN_DATE" SortExpression="SIGN_DATE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="Người ký" DataField="SIGN_NAME"
                        UniqueName="SIGN_NAME" SortExpression="SIGN_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Chức danh người ký" DataField="SIGN_TITLE"
                        UniqueName="SIGN_TITLE" SortExpression="SIGN_TITLE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC"
                        SortExpression="ORG_DESC" HeaderStyle-Width="130px" Visible="false">
                    </tlk:GridBoundColumn>--%>
                </Columns>
                <HeaderStyle Width="120px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
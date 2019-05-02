<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalTraining.ascx.vb"
    Inherits="Profile.ctrlPortalTraining" %>
<tlk:RadGrid PageSize="20" ID="rgTraining" runat="server" Height="350px" AllowFilteringByColumn="true"
    Scrolling="both">
    <MasterTableView DataKeyNames="ID">
        <Columns>
            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
            </tlk:GridClientSelectColumn>
            <tlk:GridBoundColumn HeaderText="Tên khóa đào tạo" DataField="TR_COURSE_NAME"
                SortExpression="TR_COURSE_NAME" UniqueName="TR_COURSE_NAME" />
            <tlk:GridBoundColumn HeaderText="Tên chương trình đào tạo" DataField="TR_PROGRAM_NAME"
                SortExpression="TR_PROGRAM_NAME" UniqueName="TR_PROGRAM_NAME" />
            <tlk:GridBoundColumn HeaderText="Nhóm chương trình" DataField="TR_PROGRAM_GROUP_NAME"
                SortExpression="TR_PROGRAM_GROUP_NAME" UniqueName="TR_PROGRAM_GROUP_NAME" />
            <tlk:GridBoundColumn HeaderText="Lĩnh vực đào tạo" DataField="TR_TRAIN_FIELD_NAME"
                SortExpression="TR_TRAIN_FIELD_NAME" UniqueName="TR_TRAIN_FIELD_NAME" />
            <tlk:GridBoundColumn HeaderText="Hình thức đào tạo" DataField="TR_TRAIN_FORM_NAME"
                SortExpression="TR_TRAIN_FORM_NAME" UniqueName="TR_TRAIN_FORM_NAME" />
            <tlk:GridBoundColumn HeaderText="Thời lượng" DataField="DURATION"
                SortExpression="DURATION" UniqueName="DURATION" />
            <tlk:GridBoundColumn HeaderText="Từ ngày" DataField="START_DATE"
                SortExpression="START_DATE" UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="Đến ngày" DataField="END_DATE"
                SortExpression="END_DATE" UniqueName="END_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="Trung tâm đào tạo" DataField="TR_UNIT_NAME"
                SortExpression="TR_UNIT_NAME" UniqueName="TR_UNIT_NAME" />
            <tlk:GridBoundColumn HeaderText="Nội dung đào tạo" DataField="CONTENT"
                SortExpression="CONTENT" UniqueName="CONTENT" />
            <tlk:GridBoundColumn HeaderText="Mục đích" DataField="TARGET_TRAIN"
                SortExpression="TARGET_TRAIN" UniqueName="TARGET_TRAIN" />
            <tlk:GridBoundColumn HeaderText="Địa điểm tổ chức" DataField="VENUE"
                SortExpression="VENUE" UniqueName="VENUE" />
            <tlk:GridBoundColumn HeaderText="Điểm đào tạo" DataField="TOEIC_FINAL_SCORE"
                SortExpression="TOEIC_FINAL_SCORE" UniqueName="TOEIC_FINAL_SCORE" />
            <tlk:GridNumericColumn HeaderText="Chi phí" DataField="COST_TOTAL"
                SortExpression="COST_TOTAL" UniqueName="COST_TOTAL" DataFormatString="{0:###,###,###,##0}" />
            <tlk:GridBoundColumn HeaderText="Kết quả" DataField="IS_REACH"
                SortExpression="IS_REACH" UniqueName="IS_REACH" />
            <tlk:GridBoundColumn HeaderText="Xếp loại" DataField="RANK_NAME"
                SortExpression="RANK_NAME" UniqueName="RANK_NAME" />
            <tlk:GridBoundColumn HeaderText="Văn bằng/Chứng chỉ" DataField="CERTIFICATE_NO"
                SortExpression="CERTIFICATE_NO" UniqueName="CERTIFICATE_NO" />
            <tlk:GridBoundColumn HeaderText="Thời hạn chứng chỉ" DataField="CERTIFICATE_DATE"
                SortExpression="CERTIFICATE_DATE" UniqueName="CERTIFICATE_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="Ngày cấp chứng chỉ" DataField="CER_RECEIVE_DATE"
                SortExpression="CER_RECEIVE_DATE" UniqueName="CER_RECEIVE_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="Ngày hết hạn chứng chỉ" DataField="CER_EXPIRED_DATE"
                SortExpression="CER_EXPIRED_DATE" UniqueName="CER_EXPIRED_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="Số cam kết" DataField="COMITMENT_TRAIN_NO"
                SortExpression="COMITMENT_TRAIN_NO" UniqueName="COMITMENT_TRAIN_NO" />
            <tlk:GridBoundColumn HeaderText="Thời gian cam kết" DataField="COMMIT_WORK"
                SortExpression="COMMIT_WORK" UniqueName="COMMIT_WORK" />
            <tlk:GridBoundColumn HeaderText="Ngày bắt đầu cam kết" DataField="COMITMENT_START_DATE"
                SortExpression="COMITMENT_START_DATE" UniqueName="COMITMENT_START_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="Ngày kết thúc cam kết" DataField="COMITMENT_END_DATE"
                SortExpression="COMITMENT_END_DATE" UniqueName="COMITMENT_END_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="REMARK" SortExpression="REMARK"
                UniqueName="REMARK" />
        </Columns>
        <HeaderStyle Width="150px" />
    </MasterTableView>
</tlk:RadGrid>
<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalTraining.ascx.vb"
    Inherits="Profile.ctrlPortalTraining" %>
<tlk:RadGrid PageSize="20" ID="rgTraining" runat="server" Height="350px" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="ID">
        <Columns>
            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
            </tlk:GridClientSelectColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên khóa đào tạo %>" DataField="TR_COURSE_NAME"
                SortExpression="TR_COURSE_NAME" UniqueName="TR_COURSE_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên chương trình đào tạo %>" DataField="TR_PROGRAM_NAME"
                SortExpression="TR_PROGRAM_NAME" UniqueName="TR_PROGRAM_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm chương trình %>" DataField="TR_PROGRAM_GROUP_NAME"
                SortExpression="TR_PROGRAM_GROUP_NAME" UniqueName="TR_PROGRAM_GROUP_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lĩnh vực đào tạo %>" DataField="TR_TRAIN_FIELD_NAME"
                SortExpression="TR_TRAIN_FIELD_NAME" UniqueName="TR_TRAIN_FIELD_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức đào tạo %>" DataField="TR_TRAIN_FORM_NAME"
                SortExpression="TR_TRAIN_FORM_NAME" UniqueName="TR_TRAIN_FORM_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời lượng %>" DataField="DURATION"
                SortExpression="DURATION" UniqueName="DURATION" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="START_DATE"
                SortExpression="START_DATE" UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="END_DATE"
                SortExpression="END_DATE" UniqueName="END_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trung tâm đào tạo %>" DataField="TR_UNIT_NAME"
                SortExpression="TR_UNIT_NAME" UniqueName="TR_UNIT_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung đào tạo %>" DataField="CONTENT"
                SortExpression="CONTENT" UniqueName="CONTENT" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mục đích %>" DataField="TARGET_TRAIN"
                SortExpression="TARGET_TRAIN" UniqueName="TARGET_TRAIN" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa điểm tổ chức %>" DataField="VENUE"
                SortExpression="VENUE" UniqueName="VENUE" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm đào tạo %>" DataField="TOEIC_FINAL_SCORE"
                SortExpression="TOEIC_FINAL_SCORE" UniqueName="TOEIC_FINAL_SCORE" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả %>" DataField="IS_REACH"
                SortExpression="IS_REACH" UniqueName="IS_REACH" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Xếp loại %>" DataField="RANK_NAME"
                SortExpression="RANK_NAME" UniqueName="RANK_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Văn bằng/Chứng chỉ %>" DataField="CERTIFICATE_NO"
                SortExpression="CERTIFICATE_NO" UniqueName="CERTIFICATE_NO" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời hạn chứng chỉ %>" DataField="CERTIFICATE_DATE"
                SortExpression="CERTIFICATE_DATE" UniqueName="CERTIFICATE_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày cấp chứng chỉ %>" DataField="CER_RECEIVE_DATE"
                SortExpression="CER_RECEIVE_DATE" UniqueName="CER_RECEIVE_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hết hạn chứng chỉ %>" DataField="CER_EXPIRED_DATE"
                SortExpression="CER_EXPIRED_DATE" UniqueName="CER_EXPIRED_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số cam kết %>" DataField="COMITMENT_TRAIN_NO"
                SortExpression="COMITMENT_TRAIN_NO" UniqueName="COMITMENT_TRAIN_NO" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian cam kết  %>" DataField="COMMIT_WORK"
                SortExpression="COMMIT_WORK" UniqueName="COMMIT_WORK" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày bắt đầu cam kết %>" DataField="COMITMENT_START_DATE"
                SortExpression="COMITMENT_START_DATE" UniqueName="COMITMENT_START_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày kết thúc cam kết %>" DataField="COMITMENT_END_DATE"
                SortExpression="COMITMENT_END_DATE" UniqueName="COMITMENT_END_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                UniqueName="REMARK" />
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
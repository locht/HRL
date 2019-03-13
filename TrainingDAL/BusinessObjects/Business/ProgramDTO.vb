Public Class ProgramDTO
    Public ID As Decimal
    Public TR_PLAN_ID As Decimal?
    Public TR_PLAN_NAME As String
    Public TR_REQUEST_ID As Decimal?
    Public YEAR As Decimal?
    Public ORG_ID As Decimal?
    Public ORG_NAME As String
    Public TR_COURSE_ID As Decimal?
    Public TR_COURSE_NAME As String
    Public NAME As String
    Public TRAIN_FORM_ID As Decimal?
    Public TRAIN_FORM_NAME As String
    Public PROPERTIES_NEED_ID As Decimal?
    Public PROPERTIES_NEED_NAME As String
    Property PROGRAM_GROUP As String
    Property TRAIN_FIELD As String
    Public DURATION As Decimal?
    Public TR_DURATION_UNIT_ID As Decimal?
    Public TR_DURATION_UNIT_NAME As String
    Public START_DATE As Date?
    Public END_DATE As Date?
    Public DURATION_STUDY_ID As Decimal?
    Public DURATION_STUDY_NAME As String
    Public DURATION_HC As Decimal?
    Public DURATION_OT As Decimal?
    Public IS_REIMBURSE As Boolean?
    Public IS_RETEST As Boolean?
    Public TR_CURRENCY_ID As Decimal?
    Public TR_CURRENCY_NAME As String
    Public Property PLAN_STUDENT_NUMBER As Decimal?
    Public Property PLAN_COST_TOTAL_US As Decimal?
    Public Property PLAN_COST_TOTAL As Decimal?
    Public Property STUDENT_NUMBER As Decimal?
    Public Property COST_TOTAL_US As Decimal?
    Public Property COST_STUDENT_US As Decimal?
    Public Property COST_TOTAL As Decimal?
    Public Property COST_STUDENT As Decimal?
    Public Costs As List(Of ProgramCostDTO)
    Public Departments_NAME As String
    Public Units As List(Of ProgramOrgDTO)
    Public Titles_NAME As String
    Public Titles As List(Of ProgramTitleDTO)
    Public WI_NAME As String
    Public WIs As List(Of ProgramTitleDTO)
    Public TR_LANGUAGE_ID As Decimal?
    Public TR_LANGUAGE_NAME As String
    Public TR_UNIT_ID As Decimal?
    Public TR_UNIT_NAME As String
    Public Centers_NAME As String
    Public Centers As List(Of ProgramCenterDTO)
    Public Lectures_NAME As String
    Public Lectures As List(Of ProgramLectureDTO)
    Public CONTENT As String
    Public TARGET_TRAIN As String
    Public VENUE As String
    Public REMARK As String

    Public CREATED_BY As String
    Public CREATED_DATE As DateTime?
    Public CREATED_LOG As String
    Public MODIFIED_BY As String
    Public MODIFIED_DATE As DateTime?
    Public MODIFIED_LOG As String

    Public TR_TRAIN_ENTRIES_ID As Decimal?
    Public TR_TRAIN_ENTRIES_NAME As String
    Public CURRENCY As String
    Public Property COMMIT_WORK As Decimal?
End Class

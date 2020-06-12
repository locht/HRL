Public Class AT_LEAVESHEETDTO
    Public Property ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property VN_FULLNAME As String
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property TITLE_NAME As String
    Public Property ISTEMINAL As Boolean?
    Public Property ORG_ID As Decimal?
    Public Property BALANCE_NOW As Decimal?
    Public Property NGHIBUCONLAI As Decimal?
    Public Property NBCL As Decimal?
    Public Property WORKINGDAY As Date?
    Public Property FROM_DATE As Date?
    Public Property END_DATE As Date?
    Public Property LEAVE_FROM As Date?
    Public Property LEAVE_TO As Date?
    Public Property MANUAL_ID As Decimal?
    Public Property MANUAL_NAME As String
    Public Property MORNING_ID As Decimal?
    Public Property AFTERNOON_ID As Decimal?
    Public Property STAFF_RANK_ID As Decimal?
    Public Property STAFF_RANK_NAME As String
    Public Property MORNING_NAME As String
    Public Property AFTERNOON_NAME As String
    Public Property NOTE As String
    Public Property IS_WORKING_DAY As Boolean
    Public Property IN_PLAN_DAYS As Decimal?
    Public Property NOT_IN_PLAN_DAYS As Decimal?
    Public Property DAY_NUM As Decimal?
    Public Property EMP_APPROVES_NAME As String
    Public Property STATUS As Decimal?
    Public Property STATUS_NAME As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property NOTE_APP As String
    Public Property IMPORT As String 'Tự sinh do import bảng công
    Public Property IS_APP As Decimal?
    Public Property REASON As String
    Public Property STATUS_SHIFT As Decimal?

    'TAMBT ADD 05272020
    Public Property FROM_SESSION As Decimal?
    Public Property FROM_SESSION_NAME As String
    Public Property TO_SESSION As Decimal?
    Public Property TO_SESSION_NAME As String
    Public Property CREATED_BY_EMP As Decimal?
    Public Property CREATED_BY_EMP_NAME As String
    Public Property MODIFIED_BY_EMP As Decimal?
    Public Property MODIFIED_BY_EMP_NAME As String
    Public Property RESTORED_BY As Decimal?
    Public Property RESTORED_BY_NAME As String
    Public Property RESTORED_DATE As Date?
    Public Property RESTORED_REASON As String
    Public Property REASON_LEAVE As Decimal?
    Public Property REASON_LEAVE_NAME As String
    Public Property DAY_LIST As String
    Public Property SEARCH_EMPLOYEE As String

End Class


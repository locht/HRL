Public Class ProgramCommitDTO
    Public Property ID As Decimal
    Public Property TR_PROGRAM_ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property IS_PLAN As Boolean?
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property CONTRACT_TYPE_ID As Decimal?
    Public Property CONTRACT_TYPE_NAME As String
    Public Property BIRTH_DATE As Date?
    Public Property ID_NO As Decimal?
    Public Property ID_DATE As Date?
    Public Property GENDER_NAME As String
    Public Property lstTitle As List(Of Decimal)
    Public Property lstContractType As List(Of Decimal)

    Public Property COMMIT_NO As String
    Public Property CONVERED_TIME As Decimal?
    Public Property COMMIT_WORK As Decimal?
    Public Property COMMIT_DATE As Date?
    Public Property COMMIT_START As Date?
    Public Property COMMIT_END As Date?
    Public Property COMMIT_REMARK As String
    Public Property APPROVER_ID As Decimal?
    Public Property APPROVER_NAME As String
    Public Property APPROVER_TITLE As String

End Class

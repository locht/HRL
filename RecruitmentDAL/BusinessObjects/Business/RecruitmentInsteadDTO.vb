Public Class RecruitmentInsteadDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property REQUEST_ID As Decimal?
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property TER_LAST_DATE As Date? ' ngày nghỉ việc
End Class


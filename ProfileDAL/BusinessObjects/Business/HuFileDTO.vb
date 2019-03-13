Public Class HuFileDTO
    Public Property ID As Decimal
    Public Property TYPE_ID As Decimal?
    Public Property TYPE_NAME As String
    Public Property NAME As String
    Public Property NUMBER_CODE As String
    Public Property NOTE As String
    Public Property ADDRESS As String
    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?
    Public Property FILENAME As String
    Public Property FILENAME_SYS As String
    Public Property ACTFLG As String
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_NAME As String

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As Decimal?
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As Decimal?
End Class

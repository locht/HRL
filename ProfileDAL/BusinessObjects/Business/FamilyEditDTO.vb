Public Class FamilyEditDTO
    Public Property ID As Decimal
    Public Property FK_PKEY As Decimal?
    Public Property STATUS As String
    Public Property STATUS_NAME As String
    Public Property REASON_UNAPROVE As String
    Public Property EMPLOYEE_ID As Decimal
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property TAXTATION As String
    Public Property FULLNAME As String
    Public Property RELATION_ID As Decimal
    Public Property RELATION_NAME As String
    Public Property PROVINCE_ID As Decimal?
    Public Property PROVINCE_NAME As String
    Public Property BIRTH_DATE As Date?
    Public Property ID_NO As String
    Public Property IS_DEDUCT As Boolean?
    Public Property DEDUCT_REG As Date?
    Public Property DEDUCT_FROM As Date?
    Public Property DEDUCT_TO As Date?
    Public Property ADDRESS As String
    Public Property REMARK As String
    Public Property TITLE_ID As Decimal? 'CHUC DANH
    Public Property TITLE_NAME As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    'Public Property TITLE_NAME As String
    Public Property CAREER As String
End Class

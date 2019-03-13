Public Class AssessmentResultDTO
    Public Property ID As Decimal
    Public Property TR_CHOOSE_FORM_ID As Decimal?
    Public Property TR_PROGRAM_ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property lstResult As List(Of AssessmentResultDtlDTO)

End Class

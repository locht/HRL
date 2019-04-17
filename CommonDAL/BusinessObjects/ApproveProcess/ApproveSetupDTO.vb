
Public Class ApproveSetupDTO
    Public Property ID As Decimal
    Public Property PROCESS_ID As Decimal
    Public Property TEMPLATE_ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property ORG_ID As Decimal?

    Public Property PROCESS_NAME As String
    Public Property TEMPLATE_NAME As String

    Public Property NUM_REQUEST As Decimal?
    Public Property REQUEST_EMAIL As String

    Public Property FROM_DATE As Date
    Public Property TO_DATE As Date?
End Class

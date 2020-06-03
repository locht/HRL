Public Class AtPortalRegistrationShiftDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property TITLE_NAME As String
    Public Property ORG_NAME As String
    Public Property SHIFT_ID As Decimal?
    Public Property SHIFT_CODE As String
    Public Property SHIFT_NAME As String
    Public Property DATE_FROM As Date?
    Public Property DATE_TO As Date?
    Public Property DATE_FROM_SEARCH As Date?
    Public Property DATE_TO_SEARCH As Date?
    Public Property REASON As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As Decimal?
    Public Property CREATED_BY_NAME As String
    Public Property EMPLOYEE As List(Of Common.CommonBusiness.EmployeeDTO)
    Public Property IS_CACULATED As Decimal?
End Class

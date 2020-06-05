Public Class AtShiftRegMngDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property WORKING_DAY As Date?
    Public Property SHIFT_ID As Decimal?
    Public Property SHIFT_CODE As String
    Public Property SHIFT_NAME As String
    Public Property WEEKEND As Decimal?
    Public Property WEEKEND_CODE As String
    Public Property HOLYDAY As Decimal?
    Public Property HOLYDAY_CODE As String
    Public Property NOTE As String
    Public Property CREATED_BY As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_LOG As String
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_LOG As String
    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?
    Public Property EMP_SEARCH As String
    Public Property EMP_LST As List(Of Common.CommonBusiness.EmployeeDTO)
    Public Property IS_CHANGE As Decimal?
End Class

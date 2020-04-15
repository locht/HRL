Public Class EmployeeBackgroundDTO

    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal
    Public Property EFFECTIVE_DATE As Date?
    Public Property ID_NO As String
    Public Property LICENSE_DATE As Date?
    Public Property LICENSE_PLACE_ID As Decimal?
    Public Property LICENSE_PLACE As String

    Public Property PERMANENT_ADDRESS As String
    Public Property PERMANENT_ADDRESS_F As String
    Public Property PERMANENT_NATION_ID As Decimal?
    Public Property PERMANENT_PROVINCE_ID As Decimal?
    Public Property PERMANENT_DISTRICT_ID As Decimal?
    Public Property PERMANENT_WARD_ID As Decimal?

    Public Property PERMANENT_NATION_NAME As String
    Public Property PERMANENT_PROVINCE_NAME As String
    Public Property PERMANENT_DISTRICT_NAME As String
    Public Property PERMANENT_WARD_NAME As String

    Public Property CURRENT_ADDRESS As String
    Public Property CURRENT_ADDRESS_F As String
    Public Property CURRENT_NATION_ID As Decimal?
    Public Property CURRENT_PROVINCE_ID As Decimal?
    Public Property CURRENT_DISTRICT_ID As Decimal?
    Public Property CURRENT_WARD_ID As Decimal?

    Public Property CURRENT_NATION_NAME As String
    Public Property CURRENT_PROVINCE_NAME As String
    Public Property CURRENT_DISTRICT_NAME As String
    Public Property CURRENT_WARD_NAME As String

    Public Property MOBILE_PHONE As String
    Public Property FIXED_PHONE As String

End Class

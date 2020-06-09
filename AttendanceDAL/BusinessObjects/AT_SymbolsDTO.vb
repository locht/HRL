Public Class AT_SymbolsDTO
    Public Property ID As Decimal
    Public Property WCODE As String
    Public Property WNAME As String
    Public Property WGROUPID As Decimal?
    Public Property WGROUP_NAME As String
    Public Property WDATATYEID As Decimal?
    Public Property WDATATYE_NAME As String
    Public Property WDATAMODEID As Decimal?
    Public Property WDATAMODE_NAME As String
    Public Property EFFECT_DATE As DateTime?
    Public Property EXPIRE_DATE As DateTime?
    Public Property WINDEX As Decimal?
    Public Property NOTE As String
    Public Property STATUS As Int16?
    Public Property IS_DISPLAY As Boolean?
    Public Property IS_DISPLAY_NAME As String
    Public Property IS_DATAFROMEXCEL As Boolean?
    Public Property IS_DATAFROMEXCEL_NAME As String
    Public Property IS_DISPLAY_PORTAL As Boolean?
    Public Property IS_DISPLAY_PORTAL_NAME As String
    Public Property IS_LEAVE As Boolean?
    Public Property IS_LEAVE_NAME As String
    'Public Property SYMBOL_FUN_ID     NUMBER,
    Public Property IS_LEAVE_WEEKLY As Boolean?
    Public Property IS_LEAVE_WEEKLY_NAME As String
    Public Property IS_LEAVE_HOLIDAY As Boolean?
    Public Property IS_LEAVE_HOLIDAY_NAME As String
    Public Property IS_DAY_HALF As Boolean?
    Public Property IS_DAY_HALF_NAME As String
    Public Property CREATED_BY As String
    Public Property CREATED_DATE As DateTime?
    Public Property CREATED_LOG As String
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_DATE As DateTime?
    Public Property MODIFIED_LOG As String
End Class

Public Class AT_SHIFTDTO
    Public Property ID As Decimal
    Public Property CODE As String
    Public Property NAME_VN As String
    Public Property NAME_EN As String
    Public Property MANUAL_ID As Decimal?
    Public Property MANUAL_CODE As String
    Public Property MANUAL_NAME As String
    Public Property APPLY_LAW As Decimal?
    Public Property APPLY_LAW_NAME As String
    Public Property PENALIZEA As Decimal?
    Public Property PENALIZEA_NAME As String
    Public Property SATURDAY As Decimal?
    Public Property SATURDAY_CODE As String
    Public Property SATURDAY_NAME As String
    Public Property SUNDAY As Decimal?
    Public Property SUNDAY_CODE As String
    Public Property SUNDAY_NAME As String
    Public Property HOURS_START As Date?
    Public Property HOURS_STOP As Date?
    Public Property HOURS_STAR_CHECKIN As Date?
    Public Property HOURS_STAR_CHECKOUT As Date?
    Public Property START_MID_HOURS As Date?
    Public Property END_MID_HOURS As Date?
    Public Property NOTE As String
    Public Property ACTFLG As String
    Public Property IS_NOON As Boolean?
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property MINHOUSER As Decimal?
    'hoaivv add 01/07/2019
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    
    Public Property SHIFT_DAY As Double?
    Public Property IS_HOURS_STOP As Boolean?
    Public Property IS_MID_END As Boolean?
    Public Property IS_HOURS_CHECKOUT As Boolean?
    Public Property IS_HOURS_STOP_NAME As String
    Public Property IS_MID_END_NAME As String
    Public Property IS_HOURS_CHECKOUT_NAME As String

    Public Property MANUAL_TYPE As String
    Public Property IS_SHIFT_NIGHT As Decimal?
    Public Property IS_SHOW_IPORTAL As Decimal?
    Public Property LATE_MINUTES As Decimal?
    Public Property SOON_MINUTES As Decimal?
    Public Property VALUE_LATE As Decimal?
    Public Property VALUE_SOON As Decimal?
    Public Property STT As Decimal?
    Public Property START_CAL_SOON As Date?
    Public Property START_CAL_LATE As Date?
End Class
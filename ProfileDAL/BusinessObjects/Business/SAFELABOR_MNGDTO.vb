Public Class SAFELABOR_MNGDTO
    Public Property ID As Decimal?
    Public Property CODE As String
    Public Property NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property DATE_OCCUR As Date?
    Public Property HOUR_OCCUR As Date?
    Public Property TYPE_ACCIDENT As Decimal?
    Public Property TYPE_ACCIDENT_NAME As String
    Public Property PLACE_ACCIDENT As String
    Public Property REASON_ACCIDENT As Decimal?
    Public Property REASON_ACCIDENT_NAME As String
    Public Property REASON_DETAIL As String
    Public Property COST_ACCIDENT As Decimal?
    Public Property REMARK As String
    Public Property LST_SAFELABOR_EMP As List(Of SAFELABOR_MNG_EMPDTO)
End Class

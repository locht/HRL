Public Class OrganizationDTO
    Public Property ID As Decimal
    Public Property UNIT_LEVEL As Decimal?
    Public Property CODE As String
    Public Property NAME_VN As String
    Public Property NAME_EN As String
    Public Property PARENT_ID As Decimal?
    Public Property PARENT_NAME As String
    Public Property FOUNDATION_DATE As Date?
    Public Property DISSOLVE_DATE As Date?
    Public Property HIERARCHICAL_PATH As String
    Public Property DESCRIPTION_PATH As String
    Public Property REMARK As String
    Public Property ACTFLG As String
    Public Property ADDRESS As String
    Public Property FAX As String
    Public Property MOBILE As String
    Public Property PROVINCE_NAME As String
    Public Property COST_CENTER_CODE As String
    Public Property REPRESENTATIVE_ID As Decimal?
    Public Property REPRESENTATIVE_CODE As String
    Public Property REPRESENTATIVE_NAME As String
    Public Property ORD_NO As Decimal?
    Public Property NUMBER_BUSINESS As String
    Public Property DATE_BUSINESS As Date?
    Public Property PIT_NO As String
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property TITLE_NAME As String
    Public Property IMAGE As String
    Public Property U_INSURANCE As Nullable(Of Global.System.Decimal)
    Public Property REGION_ID As Nullable(Of Global.System.Decimal)
    Public Property ORG_LEVEL As Nullable(Of Global.System.Decimal)
    Public Property AutoGenTimeSheet As Boolean

    Public Property NUMBER_DECISION As String
    Public Property TYPE_DECISION As String
    Public Property LOCATION_WORK As String
    Public Property CHK_ORGCHART As Decimal?
    Public Property FILES As String
    Public Property EFFECT_DATE As Date?
End Class

Public Class ComboBoxDataDTO
    Public Property GET_LIST_DISTRICT As Boolean
    Public Property LIST_LIST_DISTRICT As List(Of HU_DISTRICTDTO)
    Public Property GET_LIST_PROVINCE As Boolean
    Public Property LIST_LIST_PROVINCE As List(Of HU_PROVINCEDTO)
    Public Property GET_LIST_ARISING_TYPE As Boolean
    Public Property LIST_LIST_ARISING_TYPE As List(Of OT_OTHERLIST_DTO)
    Public Property GET_LIST_CHANGETYPE As Boolean
    Public Property LIST_LIST_CHANGETYPE As List(Of INS_CHANGE_TYPEDTO)
    Public Property GET_LOCALTION As Boolean
    Public Property LIST_LOCALTION As List(Of INS_REGION_DTO)
    Public Property GET_LIST_INS_WHEREHEALTH As Boolean
    Public Property LIST_LIST_INS_WHEREHEALTH As List(Of INS_WHEREHEALTHDTO)
    Public Property GET_LIST_STATUSNOBOOK As Boolean
    Public Property LIST_LIST_STATUSNOBOOK As List(Of OT_OTHERLIST_DTO)
    Public Property GET_LIST_STATUSCARD As Boolean
    Public Property LIST_LIST_STATUSCARD As List(Of OT_OTHERLIST_DTO)
    Public Property GET_REMIGE_TYPE As Boolean
    Public Property LIST_REMIGE_TYPE As List(Of INS_ENTITLED_TYPE_DTO)
    'Danh sách ContractType
    Public Property GET_CONTRACTTYPE As Boolean
    Public Property LIST_CONTRACTTYPE As List(Of ContractTypeDTO)
    'Đơn vị đóng bảo hiểm
    Public Property GET_LIST_ORG_ID_INS As Boolean
    Public Property LIST_LIST_ORG_ID_INS As List(Of OT_OTHERLIST_DTO)
    'Nhóm chế độ bảo hiểm
    Public Property GET_LIST_GROUP_REGIMES As Boolean
    Public Property LIST_LIST_GROUP_REGIMES As List(Of INS_GROUP_REGIMESDTO)
    'Nhóm hưởng bảo hiểm
    Public Property GET_LIST_COST_LEVER As Boolean
    Public Property LIST_LIST_COST_LEVER As List(Of INS_COST_FOLLOW_LEVERDTO)
    'Danh mục chức danh
    Public Property GET_LIST_TITLE As Boolean
    Public Property LIST_LIST_TITLE As List(Of HU_TITLEDTO)
    'Danh mục cấp nhân sự
    Public Property GET_LIST_STAFF_RANK As Boolean
    Public Property LIST_LIST_STAFF_RANK As List(Of HU_STAFF_RANKDTO)


End Class

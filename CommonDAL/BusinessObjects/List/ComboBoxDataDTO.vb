Public Class ComboBoxDataDTO
    Public Property GET_MODULE As Boolean
    Public Property LIST_MODULE As List(Of ModuleDTO)

    Public Property GET_FUNCTION_GROUP As Boolean
    Public Property LIST_FUNCTION_GROUP As List(Of FunctionGroupDTO)

    Public Property GET_USER_GROUP As Boolean
    Public Property LIST_USER_GROUP As List(Of GroupDTO)

    Public Property GET_FUNCTION As Boolean
    Public Property LIST_FUNCTION As List(Of FunctionDTO)

    Public Property GET_ACTION_NAME As Boolean
    Public Property LIST_ACTION_NAME As List(Of OtherListDTO)

    Public Property GET_OTHER_LIST_GROUP As Boolean
    Public Property LIST_OTHER_LIST_GROUP As List(Of OtherListGroupDTO)

End Class

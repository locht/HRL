Public Class AT_ObjectEmpployeeCompensatoryDTO

    Public Property STT As Decimal
    Public Property ID As Decimal
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_ID As Decimal?
    Public Property FULLNAME_VN As String
    Public Property EMPLOYEE_CODE_NAME As String ' DÙNG ĐỂ TÌM KIẾM
    Public Property CREATED_DATE As DateTime
    'trạng thái làm việc
    Public Property WORK_STATUS As Decimal?
    Public Property WORK_STATUS_NAME As String

    'PHÒNG BAN
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String

    'CHỨC DANH 
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String

    ' ĐỐI TƯỢNG NHÂN VIÊN
    Public Property OBJ_EMP_ID As Decimal?
    Public Property OBJ_EMP_NAME As String

    ' ĐỐI TƯỢNG NGHỈ BÙ
    Public Property OBJ_CSL_ID As Decimal?
    Public Property OBJ_CSL_NAME As String

    ' NGẠCH LƯƠNG HSL
    Public Property SAL_LEVEL_ID As Decimal?
    Public Property SAL_LEVEL_NAME As String

    ' BẬC LƯƠNG HSL
    Public Property SAL_RANK_ID As Decimal?
    Public Property SAL_RANK_NAME As String
End Class

﻿Public Class EmployeePopupFindDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_CODE As String
    Public Property FULLNAME_VN As String
    Public Property TITLE_ID As Decimal? ' ID cap chuc danh
    Public Property TITLE_NAME As String ' Chuc danh
    Public Property STAFF_RANK_ID As Decimal? ' ID cap nhan su
    Public Property STAFF_RANK_NAME As String ' ten cap nhan su
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property CONTRACT_ID As Decimal?
    Public Property JOIN_DATE As Date?
    Public Property EFFECT_DATE As Date?
    Public Property BIRTH_DATE As Date?
    Public Property BIRTH_PLACE As Decimal?
    Public Property BIRTH_PLACE_NAME As String
    Public Property GENDER As String
    Public Property WORK_STATUS As String
    Public Property DECISION_ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property RECORD As Decimal?
    Public Property MONEY As Decimal?
    Public Property TROPHIES_NAME As String
    Public Property PER_EMAIL As String
    Public Property WORK_EMAIL As String
    Public Property HOME_PHONE As String
    Public Property MOBILE_PHONE As String
    Public Property WORK_INVOLVE As String
    Public Property WORK_INVOLVE_ID As Decimal?
    Public Property IMAGE As String
    Public Property DIRECT_MANAGER As Decimal?
    Public Property HURT_TYPE_ID As Decimal?
    Public Property HURT_TYPE_NAME As String
    Public Property OBJECTTIMEKEEPING As Decimal?
    Public Property OBJECT_LABOR As Decimal?
    Public Property OBJECT_NAME As String
    Public Property LABOUR_NAME As String
    Public Property TER_LAST_DATE As Date? ' ngày nghỉ việc
    Public Property ID_NO As String 'SO CMND
    Public Property CREATE_PLACE As String 'NOI CAP { ID:PROVINCE }
    Public Property ID_CREATE_PLACE As Decimal? '{ ID:PROVINCE }
End Class
Public Class EmployeePopupFindListDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property FULLNAME_VN As String
    Public Property FULLNAME_EN As String
    Public Property TITLE_NAME As String ' Chuc danh
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property EMP_STATUS As String 'LOAI NHAN VIEN
    Public Property IS_KIEMNHIEM As Decimal? 'LOAI NHAN VIEN
    Public Property ORG_DESC As String
    Public Property JOIN_DATE As Date?
    Public Property GENDER As String
    Public Property WORK_STATUS As String
    Public Property WORK_INVOLVE As String
    Public Property WORK_EMAIL As String
    Public Property PER_EMAIL As String
    Public Property MOBILE_PHONE As String
    Public Property HOME_PHONE As String
    Public Property IMAGE As String
    Public Property IsOnlyWorkingWithoutTer As Boolean
    Public Property IS_3B As String
    Public Property MustHaveContract As Boolean
    Public Property LoadAllOrganization As Boolean
    Public Property IS_TER As Boolean
    Public Property DIRECT_MANAGER As Decimal?

End Class

Public Class ParamDTO
    Public Property IS_DISSOLVE As Boolean?
    Public Property ORG_ID As Decimal
End Class

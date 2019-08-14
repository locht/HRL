﻿Public Class EmployeeEditDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property STATUS As String
    Public Property STATUS_NAME As String
    Public Property REASON_UNAPROVE As String
    Public Property MARITAL_STATUS As Decimal?
    Public Property MARITAL_STATUS_NAME As String
    Public Property PER_ADDRESS As String
    Public Property PER_PROVINCE As Decimal?
    Public Property PER_PROVINCE_NAME As String
    Public Property PER_WARD As Decimal?
    Public Property PER_WARD_NAME As String
    Public Property PER_DISTRICT As Decimal?
    Public Property PER_DISTRICT_NAME As String
    Public Property ID_NO As String
    Public Property ID_DATE As Date?
    Public Property ID_PLACE As String
    Public Property ID_PLACE_NAME As String
    Public Property NAV_ADDRESS As String
    Public Property NAV_PROVINCE As Decimal?
    Public Property NAV_PROVINCE_NAME As String
    Public Property NAV_DISTRICT As Decimal?
    Public Property NAV_DISTRICT_NAME As String
    Public Property NAV_WARD As Decimal?
    Public Property NAV_WARD_NAME As String

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    'NGAY HET HAN CMND
    Public Property EXPIRE_DATE_IDNO As Date?

    ' Người liên hệ 
    Public Property CONTACT_PER As String
    Public Property RELATION_PER_CTR As Decimal?
    Public Property RELATION_PER_CTR_NAME As String
    Public Property CONTACT_PER_MBPHONE As String

    ' Ấp thôn xã
    Public Property VILLAGE As String

    Public Property HOME_PHONE As String
    Public Property MOBILE_PHONE As String
    Public Property WORK_EMAIL As String
    Public Property PER_EMAIL As String

    Public Property PERSON_INHERITANCE As String

    Public Property BANK_NO As String
    Public Property BANK_ID As Decimal?
    Public Property BANK_NAME As String
    Public Property BANK_BRANCH_ID As Decimal?
    Public Property BANK_BRANCH_NAME As String
End Class

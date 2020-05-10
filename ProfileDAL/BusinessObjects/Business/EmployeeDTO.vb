﻿Public Class EmployeeDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_ID As Decimal?
    Public Property FIRST_NAME_VN As String
    Public Property LAST_NAME_VN As String
    Public Property FULLNAME_VN As String
    Public Property FIRST_NAME_EN As String
    Public Property LAST_NAME_EN As String
    Public Property BIRTH_DATE As Date?
    Public Property FULLNAME_EN As String
    Public Property ORG_ID As Decimal
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property ORG_CODE As String
    Public Property WORK_STATUS As Decimal?
    Public Property WORK_STATUS_NAME As String
    Public Property STAFF_RANK_ID As Decimal?
    Public Property STAFF_RANK_NAME As String
    Public Property ITIME_ID As String
    Public Property PA_OBJECT_SALARY_ID As Decimal?
    Public Property PA_OBJECT_SALARY_NAME As String
    Public Property IMAGE As String                 'Ảnh đại diện ( Tên ảnh thôi ghi trong database)
    Public Property IMAGE_BINARY As Byte()          'Binary của Ảnh đại diện (Dùng service đọc ảnh ra binary)
    Public Property IMAGE_URL As String
    Public Property GENDER As Decimal?
    Public Property GENDER_NAME As String
    'Contract
    Public Property CONTRACT_ID As Decimal?             'Hop dong dang hieu luc
    Public Property CONTRACT_TYPE_ID As Decimal?        'Loại hợp đồng.
    Public Property CONTRACT_TYPE_NAME As String
    Public Property CONTRACT_NO As String
    Public Property CONTRACT_EFFECT_DATE As Date?    'Ngày hiệu lực của hợp đồng hiện tại
    Public Property CONTRACT_EXPIRE_DATE As Date?    'Ngày hết hiệu lực của hợp đồng hiện tại

    Public Property TITLE_ID As Decimal?                'Chức danh
    Public Property TITLE_NAME_VN As String
    Public Property TITLE_NAME_EN As String

    Public Property TITLE_GROUP_ID As Decimal?
    Public Property TITLE_GROUP_NAME As String

    Public Property LAST_WORKING_ID As Decimal?

    Public Property JOIN_DATE As Date?
    Public Property JOIN_DATE_STATE As Date?
    Public Property DIRECT_MANAGER As Decimal?
    Public Property DIRECT_MANAGER_NAME As String
    Public Property DIRECT_MANAGER_TITLE_NAME As String  'Chức danh quản lý trực tiếp
    Public Property LEVEL_MANAGER As Decimal?
    Public Property LEVEL_MANAGER_NAME As String

    Public Property EMPLOYEE_3B_ID As String
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property ID_NO As String
    Public Property MOBILE_PHONE As String
    Public Property WORK_EMAIL As String
    Public Property IS_HISTORY As Boolean
    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?
    Public Property lstPaper As List(Of Decimal)
    Public Property lstPaperFiled As List(Of Decimal)


    Public Property IS_TER As Boolean
    Public Property TER_EFFECT_DATE As Date?
    Public Property SAL_BASIC As Decimal?
    Public Property COST_SUPPORT As Decimal?
    Public Property EFFECT_DATE As Date?
    Public Property GHI_CHU_SUC_KHOE As String
    Public Property NHOM_MAU As String

    Public Property PARENT_ID As Decimal?
    Public Property MustHaveContract As Boolean
    Public Property EMP_STATUS As Decimal?
    Public Property EMP_STATUS_NAME As String
    Public Property AUTOGENTIMESHEET As Decimal?
    Public Property EMPLOYEE_CODE_OLD As String
    Public Property BOOKNO As String
    Public Property EMPLOYEE_NAME_OTHER As String
    Public Property OBJECTTIMEKEEPING As Decimal?
    Public Property OBJECTTIMEKEEPING_NAME As String

    Public Property OBJECT_LABOR As Decimal?
    Public Property OBJECT_LABOR_NAME As String
    'DOI TUONG BAO HIEM
    Public Property OBJECT_INS As Decimal?
    Public Property OBJECT_INS_NAME As String
    'NGAY TINH THAM NIEN
    Public Property SENIORITY_DATE As Date?

    ' ĐỐI TƯỢNG NHÂN VIÊN
    Public Property EMPLOYEE_OBJECT As Decimal?
    Public Property EMPLOYEE_OBJECT_NAME As String

    ' ĐỐI TƯỢNG NHÂN VIÊN
    Public Property COMPENSATORY_OBJECT As Decimal?
    Public Property COMPENSATORY_OBJECT_NAME As String

    'MÔI TRƯỜNG ĐỘC HẠI
    Public Property IS_HAZARDOUS As Boolean
    'TẠM HOÃN HDLD
    Public Property IS_HDLD As Boolean
    ' ORG ID CAP 2 3 4 5 
    Public Property ORG_ID2 As Decimal
    Public Property ORG_NAME2 As String
    Public Property ORG_ID3 As Decimal
    Public Property ORG_NAME3 As String
    Public Property ORG_ID4 As Decimal
    Public Property ORG_NAME4 As String
    Public Property ORG_ID5 As Decimal
    Public Property ORG_NAME5 As String

    '' THONG TIN  CONG TAC
    Public Property JOB_POSITION As Decimal?
    Public Property JOB_POSITION_NAME As String
    Public Property JOB_DESCRIPTION As Decimal?
    Public Property JOB_ATTACH_FILE As String
    Public Property JOB_FILENAME As String
    Public Property ListAttachFiles As List(Of AttachFilesDTO)
    Public Property PRODUCTION_PROCESS As Decimal?
    Public Property PRODUCTION_PROCESS_NAME As String
    Public Property unit_rank_id As Decimal?
End Class

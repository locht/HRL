﻿Public Class RequestDTO
    Public Property ID As Decimal
    Public Property SEND_DATE As Date?
    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?
    Public Property IS_IN_PLAN As Boolean?
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property CONTRACT_TYPE_ID As Decimal?
    Public Property CONTRACT_TYPE_NAME As String
    Public Property TITLE_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property RC_PLAN_ID As Decimal?
    Public Property JOBGRADE_ID As Decimal?
    Public Property JOBGRADE_NAME As String
    Public Property EXPECTED_JOIN_DATE As Date?
    Public Property RECRUIT_REASON As String
    Public Property RECRUIT_REASON_ID As Decimal?
    Public Property RECRUIT_REASON_NAME As String
    Public Property LEARNING_LEVEL_ID As Decimal?
    Public Property LEARNING_LEVEL_NAME As String
    Public Property AGE_FROM As Decimal?
    Public Property AGE_TO As Decimal?
    Public Property EXPERIENCE_NUMBER As Decimal?
    Public Property MALE_NUMBER As Decimal?
    Public Property FEMALE_NUMBER As Decimal?
    Public Property DESCRIPTION As String
    Public Property REQUEST_EXPERIENCE As String
    Public Property REQUEST_OTHER As String
    Public Property REMARK As String
    Public Property STATUS_ID As Decimal?
    Public Property STATUS_NAME As String
    Public Property lstEmp As List(Of RecruitmentInsteadDTO)
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property REMARK_REJECT As String
    Public Property LANGUAGE As String
    Public Property LANGUAGELEVEL As String
    Public Property LANGUAGESCORES As Decimal?
    Public Property SPECIALSKILLS As String
    Public Property MAINTASK As String
    Public Property QUALIFICATION As String
    Public Property DESCRIPTIONATTACHFILE As String
    Public Property COMPUTER_LEVEL As String
    Public Property RC_RECRUIT_PROPERTY As Decimal?
    Public Property RC_RECRUIT_PROPERTY_NAME As String
    Public Property IS_OVER_LIMIT As Boolean?
    Public Property IS_SUPPORT As Boolean?
    Public Property FOREIGN_ABILITY As String
    Public Property COMPUTER_APP_LEVEL As String
    Public Property GENDER_PRIORITY As Decimal?
    Public Property GENDER_PRIORITY_NAME As String
    Public Property UPLOAD_FILE As String
    Public Property RECRUIT_NUMBER As Decimal?

    Public Property FILE_NAME As String
    Public Property LOCATION_ID As Decimal?
    Public Property YEAR As Decimal?


    Public Property ID_RECRUITMENT_INSTEAD As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property REQUEST_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property TITLE_ID_RECRUITMENT_INSTEAD As Decimal?
    Public Property TITLE_NAME_RECRUITMENT_INSTEAD As String
    Public Property ORG_ID_RECRUITMENT_INSTEAD As Decimal?
    Public Property ORG_NAME_RECRUITMENT_INSTEAD As String
    Public Property TER_LAST_DATE As Date? ' ngày nghỉ việc


End Class

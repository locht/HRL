﻿Public Class WorkingBeforeDTOEdit
    Public Property ID As Decimal
    Public Property COMPANY_NAME As String
    Public Property COMPANY_ADDRESS As String
    Public Property JOIN_DATE As Date?
    Public Property END_DATE As Date?
    Public Property TELEPHONE As String
    Public Property SALARY As Decimal?
    Public Property TER_REASON As String
    Public Property TITLE_NAME As String
    Public Property LEVEL_NAME As String
    Public Property EMPLOYEE_ID As Decimal

    Public Property FK_PKEY As Decimal?
    Public Property STATUS As String
    Public Property STATUS_NAME As String
    Public Property REASON_UNAPROVE As String
    Public Property SEND_DATE As Date?

    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property MAIN_JOB As String
End Class

﻿Public Class APPOINTMENT_DTO
    Public Property ID As Guid

    Public Property EMPLOYEEID As Guid
    Public Property EMPLOYEECODE As String
    Public Property EMPLOYEENAME As String

    Public Property SIGNID As Guid
    Public Property SIGNTYPE As Nullable(Of Guid)
    Public Property SIGNCODE As String
    Public Property SIGNNAME As String
    Public Property GSIGNCODE As String

    Public Property WORKINGDAY As DateTime
    Public Property NVALUE As Nullable(Of Decimal)
    Public Property NVALUE_ID As Guid?
    Public Property NVALUE_NAME As String
    Public Property SVALUE As String
    Public Property DVALUE As Nullable(Of Date)

    Public Property SUBJECT As String
    Public Property ISAP As Boolean
    Public Property NOTE As String

    Public Property NEXT1 As Nullable(Of Decimal)
    Public Property NEXT2 As Nullable(Of Decimal)
    Public Property NEXT3 As Nullable(Of Decimal)
    Public Property DEXT1 As Nullable(Of Date)
    Public Property DEXT2 As Nullable(Of Date)
    Public Property DEXT3 As Nullable(Of Date)
    Public Property SEXT1 As String
    Public Property SEXT2 As String
    Public Property SEXT3 As String

    Public Property RecurrenceRule As String
    Public Property RecurrenceParentID As String

    Public Property PLNID As Guid?

    Public Property IS_INSERT As Boolean
    Public Property IS_DELETE As Boolean
    Public Property IS_SEND_MAIL As Boolean

    Public Property STATUS As Short
End Class

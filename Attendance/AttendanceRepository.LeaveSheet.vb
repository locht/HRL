Imports Attendance.AttendanceBusiness
Imports Framework.UI
Imports Common.CommonBusiness
Partial Class AttendanceRepository
    Public Function GetLeaveSheet_ById(ByVal Leave_SheetID As Decimal, ByVal Struct As Decimal) As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetLeaveSheet_ById(Leave_SheetID, Struct)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetLeaveSheet_Detail_ByDate(ByVal employee_id As Decimal, ByVal fromDate As Date, ByVal toDate As Date, manualId As Decimal) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetLeaveSheet_Detail_ByDate(employee_id, fromDate, toDate, manualId)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function SaveLeaveSheet(ByVal dsLeaveSheet As DataSet, Optional ByRef gID As Decimal = 0) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.SaveLeaveSheet(dsLeaveSheet, Me.Log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function GetLeaveSheet_Portal(ByVal _filter As AT_LEAVESHEETDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVESHEETDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetLeaveSheet_Portal(_filter, Total, PageIndex, PageSize, Sorts, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
End Class

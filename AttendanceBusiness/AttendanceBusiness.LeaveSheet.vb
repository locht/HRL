
Imports AttendanceBusiness.ServiceContracts
Imports AttendanceDAL
Imports Framework.Data
Imports System.Collections.Generic
Namespace AttendanceBusiness.ServiceImplementations
    Partial Public Class AttendanceBusiness
        'Implements IAttendanceBusiness
        Public Function ValidateLeaveSheetDetail(ByVal objValidate As AT_LEAVESHEETDTO) As Boolean Implements IAttendanceBusiness.ValidateLeaveSheetDetail
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.ValidateLeaveSheetDetail(objValidate)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetLeaveSheet_ById(ByVal Leave_SheetID As Decimal, ByVal Struct As Decimal) As DataSet Implements IAttendanceBusiness.GetLeaveSheet_ById
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.GetLeaveSheet_ById(Leave_SheetID, Struct)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetLeaveSheet_Detail_ByDate(ByVal employee_id As Decimal, ByVal fromDate As Date, ByVal toDate As Date, manualId As Decimal) As DataTable Implements IAttendanceBusiness.GetLeaveSheet_Detail_ByDate
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.GetLeaveSheet_Detail_ByDate(employee_id, fromDate, toDate, manualId)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Function SaveLeaveSheet(ByVal dsLeaveSheet As DataSet, Optional ByVal log As UserLog = Nothing) As Boolean Implements IAttendanceBusiness.SaveLeaveSheet
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.SaveLeaveSheet(dsLeaveSheet, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function GetLeaveSheet_Portal(ByVal _filter As AT_LEAVESHEETDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVESHEETDTO) Implements IAttendanceBusiness.GetLeaveSheet_Portal
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.GetLeaveSheet_Portal(_filter, Total, PageIndex, PageSize, Sorts, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class
End Namespace

Imports Attendance.AttendanceBusiness
Imports Framework.UI

Partial Class AttendanceRepository
    Inherits AttendanceRepositoryBase
    Function GetObjEmpCompe(ByVal _filter As AT_ObjectEmpployeeCompensatoryDTO,
                                            ByVal _param As ParamDTO,
                                              Optional ByRef Total As Integer = 0,
                                              Optional ByVal PageIndex As Integer = 0,
                                              Optional ByVal PageSize As Integer = Integer.MaxValue,
                                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_ObjectEmpployeeCompensatoryDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetObjEmpCompe(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function


    Function GetOrgShiftList(ByVal strId As String) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetOrgShiftList(strId, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function InsertOrgShifT(ByVal list As List(Of AT_ORG_SHIFT_DTO)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.InsertOrgShifT(list, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function DeleteAtOrgShift(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.DeleteAtOrgShift(lstID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function Update_ObjectEandC(ByVal list As List(Of AT_ObjectEmpployeeCompensatoryDTO),
                                       ByVal objEdit As AT_ObjectEmpployeeCompensatoryDTO,
                                        ByVal code_func As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Update_ObjectEandC(list, objEdit, code_func)
            Catch ex As Exception

                Throw ex
            End Try
        End Using
    End Function
End Class

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
End Class

Imports AttendanceBusiness.ServiceContracts
Imports AttendanceDAL
Imports Framework.Data
Imports System.Collections.Generic
Imports LinqKit

Namespace AttendanceBusiness.ServiceImplementations
    Partial Public Class AttendanceBusiness
        Public Function GetObjEmpCompe(ByVal _filter As AT_ObjectEmpployeeCompensatoryDTO,
                                            ByVal _param As ParamDTO,
                                              Optional ByRef Total As Integer = 0,
                                              Optional ByVal PageIndex As Integer = 0,
                                              Optional ByVal PageSize As Integer = Integer.MaxValue,
                                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                                             Optional ByVal log As UserLog = Nothing) As List(Of AT_ObjectEmpployeeCompensatoryDTO) _
        Implements ServiceContracts.IAttendanceBusiness.GetObjEmpCompe
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetObjEmpCompe(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
    End Class
End Namespace
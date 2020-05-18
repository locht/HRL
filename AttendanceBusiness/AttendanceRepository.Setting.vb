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
        Public Function GetOrgShiftList(ByVal strId As String, Optional ByVal log As UserLog = Nothing) As DataTable _
        Implements ServiceContracts.IAttendanceBusiness.GetOrgShiftList
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetOrgShiftList(strId, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertOrgShifT(ByVal list As List(Of AT_ORG_SHIFT_DTO), Optional ByVal log As UserLog = Nothing) As Boolean _
        Implements ServiceContracts.IAttendanceBusiness.InsertOrgShifT
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertOrgShifT(list, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAtOrgShift(ByVal lstID As List(Of Decimal)) As Boolean _
        Implements ServiceContracts.IAttendanceBusiness.DeleteAtOrgShift
            Using rep As New AttendanceRepository
                Try
                    Return rep.DeleteAtOrgShift(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Update_ObjectEandC(ByVal list As List(Of AT_ObjectEmpployeeCompensatoryDTO),
                                       ByVal objEdit As AT_ObjectEmpployeeCompensatoryDTO,
                                        ByVal code_func As String) As Boolean _
Implements ServiceContracts.IAttendanceBusiness.Update_ObjectEandC
            Using rep As New AttendanceRepository
                Try
                    Return rep.Update_ObjectEandC(list, objEdit, code_func)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

    End Class
End Namespace
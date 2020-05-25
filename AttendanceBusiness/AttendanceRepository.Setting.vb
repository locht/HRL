Imports AttendanceBusiness.ServiceContracts
Imports AttendanceDAL
Imports Framework.Data
Imports System.Collections.Generic
Imports LinqKit

Namespace AttendanceBusiness.ServiceImplementations
    Partial Public Class AttendanceBusiness
#Region "at_formular"
        Public Function GetAllFomulerGroup(ByVal _filter As ATFormularDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "CFDESC ASC") As List(Of ATFormularDTO) _
            Implements ServiceContracts.IAttendanceBusiness.GetAllFomulerGroup
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetAllFomulerGroup(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveFolmulerGroup(ByVal lstID As Decimal, ByVal log As UserLog, ByVal bActive As Decimal) As Boolean _
            Implements ServiceContracts.IAttendanceBusiness.ActiveFolmulerGroup
            Using rep As New AttendanceRepository
                Try
                    Return rep.ActiveFolmulerGroup(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetListInputColumn(ByVal gID As Decimal) As DataSet _
            Implements ServiceContracts.IAttendanceBusiness.GetListInputColumn
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetListInputColumn(gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetListCalculation() As List(Of OT_OTHERLIST_DTO) _
             Implements ServiceContracts.IAttendanceBusiness.GetListCalculation
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetListCalculation()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function PRU_SYNCHFORMULAR(ByVal gID As Decimal, Optional ByVal log As UserLog = Nothing) As DataTable _
             Implements ServiceContracts.IAttendanceBusiness.PRU_SYNCHFORMULAR
            Using rep As New AttendanceRepository
                Try
                    Return rep.PRU_SYNCHFORMULAR(gID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckFomuler(ByVal sCol As String, ByVal sFormuler As String, ByVal objID As Decimal) As Boolean _
             Implements ServiceContracts.IAttendanceBusiness.CheckFomuler
            Using rep As New AttendanceRepository
                Try
                    Return rep.CheckFomuler(sCol, sFormuler, objID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function SaveFomuler(ByVal objData As ATFml_DetailDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
             Implements ServiceContracts.IAttendanceBusiness.SaveFomuler
            Using rep As New AttendanceRepository
                Try
                    Return rep.SaveFomuler(objData, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region
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
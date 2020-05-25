Imports Attendance.AttendanceBusiness
Imports Framework.UI

Partial Class AttendanceRepository
    Inherits AttendanceRepositoryBase
#Region "at_formular"
    Public Function GetAllFomulerGroup(ByVal _filter As ATFormularDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "CFDESC ASC") As List(Of ATFormularDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetAllFomulerGroup(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function ActiveFolmulerGroup(ByVal lstID As Decimal, ByVal bActive As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveFolmulerGroup(lstID, Me.Log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetListInputColumn(ByVal gID As Decimal) As System.Data.DataSet
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetListInputColumn(gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetListCalculation() As List(Of OT_OTHERLIST_DTO)
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetListCalculation()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function PRU_SYNCHFORMULAR(ByVal gID As Decimal) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.PRU_SYNCHFORMULAR(gID, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function CheckFomuler(ByVal sCol As String, ByVal sFormuler As String, ByVal objID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CheckFomuler(sCol, sFormuler, objID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function SaveFomuler(ByVal objData As ATFml_DetailDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.SaveFomuler(objData, Me.Log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
#End Region
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

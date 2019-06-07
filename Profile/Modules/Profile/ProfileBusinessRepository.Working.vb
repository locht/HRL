﻿Imports Profile.ProfileBusiness

Partial Public Class ProfileBusinessRepository
    Inherits ProfileRepositoryBase

#Region "Working"
    Public Function ApproveListChangeInfoMng(ByVal listID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ApproveListChangeInfoMng(listID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetWorking(ByVal _filter As WorkingDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ProfileBusiness.ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WorkingDTO)
        Dim lstWorking As List(Of WorkingDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstWorking = rep.GetWorking(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
                Return lstWorking
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function ValidateNewEdit(ByVal _validate As HUAllowanceDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateNewEdit(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetWorkingAllowanceFull(ByVal _filter As HUAllowanceDTO,
                                        ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "EMPLOYEE_CODE") As List(Of HUAllowanceDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetWorkingAllowance1(_filter, _param, 0, Integer.MaxValue, 0, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetWorkingAllowanceFull1(ByVal _filter As HUAllowanceDTO,
                                        ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "EMPLOYEE_CODE") As List(Of HUAllowanceDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetWorkingAllowance1(_filter, _param, 0, Integer.MaxValue, 0, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    'Public Function GetWorkingAllowance(ByVal _filter As WorkingAllowanceDTO,
    '                           ByVal PageIndex As Integer,
    '                           ByVal PageSize As Integer,
    '                           ByRef Total As Integer,
    '                           Optional ByVal Sorts As String = "EMPLOYEE_CODE") As List(Of WorkingAllowanceDTO)
    '    Dim lstWorking As List(Of WorkingAllowanceDTO)

    '    Using rep As New ProfileBusinessClient
    '        Try
    '            lstWorking = rep.GetWorkingAllowance(_filter, PageIndex, PageSize, Total, Sorts)
    '            Return lstWorking
    '        Catch ex As Exception
    '            rep.Abort()
    '            Throw ex
    '        End Try
    '    End Using
    '    Return Nothing
    'End Function
    Public Function GetWorkingAllowance1(ByVal _filter As HUAllowanceDTO,
                                        ByVal _param As ParamDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HUAllowanceDTO)
        Dim lstWorking As List(Of HUAllowanceDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstWorking = rep.GetWorkingAllowance1(_filter, _param, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lstWorking
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function ModifyWorkingAllowanceNew(ByVal objWorkingAllowance As HUAllowanceDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyWorkingAllowanceNew(objWorkingAllowance, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function InsertWorkingAllowance(ByVal objWorkingAllowance As HUAllowanceDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertWorkingAllowance(objWorkingAllowance, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function DeleteWorkingAllowance(ByVal lstWorkingAllowance As List(Of HUAllowanceDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteWorkingAllowance(lstWorkingAllowance, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ApproveWorkings(ByVal ids As List(Of Decimal)) As CommandResult
        Using rep As New ProfileBusinessClient
            Return rep.ApproveWorkings(ids, Log)
        End Using
    End Function
    Public Function GetWorking(ByVal _filter As WorkingDTO, ByVal _param As ProfileBusiness.ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WorkingDTO)
        Dim lstWorking As List(Of WorkingDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstWorking = rep.GetWorking(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
                Return lstWorking
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetWorkingAllowance(ByVal _filter As WorkingAllowanceDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer,
                               Optional ByVal Sorts As String = "EMPLOYEE_CODE") As List(Of WorkingAllowanceDTO)
        Dim lstWorking As List(Of WorkingAllowanceDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstWorking = rep.GetWorkingAllowance(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstWorking
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ModifyWorkingAllowance(ByVal objWorking As WorkingAllowanceDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyWorkingAllowance(objWorking, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetLastWorkingSalary(Optional ByVal _filter As WorkingDTO = Nothing) As WorkingDTO
        Dim obj As WorkingDTO

        Using rep As New ProfileBusinessClient
            Try
                obj = rep.GetLastWorkingSalary(_filter)
                Return obj
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ValidateWorking(ByVal sType As String, ByVal obj As WorkingDTO) As Boolean

        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateWorking(sType, obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetWorkingByID(Optional ByVal _filter As WorkingDTO = Nothing) As WorkingDTO
        Dim lstWorking As WorkingDTO

        Using rep As New ProfileBusinessClient
            Try
                lstWorking = rep.GetWorkingByID(_filter)
                Return lstWorking
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetEmployeCurrentByID(ByVal _filter As WorkingDTO) As WorkingDTO
        Dim lstWorking As WorkingDTO

        Using rep As New ProfileBusinessClient
            Try
                lstWorking = rep.GetEmployeCurrentByID(_filter)
                Return lstWorking
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertWorking(ByVal objWorking As WorkingDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertWorking(objWorking, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyWorking(ByVal objWorking As WorkingDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyWorking(objWorking, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function InsertWorking1(ByVal objWorking As WorkingDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertWorking1(objWorking, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyWorking1(ByVal objWorking As WorkingDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyWorking1(objWorking, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteWorking(ByVal objWorking As WorkingDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteWorking(objWorking)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetAllowanceByDate(ByVal _filter As WorkingAllowanceDTO) As List(Of WorkingAllowanceDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetAllowanceByDate(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetAllowanceByWorkingID(ByVal _filter As WorkingAllowanceDTO) As List(Of WorkingAllowanceDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetAllowanceByWorkingID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetChangeInfoImport(param As ProfileBusiness.ParamDTO) As DataSet
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetChangeInfoImport(param, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ImportChangeInfo(lstData As List(Of WorkingDTO),
                                     ByRef dtError As DataTable) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ImportChangeInfo(lstData, dtError, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UnApproveWorking(ByVal objWorking As WorkingDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UnApproveWorking(objWorking, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "3B"


    Public Function GetWorking3B(ByVal _filter As WorkingDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ProfileBusiness.ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WorkingDTO)
        Dim lstWorking As List(Of WorkingDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstWorking = rep.GetWorking3B(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
                Return lstWorking
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetWorking3B(ByVal _filter As WorkingDTO, ByVal _param As ProfileBusiness.ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WorkingDTO)
        Dim lstWorking As List(Of WorkingDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstWorking = rep.GetWorking3B(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
                Return lstWorking
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertWorking3B(ByVal objWorking As WorkingDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertWorking3B(objWorking, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyWorking3B(ByVal objWorking As WorkingDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyWorking3B(objWorking, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteWorking3B(ByVal objWorking As WorkingDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteWorking3B(objWorking)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

End Class

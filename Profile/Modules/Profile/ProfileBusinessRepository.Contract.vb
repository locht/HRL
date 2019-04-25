﻿Imports Profile.ProfileBusiness

Partial Public Class ProfileBusinessRepository
    Inherits ProfileRepositoryBase
#Region "evaluate"
    Public Function GetTrainingEvaluate(ByVal _filter As TrainningEvaluateDTO, ByVal PageIndex As Integer,
                              ByVal PageSize As Integer,
                              ByRef Total As Integer, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningEvaluateDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingEvaluate(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetTrainingEvaluate(ByVal _filter As TrainningEvaluateDTO, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningEvaluateDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingEvaluate(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertTrainingEvaluate(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyTrainingEvaluate(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetTrainingEvaluateByID(ByVal _filter As TrainningEvaluateDTO) As TrainningEvaluateDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingEvaluateByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function DeleteTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteTrainingEvaluate(objContract)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "traning manage"
    Public Function GetTrainingManage(ByVal _filter As TrainningManageDTO, ByVal PageIndex As Integer,
                           ByVal PageSize As Integer,
                           ByRef Total As Integer, ByVal _param As ParamDTO,
                           Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingManage(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetTrainingManage(ByVal _filter As TrainningManageDTO, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingManage(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertTrainingManage(ByVal objContract As TrainningManageDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertTrainingManage(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyTrainingManage(ByVal objContract As TrainningManageDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyTrainingManage(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetTrainingManageByID(ByVal _filter As TrainningManageDTO) As TrainningManageDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingManageByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function DeleteTrainingManage(ByVal objContract As TrainningManageDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteTrainingManage(objContract)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
#Region "trainingforeign"
    Public Function GetTrainingForeign(ByVal _filter As TrainningForeignDTO, ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningForeignDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingForeign(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetTrainingForeign(ByVal _filter As TrainningForeignDTO, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningForeignDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingForeign(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertTrainingForeign(ByVal objContract As TrainningForeignDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertTrainingForeign(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyTrainingForeign(ByVal objContract As TrainningForeignDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyTrainingForeign(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetTrainingForeignByID(ByVal _filter As TrainningForeignDTO) As TrainningForeignDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingForeignByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function DeleteTrainingForeign(ByVal objContract As TrainningForeignDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteTrainingForeign(objContract)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
    Public Function GetContract(ByVal _filter As ContractDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ContractDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContract(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetContract(ByVal _filter As ContractDTO, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ContractDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContract(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetContractByID(ByVal _filter As ContractDTO) As ContractDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContractByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ValidateContract(ByVal sType As String, ByVal obj As ContractDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateContract(sType, obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertContract(ByVal objContract As ContractDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertContract(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyContract(ByVal objContract As ContractDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyContract(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteContract(ByVal objContract As ContractDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteContract(objContract)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CreateContractNo(ByVal objContract As ContractDTO) As String
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CreateContractNo(objContract)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckContractNo(ByVal objContract As ContractDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckContractNo(objContract)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetContractEmployeeByID(ByVal gEmployeeID As Decimal) As EmployeeDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContractEmployeeByID(gEmployeeID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UnApproveContract(ByVal objContract As ContractDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UnApproveContract(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
End Class

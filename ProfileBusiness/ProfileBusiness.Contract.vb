Imports ProfileBusiness.ServiceContracts
Imports ProfileDAL
Imports Framework.Data
Imports System.ServiceModel.Activation
Imports System.Reflection
Imports System.Configuration

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness

        Public Function GetContract(ByVal _filter As ContractDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of ContractDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetContract
            Using rep As New ProfileRepository
                Try
                    Return rep.GetContract(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetContractByID(ByVal _filter As ContractDTO) As ContractDTO _
                                    Implements ServiceContracts.IProfileBusiness.GetContractByID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetContractByID(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateContract(ByVal sType As String, ByVal obj As ContractDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValidateContract
            Using rep As New ProfileRepository
                Try
                    Return rep.ValidateContract(sType, obj)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertContract(ByVal objContract As ContractDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertContract
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertContract(objContract, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyContract(ByVal objContract As ContractDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyContract
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyContract(objContract, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteContract(ByVal objContract As ContractDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteContract
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteContract(objContract)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CreateContractNo(ByVal objContract As ContractDTO) As String _
            Implements ServiceContracts.IProfileBusiness.CreateContractNo
            Using rep As New ProfileRepository
                Try
                    Return rep.CreateContractNo(objContract)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckContractNo(ByVal objContract As ContractDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.CheckContractNo
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckContractNo(objContract)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetContractEmployeeByID(ByVal gEmployeeID As Decimal) As EmployeeDTO _
            Implements ServiceContracts.IProfileBusiness.GetContractEmployeeByID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetContractEmployeeByID(gEmployeeID)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function UnApproveContract(ByVal objContract As ContractDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.UnApproveContract
            Using rep As New ProfileRepository
                Try
                    Return rep.UnApproveContract(objContract, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetCheckContractTypeID(ByVal listID As String) As DataTable _
                          Implements ServiceContracts.IProfileBusiness.GetCheckContractTypeID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetCheckContractTypeID(listID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetContractForm(ByVal formID As Decimal) As OtherListDTO Implements ServiceContracts.IProfileBusiness.GetContractForm
            Using rep As New ProfileRepository
                Try
                    Return rep.GetContractForm(formID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
    End Class
End Namespace

Imports PayrollBusiness.ServiceContracts
Imports PayrollDAL
Imports Framework.Data

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "SalaryLevelGroup"
        Public Function GetSalaryLevelGroup(ByVal _filter As SalaryLevelGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryLevelGroupDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryLevelGroup
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSalaryLevelGroup(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertSalaryLevelGroup(ByVal objSalaryLevel As SalaryLevelGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSalaryLevelGroup
            Try
                Return PayrollRepositoryStatic.Instance.InsertSalaryLevelGroup(objSalaryLevel, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateSalaryLevelGroup(ByVal objSalaryLevel As SalaryLevelGroupDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateSalaryLevelGroup
            Try
                Return PayrollRepositoryStatic.Instance.ValidateSalaryLevelGroup(objSalaryLevel)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifySalaryLevelGroup(ByVal objSalaryLevel As SalaryLevelGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySalaryLevelGroup
            Try
                Return PayrollRepositoryStatic.Instance.ModifySalaryLevelGroup(objSalaryLevel, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ActiveSalaryLevelGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.ActiveSalaryLevelGroup
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveSalaryLevelGroup(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteSalaryLevelGroup(ByVal objSalaryLevel As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSalaryLevelGroup
            Try
                Return PayrollRepositoryStatic.Instance.DeleteSalaryLevelGroup(objSalaryLevel)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
    End Class
End Namespace


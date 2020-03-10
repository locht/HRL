Imports PayrollBusiness.ServiceContracts
Imports PayrollDAL
Imports Framework.Data

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "SalaryLevelType"
        Public Function GetSalaryLevelTypeList() As List(Of SalaryLevelTypeDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryLevelTypeList
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSalaryLevelTypeList()
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertSalaryLevelType(ByVal objSalaryLevelType As SalaryLevelTypeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSalaryLevelType
            Try
                Return PayrollRepositoryStatic.Instance.InsertSalaryLevelType(objSalaryLevelType, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateSalaryLevelType(ByVal objSalaryLevelType As SalaryLevelTypeDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateSalaryLevelType
            Try
                Return PayrollRepositoryStatic.Instance.ValidateSalaryLevelType(objSalaryLevelType)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifySalaryLevelType(ByVal objSalaryLevel As SalaryLevelTypeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySalaryLevelType
            Try
                Return PayrollRepositoryStatic.Instance.ModifySalaryLevelType(objSalaryLevel, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ActiveSalaryLevelType(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.ActiveSalaryLevelType
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveSalaryLevel(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteSalaryLevelType(ByVal objSalaryLevel As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSalaryLevelType
            Try
                Return PayrollRepositoryStatic.Instance.DeleteSalaryLevel(objSalaryLevel)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
    End Class
End Namespace


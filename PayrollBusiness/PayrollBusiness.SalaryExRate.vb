Imports PayrollBusiness.ServiceContracts
Imports PayrollDAL
Imports Framework.Data

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "SalaryExRate"
        Public Function GetSalaryExRate(ByVal _filter As SalaryExRateDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryExRateDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryExRate
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSalaryExRate(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetEffectSalaryExRate() As SalaryExRateDTO Implements ServiceContracts.IPayrollBusiness.GetEffectSalaryExRate
            Try
                Return PayrollRepositoryStatic.Instance.GetEffectSalaryExRate()
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetSalaryExRateCombo(ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IPayrollBusiness.GetSalaryExRateCombo
            Using rep As New PayrollRepository
                Try

                    Dim lst = rep.GetSalaryExRateCombo(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertSalaryExRate(ByVal objSalaryExRate As SalaryExRateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSalaryExRate
            Try
                Return PayrollRepositoryStatic.Instance.InsertSalaryExRate(objSalaryExRate, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateSalaryExRate(ByVal objSalaryExRate As SalaryExRateDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateSalaryExRate
            Try
                Return PayrollRepositoryStatic.Instance.ValidateSalaryExRate(objSalaryExRate)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifySalaryExRate(ByVal objSalaryExRate As SalaryExRateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySalaryExRate
            Try
                Return PayrollRepositoryStatic.Instance.ModifySalaryExRate(objSalaryExRate, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Function ActiveSalaryExRate(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveSalaryExRate
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveSalaryExRate(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeleteSalaryExRate(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSalaryExRate
            Try
                Return PayrollRepositoryStatic.Instance.DeleteSalaryExRate(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

    End Class

End Namespace


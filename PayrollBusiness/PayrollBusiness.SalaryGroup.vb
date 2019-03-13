Imports PayrollBusiness.ServiceContracts
Imports PayrollDAL
Imports Framework.Data

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "SalaryGroup"
        Public Function GetSalaryGroup(ByVal _filter As SalaryGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryGroupDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryGroup
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSalaryGroup(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetEffectSalaryGroup() As SalaryGroupDTO Implements ServiceContracts.IPayrollBusiness.GetEffectSalaryGroup
            Try
                Return PayrollRepositoryStatic.Instance.GetEffectSalaryGroup()
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetSalaryGroupCombo(dateValue As Date, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IPayrollBusiness.GetSalaryGroupCombo
            Using rep As New PayrollRepository
                Try

                    Dim lst = rep.GetSalaryGroupCombo(dateValue, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertSalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSalaryGroup
            Try
                Return PayrollRepositoryStatic.Instance.InsertSalaryGroup(objSalaryGroup, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateSalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateSalaryGroup
            Try
                Return PayrollRepositoryStatic.Instance.ValidateSalaryGroup(objSalaryGroup)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifySalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySalaryGroup
            Try
                Return PayrollRepositoryStatic.Instance.ModifySalaryGroup(objSalaryGroup, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Function ActiveSalaryGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveSalaryGroup
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveSalaryGroup(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeleteSalaryGroup(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSalaryGroup
            Try
                Return PayrollRepositoryStatic.Instance.DeleteSalaryGroup(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

    End Class

End Namespace


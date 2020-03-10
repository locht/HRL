Imports Framework.UI
Imports Payroll.PayrollBusiness

Partial Public Class PayrollRepository

#Region "SalaryLevelType"

    Public Function GetSalaryLevelTypeList() As List(Of SalaryLevelTypeDTO)
        Using rep As New PayrollBusinessClient

            Try
                Return rep.GetSalaryLevelTypeList()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function InsertSalaryLevelType(ByVal objSalaryLevel As SalaryLevelTypeDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSalaryLevelType(objSalaryLevel, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateSalaryLevelType(ByVal objSalaryLevel As SalaryLevelTypeDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateSalaryLevelType(objSalaryLevel)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function ActiveSalaryLevelType(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveSalaryLevelType(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function ModifySalaryLevelType(ByVal objSalaryLevel As SalaryLevelTypeDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySalaryLevelType(objSalaryLevel, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteSalaryLevelType(ByVal lstSalaryLevel As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSalaryLevelType(lstSalaryLevel)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

End Class

Imports Framework.UI
Imports Payroll.PayrollBusiness

Partial Public Class PayrollRepository

#Region "SalaryLevelGroup"

    Public Function GetSalaryLevelGroup(ByVal _filter As SalaryLevelGroupDTO,
                                ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryLevelGroupDTO)
        Using rep As New PayrollBusinessClient

            Try
                Return rep.GetSalaryLevelGroup(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryLevelGroup(ByVal _filter As SalaryLevelGroupDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryLevelGroupDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryLevelGroup(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertSalaryLevelGroup(ByVal objSalaryLevel As SalaryLevelGroupDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSalaryLevelGroup(objSalaryLevel, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateSalaryLevelGroup(ByVal objSalaryLevel As SalaryLevelGroupDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateSalaryLevelGroup(objSalaryLevel)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function ActiveSalaryLevelGroup(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveSalaryLevelGroup(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function ModifySalaryLevelGroup(ByVal objSalaryLevel As SalaryLevelGroupDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySalaryLevelGroup(objSalaryLevel, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteSalaryLevelGroup(ByVal lstSalaryLevel As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSalaryLevelGroup(lstSalaryLevel)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCheckExistSalaryLevelGroup(ByVal lstSalaryLevel As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateCheckExistSalaryLevelGroup(lstSalaryLevel)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

End Class

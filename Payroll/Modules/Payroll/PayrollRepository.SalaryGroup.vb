Imports Framework.UI
Imports Payroll.PayrollBusiness

Partial Public Class PayrollRepository

#Region "SalaryGroup"

    Public Function GetSalaryGroup(ByVal _filter As SalaryGroupDTO,
                                ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SalaryGroupDTO)
        Dim lstSalaryGroup As List(Of SalaryGroupDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstSalaryGroup = rep.GetSalaryGroup(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstSalaryGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetSalaryGroup(ByVal _filter As SalaryGroupDTO,
                                       Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SalaryGroupDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryGroup(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetEffectSalaryGroup() As SalaryGroupDTO
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetEffectSalaryGroup()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryGroupCombo(dateValue As Date, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New PayrollBusinessClient
            Try
                dtData = rep.GetSalaryGroupCombo(dateValue, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertSalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSalaryGroup(objSalaryGroup, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateSalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateSalaryGroup(objSalaryGroup)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifySalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySalaryGroup(objSalaryGroup, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveSalaryGroup(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveSalaryGroup(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteSalaryGroup(ByVal lstSalaryGroup As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSalaryGroup(lstSalaryGroup)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

End Class

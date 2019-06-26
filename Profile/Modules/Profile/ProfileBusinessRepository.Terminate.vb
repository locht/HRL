Imports Profile.ProfileBusiness

Partial Public Class ProfileBusinessRepository
    Inherits ProfileRepositoryBase


#Region "Debt"
    Public Function GetDebt(ByVal empId As Decimal, ByVal PageIndex As Integer, ByVal PageSize As Integer, ByVal Total As Integer) As List(Of DebtDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetDebt(empId, PageIndex, PageSize, Total)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function InsertDebt(ByVal objDebt As DebtDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertDebt(objDebt, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyDebt(ByVal objDebt As DebtDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyDebt(objDebt, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Terminate"
    Public Function ApproveListTerminate(ByVal listID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ApproveListTerminate(listID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function CalculateTerminate(ByVal EmployeeId As Decimal, ByVal TerLateDate As Date) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Dim data As DataTable = rep.CalculateTerminate(EmployeeId, TerLateDate)
                Return data
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function GetTerminate(ByVal _filter As TerminateDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TerminateDTO)
        Dim lstTerminate As List(Of TerminateDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTerminate = rep.GetTerminate(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
                Return lstTerminate
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTerminate(ByVal _filter As TerminateDTO, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TerminateDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTerminate(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetLabourProtectByTerminate(ByVal gID As Decimal) As List(Of LabourProtectionMngDTO)

        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetLabourProtectByTerminate(gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetAssetByTerminate(ByVal gID As Decimal) As List(Of AssetMngDTO)

        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetAssetByTerminate(gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTerminateByID(ByVal _filter As TerminateDTO) As TerminateDTO

        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTerminateByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetEmployeeByID(ByVal gEmployeeID As Decimal) As EmployeeDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeByID(gEmployeeID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertTerminate(ByVal objTerminate As TerminateDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertTerminate(objTerminate, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckTerminateNo(ByVal objTerminate As TerminateDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckTerminateNo(objTerminate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyTerminate(ByVal objTerminate As TerminateDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyTerminate(objTerminate, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteTerminate(ByVal objID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteTerminate(objID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteBlackList(ByVal objID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteBlackList(objID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ApproveTerminate(ByVal objTerminate As TerminateDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ApproveTerminate(objTerminate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Insurance"

    Public Function GetTyleNV() As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTyleNV()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetSalaryNew(ByRef P_EMPLOYEEID As Integer) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetSalaryNew(P_EMPLOYEEID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Terminate3B"
    Public Function GetTerminate3B(ByVal _filter As Terminate3BDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Terminate3BDTO)
        Dim lstTerminate3B As List(Of Terminate3BDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTerminate3B = rep.GetTerminate3B(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
                Return lstTerminate3B
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTerminate3B(ByVal _filter As Terminate3BDTO, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Terminate3BDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTerminate3B(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetTerminate3BByID(ByVal _filter As Terminate3BDTO) As Terminate3BDTO

        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTerminate3BByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTerminate3bEmployeeByID(ByVal gEmployeeID As Decimal) As EmployeeDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTerminate3bEmployeeByID(gEmployeeID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertTerminate3B(ByVal objTerminate3B As Terminate3BDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertTerminate3B(objTerminate3B, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyTerminate3B(ByVal objTerminate3B As Terminate3BDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyTerminate3B(objTerminate3B, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteTerminate3B(ByVal objID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteTerminate3B(objID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    
    Public Function CheckExistApproveTerminate3B(ByVal gid As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckExistApproveTerminate3B(gid)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ApproveTerminate3B(ByVal objTerminate3B As Terminate3BDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ApproveTerminate3B(objTerminate3B)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region


End Class

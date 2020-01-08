Imports ProfileBusiness.ServiceContracts
Imports ProfileDAL
Imports Framework.Data
Imports System.ServiceModel.Activation
Imports System.Reflection
Imports System.Configuration

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness
#Region "Other"
        Public Function GetCurrentPeriod(ByVal _year As Decimal) As DataTable Implements ServiceContracts.IProfileBusiness.GetCurrentPeriod
            Using rep As New ProfileRepository
                Try

                    Return rep.GetCurrentPeriod(_year)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "Debt"
        Public Function GetDebt(ByVal empId As Decimal, ByVal PageIndex As Integer, ByVal PageSize As Integer, ByVal Total As Integer) As List(Of DebtDTO) Implements ServiceContracts.IProfileBusiness.GetDebt
            Using rep As New ProfileRepository
                Try
                    Return rep.GetDebt(empId, PageIndex, PageSize, Total)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertDebt(ByVal objDebt As DebtDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.InsertDebt
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertDebt(objDebt, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyDebt(ByVal objDebt As DebtDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyDebt
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyDebt(objDebt, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Terminate"
        Public Function Check_has_Ter(ByVal empid As Decimal) As Decimal Implements ServiceContracts.IProfileBusiness.Check_has_Ter
            Using rep As New ProfileRepository
                Try
                    Return rep.Check_has_Ter(empid)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ApproveListTerminate(ByVal listID As List(Of Decimal), ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ApproveListTerminate
            Using rep As New ProfileRepository
                Try
                    Return rep.ApproveListTerminate(listID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CalculateTerminate(ByVal EmployeeId As Decimal, ByVal TerLateDate As Date) As DataTable Implements ServiceContracts.IProfileBusiness.CalculateTerminate
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.CalculateTerminate(EmployeeId, TerLateDate)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetLabourProtectByTerminate(ByVal gID As Decimal) As List(Of LabourProtectionMngDTO) _
            Implements ServiceContracts.IProfileBusiness.GetLabourProtectByTerminate
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetLabourProtectByTerminate(gID)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAssetByTerminate(ByVal gID As Decimal) As List(Of AssetMngDTO) _
            Implements ServiceContracts.IProfileBusiness.GetAssetByTerminate
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetAssetByTerminate(gID)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTerminate(ByVal _filter As TerminateDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of TerminateDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetTerminate
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetTerminate(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTerminateByID(ByVal _filter As TerminateDTO) As TerminateDTO Implements ServiceContracts.IProfileBusiness.GetTerminateByID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetTerminateByID(_filter)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeeByID(ByVal gEmployeeID As Decimal) As EmployeeDTO Implements ServiceContracts.IProfileBusiness.GetEmployeeByID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetEmployeeByID(gEmployeeID)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertTerminate(ByVal objTerminate As TerminateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertTerminate
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertTerminate(objTerminate, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyTerminate(ByVal objTerminate As TerminateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyTerminate
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyTerminate(objTerminate, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteTerminate(ByVal objID As Decimal, log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteTerminate
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteTerminate(objID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Function DeleteBlackList(ByVal objID As Decimal, log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteBlackList
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteBlackList(objID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ApproveTerminate(ByVal objTerminate As TerminateDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ApproveTerminate
            Using rep As New ProfileRepository
                Try

                    Return rep.ApproveTerminate(objTerminate, Nothing)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckTerminateNo(ByVal objTerminate As TerminateDTO) As Boolean _
           Implements ServiceContracts.IProfileBusiness.CheckTerminateNo
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckTerminateNo(objTerminate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Insurance"
        Public Function GetTyleNV() As DataTable Implements ServiceContracts.IProfileBusiness.GetTyleNV
            Using rep As New ProfileRepository
                Try
                    Return rep.GetTyleNV()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetSalaryNew(ByRef P_EMPLOYEEID As Integer) As DataTable Implements ServiceContracts.IProfileBusiness.GetSalaryNew
            Using rep As New ProfileRepository
                Try
                    Return rep.GetSalaryNew(P_EMPLOYEEID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Terminate3b"
        Public Function GetTerminate3b(ByVal _filter As Terminate3BDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of Terminate3BDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetTerminate3b
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetTerminate3b(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTerminate3bByID(ByVal _filter As Terminate3BDTO) As Terminate3BDTO Implements ServiceContracts.IProfileBusiness.GetTerminate3bByID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetTerminate3bByID(_filter)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTerminate3bEmployeeByID(ByVal gEmployeeID As Decimal) As EmployeeDTO _
            Implements ServiceContracts.IProfileBusiness.GetTerminate3bEmployeeByID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetTerminate3bEmployeeByID(gEmployeeID)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertTerminate3b(ByVal objTerminate3b As Terminate3BDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertTerminate3b
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertTerminate3b(objTerminate3b, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyTerminate3b(ByVal objTerminate3b As Terminate3BDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyTerminate3b
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyTerminate3b(objTerminate3b, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckExistApproveTerminate3b(ByVal gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.CheckExistApproveTerminate3b
            Using rep As New ProfileRepository
                Try

                    Return rep.CheckExistApproveTerminate3b(gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteTerminate3b(ByVal objID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteTerminate3b
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteTerminate3b(objID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ApproveTerminate3b(ByVal objTerminate3b As Terminate3BDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ApproveTerminate3b
            Using rep As New ProfileRepository
                Try

                    Return rep.ApproveTerminate3b(objTerminate3b)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

    End Class
End Namespace
Imports ProfileBusiness.ServiceContracts
Imports ProfileDAL
Imports Framework.Data
Imports System.ServiceModel.Activation
Imports System.Reflection
Imports System.Configuration

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness

#Region "WelfareMng"
        Function GetWelfareListAuto(ByVal _filter As WelfareMngDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       ByVal log As UserLog) As DataTable Implements ServiceContracts.IProfileBusiness.GetWelfareListAuto
            Try
                Using rep As New ProfileRepository
                    Dim dt = rep.GetWelfareListAuto(_filter, PageIndex, PageSize, Total, log)
                    Return dt
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetWelfareMng(ByVal _filter As WelfareMngDTO, ByVal IsDissolve As Integer, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal UserLog As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WelfareMngDTO) Implements ServiceContracts.IProfileBusiness.GetWelfareMng
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetWelfareMng(_filter, IsDissolve, PageIndex, PageSize, Total, UserLog, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetWelfareMngById(ByVal Id As Integer
                                        ) As WelfareMngDTO Implements ServiceContracts.IProfileBusiness.GetWelfareMngById
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetWelfareMngById(Id)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckWelfareMngEffect(ByVal _filter As List(Of WelfareMngDTO)) As Boolean Implements ServiceContracts.IProfileBusiness.CheckWelfareMngEffect
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.CheckWelfareMngEffect(_filter)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertWelfareMng(ByVal lstWelfareMng As List(Of WelfareMngDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.InsertWelfareMng
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertWelfareMng(lstWelfareMng, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyWelfareMng(ByVal lstWelfareMng As List(Of WelfareMngDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyWelfareMng
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyWelfareMng(lstWelfareMng, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveWelfareMng(ByVal objWelfareMng() As WelfareMngDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteWelfareMng
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteWelfareMng(objWelfareMng, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

    End Class
End Namespace

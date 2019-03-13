Imports ProfileBusiness.ServiceContracts
Imports ProfileDAL
Imports Framework.Data
Imports System.ServiceModel.Activation
Imports System.Reflection
Imports System.Configuration

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness
#Region "Competency Course"

        Public Function GetCompetencyCourse(ByVal _filter As CompetencyCourseDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyCourseDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetCompetencyCourse
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetCompetencyCourse(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertCompetencyCourse(ByVal objCompetencyCourse As CompetencyCourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertCompetencyCourse
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertCompetencyCourse(objCompetencyCourse, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyCompetencyCourse(ByVal objCompetencyCourse As CompetencyCourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyCompetencyCourse
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyCompetencyCourse(objCompetencyCourse, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteCompetencyCourse(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteCompetencyCourse
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteCompetencyCourse(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region


    End Class
End Namespace
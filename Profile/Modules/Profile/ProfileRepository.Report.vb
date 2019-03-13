Imports Framework.UI

Imports Profile.ProfileBusiness

Partial Public Class ProfileRepository


    Public Function GetReportList(ByVal _filter As WorkingDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer, ByVal _param As ParamDTO,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WorkingDTO)
        Dim lstWorking As List(Of WorkingDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstWorking = rep.GetWorking(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
                Return lstWorking
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
End Class

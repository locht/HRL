Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Profile
Imports Framework.UI
Imports Common

Partial Class AttendanceStoreProcedure
    Dim hfr As New HistaffFrameworkRepository
    Public Function CHECK_KIEM_NHIEM(ByVal emp_code As String) As Int32
        Try
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_AT_LIST.CHECK_KIEM_NHIEM", New List(Of Object)(New Object() {emp_code, OUT_NUMBER}))
            Return Int32.Parse(obj(0).ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

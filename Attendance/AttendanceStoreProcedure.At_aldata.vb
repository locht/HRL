Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Profile
Imports Framework.UI
Imports Common

Partial Class AttendanceStoreProcedure
    Dim hfr As New HistaffFrameworkRepository
    Public Function GET_INFO_NGHIBU(ByVal id As Decimal, ByVal fromDate As Date) As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_AT_LIST.GET_INFO_NGHIBU",
                                           New List(Of Object)(New Object() {id, fromDate, OUT_CURSOR}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

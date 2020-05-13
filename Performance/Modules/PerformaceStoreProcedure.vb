Imports Common
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Public Class PerformaceStoreProcedure
    Private hfr As New HistaffFrameworkRepository
    Public Function Import_KPI(ByVal P_USER As String, ByVal P_DOCXML As String) As Boolean
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_BUSINESS.Import_KPI", New List(Of Object)(New Object() {P_USER, P_DOCXML}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return CBool(ds.Tables(0)(0)(0))
        End If
    End Function
End Class

Imports System.Web.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Telerik.Web.UI
Imports System.Reflection
Imports Common.CommonBusiness

Partial Class AttendanceStoreProcedure
    Public Function GET_ORGID(ByVal EMPID As Integer) As Int32
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_AT_ATTENDANCE_PORTAL.GET_ORGID", New List(Of Object)(New Object() {EMPID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
    Public Function GET_PERIOD(ByVal DATE_CURRENT As Date) As Int32
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_AT.GET_PERIOD", New List(Of Object)(New Object() {DATE_CURRENT, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
End Class

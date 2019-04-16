Imports System.Web.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Telerik.Web.UI
Imports System.Reflection
Imports Common.CommonBusiness

Partial Class AttendanceStoreProcedure
    Public Function CHECK_SEND_APPROVE(ByVal listID As String) As DataTable
        Dim dt As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.CHECK_SEND_APPROVE", New List(Of Object)(New Object() {listID}))
        'Return ds
        If ds IsNot Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    'APPROVE BY PROCESS
    Public Function GET_APPROVE_STATUS(ByVal ID As Decimal, ByVal P_PROCESS_CODE As String) As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_PROCESS.GET_APPROVE_STATUS", New List(Of Object)(New Object() {ID, P_PROCESS_CODE}))
        If ds IsNot Nothing Then
            Return ds.Tables(0)
        End If
    End Function
    Public Function APPROVE_REG(ByVal employee As Decimal, ByVal employee_app As Decimal, ByVal period_id As Decimal, ByVal status_id As Decimal, ByVal P_PROCESS_CODE As String, ByVal P_NOTES As String, ByVal P_ID_REGGROUP As Decimal) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_AT_PROCESS.PRI_PROCESS", New List(Of Object)(New Object() {employee, employee_app, period_id, status_id, P_PROCESS_CODE, P_NOTES, P_ID_REGGROUP, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
End Class

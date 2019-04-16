Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Profile
Imports Framework.UI
Imports Common

Partial Class AttendanceStoreProcedure
    Dim hfr As New HistaffFrameworkRepository
    Public Function AT_CHECK_ORG_PERIOD_STATUS_OT(ByVal LISTORG As String, ByVal PERIOD As Decimal) As Int32
        Dim objects = hfr.ExecuteStoreScalar("PKG_AT.AT_CHECK_ORG_PERIOD_STATUS_OT", New List(Of Object)(New Object() {LISTORG, PERIOD, OUT_NUMBER}))
        Return Int32.Parse(objects(0).ToString())
    End Function
    Public Function GET_LIST_HOURS() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT.GET_LIST_HOURS", New List(Of Object)(New Object() {}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_LIST_MINUTE() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT.GET_LIST_MINUTE", New List(Of Object)(New Object() {}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function PRI_PROCESS_APP(employee_id As Decimal, period_id As Integer, process_type As String, totalHours As Decimal, totalDay As Decimal, sign_id As Integer, id_reggroup As Integer) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_AT_PROCESS.PRI_PROCESS_APP", New List(Of Object)(New Object() {employee_id, period_id, process_type, totalHours, totalDay, sign_id, id_reggroup, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
    ' lấy seq đăng ký portal
    Public Function GET_SEQ_PORTAL_RGT() As Decimal
        Dim objects = hfr.ExecuteStoreScalar("PKG_AT_ATTENDANCE_PORTAL.GET_SEQ_PORTAL_RGT", New List(Of Object)(New Object() {OUT_NUMBER}))
        Return Decimal.Parse(objects(0).ToString())
    End Function
    ' Lấy tổng số giờ OT phê duyệt và số giờ OT đăng ký chưa phê duyệt
    Public Function GET_TOTAL_OT_APPROVE(ByVal EMPID As Decimal?, ByVal ENDDATE As Date) As Decimal
        Dim objects = hfr.ExecuteStoreScalar("PKG_AT.GET_TOTAL_OT_APPROVE", New List(Of Object)(New Object() {EMPID, ENDDATE, OUT_NUMBER}))
        Return Decimal.Parse(objects(0).ToString())
    End Function
End Class

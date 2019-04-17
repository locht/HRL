Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Attendance.AttendanceBusiness

Partial Class AttendanceStoreProcedure
    Private rep As New HistaffFrameworkRepository

    ' Xóa đăng ký nghỉ
    Public Function AT_DELETE_PORTAL_REG(ByVal LSTID As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_AT_ATTENDANCE_PORTAL.AT_DELETE_PORTAL_REG", _
                                                   New List(Of Object)(New Object() {LSTID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
    'Lay danh sach dang ky tren portal
    Public Function GET_REG_PORTAL(ByVal EMPLOYEEID As Decimal?, ByVal START_DATE As Date?, ByVal END_DATE As Date?, ByVal LSTSTATUS As String, ByVal TYPE As String) As List(Of APPOINTMENT_DTO)
        Dim dt As New DataTable
        Dim lst As New List(Of APPOINTMENT_DTO)
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.GET_REG_PORTAL", New List(Of Object)(New Object() {EMPLOYEEID, START_DATE, END_DATE, LSTSTATUS, TYPE}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim itm As New APPOINTMENT_DTO
                itm.ID = dr("ID")
                itm.EMPLOYEEID = dr("EMPLOYEE_ID")
                itm.SIGNID = dr("SIGN_ID").ToString
                itm.SIGNCODE = dr("SIGN_CODE").ToString
                itm.SIGNNAME = dr("SIGN_NAME").ToString
                itm.GSIGNCODE = dr("GSIGN_CODE").ToString
                itm.SUBJECT = dr("SUBJECT").ToString
                If dr("FROM_HOUR").ToString <> "" Then
                    itm.FROMHOUR = dr("FROM_HOUR").ToString
                End If
                If dr("TO_HOUR").ToString <> "" Then
                    itm.TOHOUR = dr("TO_HOUR").ToString
                End If
                If dr("WORKING_DAY").ToString <> "" Then
                    itm.WORKINGDAY = dr("WORKING_DAY").ToString
                End If
                itm.NVALUE = dr("NVALUE").ToString
                itm.NVALUE_NAME = dr("NVALUE_NAME").ToString
                itm.STATUS = dr("STATUS").ToString
                itm.NOTE = dr("NOTE").ToString
                If dr("JOIN_DATE").ToString <> "" Then
                    itm.JOINDATE = Convert.ToDateTime(dr("JOIN_DATE"))
                End If

                lst.Add(itm)
            Next
        End If
        Return lst
    End Function
End Class

﻿Imports System.Web.UI
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
    Public Function INSERT_ID_BY_DELETE_SIGN_WORK(ByVal listID As String) As DataTable
        Dim dt As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.INSERT_ID_SIGN_WORK_BY_DELETE", New List(Of Object)(New Object() {listID}))
        'Return ds
        'If ds IsNot Nothing Then
        '    dt = ds.Tables(0)
        'End If
        Return dt
    End Function
    Public Function SELECT_ID_BY_DELETE_SIGN_WORK(ByVal Index As Integer) As DataTable
        Dim dt As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.SELECT_ID_BY_DELETE_SIGN_WORK", New List(Of Object)(New Object() {Index}))

        If ds IsNot Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function CHECK_DELETE_SIGNWORK(ByVal listID As String) As DataTable
        Dim dt As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.CHECK_DELETE_SIGNWORK", New List(Of Object)(New Object() {listID}))
        'Return ds
        If ds IsNot Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function CHECK_APPROVAL(ByVal listID As String) As DataTable
        Dim dt As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.CHECK_APPROVAL", New List(Of Object)(New Object() {listID}))
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
    Public Function GET_PERIOD_BYDATE(ByVal P_DATE As Date?) As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.GET_PERIOD_BYDATE", New List(Of Object)(New Object() {P_DATE}))
        If ds IsNot Nothing Then
            Return ds.Tables(0)
        End If
    End Function

    Public Function CHECK_EXIST_SHIFT(ByVal p_EMP_ID As Decimal, ByVal P_DATE_FROM As Date?, ByVal P_DATE_TO As Date?) As Decimal
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.CHECK_EXIST_SHIFT", New List(Of Object)(New Object() {p_EMP_ID, P_DATE_FROM, P_DATE_TO}))
        If ds IsNot Nothing Then
            Return ds.Tables(0).Rows(0)(0)
        End If
    End Function
End Class

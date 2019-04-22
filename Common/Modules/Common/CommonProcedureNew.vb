﻿Imports System.Web.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Telerik.Web.UI
Imports System.Reflection
Imports Common.CommonBusiness

Public Class CommonProcedureNew
    Private rep As New HistaffFrameworkRepository

    Function IMPORT_APPROVE_SETUP_ORG(DocXml As String, User As UserLog) As Boolean
        Try
            Dim objects = rep.ExecuteStoreScalar("PKG_COMMON_LIST.IMPORT_APPROVE_SETUP_ORG",
                                            New List(Of Object)(New Object() {DocXml, User.Username, User.Ip + "/" + User.ComputerName}))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function GET_ALL_APPROVE_SETUP_ORG() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_COMMON_LIST.GET_ALL_APPROVE_SETUP_ORG", _
                                                   New List(Of Object)(New Object() {}))
        If ds IsNot Nothing Then
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function

    Public Function GetSignLeaveList() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_LIST.GET_SIGN_LEAVE_LIST",
                                                 New List(Of Object)(New Object() {}))
        If ds IsNot Nothing Then
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function

    Public Function GET_SIGN_ID(ByVal signID As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HU_IPROFILE_EMPLOYEE.GET_SIGN_ID",
                                                 New List(Of Object)(New Object() {signID}))
        If ds IsNot Nothing Then
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function

    Public Function IMPORT_APPROVE(ByVal template_code As String, ByVal template_name As String, _
                                   ByVal template_type As Integer, ByVal app_level As Integer, ByVal app_type As String,
                                   ByVal user As UserLog) As Boolean
        Try
            Dim objects = rep.ExecuteStoreScalar("PKG_COMMON_LIST.IMPORT_APPROVE",
                                            New List(Of Object)(New Object() {template_code.Trim(), template_name.Trim(), template_type, app_level, app_type, user.Username, user.Ip + "/" + user.ComputerName}))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GET_ALL_APPROVE() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_COMMON_LIST.GET_ALL_APPROVE", _
                                                   New List(Of Object)(New Object() {}))
        If ds IsNot Nothing Then
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function

    ''' <summary>
    ''' Lấy danh sách dữ liệu từ table, danh mục dùng chung
    ''' </summary>
    ''' <param name="TABLE_NAME"></param>
    ''' <param name="COL_VALUE"></param>
    ''' <param name="COL_DISPLAY"></param>
    ''' <param name="WHERE"></param>
    ''' <param name="COL_SORT"></param>
    ''' <param name="DESC"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GET_COMBOBOX(ByVal TABLE_NAME As String, ByVal COL_VALUE As String, ByVal COL_DISPLAY As String, _
                                    ByVal WHERE As String, ByVal COL_SORT As String, ByVal DESC As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_COMMON_LIST.GET_LIST_COMBOBOX", _
                                                 New List(Of Object)(New Object() {TABLE_NAME, COL_VALUE, COL_DISPLAY, WHERE, COL_SORT, DESC}))
        If ds IsNot Nothing Then
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function

    'Excute SQL 
    Public Function GET_DATA_BY_EXCUTE(ByVal p_sql As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_COMMON_LIST.GET_DATA_BY_EXCUTE", New List(Of Object)(New Object() {p_sql}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function UPDATE_DATA_BY_EXCUTE(ByVal p_sql As String) As Integer
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_COMMON_LIST.UPDATE_DATA_BY_EXCUTE", _
                                                   New List(Of Object)(New Object() {p_sql, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function GET_LOG_CACULATOR(ByVal requestID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.PRO_GET_LOG_FILE_NEW", _
                                                   New List(Of Object)(New Object() {requestID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
End Class

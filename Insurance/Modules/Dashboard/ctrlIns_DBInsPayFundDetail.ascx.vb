﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Telerik.Charting
Imports WebAppLog

Public Class ctrlIns_DBInsPayFundDetail
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Insurance/Module/Insurance/Dashboard/" + Me.GetType().Name.ToString()
#Region "Property"
    ''' <summary>
    ''' StatisticType
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StatisticType As String
        Get
            Return Session(Me.ID & "_StatisticType")
        End Get
        Set(ByVal value As String)
            Session(Me.ID & "_StatisticType") = value
        End Set
    End Property
    ''' <summary>
    ''' StatisticData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StatisticData As List(Of StatisticDTO)
        Get
            Return Session(Me.ID & "_StatisticData")
        End Get
        Set(ByVal value As List(Of StatisticDTO))
            Session(Me.ID & "_StatisticData") = value
        End Set
    End Property
#End Region

#Region "Page"
    Private width As Integer
    Private height As Integer
    Public name As String = "Tỷ lệ"
    Public data As String ''{name: 'Microsoft Internet Explorer', y: 56.33},\n
    Public _year As Integer = Date.Now.Year
    ''' <summary>
    ''' ViewLoad
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Request("width") IsNot Nothing Then
                width = CInt(Request("width"))
            End If
            If Request("height") IsNot Nothing Then
                height = CInt(Request("height"))
            End If
            Dim resize As Integer = 0
            If Request("resize") IsNot Nothing Then
                resize = CInt(Request("resize"))
            End If
            If Not IsPostBack Then
                Refresh(CommonMessage.ACTION_UPDATED)
            Else
                Refresh()
            End If
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Bind dữ liệu cho các combobox cboYear, cboMonth
    ''' Lấy danh sách các tổ chức bind lên cây thư mục
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Try
                LoadYear()
                LoadMonth()
                cboYear.SelectedValue = Date.Now.Year
                If Date.Now.Month = 1 Then
                    cboYear.SelectedValue = Date.Now.Year - 1
                    cboMonth.SelectedValue = 12
                Else
                    cboMonth.SelectedValue = Date.Now.Month - 1
                End If
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
                Throw ex
            End Try
            If Common.Common.OrganizationLocationDataSession Is Nothing Then
                Dim rep1 As New CommonRepository
                rep1.GetOrganizationLocationTreeView()
            End If
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Làm mới dữ liệu, thiết lập cho các thành phần trên trang
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If width > 1000 Then
                btnMax.Text = "Restore"
            Else
                btnMax.Text = "Maximize"
            End If
            data = String.Empty
            If StatisticData Is Nothing Or Message = CommonMessage.ACTION_UPDATED Then
                StatisticData = rep.GetStatisticInsPayFundDetail(Utilities.ObjToInt(cboYear.SelectedValue), Utilities.ObjToInt(cboMonth.SelectedValue))
            End If
            If (From p In StatisticData Where p.VALUE <> 0).Any Then
                For index As Integer = 0 To StatisticData.Count - 1
                    If index = StatisticData.Count - 1 Then
                        data &= "{name: '" & StatisticData(index).NAME & "', y: " & StatisticData(index).VALUE & "}"
                    Else
                        data &= "{name: '" & StatisticData(index).NAME & "', y: " & StatisticData(index).VALUE & "}," & vbNewLine
                    End If
                Next
            End If
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' Xử lý sự kiện TextChanged cho combobox cboYear
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cboYear_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboYear.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            LoadMonth()
            Refresh(CommonMessage.ACTION_UPDATED)
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Xử lý sự kiện SelectedIndexChanged cho control cboMonth
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cboMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboMonth.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Refresh(CommonMessage.ACTION_UPDATED)
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"
    ''' <summary>
    ''' Load dữ liệu cho control cboYear
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadYear()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2015 To Date.Now.Year + 1
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, table, "YEAR", "ID")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' load dữ liệu cho control cboMonth
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadMonth()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim tableMonth As New DataTable
            tableMonth.Columns.Add("MONTH", GetType(String))
            tableMonth.Columns.Add("ID", GetType(String))
            Dim rowMonth As DataRow
            For index = 1 To 13
                rowMonth = tableMonth.NewRow

                If index = 0 Then
                    rowMonth("ID") = 0
                    rowMonth("MONTH") = ""
                    tableMonth.Rows.Add(rowMonth)
                ElseIf index = 13 Then
                    rowMonth("ID") = index
                    rowMonth("MONTH") = "Tất cả"
                    tableMonth.Rows.Add(rowMonth)
                Else
                    rowMonth("ID") = index
                    rowMonth("MONTH") = index
                    tableMonth.Rows.Add(rowMonth)
                End If
            Next
            FillRadCombobox(cboMonth, tableMonth, "MONTH", "ID")

        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
        
    End Sub
#End Region
End Class
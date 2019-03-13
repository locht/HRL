﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI
Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Telerik.Charting

Public Class ctrlPE_ViewRating
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"
    Public Property StatisticType As String
        Get
            Return Session(Me.ID & "_StatisticType")
        End Get
        Set(ByVal value As String)
            Session(Me.ID & "_StatisticType") = value
        End Set
    End Property
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
    Public name As String = "Nhân viên"
    Public data As String ''{name: 'Microsoft Internet Explorer', y: 56.33},\n
    Public _year As Integer = Date.Now.Year
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
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
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
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
            txtYear.Value = Date.Now.Year
            LoadTypeAss()
            Refresh(CommonMessage.ACTION_UPDATED)
            If Common.Common.OrganizationLocationDataSession Is Nothing Then
                Dim rep1 As New CommonRepository
                rep1.GetOrganizationLocationTreeView()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New PerformanceRepository
        Try
            If width > 1000 Then
                btnMax.Text = "Restore"
            Else
                btnMax.Text = "Maximize"
            End If
            data = String.Empty
            If StatisticData Is Nothing Or Message = CommonMessage.ACTION_UPDATED Then
                StatisticData = rep.GetStatisticKPIByClassification(Utilities.ObjToInt(cboPeriod.SelectedValue))
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
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub txtYear_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtYear.TextChanged
        LoadPeriod()
    End Sub
    Protected Sub cboTypeAss_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTypeAss.SelectedIndexChanged
        cboPeriod.ClearValue()
        LoadTypeAss()
    End Sub
    Protected Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriod.SelectedIndexChanged
        Refresh(CommonMessage.ACTION_UPDATED)
    End Sub
#End Region

#Region "Custom"
    Public Sub LoadTypeAss()
        Try
            Using rep As New PerformanceRepository
                'Kieu danh gia
                Dim dtData = rep.GetOtherList("TYPE_ASS", True)
                FillRadCombobox(cboTypeAss, dtData, "NAME", "ID")
                If (dtData IsNot Nothing) Then
                    cboTypeAss.SelectedValue = dtData(0)("ID")
                    LoadPeriod()
                End If
            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub LoadPeriod()
        Dim rep As New PerformanceRepository
        Dim Periods As List(Of PeriodDTO)
        Dim _filter As New PeriodDTO
        _filter.TYPE_ASS = Utilities.ObjToInt(cboTypeAss.SelectedValue)
        _filter.YEAR = Utilities.ObjToInt(txtYear.Value)
        Periods = rep.GetPeriod(_filter)
        FillRadCombobox(cboPeriod, Periods.ToTable(), "NAME", "ID")
        If (Periods IsNot Nothing And Periods.Count > 0) Then
            cboPeriod.SelectedValue = Periods(0).ID
        End If

    End Sub
#End Region
End Class
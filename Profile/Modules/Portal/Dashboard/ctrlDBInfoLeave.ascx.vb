Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Telerik.Charting

Public Class ctrlDBInfoLeave
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
    Public Property StatisticList As List(Of OtherListDTO)
        Get
            Return Session(Me.ID & "_StatisticList")
        End Get
        Set(ByVal value As List(Of OtherListDTO))
            Session(Me.ID & "_StatisticList") = value
        End Set
    End Property
    Public Property EmployeeID As Decimal
#End Region

#Region "Page"
    Private width As Integer
    Private height As Integer
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
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileDashboardRepository
        Try
            Dim _filter As New TotalDayOffDTO
            _filter.DATE_REGISTER = Date.Now
            _filter.LEAVE_TYPE = 251
            _filter.EMPLOYEE_ID = Session("_EmployeeID")
            Dim obj As New TotalDayOffDTO
            obj = rep.GetTotalDayOff(_filter)
            If obj IsNot Nothing Then
                If obj.TOTAL_DAY IsNot Nothing Then
                    lblNgayDuocNghi.Text = If(obj.TOTAL_DAY = 0, 0, Decimal.Parse(obj.TOTAL_DAY).ToString())
                Else
                    lblNgayDuocNghi.Text = Decimal.Parse(0).ToString()
                End If
                If obj.REST_DAY IsNot Nothing Then
                    lblConLai.Text = If(obj.REST_DAY = 0, 0, Decimal.Parse(obj.REST_DAY).ToString())
                Else
                    lblConLai.Text = Decimal.Parse(0).ToString()
                End If
            Else
                lblNgayDuocNghi.Text = Decimal.Parse(0).ToString()
                lblConLai.Text = Decimal.Parse(0).ToString()
            End If

            _filter.LEAVE_TYPE = 255
            obj = rep.GetTotalDayOff(_filter)
            If obj IsNot Nothing Then
                If obj.TOTAL_DAY IsNot Nothing Then
                    lblNgayDuocNghiB.Text = If(obj.TOTAL_DAY = 0, 0, Decimal.Parse(obj.TOTAL_DAY).ToString())
                Else
                    lblNgayDuocNghiB.Text = Decimal.Parse(0).ToString()
                End If
                If obj.REST_DAY IsNot Nothing Then
                    lblConLaiB.Text = If(obj.REST_DAY = 0, 0, Decimal.Parse(obj.REST_DAY).ToString())
                Else
                    lblConLaiB.Text = Decimal.Parse(0).ToString()
                End If
            Else
                lblNgayDuocNghiB.Text = Decimal.Parse(0).ToString()
                lblConLaiB.Text = Decimal.Parse(0).ToString()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"

#End Region

#Region "Custom"
#End Region
End Class
Imports Profile.ProfileBusiness
Public Class ProfileDashboardRepository
    Inherits ProfileRepositoryBase
#Region "Profile dashboard"

    Public Function GetEmployeeStatistic(ByVal _type As String) As List(Of StatisticDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeStatistic(_type, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetListEmployeeStatistic() As List(Of OtherListDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListEmployeeStatistic()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetListChangeStatistic() As List(Of OtherListDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListChangeStatistic()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetChangeStatistic(ByVal _type As String) As List(Of StatisticDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetChangeStatistic(_type, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#Region "Reminder"
    Public Function GetRemind(ByVal _dayRemind As String) As List(Of ReminderLogDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetRemind(_dayRemind, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

    Public Function GetCompanyNewInfo() As List(Of StatisticDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCompanyNewInfo(Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetStatisticSeniority() As List(Of StatisticDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetStatisticSeniority(Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Competency Dashboard"
    Public Function GetStatisticTop5Competency(ByVal _year As Integer) As List(Of StatisticDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetStatisticTop5Competency(_year, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetStatisticTop5CopAvg(ByVal _year As Integer) As List(Of CompetencyAvgEmplDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetStatisticTop5CopAvg(_year, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "Portal Dashboard"
    Public Function GetEmployeeReg(ByVal _employee_id As Integer) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeReg(_employee_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Function GetTotalDayOff(ByVal _filter As TotalDayOffDTO) As TotalDayOffDTO
        Using rep As New ProfileBusinessClient
            Try
                Dim lst = rep.GetTotalDayOff(_filter, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetCertificateExpires(ByVal _employee_id As Integer) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCertificateExpires(_employee_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region


End Class

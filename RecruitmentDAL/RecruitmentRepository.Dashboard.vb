Imports System.Web
Imports Framework.Data
Imports System.Data.Objects
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Data.EntityClient
Imports Framework.Data.System.Linq.Dynamic
Imports System.Data.Entity
Imports System.Text.RegularExpressions
Imports System.Reflection

Partial Class RecruitmentRepository

#Region "Dashboard"

    Public Function GetStatisticGender(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_RECRUITMENT_DASHBOARD.GET_STATISTIC_GENDER", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = dr("NAME") & " (" & dr("VALUE") & ")",
                                   .VALUE = Decimal.Parse(dr("VALUE"))})
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetStatisticEduacation(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_RECRUITMENT_DASHBOARD.GET_STATISTIC_EDUCATION", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = dr("NAME") & " (" & dr("VALUE") & ")",
                                   .VALUE = Decimal.Parse(dr("VALUE"))})
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetStatisticCanToEmp(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_RECRUITMENT_DASHBOARD.GET_STATISTIC_CAN_TO_EMP", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = dr("NAME"),
                                   .VALUE = Decimal.Parse(dr("VALUE")),
                                   .VALUE_SECOND = 0
                                   })
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetStatisticEstimateReality(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_RECRUITMENT_DASHBOARD.GET_STATISTIC_ESTIMATE_REALITY", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = dr("NAME"),
                                   .VALUE = Decimal.Parse(dr("VALUE")),
                                   .VALUE_SECOND = Decimal.Parse(dr("VALUE_SECOND"))
                               })
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

End Class

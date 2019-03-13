Imports Recruitment.RecruitmentBusiness
Imports Framework.UI

Partial Class RecruitmentRepository

#Region "Dashboard"

    Public Function GetStatisticGender(ByVal _year As Integer) As List(Of StatisticDTO)

        Dim lstStatistic As List(Of StatisticDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstStatistic = rep.GetStatisticGender(_year, Me.Log)
                Return lstStatistic
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetStatisticEduacation(ByVal _year As Integer) As List(Of StatisticDTO)

        Dim lstStatistic As List(Of StatisticDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstStatistic = rep.GetStatisticEduacation(_year, Me.Log)
                Return lstStatistic
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetStatisticCanToEmp(ByVal _year As Integer) As List(Of StatisticDTO)

        Dim lstStatistic As List(Of StatisticDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstStatistic = rep.GetStatisticCanToEmp(_year, Me.Log)
                Return lstStatistic
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetStatisticEstimateReality(ByVal _year As Integer) As List(Of StatisticDTO)

        Dim lstStatistic As List(Of StatisticDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstStatistic = rep.GetStatisticEstimateReality(_year, Me.Log)
                Return lstStatistic
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region
End Class

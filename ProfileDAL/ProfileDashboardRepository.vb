Imports System.Transactions
Imports System.Web
Imports Framework.Data
Imports System.Data.Objects
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Data.EntityClient

Public Class ProfileDashboardRepository
    Inherits ProfileRepositoryBase

#Region "Profile Dashboard"
    Public Function GetListEmployeeStatistic() As List(Of OtherListDTO)
        Try
            '
            Dim query As List(Of OtherListDTO)
            query = (From p In Context.OT_OTHER_LIST
                   Where p.OT_OTHER_LIST_TYPE.CODE = ProfileCommon.OT_DASHBOARD_EMPLOYEE_STATISTIC.Name And
                   p.ACTFLG = "A"
                   Order By p.ATTRIBUTE1
                   Select New OtherListDTO With {
                       .ID = p.ID,
                       .CODE = p.CODE,
                       .NAME_VN = p.NAME_VN,
                       .NAME_EN = p.NAME_EN}).ToList
            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetListChangeStatistic() As List(Of OtherListDTO)
        Try
            Dim query As List(Of OtherListDTO)
            query = (From p In Context.OT_OTHER_LIST
                   Where p.OT_OTHER_LIST_TYPE.CODE = ProfileCommon.OT_DASHBOARD_CHANGE_STATISTIC.Name And
                   p.ACTFLG = "A"
                   Order By p.ATTRIBUTE1
                   Select New OtherListDTO With {
                       .ID = p.ID,
                       .CODE = p.CODE,
                       .NAME_VN = p.NAME_VN,
                       .NAME_EN = p.NAME_EN}).ToList
            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeStatistic(ByVal _type As String, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Select Case _type
                Case ProfileCommon.OT_DASHBOARD_EMPLOYEE_STATISTIC.GENDER
                    Return GetStatisticGender(log)
                Case ProfileCommon.OT_DASHBOARD_EMPLOYEE_STATISTIC.CONTRACT_TYPE
                    Return GetStatisticContractType(log)
                Case ProfileCommon.OT_DASHBOARD_EMPLOYEE_STATISTIC.LEARNING
                    Return GetStatisticLearing(log)
            End Select
            Return Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetChangeStatistic(ByVal _type As String, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Select Case _type
                Case ProfileCommon.OT_DASHBOARD_CHANGE_STATISTIC.EMP_COUNT_YEAR
                    Return GetStatisticEmployeeCountYear(log)
                Case ProfileCommon.OT_DASHBOARD_CHANGE_STATISTIC.EMP_COUNT_MON
                    Return GetStatisticEmployeeCountMonth(log)
                Case ProfileCommon.OT_DASHBOARD_CHANGE_STATISTIC.EMP_CHANGE_MON
                    Return GetStatisticEmployeeChangeMonth(log)
                Case ProfileCommon.OT_DASHBOARD_CHANGE_STATISTIC.TER_CHANGE_MON
                    Return GetStatisticTerChangeMOnth(log)
            End Select
            Return Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetStatisticGender(ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_STATISTIC_GENDER", obj)
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

    Private Function GetStatisticContractType(ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_STATISTIC_CTRACT_TYPE", obj)

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

    Private Function GetStatisticLearing(ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_STATISTIC_LEARNING", obj)

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

    Private Function GetStatisticEmployeeCountYear(ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)
            Dim lst As List(Of StatisticDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_STATISTIC_EMPLOYEE_YEAR",
                                                    New With {.P_USERNAME = log.Username,
                                                              .P_CUR = cls.OUT_CURSOR})
                lst = dtData.ToList(Of StatisticDTO)()
            End Using

            Return lst
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetStatisticEmployeeCountMonth(ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)
            Dim lst As List(Of StatisticDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_STATISTIC_EMPLOYEE_MONTH",
                                                    New With {.P_USERNAME = log.Username,
                                                              .P_CUR = cls.OUT_CURSOR})
                lst = dtData.ToList(Of StatisticDTO)()
            End Using

            For i = 1 To 12
                Dim str As String = IIf(i < 10, "0" & i.ToString, i.ToString())
                Dim q = (From p In lst Where p.NAME = str).FirstOrDefault
                If q Is Nothing Then
                    lst.Add(New StatisticDTO With {.NAME = str, .VALUE = 0})
                End If
            Next
            Return lst
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetStatisticTerChangeMOnth(ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)
            Dim lst1 As List(Of StatisticDTO)
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_STATISTIC_TER_CHANGE_MONTH",
                                                    New With {.P_USERNAME = log.Username,
                                                              .P_CUR = cls.OUT_CURSOR}, False)
                lst1 = dsData.Tables(0).ToList(Of StatisticDTO)()
            End Using

            For i = 0 To lst1.Count - 1
                Dim r As New StatisticDTO
                r.NAME = lst1(i).NAME
                r.VALUE = lst1(i).VALUE
                result.Add(r)
            Next

            For i = 1 To 12
                Dim str As String
                str = i.ToString
                If i > 0 And i < 10 Then
                    str = "0" & i.ToString()
                End If
                Dim q = (From p In result Where p.NAME = str).FirstOrDefault
                If q Is Nothing Then
                    result.Add(New StatisticDTO With {.NAME = str, .VALUE = 0, .VALUE_SECOND = 0})
                End If
            Next
            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetStatisticEmployeeChangeMonth(ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)
            Dim lst1 As List(Of StatisticDTO)
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_STATISTIC_EMP_CHANGE_MONTH",
                                                    New With {.P_USERNAME = log.Username,
                                                              .P_CUR = cls.OUT_CURSOR}, False)
                lst1 = dsData.Tables(0).ToList(Of StatisticDTO)()
            End Using

            For i = 0 To lst1.Count - 1
                Dim r As New StatisticDTO
                r.NAME = lst1(i).NAME
                r.VALUE = lst1(i).VALUE
                result.Add(r)
            Next

            For i = 1 To 12
                Dim str As String
                str = i.ToString
                If i > 0 And i < 10 Then
                    str = "0" & i.ToString()
                End If
                Dim q = (From p In result Where p.NAME = str).FirstOrDefault
                If q Is Nothing Then
                    result.Add(New StatisticDTO With {.NAME = str, .VALUE = 0, .VALUE_SECOND = 0})
                End If
            Next
            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetStatisticSeniority(ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_STATISTIC_SENIORITY", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = "1 năm" & " (" & dr("MOT") & ")",
                                   .VALUE = Decimal.Parse(IIf(dr("MOT") Is Nothing, 0, dr("MOT")))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "2 năm" & " (" & dr("HAI") & ")",
                                   .VALUE = Decimal.Parse(IIf(dr("HAI") Is Nothing, 0, dr("HAI")))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "3 năm" & " (" & dr("BA") & ")",
                                   .VALUE = Decimal.Parse(IIf(dr("BA") Is Nothing, 0, dr("BA")))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "4 năm" & " (" & dr("BON") & ")",
                                   .VALUE = Decimal.Parse(IIf(dr("BON") Is Nothing, 0, dr("BON")))})
                        result.Add(New StatisticDTO With {
                                   .NAME = ">5 năm" & " (" & dr("NAM") & ")",
                                   .VALUE = Decimal.Parse(IIf(dr("NAM") Is Nothing, 0, dr("BON")))})
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "Reminder"

    Public Function GetRemind(ByVal sRemind As String, ByVal log As UserLog) As List(Of ReminderLogDTO)
        Try
            Dim query As List(Of ReminderLogDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_REMIND",
                                                    New With {.P_USERNAME = log.Username,
                                                              .P_REMIND = sRemind,
                                                              .P_CUR = cls.OUT_CURSOR})
                query = dtData.ToList(Of ReminderLogDTO)()
            End Using

            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetCompanyNewInfo(ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim query As List(Of StatisticDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_COMPANY_NEW_INFO",
                                                    New With {.P_USERNAME = log.Username,
                                                              .P_CUR = cls.OUT_CURSOR})
                query = dtData.ToList(Of StatisticDTO)()
            End Using

            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#End Region

#Region "Competency Dashboard"
    Public Function GetStatisticTop5Competency(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMPETENCY_DASHBOARD.GET_STATISTIC_TOP5_COMPETENCY", obj)
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

    Public Function GetStatisticTop5CopAvg(ByVal _year As Integer, ByVal log As UserLog) As List(Of CompetencyAvgEmplDTO)
        Try
            Dim result As New List(Of CompetencyAvgEmplDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMPETENCY_DASHBOARD.GET_STATISTIC_TOP5_COP_AVG", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New CompetencyAvgEmplDTO With {
                                   .COMPETENCY_ID = dr("ID"),
                                   .COMPETENCY_NAME = dr("NV_NAME"),
                                   .LEVEL_NUMBER_STANDARD = Decimal.Parse(dr("NL_CHUAN")),
                                   .LEVEL_NUMBER_ASS_AVG = Decimal.Parse(dr("NLNV_TB"))})
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function



#End Region
#Region "Portal Dashboard"
    Public Function GetEmployeeReg(ByVal _employee_id As Integer) As DataTable
        Try
            Dim result As New DataTable

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_ID_EMPLOYEE = _employee_id,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PORTAL_DASHBOARD.GET_EMPLOYEE_REG", obj)
                'If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                result = dtData
                'End If
            End Using
            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTotalDayOff(ByVal _filter As TotalDayOffDTO,
                                Optional ByVal log As UserLog = Nothing) As TotalDayOffDTO
        Try
            Dim result As New TotalDayOffDTO
            Using cls As New DataAccess.QueryData

                ' nghi bu 
                If (_filter.LEAVE_TYPE = 255) Then
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.MANAGEMENT_TOTAL_COMPENSATORY",
                                     New With {.P_EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                               .P_DATE_TIME = _filter.DATE_REGISTER,
                                               .P_OUT = cls.OUT_CURSOR})
                    If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                        result.EMPLOYEE_ID = _filter.EMPLOYEE_ID
                        result.DATE_REGISTER = _filter.DATE_REGISTER
                        result.LEAVE_TYPE = _filter.LEAVE_TYPE
                        result.TOTAL_DAY = dtData.Rows(0)("TONG_NB")
                        result.USED_DAY = dtData.Rows(0)("DA_NB")
                        result.REST_DAY = dtData.Rows(0)("NB_CON_LAI")
                    End If
                    'nghi phep
                ElseIf (_filter.LEAVE_TYPE = 251) Then
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.MANAGEMENT_TOTAL_ENTITLEMENT",
                                    New With {.P_EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                              .P_DATE_TIME = _filter.DATE_REGISTER,
                                              .P_OUT = cls.OUT_CURSOR})
                    If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                        result.EMPLOYEE_ID = _filter.EMPLOYEE_ID
                        result.DATE_REGISTER = _filter.DATE_REGISTER
                        result.LEAVE_TYPE = _filter.LEAVE_TYPE
                        result.TOTAL_DAY = dtData.Rows(0)("PHEP_TRONG_NAM")
                        result.USED_DAY = dtData.Rows(0)("PHEP_DA_NGHI")
                        result.REST_DAY = dtData.Rows(0)("PHEP_CON_LAI")

                    End If
                End If
            End Using
            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetCertificateExpires(ByVal _employee_id As Integer) As DataTable
        Try
            'Dim result As New DataTable

            'Using cls As New DataAccess.QueryData
            '    Dim obj = New With {.P_EMPLOYEE_ID = _employee_id,
            '                        .P_CUR = cls.OUT_CURSOR}
            '    Dim dtData As DataTable = cls.ExecuteStore("PKG_PORTAL_DASHBOARD.GET_CERTIFICATE_EXPIRES", obj)
            '    If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
            '        result = dtData
            '    End If
            'End Using
            'Return result
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PORTAL_DASHBOARD.GET_CERTIFICATE_EXPIRES",
                                           New With {.P_EMPLOYEE_ID = _employee_id,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
End Class

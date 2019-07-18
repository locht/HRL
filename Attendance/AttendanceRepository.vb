Imports Attendance.AttendanceBusiness
Imports Framework.UI

Public Class AttendanceRepository
    Inherits AttendanceRepositoryBase
    Private _isAvailable As Boolean

    Dim CacheMinusDataCombo As Integer = 30

    Public Function GetOtherList(ByVal sType As String, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New AttendanceBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_" & sType & "_" & Common.Common.SystemLanguage.Name & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetOtherList(sType, Common.Common.SystemLanguage.Name, isBlank)
                End If
                CacheManager.Insert("OT_" & sType & "_" & Common.Common.SystemLanguage.Name & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetProjectList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New AttendanceBusinessClient
            Try

                dtData = rep.GetProjectList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetProjectTitleList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New AttendanceBusinessClient
            Try

                dtData = rep.GetProjectTitleList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetProjectWorkList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New AttendanceBusinessClient
            Try

                dtData = rep.GetProjectWorkList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO, Optional ByVal strUser As String = "ADMIN") As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                'cbxData.USER = Me.Log.Username
                Return rep.GetComboboxData(cbxData, Me.Log.Username)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
End Class
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

#Region "PORTAL REGISTRATION SHIFT"
    Public Function GetAtRegShift(ByVal _filter As AtPortalRegistrationShiftDTO,
                                 ByRef Total As Integer,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AtPortalRegistrationShiftDTO)
        Using rep As New AttendanceBusinessClient
            Try

                Return rep.GetAtRegShift(_filter, Total, PageIndex, PageSize, Sorts, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function AddAtShift(ByVal objAtShift As AtPortalRegistrationShiftDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try

                Return rep.AddAtShift(objAtShift)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteAtShift(ByVal _lst_id As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try

                Return rep.DeleteAtShift(_lst_id)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetRegShiftByID(ByVal _id As Decimal) As AtPortalRegistrationShiftDTO
        Using rep As New AttendanceBusinessClient
            Try

                Return rep.GetRegShiftByID(_id)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
#End Region
#Region "STORE PROCEDURE"
    Public Function GET_MANUAL_BY_ID(ByVal id As Decimal) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GET_MANUAL_BY_ID(id)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_INFO_PHEPNAM(ByVal id As Decimal, ByVal fromDate As Date) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GET_INFO_PHEPNAM(id, fromDate)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
#End Region
    
End Class
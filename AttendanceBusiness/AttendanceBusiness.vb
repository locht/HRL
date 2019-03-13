Imports AttendanceBusiness.ServiceContracts
Imports AttendanceDAL
Imports Framework.Data
Imports System.Collections.Generic
Imports LinqKit

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace AttendanceBusiness.ServiceImplementations
    Partial Public Class AttendanceBusiness
        Implements IAttendanceBusiness
#Region "Get data combobox"

        Public Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable _
            Implements IAttendanceBusiness.GetOtherList
            Using rep As New AttendanceRepository
                Try

                    Dim lst = rep.GetOtherList(sType, sLang, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetProjectList(ByVal isBlank As Boolean) As DataTable _
            Implements IAttendanceBusiness.GetProjectList
            Using rep As New AttendanceRepository
                Try

                    Dim lst = rep.GetProjectList(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetProjectTitleList(ByVal isBlank As Boolean) As DataTable _
            Implements IAttendanceBusiness.GetProjectTitleList
            Using rep As New AttendanceRepository
                Try

                    Dim lst = rep.GetProjectTitleList(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetProjectWorkList(ByVal isBlank As Boolean) As DataTable _
            Implements IAttendanceBusiness.GetProjectWorkList
            Using rep As New AttendanceRepository
                Try

                    Dim lst = rep.GetProjectWorkList(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function



        Public Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO) As Boolean _
            Implements IAttendanceBusiness.GetComboboxData
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetComboboxData(cbxData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
    End Class
End Namespace

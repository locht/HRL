Imports Profile.ProfileBusiness

Partial Public Class ProfileBusinessRepository
    Inherits ProfileRepositoryBase


#Region "Employee File"
    Public Function InsertAttatch_Manager(ByVal fileInfo As EmployeeFileDTO, ByVal fileBytes As Byte()) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertAttatch_Manager(fileInfo, fileBytes)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateAttatch_Manager(ByVal fileInfo As EmployeeFileDTO, ByVal fileBytes As Byte()) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UpdateAttatch_Manager(fileInfo, fileBytes)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAttatch_Manager(ByVal fileID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteAttatch_Manager(fileID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function GetAttachFile_Manager(ByVal fileID As Decimal) As EmployeeFileDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetAttachFile_Manager(fileID)
            Catch ex As Exception
                rep.Abort()
                Throw
            End Try
        End Using

    End Function

    Public Function DownloadAttachFile_Manager(ByVal fileid As Decimal, ByRef fileInfo As EmployeeFileDTO) As Byte()
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DownloadAttachFile_Manager(fileid, fileInfo)
            Catch ex As Exception
                rep.Abort()
                Throw
            End Try
        End Using

    End Function
    Public Function GetAttachFiles_Manager(ByVal fileType As Decimal, ByVal page As Integer, ByVal pageSize As Integer, ByRef totalPage As Integer, ByVal Employee_id As Decimal) As List(Of EmployeeFileDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetAttachFiles_Manager(fileType, page, pageSize, totalPage, Employee_id)
            Catch ex As Exception
                rep.Abort()
                Throw
            End Try
        End Using

    End Function
#End Region

End Class

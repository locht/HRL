Imports ProfileBusiness.ServiceContracts
Imports ProfileDAL
Imports Framework.Data
Imports System.ServiceModel.Activation
Imports System.Reflection
Imports System.Configuration

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness


        Public Function InsertAttatch_Manager(ByVal fileInfo As EmployeeFileDTO, ByVal fileBytes As Byte()) As Boolean Implements IProfileBusiness.InsertAttatch_Manager
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertAttatch_Manager(fileInfo, fileBytes)
                Catch ex As Exception
                    Throw
                End Try
            End Using
        End Function

        Public Function UpdateAttatch_Manager(ByVal fileInfo As EmployeeFileDTO, ByVal fileBytes As Byte()) As Boolean Implements IProfileBusiness.UpdateAttatch_Manager
            Using rep As New ProfileRepository
                Try

                    Return rep.UpdateAttatch_Manager(fileInfo, fileBytes)
                Catch ex As Exception
                    Throw
                End Try
            End Using
        End Function

        Public Function DeleteAttatch_Manager(ByVal fileID As Decimal) As Boolean Implements IProfileBusiness.DeleteAttatch_Manager
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteAttatch_Manager(fileID)
                Catch ex As Exception
                    Throw
                End Try
            End Using
        End Function


        Public Function GetAttachFile_Manager(ByVal fileId As Decimal) As EmployeeFileDTO Implements IProfileBusiness.GetAttachFile_Manager
            Using rep As New ProfileRepository
                Try

                    Return rep.GetAttachFile_Manager(fileId)
                Catch ex As Exception
                    Throw
                End Try
            End Using
        End Function

        Public Function GetAttachFiles_Manager(ByVal fileType As Decimal, ByVal page As Integer, ByVal pageSize As Integer, ByRef totalPage As Integer, ByVal Employee_id As Decimal) As List(Of EmployeeFileDTO) Implements IProfileBusiness.GetAttachFiles_Manager
            Using rep As New ProfileRepository
                Try

                    Return rep.GetAttachFiles_Manager(fileType, page, pageSize, totalPage, Employee_id)
                Catch ex As Exception
                    Throw
                End Try
            End Using
        End Function

        Public Function DownloadAttachFile_Manager(ByVal fileID As Decimal, ByRef fileInfo As EmployeeFileDTO) As Byte() Implements IProfileBusiness.DownloadAttachFile_Manager
            Using rep As New ProfileRepository
                Try

                    Return rep.DownloadAttachFile_Manager(fileID, fileInfo)
                Catch ex As Exception
                    Throw
                End Try
            End Using
        End Function


    End Class
End Namespace

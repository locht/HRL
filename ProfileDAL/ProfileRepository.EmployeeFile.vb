Imports System.IO
Imports Framework.Data
Imports System.Data.Objects
Imports Framework.Data.System.Linq.Dynamic
Imports System.Reflection

Partial Class ProfileRepository

#Region "File Upload Management"
    Private Function SaveFile_Manager(ByVal fileId As Decimal, ByVal fileBytes As Byte()) As Boolean
        Try
            Dim filePath = AppDomain.CurrentDomain.BaseDirectory & "\AttatchFilesManager"
            If Not Directory.Exists(filePath) Then
                Directory.CreateDirectory(filePath)
            End If

            Dim fPath = Path.Combine(filePath, fileId.ToString)
            Dim fInfo As New FileInfo(fPath)
            Dim fStream As FileStream

            fStream = fInfo.Open(FileMode.Create)

            fStream.Write(fileBytes, 0, fileBytes.Length)

            fStream.Close()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

    Private Function DeleteFile_Manager(ByVal fileId As Decimal) As Boolean
        Try
            Dim filePath = AppDomain.CurrentDomain.BaseDirectory & "\AttatchFilesManager"
            Dim fPath = Path.Combine(filePath, fileId.ToString)
            Dim fInfo As New FileInfo(fPath)

            If fInfo.Exists Then
                fInfo.Delete()
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

    Public Function DownloadAttachFile_Manager(ByVal fileID As Decimal, ByRef fileInfo As EmployeeFileDTO) As Byte()
        Try
            Dim filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AttatchFilesManager")

            Dim fPath = Path.Combine(filePath, fileID.ToString)

            If File.Exists(fPath) Then

                Dim fInfo = New FileInfo(fPath)

                Dim fileBytes As Byte() = New Byte(fInfo.Length) {}

                Using fStream As System.IO.Stream = fInfo.OpenRead()
                    fStream.Read(fileBytes, 0, fInfo.Length)
                End Using

                fileInfo = GetAttachFile_Manager(fileID)

                Return fileBytes
            Else
                Return New Byte() {}
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

    Public Function InsertAttatch_Manager(ByVal fileInfo As EmployeeFileDTO, ByVal fileBytes As Byte()) As Boolean
        Try

            Dim attFile As HU_EMPLOYEE_FILE
            Dim fileID As Decimal = Utilities.GetNextSequence(Context, Context.HU_EMPLOYEE_FILE.EntitySet.Name)

            If SaveFile(fileID, fileBytes) Then

                attFile = New HU_EMPLOYEE_FILE With {
                    .FILE_NAME = fileInfo.FILE_NAME,
                    .FILE_SIZE = fileInfo.FILE_SIZE,
                    .FILE_TYPE = fileInfo.FILE_TYPE,
                    .NOTE = fileInfo.NOTE,
                    .UPLOAD_BY = fileInfo.UPLOAD_BY,
                    .UPLOAD_DATE = Date.Now,
                    .ID = fileID,
                    .EFFECT_DATE = fileInfo.EFFECT_DATE,
                    .EXPIRE_DATE = fileInfo.EXPIRE_DATE,
                    .FILE_NO = fileInfo.FILE_NO,
                    .EFFECT_LOCATION = fileInfo.EFFECT_LOCATION,
                    .FILE_NUMBER = fileInfo.FILE_NUMBER,
                    .NAME_VN = fileInfo.NAME_VN,
                    .EMPLOYEE_ID = fileInfo.EMPLOYEE_ID
                }

                Context.HU_EMPLOYEE_FILE.AddObject(attFile)
            Else
                Return False
            End If

            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

    Public Function UpdateAttatch_Manager(ByVal fileInfo As EmployeeFileDTO, ByVal fileBytes As Byte()) As Boolean
        Try
            Dim objUpdate As New HU_EMPLOYEE_FILE With {.ID = fileInfo.ID}
            Context.HU_EMPLOYEE_FILE.Attach(objUpdate)
            With objUpdate

                If fileInfo.FILE_NAME <> "-1" And fileInfo.FILE_SIZE > 0 Then
                    If DeleteFile_Manager(fileInfo.ID) And SaveFile(fileInfo.ID, fileBytes) Then
                        .FILE_NAME = fileInfo.FILE_NAME
                        .FILE_SIZE = fileInfo.FILE_SIZE
                    Else
                        Return False
                    End If
                End If

                .FILE_TYPE = fileInfo.FILE_TYPE
                .NOTE = fileInfo.NOTE
                .UPLOAD_DATE = Date.Now
                .EFFECT_DATE = fileInfo.EFFECT_DATE
                .EXPIRE_DATE = fileInfo.EXPIRE_DATE
                .FILE_NO = fileInfo.FILE_NO
                .EFFECT_LOCATION = fileInfo.EFFECT_LOCATION
                .FILE_NUMBER = fileInfo.FILE_NUMBER
                .NAME_VN = fileInfo.NAME_VN

            End With
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

    Public Function DeleteAttatch_Manager(ByVal fileID As Decimal) As Boolean
        Try
            Dim fileDelete = Context.HU_EMPLOYEE_FILE.SingleOrDefault(Function(p) p.ID = fileID)

            If fileDelete IsNot Nothing Then

                'Clean file trên server

                If DeleteFile(fileID) Then

                    Context.HU_EMPLOYEE_FILE.DeleteObject(fileDelete)

                    Context.SaveChanges()

                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

    Public Function GetAttachFiles_Manager(ByVal fileType As Decimal, ByVal page As Integer, ByVal pageSize As Integer, ByRef totalPage As Integer, ByVal Employee_id As Decimal) As List(Of EmployeeFileDTO)
        Try
            Dim listFile As New List(Of EmployeeFileDTO)
            totalPage = 0
            Dim fList = From a In Context.HU_EMPLOYEE_FILE
                    Join p In Context.OT_OTHER_LIST On a.FILE_TYPE Equals p.ID
                    Where a.EMPLOYEE_ID = Employee_id
                        Order By p.NAME_VN Ascending, a.UPLOAD_DATE Descending
                        Select New EmployeeFileDTO With {
                            .ID = a.ID,
                            .FILE_NAME = a.FILE_NAME,
                            .FILE_SIZE = a.FILE_SIZE,
                            .FILE_TYPE = a.FILE_TYPE,
                            .FILE_TYPE_NAME = p.NAME_VN,
                            .NOTE = a.NOTE,
                            .UPLOAD_DATE = a.UPLOAD_DATE,
                            .EFFECT_DATE = a.EFFECT_DATE,
                            .EXPIRE_DATE = a.EXPIRE_DATE,
                            .FILE_NO = a.FILE_NO,
                            .EFFECT_LOCATION = a.EFFECT_LOCATION,
                            .FILE_NUMBER = a.FILE_NUMBER,
                            .NAME_VN = a.NAME_VN,
            .UPLOAD_BY = a.UPLOAD_BY
                        }


            If fileType <> 0 Then
                fList = fList.Where(Function(p) p.FILE_TYPE = fileType)
            End If

            totalPage = fList.Count

            listFile = fList.Skip(page * pageSize).Take(pageSize).ToList

            Return listFile

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

    Public Function GetAttachFile_Manager(ByVal fileId As Decimal) As EmployeeFileDTO
        Try
            Dim a = Context.HU_EMPLOYEE_FILE.SingleOrDefault(Function(p) p.ID = fileId)

            If a IsNot Nothing Then
                Dim fReturn As New EmployeeFileDTO With {
                            .ID = a.ID,
                            .FILE_NAME = a.FILE_NAME,
                            .FILE_SIZE = a.FILE_SIZE,
                            .FILE_TYPE = a.FILE_TYPE,
                            .NOTE = a.NOTE,
                            .EFFECT_DATE = a.EFFECT_DATE,
                            .EXPIRE_DATE = a.EXPIRE_DATE,
                            .FILE_NO = a.FILE_NO,
                            .EFFECT_LOCATION = a.EFFECT_LOCATION,
                            .FILE_NUMBER = a.FILE_NUMBER,
                            .UPLOAD_BY = a.UPLOAD_BY,
                            .NAME_VN = a.NAME_VN
                        }
                Return fReturn
            Else
                Return Nothing
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function


    Private Function DeleteFile(ByVal fileId As Decimal) As Boolean
        Try
            Dim filePath = AppDomain.CurrentDomain.BaseDirectory & "\AttatchFilesManager"
            Dim fPath = Path.Combine(filePath, fileId.ToString)
            Dim fInfo As New FileInfo(fPath)

            If fInfo.Exists Then
                fInfo.Delete()
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteFile")
            Throw ex
        End Try
    End Function

    Private Function SaveFile(ByVal fileId As Decimal, ByVal fileBytes As Byte()) As Boolean
        Try
            Dim filePath = AppDomain.CurrentDomain.BaseDirectory & "\AttatchFilesManager"
            If Not Directory.Exists(filePath) Then
                Directory.CreateDirectory(filePath)
            End If

            Dim fPath = Path.Combine(filePath, fileId.ToString)
            Dim fInfo As New FileInfo(fPath)
            Dim fStream As FileStream

            fStream = fInfo.Open(FileMode.Create)

            fStream.Write(fileBytes, 0, fileBytes.Length)

            fStream.Close()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".SaveFile")
            Throw ex
        End Try
    End Function


#End Region

End Class

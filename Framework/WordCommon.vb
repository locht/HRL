Imports Aspose.Words
Imports Aspose.Words.Reporting
Imports System.IO

Public Class WordCommon
    Implements IDisposable

    Public Sub ExportMailMerge(ByVal link As String,
                               ByVal filename As String,
                               ByVal dtData As DataTable,
                               ByVal response As System.Web.HttpResponse)
        Try
            ' Open an existing document. 
            Dim doc As New Document(link)
            ' Fill the fields in the document with user data.
            doc.MailMerge.Execute(dtData)
            ' Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.

            'doc.Save(filename, SaveFormat.Doc, SaveType.OpenInApplication, response)

            doc.Save(response, filename & ".doc",
                     Aspose.Words.ContentDisposition.Attachment,
                     Aspose.Words.Saving.SaveOptions.CreateSaveOptions(SaveFormat.Doc))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ExportMailMerge(ByVal filePath As String,
                               ByVal fileName As String,
                               ByVal dsData As DataSet,
                               ByVal response As System.Web.HttpResponse)
        Try
            ' Open an existing document. 
            Dim doc As New Document(filePath)
            ' Fill the fields in the document with user data.
            doc.MailMerge.FieldMergingCallback = New HandleMergeImageFieldFromBlob()
            doc.MailMerge.Execute(dsData.Tables(0))
            For i As Integer = 1 To dsData.Tables.Count - 1
                doc.MailMerge.ExecuteWithRegions(dsData.Tables(i))
            Next
            doc.MailMerge.DeleteFields()
            doc.MailMerge.RemoveEmptyRegions = True
            doc.MailMerge.RemoveEmptyParagraphs = True

            doc.Save(response, fileName & ".doc",
                     Aspose.Words.ContentDisposition.Attachment,
                     Aspose.Words.Saving.SaveOptions.CreateSaveOptions(SaveFormat.Doc))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub ExportMailMerge(ByVal filePath As String,
                              ByVal fileName As String,
                              ByVal dsData As DataSet,
                              ByVal fileImage As Byte(),
                              ByVal response As System.Web.HttpResponse)
        Try
            ' Open an existing document. 
            Dim doc As New Document(filePath)
            ' Fill the fields in the document with user data.
            doc.MailMerge.FieldMergingCallback = New HandleMergeImageFieldFromBlob()
            doc.MailMerge.Execute(dsData.Tables(0))
            For i As Integer = 1 To dsData.Tables.Count - 1
                doc.MailMerge.ExecuteWithRegions(dsData.Tables(i))
            Next
            doc.MailMerge.DeleteFields()
            doc.MailMerge.RemoveEmptyRegions = True
            doc.MailMerge.RemoveEmptyParagraphs = True

            Dim builder As New DocumentBuilder(doc)
            Dim imageStream As New MemoryStream(fileImage)
            builder.InsertImage(imageStream, Drawing.RelativeHorizontalPosition.Margin, 440, Drawing.RelativeVerticalPosition.Margin, 56, 70, 90, Drawing.WrapType.None)

            doc.Save(response, fileName & ".doc",
                     Aspose.Words.ContentDisposition.Attachment,
                     Aspose.Words.Saving.SaveOptions.CreateSaveOptions(SaveFormat.Doc))
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
#Region "Report"
    Private Class HandleMergeImageFieldFromBlob
        Implements IFieldMergingCallback
        Private Sub IFieldMergingCallback_FieldMerging(ByVal args As FieldMergingArgs) Implements IFieldMergingCallback.FieldMerging
            ' Do nothing.
        End Sub

        ''' <summary>
        ''' This is called when mail merge engine encounters Image:XXX merge field in the document.
        ''' You have a chance to return an Image object, file name or a stream that contains the image.
        ''' </summary>
        Private Sub ImageFieldMerging(ByVal e As ImageFieldMergingArgs) Implements IFieldMergingCallback.ImageFieldMerging
            ' The field value is a byte array, just cast it and create a stream on it.
            If Not IsDBNull(e.FieldValue) OrElse e.FieldValue IsNot Nothing Then

                Dim builder As New DocumentBuilder(e.Document)
                Dim imageStream As New MemoryStream(CType(e.FieldValue, Byte()))
                builder.MoveToMergeField(e.FieldName)
                builder.InsertImage(imageStream, 90, 120)

                'Now the mail merge engine will retrieve the image from the stream.
                e.ImageStream = imageStream
            End If
        End Sub
    End Class
    Public Function mf_sConvertVietnameseToEn(ByVal sv_sText As String) As String
        Try
            Dim TextToFind As String = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ"
            Dim TextToReplace As String = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY"

            Dim schar As String = "", sTextResult As String = "", strVietNamese As String = mf_ObjectToString(sv_sText)
            Dim index As Integer = -1

            For i As Integer = 0 To strVietNamese.Length - 1
                schar = (strVietNamese(i))
                index = TextToFind.IndexOf(schar)
                If index >= 0 Then
                    strVietNamese = strVietNamese.Replace(schar, mf_ObjectToString(TextToReplace.Substring(index, 1)))
                End If
            Next
            Return strVietNamese
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function mf_ObjectToString(ByVal obj As Object) As String
        Try
            If obj Is Nothing Then Return ""
            Return obj.ToString().Trim()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

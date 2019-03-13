﻿Imports System.Configuration
Imports System.Text
Imports System.IO

Public Module Utility
    ''' <summary>
    ''' Writes the exception log.
    ''' </summary>
    ''' <param name="objErr">The obj err.</param>
    ''' <param name="sFunc">The s func.</param>
    Public Sub WriteExceptionLog(ByVal objErr As Exception, ByVal sFunc As String, Optional ByVal OtherText As String = "")
        Dim err As New StringBuilder()
        Dim logFile As String = String.Empty
        Dim logWriter As System.IO.StreamWriter = Nothing
        Dim logfolder As String
        Dim logformat As String
        Try
            err.AppendLine("Function: " + sFunc)
            err.AppendLine("Datetime: " + DateTime.Now)
            err.AppendLine("Error message: " + objErr.Message)
            err.AppendLine("Stack trace: " + objErr.StackTrace)

            If objErr.InnerException IsNot Nothing Then
                err.AppendLine("Inner error message: " + objErr.InnerException.Message)
                err.AppendLine("Inner stack trace: " + objErr.InnerException.StackTrace)
            End If

            err.AppendLine("Other Text: " + OtherText)

            logfolder = "Recruitment\Exception"
            If String.IsNullOrEmpty(logfolder) Then logfolder = "Exception"

            logformat = "yyyyMMdd.log"

            If Not Directory.Exists(AppDomain.CurrentDomain.BaseDirectory & "\" & logfolder) Then
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory & "\" & logfolder)
            End If

            logFile = AppDomain.CurrentDomain.BaseDirectory & "\" & logfolder & "\" & DateTime.Now.ToString(logformat)

            If System.IO.File.Exists(logFile) Then
                logWriter = System.IO.File.AppendText(logFile)
            Else
                logWriter = System.IO.File.CreateText(logFile)
            End If

            logWriter.WriteLine(err.ToString())
            logWriter.Close()
        Catch e As Exception

        End Try
    End Sub

    Public Function DateDiffDay(ByVal startdate As Date, ByVal enddate As Date) As Double
        Try
            Dim sDate As TimeSpan = enddate - startdate
            Return sDate.TotalDays + 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Module

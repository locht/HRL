Imports System
Imports Quartz
Imports System.IO
Imports Quartz.Impl
Imports Aspose.Cells

Public Class JobSheduleImportAT
    Implements IJob

    Public Sub Execute(ByVal context As IJobExecutionContext) Implements IJob.Execute
        Execute()
    End Sub
    Public Sub Execute()
        Dim fIn As String = System.Configuration.ConfigurationManager.AppSettings("FileIn").ToString()
        Dim fOut As String = System.Configuration.ConfigurationManager.AppSettings("FileOut").ToString()
        Dim pass = System.Configuration.ConfigurationManager.AppSettings("FileInPassword")
        Dim streamWriter As StreamWriter = New StreamWriter(System.IO.Path.Combine(fIn, "log.txt"), True)
        streamWriter.WriteLine("Run at: " + DateTime.Now.ToString)

        Dim rep As New Profile.ProfileStoreProcedure
        Dim allFile = Directory.EnumerateFiles(fIn, "*.*", SearchOption.AllDirectories).Where(Function(w) w.EndsWith(".xls") Or w.EndsWith("xlsx"))
        For Each file As String In allFile
            Try
                Dim dsDataPrepare As New DataSet
                Dim options As New Aspose.Cells.LoadOptions
                If Not String.IsNullOrWhiteSpace(pass) Then
                    options.Password = pass
                End If
                Dim workbook = New Aspose.Cells.Workbook(file)
                Dim worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.GetLastDataRow(0) + 1, 3, True))
                dsDataPrepare.Tables(0).TableName = "DATA"
                dsDataPrepare.Tables(0).Columns(0).ColumnName = "ID"
                dsDataPrepare.Tables(0).Columns(2).ColumnName = "VAL"
                dsDataPrepare.Tables(0).Columns.RemoveAt(1)
                Dim strXML As String = dsDataPrepare.GetXml()

                'If rep.IMPORT_SWIPE_DATA_AUTO(strXML) Then
                '    My.Computer.FileSystem.MoveFile(file, System.IO.Path.Combine(fOut, "Success", Path.GetFileName(file)), True)
                'Else
                '    My.Computer.FileSystem.MoveFile(file, System.IO.Path.Combine(fOut, "Error", Path.GetFileName(file)), True)
                'End If

            Catch ex As Exception
                streamWriter.WriteLine("Error: " + ex.Message)
                My.Computer.FileSystem.MoveFile(file, System.IO.Path.Combine(fOut, "Error", Path.GetFileName(file)), True)
                'streamWriter.Close()
                Continue For
            End Try
        Next
        streamWriter.Close()
    End Sub
End Class

Public Class SheduleImportAT

    Public Shared Sub Start()
        Dim cronStr As String = System.Configuration.ConfigurationManager.AppSettings("Cron").ToString()
        Dim scheduler As IScheduler = StdSchedulerFactory.GetDefaultScheduler
        scheduler.Start()
        Dim job As IJobDetail = JobBuilder.Create(Of JobSheduleImportAT).Build
        Dim trigger As ITrigger = TriggerBuilder.Create.WithIdentity("JobSheduleImportAT", "ImportAT").WithCronSchedule(cronStr).WithPriority(1).Build()
        scheduler.ScheduleJob(job, trigger)
    End Sub
End Class

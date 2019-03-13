Imports AttendanceDAL
Imports Framework.Data
Imports System.Collections.Generic
Imports LinqKit
Imports System.Threading

Namespace AttendanceBusiness.BackgroundProcess
    Public Class AttendanceBusinessBackgroundProcess

        Private WithEvents timer As Timers.Timer
        Private _interval As Integer = 60000 '1min
        Public Property Interval As Integer
            Get
                Return _interval
            End Get
            Set(ByVal value As Integer)
                _interval = value
            End Set
        End Property

        Public Sub Start()

            timer = New Timers.Timer(_interval) '1min
            timer.Start()

        End Sub

        Private Sub Timer_Tick(ByVal sender As Object, ByVal e As Timers.ElapsedEventArgs) Handles timer.Elapsed
            'Import file quẹt thẻ / vân tay
            'ThreadPool.QueueUserWorkItem(AddressOf AttendanceRepositoryStatic.Instance.ImportIOFile, Now)
        End Sub

    End Class
End Namespace
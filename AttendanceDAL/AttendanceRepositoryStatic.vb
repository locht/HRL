﻿Imports System.Configuration

Public Class AttendanceRepositoryStatic

    ''' <summary>
    ''' Gets the instance.
    ''' </summary>
    Public Shared ReadOnly Property Instance() As AttendanceRepository
        Get
            Return New AttendanceRepository()
        End Get
    End Property
End Class

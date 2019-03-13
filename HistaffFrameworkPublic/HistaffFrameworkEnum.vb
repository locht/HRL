Public Module HistaffFrameworkEnum
    ''' <summary>
    ''' Trạng thái bảng công
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enStatusParox
        Working = 0
        Close = 1
        Reactive = 2
        FinalClose = 3
    End Enum

    ''' <summary>
    ''' Trạng thái bảng lương
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enStatusColex
        Working = 0
        Close = 1
        Reactive = 2
        FinalClose = 3
    End Enum

    Public Enum enGroupSalaryType
        ImportOther = 1
        ImportDeduct = 2
    End Enum
End Module

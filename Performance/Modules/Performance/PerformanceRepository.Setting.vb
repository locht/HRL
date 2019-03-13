Imports Performance.PerformanceBusiness
Imports Framework.UI

Partial Class PerformanceRepository

#Region "ObjectGroupPeriod"

    Public Function GetObjectGroupNotByPeriodID(ByVal _filter As ObjectGroupPeriodDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "OBJECT_GROUP_CODE desc") As  _
                                         List(Of ObjectGroupPeriodDTO)

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetObjectGroupNotByPeriodID(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetObjectGroupByPeriodID(ByVal _filter As ObjectGroupPeriodDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "OBJECT_GROUP_CODE desc") As  _
                                         List(Of ObjectGroupPeriodDTO)

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetObjectGroupByPeriodID(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertObjectGroupByPeriod(ByVal lst As List(Of ObjectGroupPeriodDTO)) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertObjectGroupByPeriod(lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function DeleteObjectGroupByPeriod(ByVal lst As List(Of Decimal)) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteObjectGroupByPeriod(lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region

#Region "CriteriaObjectGroup"

    Public Function GetCriteriaNotByObjectGroupID(ByVal _filter As CriteriaObjectGroupDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CRITERIA_CODE") As  _
                                         List(Of CriteriaObjectGroupDTO)

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetCriteriaNotByObjectGroupID(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCriteriaByObjectGroupID(ByVal _filter As CriteriaObjectGroupDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CRITERIA_CODE") As  _
                                         List(Of CriteriaObjectGroupDTO)

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetCriteriaByObjectGroupID(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCriteriaByObjectGroup(ByVal lst As List(Of CriteriaObjectGroupDTO)) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertCriteriaByObjectGroup(lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function DeleteCriteriaByObjectGroup(ByVal lst As List(Of Decimal)) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteCriteriaByObjectGroup(lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateCriteriaObjectGroup(ByVal lst As List(Of CriteriaObjectGroupDTO)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.UpdateCriteriaObjectGroup(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "EmployeeAssessment"

    Public Function GetEmployeeNotByObjectGroupID(ByVal _filter As EmployeeAssessmentDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "Employee_CODE") As  _
                                         List(Of EmployeeAssessmentDTO)

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetEmployeeNotByObjectGroupID(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetEmployeeByObjectGroupID(ByVal _filter As EmployeeAssessmentDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "Employee_CODE") As  _
                                         List(Of EmployeeAssessmentDTO)

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetEmployeeByObjectGroupID(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertEmployeeByObjectGroup(ByVal lst As List(Of EmployeeAssessmentDTO)) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertEmployeeByObjectGroup(lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function DeleteEmployeeByObjectGroup(ByVal lst As List(Of Decimal)) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteEmployeeByObjectGroup(lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

End Class

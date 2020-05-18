Imports Performance.PerformanceBusiness
Imports Framework.UI
Imports Common.CommonBusiness

Partial Class PerformanceRepository

#Region "Criteria"

    Public Function GetCriteria(ByVal _filter As CriteriaDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaDTO)
        Dim lstCriteria As List(Of CriteriaDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstCriteria = rep.GetCriteria(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCriteria
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCriteria(ByVal _filter As CriteriaDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaDTO)
        Dim lstCriteria As List(Of CriteriaDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstCriteria = rep.GetCriteria(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstCriteria
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCriteria(ByVal objCriteria As CriteriaDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertCriteria(objCriteria, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCriteria(ByVal objCriteria As CriteriaDTO) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ValidateCriteria(objCriteria)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCriteria(ByVal objCriteria As CriteriaDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ModifyCriteria(objCriteria, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveCriteria(ByVal lstID As List(Of Decimal), ByVal bActive As String) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ActiveCriteria(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCriteria(ByVal lstCriteria As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteCriteria(lstCriteria)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "Classification"

    Public Function GetClassification(ByVal _filter As ClassificationDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationDTO)
        Dim lstClassification As List(Of ClassificationDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstClassification = rep.GetClassification(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClassification
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetClassification(ByVal _filter As ClassificationDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationDTO)
        Dim lstClassification As List(Of ClassificationDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstClassification = rep.GetClassification(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstClassification
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertClassification(ByVal objClassification As ClassificationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertClassification(objClassification, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateClassification(ByVal objClassification As ClassificationDTO) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ValidateClassification(objClassification)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyClassification(ByVal objClassification As ClassificationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ModifyClassification(objClassification, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveClassification(ByVal lstID As List(Of Decimal), ByVal bActive As String) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ActiveClassification(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteClassification(ByVal lstClassification As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteClassification(lstClassification)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "ObjectGroup"

    Public Function GetObjectGroup(ByVal _filter As ObjectGroupDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ObjectGroupDTO)
        Dim lstObjectGroup As List(Of ObjectGroupDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstObjectGroup = rep.GetObjectGroup(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstObjectGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetObjectGroup(ByVal _filter As ObjectGroupDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ObjectGroupDTO)
        Dim lstObjectGroup As List(Of ObjectGroupDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstObjectGroup = rep.GetObjectGroup(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstObjectGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertObjectGroup(ByVal objObjectGroup As ObjectGroupDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertObjectGroup(objObjectGroup, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateObjectGroup(ByVal objObjectGroup As ObjectGroupDTO) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ValidateObjectGroup(objObjectGroup)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyObjectGroup(ByVal objObjectGroup As ObjectGroupDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ModifyObjectGroup(objObjectGroup, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveObjectGroup(ByVal lstID As List(Of Decimal), ByVal bActive As String) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ActiveObjectGroup(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteObjectGroup(ByVal lstObjectGroup As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteObjectGroup(lstObjectGroup)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Period"

    Public Function GetPeriod(ByVal _filter As PeriodDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO)
        Dim lstPeriod As List(Of PeriodDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstPeriod = rep.GetPeriod(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetPeriod(ByVal _filter As PeriodDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO)
        Dim lstPeriod As List(Of PeriodDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstPeriod = rep.GetPeriod(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetPeriodById(ByVal _filter As PeriodDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO)
        Dim lstPeriod As List(Of PeriodDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstPeriod = rep.GetPeriodById(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertPeriod(ByVal objPeriod As PeriodDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertPeriod(objPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidatePeriod(ByVal objPeriod As PeriodDTO) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ValidatePeriod(objPeriod)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPeriod(ByVal objPeriod As PeriodDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ModifyPeriod(objPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActivePeriod(ByVal lstID As List(Of Decimal), ByVal bActive As String) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ActivePeriod(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeletePeriod(ByVal lstPeriod As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeletePeriod(lstPeriod)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Import đánh giá ABC"
    Public Function GET_LIST_YEAR() As DataTable
        Dim dtdata As DataTable

        Using rep As New PerformanceBusinessClient
            Try
                dtdata = rep.GET_LIST_YEAR()
                Return dtdata
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GET_PERIOD_BY_YEAR(ByVal P_YEAR As Integer) As DataTable
        Dim dtdata As DataTable

        Using rep As New PerformanceBusinessClient
            Try
                dtdata = rep.GET_PERIOD_BY_YEAR(P_YEAR)
                Return dtdata
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GET_DATE_BY_PERIOD(ByVal P_PERIOD_ID As Integer) As DataTable
        Dim dtdata As DataTable

        Using rep As New PerformanceBusinessClient
            Try
                dtdata = rep.GET_DATE_BY_PERIOD(P_PERIOD_ID)
                Return dtdata
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function EXPORT_EVALUATE_ABC(ByVal P_PERIOD_ID As Integer) As DataSet
        Dim dtdata As DataSet

        Using rep As New PerformanceBusinessClient
            Try
                dtdata = rep.EXPORT_EVALUATE_ABC(P_PERIOD_ID)
                Return dtdata
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function INPORT_EVALUATE_ABC(ByVal P_DOCXML As String, ByVal P_PERIOD_ID As Decimal, ByVal P_USER As String) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.INPORT_EVALUATE_ABC(P_DOCXML, P_PERIOD_ID, Me.Log.Username)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

End Class

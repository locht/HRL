Imports PerformanceBusiness.ServiceContracts
Imports PerformanceDAL
Imports Framework.Data
Imports System.Collections.Generic

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace PerformanceBusiness.ServiceImplementations
    Partial Class PerformanceBusiness

#Region "Criteria"

        Public Function GetCriteria(ByVal _filter As CriteriaDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaDTO) Implements ServiceContracts.IPerformanceBusiness.GetCriteria
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetCriteria(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCriteria(ByVal objCriteria As CriteriaDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.InsertCriteria
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertCriteria(objCriteria, log, gID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateCriteria(ByVal objCriteria As CriteriaDTO) As Boolean Implements ServiceContracts.IPerformanceBusiness.ValidateCriteria
            Try
                Using rep As New PerformanceRepository
                    Return rep.ValidateCriteria(objCriteria)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCriteria(ByVal objCriteria As CriteriaDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.ModifyCriteria
            Try
                Using rep As New PerformanceRepository
                    Return rep.ModifyCriteria(objCriteria, log, gID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveCriteria(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPerformanceBusiness.ActiveCriteria
            Try
                Using rep As New PerformanceRepository
                    Return rep.ActiveCriteria(lstID, log, bActive)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteCriteria(ByVal lstCriteria() As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.DeleteCriteria
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeleteCriteria(lstCriteria)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region
#Region "Classification"

        Public Function GetClassification(ByVal _filter As ClassificationDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationDTO) Implements ServiceContracts.IPerformanceBusiness.GetClassification
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetClassification(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertClassification(ByVal objClassification As ClassificationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.InsertClassification
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertClassification(objClassification, log, gID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateClassification(ByVal objClassification As ClassificationDTO) As Boolean Implements ServiceContracts.IPerformanceBusiness.ValidateClassification
            Try
                Using rep As New PerformanceRepository
                    Return rep.ValidateClassification(objClassification)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyClassification(ByVal objClassification As ClassificationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.ModifyClassification
            Try
                Using rep As New PerformanceRepository
                    Return rep.ModifyClassification(objClassification, log, gID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveClassification(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPerformanceBusiness.ActiveClassification
            Try
                Using rep As New PerformanceRepository
                    Return rep.ActiveClassification(lstID, log, bActive)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteClassification(ByVal lstClassification() As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.DeleteClassification
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeleteClassification(lstClassification)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

#Region "ObjectGroup"

        Public Function GetObjectGroup(ByVal _filter As ObjectGroupDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ObjectGroupDTO) Implements ServiceContracts.IPerformanceBusiness.GetObjectGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetObjectGroup(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertObjectGroup(ByVal objObjectGroup As ObjectGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.InsertObjectGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertObjectGroup(objObjectGroup, log, gID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateObjectGroup(ByVal objObjectGroup As ObjectGroupDTO) As Boolean Implements ServiceContracts.IPerformanceBusiness.ValidateObjectGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.ValidateObjectGroup(objObjectGroup)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyObjectGroup(ByVal objObjectGroup As ObjectGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.ModifyObjectGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.ModifyObjectGroup(objObjectGroup, log, gID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveObjectGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPerformanceBusiness.ActiveObjectGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.ActiveObjectGroup(lstID, log, bActive)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteObjectGroup(ByVal lstObjectGroup() As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.DeleteObjectGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeleteObjectGroup(lstObjectGroup)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

#Region "Period"

        Public Function GetPeriod(ByVal _filter As PeriodDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO) Implements ServiceContracts.IPerformanceBusiness.GetPeriod
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetPeriod(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetPeriodById(ByVal _filter As PeriodDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO) Implements ServiceContracts.IPerformanceBusiness.GetPeriodById
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetPeriodById(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertPeriod(ByVal objPeriod As PeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.InsertPeriod
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertPeriod(objPeriod, log, gID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidatePeriod(ByVal objPeriod As PeriodDTO) As Boolean Implements ServiceContracts.IPerformanceBusiness.ValidatePeriod
            Try
                Using rep As New PerformanceRepository
                    Return rep.ValidatePeriod(objPeriod)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyPeriod(ByVal objPeriod As PeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.ModifyPeriod
            Try
                Using rep As New PerformanceRepository
                    Return rep.ModifyPeriod(objPeriod, log, gID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActivePeriod(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPerformanceBusiness.ActivePeriod
            Try
                Using rep As New PerformanceRepository
                    Return rep.ActivePeriod(lstID, log, bActive)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeletePeriod(ByVal lstPeriod() As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.DeletePeriod
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeletePeriod(lstPeriod)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region
#Region "Import đánh giá ABC"
        Public Function GET_LIST_YEAR() As DataTable Implements ServiceContracts.IPerformanceBusiness.GET_LIST_YEAR
            Try
                Using rep As New PerformanceRepository
                    Return rep.GET_LIST_YEAR()
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GET_PERIOD_BY_YEAR(ByVal P_YEAR As Integer) As DataTable Implements ServiceContracts.IPerformanceBusiness.GET_PERIOD_BY_YEAR
            Try
                Using rep As New PerformanceRepository
                    Return rep.GET_PERIOD_BY_YEAR(P_YEAR)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GET_DATE_BY_PERIOD(ByVal P_PERIOD_ID As Integer) As DataTable Implements ServiceContracts.IPerformanceBusiness.GET_DATE_BY_PERIOD
            Try
                Using rep As New PerformanceRepository
                    Return rep.GET_DATE_BY_PERIOD(P_PERIOD_ID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetPeEvaluatePeriod(ByVal _filter As PE_EVALUATE_PERIODDTO,
                                         ByVal _param As ParamDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PE_EVALUATE_PERIODDTO) Implements ServiceContracts.IPerformanceBusiness.GetPeEvaluatePeriod
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetPeEvaluatePeriod(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function EXPORT_EVALUATE_ABC(ByVal P_PERIOD_ID As Integer) As DataSet Implements ServiceContracts.IPerformanceBusiness.EXPORT_EVALUATE_ABC
            Try
                Using rep As New PerformanceRepository
                    Return rep.EXPORT_EVALUATE_ABC(P_PERIOD_ID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function INPORT_EVALUATE_ABC(ByVal P_DOCXML As String, ByVal P_PERIOD_ID As Decimal, ByVal P_USER As String) As Boolean _
         Implements ServiceContracts.IPerformanceBusiness.INPORT_EVALUATE_ABC
            Using rep As New PerformanceRepository
                Try
                    Return rep.INPORT_EVALUATE_ABC(P_DOCXML, P_PERIOD_ID, P_USER)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

    End Class
End Namespace

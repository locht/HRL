Imports PerformanceBusiness.ServiceContracts
Imports PerformanceDAL
Imports Framework.Data
Imports System.Collections.Generic

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace PerformanceBusiness.ServiceImplementations
    Partial Class PerformanceBusiness

#Region "PE_PERSONAL Quản lý dữ liệu đánh giá cá nhân"
        Public Function GetEmployeeImport(ByVal group_obj_id As Decimal, ByVal period_id As Decimal, ByVal log As UserLog) As DataSet Implements ServiceContracts.IPerformanceBusiness.GetEmployeeImport
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetEmployeeImport(group_obj_id, period_id, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetCriteriaImport(ByVal group_obj_id As Decimal) As DataSet Implements ServiceContracts.IPerformanceBusiness.GetCriteriaImport
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetCriteriaImport(group_obj_id)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ImportEmployeeAssessment(ByVal dtData As DataTable,
                                                 ByVal periodID As Decimal,
                                                 ByVal group_obj_ID As Decimal,
                                                 ByVal log As UserLog) As Boolean Implements ServiceContracts.IPerformanceBusiness.ImportEmployeeAssessment
            Try
                Using rep As New PerformanceRepository
                    Return rep.ImportEmployeeAssessment(dtData, periodID, group_obj_ID, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetEmployeeImportAssessment(ByVal _param As ParamDTO,
                                                ByVal obj As PE_EMPLOYEE_ASSESSMENT_DTLDTO,
                                                ByVal P_PAGE_INDEX As Decimal,
                                                ByVal P_PAGE_SIZE As Decimal,
                                                ByVal log As UserLog) As DataSet Implements ServiceContracts.IPerformanceBusiness.GetEmployeeImportAssessment
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetEmployeeImportAssessment(_param, obj, P_PAGE_INDEX, P_PAGE_SIZE, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
#End Region

#Region "PE_ORGANIZATION Đánh giá đơn vị"
        Public Function GetPe_Organization(ByVal _filter As PE_ORGANIZATIONDTO,
                            ByVal _param As ParamDTO,
                            Optional ByVal PageIndex As Integer = 0,
                            Optional ByVal PageSize As Integer = Integer.MaxValue,
                            Optional ByRef Total As Integer = 0,
                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                            Optional ByVal log As UserLog = Nothing) As List(Of PE_ORGANIZATIONDTO) Implements ServiceContracts.IPerformanceBusiness.GetPe_Organization
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetPe_Organization(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetPe_OrganizationByID(ByVal _filter As PE_ORGANIZATIONDTO) As PE_ORGANIZATIONDTO Implements ServiceContracts.IPerformanceBusiness.GetPe_OrganizationByID
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetPe_OrganizationByID(_filter)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertPe_Organization(ByVal objData As PE_ORGANIZATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.InsertPe_Organization
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertPe_Organization(objData, log, gID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyPe_Organization(ByVal objData As PE_ORGANIZATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.ModifyPe_Organization
            Try
                Using rep As New PerformanceRepository
                    Return rep.ModifyPe_Organization(objData, log, gID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeletePe_Organization(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPerformanceBusiness.DeletePe_Organization
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeletePe_Organization(lstID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region


#Region "Assessment"
        Public Function GetAssessment(ByVal _filter As AssessmentDTO, ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentDTO) Implements ServiceContracts.IPerformanceBusiness.GetAssessment
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetAssessment(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function ModifyAssessment(ByVal objAssessment As AssessmentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.ModifyAssessment
            Try
                Using rep As New PerformanceRepository
                    Return rep.ModifyAssessment(objAssessment, log, gID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function UpdateStatusEmployeeAssessment(ByVal obj As AssessmentDTO,
                                                      ByVal log As UserLog) As Boolean Implements ServiceContracts.IPerformanceBusiness.UpdateStatusEmployeeAssessment
            Try
                Using rep As New PerformanceRepository
                    Return rep.UpdateStatusEmployeeAssessment(obj, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetListEmployeePortal(ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO) Implements ServiceContracts.IPerformanceBusiness.GetListEmployeePortal
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetListEmployeePortal(_filter)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetTotalPointRating(ByVal obj As AssessmentDTO) As DataTable Implements ServiceContracts.IPerformanceBusiness.GetTotalPointRating
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetTotalPointRating(obj)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
#End Region

#Region "PORTAL QL PKI cua nhan vien"

        Public Function GetAssessmentDirect(ByVal _empId As System.Decimal, ByVal _year As Decimal?, ByVal _status As Decimal?) As List(Of AssessmentDirectDTO) _
    Implements ServiceContracts.IPerformanceBusiness.GetAssessmentDirect
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetAssessmentDirect(_empId, _year, _status)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_DM_STATUS() As DataTable _
    Implements ServiceContracts.IPerformanceBusiness.GET_DM_STATUS
            Using rep As New PerformanceRepository
                Try
                    Return rep.GET_DM_STATUS()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function CHECK_APP(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable _
   Implements ServiceContracts.IPerformanceBusiness.CHECK_APP
            Using rep As New PerformanceRepository
                Try
                    Return rep.CHECK_APP(_empId, __PE_PERIOD_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_PE_ASSESSMENT_HISTORY(ByVal _Id As Decimal) As DataTable _
  Implements ServiceContracts.IPerformanceBusiness.GET_PE_ASSESSMENT_HISTORY
            Using rep As New PerformanceRepository
                Try
                    Return rep.GET_PE_ASSESSMENT_HISTORY(_Id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function CHECK_APP_1(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable _
   Implements ServiceContracts.IPerformanceBusiness.CHECK_APP_1
            Using rep As New PerformanceRepository
                Try
                    Return rep.CHECK_APP_1(_empId, __PE_PERIOD_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_APP_2(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable _
  Implements ServiceContracts.IPerformanceBusiness.CHECK_APP_2
            Using rep As New PerformanceRepository
                Try
                    Return rep.CHECK_APP_2(_empId, __PE_PERIOD_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function PRI_PERFORMACE_APP(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As Boolean _
 Implements ServiceContracts.IPerformanceBusiness.PRI_PERFORMACE_APP
            Using rep As New PerformanceRepository
                Try
                    Return rep.PRI_PERFORMACE_APP(_empId, __PE_PERIOD_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function INSERT_PE_ASSESSMENT_HISTORY(ByVal P_PE_PE_ASSESSMENT_ID As Decimal,
                                                 ByVal P_RESULT_DIRECT As String,
                                                 ByVal P_ASS_DATE As Date?,
                                                 ByVal P_REMARK_DIRECT As String,
                                                 ByVal P_CREATED_BY As String,
                                                 ByVal P_CREATED_LOG As String,
                                                 ByVal P_EMPLOYEE_ID As Decimal,
                                                 ByVal P_SIGN_ID As Decimal) As Boolean _
Implements ServiceContracts.IPerformanceBusiness.INSERT_PE_ASSESSMENT_HISTORY
            Using rep As New PerformanceRepository
                Try
                    Return rep.INSERT_PE_ASSESSMENT_HISTORY(P_PE_PE_ASSESSMENT_ID, P_RESULT_DIRECT, P_ASS_DATE, P_REMARK_DIRECT, P_CREATED_BY, P_CREATED_LOG, P_EMPLOYEE_ID, P_SIGN_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function PRI_PERFORMACE_PROCESS(ByVal P_EMPLOYEE_APP_ID As Decimal, ByVal P_EMPLOYEE_ID As Decimal,
                                           ByVal P_PE_PERIOD_ID As Decimal, ByVal P_STATUS_ID As Decimal,
                                           ByVal P_NOTES As String) As Boolean _
Implements ServiceContracts.IPerformanceBusiness.PRI_PERFORMACE_PROCESS
            Using rep As New PerformanceRepository
                Try
                    Return rep.PRI_PERFORMACE_PROCESS(P_EMPLOYEE_APP_ID, P_EMPLOYEE_ID, P_PE_PERIOD_ID, P_STATUS_ID, P_NOTES)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateStatusAssessmentDirect(ByVal obj As Decimal,
                                                      ByVal log As UserLog) As Boolean Implements ServiceContracts.IPerformanceBusiness.UpdateStatusAssessmentDirect
            Using rep As New PerformanceRepository
                Try
                    Return rep.UpdateStatusAssessmentDirect(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetKPIAssessEmp(ByVal _empId As System.Decimal) As List(Of AssessmentDirectDTO) _
Implements ServiceContracts.IPerformanceBusiness.GetKPIAssessEmp
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetKPIAssessEmp(_empId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


#End Region

#Region "CBNS xem KPI cua nhan vien"

        Public Function GetDirectKPIEmployee(ByVal filter As AssessmentDirectDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "EMPLOYEE_ID desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of AssessmentDirectDTO) Implements ServiceContracts.IPerformanceBusiness.GetDirectKPIEmployee
            Try
                Dim rep As New PerformanceRepository
                Return rep.GetDirectKPIEmployee(filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Bieu do xep hang nhan vien"
        Public Function GetAssessRatingEmployee(ByVal filter As AssessRatingDTO) As List(Of AssessRatingDTO) Implements ServiceContracts.IPerformanceBusiness.GetAssessRatingEmployee
            Try
                Dim rep As New PerformanceRepository
                Return rep.GetAssessRatingEmployee(filter)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAssessRatingEmployeeOrg(ByVal filter As AssessRatingDTO, Optional ByVal log As UserLog = Nothing) As List(Of AssessRatingDTO) Implements ServiceContracts.IPerformanceBusiness.GetAssessRatingEmployeeOrg
            Try
                Dim rep As New PerformanceRepository
                Return rep.GetAssessRatingEmployeeOrg(filter, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

        Public Function PRINT_PE_ASSESS(ByVal empID As Decimal, ByVal period As Decimal, ByVal obj As Decimal) As DataSet _
   Implements ServiceContracts.IPerformanceBusiness.PRINT_PE_ASSESS
            Using rep As New PerformanceRepository
                Try
                    Return rep.PRINT_PE_ASSESS(empID, period, obj)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#Region "danh gia kpis"
        Public Function GetExportKPI(ByVal id As Decimal) As DataSet Implements ServiceContracts.IPerformanceBusiness.GetExportKPI
            Using rep As New PerformanceRepository
                Return rep.GetExportKPI(id)
            End Using
        End Function
        Public Function GetlistYear() As DataTable Implements ServiceContracts.IPerformanceBusiness.GetlistYear
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetlistYear()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetLstPeriod(ByVal year As Decimal) As DataTable Implements ServiceContracts.IPerformanceBusiness.GetLstPeriod
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetLstPeriod(year)
                Catch ex As Exception

                End Try
            End Using

        End Function
        Public Function GetPeriodDate(ByVal id As Decimal) As PeriodDTO Implements ServiceContracts.IPerformanceBusiness.GetPeriodDate
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetPeriodDate(id)
                Catch ex As Exception

                End Try
            End Using
        End Function
#End Region
    End Class

End Namespace

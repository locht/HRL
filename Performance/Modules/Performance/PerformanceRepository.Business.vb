Imports Performance.PerformanceBusiness
Imports Framework.UI

Partial Class PerformanceRepository
#Region "PE_PERSONAL Quản lý dữ liệu đánh giá cá nhân"
    Public Function GetEmployeeImport(ByVal group_obj_id As Decimal, ByVal period_id As Decimal) As DataSet
        Dim lstEmployee As DataSet

        Using rep As New PerformanceBusinessClient
            Try
                lstEmployee = rep.GetEmployeeImport(group_obj_id, period_id, Me.Log)
                Return lstEmployee
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCriteriaImport(ByVal group_obj_id As Decimal) As DataSet
        Dim lstData As DataSet

        Using rep As New PerformanceBusinessClient
            Try
                lstData = rep.GetCriteriaImport(group_obj_id)
                Return lstData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function ImportEmployeeAssessment(ByVal dtData As DataTable,
                                             ByVal periodID As Decimal,
                                             ByVal group_obj_ID As Decimal) As Boolean
        Dim lst As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                lst = rep.ImportEmployeeAssessment(dtData, periodID, group_obj_ID, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetEmployeeImportAssessment(ByVal _param As ParamDTO,
                                                ByVal obj As PE_EMPLOYEE_ASSESSMENT_DTLDTO,
                                                ByVal P_PAGE_INDEX As Decimal,
                                                ByVal P_PAGE_SIZE As Decimal) As DataSet
        Dim lst As DataSet

        Using rep As New PerformanceBusinessClient
            Try
                lst = rep.GetEmployeeImportAssessment(_param, obj, P_PAGE_INDEX, P_PAGE_SIZE, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "PE_ORGANIZATION Đánh giá đơn vị"
    Public Function GetPe_Organization(ByVal _filter As PE_ORGANIZATIONDTO,
                            ByVal _param As ParamDTO,
                            Optional ByVal PageIndex As Integer = 0,
                            Optional ByVal PageSize As Integer = Integer.MaxValue,
                            Optional ByRef Total As Integer = 0,
                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_ORGANIZATIONDTO)
        Dim lstData As List(Of PE_ORGANIZATIONDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstData = rep.GetPe_Organization(_filter, _param, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lstData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetPe_OrganizationByID(ByVal _filter As PE_ORGANIZATIONDTO) As PE_ORGANIZATIONDTO
        Dim lstData As PE_ORGANIZATIONDTO

        Using rep As New PerformanceBusinessClient
            Try
                lstData = rep.GetPe_OrganizationByID(_filter)
                Return lstData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertPe_Organization(ByVal objData As PE_ORGANIZATIONDTO, ByRef gID As Decimal) As Boolean
         Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertPe_Organization(objData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyPe_Organization(ByVal objData As PE_ORGANIZATIONDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ModifyPe_Organization(objData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeletePe_Organization(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeletePe_Organization(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Assessment"
    Public Function GetAssessment(ByVal _filter As AssessmentDTO, ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentDTO)
        Dim lstAssessment As List(Of AssessmentDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstAssessment = rep.GetAssessment(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstAssessment
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetAssessment(ByVal _filter As AssessmentDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentDTO)
        Dim lstAssessment As List(Of AssessmentDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstAssessment = rep.GetAssessment(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstAssessment
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function ModifyAssessment(ByVal objAssessment As AssessmentDTO, ByRef gID As Decimal) As Boolean
        Try
            Using rep As New PerformanceBusinessClient
                Return rep.ModifyAssessment(objAssessment, Me.Log, gID)
            End Using
        Catch ex As Exception

            Throw ex
        End Try
    End Function
    Public Function UpdateStatusEmployeeAssessment(ByVal obj As AssessmentDTO) As Boolean
        Try
            Using rep As New PerformanceBusinessClient
                Return rep.UpdateStatusEmployeeAssessment(obj, Me.Log)
            End Using
        Catch ex As Exception

            Throw ex
        End Try
    End Function
    Public Function GetListEmployeePortal(ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO)
        Try
            Using rep As New PerformanceBusinessClient
                Return rep.GetListEmployeePortal(_filter)
            End Using
        Catch ex As Exception

            Throw ex
        End Try
    End Function
    Public Function GetTotalPointRating(ByVal obj As AssessmentDTO) As DataTable
        Try
            Using rep As New PerformanceBusinessClient
                Return rep.GetTotalPointRating(obj)
            End Using
        Catch ex As Exception

            Throw ex
        End Try
    End Function
#End Region

#Region "PORTAL QL PKI cua nhan vien"

    Public Function GetAssessmentDirect(ByVal _empId As Decimal, ByVal _year As Decimal?, ByVal _status As Decimal?) As List(Of AssessmentDirectDTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetAssessmentDirect(_empId, _year, _status)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_DM_STATUS() As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GET_DM_STATUS()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_APP(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.CHECK_APP(_empId, __PE_PERIOD_ID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_PE_ASSESSMENT_HISTORY(ByVal _Id As Decimal) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GET_PE_ASSESSMENT_HISTORY(_Id)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_APP_1(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.CHECK_APP_1(_empId, __PE_PERIOD_ID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_APP_2(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.CHECK_APP_2(_empId, __PE_PERIOD_ID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function PRI_PERFORMACE_APP(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
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
                                                 ByVal P_SIGN_ID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.INSERT_PE_ASSESSMENT_HISTORY(P_PE_PE_ASSESSMENT_ID, P_RESULT_DIRECT, P_ASS_DATE, P_REMARK_DIRECT, P_CREATED_BY, P_CREATED_LOG, P_EMPLOYEE_ID, P_SIGN_ID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function PRI_PERFORMACE_PROCESS(ByVal P_EMPLOYEE_APP_ID As Decimal, ByVal P_EMPLOYEE_ID As Decimal,
                                           ByVal P_PE_PERIOD_ID As Decimal, ByVal P_STATUS_ID As Decimal,
                                           ByVal P_NOTES As String) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.PRI_PERFORMACE_PROCESS(P_EMPLOYEE_APP_ID, P_EMPLOYEE_ID, P_PE_PERIOD_ID, P_STATUS_ID, P_NOTES)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function UpdateStatusAssessmentDirect(ByVal obj As Decimal) As Boolean
        Try
            Using rep As New PerformanceBusinessClient
                Return rep.UpdateStatusAssessmentDirect(obj, Me.Log)
            End Using
        Catch ex As Exception

            Throw ex
        End Try
    End Function


    Public Function GetKPIAssessEmp(ByVal _empId As Decimal) As List(Of AssessmentDirectDTO)
        Using rep As New PerformanceBusinessClient
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
                                    Optional ByVal Sorts As String = "EMPLOYEE_ID desc") As List(Of AssessmentDirectDTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetDirectKPIEmployee(filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "CBNS xem KPI cua nhan vien"
    Public Function GetAssessRatingEmployee(ByVal filter As AssessRatingDTO) As List(Of AssessRatingDTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetAssessRatingEmployee(filter)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetAssessRatingEmployeeOrg(ByVal filter As AssessRatingDTO) As List(Of AssessRatingDTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetAssessRatingEmployeeOrg(filter, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function


#End Region

    Public Function PRINT_PE_ASSESS(ByVal empID As Decimal, ByVal period As Decimal, ByVal obj As Decimal) As DataSet
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.PRINT_PE_ASSESS(empID, period, obj)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
#Region "danh gia kpis"
    Public Function GetExportKPI(ByVal id As Decimal) As DataSet
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetExportKPI(id)
            Catch ex As Exception

            End Try
        End Using
    End Function
    Public Function GetlistYear() As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetlistYear()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetLstPeriod(ByVal year As Decimal) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetLstPeriod(year)
            Catch ex As Exception

            End Try
        End Using

    End Function
    Public Function GetPeriod(ByVal id As Decimal) As PeriodDTO
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetPeriodDate(id)
            Catch ex As Exception

            End Try
        End Using
    End Function
#End Region
End Class

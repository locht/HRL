Imports PayrollBusiness.ServiceContracts
Imports PayrollDAL
Imports Framework.Data

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "Hold Salary"

        Public Function GetHoldSalaryList(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal log As UserLog,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAHoldSalaryDTO) Implements ServiceContracts.IPayrollBusiness.GetHoldSalaryList
            Try
                Dim rep As New PayrollRepository
                Return rep.GetHoldSalaryList(PeriodId, OrgId, IsDissolve, log, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertHoldSalary(ByVal objPeriod As List(Of PAHoldSalaryDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertHoldSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertHoldSalary(objPeriod, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteHoldSalary(ByVal lstDelete As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteHoldSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteHoldSalary(lstDelete)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "SALARYTYPE_GROUP"
        Public Function GetSalaryType_Group(ByVal _filter As SalaryType_GroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SalaryType_GroupDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryType_Group
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSalaryType_Group(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertSalaryType_Group(ByVal objSalaryType_Group As SalaryType_GroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSalaryType_Group
            Try
                Return PayrollRepositoryStatic.Instance.InsertSalaryType_Group(objSalaryType_Group, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifySalaryType_Group(ByVal objSalaryType_Group As SalaryType_GroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySalaryType_Group
            Try
                Return PayrollRepositoryStatic.Instance.ModifySalaryType_Group(objSalaryType_Group, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateSalaryType_Group(ByVal _validate As SalaryType_GroupDTO) Implements ServiceContracts.IPayrollBusiness.ValidateSalaryType_Group
            Try
                Return PayrollRepositoryStatic.Instance.ValidateSalaryType_Group(_validate)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ActiveSalaryType_Group(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveSalaryType_Group
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveSalaryType_Group(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteSalaryType_GroupStatus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSalaryType_GroupStatus
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteSalaryType_GroupStatus(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteSalaryType_Group(ByVal lstSalaryType_Group() As SalaryType_GroupDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSalaryType_Group
            Try
                Return PayrollRepositoryStatic.Instance.DeleteSalaryType_Group(lstSalaryType_Group)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
#Region "SalaryPlanning"

        Public Function GetSalaryPlanning(ByVal _filter As PASalaryPlanningDTO,
                                          ByVal _param As ParamDTO,
                                          Optional ByVal log As UserLog = Nothing) As DataTable _
                                    Implements ServiceContracts.IPayrollBusiness.GetSalaryPlanning
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.GetSalaryPlanning(_filter, _param, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetSalaryPlanningByID(ByVal _filter As PASalaryPlanningDTO) As PASalaryPlanningDTO _
            Implements ServiceContracts.IPayrollBusiness.GetSalaryPlanningByID
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.GetSalaryPlanningByID(_filter)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ImportSalaryPlanning(ByVal dtData As DataTable, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ImportSalaryPlanning
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.ImportSalaryPlanning(dtData, log, gID)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertSalaryPlanning(ByVal objSalaryPlanning As PASalaryPlanningDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSalaryPlanning
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.InsertSalaryPlanning(objSalaryPlanning, log, gID)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifySalaryPlanning(ByVal objSalaryPlanning As PASalaryPlanningDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySalaryPlanning
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.ModifySalaryPlanning(objSalaryPlanning, log, gID)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteSalaryPlanning(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSalaryPlanning
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.DeleteSalaryPlanning(lstID)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTitleByOrgList(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IPayrollBusiness.GetTitleByOrgList
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.GetTitleByOrgList(orgID, sLang, isBlank)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetSalaryPlanningImport(ByVal org_id As Decimal, ByVal log As UserLog) As DataSet _
            Implements ServiceContracts.IPayrollBusiness.GetSalaryPlanningImport
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.GetSalaryPlanningImport(org_id, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region

#Region "SalaryTracker"

        Public Function GetSalaryTracker(ByVal _filter As PASalaryPlanningDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByVal log As UserLog = Nothing) As DataSet _
                                    Implements ServiceContracts.IPayrollBusiness.GetSalaryTracker
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.GetSalaryTracker(_filter, _param, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetSalaryEmpTracker(ByVal _filter As PASalaryPlanningDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByVal log As UserLog = Nothing) As DataSet _
                                    Implements ServiceContracts.IPayrollBusiness.GetSalaryEmpTracker
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.GetSalaryEmpTracker(_filter, _param, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region
    End Class
End Namespace


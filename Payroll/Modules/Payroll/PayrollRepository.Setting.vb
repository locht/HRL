Imports Framework.UI
Imports Payroll.PayrollBusiness

Partial Public Class PayrollRepository

#Region "HoldSalary"

    Public Function GetHoldSalaryList(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAHoldSalaryDTO)
        Dim lstHoldSalary As List(Of PAHoldSalaryDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstHoldSalary = rep.GetHoldSalaryList(PeriodId, OrgId, IsDissolve, Me.Log, PageIndex, PageSize, Total, Sorts)
                Return lstHoldSalary
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertHoldSalary(ByVal objPeriod As List(Of PAHoldSalaryDTO)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertHoldSalary(objPeriod, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteHoldSalary(ByVal lstDelete As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteHoldSalary(lstDelete)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "SALARYTYPE_GROUP"

    Public Function GetSalaryType_Group(ByVal _filter As SalaryType_GroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SalaryType_GroupDTO)
        Using rep As New PayrollBusinessClient

            Try
                Return rep.GetSalaryType_Group(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertSalaryType_Group(ByVal objSalaryType_Group As SalaryType_GroupDTO,ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSalaryType_Group(objSalaryType_Group, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    
    Public Function ModifySalaryType_Group(ByVal objSalaryType_Group As SalaryType_GroupDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySalaryType_Group(objSalaryType_Group, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateSalaryType_Group(ByVal _validate As SalaryType_GroupDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateSalaryType_Group(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveSalaryType_Group(ByVal lstID As List(Of Decimal), ByVal bActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveSalaryType_Group(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteSalaryType_GroupStatus(ByVal lstID As List(Of Decimal), ByVal bActive As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSalaryType_GroupStatus(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteSalaryType_Group(ByVal lstSalaryType_Group As List(Of SalaryType_GroupDTO)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSalaryType_Group(lstSalaryType_Group)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "SalaryPlanning"

    Public Function GetSalaryPlanning(ByVal _filter As PASalaryPlanningDTO,
                                      ByVal _param As ParamDTO) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryPlanning(_filter, _param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryPlanningByID(ByVal _filter As PASalaryPlanningDTO) As PASalaryPlanningDTO
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryPlanningByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertSalaryPlanning(ByVal objSalaryPlanning As PASalaryPlanningDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSalaryPlanning(objSalaryPlanning, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ImportSalaryPlanning(ByVal dtData As DataTable, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ImportSalaryPlanning(dtData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifySalaryPlanning(ByVal objSalaryPlanning As PASalaryPlanningDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySalaryPlanning(objSalaryPlanning, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteSalaryPlanning(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSalaryPlanning(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetTitleByOrgList(ByVal orgID As Decimal, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New PayrollBusinessClient
            Try
                dtData = rep.GetTitleByOrgList(orgID, Common.Common.SystemLanguage.Name, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryPlanningImport(ByVal org_id As Decimal) As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryPlanningImport(org_id, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "SalaryTracker"

    Public Function GetSalaryTracker(ByVal _filter As PASalaryPlanningDTO,
                                      ByVal _param As ParamDTO) As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryTracker(_filter, _param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryEmpTracker(ByVal _filter As PASalaryPlanningDTO,
                                      ByVal _param As ParamDTO) As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryEmpTracker(_filter, _param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region
End Class

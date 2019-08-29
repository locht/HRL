﻿Imports RecruitmentDAL
Imports Framework.Data
Imports System.Collections.Generic
Imports RecruitmentBusiness.ServiceContracts


' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace RecruitmentBusiness.ServiceImplementations
    Partial Class RecruitmentBusiness

#Region "danh muc phuong xa"
        Public Function GetWardList(ByVal districtID As Decimal, ByVal isBlank As Boolean) As DataTable _
           Implements ServiceContracts.IRecruitmentBusiness.GetWardList
                Try
                Dim dtdata As DataTable
                dtdata = RecruitmentRepositoryStatic.Instance.GetWardList(districtID, isBlank)
                Return dtdata
                Catch ex As Exception
                    Throw ex
                End Try
        End Function
#End Region
#Region "CostCenter"

        Public Function GetCostCenter(ByVal _filter As CostCenterDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CostCenterDTO) Implements ServiceContracts.IRecruitmentBusiness.GetCostCenter
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetCostCenter(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.InsertCostCenter
            Try
                Return RecruitmentRepositoryStatic.Instance.InsertCostCenter(objCostCenter, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateCostCenter(ByVal objCostCenter As CostCenterDTO) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ValidateCostCenter
            Try
                Return RecruitmentRepositoryStatic.Instance.ValidateCostCenter(objCostCenter)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ModifyCostCenter
            Try
                Return RecruitmentRepositoryStatic.Instance.ModifyCostCenter(objCostCenter, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveCostCenter(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ActiveCostCenter
            Try
                Return RecruitmentRepositoryStatic.Instance.ActiveCostCenter(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteCostCenter(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IRecruitmentBusiness.DeleteCostCenter
            Try
                Return RecruitmentRepositoryStatic.Instance.DeleteCostCenter(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Examsdtl"

        Public Function GetExamsDtl(ByVal _filter As ExamsDtlDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EXAMS_ORDER") As List(Of ExamsDtlDTO) Implements ServiceContracts.IRecruitmentBusiness.GetExamsDtl
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetExamsDtl(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function UpdateExamsDtl(ByVal objExams As ExamsDtlDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IRecruitmentBusiness.UpdateExamsDtl
            Try
                Return RecruitmentRepositoryStatic.Instance.UpdateExamsDtl(objExams, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteExamsDtl(ByVal obj As ExamsDtlDTO) As Boolean Implements ServiceContracts.IRecruitmentBusiness.DeleteExamsDtl
            Try
                Return RecruitmentRepositoryStatic.Instance.DeleteExamsDtl(obj)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

    End Class
End Namespace
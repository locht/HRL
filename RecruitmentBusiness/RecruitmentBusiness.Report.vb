﻿Imports RecruitmentDAL
Imports Framework.Data
Imports System.Collections.Generic
Imports RecruitmentBusiness.ServiceContracts


' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace RecruitmentBusiness.ServiceImplementations
    Partial Class RecruitmentBusiness

#Region "BAO CAO"
        Public Function ExportReport(ByVal sPkgName As String,
                                 ByVal sStartDate As Date?,
                                 ByVal sEndDate As Date?,
                                 ByVal sOrg As String,
                                 ByVal IsDissolve As Integer,
                                 ByVal sUserName As String,
                                 ByVal sLang As String) As DataSet Implements ServiceContracts.IRecruitmentBusiness.ExportReport
            Try
                Return RecruitmentRepositoryStatic.Instance.ExportReport(sPkgName, sStartDate, sEndDate, sOrg, IsDissolve, sUserName, sLang)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                ByVal log As UserLog,
                                Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO) Implements ServiceContracts.IRecruitmentBusiness.GetReportById
                Try

                    Return RecruitmentRepositoryStatic.Instance.GetReportById(_filter, PageIndex, PageSize, Total, log, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
        End Function

#End Region



    End Class

End Namespace
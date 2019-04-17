Imports CommonBusiness.ServiceContracts
Imports CommonDAL
Imports Framework.Data
Imports System.Collections.Generic
Imports Framework.Data.SystemConfig

Namespace CommonBusiness.ServiceImplementations
    Partial Public Class CommonBusiness
        Implements ICommonBusiness

#Region "Process Setup"

        Public Function GetApproveProcessList() As List(Of ApproveProcessDTO) _
            Implements ICommonBusiness.GetApproveProcessList
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveProcess
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveProcess(ByVal processId As Decimal) As ApproveProcessDTO _
            Implements ICommonBusiness.GetApproveProcess
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveProcess(processId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertApproveProcess(ByVal item As ApproveProcessDTO, ByVal log As UserLog) As Boolean _
            Implements ICommonBusiness.InsertApproveProcess
            Using rep As New CommonRepository
                Try
                    Return rep.InsertApproveProcess(item)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateProcess(ByVal item As ApproveProcessDTO, ByVal log As UserLog) As Boolean _
            Implements ICommonBusiness.UpdateApproveProcess
            Using rep As New CommonRepository
                Try
                    Return rep.UpdateApproveProcess(item)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateProcessStatus(ByVal itemUpdates As List(Of Decimal), ByVal status As String) As Boolean _
            Implements ICommonBusiness.UpdateApproveProcessStatus
            Using rep As New CommonRepository
                Try
                    Return rep.UpdateApproveProcessStatus(itemUpdates, status)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Approve Setup"

        Public Function GetApproveSetup(ByVal id As Decimal) As ApproveSetupDTO _
            Implements ICommonBusiness.GetApproveSetup
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveSetup(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveSetupByEmployee(ByVal employeeId As Decimal,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupDTO) _
                                    Implements ICommonBusiness.GetApproveSetupByEmployee
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveSetupByEmployee(employeeId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveSetupByOrg(ByVal orgId As Decimal,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupDTO) _
                                    Implements ICommonBusiness.GetApproveSetupByOrg
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveSetupByOrg(orgId, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertApproveSetup(ByVal item As ApproveSetupDTO, ByVal log As UserLog) As Boolean _
            Implements ICommonBusiness.InsertApproveSetup
            Using rep As New CommonRepository
                Try
                    Return rep.InsertApproveSetup(item, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateApproveSetup(ByVal item As ApproveSetupDTO, ByVal log As UserLog) As Boolean _
            Implements ICommonBusiness.UpdateApproveSetup
            Using rep As New CommonRepository
                Try
                    Return rep.UpdateApproveSetup(item, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteApproveSetup(ByVal itemDeletes As List(Of Decimal)) As Boolean _
            Implements ICommonBusiness.DeleteApproveSetup
            Using rep As New CommonRepository
                Try
                    Return rep.DeleteApproveSetup(itemDeletes)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function IsExistSetupByDate(ByVal itemCheck As ApproveSetupDTO,
                                           Optional ByVal idExclude As Decimal? = Nothing) As Boolean _
                                       Implements ICommonBusiness.IsExistSetupByDate
            Using rep As New CommonRepository
                Try
                    Return rep.IsExistSetupByDate(itemCheck, idExclude)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Approve Template"

        Public Function InsertApproveTemplate(ByVal item As ApproveTemplateDTO,
                                              ByVal log As UserLog) As Boolean _
            Implements ICommonBusiness.InsertApproveTemplate
            Using rep As New CommonRepository
                Try
                    Return rep.InsertApproveTemplate(item, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateApproveTemplate(ByVal item As ApproveTemplateDTO,
                                              ByVal log As UserLog) As Boolean _
                                          Implements ICommonBusiness.UpdateApproveTemplate
            Using rep As New CommonRepository
                Try
                    Return rep.UpdateApproveTemplate(item, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveTemplate(ByVal id As Decimal) As ApproveTemplateDTO _
            Implements ICommonBusiness.GetApproveTemplate
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveTemplate(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveTemplateList() As List(Of ApproveTemplateDTO) _
            Implements ICommonBusiness.GetApproveTemplateList
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveTemplate
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteApproveTemplate(ByVal itemDeletes As List(Of Decimal)) As Boolean _
            Implements ICommonBusiness.DeleteApproveTemplate
            Using rep As New CommonRepository
                Try
                    Return rep.DeleteApproveTemplate(itemDeletes)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function IsApproveTemplateUsed(ByVal templateID As Decimal) As Boolean _
            Implements ICommonBusiness.IsApproveTemplateUsed
            Using rep As New CommonRepository
                Try
                    Return rep.IsApproveTemplateUsed(templateID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Approve Template Detail"

        Public Function InsertApproveTemplateDetail(ByVal item As ApproveTemplateDetailDTO,
                                                    ByVal log As UserLog) As Boolean _
                                                Implements ICommonBusiness.InsertApproveTemplateDetail
            Using rep As New CommonRepository
                Try
                    Return rep.InsertApproveTemplateDetail(item, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateApproveTemplateDetail(ByVal item As ApproveTemplateDetailDTO,
                                                    ByVal log As UserLog) As Boolean _
                                                Implements ICommonBusiness.UpdateApproveTemplateDetail
            Using rep As New CommonRepository
                Try
                    Return rep.UpdateApproveTemplateDetail(item, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveTemplateDetail(ByVal id As Decimal) As ApproveTemplateDetailDTO _
            Implements ICommonBusiness.GetApproveTemplateDetail
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveTemplateDetail(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveTemplateDetailList(ByVal templateId As Decimal) As List(Of ApproveTemplateDetailDTO) _
            Implements ICommonBusiness.GetApproveTemplateDetailList
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveTemplateDetailList(templateId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteApproveTemplateDetail(ByVal itemDeeltes As List(Of Decimal)) As Boolean _
            Implements ICommonBusiness.DeleteApproveTemplateDetail
            Using rep As New CommonRepository
                Try
                    Return rep.DeleteApproveTemplateDetail(itemDeeltes)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckLevelInsert(ByVal level As Decimal,
                                         ByVal idExclude As Decimal,
                                         ByVal idTemplate As Decimal) As Boolean _
                                     Implements ICommonBusiness.CheckLevelInsert
            Using rep As New CommonRepository
                Try
                    Return rep.CheckLevelInsert(level, idExclude, idTemplate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Setup Approve Ext - Thiết lập người thay thế"

        Public Function InsertApproveSetupExt(ByVal item As ApproveSetupExtDTO,
                                              ByVal log As UserLog) As Boolean _
                                          Implements ICommonBusiness.InsertApproveSetupExt
            Using rep As New CommonRepository
                Try
                    Return rep.InsertApproveSetupExt(item, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateApproveSetupExt(ByVal item As ApproveSetupExtDTO,
                                              ByVal log As UserLog) As Boolean _
                                          Implements ICommonBusiness.UpdateApproveSetupExt
            Using rep As New CommonRepository
                Try
                    Return rep.UpdateApproveSetupExt(item, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveSetupExt(ByVal id As Decimal) As ApproveSetupExtDTO _
            Implements ICommonBusiness.GetApproveSetupExt
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveSetupExt(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveSetupExtList(ByVal employeeId As Decimal,
                                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupExtDTO) _
                                           Implements ICommonBusiness.GetApproveSetupExtList
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveSetupExtList(employeeId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteApproveSetupExt(ByVal itemDeletes As List(Of Decimal)) As Boolean _
            Implements ICommonBusiness.DeleteApproveSetupExt
            Using rep As New CommonRepository
                Try
                    Return rep.DeleteApproveSetupExt(itemDeletes)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function IsExistSetupExtByDate(ByVal itemCheck As ApproveSetupExtDTO,
                                              Optional ByVal idExclude As Decimal? = Nothing) As Boolean _
                                          Implements ICommonBusiness.IsExistSetupExtByDate
            Using rep As New CommonRepository
                Try
                    Return rep.IsExistSetupExtByDate(itemCheck, idExclude)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

        Public Function GetApproveUsers(ByVal employeeId As Decimal,
                                        ByVal processCode As String) As List(Of ApproveUserDTO) _
                                    Implements ICommonBusiness.GetApproveUsers
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveUsers(employeeId, processCode)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetListEmployee(ByVal _orgIds As List(Of Decimal)) As List(Of EmployeeDTO) _
            Implements ICommonBusiness.GetListEmployee
            Using rep As New CommonRepository
                Try
                    Return rep.GetListEmployee(_orgIds)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


    End Class
End Namespace
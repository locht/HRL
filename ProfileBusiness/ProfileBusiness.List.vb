Imports ProfileBusiness.ServiceContracts
Imports ProfileDAL
Imports Framework.Data
Imports System.ServiceModel.Activation
Imports System.Reflection
Imports System.Configuration

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness

#Region "ngach, bac, thang luong"

        Function GetSalaryGroupCombo(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IProfileBusiness.GetSalaryGroupCombo
            Using rep As New ProfileRepository
                Try
                    Return rep.GetSalaryGroupCombo(dateValue, isBlank)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetSalaryRankCombo(ByVal SalaryLevel As Decimal, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IProfileBusiness.GetSalaryRankCombo
            Using rep As New ProfileRepository
                Try
                    Return rep.GetSalaryRankCombo(SalaryLevel, isBlank)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetSalaryLevelCombo(ByVal SalaryGroup As Decimal, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IProfileBusiness.GetSalaryLevelCombo
            Using rep As New ProfileRepository
                Try
                    Return rep.GetSalaryLevelCombo(SalaryGroup, isBlank)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetSalaryLevelComboNotByGroup(ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IProfileBusiness.GetSalaryLevelComboNotByGroup
            Using rep As New ProfileRepository
                Try
                    Return rep.GetSalaryLevelComboNotByGroup(isBlank)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Danh muc"
        Function GetEmpInfomations(ByVal orgIDs As List(Of Decimal),
                                     ByVal _filter As EmployeeDTO,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO) Implements ServiceContracts.IProfileBusiness.GetEmpInfomations
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmpInfomations(orgIDs, _filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Hoadm"

#Region "Title"

        Public Function GetTitle(ByVal _filter As TitleDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetTitle
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetTitle(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertTitle(ByVal objTitle As TitleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertTitle
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertTitle(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateTitle(ByVal objTitle As TitleDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateTitle
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateTitle(objTitle)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyTitle(ByVal objTitle As TitleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyTitle
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyTitle(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveTitle(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ActiveTitle
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveTitle(lstID, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteTitle(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteTitle
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteTitle(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTitleByID(ByVal sID As Decimal) As List(Of ProfileDAL.TitleDTO) _
            Implements ServiceContracts.IProfileBusiness.GetTitleByID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetTitleByID(sID)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTitleID(ByVal sID As Decimal) As TitleDTO _
         Implements ServiceContracts.IProfileBusiness.GetTitleID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetTitleID(sID)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "TitleConcurrent"
        Public Function GetTitleConcurrent1(ByVal _filter As TitleConcurrentDTO,
                                      ByVal PageIndex As Integer,
                                      ByVal PageSize As Integer,
                                      ByRef Total As Integer, ByVal _param As ParamDTO,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc",
                                      Optional ByVal log As UserLog = Nothing) As List(Of TitleConcurrentDTO) _
                                  Implements ServiceContracts.IProfileBusiness.GetTitleConcurrent1
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetTitleConcurrent1(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTitleConcurrent(ByVal _filter As TitleConcurrentDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleConcurrentDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetTitleConcurrent
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetTitleConcurrent(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertTitleConcurrent(ByVal objTitleConcurrent As TitleConcurrentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertTitleConcurrent
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertTitleConcurrent(objTitleConcurrent, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyTitleConcurrent(ByVal objTitleConcurrent As TitleConcurrentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyTitleConcurrent
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyTitleConcurrent(objTitleConcurrent, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


        Public Function DeleteTitleConcurrent(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteTitleConcurrent
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteTitleConcurrent(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "ContractType"

        Public Function GetContractType(ByVal _filter As ContractTypeDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ContractTypeDTO) Implements ServiceContracts.IProfileBusiness.GetContractType
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetContractType(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


        Public Function InsertContractType(ByVal objContractType As ContractTypeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertContractType
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertContractType(objContractType, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateContractType(ByVal objContractType As ContractTypeDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateContractType
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateContractType(objContractType)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyContractType(ByVal objContractType As ContractTypeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyContractType
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyContractType(objContractType, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveContractType(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ActiveContractType
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveContractType(lstID, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteContractType(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteContractType
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteContractType(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "WelfareList"
        Public Function GetWelfareList(ByVal _filter As WelfareListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WelfareListDTO) Implements ServiceContracts.IProfileBusiness.GetWelfareList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetWelfareList(_filter, PageIndex, PageSize, Total, log, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertWelfareList(ByVal objWelfareList As WelfareListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertWelfareList
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertWelfareList(objWelfareList, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateWelfareList(ByVal objWelfareList As WelfareListDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateWelfareList
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateWelfareList(objWelfareList)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyWelfareList(ByVal objWelfareList As WelfareListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyWelfareList
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyWelfareList(objWelfareList, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveWelfareList(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ActiveWelfareList
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveWelfareList(lstID, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteWelfareList(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteWelfareList
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteWelfareList(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "AllowanceList"
        Public Function GetAllowanceList(ByVal _filter As AllowanceListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceListDTO) Implements ServiceContracts.IProfileBusiness.GetAllowanceList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetAllowanceList(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAllowanceList(ByVal objAllowanceList As AllowanceListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertAllowanceList
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertAllowanceList(objAllowanceList, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAllowanceList(ByVal objAllowanceList As AllowanceListDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateAllowanceList
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateAllowanceList(objAllowanceList)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAllowanceList(ByVal objAllowanceList As AllowanceListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyAllowanceList
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyAllowanceList(objAllowanceList, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAllowanceList(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ActiveAllowanceList
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveAllowanceList(lstID, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAllowanceList(ByVal objAllowanceList() As AllowanceListDTO, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteAllowanceList
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteAllowanceList(objAllowanceList, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "RelationshipList"
        Public Function GetRelationshipGroupList() As DataTable Implements ServiceContracts.IProfileBusiness.GetRelationshipGroupList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetRelationshipGroupList()
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetRelationshipList(ByVal _filter As RelationshipListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RelationshipListDTO) Implements ServiceContracts.IProfileBusiness.GetRelationshipList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetRelationshipList(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertRelationshipList(ByVal objRelationshipList As RelationshipListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertRelationshipList
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertRelationshipList(objRelationshipList, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateRelationshipList(ByVal objRelationshipList As RelationshipListDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateRelationshipList
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateRelationshipList(objRelationshipList)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyRelationshipList(ByVal objRelationshipList As RelationshipListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyRelationshipList
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyRelationshipList(objRelationshipList, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveRelationshipList(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ActiveRelationshipList
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveRelationshipList(lstID, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteRelationshipList(ByVal objRelationshipList() As RelationshipListDTO, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteRelationshipList
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteRelationshipList(objRelationshipList, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Organization"
        Public Function GetTreeOrgByID(ByVal ID As Decimal) As OrganizationTreeDTO Implements ServiceContracts.IProfileBusiness.GetTreeOrgByID
            Using rep As New ProfileRepository
                Try
                    Dim obj = rep.GetTreeOrgByID(ID)
                    Return obj
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOrganization(ByVal sACT As String) As List(Of OrganizationDTO) Implements ServiceContracts.IProfileBusiness.GetOrganization
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetOrganization(sACT)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetOrganizationByID(ByVal ID As Decimal) As OrganizationDTO Implements ServiceContracts.IProfileBusiness.GetOrganizationByID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetOrganizationByID(ID)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertOrganization(ByVal objOrganization As OrganizationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertOrganization
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertOrganization(objOrganization, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateOrganization(ByVal objOrganization As OrganizationDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateOrganization
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateOrganization(objOrganization)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateCostCenterCode(ByVal objOrganization As OrganizationDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateCostCenterCode
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateCostCenterCode(objOrganization)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckEmployeeInOrganization(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IProfileBusiness.CheckEmployeeInOrganization
            Using rep As New ProfileRepository
                Try

                    Return rep.CheckEmployeeInOrganization(lstID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyOrganization(ByVal objOrganization As OrganizationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyOrganization
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyOrganization(objOrganization, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyOrganizationPath(ByVal lstPath As List(Of OrganizationPathDTO)) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyOrganizationPath
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyOrganizationPath(lstPath)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveOrganization(ByVal objOrganization() As OrganizationDTO, ByVal sActive As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ActiveOrganization
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveOrganization(objOrganization, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "OrgTitle"

        Public Function GetOrgTitle(ByVal filter As OrgTitleDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OrgTitleDTO) Implements ServiceContracts.IProfileBusiness.GetOrgTitle
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetOrgTitle(filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertOrgTitle(ByVal objOrgTitle As List(Of OrgTitleDTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertOrgTitle
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertOrgTitle(objOrgTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckTitleInEmployee(ByVal lstID As List(Of Decimal), ByVal orgID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.CheckTitleInEmployee
            Using rep As New ProfileRepository
                Try

                    Return rep.CheckTitleInEmployee(lstID, orgID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteOrgTitle(ByVal objOrgTitle As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteOrgTitle
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteOrgTitle(objOrgTitle, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveOrgTitle(ByVal objOrgTitle As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ActiveOrgTitle
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveOrgTitle(objOrgTitle, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region
#Region "Danh muc tham so he thong"
        Public Function ValidateOtherList(ByVal objOtherList As OtherListDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValidateOtherList
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateOtherList(objOtherList)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "Nation- Danh mục quốc gia"
        Public Function GetNation(ByVal _filter As NationDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of NationDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetNation
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetNation(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertNation(ByVal objNation As NationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertNation
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertNation(objNation, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyNation(ByVal objNation As NationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyNation
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyNation(objNation, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateNation(ByVal objNation As NationDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValidateNation
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateNation(objNation)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveNation(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ActiveNation
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveNation(lstID, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteNation(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteNation
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteNation(lstDecimals, log, strError)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Province- Danh mục tỉnh thành"

        Public Function GetProvinceByNationID(ByVal sNationID As Decimal, ByVal sACTFLG As String) As List(Of ProvinceDTO) _
            Implements ServiceContracts.IProfileBusiness.GetProvinceByNationID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetProvinceByNationID(sNationID, sACTFLG)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetProvinceByNationCode(ByVal sNationCode As String, ByVal sACTFLG As String) As List(Of ProvinceDTO) _
            Implements ServiceContracts.IProfileBusiness.GetProvinceByNationCode
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetProvinceByNationCode(sNationCode, sACTFLG)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetProvince(ByVal _filter As ProvinceDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProvinceDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetProvince
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetProvince(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertProvince(ByVal objProvince As ProvinceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertProvince
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertProvince(objProvince, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyProvince(ByVal objProvince As ProvinceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyProvince
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyProvince(objProvince, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateProvince(ByVal objProvince As ProvinceDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValidateProvince
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateProvince(objProvince)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveProvince(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ActiveProvince
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveProvince(lstID, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteProvince(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteProvince
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteProvince(lstDecimals, log, strError)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "District- Danh mục quận huyện"
        Public Function GetDistrictByProvinceID(ByVal sProvinceID As Decimal, ByVal sACTFLG As String) As List(Of DistrictDTO) _
            Implements ServiceContracts.IProfileBusiness.GetDistrictByProvinceID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetDistrictByProvinceID(sProvinceID, sACTFLG)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetDistrict(ByVal _filter As DistrictDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DistrictDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetDistrict
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetDistrict(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertDistrict(ByVal objDistrict As DistrictDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertDistrict
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertDistrict(objDistrict, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyDistrict(ByVal objDistrict As DistrictDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyDistrict
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyDistrict(objDistrict, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateDistrict(ByVal objData As DistrictDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateDistrict
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateDistrict(objData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveDistrict(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ActiveDistrict
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveDistrict(lstID, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteDistrict(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean _
           Implements ServiceContracts.IProfileBusiness.DeleteDistrict
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteDistrict(lstDecimals, log, strError)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Ward- Danh mục xã phường"
        Public Function GetWardByDistrictID(ByVal sDistrictID As Decimal, ByVal sACTFLG As String) As List(Of Ward_DTO) _
            Implements ServiceContracts.IProfileBusiness.GetWardByDistrictID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetWardByDistrictID(sDistrictID, sACTFLG)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetWard(ByVal _filter As Ward_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Ward_DTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetWard
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetWard(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateWard(ByVal objData As Ward_DTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateWard
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateWard(objData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertWard(ByVal objDistrict As Ward_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertWard
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertWard(objDistrict, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyWard(ByVal objDistrict As Ward_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyWard
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyWard(objDistrict, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveWard(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ActiveWard
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveWard(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteWard(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteWard
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteWard(lstID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region


#Region "Bank- Danh mục Ngan hang"

        Public Function GetBank(ByVal _filter As BankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BankDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetBank
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetBank(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertBank(ByVal objBank As BankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertBank
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertBank(objBank, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyBank(ByVal objBank As BankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyBank
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyBank(objBank, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateBank(ByVal objBank As BankDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValidateBank
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateBank(objBank)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveBank(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ActiveBank
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveBank(lstID, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteBank(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean _
           Implements ServiceContracts.IProfileBusiness.DeleteBank
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteBank(lstDecimals, log, strError)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "BankBranch- Danh mục chi nhanh Ngan hang"

        Public Function GetBankBranchByBankID(ByVal sBank_ID As Decimal) As List(Of BankBranchDTO) _
            Implements ServiceContracts.IProfileBusiness.GetBankBranchByBankID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetBankBranchByBankID(sBank_ID)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetBankBranch(ByVal _filter As BankBranchDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BankBranchDTO) _
                Implements ServiceContracts.IProfileBusiness.GetBankBranch
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetBankBranch(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertBankBranch(ByVal objBankBranch As BankBranchDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertBankBranch
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertBankBranch(objBankBranch, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyBankBranch(ByVal objBankBranch As BankBranchDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyBankBranch
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyBankBranch(objBankBranch, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateBankBranch(ByVal objBankBranch As BankBranchDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValidateBankBranch
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateBankBranch(objBankBranch)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveBankBranch(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ActiveBankBranch
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveBankBranch(lstID, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteBankBranch(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean _
           Implements ServiceContracts.IProfileBusiness.DeleteBankBranch
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteBankBranch(lstDecimals, log, strError)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Asset - Danh muc tai san cap phat"
        Function GetAsset(ByVal _filter As AssetDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssetDTO) _
                Implements ServiceContracts.IProfileBusiness.GetAsset
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetAsset(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAsset(ByVal objAsset As AssetDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertAsset
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertAsset(objAsset, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAsset(ByVal objAsset As AssetDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyAsset
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyAsset(objAsset, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAsset(ByVal objAsset As AssetDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValidateAsset
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateAsset(objAsset)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAsset(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ActiveAsset
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveAsset(lstID, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAsset(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean _
          Implements ServiceContracts.IProfileBusiness.DeleteAsset
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteAsset(lstDecimals, log, strError)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "StaffRank"

        Public Function GetStaffRank(ByVal _filter As StaffRankDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of StaffRankDTO) Implements ServiceContracts.IProfileBusiness.GetStaffRank
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.GetStaffRank(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertStaffRank(ByVal objStaffRank As StaffRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertStaffRank
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertStaffRank(objStaffRank, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateStaffRank(ByVal objStaffRank As StaffRankDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateStaffRank
            Using rep As New ProfileRepository
                Try
                    Return rep.ValidateStaffRank(objStaffRank)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyStaffRank(ByVal objStaffRank As StaffRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyStaffRank
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyStaffRank(objStaffRank, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveStaffRank(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal sActive As String) As Boolean Implements ServiceContracts.IProfileBusiness.ActiveStaffRank
            Using rep As New ProfileRepository
                Try
                    Return rep.ActiveStaffRank(lstID, log, sActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function DeleteStaffRank(ByVal lstStaffRank() As StaffRankDTO, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteStaffRank
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteStaffRank(lstStaffRank, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Năng lực"


#Region "CompetencyGroup"

        Public Function GetCompetencyGroup(ByVal _filter As CompetencyGroupDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyGroupDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetCompetencyGroup
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetCompetencyGroup(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertCompetencyGroup(ByVal objCompetencyGroup As CompetencyGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertCompetencyGroup
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertCompetencyGroup(objCompetencyGroup, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateCompetencyGroup(ByVal objCompetencyGroup As CompetencyGroupDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateCompetencyGroup
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateCompetencyGroup(objCompetencyGroup)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyCompetencyGroup(ByVal objCompetencyGroup As CompetencyGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyCompetencyGroup
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyCompetencyGroup(objCompetencyGroup, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveCompetencyGroup(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ActiveCompetencyGroup
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveCompetencyGroup(lstID, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteCompetencyGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteCompetencyGroup
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteCompetencyGroup(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Competency"

        Public Function GetCompetency(ByVal _filter As CompetencyDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetCompetency
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetCompetency(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertCompetency(ByVal objCompetency As CompetencyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertCompetency
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertCompetency(objCompetency, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateCompetency(ByVal objCompetency As CompetencyDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateCompetency
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateCompetency(objCompetency)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyCompetency(ByVal objCompetency As CompetencyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyCompetency
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyCompetency(objCompetency, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveCompetency(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ActiveCompetency
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveCompetency(lstID, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteCompetency(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteCompetency
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteCompetency(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "CompetencyBuild"

        Public Function GetCompetencyBuild(ByVal _filter As CompetencyBuildDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyBuildDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetCompetencyBuild
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetCompetencyBuild(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertCompetencyBuild(ByVal objCompetencyBuild As CompetencyBuildDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertCompetencyBuild
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertCompetencyBuild(objCompetencyBuild, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyCompetencyBuild(ByVal objCompetencyBuild As CompetencyBuildDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyCompetencyBuild
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyCompetencyBuild(objCompetencyBuild, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteCompetencyBuild(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteCompetencyBuild
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteCompetencyBuild(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "CompetencyStandard"

        Public Function GetCompetencyStandard(ByVal _filter As CompetencyStandardDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyStandardDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetCompetencyStandard
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetCompetencyStandard(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertCompetencyStandard(ByVal objCompetencyStandard As CompetencyStandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertCompetencyStandard
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertCompetencyStandard(objCompetencyStandard, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyCompetencyStandard(ByVal objCompetencyStandard As CompetencyStandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyCompetencyStandard
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyCompetencyStandard(objCompetencyStandard, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteCompetencyStandard(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteCompetencyStandard
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteCompetencyStandard(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "CompetencyAppendix"

        Public Function GetCompetencyAppendix(ByVal _filter As CompetencyAppendixDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyAppendixDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetCompetencyAppendix
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetCompetencyAppendix(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertCompetencyAppendix(ByVal objCompetencyAppendix As CompetencyAppendixDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertCompetencyAppendix
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertCompetencyAppendix(objCompetencyAppendix, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyCompetencyAppendix(ByVal objCompetencyAppendix As CompetencyAppendixDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyCompetencyAppendix
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyCompetencyAppendix(objCompetencyAppendix, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteCompetencyAppendix(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteCompetencyAppendix
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteCompetencyAppendix(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "CompetencyEmp"

        Public Function GetCompetencyEmp(ByVal _filter As CompetencyEmpDTO) As List(Of CompetencyEmpDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetCompetencyEmp
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetCompetencyEmp(_filter)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateCompetencyEmp(ByVal lstCom As List(Of CompetencyEmpDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.UpdateCompetencyEmp
            Using rep As New ProfileRepository
                Try

                    Return rep.UpdateCompetencyEmp(lstCom, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region


#Region "CompetencyPeriod"

        Public Function GetCompetencyPeriod(ByVal _filter As CompetencyPeriodDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyPeriodDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetCompetencyPeriod
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetCompetencyPeriod(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertCompetencyPeriod(ByVal objCompetencyPeriod As CompetencyPeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertCompetencyPeriod
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertCompetencyPeriod(objCompetencyPeriod, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyCompetencyPeriod(ByVal objCompetencyPeriod As CompetencyPeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyCompetencyPeriod
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyCompetencyPeriod(objCompetencyPeriod, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteCompetencyPeriod(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteCompetencyPeriod
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteCompetencyPeriod(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "CompetencyAssDtl"

        Public Function GetCompetencyAss(ByVal _filter As CompetencyAssDTO) As List(Of CompetencyAssDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetCompetencyAss
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetCompetencyAss(_filter)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetCompetencyAssDtl(ByVal _filter As CompetencyAssDtlDTO) As List(Of CompetencyAssDtlDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetCompetencyAssDtl
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetCompetencyAssDtl(_filter)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateCompetencyAssDtl(ByVal objAss As CompetencyAssDTO, ByVal lstCom As List(Of CompetencyAssDtlDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.UpdateCompetencyAssDtl
            Using rep As New ProfileRepository
                Try

                    Return rep.UpdateCompetencyAssDtl(objAss, lstCom, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteCompetencyAss(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
    Implements ServiceContracts.IProfileBusiness.DeleteCompetencyAss
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteCompetencyAss(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#End Region

#End Region

#Region "Danh mục bảo hộ lao động"
        Public Function GetLabourProtection(ByVal _filter As LabourProtectionDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LabourProtectionDTO) Implements ServiceContracts.IProfileBusiness.GetLabourProtection
            Using rep As New ProfileRepository
                Try

                    Return rep.GetLabourProtection(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertLabourProtection(ByVal objTitle As LabourProtectionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertLabourProtection
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertLabourProtection(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyLabourProtection(ByVal objTitle As LabourProtectionDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyLabourProtection
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyLabourProtection(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidateLabourProtection(ByVal _validate As LabourProtectionDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateLabourProtection
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateLabourProtection(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveLabourProtection(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IProfileBusiness.ActiveLabourProtection
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveLabourProtection(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteLabourProtection(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteLabourProtection
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteLabourProtection(lstID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Danh mục thônng tin lương"
        Public Function GetPeriodbyYear(ByVal year As Decimal) As List(Of ATPeriodDTO) Implements ServiceContracts.IProfileBusiness.GetPeriodbyYear
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetPeriodbyYear(year)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region
#Region "DM Khen thưởng (CommendList)"
        Public Function GetCommendList(ByVal _filter As CommendListDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendListDTO) Implements IProfileBusiness.GetCommendList
            Try
                Dim rep As New ProfileRepository
                Return rep.GetCommendList(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                Throw
            End Try
        End Function
        Public Function GetCommendListID(ByVal ID As Decimal) As List(Of CommendListDTO) Implements IProfileBusiness.GetCommendListID
            Try
                Dim rep As New ProfileRepository
                Return rep.GetCommendListID(ID)
            Catch ex As Exception
                Throw
            End Try
        End Function
        Public Function GetListCommendList(ByVal actflg As String) As List(Of CommendListDTO) Implements IProfileBusiness.GetListCommendList
            Try
                Dim rep As New ProfileRepository
                Return rep.GetListCommendList(actflg)
            Catch ex As Exception
                Throw
            End Try
        End Function
        Public Function InsertCommendList(ByVal objCommendList As CommendListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IProfileBusiness.InsertCommendList
            Try
                Dim rep As New ProfileRepository
                Return rep.InsertCommendList(objCommendList, log, gID)
            Catch ex As Exception
                Throw
            End Try
        End Function
        Public Function ModifyCommendList(ByVal objCommendList As CommendListDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IProfileBusiness.ModifyCommendList
            Try
                Dim rep As New ProfileRepository
                Return rep.ModifyCommendList(objCommendList, log, gID)
            Catch ex As Exception
                Throw
            End Try
        End Function
        Public Function ActiveCommendList(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                  ByVal log As UserLog) As Boolean Implements IProfileBusiness.ActiveCommendList
            Try
                Dim rep As New ProfileRepository
                Return rep.ActiveCommendList(lstID, sActive, log)
            Catch ex As Exception
                Throw
            End Try
        End Function
        Public Function DeleteCommendList(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean Implements IProfileBusiness.DeleteCommendList
            Try
                Dim rep As New ProfileRepository
                Return rep.DeleteCommendList(lstDecimals, log, strError)
            Catch ex As Exception
                Throw
            End Try
        End Function
        Public Function ValidateCommendList(ByVal _validate As CommendListDTO) Implements IProfileBusiness.ValidateCommendList
            Try
                Dim rep As New ProfileRepository
                Return rep.ValidateCommendList(_validate)
            Catch ex As Exception
                Throw
            End Try
        End Function
        Function GetCommendCode(ByVal id As Decimal) As String Implements IProfileBusiness.GetCommendCode
            Try
                Dim rep As New ProfileRepository
                Return rep.GetCommendCode(id)
            Catch ex As Exception
                Throw
            End Try
        End Function
#End Region
#Region "Commend_Level - Cấp khen thưởng"
        Public Function GetCommendLevel(ByVal _filter As CommendLevelDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendLevelDTO) _
                                   Implements ServiceContracts.IProfileBusiness.GetCommendLevel
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetCommendLevel(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetCommendLevelID(ByVal ID As Decimal) As CommendLevelDTO _
                                  Implements ServiceContracts.IProfileBusiness.GetCommendLevelID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetCommendLevelID(ID)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertCommendLevel(ByVal objCommendLevel As CommendLevelDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
                                 Implements ServiceContracts.IProfileBusiness.InsertCommendLevel
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.InsertCommendLevel(objCommendLevel, log, gID)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidateCommendLevel(ByVal _validate As CommendLevelDTO) _
                               Implements ServiceContracts.IProfileBusiness.ValidateCommendLevel
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.ValidateCommendLevel(_validate)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyCommendLevel(ByVal objCommendLevel As CommendLevelDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
                               Implements ServiceContracts.IProfileBusiness.ModifyCommendLevel
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.ModifyCommendLevel(objCommendLevel, log, gID)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ActiveCommendLevel(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean _
                              Implements ServiceContracts.IProfileBusiness.ActiveCommendLevel
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.ActiveCommendLevel(lstID, sActive, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteCommendLevel(ByVal lstID As List(Of Decimal)) As Boolean _
                              Implements ServiceContracts.IProfileBusiness.DeleteCommendLevel
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.DeleteCommendLevel(lstID)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Tìm kiếm kế nhiệm (Talent Pool)"

        Public Function GetTalentPool(ByVal PageIndex As Integer,
                                  ByVal PageSize As Integer,
                                  ByRef Total As Integer, ByVal _param As ParamDTO,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc",
                                  Optional ByVal log As UserLog = Nothing) As List(Of TalentPoolDTO) Implements ServiceContracts.IProfileBusiness.GetTalentPool
            Using rep As New ProfileRepository
                Try
                    Return rep.GetTalentPool(PageIndex, PageSize, Total, _param, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveTalentPool(ByVal objTalentPool As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ActiveTalentPool
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveTalentPool(objTalentPool, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertTalentPool(ByVal lstTalentPool As List(Of TalentPoolDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.InsertTalentPool
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertTalentPool(lstTalentPool, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function FILTER_TALENT_POOL(ByVal obj As FilterParamDTO, ByVal log As UserLog) As DataTable Implements ServiceContracts.IProfileBusiness.FILTER_TALENT_POOL
            Using rep As New ProfileRepository
                Try
                    Return rep.FILTER_TALENT_POOL(obj, log)
                Catch ex As Exception

                End Try
            End Using
        End Function

#End Region

#Region "Competency - Course"
        Public Function GetCourseByList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IProfileBusiness.GetCourseByList
            Try
                Dim rep As New ProfileRepository
                Return rep.GetCourseByList(sLang, isBlank)
            Catch ex As Exception
                Throw
            End Try
        End Function
#End Region

        '#Region "HomeBase"
        '        Public Function GetHomeBase(ByVal _filter As HomeBaseDTO, ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Total As Integer, Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HomeBaseDTO) Implements IProfileBusiness.GetHomeBase
        '            Try
        '                Dim rep As New ProfileRepository
        '                Dim lst = rep.GetHomeBase(_filter, PageIndex, PageSize, Total, Sorts)
        '                Return lst
        '            Catch ex As Exception
        '                Throw ex
        '            End Try
        '        End Function

        '        Public Function InsertHomeBase(ByVal objHomeBase As HomeBaseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IProfileBusiness.InsertHomeBase
        '            Try
        '                Dim rep As New ProfileRepository
        '                Return rep.InsertHomeBase(objHomeBase, log, gID)
        '            Catch ex As Exception
        '                Throw ex
        '            End Try
        '        End Function

        '        Public Function ValidateHomeBase(ByVal _validate As HomeBaseDTO) As Boolean Implements IProfileBusiness.ValidateHomeBase
        '            Try
        '                Dim rep As New ProfileRepository
        '                Return rep.ValidateHomeBase(_validate)
        '            Catch ex As Exception
        '                Throw ex
        '            End Try
        '        End Function

        '        Public Function ModifyHomeBase(ByVal objHomeBase As HomeBaseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IProfileBusiness.ModifyHomeBase
        '            Try
        '                Dim rep As New ProfileRepository
        '                Return rep.ModifyHomeBase(objHomeBase, log, gID)
        '            Catch ex As Exception

        '                Throw ex
        '            End Try
        '        End Function

        '        'Public Function ActiveHomeBase(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal sActive As String) As Boolean Implements IProfileBusiness.ActiveHomeBase
        '        '    Try
        '        '        Dim rep As New ProfileRepository
        '        '        Return rep.ActiveHomeBase(lstID, log, sActive)
        '        '    Catch ex As Exception
        '        '        Throw ex
        '        '    End Try
        '        'End Function

        '        'Public Function DeleteHomeBase(ByVal lstHomeBase As HomeBaseDTO(), ByVal log As UserLog) As Boolean Implements IProfileBusiness.DeleteHomeBase
        '        '    Try
        '        '        Dim rep As New ProfileRepository
        '        '        Return rep.DeleteHomeBase(lstHomeBase, log)
        '        '    Catch ex As Exception
        '        '        Throw ex
        '        '    End Try
        '        'End Function
        '#End Region

#Region "Location"
        Public Function GetLocationID(ByVal ID As Decimal) As LocationDTO Implements ServiceContracts.IProfileBusiness.GetLocationID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetLocationID(ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetLocation(ByVal sACT As String, ByVal lstOrgID As List(Of Decimal)) As List(Of LocationDTO) Implements ServiceContracts.IProfileBusiness.GetLocation
            Using rep As New ProfileRepository
                Try
                    Return rep.GetLocation(sACT, lstOrgID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertLocation(ByVal objLocation As LocationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertLocation
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertLocation(objLocation, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyLocation(ByVal objLocation As LocationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyLocation
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyLocation(objLocation, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveLocation(ByVal lstLocation As List(Of LocationDTO), ByVal sActive As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ActiveLocation
            Using rep As New ProfileRepository
                Try
                    Return rep.ActiveLocation(lstLocation, sActive, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveLocationID(ByVal lstLocation As LocationDTO, ByVal sActive As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ActiveLocationID
            Using rep As New ProfileRepository
                Try
                    Return rep.ActiveLocationID(lstLocation, sActive, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteLocationID(ByVal lstlocation As Decimal, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteLocationID
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteLocationID(lstlocation, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "danh mục người ký"
        Public Function GET_HU_SIGNER(ByVal _filter As SignerDTO, ByVal _param As ParamDTO, ByVal log As UserLog) As DataTable Implements ServiceContracts.IProfileBusiness.GET_HU_SIGNER
            Try
                Dim rep As New ProfileRepository
                Return rep.GET_HU_SIGNER(_filter, _param, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'them ng ki
        Public Function INSERT_HU_SIGNER(ByVal PA As SignerDTO) As Boolean Implements ServiceContracts.IProfileBusiness.INSERT_HU_SIGNER
            Try
                Dim rep As New ProfileRepository
                Return rep.INSERT_HU_SIGNER(PA)
            Catch ex As Exception

            End Try
        End Function
        'CHECK TON TAI HAY CHUA 
        Public Function CHECK_EXIT(ByVal P_ID As String, ByVal idemp As Decimal, ByVal ORG_ID As Decimal) As Decimal Implements ServiceContracts.IProfileBusiness.CHECK_EXIT
            Try
                Dim rep As New ProfileRepository
                Return rep.CHECK_EXIT(P_ID, idemp, ORG_ID)
            Catch ex As Exception

            End Try
        End Function
        Public Function UPDATE_HU_SIGNER(ByVal PA As SignerDTO) As Boolean Implements ServiceContracts.IProfileBusiness.UPDATE_HU_SIGNER
            Try
                Dim rep As New ProfileRepository
                Return rep.UPDATE_HU_SIGNER(PA)
            Catch ex As Exception

            End Try
        End Function
        Public Function DeactiveAndActiveSigner(ByVal lstID As String, ByVal sActive As Decimal) Implements ServiceContracts.IProfileBusiness.DeactiveAndActiveSigner
            Try
                Dim rep As New ProfileRepository
                Return rep.DeactiveAndActiveSigner(lstID, sActive)
            Catch ex As Exception

            End Try
        End Function
        Public Function DeleteSigner(ByVal lstID As String) Implements ServiceContracts.IProfileBusiness.DeleteSigner
            Try
                Dim rep As New ProfileRepository
                Return rep.DeleteSigner(lstID)
            Catch ex As Exception

            End Try
        End Function
#End Region
    End Class
End Namespace
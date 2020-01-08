Imports Profile.ProfileBusiness

Imports Framework.UI
Imports System.IO

Partial Public Class ProfileRepository
    Inherits ProfileRepositoryBase

    Dim CacheMinusDataCombo As Integer = 30
#Region "VALIDATE BUSINESS"
    Public Function CheckExistID(ByVal lstID As List(Of Decimal), ByVal table As String, ByVal column As String) As Boolean
        Try
            Using rep As New ProfileBusinessClient
                Try
                    Return rep.ValidateBusiness(table, column, lstID)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using
        Catch ex As Exception

        End Try
    End Function
#End Region
#Region "Other"
    Public Function GetCurrentPeriod(ByVal _year As Decimal) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCurrentPeriod(_year)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
#Region "Contract appendix"
    Public Function GET_NEXT_APPENDIX_ORDER(ByVal id As Decimal, ByVal contract_id As Decimal, ByVal emp_id As Decimal) As Integer
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GET_NEXT_APPENDIX_ORDER(id, contract_id, emp_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
#Region "Common"

    Public Function GetOtherList(ByVal sType As String, Optional ByVal isBlank As Boolean = False) As DataTable
        'Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            'Try
            '    dtData = CacheManager.GetValue("OT_" & sType & "_" & Common.Common.SystemLanguage.Name & IIf(isBlank, "Blank", "NoBlank"))
            '    If dtData Is Nothing Then
            '        dtData = rep.GetOtherList(sType, Common.Common.SystemLanguage.Name, isBlank)
            '    End If
            '    CacheManager.Insert("OT_" & sType & "_" & Common.Common.SystemLanguage.Name & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
            '    Return dtData
            'Catch ex As Exception
            '    rep.Abort()
            '    Throw ex
            'End Try
            Return rep.GetOtherList(sType, Common.Common.SystemLanguage.Name, isBlank)
        End Using

        Return Nothing
    End Function

    Public Function HU_PAPER_LIST(ByVal P_EMP_ID As Decimal) As DataTable
        'Dim dtData As DataTable

        Using rep As New ProfileBusinessClient

            Return rep.HU_PAPER_LIST(P_EMP_ID, Common.Common.SystemLanguage.Name)
        End Using

        Return Nothing
    End Function

    Public Function GetBankList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_HU_BANK_LIST_" & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetBankList(isBlank)
                End If
                CacheManager.Insert("OT_HU_BANK_LIST_" & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetBankBranchList(ByVal bankID As Decimal, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetBankBranchList(bankID, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTitleByOrgID(ByVal orgID As Decimal, ByVal sLang As String, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetTitleByOrgID(orgID, Common.Common.SystemLanguage.Name, True)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTitleList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetTitleList(Common.Common.SystemLanguage.Name, True)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetWardList(ByVal districtID As Decimal, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_HU_WARD_LIST_" & districtID & "_" & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetWardList(districtID, isBlank)
                End If
                CacheManager.Insert("OT_HU_WARD_LIST_" & districtID & "_" & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetDistrictList(ByVal provinceID As Decimal, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_HU_DISTRICT_LIST_" & provinceID & "_" & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetDistrictList(provinceID, isBlank)
                End If
                CacheManager.Insert("OT_HU_DISTRICT_LIST_" & provinceID & "_" & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetProvinceList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_HU_PROVINCE_LIST_" & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetProvinceList(isBlank)
                End If
                CacheManager.Insert("OT_HU_PROVINCE_LIST_" & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetProvinceList1(ByVal P_NATIVE As Decimal, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                'dtData = CacheManager.GetValue("OT_HU_PROVINCE_LIST_" & IIf(isBlank, "Blank", "NoBlank"))
                    dtData = rep.GetProvinceList1(P_NATIVE, isBlank)
                'CacheManager.Insert("OT_HU_PROVINCE_LIST_" & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetNationList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_HU_NATION_LIST_" & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetNationList(isBlank)
                End If
                CacheManager.Insert("OT_HU_NATION_LIST_" & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetStaffRankList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_HU_STAFF_RANK_LIST_" & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetStaffRankList(isBlank)
                End If
                CacheManager.Insert("OT_HU_STAFF_RANK_LIST_" & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryGroupList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetSalaryGroupList(dateValue, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetSalaryTypeList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetSalaryTypeList(dateValue, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using



        Return Nothing
    End Function

    Public Function GetSaleCommisionList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetSaleCommisionList(dateValue, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetSalaryLevelList(ByVal salGroupID As Decimal, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_PA_SALARY_LEVEL_LIST_" & salGroupID & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetSalaryLevelList(salGroupID, isBlank)
                End If
                CacheManager.Insert("OT_PA_SALARY_LEVEL_LIST_" & salGroupID & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryRankList(ByVal salLevelID As Decimal, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_PA_SALARY_RANK_LIST_" & salLevelID & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetSalaryRankList(salLevelID, isBlank)
                End If
                CacheManager.Insert("OT_PA_SALARY_RANK_LIST_" & salLevelID & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetHU_AllowanceList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_HU_ALLOWANCE_LIST_" & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetHU_AllowanceList(isBlank)
                End If
                CacheManager.Insert("OT_HU_ALLOWANCE_LIST_" & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetPA_ObjectSalary(Optional ByVal isBlank As Boolean = False) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetPA_ObjectSalary(isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetOT_WageTypeList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetOT_WageTypeList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetOT_MissionTypeList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetOT_MissionTypeList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetOT_TripartiteTypeList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetOT_TripartiteTypeList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetHU_TemplateType(ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetHU_TemplateType(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetHU_MergeFieldList(ByVal isBlank As Boolean,
                                           ByVal isTemplateType As Decimal) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetHU_MergeFieldList(isBlank, isTemplateType)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetHU_TemplateList(ByVal isBlank As Boolean,
                                           ByVal isTemplateType As Decimal) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetHU_TemplateList(isBlank, isTemplateType)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetHU_DataDynamic(ByVal dID As Decimal,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String,
                                       Optional ByVal isLogo As Boolean = True) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetHU_DataDynamic(dID, tempID, folderName)

                If Not dtData.Columns.Contains("IMAGE") Then
                    dtData.Columns.Add("IMAGE", GetType(Byte()))
                Else
                    If dtData.Rows(0)("IMAGE").ToString <> "" Then
                        Dim org_id2 As Decimal = dtData.Rows(0)("IMAGE")
                        dtData.Columns.Remove("IMAGE")
                        dtData.Columns.Add("IMAGE", GetType(Byte()))
                        Dim logoPath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\Logo\"
                        If Not Directory.Exists(logoPath) Then
                            Directory.CreateDirectory(logoPath)
                        End If
                        Dim logo = org_id2 & "_*"
                        Dim dirs As String() = Directory.GetFiles(logoPath, logo)
                        If dirs.Length > 0 Then
                            Using FileStream = File.Open(dirs(0), FileMode.Open)
                                Dim fileContent As Byte() = New Byte(FileStream.Length) {}
                                Using ms As New MemoryStream
                                    FileStream.CopyTo(ms)
                                    dtData.Rows(0)("IMAGE") = ms.ToArray
                                End Using
                            End Using
                        Else
                            logoPath = AppDomain.CurrentDomain.BaseDirectory & "Static\logo_default.png"
                            Using FileStream = File.Open(logoPath, FileMode.Open)
                                Dim fileContent As Byte() = New Byte(FileStream.Length) {}
                                Using ms As New MemoryStream
                                    FileStream.CopyTo(ms)
                                    dtData.Rows(0)("IMAGE") = ms.ToArray
                                End Using
                            End Using
                        End If
                    End If
                End If

                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetHU_MultyDataDynamic(ByVal strID As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String,
                                       Optional ByVal isLogo As Boolean = True) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetHU_MultyDataDynamic(strID, tempID, folderName)

                'If Not dtData.Columns.Contains("IMAGE") Then
                '    dtData.Columns.Add("IMAGE", GetType(Byte()))
                'Else
                '    If dtData.Rows(0)("IMAGE").ToString <> "" Then
                '        Dim org_id2 As Decimal = dtData.Rows(0)("IMAGE")
                '        dtData.Columns.Remove("IMAGE")
                '        dtData.Columns.Add("IMAGE", GetType(Byte()))
                '        Dim logoPath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\Logo\"
                '        If Not Directory.Exists(logoPath) Then
                '            Directory.CreateDirectory(logoPath)
                '        End If
                '        Dim logo = org_id2 & "_*"
                '        Dim dirs As String() = Directory.GetFiles(logoPath, logo)
                '        If dirs.Length > 0 Then
                '            Using FileStream = File.Open(dirs(0), FileMode.Open)
                '                Dim fileContent As Byte() = New Byte(FileStream.Length) {}
                '                Using ms As New MemoryStream
                '                    FileStream.CopyTo(ms)
                '                    dtData.Rows(0)("IMAGE") = ms.ToArray
                '                End Using
                '            End Using
                '        Else
                '            logoPath = AppDomain.CurrentDomain.BaseDirectory & "Static\logo_default.png"
                '            Using FileStream = File.Open(logoPath, FileMode.Open)
                '                Dim fileContent As Byte() = New Byte(FileStream.Length) {}
                '                Using ms As New MemoryStream
                '                    FileStream.CopyTo(ms)
                '                    dtData.Rows(0)("IMAGE") = ms.ToArray
                '                End Using
                '            End Using
                '        End If
                '    End If
                'End If

                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    '' In hop dong hang loat
    Public Function GetHU_DataDynamicContract(ByVal dID As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String,
                                       Optional ByVal isLogo As Boolean = True) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetHU_DataDynamicContract(dID, tempID, folderName)
                If Not dtData.Columns.Contains("IMAGE") Then
                    dtData.Columns.Add("IMAGE", GetType(Byte()))
                Else
                    If dtData.Rows(0)("IMAGE").ToString <> "" Then
                        Dim org_id2 As Decimal = dtData.Rows(0)("IMAGE")
                        dtData.Columns.Remove("IMAGE")
                        dtData.Columns.Add("IMAGE", GetType(Byte()))
                        Dim logoPath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\Logo\"
                        If Not Directory.Exists(logoPath) Then
                            Directory.CreateDirectory(logoPath)
                        End If
                        Dim logo = org_id2 & "_*"
                        Dim dirs As String() = Directory.GetFiles(logoPath, logo)
                        If dirs.Length > 0 Then
                            Using FileStream = File.Open(dirs(0), FileMode.Open)
                                Dim fileContent As Byte() = New Byte(FileStream.Length) {}
                                Using ms As New MemoryStream
                                    FileStream.CopyTo(ms)
                                    dtData.Rows(0)("IMAGE") = ms.ToArray
                                End Using
                            End Using
                        Else
                            logoPath = AppDomain.CurrentDomain.BaseDirectory & "Static\logo_default.png"
                            Using FileStream = File.Open(logoPath, FileMode.Open)
                                Dim fileContent As Byte() = New Byte(FileStream.Length) {}
                                Using ms As New MemoryStream
                                    FileStream.CopyTo(ms)
                                    dtData.Rows(0)("IMAGE") = ms.ToArray
                                End Using
                            End Using
                        End If
                    End If
                End If

                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetHU_DataDynamicContractAppendix(ByVal dID As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String,
                                       Optional ByVal isLogo As Boolean = True) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetHU_DataDynamicContractAppendix(dID, tempID, folderName)
                
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As ProfileCommonTABLE_NAME) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckExistInDatabase(lstID, table)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
        Using rep As New ProfileBusinessClient
            Try
                Return rep.AutoGenCode(firstChar, tableName, colName)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateMergeField(ByVal lstData As List(Of MergeFieldDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UpdateMergeField(lstData, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetDataPrintBBBR(ByVal id As Decimal) As DataSet
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetDataPrintBBBR(id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetDataPrintBBBR3B(ByVal id As Decimal) As DataSet
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetDataPrintBBBR3B(id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetInsRegionList(Optional ByVal isBlank As Boolean = False) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetInsRegionList(isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetHU_CompetencyGroupList(Optional ByVal isBlank As Boolean = False) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetHU_CompetencyGroupList(isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetHU_CompetencyList(ByVal groupID As Decimal, Optional ByVal isBlank As Boolean = False) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetHU_CompetencyList(groupID, isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetHU_CompetencyPeriodList(ByVal year As Decimal, Optional ByVal isBlank As Boolean = False) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetHU_CompetencyPeriodList(year, isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function



#Region "Get combobox Data"
    Public Function GetComboList(ByRef _combolistDTO As ComboBoxDataDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try

                Return rep.GetComboList(_combolistDTO)

            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "Service Auto Update Employee Information"
    Public Function CheckAndUpdateEmployeeInformation() As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckAndUpdateEmployeeInformation
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Service Send Mail Reminder"
    Public Function CheckAndSendMailReminder() As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckAndSendMailReminder
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#End Region

#Region "Setting"

#Region "Organization"

    Public Function GetOrganization(Optional ByVal sACT As String = "") As List(Of OrganizationDTO)
        Dim lstOrganization As List(Of OrganizationDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstOrganization = rep.GetOrganization(sACT)
                Return lstOrganization
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetOrganizationByID(ByVal ID As Decimal) As OrganizationDTO
        Using rep As New ProfileBusinessClient
            Try

                Return rep.GetOrganizationByID(ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertOrganization(ByVal objOrganization As OrganizationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertOrganization(objOrganization, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateOrganization(ByVal objOrganization As OrganizationDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateOrganization(objOrganization)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCostCenterCode(ByVal objOrganization As OrganizationDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateCostCenterCode(objOrganization)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckEmployeeInOrganization(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckEmployeeInOrganization(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyOrganization(ByVal objOrganization As OrganizationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyOrganization(objOrganization, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyOrganizationPath(ByVal lstPath As List(Of OrganizationPathDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyOrganizationPath(lstPath)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function ActiveOrganization(ByVal lstOrganization As List(Of OrganizationDTO), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveOrganization(lstOrganization, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
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
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OrgTitleDTO)
        Dim lstOrgTitle As List(Of OrgTitleDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstOrgTitle = rep.GetOrgTitle(filter, PageIndex, PageSize, Total, Sorts)
                Return lstOrgTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertOrgTitle(ByVal lstOrgTitle As List(Of OrgTitleDTO), Optional ByRef gID As Decimal = Nothing) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertOrgTitle(lstOrgTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckTitleInEmployee(ByVal lstID As List(Of Decimal), ByVal orgID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckTitleInEmployee(lstID, orgID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteOrgTitle(ByVal lstOrgTitle As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteOrgTitle(lstOrgTitle, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveOrgTitle(ByVal lstOrgTitle As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveOrgTitle(lstOrgTitle, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveTalentPool(ByVal lst As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveTalentPool(lst, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Function GetTalentPool(ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TalentPoolDTO)

        Dim lst As List(Of TalentPoolDTO)

        Using rep As New ProfileBusinessClient
            Try
                lst = rep.GetTalentPool(PageIndex, PageSize, Total, _param, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region

#End Region

#Region "Hoadm"
    Public Function GetOrgFromUsername(ByVal username As String) As Decimal?
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetOrgFromUsername(username)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetLineManager(ByVal username As String) As List(Of EmployeeDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetLineManager(username)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "BÁO CÁO"
    Public Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)

        Dim lstTitle As List(Of Se_ReportDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetReportById(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetEmployeeCVByID(ByVal sPkgName As String, ByVal sEmployee_id As String) As DataSet
        Dim dtData As DataSet

        Using rep As New ProfileBusinessClient
            Try

                'If dtData Is Nothing Then
                dtData = rep.GetEmployeeCVByID(sPkgName, sEmployee_id)
                'End If
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ExportReport(ByVal sPkgName As String,
                                 ByVal sStartDate As Date?,
                                 ByVal sEndDate As Date?,
                                 ByVal sOrg As String,
                                 ByVal IsDissolve As Integer,
                                 ByVal sLang As String) As DataSet
        Dim dtData As DataSet

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.ExportReport(sPkgName, sStartDate, sEndDate, sOrg, IsDissolve, Log.Username.ToUpper, Common.Common.SystemLanguage.Name)

                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region
#Region "FILTER TALENT POOL HONGDX"
    Public Function FILTER_TALENT_POOL(ByVal obj As FilterParamDTO) As DataTable
        Dim dt As DataTable
        Try
            Using rep As New ProfileBusinessClient
                Try
                    dt = rep.FILTER_TALENT_POOL(obj, Me.Log)
                    Return dt
                Catch ex As Exception
                End Try
            End Using
        Catch ex As Exception

        End Try
    End Function

    Public Function InsertTalentPool(ByVal lstTalentPool As List(Of TalentPoolDTO)) As Boolean
        Try
            Using rep As New ProfileBusinessClient
                Try
                    Return rep.InsertTalentPool(lstTalentPool, Me.Log)
                Catch ex As Exception
                End Try
            End Using
        Catch ex As Exception

        End Try
    End Function
#End Region

#Region "in phụ lục hợp đồng, hợp đồng"
    Public Function PrintFileContract(ByVal emp_Code As String, ByVal fileContract_ID As String) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.PrintFileContract(emp_Code, fileContract_ID)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetFileConTractID(ByVal ID As Decimal) As FileContractDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetFileConTractID(ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetContractAppendixPaging(ByVal _filter As FileContractDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of FileContractDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContractAppendixPaging(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteFileContract(ByVal objContract As FileContractDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteFileContract(objContract, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetListContractType(ByVal type As String) As List(Of ContractTypeDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListContractType(type)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckExpireFileContract(ByVal StartDate As Date, ByVal EndDate As Date, ByVal ID As Decimal) As Boolean


        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckExpireFileContract(StartDate, EndDate, ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckExistFileContract(ByVal empID As Decimal, ByVal StartDate As Date, ByVal type_id As Decimal) As Boolean


        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckExistFileContract(empID, StartDate, type_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertFileContract(ByVal fileInfo As FileContractDTO, ByRef gID As Decimal, ByRef appNum As String) As Boolean


        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertFileContract(fileInfo, Me.Log, gID, appNum)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateFileContract(ByVal fileInfo As FileContractDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UpdateFileContract(fileInfo, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetContractList(ByVal empID As Decimal) As List(Of ContractDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContractList(empID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetTitileBaseOnEmp(ByVal ID As Decimal) As Profile.ProfileBusiness.TitleDTO
        Dim lstTitle As Profile.ProfileBusiness.TitleDTO
        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetTitileBaseOnEmp(ID)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetFileContract_No(ByVal Contract As ContractDTO, ByRef STT As Decimal) As String


        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetFileContract_No(Contract, STT)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetContractAppendix(ByVal _filter As FileContractDTO) As List(Of FileContractDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContractAppendix(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetContractTypeID(ByVal ID As Decimal) As ContractTypeDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContractTypeID(ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetListContractBaseOnEmp(ByVal ID As Decimal) As List(Of ContractDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListContractBaseOnEmp(ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetListContract(ByVal ID As Decimal) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetListContract(ID)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GET_PROCESS_PLCONTRACT(ByVal P_EMP_CODE As String) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GET_PROCESS_PLCONTRACT(P_EMP_CODE)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function EXPORT_PLHD(ByVal _param As ParamDTO) As DataSet
        Using rep As New ProfileBusinessClient
            Try
                Return rep.EXPORT_PLHD(_param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CHECK_EMPLOYEE(P_EMP_CODE)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_CONTRACT(ByVal P_ID As Decimal) As Integer
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CHECK_CONTRACT(P_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_SALARY(ByVal P_ID As Decimal) As Integer
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CHECK_SALARY(P_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_CONTRACT_EXITS(ByVal P_CONTRACT As Decimal, ByVal P_EMP_CODE As String, ByVal P_DATE As Date) As Integer
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CHECK_CONTRACT_EXITS(P_CONTRACT, P_EMP_CODE, P_DATE)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_SIGN(ByVal P_EMP_CODE As String) As Integer
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CHECK_SIGN(P_EMP_CODE)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function INPORT_PLHD(ByVal P_DOCXML As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.INPORT_PLHD(P_DOCXML, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_PROCESS_PLCONTRACT_PORTAL(ByVal P_EMP_ID As Decimal) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GET_PROCESS_PLCONTRACT_PORTAL(P_EMP_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

End Class

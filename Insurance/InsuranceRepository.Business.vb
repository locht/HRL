﻿
Imports Insurance.InsuranceBusiness
Partial Class InsuranceRepository
    Inherits InsuranceRepositoryBase
#Region "Quan ly dong bao hiem"
    Function GetINS_ARISING(ByVal _filter As INS_ARISINGDTO,
                                     ByVal _param As PARAMDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_ARISINGDTO)
        Using rep As New InsuranceBusinessClient
            Try
                Dim lst = rep.GetINS_ARISING(_filter, _param, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyINS_ARISING(ByVal objRegister As INS_ARISINGDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ModifyINS_ARISING(objRegister, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function InsertINS_ARISING(ByVal objRegister As List(Of INS_ARISINGDTO), ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.InsertINS_ARISING(objRegister, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetINS_ARISINGyById(ByVal _id As Decimal?) As INS_ARISINGDTO
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetINS_ARISINGyById(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Quản ly thong tin bao hiem "
    Function GetINS_INFO(ByVal _filter As INS_INFORMATIONDTO,
                                      ByVal _param As PARAMDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_INFORMATIONDTO)
        Using rep As New InsuranceBusinessClient
            Try
                Dim lst = rep.GetINS_INFO(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function InsertINS_INFO(ByVal objIns_Info As INS_INFORMATIONDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.InsertINS_INFO(objIns_Info, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetINS_INFOById(ByVal _id As Decimal?) As INS_INFORMATIONDTO
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetINS_INFOById(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyINS_INFO(ByVal objIns_Info As INS_INFORMATIONDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ModifyINS_INFO(objIns_Info, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteINS_INFO(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.DeleteINS_INFO(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Function GetInfoPrint(ByVal LISTID As String) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Dim lst = rep.GetInfoPrint(LISTID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Function GetEmployeeID(ByVal p_employee_id As String) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Dim lst = rep.GetEmployeeID(p_employee_id)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Function GetLuongBH(ByVal p_employee_id As Integer) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Dim lst = rep.GetLuongBH(p_employee_id)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Function GetAllowanceTotalByDate(ByVal Employee_id As Decimal) As Decimal?
        Using rep As New InsuranceBusinessClient
            Try
                Dim lst = rep.GetAllowanceTotalByDate(Employee_id)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Quản ly thong tin bao hiem cu"
    Function GetINS_INFOOLD(ByVal _filter As INS_INFOOLDDTO,
                                      ByVal _param As PARAMDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_INFOOLDDTO)
        Using rep As New InsuranceBusinessClient
            Try
                Dim lst = rep.GetINS_INFOOLD(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function InsertINS_INFOOLD(ByVal objRegister As INS_INFOOLDDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.InsertINS_INFOOLD(objRegister, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetINS_INFOOLDById(ByVal _id As Decimal?) As INS_INFOOLDDTO
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetINS_INFOOLDById(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyINS_INFOOLD(ByVal objRegister As INS_INFOOLDDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ModifyINS_INFOOLD(objRegister, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteINS_INFOOLD(ByVal lstID As List(Of INS_INFOOLDDTO)) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.DeleteINS_INFOOLD(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetEmployeeById(ByVal _id As Decimal?) As EmployeeDTO
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetEmployeeById(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetEmployeeByIdProcess(ByRef P_EMPLOYEEID As Integer) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetEmployeeByIdProcess(P_EMPLOYEEID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Quản ly bien dong bao hiem"
    Function GetINS_CHANGE(ByVal _filter As INS_CHANGEDTO,
                                      ByVal _param As PARAMDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_CHANGEDTO)
        Using rep As New InsuranceBusinessClient
            Try
                Dim lst = rep.GetINS_CHANGE(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertINS_CHANGE(ByVal objRegister As INS_CHANGEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.InsertINS_CHANGE(objRegister, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetINS_CHANGEById(ByVal _id As Decimal?) As INS_CHANGEDTO
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetINS_CHANGEById(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyINS_CHANGE(ByVal objRegister As INS_CHANGEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ModifyINS_CHANGE(objRegister, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteINS_CHANGE(ByVal lstID As List(Of INS_CHANGEDTO)) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.DeleteINS_CHANGE(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetTiLeDong() As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetTiLeDong()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GETLUONGBIENDONG(ByRef P_EMPLOYEEID As Integer) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GETLUONGBIENDONG(P_EMPLOYEEID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Quản lý bảo hiểm Sun Care"
    Public Function GetSunCare(ByVal _filter As INS_SUN_CARE_DTO,
                                 ByVal OrgId As Integer,
                                 ByVal Fillter As String,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_SUN_CARE_DTO)
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetSunCare(_filter, OrgId, Fillter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetSunCareById(ByVal _id As Decimal?) As INS_SUN_CARE_DTO
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetSunCareById(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetIns_Cost_LeverByID(ByVal _id As Decimal?) As INS_COST_FOLLOW_LEVERDTO
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetIns_Cost_LeverByID(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetLevelImport() As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetLevelImport()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertSunCare(ByVal objTitle As INS_SUN_CARE_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.InsertSunCare(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifySunCare(ByVal objTitle As INS_SUN_CARE_DTO,
                                ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ModifySunCare(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_MANAGER_SUN_CARE(ByVal P_EMP_CODE As String, ByVal P_START_DATE As String, ByVal P_END_DATE As String, ByVal P_LEVEL_ID As Decimal) As Integer
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.CHECK_MANAGER_SUN_CARE(P_EMP_CODE, P_START_DATE, P_END_DATE, P_LEVEL_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ActiveSunCare(ByVal lstID As List(Of Decimal), ByVal bActive As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ActiveSunCare(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteSunCare(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.DeleteSunCare(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.CHECK_EMPLOYEE(P_EMP_CODE)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function INPORT_MANAGER_SUN_CARE(ByVal P_DOCXML As String) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.INPORT_MANAGER_SUN_CARE(P_DOCXML, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function


#End Region

#Region "Quản lý chế độ hưởng bảo hiểm "
    Public Function GetInfoInsByEmpID(ByVal employee_id As Integer) As INS_INFORMATIONDTO
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetInfoInsByEmpID(employee_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetLuyKe(ByVal P_TUNGAY As Date,
                                      ByVal P_DENNGAY As Date,
                                      ByRef P_EMPLOYEEID As Integer,
                                      ByVal P_ENTITLED_ID As Integer) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetLuyKe(P_TUNGAY, P_DENNGAY, P_EMPLOYEEID, P_ENTITLED_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CALCULATOR_DAY(ByVal P_TUNGAY As Date,
                                      ByVal P_DENNGAY As Date) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.CALCULATOR_DAY(P_TUNGAY, P_DENNGAY)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetTienHuong(ByVal P_NUMOFF As Integer,
                                    ByVal P_ATHOME As Integer,
                                    ByRef P_EMPLOYEEID As Integer,
                                    ByVal P_INSENTILEDKEY As Integer,
                                    ByVal P_SALARY_ADJACENT As Decimal,
                                    ByVal P_FROMDATE As Date,
                                    ByVal P_SOCON As Integer) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetTienHuong(P_NUMOFF, P_ATHOME, P_EMPLOYEEID, P_INSENTILEDKEY, P_SALARY_ADJACENT, P_FROMDATE, P_SOCON)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetMaxDayByID(ByVal P_ENTITLED_ID As Integer) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetMaxDayByID(P_ENTITLED_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetRegimeManager(ByVal _filter As INS_REMIGE_MANAGER_DTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal OrgId As Integer,
                                        ByVal IsDissolve As Integer,
                                        ByVal EntiledID As Integer,
                                        ByVal Fillter As String,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_REMIGE_MANAGER_DTO)
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetRegimeManager(_filter, PageIndex, PageSize, Total, OrgId, IsDissolve, EntiledID, Fillter, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetRegimeManagerByID(ByVal _id As Decimal?) As INS_REMIGE_MANAGER_DTO
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetRegimeManagerByID(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertRegimeManager(ByVal objTitle As INS_REMIGE_MANAGER_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.InsertRegimeManager(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyRegimeManager(ByVal objTitle As INS_REMIGE_MANAGER_DTO,
                                ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ModifyRegimeManager(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function Validate_KhamThai(ByVal _validate As INS_REMIGE_MANAGER_DTO) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.Validate_KhamThai(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateRegimeManager(ByVal _validate As INS_REMIGE_MANAGER_DTO)
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ValidateRegimeManager(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ValidateGroupRegime(ByVal _validate As INS_GROUP_REGIMESDTO)
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ValidateGroupRegime(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteRegimeManager(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.DeleteRegimeManager(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Khai báo điểu chỉnh nhóm bảo hiểm sun care"
    Function GetGroup_SunCare(ByVal _filter As INS_GROUP_SUN_CAREDTO,
                                     ByVal _param As PARAMDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_GROUP_SUN_CAREDTO)
        Using rep As New InsuranceBusinessClient
            Try
                Dim lst = rep.GetGroup_SunCare(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function InsertGroup_SunCare(ByVal objIns_Info As INS_GROUP_SUN_CAREDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.InsertGroup_SunCare(objIns_Info, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetGroup_SunCareById(ByVal _id As Decimal?) As INS_GROUP_SUN_CAREDTO
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetGroup_SunCareById(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyGroup_SunCare(ByVal objIns_Info As INS_GROUP_SUN_CAREDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ModifyGroup_SunCare(objIns_Info, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteGroup_SunCare(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.DeleteGroup_SunCare(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Báo cáo bảo hiểm"
    Public Function GetReportList() As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetReportList()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)

        Dim lstTitle As List(Of Se_ReportDTO)

        Using rep As New InsuranceBusinessClient
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
    Public Function GetD02Tang(ByVal p_month As Decimal, ByVal p_year As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetD02Tang(p_month, p_year, p_Username, p_Org_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetD02Giam(ByVal p_month As Decimal, ByVal p_Year As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetD02Giam(p_month, p_Year, p_Username, p_Org_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetC70_HD(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetC70_HD(p_MONTH, p_YEAR, p_Username, p_Org_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetQuyLuongBH(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetQuyLuongBH(p_MONTH, p_YEAR, p_Username, p_Org_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetDsBHSunCare(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetDsBHSunCare(p_Tungay, p_Toingay, p_Username, p_Org_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetDsDieuChinhSunCare(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetDsDieuChinhSunCare(p_Tungay, p_Toingay, p_Username, p_Org_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetChiPhiSunCare(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetChiPhiSunCare(p_Tungay, p_Toingay, p_Username, p_Org_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetOrgInfo(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Org_ID As Decimal) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetOrgInfo(p_Tungay, p_Toingay, p_Org_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetOrgInfoMONTH(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Org_ID As Decimal) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetOrgInfoMONTH(p_MONTH, p_YEAR, p_Org_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Quản lý tai nạn"
    Public Function GetAccidentRisk(ByVal _filter As INS_ACCIDENT_RISKDTO,
                                 ByVal OrgId As Integer,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_ACCIDENT_RISKDTO)
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetAccidentRisk(_filter, OrgId, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteAccidentRisk(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.DeleteAccidentRisk(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_INS_ACCIDENT_RISK(ByVal P_ID As Integer) As INS_ACCIDENT_RISKDTO
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GET_INS_ACCIDENT_RISK(P_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_EMPLOYEE_ACCIDENT_RISK(ByVal P_EMP_ID As Integer) As INS_ACCIDENT_RISKDTO
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GET_EMPLOYEE_ACCIDENT_RISK(P_EMP_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function INSERT_INS_ACCIDENT_RISK(ByVal obj As INS_ACCIDENT_RISKDTO) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.INSERT_INS_ACCIDENT_RISK(obj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function UPDATE_INS_ACCIDENT_RISK(ByVal obj As INS_ACCIDENT_RISKDTO) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.UPDATE_INS_ACCIDENT_RISK(obj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_SENIORITY(ByVal p_EMP_ID As Decimal) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GET_SENIORITY(p_EMP_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function EXPORT_INS_INFORMATION(ByVal P_USER_NAME As String, ByVal P_ORG_ID As Decimal, ByVal P_IS_DISSOLVE As Boolean) As DataSet
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.EXPORT_INS_INFORMATION(P_USER_NAME, P_ORG_ID, P_IS_DISSOLVE)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CheckEmployee_Exits(ByVal empCode As String) As Integer
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.CheckEmployee_Exits(empCode)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function INPORT_INS_INFORMATION(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.INPORT_INS_INFORMATION(P_DOCXML, Me.Log.Username)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_INS_TOTALSALARY_LOCK(ByVal P_YEAR As Integer, ByVal P_MONTH As Integer, ByVal P_INS_ORG_ID As Integer) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.CHECK_INS_TOTALSALARY_LOCK(P_YEAR, P_MONTH, P_INS_ORG_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function INSERT_TOTALSALARY_CAL(ByVal P_INS_ORG_ID As Decimal, ByVal P_YEAR As Decimal, ByVal P_MONTH As Decimal, ByVal P_SALARY As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.INSERT_TOTALSALARY_CAL(P_INS_ORG_ID, P_YEAR, P_MONTH, P_SALARY)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_REARL_FILED(ByVal P_INS_ORG_ID As Decimal, ByVal P_YEAR As Decimal, ByVal P_MONTH As Decimal) As Decimal
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GET_REARL_FILED(P_INS_ORG_ID, P_YEAR, P_MONTH)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
End Class
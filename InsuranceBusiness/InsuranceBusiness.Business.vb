Imports InsuranceBusiness.ServiceContracts
Imports InsuranceDAL
Imports Framework.Data
Imports System.ServiceModel.Activation
Imports System.Reflection
Imports System.Configuration

Imports Framework.UI
Imports Framework.UI.Utilities

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace InsuranceBusiness.ServiceImplementations
    Partial Public Class InsuranceBusiness
        Implements IInsuranceBusiness
#Region "Đóng mới bảo hiểm"
        Public Function GetINS_ARISING(ByVal _filter As INS_ARISINGDTO,
                                    ByVal _param As PARAMDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_ARISINGDTO) Implements IInsuranceBusiness.GetINS_ARISING

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.GetINS_ARISING(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertINS_ARISING(ByVal objLeave As List(Of INS_ARISINGDTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IInsuranceBusiness.InsertINS_ARISING

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.InsertINS_ARISING(objLeave, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyINS_ARISING(ByVal objLeave As INS_ARISINGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IInsuranceBusiness.ModifyINS_ARISING

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.ModifyINS_ARISING(objLeave, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetINS_ARISINGyById(ByVal _id As Decimal?) As INS_ARISINGDTO Implements ServiceContracts.IInsuranceBusiness.GetINS_ARISINGyById
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetINS_ARISINGyById(_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Quản lý thông tin bảo hiểm"
        Function GetINS_INFO(ByVal _filter As INS_INFORMATIONDTO,
                                    ByVal _param As PARAMDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_INFORMATIONDTO) Implements ServiceContracts.IInsuranceBusiness.GetINS_INFO

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.GetINS_INFO(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetINS_INFOById(ByVal _id As Decimal?) As INS_INFORMATIONDTO Implements ServiceContracts.IInsuranceBusiness.GetINS_INFOById
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetINS_INFOById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertINS_INFO(ByVal objIns_Info As INS_INFORMATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertINS_INFO
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertINS_INFO(objIns_Info, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyINS_INFO(ByVal objIns_Info As INS_INFORMATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifyINS_INFO
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyINS_INFO(objIns_Info, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteINS_INFO(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteINS_INFO
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteINS_INFO(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetInfoPrint(ByVal LISTID As String) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetInfoPrint

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.GetInfoPrint(LISTID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetLuongBH(ByVal p_EMPLOYEE_ID As Integer) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetLuongBH

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.GetLuongBH(p_EMPLOYEE_ID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetEmployeeID(ByVal p_EMPLOYEE_ID As String) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetEmployeeID

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.GetEmployeeID(p_EMPLOYEE_ID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetAllowanceTotalByDate(ByVal EMPLOYEE_ID As Decimal) As Decimal? Implements ServiceContracts.IInsuranceBusiness.GetAllowanceTotalByDate

            Using rep As New InsuranceRepository
                Try
                    Dim dInsuran As Decimal?
                    dInsuran = rep.GetAllowanceTotalByDate(EMPLOYEE_ID)
                    Return dInsuran
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Quan ly thong tin bao hiem cu"
        Function GetINS_INFOOLD(ByVal _filter As INS_INFOOLDDTO,
                                    ByVal _param As PARAMDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_INFOOLDDTO) Implements IInsuranceBusiness.GetINS_INFOOLD

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.GetINS_INFOOLD(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetINS_INFOOLDById(ByVal _id As Decimal?) As INS_INFOOLDDTO Implements ServiceContracts.IInsuranceBusiness.GetINS_INFOOLDById
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetINS_INFOOLDById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertINS_INFOOLD(ByVal objRegisterOT As INS_INFOOLDDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertINS_INFOOLD
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertINS_INFOOLD(objRegisterOT, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyINS_INFOOLD(ByVal objRegisterOT As INS_INFOOLDDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifyINS_INFOOLD
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyINS_INFOOLD(objRegisterOT, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteINS_INFOOLD(ByVal lstID As List(Of INS_INFOOLDDTO)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteINS_INFOOLD
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteINS_INFOOLD(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmployeeById(ByVal _id As Decimal?) As EmployeeDTO Implements ServiceContracts.IInsuranceBusiness.GetEmployeeById
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetEmployeeById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmployeeByIdProcess(ByRef P_EMPLOYEEID As Integer) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetEmployeeByIdProcess
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetEmployeeByIdProcess(P_EMPLOYEEID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Quan ly bien dong bao hiem"
        Function GetINS_CHANGE(ByVal _filter As INS_CHANGEDTO,
                                    ByVal _param As PARAMDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_CHANGEDTO) Implements IInsuranceBusiness.GetINS_CHANGE

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.GetINS_CHANGE(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetINS_CHANGEById(ByVal _id As Decimal?) As INS_CHANGEDTO Implements ServiceContracts.IInsuranceBusiness.GetINS_CHANGEById
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetINS_CHANGEById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertINS_CHANGE(ByVal objRegisterOT As INS_CHANGEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertINS_CHANGE
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertINS_CHANGE(objRegisterOT, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyINS_CHANGE(ByVal objRegisterOT As INS_CHANGEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifyINS_CHANGE
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyINS_CHANGE(objRegisterOT, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteINS_CHANGE(ByVal lstID As List(Of INS_CHANGEDTO)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteINS_CHANGE
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteINS_CHANGE(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTiLeDong() As DataTable Implements ServiceContracts.IInsuranceBusiness.GetTiLeDong
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetTiLeDong()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GETLUONGBIENDONG(ByRef P_EMPLOYEEID As Integer) As DataTable Implements ServiceContracts.IInsuranceBusiness.GETLUONGBIENDONG
            Using rep As New InsuranceRepository
                Try
                    Return rep.GETLUONGBIENDONG(P_EMPLOYEEID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Quản lý Sun Care"
        Public Function GetSunCare(ByVal _filter As INS_SUN_CARE_DTO,
                                    ByVal OrgId As Integer,
                                    ByVal Fillter As String,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_SUN_CARE_DTO) Implements ServiceContracts.IInsuranceBusiness.GetSunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetSunCare(_filter, OrgId, Fillter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetSunCareById(ByVal _id As Decimal?) As INS_SUN_CARE_DTO Implements ServiceContracts.IInsuranceBusiness.GetSunCareById
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetSunCareById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetIns_Cost_LeverByID(ByVal _id As Decimal?) As INS_COST_FOLLOW_LEVERDTO Implements ServiceContracts.IInsuranceBusiness.GetIns_Cost_LeverByID
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetIns_Cost_LeverByID(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetLevelImport() As DataTable Implements ServiceContracts.IInsuranceBusiness.GetLevelImport
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetLevelImport()
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertSunCare(ByVal objTitle As INS_SUN_CARE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertSunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertSunCare(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifySunCare(ByVal objTitle As INS_SUN_CARE_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifySunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifySunCare(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_MANAGER_SUN_CARE(ByVal P_EMP_CODE As String, ByVal P_START_DATE As String, ByVal P_END_DATE As String, ByVal P_LEVEL_ID As Decimal) As Integer Implements ServiceContracts.IInsuranceBusiness.CHECK_MANAGER_SUN_CARE
            Using rep As New InsuranceRepository
                Try

                    Return rep.CHECK_MANAGER_SUN_CARE(P_EMP_CODE, P_START_DATE, P_END_DATE, P_LEVEL_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveSunCare(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ActiveSunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.ActiveSunCare(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteSunCare(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteSunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteSunCare(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer Implements ServiceContracts.IInsuranceBusiness.CHECK_EMPLOYEE
            Using rep As New InsuranceRepository
                Try

                    Return rep.CHECK_EMPLOYEE(P_EMP_CODE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function INPORT_MANAGER_SUN_CARE(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IInsuranceBusiness.INPORT_MANAGER_SUN_CARE
            Using rep As New InsuranceRepository
                Try

                    Return rep.INPORT_MANAGER_SUN_CARE(P_DOCXML, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Quản lý chế độ bảo hiểm"
        Public Function GetInfoInsByEmpID(ByVal employee_id As Integer) As INS_INFORMATIONDTO Implements ServiceContracts.IInsuranceBusiness.GetInfoInsByEmpID
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetInfoInsByEmpID(employee_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetLuyKe(ByVal P_TUNGAY As Date,
                                      ByVal P_DENNGAY As Date,
                                      ByRef P_EMPLOYEEID As Integer,
                                      ByVal P_ENTITLED_ID As Integer) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetLuyKe
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetLuyKe(P_TUNGAY, P_DENNGAY, P_EMPLOYEEID, P_ENTITLED_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CALCULATOR_DAY(ByVal P_TUNGAY As Date,
                                      ByVal P_DENNGAY As Date) As DataTable Implements ServiceContracts.IInsuranceBusiness.CALCULATOR_DAY
            Using rep As New InsuranceRepository
                Try

                    Return rep.CALCULATOR_DAY(P_TUNGAY, P_DENNGAY)
                Catch ex As Exception
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
                                    ByVal P_SOCON As Integer) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetTienHuong
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetTienHuong(P_NUMOFF, P_ATHOME, P_EMPLOYEEID, P_INSENTILEDKEY, P_SALARY_ADJACENT, P_FROMDATE, P_SOCON)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetMaxDayByID(ByVal P_ENTITLED_ID As Integer) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetMaxDayByID
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetMaxDayByID(P_ENTITLED_ID)
                Catch ex As Exception
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
                                        ByVal log As UserLog,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_REMIGE_MANAGER_DTO) Implements ServiceContracts.IInsuranceBusiness.GetRegimeManager
            Using rep As New InsuranceRepository
                Try
                    Return rep.GetRegimeManager(_filter, PageIndex, PageSize, Total, OrgId, IsDissolve, EntiledID, Fillter, log, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetRegimeManagerByID(ByVal _id As Decimal?) As INS_REMIGE_MANAGER_DTO Implements ServiceContracts.IInsuranceBusiness.GetRegimeManagerByID
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetRegimeManagerByID(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertRegimeManager(ByVal objTitle As INS_REMIGE_MANAGER_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertRegimeManager
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertRegimeManager(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyRegimeManager(ByVal objTitle As INS_REMIGE_MANAGER_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifyRegimeManager
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyRegimeManager(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Validate_KhamThai(ByVal _validate As INS_REMIGE_MANAGER_DTO) As DataTable Implements ServiceContracts.IInsuranceBusiness.Validate_KhamThai
            Using rep As New InsuranceRepository
                Try

                    Return rep.Validate_KhamThai(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidateGroupRegime(ByVal _validate As INS_GROUP_REGIMESDTO) Implements ServiceContracts.IInsuranceBusiness.ValidateGroupRegime
            Using rep As New InsuranceRepository
                Try
                    Return rep.ValidateGroupRegime(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateRegimeManager(ByVal _validate As INS_REMIGE_MANAGER_DTO) Implements ServiceContracts.IInsuranceBusiness.ValidateRegimeManager
            Using rep As New InsuranceRepository
                Try
                    Return rep.ValidateRegimeManager(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteRegimeManager(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteRegimeManager
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteRegimeManager(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Khai báo điều chỉnh nhóm bảo hiểm sun care"
        Function GetGroup_SunCare(ByVal _filter As INS_GROUP_SUN_CAREDTO,
                                  ByVal _param As PARAMDTO,
                                  Optional ByRef Total As Integer = 0,
                                  Optional ByVal PageIndex As Integer = 0,
                                  Optional ByVal PageSize As Integer = Integer.MaxValue,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_GROUP_SUN_CAREDTO) Implements ServiceContracts.IInsuranceBusiness.GetGroup_SunCare

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.GetGroup_SunCare(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetGroup_SunCareById(ByVal _id As Decimal?) As INS_GROUP_SUN_CAREDTO Implements ServiceContracts.IInsuranceBusiness.GetGroup_SunCareById
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetGroup_SunCareById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertGroup_SunCare(ByVal objIns_Info As INS_GROUP_SUN_CAREDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertGroup_SunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertGroup_SunCare(objIns_Info, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyGroup_SunCare(ByVal objIns_Info As INS_GROUP_SUN_CAREDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifyGroup_SunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyGroup_SunCare(objIns_Info, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteGroup_SunCare(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteGroup_SunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteGroup_SunCare(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Báo cáo bảo hiểm"
        Public Function GetReportList() As DataTable Implements ServiceContracts.IInsuranceBusiness.GetReportList
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetReportList()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       log As UserLog,
                                       Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO) Implements ServiceContracts.IInsuranceBusiness.GetReportById
            Using rep As New InsuranceRepository
                Try

                    Dim lst = rep.GetReportById(_filter, PageIndex, PageSize, Total, log, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetD02Tang(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetD02Tang
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetD02Tang(p_MONTH, p_YEAR, p_Username, p_Org_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetD02Giam(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetD02Giam
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetD02Giam(p_MONTH, p_YEAR, p_Username, p_Org_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetC70_HD(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetC70_HD
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetC70_HD(p_MONTH, p_YEAR, p_Username, p_Org_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetQuyLuongBH(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetQuyLuongBH
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetQuyLuongBH(p_MONTH, p_YEAR, p_Username, p_Org_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetDsBHSunCare(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetDsBHSunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetDsBHSunCare(p_Tungay, p_Toingay, p_Username, p_Org_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetDsDieuChinhSunCare(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetDsDieuChinhSunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetDsDieuChinhSunCare(p_Tungay, p_Toingay, p_Username, p_Org_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetChiPhiSunCare(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetChiPhiSunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetChiPhiSunCare(p_Tungay, p_Toingay, p_Username, p_Org_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOrgInfo(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetOrgInfo
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetOrgInfo(p_Tungay, p_Toingay, p_Org_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOrgInfoMONTH(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetOrgInfoMONTH
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetOrgInfoMONTH(p_MONTH, p_YEAR, p_Org_ID)
                Catch ex As Exception
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
                               Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_ACCIDENT_RISKDTO) Implements ServiceContracts.IInsuranceBusiness.GetAccidentRisk
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetAccidentRisk(_filter, OrgId, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteAccidentRisk(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteAccidentRisk
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteAccidentRisk(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_INS_ACCIDENT_RISK(ByVal P_ID As Integer) As INS_ACCIDENT_RISKDTO Implements ServiceContracts.IInsuranceBusiness.GET_INS_ACCIDENT_RISK
            Using rep As New InsuranceRepository
                Try

                    Return rep.GET_INS_ACCIDENT_RISK(P_ID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_EMPLOYEE_ACCIDENT_RISK(ByVal P_EMP_ID As Integer) As INS_ACCIDENT_RISKDTO Implements ServiceContracts.IInsuranceBusiness.GET_EMPLOYEE_ACCIDENT_RISK
            Using rep As New InsuranceRepository
                Try

                    Return rep.GET_EMPLOYEE_ACCIDENT_RISK(P_EMP_ID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function INSERT_INS_ACCIDENT_RISK(ByVal obj As INS_ACCIDENT_RISKDTO, Optional ByVal log As UserLog = Nothing) As Boolean Implements ServiceContracts.IInsuranceBusiness.INSERT_INS_ACCIDENT_RISK
            Using rep As New InsuranceRepository
                Try

                    Return rep.INSERT_INS_ACCIDENT_RISK(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UPDATE_INS_ACCIDENT_RISK(ByVal obj As INS_ACCIDENT_RISKDTO, Optional ByVal log As UserLog = Nothing) As Boolean Implements ServiceContracts.IInsuranceBusiness.UPDATE_INS_ACCIDENT_RISK
            Using rep As New InsuranceRepository
                Try

                    Return rep.UPDATE_INS_ACCIDENT_RISK(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_SENIORITY(ByVal p_EMP_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GET_SENIORITY
            Using rep As New InsuranceRepository
                Try

                    Return rep.GET_SENIORITY(p_EMP_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function EXPORT_INS_INFORMATION(ByVal P_USER_NAME As String, ByVal P_ORG_ID As Decimal, ByVal P_IS_DISSOLVE As Boolean) As DataSet Implements ServiceContracts.IInsuranceBusiness.EXPORT_INS_INFORMATION
            Using rep As New InsuranceRepository
                Try

                    Return rep.EXPORT_INS_INFORMATION(P_USER_NAME, P_ORG_ID, P_IS_DISSOLVE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckEmployee_Exits(ByVal empCode As String) As Integer _
          Implements ServiceContracts.IInsuranceBusiness.CheckEmployee_Exits
            Using rep As New InsuranceRepository
                Try
                    Return rep.CheckEmployee_Exits(empCode)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function INPORT_INS_INFORMATION(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean _
         Implements ServiceContracts.IInsuranceBusiness.INPORT_INS_INFORMATION
            Using rep As New InsuranceRepository
                Try
                    Return rep.INPORT_INS_INFORMATION(P_DOCXML, P_USER)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_INS_TOTALSALARY_LOCK(ByVal P_YEAR As Integer, ByVal P_MONTH As Integer, ByVal P_INS_ORG_ID As Integer) As Boolean _
         Implements ServiceContracts.IInsuranceBusiness.CHECK_INS_TOTALSALARY_LOCK
            Using rep As New InsuranceRepository
                Try
                    Return rep.CHECK_INS_TOTALSALARY_LOCK(P_YEAR, P_MONTH, P_INS_ORG_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function INSERT_TOTALSALARY_CAL(ByVal P_INS_ORG_ID As Decimal, ByVal P_YEAR As Decimal, ByVal P_MONTH As Decimal, ByVal P_SALARY As Decimal) As Boolean _
         Implements ServiceContracts.IInsuranceBusiness.INSERT_TOTALSALARY_CAL
            Using rep As New InsuranceRepository
                Try
                    Return rep.INSERT_TOTALSALARY_CAL(P_INS_ORG_ID, P_YEAR, P_MONTH, P_SALARY)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_REARL_FILED(ByVal P_INS_ORG_ID As Decimal, ByVal P_YEAR As Decimal, ByVal P_MONTH As Decimal) As Decimal _
         Implements ServiceContracts.IInsuranceBusiness.GET_REARL_FILED
            Using rep As New InsuranceRepository
                Try
                    Return rep.GET_REARL_FILED(P_INS_ORG_ID, P_YEAR, P_MONTH)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_INS_LOCK(ByVal P_EMPLOYEE_ID As Decimal, ByVal P_DATE As Date) As Decimal _
         Implements ServiceContracts.IInsuranceBusiness.CHECK_INS_LOCK
            Using rep As New InsuranceRepository
                Try
                    Return rep.CHECK_INS_LOCK(P_EMPLOYEE_ID, P_DATE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_INS_LOCK1(ByVal P_INS_ORG_ID As Decimal, ByVal P_DATE As Date) As Decimal _
        Implements ServiceContracts.IInsuranceBusiness.CHECK_INS_LOCK1
            Using rep As New InsuranceRepository
                Try
                    Return rep.CHECK_INS_LOCK1(P_INS_ORG_ID, P_DATE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

    End Class
End Namespace

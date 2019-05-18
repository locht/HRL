Imports AttendanceDAL
Imports Framework.Data
Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.ServiceModel.Web
' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
Namespace AttendanceBusiness.ServiceContracts
    <ServiceContract()>
    Public Interface IAttendanceBusiness
#Region "CONFIG TEMPLATE"
        <OperationContract()>
        Function GET_CONFIG_TEMPLATE(ByVal MACHINE_TYPE As Decimal?) As DataSet
#End Region

        <OperationContract()>
        Function GetDataFromOrg(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function getSetUpAttEmp(ByVal _filter As SetUpCodeAttDTO,
                                   Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SetUpCodeAttDTO)
        <OperationContract()>
        Function InsertSetUpAttEmp(ByVal objValue As SetUpCodeAttDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifySetUpAttEmp(ByVal objValue As SetUpCodeAttDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteSetUpAttEmp(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function CheckValidateMACC(ByVal obj As SetUpCodeAttDTO) As Boolean
        <OperationContract()>
        Function CheckValidateAPPROVE_DATE(ByVal obj As SetUpCodeAttDTO) As Boolean
#Region "Get Data Combobox"

        <OperationContract()>
        Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetProjectList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetProjectTitleList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetProjectWorkList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO) As Boolean

        <OperationContract()>
        Function GET_AT_PORTAL_REG(ByVal P_ID As Decimal, ByVal P_EMPLOYEE As Decimal, ByVal P_DATE_TIME As Date) As DataTable

        <OperationContract()>
        Function GET_AT_PORTAL_REG_OT(ByVal P_ID As Decimal, ByVal P_EMPLOYEE As Decimal, ByVal P_DATE_TIME As Date) As DataTable

#End Region

#Region "Ky cong"
        <OperationContract()>
        Function LOAD_PERIOD(ByVal obj As AT_PERIODDTO, ByVal log As UserLog) As DataTable
        <OperationContract()>
        Function LOAD_PERIODBylinq(ByVal obj As AT_PERIODDTO, ByVal log As UserLog) As List(Of AT_PERIODDTO)
        <OperationContract()>
        Function LOAD_PERIODByID(ByVal obj As AT_PERIODDTO, ByVal log As UserLog) As AT_PERIODDTO
        <OperationContract()>
        Function CLOSEDOPEN_PERIOD(ByVal param As ParamDTO, ByVal log As Framework.Data.UserLog) As Boolean
        <OperationContract()>
        Function IS_PERIOD_PAYSTATUS(ByVal _param As ParamDTO, ByVal isAfter As Boolean, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function IS_PERIODSTATUS(ByVal _param As ParamDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function IS_PERIODSTATUS_BY_DATE(ByVal _param As ParamDTO, ByVal log As UserLog) As Boolean
#End Region

#Region "QUAN LY VAO RA"
        <OperationContract()>
        Function GetDataInout(ByVal _filter As AT_DATAINOUTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "EMPLOYEE_CODE, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_DATAINOUTDTO)
        <OperationContract()>
        Function InsertDataInout(ByVal lstDataInout As List(Of AT_DATAINOUTDTO), ByVal fromDate As Date, ByVal toDate As Date,
                                 ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyDataInout(ByVal objDataInout As AT_DATAINOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteDataInout(ByVal lstDataInout() As AT_DATAINOUTDTO) As Boolean

#End Region

#Region "Dang ky lam them"
        <OperationContract()>
        Function GetRegisterOT(ByVal _filter As AT_REGISTER_OTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_REGISTER_OTDTO)
        <OperationContract()>
        Function InsertRegisterOT(ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertDataRegisterOT(ByVal objRegisterOTList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyRegisterOT(ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function GetRegisterById(ByVal _id As Decimal?) As AT_REGISTER_OTDTO
        <OperationContract()>
        Function ValidateRegisterOT(ByVal _validate As AT_REGISTER_OTDTO) As Boolean
        <OperationContract()>
        Function DeleteRegisterOT(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function CheckImporAddNewtOT(ByVal objRegisterOT As AT_REGISTER_OTDTO) As Boolean
        <OperationContract()>
        Function CheckDataListImportAddNew(ByVal objRegisterOTList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByRef strEmployeeCode As String) As Boolean

        <OperationContract()>
        Function ApproveRegisterOT(ByVal lstData As List(Of AT_REGISTER_OTDTO), ByVal status As Decimal) As Boolean

#End Region

#Region "Bang cong may"
        <OperationContract()>
        Function GetMachines(ByVal _filter As AT_TIME_TIMESHEET_MACHINETDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "EMPLOYEE_ID, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MACHINETDTO)
        <OperationContract()>
        Function Init_TimeTImesheetMachines(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal p_fromdate As Date, ByVal p_enddate As Date, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?), ByVal p_delAll As Decimal) As Boolean
#End Region

#Region "Bang cong TAY"

        <OperationContract()>
        Function GetCCT(ByVal param As AT_TIME_TIMESHEET_DAILYDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function GetCCT_Origin(ByVal param As AT_TIME_TIMESHEET_DAILYDTO, ByVal log As UserLog) As DataTable

        <OperationContract()>
        Function ModifyLeaveSheetDaily(ByVal objLeave As AT_TIME_TIMESHEET_DAILYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertLeaveSheetDaily(ByVal dtData As DataTable, ByVal log As UserLog, ByVal PeriodID As Decimal) As Boolean

        <OperationContract()>
        Function GetTimeSheetDailyById(ByVal obj As AT_TIME_TIMESHEET_DAILYDTO) As AT_TIME_TIMESHEET_DAILYDTO
#End Region

#Region "Bang cong lam them"
        <OperationContract()>
        Function Cal_TimeTImesheet_OT(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal p_period_id As Decimal?, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?)) As Boolean

        <OperationContract()>
        Function GetSummaryOT(ByVal param As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function Cal_TimeTImesheet_NB(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal p_period_id As Decimal?, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?)) As Boolean

        <OperationContract()>
        Function GetSummaryNB(ByVal param As AT_TIME_TIMESHEET_NBDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function ModifyLeaveSheetOt(ByVal objLeave As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertLeaveSheetOt(ByVal objLeave As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function GetTimeSheetOtById(ByVal obj As AT_TIME_TIMESHEET_OTDTO) As AT_TIME_TIMESHEET_OTDTO
#End Region

#Region "Tổng hợp công"
        <OperationContract()>
        Function GetTimeSheet(ByVal _filter As AT_TIME_TIMESHEET_MONTHLYDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MONTHLYDTO)
        <OperationContract()>
        Function CAL_TIME_TIMESHEET_MONTHLY(ByVal param As ParamDTO, ByVal lstEmployee As List(Of Decimal?), ByVal log As Framework.Data.UserLog) As Boolean

        <OperationContract()>
        Function GetTimeSheetPortal(ByVal _filter As AT_TIME_TIMESHEET_MONTHLYDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MONTHLYDTO)

        <OperationContract()>
        Function ValidateTimesheet(ByVal _validate As AT_TIME_TIMESHEET_MONTHLYDTO, ByVal sType As String, ByVal log As UserLog)


#End Region

#Region "Khai bao cong com"
        <OperationContract()>
        Function GetDelareRice(ByVal _filter As AT_TIME_RICEDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_RICEDTO)
        <OperationContract()>
        Function InsertDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertDelareRiceList(ByVal objDelareRiceList As List(Of AT_TIME_RICEDTO), ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveDelareRice(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function ModifyDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateDelareRice(ByVal _validate As AT_TIME_RICEDTO) As Boolean
        <OperationContract()>
        Function GetDelareRiceById(ByVal _id As Decimal?) As AT_TIME_RICEDTO
        <OperationContract()>
        Function DeleteDelareRice(ByVal lstID As List(Of Decimal)) As Boolean

#End Region
#Region "quan ly bu tru cham cong"
        <OperationContract()>
        Function GetOffSettingTimeKeeping(ByVal _filter As AT_OFFFSETTINGDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_OFFFSETTINGDTO)

        <OperationContract()>
        Function GetOffSettingTimeKeepingById(ByVal _id As Decimal?) As AT_OFFFSETTINGDTO
        <OperationContract()>
        Function GetEmployeeTimeKeepingID(ByVal _id As Decimal) As List(Of AT_OFFFSETTING_EMPDTO)
#End Region
#Region "Khai bao điều chỉnh thâm niên phép"
        <OperationContract()>
        Function GetDelareEntitlementNB(ByVal _filter As AT_DECLARE_ENTITLEMENTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_DECLARE_ENTITLEMENTDTO)
        <OperationContract()>
        Function InsertDelareEntitlementNB(ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        <OperationContract()>
        Function InsertMultipleDelareEntitlementNB(ByVal objDelareEntitlementlist As List(Of AT_DECLARE_ENTITLEMENTDTO), ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        <OperationContract()>
        Function ImportDelareEntitlementNB(ByVal dtData As DataTable, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        <OperationContract()>
        Function ActiveDelareEntitlementNB(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function ModifyDelareEntitlementNB(ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function GetDelareEntitlementNBById(ByVal _id As Decimal?) As AT_DECLARE_ENTITLEMENTDTO
        <OperationContract()>
        Function DeleteOffTimeKeeping(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function DeleteDelareEntitlementNB(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function ValidateMonthThamNien(ByVal _validate As AT_DECLARE_ENTITLEMENTDTO) As Boolean
        <OperationContract()>
        Function ValidateMonthPhepNam(ByVal _validate As AT_DECLARE_ENTITLEMENTDTO) As Boolean
        <OperationContract()>
        Function ValidateMonthNghiBu(ByVal _validate As AT_DECLARE_ENTITLEMENTDTO) As Boolean

#End Region

#Region "Bang tong hop cong com"
        <OperationContract()>
        Function Cal_TimeTImesheet_Rice(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal p_period_id As Decimal?, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?)) As Boolean
        <OperationContract()>
        Function GetSummaryRice(ByVal param As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function ModifyLeaveSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ApprovedTimeSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertLeaveSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function GetTimeSheetRiceById(ByVal obj As AT_TIME_TIMESHEET_RICEDTO) As AT_TIME_TIMESHEET_RICEDTO
#End Region

#Region "Dang ky công"
        <OperationContract()>
        Function GetLeaveSheet(ByVal _filter As AT_LEAVESHEETDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVESHEETDTO)
        <OperationContract()>
        Function InsertLeaveSheet(ByVal objRegisterOT As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertLeaveSheetList(ByVal objRegisterList As List(Of AT_LEAVESHEETDTO), ByVal objRegisterOT As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyLeaveSheet(ByVal objRegisterOT As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function GetTotalDAY(ByVal P_EMPLOYEE_ID As Integer,
                                ByVal P_TYPE_MANUAL As Integer,
                                ByVal P_FROM_DATE As Date,
                                ByVal P_TO_DATE As Date) As DataTable
        <OperationContract()>
        Function GetCAL_DAY_LEAVE_OLD(ByVal P_EMPLOYEE_ID As Integer,
                                    ByVal P_FROM_DATE As Date,
                                    ByVal P_TO_DATE As Date) As DataTable
        <OperationContract()>
        Function GetTotalPHEPNAM(ByVal P_EMPLOYEE_ID As Integer,
                                      ByVal P_YEAR As Integer,
                                      ByVal P_TYPE_LEAVE_ID As Integer) As DataTable
        <OperationContract()>
        Function GetTotalPHEPBU(ByVal P_EMPLOYEE_ID As Integer,
                                      ByVal P_YEAR As Integer,
                                      ByVal P_TYPE_LEAVE_ID As Integer) As DataTable
        <OperationContract()>
        Function GetPHEPBUCONLAI(ByVal lstEmpID As List(Of AT_LEAVESHEETDTO), ByVal _year As Decimal?) As List(Of AT_LEAVESHEETDTO)

        <OperationContract()>
        Function GetLeaveById(ByVal _id As Decimal?) As AT_LEAVESHEETDTO

        <OperationContract()>
        Function ValidateLeaveSheet(ByVal _validate As AT_LEAVESHEETDTO) As Boolean
        <OperationContract()>
        Function GetPhepNam(ByVal _id As Decimal?, ByVal _year As Decimal?) As AT_ENTITLEMENTDTO
        <OperationContract()>
        Function GetNghiBu(ByVal _id As Decimal?, ByVal _year As Decimal?) As AT_COMPENSATORYDTO
        <OperationContract()>
        Function DeleteLeaveSheet(ByVal lstID As List(Of AT_LEAVESHEETDTO)) As Boolean
        <OperationContract()>
        Function checkLeaveImport(ByVal dtData As DataTable) As DataTable

#End Region

#Region "di som ve muon"
        <OperationContract()>
        Function GetDSVM(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LATE_COMBACKOUTDTO)
        <OperationContract()>
        Function GetLate_CombackoutById(ByVal _id As Decimal?) As AT_LATE_COMBACKOUTDTO
        <OperationContract()>
        Function ImportLate_combackout(ByVal objDataInout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertLate_combackout(ByVal objRegisterDMVSList As List(Of AT_LATE_COMBACKOUTDTO), ByVal objDataInout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyLate_combackout(ByVal objDataInout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateLate_combackout(ByVal _validate As AT_LATE_COMBACKOUTDTO) As Boolean
        <OperationContract()>
        Function DeleteLate_combackout(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "lam bu"
        <OperationContract()>
        Function CALCULATE_ENTITLEMENT_NB(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetNB(ByVal _filter As AT_COMPENSATORYDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_COMPENSATORYDTO)
#End Region

#Region "PHEP NAM"
        <OperationContract()>
        Function CALCULATE_ENTITLEMENT(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetEntitlement(ByVal _filter As AT_ENTITLEMENTDTO,
                                  ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_ENTITLEMENTDTO)
#End Region

#Region "WORKSIGN"
        <OperationContract()>
        Function GET_WORKSIGN(ByVal param As AT_WORKSIGNDTO, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function InsertWORKSIGNByImport(ByVal dtData As DataTable,
                                           ByVal period_id As Decimal,
                                           ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function InsertWorkSign(ByVal objWorkSigns As List(Of AT_WORKSIGNDTO), ByVal objWork As AT_WORKSIGNDTO, ByVal p_fromdate As Date, ByVal p_endDate As Date?, ByVal log As UserLog, ByRef gID As Decimal)
        <OperationContract()>
        Function ValidateWORKSIGN(ByVal objWORKSIGN As AT_WORKSIGNDTO) As Boolean

        <OperationContract()>
        Function ModifyWORKSIGN(ByVal objWORKSIGN As AT_WORKSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteWORKSIGN(ByVal lstWORKSIGN() As AT_WORKSIGNDTO) As Boolean
        <OperationContract()>
        Function GETSIGNDEFAULT(ByVal param As ParamDTO, ByVal log As UserLog) As DataTable
        <OperationContract()>
        Function Del_WorkSign_ByEmp(ByVal employee_id As Decimal, ByVal p_From As Date, ByVal p_to As Date) As Boolean
#End Region

#Region "ProjectAssign"
        <OperationContract()>
        Function GET_ProjectAssign(ByVal param As AT_PROJECT_ASSIGNDTO, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function InsertProjectAssign(ByVal objProjectAssigns As List(Of AT_PROJECT_ASSIGNDTO), ByVal objWork As AT_PROJECT_ASSIGNDTO, ByVal p_fromdate As Date, ByVal p_endDate As Date?, ByVal log As UserLog, ByRef gID As Decimal)

        <OperationContract()>
        Function ModifyProjectAssign(ByVal objProjectAssign As AT_PROJECT_ASSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteProjectAssign(ByVal lstProjectAssign() As AT_PROJECT_ASSIGNDTO) As Boolean
#End Region

#Region "Holiday"
        <OperationContract()>
        Function GetHoliday(ByVal _filter As AT_HOLIDAYDTO,
                             Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAYDTO)
        <OperationContract()>
        Function GetHoliday_Hose(ByVal _filter As AT_HOLIDAYDTO,
                             Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAYDTO)
        <OperationContract()>
        Function InsertHOLIDAY(ByVal objHOLIDAY As AT_HOLIDAYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertHOLIDAY_Hose(ByVal objHOLIDAY As AT_HOLIDAYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateHOLIDAY(ByVal objHOLIDAY As AT_HOLIDAYDTO) As Boolean
        <OperationContract()>
        Function ValidateHOLIDAY_Hose(ByVal objHOLIDAY As AT_HOLIDAYDTO) As Boolean

        <OperationContract()>
        Function ModifyHOLIDAY(ByVal objHOLIDAY As AT_HOLIDAYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveHoliday(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteHOLIDAY(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function DeleteHOLIDAY_Hose(ByVal lstID As List(Of Decimal)) As Boolean

#End Region

#Region "Holiday gereden"
        <OperationContract()>
        Function GetHolidayGerenal(ByVal _filter As AT_HOLIDAY_GENERALDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAY_GENERALDTO)
        <OperationContract()>
        Function InsertHolidayGerenal(ByVal objHOLIDAYGR As AT_HOLIDAY_GENERALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateHolidayGerenal(ByVal objHOLIDAYGR As AT_HOLIDAY_GENERALDTO) As Boolean

        <OperationContract()>
        Function ModifyHolidayGerenal(ByVal objHOLIDAYGR As AT_HOLIDAY_GENERALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveHolidayGerenal(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteHolidayGerenal(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "AT_FML danh mục kiểu công"
        <OperationContract()>
        Function GetSignByPage(ByVal pagecode As String) As List(Of AT_TIME_MANUALDTO)
        <OperationContract()>
        Function GetAT_FML(ByVal _filter As AT_FMLDTO,
                               Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_FMLDTO)
        <OperationContract()>
        Function InsertAT_FML(ByVal objATFML As AT_FMLDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_FML(ByVal objATFML As AT_FMLDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_FML(ByVal objATFML As AT_FMLDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_FML(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_FML(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Danh mục ca làm việc"
        <OperationContract()>
        Function GetAT_GSIGN(ByVal _filter As AT_GSIGNDTO,
                               Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_GSIGNDTO)
        <OperationContract()>
        Function InsertAT_GSIGN(ByVal objGSIGN As AT_GSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_GSIGN(ByVal objGSIGN As AT_GSIGNDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_GSIGN(ByVal objGSIGN As AT_GSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_GSIGN(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_GSIGN(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Quy định phạt đi sớm về muộn"
        <OperationContract()>
        Function GetAT_DMVS(ByVal _filter As AT_DMVSDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_DMVSDTO)
        <OperationContract()>
        Function InsertAT_DMVS(ByVal objData As AT_DMVSDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_DMVS(ByVal objData As AT_DMVSDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_DMVS(ByVal objData As AT_DMVSDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_DMVS(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_DMVS(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "May cham cong"
        <OperationContract()>
        Function GetTerminalFromOtOtherList() As DataTable

        <OperationContract()>
        Function GetTerminal(ByVal obj As AT_TERMINALSDTO, ByVal log As UserLog) As DataTable

        <OperationContract()>
        Function GetTerminalAuto() As DataTable

        <OperationContract()>
        Function UpdateTerminalLastTime(ByVal obj As AT_TERMINALSDTO) As Boolean

        <OperationContract()>
        Function UpdateTerminalStatus(ByVal obj As AT_TERMINALSDTO) As Boolean

        <OperationContract()>
        Function GetSwipeData(ByVal _filter As AT_SWIPE_DATADTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "iTime_id, VALTIME desc") As List(Of AT_SWIPE_DATADTO)
        <OperationContract()>
        Function InsertSwipeData(ByVal objSwipeData As List(Of AT_SWIPE_DATADTO), ByVal machine As AT_TERMINALSDTO, ByVal P_FROMDATE As Date?, ByVal P_ENDDATE As Date?, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ImportSwipeDataAuto(ByVal lstSwipeData As List(Of AT_SWIPE_DATADTO)) As Boolean

        <OperationContract()>
        Function InsertSwipeDataImport(ByVal objDelareRice As List(Of AT_SWIPE_DATADTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ImportSwipeData(ByVal dtData As DataTable, ByVal log As UserLog) As Boolean

#End Region

#Region "Danh mục ca làm việc"
        <OperationContract()>
        Function GetAT_SHIFT(ByVal _filter As AT_SHIFTDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SHIFTDTO)
        <OperationContract()>
        Function InsertAT_SHIFT(ByVal objData As AT_SHIFTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_SHIFT(ByVal objData As AT_SHIFTDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_SHIFT(ByVal objData As AT_SHIFTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_SHIFT(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_SHIFT(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetAT_TIME_MANUALBINCOMBO() As DataTable

        <OperationContract()>
        Function ValidateCombobox(ByVal cbxData As ComboBoxDataDTO) As Boolean
#End Region

#Region "Thiết lập số ngày nghỉ theo đối tượng"
        <OperationContract()>
        Function GetAT_Holiday_Object(ByVal _filter As AT_HOLIDAY_OBJECTDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAY_OBJECTDTO)
        <OperationContract()>
        Function InsertAT_Holiday_Object(ByVal objHoliO As AT_HOLIDAY_OBJECTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_Holiday_Object(ByVal objHoliO As AT_HOLIDAY_OBJECTDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_Holiday_Object(ByVal objHoliO As AT_HOLIDAY_OBJECTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_Holiday_Object(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_Holiday_Object(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Thiết lập đối tượng chấm công theo cấp nhân sự"
        <OperationContract()>
        Function GetAT_SETUP_SPECIAL(ByVal _filter As AT_SETUP_SPECIALDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_SPECIALDTO)
        <OperationContract()>
        Function InsertAT_SETUP_SPECIAL(ByVal objData As AT_SETUP_SPECIALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_SETUP_SPECIAL(ByVal objData As AT_SETUP_SPECIALDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_SETUP_SPECIAL(ByVal objData As AT_SETUP_SPECIALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_SETUP_SPECIAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_SETUP_SPECIAL(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Thiết lập đối tượng chấm công theo nhân viên"
        <OperationContract()>
        Function GetAT_SETUP_TIME_EMP(ByVal _filter As AT_SETUP_TIME_EMPDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                Optional ByVal PageSize As Integer = Integer.MaxValue,
                                Optional ByRef Total As Integer = 0,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_TIME_EMPDTO)
        <OperationContract()>
        Function InsertAT_SETUP_TIME_EMP(ByVal objData As AT_SETUP_TIME_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_SETUP_TIME_EMP(ByVal objData As AT_SETUP_TIME_EMPDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_SETUP_TIME_EMP(ByVal objData As AT_SETUP_TIME_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_SETUP_TIME_EMP(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_SETUP_TIME_EMP(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Đăng ký máy chấm công"
        <OperationContract()>
        Function GetAT_TERMINAL(ByVal _filter As AT_TERMINALSDTO,
                                   Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_TERMINALSDTO)
        <OperationContract()>
        Function GetAT_TERMINAL_STATUS(ByVal _filter As AT_TERMINALSDTO,
                                   Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_TERMINALSDTO)

        <OperationContract()>
        Function InsertAT_TERMINAL(ByVal objData As AT_TERMINALSDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_TERMINAL(ByVal objData As AT_TERMINALSDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_TERMINAL(ByVal objData As AT_TERMINALSDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_TERMINAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_TERMINAL(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Đăng ký chấm công mặc định"
        <OperationContract()>
        Function GetAT_SIGNDEFAULT(ByVal _filter As AT_SIGNDEFAULTDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of AT_SIGNDEFAULTDTO)
        <OperationContract()>
        Function InsertAT_SIGNDEFAULT(ByVal objSIGNDEF As AT_SIGNDEFAULTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function GetAT_ListShift() As DataTable

        <OperationContract()>
        Function GetAT_PERIOD() As DataTable

        <OperationContract()>
        Function GetEmployeeID(ByVal employee_code As String, ByVal period_id As Decimal) As DataTable

        <OperationContract()>
        Function GetEmployeeIDInSign(ByVal employee_code As String) As DataTable
        <OperationContract()>
        Function GetEmployeeByTimeID(ByVal time_id As Decimal) As DataTable

        <OperationContract()>
        Function ModifyAT_SIGNDEFAULT(ByVal objSIGNDEF As AT_SIGNDEFAULTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_SIGNDEFAULT(ByVal objSIGNDEF As AT_SIGNDEFAULTDTO) As Boolean

        <OperationContract()>
        Function ActiveAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Đăng ký OT trên portal"
        <OperationContract()>
        Function GetOtRegistration(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                         Optional ByRef Total As Integer = 0,
                                         Optional ByVal PageIndex As Integer = 0,
                                         Optional ByVal PageSize As Integer = Integer.MaxValue,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of AT_OT_REGISTRATIONDTO)
        <OperationContract()>
        Function InsertOtRegistration(ByVal obj As AT_OT_REGISTRATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyotRegistration(ByVal obj As AT_OT_REGISTRATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ApproveOtRegistration(ByVal obj As List(Of AT_OT_REGISTRATIONDTO), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ValidateOtRegistration(ByVal _validate As AT_OT_REGISTRATIONDTO)
        <OperationContract()>
        Function HRReviewOtRegistration(ByVal lst As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function DeleteOtRegistration(ByVal lstId As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function GET_REG_PORTAL(ByVal empid As Decimal, ByVal startdate As Date, ByVal enddate As Date,
                                ByVal strId As String, ByVal type As String) As List(Of APPOINTMENT_DTO)
        <OperationContract()>
        Function GET_TOTAL_OT_APPROVE(ByVal empid As Decimal, ByVal enddate As Date) As Decimal
        <OperationContract()>
        Function AT_CHECK_ORG_PERIOD_STATUS_OT(ByVal LISTORG As String, ByVal PERIOD As Decimal) As Int32
        <OperationContract()>
        Function GET_LIST_HOURS() As DataTable
        <OperationContract()>
        Function GET_LIST_MINUTE() As DataTable
        <OperationContract()>
        Function PRI_PROCESS_APP(ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal process_type As String, ByVal totalHours As Decimal, ByVal totalDay As Decimal, ByVal sign_id As Integer, ByVal id_reggroup As Integer) As Int32
        <OperationContract()>
        Function GET_SEQ_PORTAL_RGT() As Decimal
        <OperationContract()>
        Function GET_ORGID(ByVal EMPID As Integer) As Int32
        <OperationContract()>
        Function GET_PERIOD(ByVal DATE_CURRENT As Date) As Int32
        <OperationContract()>
        Function AT_CHECK_EMPLOYEE(ByVal EMPID As Decimal, ByVal ENDDATE As Date) As Int32
        <OperationContract()>
        Function GET_TOTAL_OT_APPROVE3(ByVal EMPID As Decimal?, ByVal ENDDATE As Date) As Decimal
        <OperationContract()>
        Function CHECK_RGT_OT(ByVal EMPID As Decimal, ByVal STARTDATE As Date, ByVal ENDDATE As Date, _
                                 ByVal FROM_HOUR As String, ByVal TO_HOUR As String, ByVal HOUR_RGT As Decimal) As Int32
#End Region

#Region "Đăng ký nghỉ trên portal"
        <OperationContract()>
        Function GetHolidayByCalenderToTable(ByVal startdate As Date, ByVal enddate As Date) As DataTable
        <OperationContract()>
        Function GetPlanningAppointmentByEmployee(ByVal empid As Decimal, ByVal startdate As DateTime, ByVal enddate As DateTime, _
                                                  ByVal listSign As List(Of AT_TIME_MANUALDTO)) As List(Of AT_TIMESHEET_REGISTERDTO)
        <OperationContract()>
        Function InsertPortalRegister(ByVal itemRegister As AT_PORTAL_REG_DTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetHolidayByCalender(ByVal startdate As Date, ByVal enddate As Date) As List(Of Date)
        <OperationContract()>
        Function GetRegisterAppointmentInPortalByEmployee(ByVal empid As Decimal, ByVal startdate As Date, ByVal enddate As Date,
                                                          ByVal listSign As List(Of AT_TIME_MANUALDTO), ByVal status As List(Of Short)) As List(Of AT_TIMESHEET_REGISTERDTO)
        <OperationContract()>
        Function GetRegisterAppointmentInPortalOT(ByVal empid As Decimal, ByVal startdate As Date, ByVal enddate As Date,
                                                          ByVal listSign As List(Of OT_OTHERLIST_DTO), ByVal status As List(Of Short)) As List(Of AT_TIMESHEET_REGISTERDTO)
        <OperationContract()>
        Function GetTotalLeaveInYear(ByVal empid As Decimal, ByVal p_year As Decimal) As Decimal

        <OperationContract()>
        Function DeletePortalRegisterByDate(ByVal listappointment As List(Of AT_TIMESHEET_REGISTERDTO), ByVal listSign As List(Of AT_TIME_MANUALDTO)) As Boolean
        <OperationContract()>
        Function DeletePortalRegisterByDateOT(ByVal listappointment As List(Of AT_TIMESHEET_REGISTERDTO), ByVal listSign As List(Of OT_OTHERLIST_DTO)) As Boolean
        <OperationContract()>
        Function DeletePortalRegister(ByVal id As Decimal) As Boolean
        <OperationContract()>
        Function SendRegisterToApprove(ByVal objLstRegisterId As List(Of Decimal), ByVal process As String, ByVal currentUrl As String) As String

#End Region

#Region "Phê duyệt đang ký nghỉ trên portal"
        <OperationContract()>
        Function GetListSignCode(ByVal gSignCode As String) As List(Of AT_TIME_MANUALDTO)
        <OperationContract()>
        Function GetListWaitingForApprove(ByVal approveId As Decimal, ByVal process As String, ByVal filter As ATRegSearchDTO) As List(Of AT_PORTAL_REG_DTO)
        <OperationContract()>
        Function GetListWaitingForApproveOT(ByVal approveId As Decimal, ByVal process As String, ByVal filter As ATRegSearchDTO) As List(Of AT_PORTAL_REG_DTO)
        <OperationContract()>
        Function ApprovePortalRegister(ByVal regID As Decimal?, ByVal approveId As Decimal,
                                       ByVal status As Integer, ByVal note As String,
                                       ByVal currentUrl As String, ByVal process As String,
                                             ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetEmployeeList() As DataTable
        <OperationContract()>
        Function GetLeaveDay(ByVal dDate As Date) As DataTable
#End Region

#Region "Thiết lập kiểu công"
        <OperationContract()>
        Function GetAT_TIME_MANUAL(ByVal _filter As AT_TIME_MANUALDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                       Optional ByVal PageSize As Integer = Integer.MaxValue,
                                       Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TIME_MANUALDTO)
        <OperationContract()>
        Function GetAT_TIME_MANUALById(ByVal _id As Decimal?) As AT_TIME_MANUALDTO
        <OperationContract()>
        Function InsertAT_TIME_MANUAL(ByVal objHOLIDAY As AT_TIME_MANUALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_TIME_MANUAL(ByVal objHOLIDAY As AT_TIME_MANUALDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_TIME_MANUAL(ByVal objHOLIDAY As AT_TIME_MANUALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_TIME_MANUAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_TIME_MANUAL(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetDataImportCO() As DataTable
#End Region

#Region "Danh mục tham số hệ thống"
        <OperationContract()>
        Function GetListParamItime(ByVal _filter As AT_LISTPARAM_SYSTEAMDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_LISTPARAM_SYSTEAMDTO)
        <OperationContract()>
        Function InsertListParamItime(ByVal objHOLIDAY As AT_LISTPARAM_SYSTEAMDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateListParamItime(ByVal objHOLIDAY As AT_LISTPARAM_SYSTEAMDTO) As Boolean

        <OperationContract()>
        Function ModifyListParamItime(ByVal objHOLIDAY As AT_LISTPARAM_SYSTEAMDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveListParamItime(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteListParamItime(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Báo cáo"
        <OperationContract()>
        Function GET_REPORT() As DataTable
        <OperationContract()>
        Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)

        <OperationContract()>
        Function GET_AT001(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function GET_AT002(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function GET_AT003(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function GET_AT004(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function GET_AT005(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function GET_AT006(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function GET_AT007(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function GET_AT008(ByVal obj As ParamDTO, ByVal P_DATE As Date, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function GET_AT009(ByVal obj As ParamDTO, ByVal P_FROMDATE As Date, ByVal P_TODATE As Date, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function GET_AT010(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function GET_AT011(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
#End Region

#Region "Systeam"
        <OperationContract()>
        Function CheckPeriodClose(ByVal lstEmp As List(Of Decimal), ByVal startdate As Date, ByVal enddate As Date, ByRef sAction As String) As Boolean
        <OperationContract()>
        Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
        <OperationContract()>
        Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As AttendanceCommon.TABLE_NAME) As Boolean
        <OperationContract()>
        Function CheckExistInDatabaseAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal), ByVal lstWorking As List(Of Date), ByVal lstShift As List(Of Decimal), ByVal table As AttendanceCommon.TABLE_NAME) As Boolean
#End Region

#Region "IPORTAL - View bảng công"

        <OperationContract()>
        Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean
#End Region

#Region "LOG"
        <OperationContract()>
        Function GetActionLog(ByVal _filter As AT_ACTION_LOGDTO,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByVal Sorts As String = "ACTION_DATE desc") As List(Of AT_ACTION_LOGDTO)
        <OperationContract()>
        Function DeleteActionLogsAT(ByVal lstDeleteIds As List(Of Decimal)) As Integer
#End Region


#Region "AT_PROJECT_TITLE"

        <OperationContract()>
        Function GetAT_PROJECT_TITLE(ByVal _filter As AT_PROJECT_TITLEDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECT_TITLEDTO)
        <OperationContract()>
        Function InsertAT_PROJECT_TITLE(ByVal objATFML As AT_PROJECT_TITLEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_PROJECT_TITLE(ByVal objATFML As AT_PROJECT_TITLEDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_PROJECT_TITLE(ByVal objATFML As AT_PROJECT_TITLEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteAT_PROJECT_TITLE(ByVal lstID As List(Of Decimal)) As Boolean

#End Region

#Region "AT_PROJECT"

        <OperationContract()>
        Function GetAT_PROJECT(ByVal _filter As AT_PROJECTDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECTDTO)
        <OperationContract()>
        Function InsertAT_PROJECT(ByVal objATFML As AT_PROJECTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_PROJECT(ByVal objATFML As AT_PROJECTDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_PROJECT(ByVal objATFML As AT_PROJECTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteAT_PROJECT(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ValidateOtherList(ByVal objOtherList As OT_OTHERLIST_DTO) As Boolean

#End Region

#Region "AT_PROJECT_WORK"

        <OperationContract()>
        Function GetAT_PROJECT_WORK(ByVal _filter As AT_PROJECT_WORKDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECT_WORKDTO)
        <OperationContract()>
        Function InsertAT_PROJECT_WORK(ByVal objATFML As AT_PROJECT_WORKDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_PROJECT_WORK(ByVal objATFML As AT_PROJECT_WORKDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_PROJECT_WORK(ByVal objATFML As AT_PROJECT_WORKDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteAT_PROJECT_WORK(ByVal lstID As List(Of Decimal)) As Boolean

#End Region

#Region "AT_PROJECT_EMP"

        <OperationContract()>
        Function GetAT_PROJECT_EMP(ByVal _filter As AT_PROJECT_EMPDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECT_EMPDTO)
        <OperationContract()>
        Function InsertAT_PROJECT_EMP(ByVal objATFML As AT_PROJECT_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAT_PROJECT_EMP(ByVal objATFML As AT_PROJECT_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteAT_PROJECT_EMP(ByVal lstID As List(Of Decimal)) As Boolean

#End Region

#Region "Dashboard"
        <OperationContract()>
        Function GetStatisticTotalWorking(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticTimeOff(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticTimeOtByOrg(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticTimeProject(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)


#End Region
#Region "quan ly cham cong bu tru"
        <OperationContract()>
        Function InsertOffSettingTime(ByVal objOffSetting As AT_OFFFSETTINGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyOffSettingTime(ByVal objOffSetting As AT_OFFFSETTINGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
#End Region
#Region "Portal quan ly nghi phep, nghi bu"
        <OperationContract()>
        Function GetTotalDayOff(ByVal _filter As TotalDayOffDTO, Optional ByVal log As UserLog = Nothing) As TotalDayOffDTO

        <OperationContract()>
        Function GET_TIME_MANUAL(ByVal _filter As TotalDayOffDTO, Optional ByVal log As UserLog = Nothing) As TotalDayOffDTO

        <OperationContract()>
        Function GetHistoryLeave(ByVal _filter As HistoryLeaveDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "REGDATE DESC", Optional ByVal log As UserLog = Nothing) As List(Of HistoryLeaveDTO)

#End Region
#Region "AT_PORTAL_REG_LIST"
        <OperationContract()>
        Function GetLeaveRegistrationList(ByVal _filter As AT_PORTAL_REG_DTO,
                                   Optional ByRef Total As Integer = 0,
                                   Optional ByVal PageIndex As Integer = 0,
                                   Optional ByVal PageSize As Integer = Integer.MaxValue,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_PORTAL_REG_DTO)
        <OperationContract()>
        Function CheckTimeSheetApproveVerify(ByVal obj As List(Of AT_PROCESS_DTO), ByVal type As String, ByRef itemError As AT_PROCESS_DTO) As Boolean
        <OperationContract()>
        Function DeletePortalReg(ByVal lstId As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function ApprovePortalRegList(ByVal obj As List(Of AT_PORTAL_REG_LIST_DTO), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetEmployeeInfor(ByVal P_EmpId As Decimal?, ByVal P_Org_ID As Decimal?, Optional ByVal fromDate As Date? = Nothing) As DataTable
        <OperationContract()>
        Function GetLeaveRegistrationById(ByVal _filter As AT_PORTAL_REG_DTO) As AT_PORTAL_REG_DTO
        <OperationContract()>
        Function GetLeaveEmpDetail(ByVal employee_Id As Decimal, ByVal fromDate As Date, ByVal toDate As Date, Optional ByVal isUpdate As Boolean = False) As List(Of LEAVE_DETAIL_EMP_DTO)
        <OperationContract()>
        Function GetLeaveRegistrationDetailById(ByVal listId As Decimal) As List(Of AT_PORTAL_REG_DTO)
        <OperationContract()>
        Function InsertPortalRegList(ByVal obj As AT_PORTAL_REG_LIST_DTO, ByVal lstObjDetail As List(Of AT_PORTAL_REG_DTO), ByVal log As UserLog, ByRef gID As Decimal, ByRef itemExist As AT_PORTAL_REG_DTO, ByRef isOverAnnualLeave As Boolean) As Boolean
        <OperationContract()>
        Function ModifyPortalRegList(ByVal obj As AT_PORTAL_REG_DTO, ByVal itemRegister As AT_PORTAL_REG_DTO, ByVal log As UserLog) As Boolean
#End Region
#Region "SHIFT CYCLE"
        <OperationContract()>
        Function GetShiftCycle(ByVal _filter As AT_SHIFTCYCLEDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of AT_SHIFTCYCLEDTO)
        <OperationContract()>
        Function GetEmployeeShifts(ByVal employee_Id As Decimal, ByVal fromDate As Date, ByVal toDate As Date) As List(Of EMPLOYEE_SHIFT_DTO)
#End Region

        <OperationContract()>
        Function GET_PE_ASSESS_MESS(ByVal EMP As Decimal?) As DataTable
    End Interface
End Namespace

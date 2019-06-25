Imports System.IO
Imports Framework.Data
Imports System.Data.Objects
Imports Framework.Data.System.Linq.Dynamic
Imports System.Data.Common
Imports System.Reflection

Partial Class ProfileRepository

#Region "Other"
    Public Function GetCurrentPeriod() As DataTable
        Try
            Dim query = (From p In Context.AT_PERIOD
                        Where p.YEAR = DateTime.Now.Year
                        Select New With {.ID = p.ID, .PERIOD_NAME = p.PERIOD_NAME}).ToList.ToTable
            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "Debt"
    Public Function GetDebt(ByVal empId As Decimal, ByVal PageIndex As Integer, ByVal PageSize As Integer, ByVal Total As Integer) As List(Of DebtDTO)
        Try
            Dim query = (From p In Context.HU_DEBT
                         From debt_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DEBT_TYPE_ID).DefaultIfEmpty
                         From debt_status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DEBT_STATUS).DefaultIfEmpty
                         Where p.EMPLOYEE_ID = empId)
            Dim sql = query.Select(Function(p) New DebtDTO With {
                                         .ID = p.p.ID,
                                         .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                         .DEBT_STATUS = p.p.DEBT_STATUS,
                                         .DEBT_STATUS_NAME = p.debt_status.NAME_VN,
                                         .DEBT_TYPE_ID = p.p.DEBT_TYPE_ID,
                                         .DEBT_TYPE_NAME = p.debt_type.NAME_VN,
                                         .MONEY = p.p.MONEY,
                                         .REMARK = p.p.REMARK})
            sql = sql.OrderBy(Function(f) f.DEBT_TYPE_ID)
            sql = sql.Skip(PageIndex * PageSize).Take(Total)
            Return sql.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")

        End Try
    End Function
    Public Function InsertDebt(ByVal objDebt As DebtDTO,
                                   ByVal log As UserLog) As Boolean
        Try
            Dim objDebtData As New HU_DEBT
            objDebtData.ID = Utilities.GetNextSequence(Context, Context.HU_DEBT.EntitySet.Name)
            objDebtData.DEBT_TYPE_ID = objDebt.DEBT_TYPE_ID
            objDebtData.DEBT_STATUS = objDebt.DEBT_STATUS
            objDebtData.MONEY = objDebt.MONEY
            objDebtData.REMARK = objDebt.REMARK
            objDebtData.CREATED_DATE = DateTime.Now
            objDebtData.CREATED_BY = log.Username
            objDebtData.CREATED_LOG = log.ComputerName
            objDebtData.MODIFIED_DATE = DateTime.Now
            objDebtData.MODIFIED_BY = log.Username
            objDebtData.MODIFIED_LOG = log.ComputerName
            Context.HU_DEBT.AddObject(objDebtData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ModifyDebt(ByVal objDebt As DebtDTO, ByVal log As UserLog) As Boolean
        Dim objDebtData As New HU_DEBT With {.ID = objDebt.ID}
        Try
            objDebtData = (From p In Context.HU_DEBT Where p.ID = objDebtData.ID).FirstOrDefault
            objDebtData.DEBT_TYPE_ID = objDebt.DEBT_TYPE_ID
            objDebtData.DEBT_STATUS = objDebt.DEBT_STATUS
            objDebtData.MONEY = objDebt.MONEY
            objDebtData.REMARK = objDebt.REMARK
            objDebtData.MODIFIED_DATE = DateTime.Now
            objDebtData.MODIFIED_BY = log.Username
            objDebtData.MODIFIED_LOG = log.ComputerName
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

#Region "Terminate"
    Public Function CalculateTerminate(ByVal EmployeeId As Decimal, ByVal TerLateDate As Date) As DataTable
        Try
            Using rep As New DataAccess.QueryData
                Dim dt As DataTable = rep.ExecuteStore("PKG_PROFILE_BUSINESS.GET_INFO_TERMINATE",
                                                       New With {.P_EMPLOYEE_ID = EmployeeId,
                                                                 .P_TER_DATE = TerLateDate,
                                                                 .P_CUR = rep.OUT_CURSOR}, True)
                Return dt
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return Nothing
        End Try
    End Function

    Public Function GetLabourProtectByTerminate(ByVal gID As Decimal) As List(Of LabourProtectionMngDTO)
        Try
            Dim sql = (From l In Context.HU_LABOURPROTECTION_MNG
                       From p In Context.HU_LABOURPROTECTION.Where(Function(p) p.ID = l.LABOURPROTECTION_ID)
                       Where l.EMPLOYEE_ID = gID
                       Select New LabourProtectionMngDTO With
                              {.ID = l.ID,
                               .LABOURPROTECTION_ID = l.LABOURPROTECTION_ID,
                               .LABOURPROTECTION_NAME = p.NAME,
                               .QUANTITY = l.QUANTITY,
                               .RETRIEVED = l.RETRIEVED,
                               .DAYS_ALLOCATED = l.DAYS_ALLOCATED,
                               .RETRIEVE_DATE = l.RETRIEVE_DATE,
                               .INDEMNITY = l.INDEMNITY,
                               .UNIT_PRICE = p.UNIT_PRICE,
                               .SDESC = l.SDESC,
                               .DEPOSIT = l.DEPOSIT}).ToList

            Return sql
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")

        End Try
    End Function

    Public Function GetAssetByTerminate(ByVal gID As Decimal) As List(Of AssetMngDTO)
        Try
            Dim query = (From p In Context.HU_ASSET_MNG
                         From d In Context.HU_ASSET.Where(Function(d) d.ID = p.ASSET_DECLARE_ID)
                         From assetgroup In Context.OT_OTHER_LIST.Where(Function(f) f.ID = d.GROUP_ID _
                                                                            And f.TYPE_ID = ProfileCommon.OT_ASSET_GROUP.TYPE_ID).DefaultIfEmpty
                         From assetstatus In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID _
                                                                            And f.TYPE_ID = ProfileCommon.OT_ASSET_STATUS.TYPE_ID).DefaultIfEmpty
                         Where p.EMPLOYEE_ID = gID
                         Order By d.CODE)


            Dim sql = query.Select(Function(p) New AssetMngDTO With {
                                         .ID = p.p.ID,
                                         .ASSET_CODE = p.d.CODE,
                                         .ASSET_NAME = p.d.NAME,
                                         .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                         .ASSET_VALUE = p.p.ASSET_VALUE,
                                         .ASSET_GROUP_NAME = p.assetgroup.NAME_VN,
                                         .DEPOSITS = p.p.DEPOSITS,
                                         .DESC = p.p.SDESC,
                                         .ISSUE_DATE = p.p.ISSUE_DATE,
                                         .RETURN_DATE = p.p.RETURN_DATE,
                                         .ORG_TRANFER = p.p.ORG_TRANFER,
                                         .ORG_RECEIVE = p.p.ORG_RECEIVE,
                                         .ASSET_BARCODE = p.p.ASSET_BARCODE,
                                         .ASSET_SERIAL = p.p.ASSET_SERIAL,
                                         .STATUS_ID = p.p.STATUS_ID,
                                         .REMARK = p.d.REMARK,
                                         .QUANTITY = p.d.QUANTITY,
                                         .STATUS_NAME = p.assetstatus.NAME_VN}).ToList

            For Each item In sql
                If item.RETURN_DATE IsNot Nothing And item.ISSUE_DATE IsNot Nothing Then
                    item.DATE_USE = (item.RETURN_DATE - item.ISSUE_DATE).Value.Days + 1
                End If
            Next

            Return sql
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")

        End Try
    End Function

    Public Function ApproveListTerminate(ByVal listID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim objTerData As HU_TERMINATE
        Try
            Dim item As Decimal = 0
            For idx = 0 To listID.Count - 1
                item = listID(idx)
                Dim objTerminateData As New TerminateDTO
                Dim objTerminate = (From p In Context.HU_TERMINATE Where p.ID = item).FirstOrDefault
                objTerminateData.EMPLOYEE_ID = objTerminate.EMPLOYEE_ID
                objTerminateData.IS_NOHIRE = objTerminate.IS_NOHIRE
                objTerminateData.UPLOADFILE = objTerminate.UPLOADFILE
                objTerminateData.FILENAME = objTerminate.FILENAME
                objTerminateData.LAST_DATE = objTerminate.LAST_DATE
                objTerminateData.SEND_DATE = objTerminate.SEND_DATE
                objTerminateData.STATUS_ID = objTerminate.STATUS_ID
                objTerminateData.TER_REASON_DETAIL = objTerminate.TER_REASON_DETAIL
                objTerminateData.EMPLOYEE_SENIORITY = objTerminate.EMP_SENIORITY
                objTerminateData.REMARK = objTerminate.REMARK
                objTerminateData.MODIFIED_DATE = DateTime.Now
                objTerminateData.MODIFIED_BY = log.Username
                objTerminateData.MODIFIED_LOG = log.ComputerName

                objTerminateData.APPROVAL_DATE = objTerminate.APPROVAL_DATE
                objTerminateData.IDENTIFI_CARD = objTerminate.IDENTIFI_CARD
                objTerminateData.IDENTIFI_RDATE = objTerminate.IDENTIFI_RDATE
                objTerminateData.IDENTIFI_STATUS = objTerminate.IDENTIFI_STATUS
                objTerminateData.IDENTIFI_MONEY = objTerminate.IDENTIFI_MONEY
                objTerminateData.SUN_CARD = objTerminate.SUN_CARD
                objTerminateData.SUN_RDATE = objTerminate.SUN_RDATE
                objTerminateData.SUN_STATUS = objTerminate.SUN_STATUS
                objTerminateData.SUN_MONEY = objTerminate.SUN_MONEY
                objTerminateData.INSURANCE_CARD = objTerminate.INSURANCE_CARD
                objTerminateData.INSURANCE_RDATE = objTerminate.INSURANCE_RDATE
                objTerminateData.INSURANCE_STATUS = objTerminate.INSURANCE_STATUS
                objTerminateData.INSURANCE_MONEY = objTerminate.INSURANCE_MONEY
                objTerminateData.REMAINING_LEAVE = objTerminate.REMAINING_LEAVE
                objTerminateData.PAYMENT_LEAVE = objTerminate.PAYMENT_LEAVE
                objTerminateData.AMOUNT_VIOLATIONS = objTerminate.AMOUNT_VIOLATIONS
                objTerminateData.AMOUNT_WRONGFUL = objTerminate.AMOUNT_WRONGFUL
                objTerminateData.ALLOWANCE_TERMINATE = objTerminate.ALLOWANCE_TERMINATE
                objTerminateData.TRAINING_COSTS = objTerminate.TRAINING_COSTS
                objTerminateData.OTHER_COMPENSATION = objTerminate.OTHER_COMPENSATION
                objTerminateData.COMPENSATORY_LEAVE = objTerminate.COMPENSATORY_LEAVE
                objTerminateData.COMPENSATORY_PAYMENT = objTerminate.COMPENSATORY_PAYMENT
                objTerminateData.EFFECT_DATE = objTerminate.EFFECT_DATE
                objTerminateData.DECISION_NO = objTerminate.DECISION_NO
                objTerminateData.SIGN_DATE = objTerminate.SIGN_DATE
                objTerminateData.SIGN_ID = objTerminate.SIGN_ID
                objTerminateData.SIGN_NAME = objTerminate.SIGN_NAME
                objTerminateData.SIGN_TITLE = objTerminate.SIGN_TITLE

                'bỔ XUNG THÊM TRƯỜNG
                objTerminateData.SALARYMEDIUM = objTerminate.SALARYMEDIUM
                objTerminateData.YEARFORALLOW = objTerminate.YEARFORALLOW
                objTerminateData.MONEYALLOW = objTerminate.MONEYALLOW
                objTerminateData.REMAINING_LEAVE_MONEY = objTerminate.REMAINING_LEAVE_MONEY
                objTerminateData.IDENTIFI_VIOLATE_MONEY = objTerminate.IDENTIFI_VIOLATE_MONEY
                objTerminateData.MONEY_RETURN = objTerminate.MONEY_RETURN
                objTerminateData.ORG_ID = objTerminate.ORG_ID
                objTerminateData.TITLE_ID = objTerminate.TITLE_ID
                objTerminateData.TYPE_TERMINATE = objTerminate.TYPE_TERMINATE
                objTerminateData.SALARYMEDIUM_LOSS = objTerminate.SALARYMEDIUM_LOSS
                'objTerminateData.Expire_Date = objTerminate.EXPIRE_DATE


                Dim lstAll = (From p In Context.HU_TERMINATE_REASON Where p.HU_TERMINATE_ID = objTerminate.ID).ToList
                For Each row In lstAll
                    Context.HU_TERMINATE_REASON.DeleteObject(row)
                Next

                If objTerminateData.lstReason IsNot Nothing Then
                    For Each obj In objTerminateData.lstReason
                        Dim allow As New HU_TERMINATE_REASON
                        allow.ID = Utilities.GetNextSequence(Context, Context.HU_TERMINATE_REASON.EntitySet.Name)
                        allow.HU_TERMINATE_ID = objTerminateData.ID
                        allow.TER_REASON_ID = obj.TER_REASON_ID
                        allow.DENSITY = obj.DENSITY
                        Context.HU_TERMINATE_REASON.AddObject(allow)
                    Next
                End If

                If objTerminate.STATUS_ID = ProfileCommon.OT_TER_STATUS.WAIT_APPROVE_ID Then
                    objTerminateData.STATUS_ID = ProfileCommon.OT_TER_STATUS.APPROVE_ID
                    objTerminate.STATUS_ID = ProfileCommon.OT_TER_STATUS.APPROVE_ID
                    ApproveTerminate(objTerminateData, log)
                End If
                InsertOrUpdateAssetByTerminate(objTerminateData, log)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetTerminate(ByVal _filter As TerminateDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of TerminateDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.HU_TERMINATE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) p.EMPLOYEE_ID = f.ID)
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) p.EMPLOYEE_ID = f.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) e.ORG_ID = f.ID)
                        From t In Context.HU_TITLE.Where(Function(f) e.TITLE_ID = f.ID)
                        From deci_type In Context.OT_OTHER_LIST.Where(Function(f) p.TYPE_TERMINATE = f.ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) p.STATUS_ID = f.ID)
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)
            Dim terminate = query.Select(Function(p) New TerminateDTO With {
                                             .ID = p.p.ID,
                                             .DECISION_NO = p.p.DECISION_NO,
                                             .STATUS_NAME = p.status.NAME_VN,
                                             .STATUS_CODE = p.status.CODE,
                                             .STATUS_ID = p.status.ID,
                                             .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                             .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                             .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                             .JOIN_DATE = p.e.JOIN_DATE,
                                             .CODE = p.deci_type.CODE,
                                             .IS_NOHIRE = p.p.IS_NOHIRE,
                                             .IS_NOHIRE_SHORT = p.p.IS_NOHIRE,
                                             .LAST_DATE = p.p.LAST_DATE,
                                             .SEND_DATE = p.p.SEND_DATE,
                                             .EFFECT_DATE = p.p.EFFECT_DATE,
                                             .ORG_NAME = p.o.NAME_VN,
                                             .ORG_DESC = p.o.DESCRIPTION_PATH,
                                             .TER_REASON_DETAIL = p.p.TER_REASON_DETAIL,
                                             .TITLE_NAME = p.t.NAME_VN,
                                             .APPROVAL_DATE = p.p.APPROVAL_DATE,
                                            .IDENTIFI_CARD = p.p.IDENTIFI_CARD,
                                            .IDENTIFI_RDATE = p.p.IDENTIFI_RDATE,
                                            .IDENTIFI_STATUS = p.p.IDENTIFI_STATUS,
                                             .IDENTIFI_MONEY = p.p.IDENTIFI_MONEY,
                                            .SUN_CARD = p.p.SUN_CARD,
                                            .SUN_RDATE = p.p.SUN_RDATE,
                                            .SUN_STATUS = p.p.SUN_STATUS,
                                            .SUN_MONEY = p.p.SUN_MONEY,
                                            .INSURANCE_CARD = p.p.INSURANCE_CARD,
                                            .INSURANCE_RDATE = p.p.INSURANCE_RDATE,
                                            .INSURANCE_STATUS = p.p.INSURANCE_STATUS,
                                            .INSURANCE_MONEY = p.p.INSURANCE_MONEY,
                                            .REMAINING_LEAVE = p.p.REMAINING_LEAVE,
                                            .PAYMENT_LEAVE = p.p.PAYMENT_LEAVE,
                                            .AMOUNT_VIOLATIONS = p.p.AMOUNT_VIOLATIONS,
                                            .AMOUNT_WRONGFUL = p.p.AMOUNT_WRONGFUL,
                                            .ALLOWANCE_TERMINATE = p.p.ALLOWANCE_TERMINATE,
                                            .TRAINING_COSTS = p.p.TRAINING_COSTS,
                                            .OTHER_COMPENSATION = p.p.OTHER_COMPENSATION,
                                             .COMPENSATORY_LEAVE = p.p.COMPENSATORY_LEAVE,
                                             .COMPENSATORY_PAYMENT = p.p.COMPENSATORY_PAYMENT,
                                             .CREATED_DATE = p.p.CREATED_DATE,
                                             .ID_NO = p.cv.ID_NO,
                                             .TYPE_TERMINATE = p.p.TYPE_TERMINATE,
                                             .SALARYMEDIUM = p.p.SALARYMEDIUM,
                                             .YEARFORALLOW = p.p.YEARFORALLOW,
                                             .MONEYALLOW = p.p.MONEYALLOW,
                                             .REMAINING_LEAVE_MONEY = p.p.REMAINING_LEAVE_MONEY,
                                             .IDENTIFI_VIOLATE_MONEY = p.p.IDENTIFI_VIOLATE_MONEY,
                                             .MONEY_RETURN = p.p.MONEY_RETURN,
                                             .REMARK = p.p.REMARK,
                                             .SIGN_NAME = p.p.SIGN_NAME,
                                             .SIGN_DATE = p.p.SIGN_DATE,
                                             .SIGN_TITLE = p.p.SIGN_TITLE,
                                             .SALARYMEDIUM_LOSS = p.p.SALARYMEDIUM_LOSS,
                                             .FILENAME = p.p.FILENAME,
                                             .UPLOADFILE = p.p.UPLOADFILE})

            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If

            If _filter.REMARK IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.EMPLOYEE_ID IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.FROM_LAST_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.LAST_DATE >= _filter.FROM_LAST_DATE)
            End If
            If _filter.TO_LAST_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.LAST_DATE <= _filter.TO_LAST_DATE)
            End If
            If _filter.FROM_SEND_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.SEND_DATE >= _filter.FROM_SEND_DATE)
            End If
            If _filter.TO_SEND_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.SEND_DATE <= _filter.TO_SEND_DATE)
            End If
            'If _filter.STATUS_ID <> 0 Then
            '    terminate = terminate.Where(Function(p) p.STATUS_ID = _filter.STATUS_ID)
            'End If
            If _filter.IS_NOHIRE Then
                terminate = terminate.Where(Function(p) p.IS_NOHIRE_SHORT = _filter.IS_NOHIRE)
            End If
            If _filter.ID_NO IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.ID_NO.ToUpper.Contains(_filter.ID_NO.ToUpper))
            End If

            terminate = terminate.OrderBy(Sorts)
            Total = terminate.Count
            terminate = terminate.Skip(PageIndex * PageSize).Take(PageSize)
            Return terminate.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function GetTerminateByID(ByVal _filter As TerminateDTO) As TerminateDTO

        Try
            Dim query = From p In Context.HU_TERMINATE
                        Join e In Context.HU_EMPLOYEE On p.EMPLOYEE_ID Equals e.ID
                        Join o In Context.HU_ORGANIZATION On e.ORG_ID Equals o.ID
                        Join t In Context.HU_TITLE On e.TITLE_ID Equals t.ID

            query = query.Where(Function(p) _filter.ID = p.p.ID)

            Dim obj = query.Select(Function(p) New TerminateDTO With {
                                             .ID = p.p.ID,
                                             .DECISION_NO = p.p.DECISION_NO,
                                             .STATUS_ID = p.p.STATUS_ID,
                                             .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                             .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                             .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                             .JOIN_DATE = p.e.JOIN_DATE,
                                             .IS_NOHIRE = p.p.IS_NOHIRE,
                                             .LAST_DATE = p.p.LAST_DATE,
                                             .SEND_DATE = p.p.SEND_DATE,
                                             .ORG_ID = p.o.ID,
                                             .ORG_NAME = p.o.NAME_VN,
                                             .ORG_DESC = p.o.NAME_VN,
                                             .TER_REASON_DETAIL = p.p.TER_REASON_DETAIL,
                                             .TITLE_ID = p.t.ID,
                                             .TITLE_NAME = p.t.NAME_VN,
                                             .EMPLOYEE_SENIORITY = p.p.EMP_SENIORITY,
                                             .REMARK = p.p.REMARK,
                                             .CREATED_DATE = p.p.CREATED_DATE,
                                             .APPROVAL_DATE = p.p.APPROVAL_DATE,
                                            .IDENTIFI_CARD = p.p.IDENTIFI_CARD,
                                            .IDENTIFI_RDATE = p.p.IDENTIFI_RDATE,
                                            .IDENTIFI_STATUS = p.p.IDENTIFI_STATUS,
                                             .IDENTIFI_MONEY = p.p.IDENTIFI_MONEY,
                                            .SUN_CARD = p.p.SUN_CARD,
                                            .SUN_RDATE = p.p.SUN_RDATE,
                                            .SUN_STATUS = p.p.SUN_STATUS,
                                             .SUN_MONEY = p.p.SUN_MONEY,
                                            .INSURANCE_CARD = p.p.INSURANCE_CARD,
                                            .INSURANCE_RDATE = p.p.INSURANCE_RDATE,
                                            .INSURANCE_STATUS = p.p.INSURANCE_STATUS,
                                             .INSURANCE_MONEY = p.p.INSURANCE_MONEY,
                                            .REMAINING_LEAVE = p.p.REMAINING_LEAVE,
                                            .PAYMENT_LEAVE = p.p.PAYMENT_LEAVE,
                                            .AMOUNT_VIOLATIONS = p.p.AMOUNT_VIOLATIONS,
                                            .AMOUNT_WRONGFUL = p.p.AMOUNT_WRONGFUL,
                                            .ALLOWANCE_TERMINATE = p.p.ALLOWANCE_TERMINATE,
                                            .TRAINING_COSTS = p.p.TRAINING_COSTS,
                                            .OTHER_COMPENSATION = p.p.OTHER_COMPENSATION,
                                             .COMPENSATORY_PAYMENT = p.p.COMPENSATORY_PAYMENT,
                                             .COMPENSATORY_LEAVE = p.p.COMPENSATORY_LEAVE,
                                             .EFFECT_DATE = p.p.EFFECT_DATE,
                                             .SIGN_ID = p.p.SIGN_ID,
                                             .SIGN_DATE = p.p.SIGN_DATE,
                                             .SIGN_NAME = p.p.SIGN_NAME,
                                             .SIGN_TITLE = p.p.SIGN_TITLE,
                                             .UPLOADFILE = p.p.UPLOADFILE,
                                             .FILENAME = p.p.FILENAME,
                                             .SALARYMEDIUM = p.p.SALARYMEDIUM,
                                             .YEARFORALLOW = p.p.YEARFORALLOW,
                                             .MONEYALLOW = p.p.MONEYALLOW,
                                             .REMAINING_LEAVE_MONEY = p.p.REMAINING_LEAVE_MONEY,
                                             .IDENTIFI_VIOLATE_MONEY = p.p.IDENTIFI_VIOLATE_MONEY,
                                             .MONEY_RETURN = p.p.MONEY_RETURN,
                                             .TYPE_TERMINATE = p.p.TYPE_TERMINATE,
                                             .WORK_STATUS = p.e.WORK_STATUS,
                                             .SALARYMEDIUM_LOSS = p.p.SALARYMEDIUM_LOSS,
                                             .TER_REASON = p.p.TER_REASON,
                                             .SUM_DEBT = p.p.SUM_DEBT,
                                             .SUM_COLLECT_DEBT = p.p.SUM_COLLECT_DEBT,
                                             .AMOUNT_PAYMENT_CASH = p.p.AMOUNT_PAYMENT_CASH,
                                             .AMOUNT_DEDUCT_FROM_SAL = p.p.AMOUNT_DEDUCT_FROM_SAL,
                                             .PERIOD_ID = p.p.PERIOD_ID,
                                             .DECISION_TYPE = p.p.DECISION_TYPE,
                                             .IS_ALLOW = p.p.IS_ALLOW,
                                             .IS_REPLACE_POS = p.p.IS_REPLACE_POS,
                                             .REVERSE_SENIORITY = p.p.REVERSE_SENIORITY})

            Dim ter = obj.FirstOrDefault

            'ter.lstDebts = (From p In Context.HU_DEBT
            '                From t In Context.HU_TERMINATE.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID)
            '                Select New DebtDTO With)

            'ter.lstHandoverContent = (From hc In Context.OT_OTHER_LIST
            '                          From p In Context.HU_TRANSFER_TERMINATE.Where(Function(f) f.CONTENT_ID = hc.ID And f.TERMINATE_ID = ter.ID).DefaultIfEmpty
            '                          Where hc.TYPE_ID = 2270
            '                          Order By hc.NAME_VN
            '                          Select New HandoverContentDTO With {.TERMINATE_ID = p.TERMINATE_ID,
            '                                                              .CONTENT_NAME = hc.NAME_VN,
            '                                                              .IS_FINISH = p.IS_FINISH}).ToList
            ter.lstHandoverContent = (From tr In Context.HU_TRANSFER_TERMINATE
                                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = tr.CONTENT_ID).DefaultIfEmpty
                                        Where tr.TERMINATE_ID = ter.ID
                                        Order By ot.NAME_VN
                                        Select New HandoverContentDTO With {.TERMINATE_ID = tr.TERMINATE_ID,
                                                                            .CONTENT_ID = tr.CONTENT_ID,
                                                                            .CONTENT_NAME = ot.NAME_VN,
                                                                            .IS_FINISH = tr.IS_FINISH}).ToList

            Return ter
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function GetEmployeeByID(ByVal gID As Decimal) As EmployeeDTO
        Dim obj As EmployeeDTO
        Try
            obj = (From p In Context.HU_EMPLOYEE
                   From staffrank In Context.HU_STAFF_RANK.Where(Function(f) p.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                   From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                   From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID)
                   From c In Context.HU_CONTRACT.Where(Function(f) p.CONTRACT_ID = f.ID).DefaultIfEmpty
                   From working In Context.HU_WORKING.Where(Function(f) f.ID = p.LAST_WORKING_ID).DefaultIfEmpty
                   Where p.ID = gID
                   Select New EmployeeDTO With {
                       .ID = p.ID,
                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                       .FULLNAME_VN = p.FULLNAME_VN,
                       .ORG_ID = p.ORG_ID,
                       .ORG_NAME = o.NAME_VN,
                       .ORG_CODE = o.CODE,
                       .TITLE_ID = t.ID,
                       .TITLE_NAME_VN = t.NAME_VN,
                       .JOIN_DATE = p.JOIN_DATE,
                       .JOIN_DATE_STATE = p.JOIN_DATE_STATE,
                       .CONTRACT_NO = c.CONTRACT_NO,
                       .CONTRACT_EFFECT_DATE = c.START_DATE,
                       .CONTRACT_EXPIRE_DATE = c.EXPIRE_DATE,
                       .STAFF_RANK_ID = p.STAFF_RANK_ID,
                       .STAFF_RANK_NAME = staffrank.NAME,
                       .WORK_STATUS = p.WORK_STATUS,
                       .TER_EFFECT_DATE = p.TER_EFFECT_DATE,
                       .LAST_WORKING_ID = p.LAST_WORKING_ID,
                       .SAL_BASIC = working.SAL_BASIC,
                       .COST_SUPPORT = working.COST_SUPPORT}).SingleOrDefault

            Return obj

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertTerminate(ByVal objTerminate As TerminateDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objTerminateData As New HU_TERMINATE
        Try
            ' thêm nghỉ việc
            objTerminateData.ID = Utilities.GetNextSequence(Context, Context.HU_TERMINATE.EntitySet.Name)
            objTerminateData.EMPLOYEE_ID = objTerminate.EMPLOYEE_ID
            objTerminateData.IS_NOHIRE = objTerminate.IS_NOHIRE
            objTerminateData.LAST_DATE = objTerminate.LAST_DATE
            objTerminateData.SEND_DATE = objTerminate.SEND_DATE
            objTerminateData.UPLOADFILE = objTerminate.UPLOADFILE
            objTerminateData.FILENAME = objTerminate.FILENAME
            objTerminateData.STATUS_ID = objTerminate.STATUS_ID
            objTerminateData.TER_REASON_DETAIL = objTerminate.TER_REASON_DETAIL
            objTerminateData.EMP_SENIORITY = objTerminate.EMPLOYEE_SENIORITY
            objTerminateData.REMARK = objTerminate.REMARK
            objTerminateData.CREATED_DATE = DateTime.Now
            objTerminateData.CREATED_BY = log.Username
            objTerminateData.CREATED_LOG = log.ComputerName
            objTerminateData.MODIFIED_DATE = DateTime.Now
            objTerminateData.MODIFIED_BY = log.Username
            objTerminateData.MODIFIED_LOG = log.ComputerName
            Dim empID = objTerminate.EMPLOYEE_ID
            Dim query As Decimal = (From p In Context.HU_WORKING Order By p.EFFECT_DATE Descending
                                    Where p.EMPLOYEE_ID = empID And p.STATUS_ID = ProfileCommon.OT_TER_STATUS.APPROVE_ID Select p.ID).FirstOrDefault
            If query <> 0 Then
                objTerminateData.LAST_WORKING_ID = query
            End If
            objTerminateData.APPROVAL_DATE = objTerminate.APPROVAL_DATE
            objTerminateData.IDENTIFI_CARD = objTerminate.IDENTIFI_CARD
            objTerminateData.IDENTIFI_RDATE = objTerminate.IDENTIFI_RDATE
            objTerminateData.IDENTIFI_STATUS = objTerminate.IDENTIFI_STATUS
            objTerminateData.IDENTIFI_MONEY = objTerminate.IDENTIFI_MONEY
            objTerminateData.SUN_CARD = objTerminate.SUN_CARD
            objTerminateData.SUN_RDATE = objTerminate.SUN_RDATE
            objTerminateData.SUN_STATUS = objTerminate.SUN_STATUS
            objTerminateData.SUN_MONEY = objTerminate.SUN_MONEY
            objTerminateData.INSURANCE_CARD = objTerminate.INSURANCE_CARD
            objTerminateData.INSURANCE_RDATE = objTerminate.INSURANCE_RDATE
            objTerminateData.INSURANCE_STATUS = objTerminate.INSURANCE_STATUS
            objTerminateData.INSURANCE_MONEY = objTerminate.INSURANCE_MONEY
            objTerminateData.REMAINING_LEAVE = objTerminate.REMAINING_LEAVE
            objTerminateData.PAYMENT_LEAVE = objTerminate.PAYMENT_LEAVE
            objTerminateData.AMOUNT_VIOLATIONS = objTerminate.AMOUNT_VIOLATIONS
            objTerminateData.AMOUNT_WRONGFUL = objTerminate.AMOUNT_WRONGFUL
            objTerminateData.ALLOWANCE_TERMINATE = objTerminate.ALLOWANCE_TERMINATE
            objTerminateData.TRAINING_COSTS = objTerminate.TRAINING_COSTS
            objTerminateData.OTHER_COMPENSATION = objTerminate.OTHER_COMPENSATION
            objTerminateData.COMPENSATORY_LEAVE = objTerminate.COMPENSATORY_LEAVE
            objTerminateData.COMPENSATORY_PAYMENT = objTerminate.COMPENSATORY_PAYMENT

            objTerminateData.EFFECT_DATE = objTerminate.EFFECT_DATE
            objTerminateData.DECISION_NO = objTerminate.DECISION_NO
            objTerminateData.SIGN_DATE = objTerminate.SIGN_DATE
            objTerminateData.SIGN_ID = objTerminate.SIGN_ID
            objTerminateData.SIGN_NAME = objTerminate.SIGN_NAME
            objTerminateData.SIGN_TITLE = objTerminate.SIGN_TITLE

            'bỔ XUNG THÊM TRƯỜNG
            objTerminateData.SALARYMEDIUM = objTerminate.SALARYMEDIUM
            objTerminateData.YEARFORALLOW = objTerminate.YEARFORALLOW
            objTerminateData.MONEYALLOW = objTerminate.MONEYALLOW
            objTerminateData.REMAINING_LEAVE_MONEY = objTerminate.REMAINING_LEAVE_MONEY
            objTerminateData.IDENTIFI_VIOLATE_MONEY = objTerminate.IDENTIFI_VIOLATE_MONEY
            objTerminateData.MONEY_RETURN = objTerminate.MONEY_RETURN
            objTerminateData.ORG_ID = objTerminate.ORG_ID
            objTerminateData.TITLE_ID = objTerminate.TITLE_ID
            objTerminateData.TYPE_TERMINATE = objTerminate.TYPE_TERMINATE
            'objTerminateData.Expire_Date = objTerminate.EXPIRE_DATE
            objTerminateData.SALARYMEDIUM_LOSS = objTerminate.SALARYMEDIUM_LOSS
            objTerminateData.TER_REASON = objTerminate.TER_REASON
            objTerminateData.DECISION_TYPE = objTerminate.DECISION_TYPE
            objTerminateData.SUM_COLLECT_DEBT = objTerminate.SUM_COLLECT_DEBT
            objTerminateData.AMOUNT_PAYMENT_CASH = objTerminate.AMOUNT_PAYMENT_CASH
            objTerminateData.AMOUNT_DEDUCT_FROM_SAL = objTerminate.AMOUNT_DEDUCT_FROM_SAL
            objTerminateData.PERIOD_ID = objTerminate.PERIOD_ID
            objTerminateData.IS_ALLOW = objTerminate.IS_ALLOW
            objTerminateData.IS_REPLACE_POS = objTerminate.IS_REPLACE_POS
            objTerminateData.REVERSE_SENIORITY = objTerminate.REVERSE_SENIORITY
            Context.HU_TERMINATE.AddObject(objTerminateData)

            If objTerminate.lstHandoverContent IsNot Nothing Then
                For Each item In objTerminate.lstHandoverContent
                    Dim handover As New HU_TRANSFER_TERMINATE
                    handover.ID = Utilities.GetNextSequence(Context, Context.HU_TRANSFER_TERMINATE.EntitySet.Name)
                    handover.TERMINATE_ID = objTerminate.ID
                    handover.IS_FINISH = item.IS_FINISH
                    handover.CONTENT_ID = item.CONTENT_ID
                    Context.HU_TRANSFER_TERMINATE.AddObject(handover)
                Next
            End If

            Context.SaveChanges(log)
            If objTerminate.STATUS_ID = ProfileCommon.OT_TER_STATUS.APPROVE_ID Then
                ApproveTerminate(objTerminate, log)
            Else
                ApproveTerminate_Customer(objTerminate, log)
            End If

            InsertOrUpdateDebtByTerminate(objTerminate, log)
            gID = objTerminateData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Sub InsertOrUpdateDebtByTerminate(ByVal terminate As TerminateDTO, ByVal log As UserLog)
        If terminate Is Nothing Then
            Exit Sub
        End If
        Dim existedDebts = (From debt In Context.HU_DEBT
                             Where debt.EMPLOYEE_ID = terminate.EMPLOYEE_ID
                             Select debt).ToList
        For Each debt As HU_DEBT In existedDebts
            Context.HU_DEBT.DeleteObject(debt)
        Next

        If terminate.lstDebts IsNot Nothing AndAlso terminate.lstDebts.Any Then
            For Each debt As DebtDTO In terminate.lstDebts
                Dim item As New HU_DEBT With
                {
                    .ID = Utilities.GetNextSequence(Context, Context.HU_DEBT.EntitySet.Name),
                    .DEBT_TYPE_ID = debt.DEBT_TYPE_ID,
                    .DEBT_STATUS = debt.DEBT_STATUS,
                    .MONEY = debt.MONEY,
                    .EMPLOYEE_ID = terminate.EMPLOYEE_ID,
                    .REMARK = debt.REMARK,
                    .CREATED_BY = log.Username,
                    .CREATED_DATE = DateTime.Now,
                    .CREATED_LOG = log.ComputerName,
                    .MODIFIED_DATE = DateTime.Now,
                    .MODIFIED_BY = log.Username,
                    .MODIFIED_LOG = log.ComputerName
                }
                Context.HU_DEBT.AddObject(item)
            Next
        End If
        Context.SaveChanges(log)
    End Sub

    Public Sub InsertOrUpdateAssetByTerminate(ByVal terminate As TerminateDTO, ByVal log As UserLog)
        If terminate Is Nothing Then
            Exit Sub
        End If
        Dim existedAsstes = (From asset_mng In Context.HU_ASSET_MNG
                             Where asset_mng.EMPLOYEE_ID = terminate.EMPLOYEE_ID
                             Select asset_mng).ToList
        For Each asset As HU_ASSET_MNG In existedAsstes
            Context.HU_ASSET_MNG.DeleteObject(asset)
        Next

        If terminate.AssetMngs IsNot Nothing AndAlso terminate.AssetMngs.Any Then
            For Each assetMngDto As AssetMngDTO In terminate.AssetMngs
                Dim item As New HU_ASSET_MNG With
                {
                    .ID = Utilities.GetNextSequence(Context, Context.HU_ASSET_MNG.EntitySet.Name),
                    .ASSET_DECLARE_ID = assetMngDto.ASSET_DECLARE_ID,
                    .ASSET_VALUE = assetMngDto.ASSET_VALUE,
                    .ASSET_STATUS_ID = assetMngDto.STATUS_ID,
                    .STATUS_ID = assetMngDto.STATUS_ID,
                    .EMPLOYEE_ID = terminate.EMPLOYEE_ID,
                    .QUANTITY = assetMngDto.QUANTITY
                }
                Context.HU_ASSET_MNG.AddObject(item)

            Next
        End If
        Context.SaveChanges(log)
    End Sub

    Public Function ModifyTerminate(ByVal objTerminate As TerminateDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTerminateData As New HU_TERMINATE With {.ID = objTerminate.ID}
        Try
            objTerminateData = (From p In Context.HU_TERMINATE Where p.ID = objTerminate.ID).FirstOrDefault
            ' sửa nghỉ việc
            objTerminateData.EMPLOYEE_ID = objTerminate.EMPLOYEE_ID
            objTerminateData.IS_NOHIRE = objTerminate.IS_NOHIRE
            objTerminateData.UPLOADFILE = objTerminate.UPLOADFILE
            objTerminateData.FILENAME = objTerminate.FILENAME
            objTerminateData.LAST_DATE = objTerminate.LAST_DATE
            objTerminateData.SEND_DATE = objTerminate.SEND_DATE
            objTerminateData.STATUS_ID = objTerminate.STATUS_ID
            objTerminateData.TER_REASON_DETAIL = objTerminate.TER_REASON_DETAIL
            objTerminateData.EMP_SENIORITY = objTerminate.EMPLOYEE_SENIORITY
            objTerminateData.REMARK = objTerminate.REMARK
            objTerminateData.MODIFIED_DATE = DateTime.Now
            objTerminateData.MODIFIED_BY = log.Username
            objTerminateData.MODIFIED_LOG = log.ComputerName

            objTerminateData.APPROVAL_DATE = objTerminate.APPROVAL_DATE
            objTerminateData.IDENTIFI_CARD = objTerminate.IDENTIFI_CARD
            objTerminateData.IDENTIFI_RDATE = objTerminate.IDENTIFI_RDATE
            objTerminateData.IDENTIFI_STATUS = objTerminate.IDENTIFI_STATUS
            objTerminateData.IDENTIFI_MONEY = objTerminate.IDENTIFI_MONEY
            objTerminateData.SUN_CARD = objTerminate.SUN_CARD
            objTerminateData.SUN_RDATE = objTerminate.SUN_RDATE
            objTerminateData.SUN_STATUS = objTerminate.SUN_STATUS
            objTerminateData.SUN_MONEY = objTerminate.SUN_MONEY
            objTerminateData.INSURANCE_CARD = objTerminate.INSURANCE_CARD
            objTerminateData.INSURANCE_RDATE = objTerminate.INSURANCE_RDATE
            objTerminateData.INSURANCE_STATUS = objTerminate.INSURANCE_STATUS
            objTerminateData.INSURANCE_MONEY = objTerminate.INSURANCE_MONEY
            objTerminateData.REMAINING_LEAVE = objTerminate.REMAINING_LEAVE
            objTerminateData.PAYMENT_LEAVE = objTerminate.PAYMENT_LEAVE
            objTerminateData.AMOUNT_VIOLATIONS = objTerminate.AMOUNT_VIOLATIONS
            objTerminateData.AMOUNT_WRONGFUL = objTerminate.AMOUNT_WRONGFUL
            objTerminateData.ALLOWANCE_TERMINATE = objTerminate.ALLOWANCE_TERMINATE
            objTerminateData.TRAINING_COSTS = objTerminate.TRAINING_COSTS
            objTerminateData.OTHER_COMPENSATION = objTerminate.OTHER_COMPENSATION
            objTerminateData.COMPENSATORY_LEAVE = objTerminate.COMPENSATORY_LEAVE
            objTerminateData.COMPENSATORY_PAYMENT = objTerminate.COMPENSATORY_PAYMENT
            objTerminateData.EFFECT_DATE = objTerminate.EFFECT_DATE
            objTerminateData.DECISION_NO = objTerminate.DECISION_NO
            objTerminateData.SIGN_DATE = objTerminate.SIGN_DATE
            objTerminateData.SIGN_ID = objTerminate.SIGN_ID
            objTerminateData.SIGN_NAME = objTerminate.SIGN_NAME
            objTerminateData.SIGN_TITLE = objTerminate.SIGN_TITLE

            'bỔ XUNG THÊM TRƯỜNG
            objTerminateData.SALARYMEDIUM = objTerminate.SALARYMEDIUM
            objTerminateData.YEARFORALLOW = objTerminate.YEARFORALLOW
            objTerminateData.MONEYALLOW = objTerminate.MONEYALLOW
            objTerminateData.REMAINING_LEAVE_MONEY = objTerminate.REMAINING_LEAVE_MONEY
            objTerminateData.IDENTIFI_VIOLATE_MONEY = objTerminate.IDENTIFI_VIOLATE_MONEY
            objTerminateData.MONEY_RETURN = objTerminate.MONEY_RETURN
            objTerminateData.ORG_ID = objTerminate.ORG_ID
            objTerminateData.TITLE_ID = objTerminate.TITLE_ID
            objTerminateData.TYPE_TERMINATE = objTerminate.TYPE_TERMINATE
            objTerminateData.SALARYMEDIUM_LOSS = objTerminate.SALARYMEDIUM_LOSS
            'objTerminateData.Expire_Date = objTerminate.EXPIRE_DATE
            objTerminateData.TER_REASON = objTerminate.TER_REASON
            objTerminateData.DECISION_TYPE = objTerminate.DECISION_TYPE
            objTerminateData.SUM_COLLECT_DEBT = objTerminate.SUM_COLLECT_DEBT
            objTerminateData.AMOUNT_PAYMENT_CASH = objTerminate.AMOUNT_PAYMENT_CASH
            objTerminateData.AMOUNT_DEDUCT_FROM_SAL = objTerminate.AMOUNT_DEDUCT_FROM_SAL
            objTerminateData.PERIOD_ID = objTerminate.PERIOD_ID
            objTerminateData.IS_ALLOW = objTerminate.IS_ALLOW
            objTerminateData.IS_REPLACE_POS = objTerminate.IS_REPLACE_POS
            objTerminateData.REVERSE_SENIORITY = objTerminate.REVERSE_SENIORITY
            Dim lstHandover = (From p In Context.HU_TRANSFER_TERMINATE Where p.TERMINATE_ID = objTerminate.ID).ToList
            For Each item In lstHandover
                Context.HU_TRANSFER_TERMINATE.DeleteObject(item)
            Next

            If objTerminate.lstHandoverContent IsNot Nothing Then
                For Each item In objTerminate.lstHandoverContent
                    Dim handover As New HU_TRANSFER_TERMINATE
                    handover.ID = Utilities.GetNextSequence(Context, Context.HU_TRANSFER_TERMINATE.EntitySet.Name)
                    handover.TERMINATE_ID = objTerminate.ID
                    handover.IS_FINISH = item.IS_FINISH
                    handover.CONTENT_ID = item.CONTENT_ID
                    Context.HU_TRANSFER_TERMINATE.AddObject(handover)
                Next
            End If

            Context.SaveChanges(log)
            If objTerminate.STATUS_ID = ProfileCommon.OT_TER_STATUS.APPROVE_ID Then
                ApproveTerminate(objTerminate, log)
                'AutoGenInsChangeByTerminate(objTerminate.EMPLOYEE_ID,
                '                            objTerminate.ORG_ID,
                '                            objTerminate.TITLE_ID,
                '                            objTerminate.EFFECT_DATE,
                '                            0,
                '                            log)
            End If

            gID = objTerminateData.ID
            InsertOrUpdateDebtByTerminate(objTerminate, log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteTerminate(ByVal objID As Decimal, log As UserLog) As Boolean
        Dim objTerminateData As HU_TERMINATE
        Try
            objTerminateData = (From p In Context.HU_TERMINATE Where objID = p.ID).FirstOrDefault
            Context.HU_TERMINATE.DeleteObject(objTerminateData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteBlackList(ByVal objID As Decimal, log As UserLog) As Boolean
        Dim objTerminateData As HU_TERMINATE
        Try
            objTerminateData = (From p In Context.HU_TERMINATE Where objID = p.ID).FirstOrDefault
            objTerminateData.IS_NOHIRE = False
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ApproveTerminate(ByVal objTerminate As TerminateDTO, ByVal log As UserLog) As Boolean
        Dim objEmployeeData As HU_EMPLOYEE
        Try
            objEmployeeData = (From p In Context.HU_EMPLOYEE Where objTerminate.EMPLOYEE_ID = p.ID).FirstOrDefault
            If objTerminate.EFFECT_DATE <= DateTime.Now Then
                objEmployeeData.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            Else
                objEmployeeData.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.WAIT_TERMINATE_ID
            End If
            objEmployeeData.TER_EFFECT_DATE = objTerminate.EFFECT_DATE
            objEmployeeData.TER_LAST_DATE = objTerminate.LAST_DATE
            If log IsNot Nothing Then
                Context.SaveChanges(log)
            Else
                Context.SaveChanges()
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ApproveTerminate_Customer(ByVal objTerminate As TerminateDTO, ByVal log As UserLog) As Boolean
        Dim objEmployeeData As HU_EMPLOYEE
        Try
            objEmployeeData = (From p In Context.HU_EMPLOYEE Where objTerminate.EMPLOYEE_ID = p.ID).FirstOrDefault
            objEmployeeData.EMP_STATUS = ProfileCommon.OT_WORK_STATUS.EMP_STATUS
            If objTerminate.EFFECT_DATE <= DateTime.Now Then
                objEmployeeData.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            Else
                objEmployeeData.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.WAIT_TERMINATE_ID
            End If
            If log IsNot Nothing Then
                Context.SaveChanges(log)
            Else
                Context.SaveChanges()
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Private Function AutoGenInsChangeByTerminate(employeeID As Decimal,
                                                 orgID As Decimal,
                                                 titleID As Decimal,
                                                 effectDate As Date,
                                                 id As Decimal,
                                               ByVal log As UserLog) As Boolean


        Try
            '1. Check khai báo đóng mới - INS_INFOMATION
            If (From p In Context.INS_INFORMATION Where p.EMPLOYEE_ID = employeeID).Count = 0 Then
                Return False
            End If
            '2. Lấy biến động gần nhất theo ngày hiệu lực truyền vào
            Dim insChangeBefore = (From p In Context.INS_CHANGE
                                   Where p.EMPLOYEE_ID = employeeID And
                                   p.EFFECTDATE <= effectDate
                                   Order By p.EFFECTDATE Descending).FirstOrDefault

            '3. So sánh lương gần nhất với tổng lương truyền vào. lớn hơn thì biến động tăng, nhỏ hơn thì biến động giảm
            ' Đẩy data vào bảng biến động INS_CHANGE
            If insChangeBefore IsNot Nothing Then
                If insChangeBefore.NEWSALARY IsNot Nothing Then
                    Dim insChangeNew As New INS_CHANGE
                    insChangeNew.ID = Utilities.GetNextSequence(Context, Context.INS_CHANGE.EntitySet.Name)
                    insChangeNew.EMPLOYEE_ID = employeeID
                    insChangeNew.ORG_ID = orgID
                    insChangeNew.TITLE_ID = titleID
                    insChangeNew.CHANGE_TYPE = 2
                    insChangeNew.OLDSALARY = insChangeBefore.NEWSALARY
                    insChangeNew.NEWSALARY = 0
                    insChangeNew.EFFECTDATE = effectDate
                    insChangeNew.CREATED_BY = log.Username
                    insChangeNew.CREATED_DATE = DateTime.Now
                    insChangeNew.CREATED_LOG = log.Ip & "-" & log.ComputerName
                    insChangeNew.MODIFIED_BY = log.Username
                    insChangeNew.MODIFIED_DATE = DateTime.Now
                    insChangeNew.MODIFIED_LOG = log.Ip & "-" & log.ComputerName
                    If id <> 0 Then
                        insChangeNew.TER_PKEY = id
                    End If
                    If effectDate.Day >= 15 Then
                        insChangeNew.CHANGE_MONTH = effectDate.AddMonths(1)
                    Else
                        insChangeNew.CHANGE_MONTH = effectDate
                    End If
                    insChangeNew.NOTE = "HiStaff tự sinh biến động chấm dứt hợp đồng lao động từ Nghỉ việc"
                    Context.INS_CHANGE.AddObject(insChangeNew)
                    Context.SaveChanges()
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CheckTerminateNo(ByVal objTerminate As TerminateDTO) As Boolean
        Try
            Dim query = (From p In Context.HU_TERMINATE
                         Where p.DECISION_NO = objTerminate.DECISION_NO And
                         p.ID <> objTerminate.ID).FirstOrDefault
            Return (query Is Nothing)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetDataPrintBBBR(ByVal id As Decimal) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PROFILE_BUSINESS.GET_TERMINATE_PRINT_BBBG",
                                           New With {.P_ID = id,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR}, False)

                dsData.Tables(1).TableName = "DATA"
                dsData.Tables(2).TableName = "DATA1"

                Return dsData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetDataPrintBBBR3B(ByVal id As Decimal) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PROFILE_BUSINESS.GET_TERMINATE_PRINT_BBBG3B",
                                           New With {.P_ID = id,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR}, False)

                dsData.Tables(1).TableName = "DATA"
                dsData.Tables(2).TableName = "DATA1"

                Return dsData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

#Region "Insurance"
    Public Function GetTyleNV() As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.GETTILEDONG",
                                           New With {.P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetSalaryNew(ByRef P_EMPLOYEEID As Integer) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.GETLUONGBIENDONG",
                                           New With {.P_EMPLOYEE_ID = P_EMPLOYEEID,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function
#End Region

#Region "Terminate3b"

    Public Function GetTerminate3b(ByVal _filter As Terminate3BDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of Terminate3BDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.HU_TERMINATE_3B
                        From e In Context.HU_EMPLOYEE.Where(Function(f) p.EMPLOYEE_ID = f.ID)
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) p.EMPLOYEE_ID = f.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) e.ORG_ID = f.ID)
                        From t In Context.HU_TITLE.Where(Function(f) e.TITLE_ID = f.ID)
                        From status In Context.OT_OTHER_LIST.Where(Function(f) p.STATUS_ID = f.ID)
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)

            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.EMPLOYEE_ID IsNot Nothing Then
                query = query.Where(Function(p) p.p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.STATUS_ID <> 0 Then
                query = query.Where(Function(p) p.p.STATUS_ID = _filter.STATUS_ID)
            End If
            If _filter.ID_NO IsNot Nothing Then
                query = query.Where(Function(p) p.cv.ID_NO.ToUpper.Contains(_filter.ID_NO.ToUpper))
            End If
            If _filter.EFFECT_FROM IsNot Nothing Then
                query = query.Where(Function(p) p.p.EFFECT_DATE >= _filter.EFFECT_FROM)
            End If
            If _filter.EFFECT_TO IsNot Nothing Then
                query = query.Where(Function(p) p.p.EFFECT_DATE <= _filter.EFFECT_FROM)
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            Dim Terminate3b = query.Select(Function(p) New Terminate3BDTO With {
                                             .ID = p.p.ID,
                                             .STATUS_NAME = p.status.NAME_VN,
                                             .STATUS_CODE = p.status.CODE,
                                             .STATUS_ID = p.status.ID,
                                             .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                             .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                             .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                             .JOIN_DATE = p.e.JOIN_DATE,
                                             .EFFECT_DATE = p.p.EFFECT_DATE,
                                             .ORG_NAME = p.o.NAME_VN,
                                             .ORG_DESC = p.o.DESCRIPTION_PATH,
                                             .TITLE_NAME = p.t.NAME_VN,
                                            .IDENTIFI_CARD = p.p.IDENTIFI_CARD,
                                            .IDENTIFI_RDATE = p.p.IDENTIFI_RDATE,
                                            .IDENTIFI_STATUS = p.p.IDENTIFI_STATUS,
                                             .IDENTIFI_MONEY = p.p.IDENTIFI_MONEY,
                                            .SUN_CARD = p.p.SUN_CARD,
                                            .SUN_RDATE = p.p.SUN_RDATE,
                                            .SUN_STATUS = p.p.SUN_STATUS,
                                            .SUN_MONEY = p.p.SUN_MONEY,
                                            .INSURANCE_CARD = p.p.INSURANCE_CARD,
                                            .INSURANCE_RDATE = p.p.INSURANCE_RDATE,
                                            .INSURANCE_STATUS = p.p.INSURANCE_STATUS,
                                            .INSURANCE_MONEY = p.p.INSURANCE_MONEY,
                                            .REMAINING_LEAVE = p.p.REMAINING_LEAVE,
                                             .COMPENSATORY_LEAVE = p.p.COMPENSATORY_LEAVE,
                                             .IS_REMAINING_LEAVE = p.p.IS_REMAINING_LEAVE,
                                             .IS_COMPENSATORY_LEAVE = p.p.IS_COMPENSATORY_LEAVE,
                                             .CREATED_DATE = p.p.CREATED_DATE,
                                             .ID_NO = p.cv.ID_NO})

            Terminate3b = Terminate3b.OrderBy(Sorts)
            Total = Terminate3b.Count
            Terminate3b = Terminate3b.Skip(PageIndex * PageSize).Take(PageSize)
            Return Terminate3b.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function GetTerminate3bByID(ByVal _filter As Terminate3BDTO) As Terminate3BDTO

        Try
            Dim query = From p In Context.HU_TERMINATE_3B
                        Join e In Context.HU_EMPLOYEE On p.EMPLOYEE_ID Equals e.ID
                        Join o In Context.HU_ORGANIZATION On e.ORG_ID Equals o.ID
                        Join t In Context.HU_TITLE On e.TITLE_ID Equals t.ID

            query = query.Where(Function(p) _filter.ID = p.p.ID)

            Dim obj = query.Select(Function(p) New Terminate3BDTO With {
                                       .ID = p.p.ID,
                                       .STATUS_ID = p.p.STATUS_ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .JOIN_DATE = p.e.JOIN_DATE,
                                       .ORG_ID = p.o.ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.NAME_VN,
                                       .TITLE_ID = p.t.ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .EMPLOYEE_SENIORITY = p.p.EMP_SENIORITY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .IDENTIFI_CARD = p.p.IDENTIFI_CARD,
                                       .IDENTIFI_RDATE = p.p.IDENTIFI_RDATE,
                                       .IDENTIFI_STATUS = p.p.IDENTIFI_STATUS,
                                       .IDENTIFI_MONEY = p.p.IDENTIFI_MONEY,
                                       .SUN_CARD = p.p.SUN_CARD,
                                       .SUN_RDATE = p.p.SUN_RDATE,
                                       .SUN_STATUS = p.p.SUN_STATUS,
                                       .SUN_MONEY = p.p.SUN_MONEY,
                                       .INSURANCE_CARD = p.p.INSURANCE_CARD,
                                       .INSURANCE_RDATE = p.p.INSURANCE_RDATE,
                                       .INSURANCE_STATUS = p.p.INSURANCE_STATUS,
                                       .INSURANCE_MONEY = p.p.INSURANCE_MONEY,
                                       .REMAINING_LEAVE = p.p.REMAINING_LEAVE,
                                       .COMPENSATORY_LEAVE = p.p.COMPENSATORY_LEAVE,
                                       .IS_COMPENSATORY_LEAVE = p.p.IS_COMPENSATORY_LEAVE,
                                       .IS_REMAINING_LEAVE = p.p.IS_REMAINING_LEAVE,
                                       .EFFECT_DATE = p.p.EFFECT_DATE,
                                       .SIGN_ID = p.p.SIGN_ID,
                                       .SIGN_DATE = p.p.SIGN_DATE,
                                       .SIGN_NAME = p.p.SIGN_NAME,
                                       .SIGN_TITLE = p.p.SIGN_TITLE,
                                      .WORK_STATUS = p.e.WORK_STATUS})

            Return obj.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function GetTerminate3bEmployeeByID(ByVal gID As Decimal) As EmployeeDTO
        Dim obj As EmployeeDTO
        Try
            obj = (From p In Context.HU_EMPLOYEE
                   From staffrank In Context.HU_STAFF_RANK.Where(Function(f) p.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                   From c In Context.HU_CONTRACT.Where(Function(f) p.CONTRACT_ID = f.ID).DefaultIfEmpty
                   From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                   From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID)
                   From working In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = p.ID And f.IS_3B = True)
                   Where p.ID = gID
                   Select New EmployeeDTO With {
                       .ID = p.ID,
                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                       .FULLNAME_VN = p.FULLNAME_VN,
                       .ORG_ID = p.ORG_ID,
                       .ORG_NAME = o.NAME_VN,
                       .ORG_CODE = o.CODE,
                       .TITLE_ID = t.ID,
                       .TITLE_NAME_VN = t.NAME_VN,
                       .JOIN_DATE = p.JOIN_DATE,
                       .JOIN_DATE_STATE = p.JOIN_DATE_STATE,
                       .CONTRACT_NO = c.CONTRACT_NO,
                       .CONTRACT_EFFECT_DATE = c.START_DATE,
                       .CONTRACT_EXPIRE_DATE = c.EXPIRE_DATE,
                       .STAFF_RANK_ID = p.STAFF_RANK_ID,
                       .STAFF_RANK_NAME = staffrank.NAME,
                       .WORK_STATUS = p.WORK_STATUS,
                       .TER_EFFECT_DATE = p.TER_EFFECT_DATE,
                       .LAST_WORKING_ID = p.LAST_WORKING_ID,
                       .SAL_BASIC = working.SAL_BASIC,
                       .COST_SUPPORT = working.COST_SUPPORT,
                       .EFFECT_DATE = working.EFFECT_DATE}).FirstOrDefault

            Return obj

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertTerminate3b(ByVal objTerminate3b As Terminate3BDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objTerminate3bData As New HU_TERMINATE_3B
        Try
            ' thêm nghỉ việc
            objTerminate3bData.ID = Utilities.GetNextSequence(Context, Context.HU_TERMINATE_3B.EntitySet.Name)
            objTerminate3bData.EMPLOYEE_ID = objTerminate3b.EMPLOYEE_ID
            objTerminate3bData.STATUS_ID = objTerminate3b.STATUS_ID
            objTerminate3bData.EMP_SENIORITY = objTerminate3b.EMPLOYEE_SENIORITY
            Dim empID = objTerminate3b.EMPLOYEE_ID
            Dim query As Decimal = (From p In Context.HU_WORKING
                                    Order By p.EFFECT_DATE Descending
                                    Where p.EMPLOYEE_ID = empID And p.STATUS_ID = 447 Select p.ID).FirstOrDefault
            If query <> 0 Then
                objTerminate3bData.LAST_WORKING_ID = query
            End If
            objTerminate3bData.IDENTIFI_CARD = objTerminate3b.IDENTIFI_CARD
            objTerminate3bData.IDENTIFI_RDATE = objTerminate3b.IDENTIFI_RDATE
            objTerminate3bData.IDENTIFI_STATUS = objTerminate3b.IDENTIFI_STATUS
            objTerminate3bData.IDENTIFI_MONEY = objTerminate3b.IDENTIFI_MONEY
            objTerminate3bData.SUN_CARD = objTerminate3b.SUN_CARD
            objTerminate3bData.SUN_RDATE = objTerminate3b.SUN_RDATE
            objTerminate3bData.SUN_STATUS = objTerminate3b.SUN_STATUS
            objTerminate3bData.SUN_MONEY = objTerminate3b.SUN_MONEY
            objTerminate3bData.INSURANCE_CARD = objTerminate3b.INSURANCE_CARD
            objTerminate3bData.INSURANCE_RDATE = objTerminate3b.INSURANCE_RDATE
            objTerminate3bData.INSURANCE_STATUS = objTerminate3b.INSURANCE_STATUS
            objTerminate3bData.INSURANCE_MONEY = objTerminate3b.INSURANCE_MONEY
            objTerminate3bData.REMAINING_LEAVE = objTerminate3b.REMAINING_LEAVE
            objTerminate3bData.COMPENSATORY_LEAVE = objTerminate3b.COMPENSATORY_LEAVE
            objTerminate3bData.IS_REMAINING_LEAVE = objTerminate3b.IS_REMAINING_LEAVE
            objTerminate3bData.IS_COMPENSATORY_LEAVE = objTerminate3b.IS_COMPENSATORY_LEAVE

            objTerminate3bData.EFFECT_DATE = objTerminate3b.EFFECT_DATE
            objTerminate3bData.LAST_DATE = objTerminate3b.EFFECT_DATE.Value.AddDays(-1)
            objTerminate3bData.SIGN_DATE = objTerminate3b.SIGN_DATE
            objTerminate3bData.SIGN_ID = objTerminate3b.SIGN_ID
            objTerminate3bData.SIGN_NAME = objTerminate3b.SIGN_NAME
            objTerminate3bData.SIGN_TITLE = objTerminate3b.SIGN_TITLE
            Context.HU_TERMINATE_3B.AddObject(objTerminate3bData)
            Context.SaveChanges(log)
            If objTerminate3b.STATUS_ID = 262 Then
                ApproveTerminate3b(objTerminate3b)
                AutoGenInsChangeByTerminate(objTerminate3b.EMPLOYEE_ID,
                                            objTerminate3b.ORG_ID,
                                            objTerminate3b.TITLE_ID,
                                            objTerminate3b.EFFECT_DATE,
                                            objTerminate3bData.ID,
                                            log)
            End If

            gID = objTerminate3bData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function CheckExistApproveTerminate3b(ByVal gID As Decimal) As Boolean
        Try
            Dim idTerminate3b = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            Dim objEmployeeData = (From p In Context.HU_EMPLOYEE Where gID = p.ID).FirstOrDefault
            If idTerminate3b = objEmployeeData.WORK_STATUS Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyTerminate3b(ByVal objTerminate3b As Terminate3BDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTerminate3bData As New HU_TERMINATE_3B With {.ID = objTerminate3b.ID}
        Try
            objTerminate3bData = (From p In Context.HU_TERMINATE_3B Where p.ID = objTerminate3b.ID).FirstOrDefault
            ' sửa nghỉ việc
            objTerminate3bData.EMPLOYEE_ID = objTerminate3b.EMPLOYEE_ID
            objTerminate3bData.STATUS_ID = objTerminate3b.STATUS_ID
            objTerminate3bData.EMP_SENIORITY = objTerminate3b.EMPLOYEE_SENIORITY

            objTerminate3bData.IDENTIFI_CARD = objTerminate3b.IDENTIFI_CARD
            objTerminate3bData.IDENTIFI_RDATE = objTerminate3b.IDENTIFI_RDATE
            objTerminate3bData.IDENTIFI_STATUS = objTerminate3b.IDENTIFI_STATUS
            objTerminate3bData.IDENTIFI_MONEY = objTerminate3b.IDENTIFI_MONEY
            objTerminate3bData.SUN_CARD = objTerminate3b.SUN_CARD
            objTerminate3bData.SUN_RDATE = objTerminate3b.SUN_RDATE
            objTerminate3bData.SUN_STATUS = objTerminate3b.SUN_STATUS
            objTerminate3bData.SUN_MONEY = objTerminate3b.SUN_MONEY
            objTerminate3bData.INSURANCE_CARD = objTerminate3b.INSURANCE_CARD
            objTerminate3bData.INSURANCE_RDATE = objTerminate3b.INSURANCE_RDATE
            objTerminate3bData.INSURANCE_STATUS = objTerminate3b.INSURANCE_STATUS
            objTerminate3bData.INSURANCE_MONEY = objTerminate3b.INSURANCE_MONEY
            objTerminate3bData.REMAINING_LEAVE = objTerminate3b.REMAINING_LEAVE
            objTerminate3bData.COMPENSATORY_LEAVE = objTerminate3b.COMPENSATORY_LEAVE
            objTerminate3bData.IS_REMAINING_LEAVE = objTerminate3b.IS_REMAINING_LEAVE
            objTerminate3bData.IS_COMPENSATORY_LEAVE = objTerminate3b.IS_COMPENSATORY_LEAVE
            objTerminate3bData.EFFECT_DATE = objTerminate3b.EFFECT_DATE
            objTerminate3bData.LAST_DATE = objTerminate3b.EFFECT_DATE.Value.AddDays(-1)
            objTerminate3bData.SIGN_DATE = objTerminate3b.SIGN_DATE
            objTerminate3bData.SIGN_ID = objTerminate3b.SIGN_ID
            objTerminate3bData.SIGN_NAME = objTerminate3b.SIGN_NAME
            objTerminate3bData.SIGN_TITLE = objTerminate3b.SIGN_TITLE
            Context.SaveChanges(log)
            If objTerminate3b.STATUS_ID = 262 Then
                ApproveTerminate3b(objTerminate3b)
                AutoGenInsChangeByTerminate(objTerminate3b.EMPLOYEE_ID,
                                            objTerminate3b.ORG_ID,
                                            objTerminate3b.TITLE_ID,
                                            objTerminate3b.EFFECT_DATE,
                                            objTerminate3b.ID,
                                            log)
            End If

            gID = objTerminate3bData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteTerminate3b(ByVal objID As Decimal) As Boolean
        Dim objTerminate3bData As HU_TERMINATE_3B
        Try
            objTerminate3bData = (From p In Context.HU_TERMINATE_3B Where objID = p.ID).FirstOrDefault
            Context.HU_TERMINATE_3B.DeleteObject(objTerminate3bData)
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ApproveTerminate3b(ByVal objTerminate3b As Terminate3BDTO) As Boolean
        Dim objEmployeeData As HU_EMPLOYEE
        Try
            objEmployeeData = (From p In Context.HU_EMPLOYEE Where objTerminate3b.EMPLOYEE_ID = p.ID).SingleOrDefault

            objEmployeeData.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            objEmployeeData.TER_EFFECT_DATE = objTerminate3b.EFFECT_DATE.Value.AddDays(-1)
            objEmployeeData.TER_LAST_DATE = objTerminate3b.EFFECT_DATE.Value.AddDays(-1)

            If objTerminate3b.IS_COMPENSATORY_LEAVE Then
                Dim objEmp3b = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_3B_ID = objEmployeeData.ID).FirstOrDefault
                If objEmp3b IsNot Nothing Then
                    Dim obj As New AT_DECLARE_ENTITLEMENT
                    obj.ID = Utilities.GetNextSequence(Context, Context.AT_DECLARE_ENTITLEMENT.EntitySet.Name)
                    obj.EMPLOYEE_ID = objEmp3b.ID
                    obj.ADJUST_NB = objTerminate3b.COMPENSATORY_LEAVE
                    obj.REMARK_NB = "Hệ thống chuyển nghỉ bù khi điều chuyển 3 bên"
                    obj.START_MONTH_NB = objTerminate3b.EFFECT_DATE.Value.Month
                    obj.YEAR_NB = objTerminate3b.EFFECT_DATE.Value.Year
                    obj.YEAR_ENTITLEMENT = objTerminate3b.EFFECT_DATE.Value.Year
                    obj.YEAR = objTerminate3b.EFFECT_DATE.Value.Year
                    obj.CREATED_BY = "ADMIN"
                    obj.CREATED_DATE = Date.Now
                    obj.CREATED_LOG = "ADMIN"
                    obj.MODIFIED_BY = "ADMIN"
                    obj.MODIFIED_DATE = Date.Now
                    obj.MODIFIED_LOG = "ADMIN"
                    Context.AT_DECLARE_ENTITLEMENT.AddObject(obj)

                End If
            End If
            If objTerminate3b.IS_REMAINING_LEAVE Then
                Dim objEmp3b = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_3B_ID = objEmployeeData.ID).FirstOrDefault
                If objEmp3b IsNot Nothing Then
                    Dim obj As New AT_DECLARE_ENTITLEMENT
                    obj.ID = Utilities.GetNextSequence(Context, Context.AT_DECLARE_ENTITLEMENT.EntitySet.Name)
                    obj.EMPLOYEE_ID = objEmp3b.ID
                    obj.ADJUST_ENTITLEMENT = objTerminate3b.REMAINING_LEAVE
                    obj.REMARK_ENTITLEMENT = "Hệ thống chuyển nghỉ phép khi điều chuyển 3 bên"
                    obj.ADJUST_MONTH_ENTITLEMENT = objTerminate3b.EFFECT_DATE.Value.Month
                    obj.YEAR_NB = objTerminate3b.EFFECT_DATE.Value.Year
                    obj.YEAR_ENTITLEMENT = objTerminate3b.EFFECT_DATE.Value.Year
                    obj.YEAR = objTerminate3b.EFFECT_DATE.Value.Year
                    obj.CREATED_BY = "ADMIN"
                    obj.CREATED_DATE = Date.Now
                    obj.CREATED_LOG = "ADMIN"
                    obj.MODIFIED_BY = "ADMIN"
                    obj.MODIFIED_DATE = Date.Now
                    obj.MODIFIED_LOG = "ADMIN"
                    Context.AT_DECLARE_ENTITLEMENT.AddObject(obj)

                End If
            End If
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

End Class

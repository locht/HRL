Imports Framework.UI
Imports Payroll.PayrollBusiness

Partial Public Class PayrollRepository
#Region "GetAllowance List"

    Public Function GetAllowanceList(ByVal _filter As AllowanceListDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceListDTO)
        Dim lstAllowance As List(Of AllowanceListDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstAllowance = rep.GetAllowanceList(_filter, Sorts)
                Return lstAllowance
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function



#End Region

#Region "Alowance"
    Public Function GetAllowance(ByVal _filter As AllowanceDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceDTO)
        Dim lstAllowance As List(Of AllowanceDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstAllowance = rep.GetAllowance(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstAllowance
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetAllowance(ByVal _filter As AllowanceDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceDTO)
        Dim lstAllowance As List(Of AllowanceDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstAllowance = rep.GetAllowance(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstAllowance
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAllowance(ByVal objAllowance As AllowanceDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertAllowance(objAllowance, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAllowance(ByVal objAllowance As AllowanceDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyAllowance(objAllowance, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAllowance(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveAllowance(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAllowance(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteAllowance(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Taxation List"

    Public Function GetTaxation(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS ASC") As List(Of PATaxationDTO)
        Dim lstTaxation As List(Of PATaxationDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstTaxation = rep.GetTaxation(PageIndex, PageSize, Total, Sorts)
                Return lstTaxation
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertTaxation(ByVal objTaxation As PATaxationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertTaxation(objTaxation, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyTaxation(ByVal objTaxation As PATaxationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyTaxation(objTaxation, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveTaxation(ByVal lstTaxation As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveTaxation(lstTaxation, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteTaxation(ByVal lstTaxation As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteTaxation(lstTaxation)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Payment List"

    Public Function GetPaymentList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO)
        Dim lstPaymentList As List(Of PAPaymentListDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstPaymentList = rep.GetPaymentList(PageIndex, PageSize, Total, Sorts)
                Return lstPaymentList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertPaymentList(ByVal objPaymentList As PAPaymentListDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPaymentList(objPaymentList, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPaymentList(ByVal objPaymentList As PAPaymentListDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyPaymentList(objPaymentList, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActivePaymentList(ByVal lstPaymentList As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActivePaymentList(lstPaymentList, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeletePaymentList(ByVal lstPaymentList As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeletePaymentList(lstPaymentList)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Object Salary"

    Public Function GetObjectSalary(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO)
        Dim lstObjectSalary As List(Of PAObjectSalaryDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstObjectSalary = rep.GetObjectSalary(PageIndex, PageSize, Total, Sorts)
                Return lstObjectSalary
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertObjectSalary(objObjectSalary, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateObjectSalary(objObjectSalary)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyObjectSalary(objObjectSalary, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveObjectSalary(ByVal lstObjectSalary As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveObjectSalary(lstObjectSalary, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteObjectSalary(ByVal lstObjectSalary As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteObjectSalary(lstObjectSalary)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "List Salary"

    Public Function GetAllFomulerGroup(ByVal _filter As PAFomulerGroup,
                                   Optional ByVal Sorts As String = "IDX ASC") As List(Of PAFomulerGroup)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetAllFomulerGroup(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetAllFomulerGroup(ByVal _filter As PAFomulerGroup,
                            ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "IDX ASC") As List(Of PAFomulerGroup)
        Dim lstPAFomulerGroup As List(Of PAFomulerGroup)
        Using rep As New PayrollBusinessClient

            Try
                lstPAFomulerGroup = rep.GetAllFomulerGroup(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstPAFomulerGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertFomulerGroup(objPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyFomulerGroup(objPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function DeleteFomulerGroup(ByVal lstDelete As PAFomulerGroup) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteFomulerGroup(lstDelete)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetListAllSalary(ByVal gID As Decimal) As List(Of PAFomuler)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetListAllSalary(gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetListInputColumn(ByVal typePaymentId As Decimal) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetListInputColumn(typePaymentId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetListSalColunm(ByVal typePaymentId As Decimal) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetListSalColunm(typePaymentId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetObjectSalaryColumn(ByVal typePaymentId As Decimal) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetObjectSalaryColumn(typePaymentId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetListCalculation() As List(Of OT_OTHERLIST_DTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetListCalculation()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function CopyFomuler(ByRef F_ID As Decimal, ByRef T_ID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CopyFomuler(F_ID, Me.Log, T_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function SaveFomuler(ByVal objData As PAFomuler, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.SaveFomuler(objData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function CheckFomuler(ByVal sCol As String, ByVal sFormuler As String, ByVal objID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CheckFomuler(sCol, sFormuler, objID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ActiveFolmulerGroup(ByVal lstID As Decimal, ByVal bActive As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveFolmulerGroup(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Period"

    Public Function GetPeriodList(ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "START_DATE desc") As List(Of ATPeriodDTO)
        Dim lstPeriod As List(Of ATPeriodDTO)
        Using rep As New PayrollBusinessClient
            Try
                lstPeriod = rep.GetPeriodList(PageIndex, PageSize, Total, Sorts)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetPeriodbyYear(ByVal year As Decimal) As List(Of ATPeriodDTO)
        Dim lstPeriod As List(Of ATPeriodDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstPeriod = rep.GetPeriodbyYear(year)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetSalaryTypebyIncentive(ByVal incentive As Decimal) As List(Of SalaryTypeDTO)
        Dim lstPeriod As List(Of SalaryTypeDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstPeriod = rep.GetSalaryTypebyIncentive(incentive)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetPaymentSourcesbyYear(ByVal year As Decimal) As List(Of PaymentSourcesDTO)
        Dim lstPeriod As List(Of PaymentSourcesDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstPeriod = rep.GetPaymentSourcesbyYear(year)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetListOrgBonus() As List(Of OrgBonusDTO)
        Dim lstPeriod As List(Of OrgBonusDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstPeriod = rep.GetListOrgBonus()
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPeriod(objPeriod, objOrgPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateATPeriod(ByVal objPeriod As ATPeriodDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateATPeriod(objPeriod)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateATPeriodDay(ByVal objPeriod As ATPeriodDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateATPeriodDay(objPeriod)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyPeriod(objPeriod, objOrgPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeletePeriod(ByVal lstPeriod As ATPeriodDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeletePeriod(lstPeriod)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ActivePeriod(ByVal lstWorkFactor As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActivePeriod(lstWorkFactor, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Salaries List"

    Public Function GetListSalaries(ByVal _filter As PAListSalariesDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "COL_INDEX ASC, CREATED_DATE desc") As List(Of PAListSalariesDTO)
        Dim lstListSalaries As List(Of PAListSalariesDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstListSalaries = rep.GetListSalaries(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstListSalaries
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertListSalaries(ByVal objListSalaries As PAListSalariesDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertListSalaries(objListSalaries, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function InsertPA_SAL_MAPPING(ByVal objListSalaries As PA_SALARY_FUND_MAPPINGDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPA_SAL_MAPPING(objListSalaries, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyListSalaries(ByVal objListSalaries As PAListSalariesDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyListSalaries(objListSalaries, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveListSalaries(ByVal lstListSalaries As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveListSalaries(lstListSalaries, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteListSalariesStatus(ByVal lstListSalaries As List(Of Decimal), ByVal sActive As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteListSalariesStatus(lstListSalaries, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteListSalaries(ByVal lstListSalaries As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteListSalaries(lstListSalaries)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetListSal(ByVal _filter As PAListSalDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "COL_INDEX ASC, CREATED_DATE desc") As List(Of PAListSalDTO)
        Dim lstListSal As List(Of PAListSalDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstListSal = rep.GetListSal(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstListSal
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertListSal(ByVal objListSal As PAListSalDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertListSal(objListSal, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyListSal(ByVal objListSal As PAListSalDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyListSal(objListSal, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveListSal(ByVal lstListSal As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveListSal(lstListSal, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteListSal(ByVal lstListSal As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteListSal(lstListSal)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region
#Region "Work Standard"

    Public Function GetWorkStandard(ByVal _filter As Work_StandardDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " YEAR, PERIOD_ID desc") As List(Of Work_StandardDTO)
        Dim lstListSalaries As List(Of Work_StandardDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstListSalaries = rep.GetWorkStandard(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lstListSalaries
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetWorkStandardByYear(ByVal year As Decimal) As List(Of Work_StandardDTO)
        Dim lstListSalaries As List(Of Work_StandardDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstListSalaries = rep.GetWorkStandardbyYear(year)
                Return lstListSalaries
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertWorkStandard(ByVal objWorkStandard As Work_StandardDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertWorkStandard(objWorkStandard, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyWorkStandard(ByVal objWorkStandard As Work_StandardDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyWorkStandard(objWorkStandard, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveWorkStandard(ByVal lstWorkStandard As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveWorkStandard(lstWorkStandard, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateWorkStandard(ByVal lstWorkStandard As Work_StandardDTO) As Boolean
        'Try
        '    Return PayrollRepositoryStatic.Instance.ValidateWorkStandard(objPeriod)
        'Catch ex As Exception
        '    Throw ex
        'End Try
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateWorkStandard(lstWorkStandard)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteWorkStandard(ByVal lstWorkStandard As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteWorkStandard(lstWorkStandard)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function IsCompanyLevel(ByVal org_id As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.IsCompanyLevel(org_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "lunch list : Đơn giá tiền ăn trưa"

    Public Function GetPriceLunchList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EFFECT_DATE desc") As List(Of ATPriceLunchDTO)
        Dim lstPeriod As List(Of ATPriceLunchDTO)
        Using rep As New PayrollBusinessClient
            Try
                lstPeriod = rep.GetPriceLunchList(PageIndex, PageSize, Total, Sorts)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetPriceLunch(ByVal year As Decimal) As List(Of ATPriceLunchDTO)
        Dim lstPeriod As List(Of ATPriceLunchDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstPeriod = rep.GetPriceLunch(year)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPriceLunch(objPeriod, objOrgPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateATPriceLunch(ByVal _validate As ATPriceLunchDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateATPriceLunch(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateATPriceLunchOrg(ByVal _validate As ATPriceLunchDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateATPriceLunchOrg(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyPriceLunch(objPeriod, objOrgPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeletePriceLunch(ByVal lstPeriod As ATPriceLunchDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeletePriceLunch(lstPeriod)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "CostCenter"

    Public Function GetCostCenter(ByVal _filter As CostCenterDTO,
                                ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of CostCenterDTO)
        Dim lstCostCenter As List(Of CostCenterDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstCostCenter = rep.GetCostCenter(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCostCenter
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetCostCenter(ByVal _filter As CostCenterDTO,
                                       Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of CostCenterDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetCostCenter(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCostCenter(ByVal objCostCenter As CostCenterDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertCostCenter(objCostCenter, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCostCenter(ByVal objCostCenter As CostCenterDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyCostCenter(objCostCenter, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCostCenter(ByVal objCostCenter As CostCenterDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateCostCenter(objCostCenter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveCostCenter(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveCostCenter(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCostCenterStatus(ByVal lstID As List(Of Decimal), ByVal bActive As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteCostCenterStatus(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCostCenter(ByVal lstCostCenter As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteCostCenter(lstCostCenter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Org Bonus"
    Public Function GetOrgBonus(ByVal _filter As OrgBonusDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS desc") As List(Of OrgBonusDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetOrgBonus(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function InsertOrgBonus(ByVal lstOrgBonus As OrgBonusDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertOrgBonus(lstOrgBonus, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyOrgBonus(ByVal lstOrgBonus As OrgBonusDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyOrgBonus(lstOrgBonus, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ActiveOrgBonus(ByVal lstOrgBonus As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveOrgBonus(lstOrgBonus, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteOrgBonus(ByVal lstOrgBonus As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteOrgBonus(lstOrgBonus)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ValidateOrgBonus(ByVal objOrgBonus As OrgBonusDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateOrgBonus(objOrgBonus)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
#Region "Payment Sources"
    Public Function GetPaymentSources(ByVal _filter As PaymentSourcesDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS asc") As List(Of PaymentSourcesDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPaymentSources(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function InsertPaymentSources(ByVal lstPaymentSources As PaymentSourcesDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPaymentSources(lstPaymentSources, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyPaymentSources(ByVal lstPaymentSources As PaymentSourcesDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyPaymentSources(lstPaymentSources, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ActivePaymentSources(ByVal lstPaymentSources As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActivePaymentSources(lstPaymentSources, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeletePaymentSources(ByVal lstPaymentSources As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeletePaymentSources(lstPaymentSources)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Work Factor"
    Public Function GetWorkFactor(ByVal _filter As WorkFactorDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " EFFECT_DATE desc") As List(Of WorkFactorDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetWorkFactor(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function InsertWorkFactor(ByVal lstWorkFactor As WorkFactorDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertWorkFactor(lstWorkFactor, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyWorkFactor(ByVal lstWorkFactor As WorkFactorDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyWorkFactor(lstWorkFactor, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ActiveWorkFactor(ByVal lstWorkFactor As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveWorkFactor(lstWorkFactor, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteWorkFactor(ByVal lstWorkFactor As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteWorkFactor(lstWorkFactor)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ValidateWorkFactor(ByVal objWorkFactor As WorkFactorDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateWorkFactor(objWorkFactor)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
#Region "SalaryFund List"

    Public Function GetSalaryFundByID(ByVal _filter As PASalaryFundDTO) As PASalaryFundDTO
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryFundByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateSalaryFund(ByVal objSalaryFund As PASalaryFundDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.UpdateSalaryFund(objSalaryFund, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "TitleCost List"

    Public Function GetTitleCost(ByVal _filter As PATitleCostDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PATitleCostDTO)
        Dim lstTitleCost As List(Of PATitleCostDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstTitleCost = rep.GetTitleCost(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstTitleCost
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTitleCost(ByVal _filter As PATitleCostDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PATitleCostDTO)
        Dim lstTitleCost As List(Of PATitleCostDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstTitleCost = rep.GetTitleCost(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstTitleCost
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertTitleCost(ByVal objTitleCost As PATitleCostDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertTitleCost(objTitleCost, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyTitleCost(ByVal objTitleCost As PATitleCostDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyTitleCost(objTitleCost, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteTitleCost(ByVal lstTitleCost As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteTitleCost(lstTitleCost)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Get List Manning"

    Public Function LoadComboboxListMannName(ByVal org_id As Integer, ByVal year As Integer) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.LoadComboboxListMannName(org_id, year)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region

#Region "Validate Combobox"

    Public Function ValidateCombobox(ByVal cbxData As ComboBoxDataDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateCombobox(cbxData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region
#Region "SALE COMMISION"
    Public Function GetSaleCommision(ByVal _filter As SaleCommisionDTO,
                                ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SaleCommisionDTO)
        Dim lstSalaryType As List(Of SaleCommisionDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstSalaryType = rep.GetSaleCommision(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstSalaryType
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSaleCommision(ByVal _filter As SaleCommisionDTO,
                                      Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SaleCommisionDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSaleCommision(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertSaleCommision(ByVal objSaleCommision As SaleCommisionDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSaleCommision(objSaleCommision, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifySaleCommision(ByVal objSaleCommision As SaleCommisionDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySaleCommision(objSaleCommision, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteSaleCommision(ByVal lstSaleCommision As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSaleCommision(lstSaleCommision)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveSaleCommision(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveSaleCommision(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

End Class

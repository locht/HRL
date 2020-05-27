Imports Framework.Data
Imports System.Data.Objects.DataClasses
Imports System.Data.Common
Imports System.Data.Entity
Imports System.Threading
Imports Framework.Data.System.Linq.Dynamic
Imports Framework.Data.SystemConfig
Imports System.Configuration
Imports System.Reflection

Partial Class PerformanceRepository

#Region "Criteria"

    Public Function GetCriteria(ByVal _filter As CriteriaDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaDTO)

        Try
            Dim query = From p In Context.PE_CRITERIA
                        Select New CriteriaDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .REMARK = p.REMARK,
                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function InsertCriteria(ByVal objCriteria As CriteriaDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCriteriaData As New PE_CRITERIA
        Try
            objCriteriaData.ID = Utilities.GetNextSequence(Context, Context.PE_CRITERIA.EntitySet.Name)
            objCriteriaData.CODE = objCriteria.CODE
            objCriteriaData.NAME = objCriteria.NAME
            objCriteriaData.REMARK = objCriteria.REMARK
            objCriteriaData.ACTFLG = objCriteria.ACTFLG
            Context.PE_CRITERIA.AddObject(objCriteriaData)
            Context.SaveChanges(log)
            gID = objCriteriaData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ValidateCriteria(ByVal _validate As CriteriaDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PE_CRITERIA
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PE_CRITERIA
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ModifyCriteria(ByVal objCriteria As CriteriaDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCriteriaData As New PE_CRITERIA With {.ID = objCriteria.ID}
        Try
            objCriteriaData = (From p In Context.PE_CRITERIA Where p.ID = objCriteria.ID).FirstOrDefault
            objCriteriaData.CODE = objCriteria.CODE
            objCriteriaData.NAME = objCriteria.NAME
            objCriteriaData.REMARK = objCriteria.REMARK
            Context.SaveChanges(log)
            gID = objCriteriaData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function ActiveCriteria(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PE_CRITERIA)
        Try
            lstData = (From p In Context.PE_CRITERIA Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function DeleteCriteria(ByVal lstCriteria() As Decimal) As Boolean
        Dim lstCriteriaData As List(Of PE_CRITERIA)
        Try
            lstCriteriaData = (From p In Context.PE_CRITERIA Where lstCriteria.Contains(p.ID)).ToList
            For index = 0 To lstCriteriaData.Count - 1
                Context.PE_CRITERIA.DeleteObject(lstCriteriaData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

#End Region
#Region "Classification"

    Public Function GetClassification(ByVal _filter As ClassificationDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationDTO)

        Try
            Dim query = From p In Context.PE_CLASSIFICATION
                        Select New ClassificationDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .VALUE_FROM = p.VALUE_FROM,
                            .VALUE_TO = p.VALUE_TO,
                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.VALUE_FROM <> Nothing Then
                lst = lst.Where(Function(p) p.VALUE_FROM.ToString().Contains(_filter.VALUE_FROM.ToString()))
            End If
            If _filter.VALUE_TO <> Nothing Then
                lst = lst.Where(Function(p) p.VALUE_TO.ToString().Contains(_filter.VALUE_TO.ToString()))
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function InsertClassification(ByVal objClassification As ClassificationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objClassificationData As New PE_CLASSIFICATION
        Try
            objClassificationData.ID = Utilities.GetNextSequence(Context, Context.PE_CLASSIFICATION.EntitySet.Name)
            objClassificationData.CODE = objClassification.CODE
            objClassificationData.NAME = objClassification.NAME
            objClassificationData.VALUE_FROM = objClassification.VALUE_FROM
            objClassificationData.VALUE_TO = objClassification.VALUE_TO
            objClassificationData.ACTFLG = objClassification.ACTFLG
            Context.PE_CLASSIFICATION.AddObject(objClassificationData)
            Context.SaveChanges(log)
            gID = objClassificationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ValidateClassification(ByVal _validate As ClassificationDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PE_CLASSIFICATION
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PE_CLASSIFICATION
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ModifyClassification(ByVal objClassification As ClassificationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objClassificationData As New PE_CLASSIFICATION With {.ID = objClassification.ID}
        Try
            objClassificationData = (From p In Context.PE_CLASSIFICATION Where p.ID = objClassification.ID).FirstOrDefault
            objClassificationData.CODE = objClassification.CODE
            objClassificationData.NAME = objClassification.NAME
            objClassificationData.VALUE_FROM = objClassification.VALUE_FROM
            objClassificationData.VALUE_TO = objClassification.VALUE_TO
            Context.SaveChanges(log)
            gID = objClassificationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function ActiveClassification(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PE_CLASSIFICATION)
        Try
            lstData = (From p In Context.PE_CLASSIFICATION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function DeleteClassification(ByVal lstClassification() As Decimal) As Boolean
        Dim lstClassificationData As List(Of PE_CLASSIFICATION)
        Try
            lstClassificationData = (From p In Context.PE_CLASSIFICATION Where lstClassification.Contains(p.ID)).ToList
            For index = 0 To lstClassificationData.Count - 1
                Context.PE_CLASSIFICATION.DeleteObject(lstClassificationData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

#End Region
#Region "ObjectGroup"

    Public Function GetObjectGroup(ByVal _filter As ObjectGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ObjectGroupDTO)

        Try
            Dim query = From p In Context.PE_OBJECT_GROUP
                        Select New ObjectGroupDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .REMARK = p.REMARK,
                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function InsertObjectGroup(ByVal objObjectGroup As ObjectGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objObjectGroupData As New PE_OBJECT_GROUP
        Try
            objObjectGroupData.ID = Utilities.GetNextSequence(Context, Context.PE_OBJECT_GROUP.EntitySet.Name)
            objObjectGroupData.CODE = objObjectGroup.CODE
            objObjectGroupData.NAME = objObjectGroup.NAME
            objObjectGroupData.REMARK = objObjectGroup.REMARK
            objObjectGroupData.ACTFLG = objObjectGroup.ACTFLG
            Context.PE_OBJECT_GROUP.AddObject(objObjectGroupData)
            Context.SaveChanges(log)
            gID = objObjectGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ValidateObjectGroup(ByVal _validate As ObjectGroupDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PE_OBJECT_GROUP
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PE_OBJECT_GROUP
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ModifyObjectGroup(ByVal objObjectGroup As ObjectGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objObjectGroupData As New PE_OBJECT_GROUP With {.ID = objObjectGroup.ID}
        Try
            objObjectGroupData = (From p In Context.PE_OBJECT_GROUP Where p.ID = objObjectGroup.ID).FirstOrDefault
            objObjectGroupData.CODE = objObjectGroup.CODE
            objObjectGroupData.NAME = objObjectGroup.NAME
            objObjectGroupData.REMARK = objObjectGroup.REMARK
            Context.SaveChanges(log)
            gID = objObjectGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function ActiveObjectGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PE_OBJECT_GROUP)
        Try
            lstData = (From p In Context.PE_OBJECT_GROUP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function DeleteObjectGroup(ByVal lstObjectGroup() As Decimal) As Boolean
        Dim lstObjectGroupData As List(Of PE_OBJECT_GROUP)
        Try
            lstObjectGroupData = (From p In Context.PE_OBJECT_GROUP Where lstObjectGroup.Contains(p.ID)).ToList
            For index = 0 To lstObjectGroupData.Count - 1
                Context.PE_OBJECT_GROUP.DeleteObject(lstObjectGroupData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

#End Region

#Region "Period"

    Public Function GetPeriod(ByVal _filter As PeriodDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO)

        Try
            Dim query = (From p In Context.PE_PERIOD
                    From s In Context.OT_OTHER_LIST.Where(Function(s) s.ID = p.TYPE_ASS).DefaultIfEmpty
                        Select New PeriodDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .TYPE_ASS_NAME = s.NAME_VN,
                            .TYPE_ASS = p.TYPE_ASS,
                            .REMARK = p.REMARK,
                            .START_DATE = p.START_DATE,
                            .END_DATE = p.END_DATE,
                            .YEAR = p.YEAR,
                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE})
            Dim lst = query
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
                End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
                End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
                End If
            If _filter.START_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.START_DATE = _filter.START_DATE)
                End If
            If _filter.END_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.END_DATE = _filter.END_DATE)
                End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            If _filter.TYPE_ASS_NAME IsNot Nothing Then
                lst = lst.Where(Function(p) p.TYPE_ASS_NAME = _filter.TYPE_ASS_NAME)
            End If
            If _filter.TYPE_ASS IsNot Nothing Then
                lst = lst.Where(Function(p) p.TYPE_ASS = _filter.TYPE_ASS)
            End If
            If _filter.YEAR IsNot Nothing Then
                lst = lst.Where(Function(p) p.YEAR = _filter.YEAR)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function
    Public Function GetPeriodById(ByVal _filter As PeriodDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO)
        Try
            Dim query = (From s In Context.PE_PERIOD
                        From p In Context.PE_EMPLOYEE_ASSESSMENT.Where(Function(f) f.PERIOD_ID = s.ID And f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
                    Where s.ID = _filter.ID
                            Select New PeriodDTO With {
                            .ID = p.ID,
                            .CODE = s.CODE,
                            .NAME = s.NAME,
                            .TYPE_ASS = s.TYPE_ASS,
                            .REMARK = s.REMARK,
                            .START_DATE = s.START_DATE,
                            .END_DATE = s.END_DATE,
                            .YEAR = s.YEAR,
                            .OBJECT_GROUP_ID = p.OBJECT_GROUP_ID,
                            .PE_STATUS = p.PE_STATUS,
                            .CREATED_DATE = s.CREATED_DATE})
            Dim lst = query
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function
    Public Function InsertPeriod(ByVal objPeriod As PeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPeriodData As New PE_PERIOD
        Try
            objPeriodData.ID = Utilities.GetNextSequence(Context, Context.PE_PERIOD.EntitySet.Name)
            objPeriodData.YEAR = objPeriod.YEAR
            objPeriodData.CODE = objPeriod.CODE
            objPeriodData.NAME = objPeriod.NAME
            objPeriodData.REMARK = objPeriod.REMARK
            objPeriodData.START_DATE = objPeriod.START_DATE
            objPeriodData.END_DATE = objPeriod.END_DATE
            objPeriodData.ACTFLG = objPeriod.ACTFLG
            objPeriodData.TYPE_ASS = objPeriod.TYPE_ASS
            Context.PE_PERIOD.AddObject(objPeriodData)
            Context.SaveChanges(log)
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ValidatePeriod(ByVal _validate As PeriodDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PE_PERIOD
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PE_PERIOD
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ModifyPeriod(ByVal objPeriod As PeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPeriodData As New PE_PERIOD With {.ID = objPeriod.ID}
        Try
            objPeriodData = (From p In Context.PE_PERIOD Where p.ID = objPeriod.ID).FirstOrDefault
            objPeriodData.YEAR = objPeriod.YEAR
            objPeriodData.CODE = objPeriod.CODE
            objPeriodData.NAME = objPeriod.NAME
            objPeriodData.REMARK = objPeriod.REMARK
            objPeriodData.START_DATE = objPeriod.START_DATE
            objPeriodData.END_DATE = objPeriod.END_DATE
            objPeriodData.TYPE_ASS = objPeriod.TYPE_ASS
            Context.SaveChanges(log)
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function ActivePeriod(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PE_PERIOD)
        Try
            lstData = (From p In Context.PE_PERIOD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeletePeriod(ByVal lstPeriod() As Decimal) As Boolean
        Dim lstPeriodData As List(Of PE_PERIOD)
        Try
            lstPeriodData = (From p In Context.PE_PERIOD Where lstPeriod.Contains(p.ID)).ToList
            For index = 0 To lstPeriodData.Count - 1
                Context.PE_PERIOD.DeleteObject(lstPeriodData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

#End Region

#Region "Import đánh giá ABC"
    Public Function GET_LIST_YEAR() As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_LIST.GET_LIST_YEAR",
                                                         New With {.P_CUR = cls.OUT_CURSOR})
                Return dsdata
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "PERFORMANCE")
            Throw ex
        End Try
    End Function

    Public Function GET_PERIOD_BY_YEAR(ByVal P_YEAR As Integer) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_LIST.GET_PERIOD_BY_YEAR",
                                                         New With {.P_YEAR = P_YEAR,
                                                                    .P_CUR = cls.OUT_CURSOR})
                Return dsdata
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "PERFORMANCE")
            Throw ex
        End Try
    End Function

    Public Function GET_DATE_BY_PERIOD(ByVal P_PERIOD_ID As Integer) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_LIST.GET_DATE_BY_PERIOD",
                                                         New With {.P_PERIOD_ID = P_PERIOD_ID,
                                                                    .P_CUR = cls.OUT_CURSOR})
                Return dsdata
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "PERFORMANCE")
            Throw ex
        End Try
    End Function

    Public Function GetPeEvaluatePeriod(ByVal _filter As PE_EVALUATE_PERIODDTO,
                                         ByVal _param As ParamDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PE_EVALUATE_PERIODDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = (From p In Context.PE_EVALUATE_PERIOD
                         From e In Context.HU_EMPLOYEE.Where(Function(s) s.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                         From pe In Context.PE_PERIOD.Where(Function(s) s.ID = p.PERIOD_ID).DefaultIfEmpty
                         From o In Context.HU_ORGANIZATION.Where(Function(s) s.ID = p.ORG_ID).DefaultIfEmpty
                         From t In Context.HU_TITLE.Where(Function(s) s.ID = p.TITLE_ID).DefaultIfEmpty
                         From lv In Context.PA_SALARY_LEVEL.Where(Function(s) s.ID = p.SAL_LEVEL_ID).DefaultIfEmpty
                         From c In Context.OT_OTHER_LIST.Where(Function(s) s.ID = p.CLASSIFICATION).DefaultIfEmpty
                         Where p.PERIOD_ID = _filter.PERIOD_ID
                        Select New PE_EVALUATE_PERIODDTO With {
                            .ID = p.ID,
                            .EMPLOYEE_ID = p.EMPLOYEE_ID,
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .FULLNAME = e.FULLNAME_VN,
                            .ORG_NAME = o.NAME_VN,
                            .TITLE_NAME = t.NAME_VN,
                            .SALARY_LEVEL = lv.NAME,
                            .JOIN_DATE = e.JOIN_DATE,
                            .RESPONSIBILITY = p.RESPONSIBILITY,
                            .COOPERATION = p.COOPERATION,
                            .CLASSIFICATION_NAME = c.NAME_VN,
                            .DILIGENCE = p.DILIGENCE,
                            .TOTAL = p.TOTAL,
                            .NOTE = p.NOTE,
                            .COMMENT1 = p.COMMENT1,
                            .PERIOD_ID = p.PERIOD_ID,
                            .CREATED_DATE = p.CREATED_DATE
                           })
            Dim lst = query

            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.FULLNAME <> "" Then
                lst = lst.Where(Function(p) p.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper) = _filter.TITLE_NAME)
            End If
            If _filter.SALARY_LEVEL IsNot Nothing Then
                lst = lst.Where(Function(p) p.SALARY_LEVEL.ToUpper.Contains(_filter.SALARY_LEVEL.ToUpper) = _filter.SALARY_LEVEL)
            End If
            If _filter.JOIN_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE = _filter.JOIN_DATE)
            End If
            If _filter.RESPONSIBILITY IsNot Nothing Then
                lst = lst.Where(Function(p) p.RESPONSIBILITY = _filter.RESPONSIBILITY)
            End If
            If _filter.COOPERATION IsNot Nothing Then
                lst = lst.Where(Function(p) p.COOPERATION = _filter.COOPERATION)
            End If
            If _filter.CLASSIFICATION_NAME IsNot Nothing Then
                lst = lst.Where(Function(p) p.CLASSIFICATION_NAME.ToUpper.Contains(_filter.CLASSIFICATION_NAME.ToUpper))
            End If
            If _filter.DILIGENCE IsNot Nothing Then
                lst = lst.Where(Function(p) p.DILIGENCE = _filter.DILIGENCE)
            End If
            If _filter.TOTAL IsNot Nothing Then
                lst = lst.Where(Function(p) p.TOTAL = _filter.TOTAL)
            End If
            If _filter.NOTE IsNot Nothing Then
                lst = lst.Where(Function(p) p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            If _filter.COMMENT1 IsNot Nothing Then
                lst = lst.Where(Function(p) p.COMMENT1.ToUpper.Contains(_filter.COMMENT1.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function EXPORT_EVALUATE_ABC(ByVal P_PERIOD_ID As Integer) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataSet = cls.ExecuteStore("PKG_PERFORMANCE_LIST.EXPORT_EVALUATE_ABC",
                                                         New With {.P_PERIOD_ID = P_PERIOD_ID,
                                                                    .P_CUR = cls.OUT_CURSOR,
                                                                    .P_CUR1 = cls.OUT_CURSOR,
                                                                    .P_CUR2 = cls.OUT_CURSOR}, False)
                Return dsdata
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "PERFORMANCE")
            Throw ex
        End Try
    End Function

    Public Function INPORT_EVALUATE_ABC(ByVal P_DOCXML As String, ByVal P_PERIOD_ID As Decimal, ByVal P_USER As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PERFORMANCE_LIST.IMPORT_EVALUATE_ABC",
                                 New With {.P_DOCXML = P_DOCXML, .P_PERIOD_ID = P_PERIOD_ID, .P_USER = P_USER})
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
#End Region

End Class

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

#Region "ObjectGroupPeriod"

    Public Function GetObjectGroupNotByPeriodID(ByVal _filter As ObjectGroupPeriodDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "OBJECT_GROUP_CODE desc") As List(Of ObjectGroupPeriodDTO)

        Try
            Dim query As IQueryable(Of ObjectGroupPeriodDTO)
            query = From p In Context.PE_OBJECT_GROUP
                        Where Not (From can In Context.PE_OBJECT_GROUP_PERIOD
                                 Where can.PERIOD_ID = _filter.PERIOD_ID
                                 Select can.OBJECT_GROUP_ID).Contains(p.ID) _
                             And p.ACTFLG = "A"
                             Select New ObjectGroupPeriodDTO With {
                                 .OBJECT_GROUP_ID = p.ID,
                                 .OBJECT_GROUP_CODE = p.CODE,
                                 .OBJECT_GROUP_NAME = p.NAME}

            Dim lst = query
            If _filter.OBJECT_GROUP_CODE <> "" Then
                lst = lst.Where(Function(f) f.OBJECT_GROUP_CODE.ToUpper.Contains(_filter.OBJECT_GROUP_CODE.ToUpper))
            End If
            If _filter.OBJECT_GROUP_NAME <> "" Then
                lst = lst.Where(Function(f) f.OBJECT_GROUP_NAME.ToUpper.Contains(_filter.OBJECT_GROUP_NAME.ToUpper))
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

    Public Function GetObjectGroupByPeriodID(ByVal _filter As ObjectGroupPeriodDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "OBJECT_GROUP_CODE desc") As  _
                                         List(Of ObjectGroupPeriodDTO)

        Try
            Dim query = From p In Context.PE_OBJECT_GROUP_PERIOD
                        From obj In Context.PE_OBJECT_GROUP.Where(Function(f) f.ID = p.OBJECT_GROUP_ID).DefaultIfEmpty
                        Where p.PERIOD_ID = _filter.PERIOD_ID
                        Select New ObjectGroupPeriodDTO With {.ID = p.ID,
                                                              .PERIOD_ID = p.PERIOD_ID,
                                                              .OBJECT_GROUP_ID = p.OBJECT_GROUP_ID,
                                                              .OBJECT_GROUP_CODE = obj.CODE,
                                                              .OBJECT_GROUP_NAME = obj.NAME,
                                                              .ACTFLG = If(obj.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}

            Dim lst = query
            If _filter.OBJECT_GROUP_CODE <> "" Then
                lst = lst.Where(Function(f) f.OBJECT_GROUP_CODE.ToUpper.Contains(_filter.OBJECT_GROUP_CODE.ToUpper))
            End If
            If _filter.OBJECT_GROUP_NAME <> "" Then
                lst = lst.Where(Function(f) f.OBJECT_GROUP_NAME.ToUpper.Contains(_filter.OBJECT_GROUP_NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(f) f.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
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

    Public Function InsertObjectGroupByPeriod(ByVal lst As List(Of ObjectGroupPeriodDTO),
                                              ByVal log As UserLog) As Boolean
        Dim iCount As Integer = 0
        Dim objData As PE_OBJECT_GROUP_PERIOD
        Try
            For Each obj In lst
                Dim isExist = (From p In Context.PE_OBJECT_GROUP_PERIOD
                               Where p.PERIOD_ID = obj.PERIOD_ID _
                               And p.OBJECT_GROUP_ID = obj.OBJECT_GROUP_ID).Any
                If Not isExist Then
                    objData = New PE_OBJECT_GROUP_PERIOD
                    objData.ID = Utilities.GetNextSequence(Context, Context.PE_OBJECT_GROUP_PERIOD.EntitySet.Name)
                    objData.PERIOD_ID = obj.PERIOD_ID
                    objData.OBJECT_GROUP_ID = obj.OBJECT_GROUP_ID
                    Context.PE_OBJECT_GROUP_PERIOD.AddObject(objData)
                End If
            Next

            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function DeleteObjectGroupByPeriod(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean
        Try
            Dim lstData = (From p In Context.PE_OBJECT_GROUP_PERIOD Where lst.Contains(p.ID)).ToList
            For Each objData In lstData
                Context.PE_OBJECT_GROUP_PERIOD.DeleteObject(objData)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex

        End Try
    End Function

#End Region

#Region "CriteriaObjectGroup"

    Public Function GetCriteriaNotByObjectGroupID(ByVal _filter As CriteriaObjectGroupDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "CRITERIA_CODE desc") As List(Of CriteriaObjectGroupDTO)

        Try
            Dim query As IQueryable(Of CriteriaObjectGroupDTO)
            query = From p In Context.PE_CRITERIA
                        Where Not (From can In Context.PE_CRITERIA_OBJECT_GROUP
                                 Where can.OBJECT_GROUP_ID = _filter.OBJECT_GROUP_ID
                                 Select can.CRITERIA_ID).Contains(p.ID) _
                             And p.ACTFLG = "A"
                             Select New CriteriaObjectGroupDTO With {
                                 .CRITERIA_ID = p.ID,
                                 .CRITERIA_CODE = p.CODE,
                                 .CRITERIA_NAME = p.NAME}

            Dim lst = query
            If _filter.CRITERIA_CODE <> "" Then
                lst = lst.Where(Function(f) f.CRITERIA_CODE.ToUpper.Contains(_filter.CRITERIA_CODE.ToUpper))
            End If
            If _filter.CRITERIA_NAME <> "" Then
                lst = lst.Where(Function(f) f.CRITERIA_NAME.ToUpper.Contains(_filter.CRITERIA_NAME.ToUpper))
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

    Public Function GetCriteriaByObjectGroupID(ByVal _filter As CriteriaObjectGroupDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CRITERIA_CODE desc") As  _
                                            List(Of CriteriaObjectGroupDTO)

        Try
            Dim query = From p In Context.PE_CRITERIA_OBJECT_GROUP
                        From obj In Context.PE_CRITERIA.Where(Function(f) f.ID = p.CRITERIA_ID).DefaultIfEmpty
                        Where p.OBJECT_GROUP_ID = _filter.OBJECT_GROUP_ID
                        Select New CriteriaObjectGroupDTO With {.ID = p.ID,
                                                              .OBJECT_GROUP_ID = p.OBJECT_GROUP_ID,
                                                              .CRITERIA_ID = p.CRITERIA_ID,
                                                              .CRITERIA_CODE = obj.CODE,
                                                              .CRITERIA_NAME = obj.NAME,
                                                              .ACTFLG = If(obj.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                                .AMONG = p.AMONG,
                                                                .EXPENSE = p.EXPENSE,
                                                                .FROM_DATE = p.FROM_DATE,
                                                                .TO_DATE = p.TO_DATE}

            Dim lst = query
            If _filter.CRITERIA_CODE <> "" Then
                lst = lst.Where(Function(f) f.CRITERIA_CODE.ToUpper.Contains(_filter.CRITERIA_CODE.ToUpper))
            End If
            If _filter.CRITERIA_NAME <> "" Then
                lst = lst.Where(Function(f) f.CRITERIA_NAME.ToUpper.Contains(_filter.CRITERIA_NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(f) f.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
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

    Public Function InsertCriteriaByObjectGroup(ByVal lst As List(Of CriteriaObjectGroupDTO),
                                              ByVal log As UserLog) As Boolean
        Dim iCount As Integer = 0
        Dim objData As PE_CRITERIA_OBJECT_GROUP
        Try
            For Each obj In lst
                objData = (From p In Context.PE_CRITERIA_OBJECT_GROUP
                               Where p.OBJECT_GROUP_ID = obj.OBJECT_GROUP_ID _
                               And p.CRITERIA_ID = obj.CRITERIA_ID).FirstOrDefault
                If objData Is Nothing Then
                    objData = New PE_CRITERIA_OBJECT_GROUP
                    objData.ID = Utilities.GetNextSequence(Context, Context.PE_CRITERIA_OBJECT_GROUP.EntitySet.Name)
                    objData.CRITERIA_ID = obj.CRITERIA_ID
                    objData.OBJECT_GROUP_ID = obj.OBJECT_GROUP_ID
                    objData.EXPENSE = obj.EXPENSE
                    objData.AMONG = obj.AMONG
                    objData.FROM_DATE = obj.FROM_DATE
                    objData.TO_DATE = obj.TO_DATE
                    Context.PE_CRITERIA_OBJECT_GROUP.AddObject(objData)
                Else
                    Context.PE_CRITERIA_OBJECT_GROUP.Attach(objData)
                    objData.CRITERIA_ID = obj.CRITERIA_ID
                    objData.OBJECT_GROUP_ID = obj.OBJECT_GROUP_ID
                    objData.EXPENSE = obj.EXPENSE
                    objData.AMONG = obj.AMONG
                    objData.FROM_DATE = obj.FROM_DATE
                    objData.TO_DATE = obj.TO_DATE
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function DeleteCriteriaByObjectGroup(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean
        Try
            Dim lstData = (From p In Context.PE_CRITERIA_OBJECT_GROUP Where lst.Contains(p.ID)).ToList
            For Each objData In lstData
                Context.PE_CRITERIA_OBJECT_GROUP.DeleteObject(objData)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex

        End Try
    End Function


    Public Function UpdateCriteriaObjectGroup(ByVal lst As List(Of CriteriaObjectGroupDTO),
                               ByVal log As UserLog) As Boolean
        Try
            For Each obj In lst
                Dim objData = (From p In Context.PE_CRITERIA_OBJECT_GROUP
                               Where p.ID = obj.ID).FirstOrDefault

                If objData Is Nothing Then
                    objData = New PE_CRITERIA_OBJECT_GROUP
                    With objData
                        .ID = Utilities.GetNextSequence(Context, Context.PE_CRITERIA_OBJECT_GROUP.EntitySet.Name)
                        .OBJECT_GROUP_ID = obj.OBJECT_GROUP_ID
                        .CRITERIA_ID = obj.CRITERIA_CODE
                        .EXPENSE = obj.EXPENSE
                        .AMONG = obj.AMONG
                        .FROM_DATE = obj.FROM_DATE
                        .TO_DATE = obj.TO_DATE
                        Context.PE_CRITERIA_OBJECT_GROUP.AddObject(objData)
                    End With
                Else
                    With objData
                        .EXPENSE = obj.EXPENSE
                        .AMONG = obj.AMONG
                        .FROM_DATE = obj.FROM_DATE
                        .TO_DATE = obj.TO_DATE
                    End With
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

#Region "EmployeeAssessment"

    Public Function GetEmployeeNotByObjectGroupID(ByVal _filter As EmployeeAssessmentDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                                Optional ByVal log As UserLog = Nothing) As List(Of EmployeeAssessmentDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = 1,
                                           .P_ISDISSOLVE = 0})
            End Using

            Dim query = From p In Context.HU_EMPLOYEE
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID _
                                                                  And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From org In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        From staff In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                        Where Not (From can In Context.PE_EMPLOYEE_ASSESSMENT
                                 Where can.OBJECT_GROUP_ID = _filter.OBJECT_GROUP_ID _
                                 And can.PERIOD_ID = _filter.PERIOD_ID
                                 Select can.EMPLOYEE_ID).Contains(p.ID) _
                             And p.CONTRACT_ID IsNot Nothing
                             Select New EmployeeAssessmentDTO With {
                                 .EMPLOYEE_ID = p.ID,
                                 .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                 .EMPLOYEE_NAME = p.FULLNAME_VN,
                                 .ORG_NAME = org.NAME_VN,
                                 .ORG_PATH = org.ORG_PATH,
                                 .TITLE_NAME = title.NAME_VN,
                                 .STAFF_RANK_NAME = staff.NAME}

            Dim lst = query
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_PATH <> "" Then
                lst = lst.Where(Function(f) f.ORG_PATH.ToUpper.Contains(_filter.ORG_PATH.ToUpper))
            End If
            If _filter.STAFF_RANK_NAME <> "" Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToUpper.Contains(_filter.STAFF_RANK_NAME.ToUpper))
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

    Public Function GetEmployeeByObjectGroupID(ByVal _filter As EmployeeAssessmentDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "Employee_CODE desc") As  _
                                         List(Of EmployeeAssessmentDTO)

        Try
            Dim query = From p In Context.PE_EMPLOYEE_ASSESSMENT
                        From obj In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From org In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = obj.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = obj.TITLE_ID)
                        From staff In Context.HU_STAFF_RANK.Where(Function(f) f.ID = obj.STAFF_RANK_ID).DefaultIfEmpty
                        Where p.OBJECT_GROUP_ID = _filter.OBJECT_GROUP_ID And p.PERIOD_ID = _filter.PERIOD_ID
                        Select New EmployeeAssessmentDTO With {.ID = p.ID,
                                                              .OBJECT_GROUP_ID = p.OBJECT_GROUP_ID,
                                                              .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                              .EMPLOYEE_CODE = obj.EMPLOYEE_CODE,
                                                              .EMPLOYEE_NAME = obj.FULLNAME_VN,
                                                               .ORG_NAME = org.NAME_VN,
                                                               .ORG_PATH = org.ORG_PATH,
                                                               .TITLE_NAME = title.NAME_VN,
                                                               .STAFF_RANK_NAME = staff.NAME}

            Dim lst = query
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_PATH <> "" Then
                lst = lst.Where(Function(f) f.ORG_PATH.ToUpper.Contains(_filter.ORG_PATH.ToUpper))
            End If
            If _filter.STAFF_RANK_NAME <> "" Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToUpper.Contains(_filter.STAFF_RANK_NAME.ToUpper))
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

    Public Function InsertEmployeeByObjectGroup(ByVal lst As List(Of EmployeeAssessmentDTO),
                                              ByVal log As UserLog) As Boolean
        Dim iCount As Integer = 0
        Dim objData As PE_EMPLOYEE_ASSESSMENT
        Try
            For Each obj In lst
                Dim isExist = (From p In Context.PE_EMPLOYEE_ASSESSMENT
                               Where p.OBJECT_GROUP_ID = obj.OBJECT_GROUP_ID _
                               And p.PERIOD_ID = obj.PERIOD_ID _
                               And p.EMPLOYEE_ID = obj.EMPLOYEE_ID).Any
                If Not isExist Then
                    objData = New PE_EMPLOYEE_ASSESSMENT
                    objData.ID = Utilities.GetNextSequence(Context, Context.PE_EMPLOYEE_ASSESSMENT.EntitySet.Name)
                    objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
                    objData.PERIOD_ID = obj.PERIOD_ID
                    objData.OBJECT_GROUP_ID = obj.OBJECT_GROUP_ID
                    objData.PE_STATUS = 6140
                    Context.PE_EMPLOYEE_ASSESSMENT.AddObject(objData)
                End If
            Next

            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function DeleteEmployeeByObjectGroup(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean
        Try
            Dim lstData = (From p In Context.PE_EMPLOYEE_ASSESSMENT Where lst.Contains(p.ID)).ToList
            For Each objData In lstData
                Context.PE_EMPLOYEE_ASSESSMENT.DeleteObject(objData)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex

        End Try
    End Function

#End Region

End Class

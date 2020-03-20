Imports Framework.Data
Imports System.Data.Objects.DataClasses
Imports System.Data.Common
Imports System.Data.Entity
Imports System.Threading
Imports Framework.Data.System.Linq.Dynamic
Imports Framework.Data.SystemConfig
Imports System.Configuration
Imports System.Data.Objects
Imports Oracle.DataAccess.Client

Partial Class TrainingRepository

#Region "Otherlist"

    Public Function GetCourseList() As List(Of CourseDTO)
        Try
            Dim lst As New List(Of CourseDTO)
            lst = (From p In Context.TR_COURSE Where p.ACTFLG = -1
                    Select New CourseDTO() With {
                            .ID = p.ID,
                            .NAME = p.NAME
                    }).ToList()
            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCourseList")
            Throw ex
        End Try
    End Function

    Public Function GetTitlesByOrgs(ByVal orgIds As List(Of Decimal), ByVal langCode As String) As List(Of PlanTitleDTO)
        Try
            Dim lst As New List(Of PlanTitleDTO)
            Dim iEnum = (From ot In Context.HU_ORG_TITLE
                    Join t In Context.HU_TITLE On ot.TITLE_ID _
                    Equals t.ID Where t.ACTFLG = "A" And orgIds.Contains(ot.ORG_ID)
                    Select New With {
                        .ID = t.ID,
                        .NAME_EN = t.NAME_EN,
                        .NAME_VN = t.NAME_VN
                    })
            If langCode = "vi-VN" Then
                lst = (From ele In iEnum Select New PlanTitleDTO() With {.ID = ele.ID, .NAME = ele.NAME_VN}).Distinct().ToList
            Else
                lst = (From ele In iEnum Select New PlanTitleDTO() With {.ID = ele.ID, .NAME = ele.NAME_EN}).Distinct().ToList
            End If
            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetTitlesByOrgs")
            Throw ex
        End Try
    End Function
    Public Function GetWIByTitle(ByVal orgIds As List(Of Decimal), ByVal langCode As String) As List(Of PlanTitleDTO)
        Try

            Dim lst As New List(Of PlanTitleDTO)
            Dim iEnum = (From t In Context.HU_TITLE
                    Join tt In Context.OT_OTHER_LIST On t.WORK_INVOLVE_ID Equals tt.ID
                    Where t.ACTFLG = "A" And orgIds.Contains(t.ID) And t.WORK_INVOLVE_ID.HasValue
                    Select New With {
                        .ID = tt.ID,
                        .NAME_EN = tt.NAME_EN,
                        .NAME_VN = tt.NAME_VN
                    })
            If langCode = "vi-VN" Then
                lst = (From ele In iEnum Select New PlanTitleDTO() With {.ID = ele.ID, .NAME = ele.NAME_VN}).Distinct().ToList
            Else
                lst = (From ele In iEnum Select New PlanTitleDTO() With {.ID = ele.ID, .NAME = ele.NAME_EN}).Distinct().ToList
            End If
            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetWIByTitle")
            Throw ex
        End Try
    End Function

    Public Function GetEntryAndFormByCourseID(ByVal CourseId As Decimal, ByVal langCode As String) As CourseDTO
        Try
            Dim Course As New CourseDTO
            Dim query = From p In Context.TR_COURSE
                       From cer In Context.TR_CERTIFICATE.Where(Function(f) f.ID = p.TR_CERTIFICATE_ID).DefaultIfEmpty
                       From cer_group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TR_CER_GROUP_ID).DefaultIfEmpty
                       From tf In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TR_TRAIN_FIELD).DefaultIfEmpty
                       From pg In Context.TR_PROGRAM_GROUP.Where(Function(f) f.ID = p.TR_PROGRAM_GROUP).DefaultIfEmpty
                       Where p.ID = CourseId
                       Select New CourseDTO With {
                           .ID = p.ID,
                           .CODE = p.CODE,
                           .NAME = p.NAME,
                           .TR_CER_GROUP_ID = cer.TR_CER_GROUP_ID,
                           .TR_CER_GROUP_NAME = cer_group.NAME_VN,
                           .TR_CERTIFICATE_ID = p.TR_CERTIFICATE_ID,
                           .TR_CERTIFICATE_NAME = cer.NAME_VN,
                           .TR_TRAIN_FIELD_ID = p.TR_TRAIN_FIELD,
                           .TR_TRAIN_FIELD_NAME = tf.NAME_VN,
                           .TR_PROGRAM_GROUP_ID = p.TR_PROGRAM_GROUP,
                           .TR_PROGRAM_GROUP_NAME = pg.NAME,
                           .REMARK = p.REMARK,
                           .ACTFLG = p.ACTFLG,
                           .CREATED_DATE = p.CREATED_DATE}
            Course = query.ToList.Item(0)
            Return Course
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetEntryAndFormByCourseID")
            Throw ex
        End Try
    End Function

    Public Function GetCenters() As List(Of CenterDTO)
        Try
            Dim ListCenter As List(Of CenterDTO) = (From record In Context.TR_CENTER
                                                    Where record.ACTFLG = -1
                                                    Select New CenterDTO _
                                                        With {
                                                            .ID = record.ID,
                                                            .NAME_VN = record.NAME_VN,
                                                            .NAME_EN = record.NAME_EN}).ToList
            Return ListCenter
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCenters")
            Throw ex
        End Try
    End Function

#End Region

#Region "Plan"


    Public Function GetPlans(ByVal filter As PlanDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PlanDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.TR_PLAN
                        From u In Context.TR_UNIT.Where(Function(f) f.ID = p.UNIT_ID).DefaultIfEmpty
                    From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                    From course In Context.TR_COURSE.Where(Function(f) f.ID = p.TR_COURSE_ID).DefaultIfEmpty
                    From c In Context.TR_COURSE.Where(Function(f) f.ID = p.TR_COURSE_ID)
                    From pg In Context.TR_PROGRAM_GROUP.Where(Function(f) f.ID = c.TR_PROGRAM_GROUP).DefaultIfEmpty
                    From pn In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.PROPERTIES_NEED_ID).DefaultIfEmpty
                    From tf In Context.OT_OTHER_LIST.Where(Function(f) f.ID = c.TR_TRAIN_FIELD).DefaultIfEmpty
                    From form In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TRAIN_FORM_ID).DefaultIfEmpty
                    From duration In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TR_DURATION_UNIT_ID).DefaultIfEmpty
                    From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                    Where p.YEAR = filter.YEAR
            Select New PlanDTO With {.ID = p.ID,
                                      .YEAR = p.YEAR,
                                      .NAME = p.NAME,
                                      .ORG_NAME = org.NAME_VN,
                                      .TR_COURSE_NAME = course.NAME,
                                      .TR_TRAIN_FORM_ID = p.TRAIN_FORM_ID,
                                      .TR_TRAIN_FORM_NAME = form.NAME_VN,
                                      .TEACHER_NUMBER = p.TEACHER_NUMBER,
                                      .STUDENT_NUMBER = p.STUDENT_NUMBER,
                                      .COST_TRAIN = p.COST_TRAIN,
                                      .COST_INCURRED = p.COST_INCURRED,
                                      .COST_TRAVEL = p.COST_TRAVEL,
                                      .COST_TOTAL = p.COST_TOTAL,
                                      .COST_TOTAL_USD = p.COST_TOTAL_USD,
                                      .COST_OTHER = p.COST_OTHER,
                                      .COST_OF_STUDENT = p.COST_OF_STUDENT,
                                      .COST_OF_STUDENT_USD = p.COST_OF_STUDENT_USD,
                                      .TARGET_TRAIN = p.TARGET_TRAIN,
                                      .VENUE = p.VENUE,
                                      .REMARK = p.REMARK,
                                      .Departments_NAME = p.DEPARTMENTS,
                                      .Titles_NAME = p.TITLES,
                                      .Centers_NAME = p.CENTERS,
                                      .Months_NAME = p.MONTHS,
                                      .DURATION = p.DURATION,
                                      .TR_DURATION_UNIT_NAME = duration.NAME_VN,
                                      .TR_TRAIN_FIELD_NAME = tf.NAME_VN,
                                      .TR_PROGRAM_GROUP_NAME = pg.NAME,
                                      .PROPERTIES_NEED_ID = p.PROPERTIES_NEED_ID,
                                      .PROPERTIES_NEED_NAME = pn.NAME_VN,
                                      .UNIT_ID = p.UNIT_ID,
                                      .UNIT_NAME = u.NAME,
                                      .Work_inv_NAME = p.WORKS,
                                      .ATTACHFILE = p.ATTACHFILE,
                                      .CREATED_DATE = p.CREATED_DATE}



            Dim lst = query
            If filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(filter.NAME.ToUpper))
            End If
            If filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(filter.ORG_NAME.ToUpper))
            End If
            If filter.TR_COURSE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_COURSE_NAME.ToUpper.Contains(filter.TR_COURSE_NAME.ToUpper))
            End If
            If filter.TR_TRAIN_FORM_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_TRAIN_FORM_NAME.ToUpper.Contains(filter.TR_TRAIN_FORM_NAME.ToUpper))
            End If
            If filter.TR_TRAIN_ENTRIES_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_TRAIN_ENTRIES_NAME.ToUpper.Contains(filter.TR_TRAIN_ENTRIES_NAME.ToUpper))
            End If
            If filter.TR_DURATION_UNIT_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_DURATION_UNIT_NAME.ToUpper.Contains(filter.TR_DURATION_UNIT_NAME.ToUpper))
            End If

            If filter.DURATION.HasValue Then
                lst = lst.Where(Function(p) p.DURATION = filter.DURATION)
            End If
            If filter.TEACHER_NUMBER.HasValue Then
                lst = lst.Where(Function(p) p.TEACHER_NUMBER = filter.TEACHER_NUMBER)
            End If
            If filter.STUDENT_NUMBER.HasValue Then
                lst = lst.Where(Function(p) p.STUDENT_NUMBER = filter.STUDENT_NUMBER)
            End If
            If filter.TEACHER_NUMBER.HasValue Then
                lst = lst.Where(Function(p) p.TEACHER_NUMBER = filter.TEACHER_NUMBER)
            End If
            If filter.COST_TRAIN.HasValue Then
                lst = lst.Where(Function(p) p.COST_TRAIN = filter.COST_TRAIN)
            End If
            If filter.COST_INCURRED.HasValue Then
                lst = lst.Where(Function(p) p.COST_INCURRED = filter.COST_INCURRED)
            End If
            If filter.COST_TRAVEL.HasValue Then
                lst = lst.Where(Function(p) p.TEACHER_NUMBER = filter.COST_TRAVEL)
            End If
            If filter.COST_TOTAL.HasValue Then
                lst = lst.Where(Function(p) p.COST_TOTAL = filter.COST_TOTAL)
            End If
            If filter.COST_OTHER.HasValue Then
                lst = lst.Where(Function(p) p.COST_OTHER = filter.COST_OTHER)
            End If
            If filter.COST_OF_STUDENT.HasValue Then
                lst = lst.Where(Function(p) p.COST_OF_STUDENT = filter.COST_OF_STUDENT)
            End If
            If filter.CURRENCY <> "" Then
                lst = lst.Where(Function(p) p.CURRENCY.ToUpper.Contains(filter.CURRENCY.ToUpper))
            End If
            If filter.TARGET_TRAIN <> "" Then
                lst = lst.Where(Function(p) p.TARGET_TRAIN.ToUpper.Contains(filter.TARGET_TRAIN.ToUpper))
            End If
            If filter.VENUE <> "" Then
                lst = lst.Where(Function(p) p.VENUE.ToUpper.Contains(filter.VENUE.ToUpper))
            End If
            If filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(filter.REMARK.ToUpper))
            End If
            If filter.Departments_NAME <> "" Then
                lst = lst.Where(Function(p) p.Departments_NAME.ToUpper.Contains(filter.Departments_NAME.ToUpper))
            End If
            If filter.Titles_NAME <> "" Then
                lst = lst.Where(Function(p) p.Titles_NAME.ToUpper.Contains(filter.Titles_NAME.ToUpper))
            End If
            If filter.Months_NAME <> "" Then
                lst = lst.Where(Function(p) p.Months_NAME.ToUpper.Contains(filter.Months_NAME.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList()
            Return Nothing
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPlans")
            Throw ex
        End Try
    End Function

    Public Function GetPlanById(ByVal Id As Decimal) As PlanDTO
        Try
            Dim objPlan As PlanDTO = (From plan In Context.TR_PLAN
                                      From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = plan.ORG_ID)
                                      Where plan.ID = Id
                                      Select New PlanDTO With {.ID = plan.ID,
                                                               .YEAR = plan.YEAR,
                                                               .COST_INCURRED = plan.COST_INCURRED,
                                                               .COST_OF_STUDENT = plan.COST_OF_STUDENT,
                                                               .COST_OF_STUDENT_USD = plan.COST_OF_STUDENT_USD,
                                                               .COST_OTHER = plan.COST_OTHER,
                                                               .COST_TOTAL = plan.COST_TOTAL,
                                                                .COST_TOTAL_USD = plan.COST_TOTAL_USD,
                                                               .COST_TRAIN = plan.COST_TRAIN,
                                                               .COST_TRAVEL = plan.COST_TRAVEL,
                                                               .DURATION = plan.DURATION,
                                                               .NAME = plan.NAME,
                                                               .ORG_ID = plan.ORG_ID,
                                                               .ORG_NAME = org.NAME_VN,
                                                               .PLAN_T1 = plan.PLAN_T1,
                                                               .PLAN_T10 = plan.PLAN_T10,
                                                               .PLAN_T11 = plan.PLAN_T11,
                                                               .PLAN_T12 = plan.PLAN_T12,
                                                               .PLAN_T2 = plan.PLAN_T2,
                                                               .PLAN_T3 = plan.PLAN_T3,
                                                               .PLAN_T4 = plan.PLAN_T4,
                                                               .PLAN_T5 = plan.PLAN_T5,
                                                               .PLAN_T6 = plan.PLAN_T6,
                                                               .PLAN_T7 = plan.PLAN_T7,
                                                               .PLAN_T8 = plan.PLAN_T8,
                                                               .PLAN_T9 = plan.PLAN_T9,
                                                               .REMARK = plan.REMARK,
                                                               .STUDENT_NUMBER = plan.STUDENT_NUMBER,
                                                               .TARGET_TRAIN = plan.TARGET_TRAIN,
                                                               .TEACHER_NUMBER = plan.TEACHER_NUMBER,
                                                               .TR_COURSE_ID = plan.TR_COURSE_ID,
                                                               .TR_CURRENCY_ID = plan.TR_CURRENCY_ID,
                                                               .TR_DURATION_UNIT_ID = plan.TR_DURATION_UNIT_ID,
                                                               .TR_TRAIN_FORM_ID = plan.TRAIN_FORM_ID,
                                                               .PROPERTIES_NEED_ID = plan.PROPERTIES_NEED_ID,
                                                                    .UNIT_ID = plan.UNIT_ID,
                                                               .ATTACHFILE = plan.ATTACHFILE,
                                                               .VENUE = plan.VENUE}).FirstOrDefault

            objPlan.Units = (From p In Context.TR_PLAN_UNIT
                             From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                             Where p.TR_PLAN_ID = Id
                             Select New PlanOrgDTO With {.ID = p.ORG_ID,
                                                     .NAME = o.NAME_VN}).ToList

            objPlan.Titles = (From p In Context.TR_PLAN_TITLE
                              From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                              Where p.TR_PLAN_ID = Id
                              Select New PlanTitleDTO With {.ID = p.TITLE_ID,
                                                        .NAME = t.NAME_VN}).ToList

            objPlan.Centers = (From p In Context.TR_PLAN_CENTER
                               From c In Context.TR_CENTER.Where(Function(f) f.ID = p.CENTER_ID)
                               Where p.TR_PLAN_ID = Id
                               Select New PlanCenterDTO With {.ID = p.CENTER_ID,
                                                          .NAME_EN = c.NAME_EN,
                                                          .NAME_VN = c.NAME_VN}).ToList

            Return objPlan
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPlanById")
            Throw ex
        End Try
    End Function
    Public Function test(ByVal a As CostDetailDTO) As CostDetailDTO
        Dim b As New CostDetailDTO
        Return b
    End Function

    Public Function InsertPlan(ByVal plan As PlanDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPlan As New TR_PLAN

        Try
            With objPlan
                .ID = Utilities.GetNextSequence(Context, Context.TR_PLAN.EntitySet.Name)
                .COST_INCURRED = plan.COST_INCURRED
                .COST_OF_STUDENT = plan.COST_OF_STUDENT
                .COST_OTHER = plan.COST_OTHER
                .COST_TOTAL = plan.COST_TOTAL
                .COST_TRAIN = plan.COST_TRAIN
                .COST_TRAVEL = plan.COST_TRAVEL
                .DURATION = plan.DURATION
                .NAME = plan.NAME
                .ORG_ID = plan.ORG_ID
                .PLAN_T1 = plan.PLAN_T1
                .PLAN_T10 = plan.PLAN_T10
                .PLAN_T11 = plan.PLAN_T11
                .PLAN_T12 = plan.PLAN_T12
                .PLAN_T2 = plan.PLAN_T2
                .PLAN_T3 = plan.PLAN_T3
                .PLAN_T4 = plan.PLAN_T4
                .PLAN_T5 = plan.PLAN_T5
                .PLAN_T6 = plan.PLAN_T6
                .PLAN_T7 = plan.PLAN_T7
                .PLAN_T8 = plan.PLAN_T8
                .PLAN_T9 = plan.PLAN_T9
                .REMARK = plan.REMARK
                .STUDENT_NUMBER = plan.STUDENT_NUMBER
                .TARGET_TRAIN = plan.TARGET_TRAIN
                .TEACHER_NUMBER = plan.TEACHER_NUMBER
                .TR_COURSE_ID = plan.TR_COURSE_ID
                .TR_CURRENCY_ID = plan.TR_CURRENCY_ID
                .TR_DURATION_UNIT_ID = plan.TR_DURATION_UNIT_ID
                .VENUE = plan.VENUE
                .MONTHS = plan.Months_NAME
                .DEPARTMENTS = plan.Departments_NAME
                .CENTERS = plan.Centers_NAME
                .TITLES = plan.Titles_NAME
                .YEAR = plan.YEAR
                .PROPERTIES_NEED_ID = plan.PROPERTIES_NEED_ID
                .UNIT_ID = plan.UNIT_ID
                .ATTACHFILE = plan.ATTACHFILE
                .COST_TOTAL_USD = plan.COST_TOTAL_USD
                .COST_OF_STUDENT_USD = plan.COST_OF_STUDENT_USD
                .TRAIN_FORM_ID = plan.TR_TRAIN_FORM_ID
                .WORKS = plan.Work_inv_NAME
            End With
            gID = objPlan.ID
            Context.TR_PLAN.AddObject(objPlan)
            For Each unit In plan.CostDetail
                Context.TR_PLAN_COST_DETAIL.AddObject(New TR_PLAN_COST_DETAIL With {
                                               .ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_COST_DETAIL.EntitySet.Name),
                                               .PLAN_ID = objPlan.ID,
                                               .TYPE_ID = unit.TYPE_ID,
                                               .MONEY = unit.MONEY,
                                               .MONEY_TYPE = unit.MONEY_TYPE,
                                               .MODIFIED_DATE = Date.Now})
            Next

            For Each unit In plan.Units
                Context.TR_PLAN_UNIT.AddObject(New TR_PLAN_UNIT With {
                                               .ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_UNIT.EntitySet.Name),
                                               .TR_PLAN_ID = objPlan.ID,
                                               .ORG_ID = unit.ID})
            Next

            For Each title In plan.Titles
                Context.TR_PLAN_TITLE.AddObject(New TR_PLAN_TITLE With {
                                                .ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_TITLE.EntitySet.Name),
                                                .TR_PLAN_ID = objPlan.ID,
                                                .TITLE_ID = title.ID})
            Next

            For Each center In plan.Centers
                Context.TR_PLAN_CENTER.AddObject(New TR_PLAN_CENTER With {
                                                 .ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_CENTER.EntitySet.Name),
                                                 .TR_PLAN_ID = objPlan.ID,
                                                 .CENTER_ID = center.ID})
            Next
            For Each lstemp In plan.Plan_Emp
                Context.TR_PLAN_EMPLOYEE.AddObject(New TR_PLAN_EMPLOYEE With {
                                               .ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_EMPLOYEE.EntitySet.Name),
                                               .TR_PLAN_ID = objPlan.ID,
                                               .EMPLOYEE_ID = lstemp.EMPLOYEE_ID})
            Next

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertPlan")
            Throw ex
        End Try
    End Function

    Public Function ModifyPlan(ByVal plan As PlanDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPlan As New TR_PLAN With {.ID = plan.ID}
        Try
            Context.TR_PLAN.Attach(objPlan)
            With objPlan
                .COST_INCURRED = plan.COST_INCURRED
                .COST_OF_STUDENT = plan.COST_OF_STUDENT
                .COST_OTHER = plan.COST_OTHER
                .COST_TOTAL = plan.COST_TOTAL
                .COST_TRAIN = plan.COST_TRAIN
                .COST_TRAVEL = plan.COST_TRAVEL
                .DURATION = plan.DURATION
                .NAME = plan.NAME
                .ORG_ID = plan.ORG_ID
                .PLAN_T1 = plan.PLAN_T1
                .PLAN_T10 = plan.PLAN_T10
                .PLAN_T11 = plan.PLAN_T11
                .PLAN_T12 = plan.PLAN_T12
                .PLAN_T2 = plan.PLAN_T2
                .PLAN_T3 = plan.PLAN_T3
                .PLAN_T4 = plan.PLAN_T4
                .PLAN_T5 = plan.PLAN_T5
                .PLAN_T6 = plan.PLAN_T6
                .PLAN_T7 = plan.PLAN_T7
                .PLAN_T8 = plan.PLAN_T8
                .PLAN_T9 = plan.PLAN_T9
                .REMARK = plan.REMARK
                .STUDENT_NUMBER = plan.STUDENT_NUMBER
                .TARGET_TRAIN = plan.TARGET_TRAIN
                .TEACHER_NUMBER = plan.TEACHER_NUMBER
                .TR_COURSE_ID = plan.TR_COURSE_ID
                .TR_CURRENCY_ID = plan.TR_CURRENCY_ID
                .TR_DURATION_UNIT_ID = plan.TR_DURATION_UNIT_ID
                .VENUE = plan.VENUE
                .MONTHS = plan.Months_NAME
                .DEPARTMENTS = plan.Departments_NAME
                .CENTERS = plan.Centers_NAME
                .TITLES = plan.Titles_NAME
                .YEAR = plan.YEAR
                .PROPERTIES_NEED_ID = plan.PROPERTIES_NEED_ID
                .UNIT_ID = plan.UNIT_ID
                .ATTACHFILE = plan.ATTACHFILE
                .COST_TOTAL_USD = plan.COST_TOTAL_USD
                .COST_OF_STUDENT_USD = plan.COST_OF_STUDENT_USD
                .TRAIN_FORM_ID = plan.TR_TRAIN_FORM_ID
                .WORKS = plan.Work_inv_NAME
            End With
            Dim oldCostDetail = From i In Context.TR_PLAN_COST_DETAIL Where i.PLAN_ID = plan.ID
            For Each unit In oldCostDetail
                Context.TR_PLAN_COST_DETAIL.DeleteObject(unit)
            Next

            Dim oldUnits = From i In Context.TR_PLAN_UNIT Where i.TR_PLAN_ID = plan.ID
            For Each unit In oldUnits
                Context.TR_PLAN_UNIT.DeleteObject(unit)
            Next

            Dim oldTItles = From i In Context.TR_PLAN_TITLE Where i.TR_PLAN_ID = plan.ID
            For Each item In oldTItles
                Context.TR_PLAN_TITLE.DeleteObject(item)
            Next

            Dim oldCenters = From i In Context.TR_PLAN_CENTER Where i.TR_PLAN_ID = plan.ID
            For Each item In oldCenters
                Context.TR_PLAN_CENTER.DeleteObject(item)
            Next
            Dim oldlstEmp = From i In Context.TR_PLAN_EMPLOYEE Where i.TR_PLAN_ID = plan.ID
            For Each item In oldlstEmp
                Context.TR_PLAN_EMPLOYEE.DeleteObject(item)
            Next
            For Each unit In plan.CostDetail
                Context.TR_PLAN_COST_DETAIL.AddObject(New TR_PLAN_COST_DETAIL With {
                                               .ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_COST_DETAIL.EntitySet.Name),
                                               .PLAN_ID = objPlan.ID,
                                               .TYPE_ID = unit.TYPE_ID,
                                               .MONEY = unit.MONEY,
                                               .MONEY_TYPE = unit.MONEY_TYPE,
                                               .MODIFIED_DATE = Date.Now})
            Next

            For Each unit In plan.Units
                Context.TR_PLAN_UNIT.AddObject(New TR_PLAN_UNIT With {.ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_UNIT.EntitySet.Name), .TR_PLAN_ID = objPlan.ID, .ORG_ID = unit.ID})
            Next

            For Each title In plan.Titles
                Context.TR_PLAN_TITLE.AddObject(New TR_PLAN_TITLE With {.ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_TITLE.EntitySet.Name), .TR_PLAN_ID = objPlan.ID, .TITLE_ID = title.ID})
            Next

            For Each center In plan.Centers
                Context.TR_PLAN_CENTER.AddObject(New TR_PLAN_CENTER With {.ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_CENTER.EntitySet.Name), .TR_PLAN_ID = objPlan.ID, .CENTER_ID = center.ID})
            Next
            For Each lstemp In plan.Plan_Emp
                Context.TR_PLAN_EMPLOYEE.AddObject(New TR_PLAN_EMPLOYEE With {.ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_EMPLOYEE.EntitySet.Name), .TR_PLAN_ID = objPlan.ID, .EMPLOYEE_ID = lstemp.EMPLOYEE_ID})
            Next
            Context.SaveChanges(log)
            gID = objPlan.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyPlan")
            Throw ex
        End Try

    End Function

    Public Function DeletePlans(ByVal lstId As List(Of Decimal)) As Boolean
        Try
            Dim deletedPlans = (From record In Context.TR_PLAN Where lstId.Contains(record.ID))
            Dim deletedPlanTitle = (From record In Context.TR_PLAN_TITLE Where lstId.Contains(record.TR_PLAN_ID))
            For Each item In deletedPlanTitle
                Context.TR_PLAN_TITLE.DeleteObject(item)
            Next

            Dim deletedPlanDeparment = (From record In Context.TR_PLAN_UNIT Where lstId.Contains(record.TR_PLAN_ID))
            For Each item In deletedPlanDeparment
                Context.TR_PLAN_UNIT.DeleteObject(item)
            Next

            Dim deletedPlanCenter = (From record In Context.TR_PLAN_CENTER Where lstId.Contains(record.TR_PLAN_ID))
            For Each item In deletedPlanCenter
                Context.TR_PLAN_CENTER.DeleteObject(item)
            Next

            Dim deletedPlanCost = (From record In Context.TR_PLAN_COST_DETAIL Where lstId.Contains(record.PLAN_ID))
            For Each item In deletedPlanCost
                Context.TR_PLAN_COST_DETAIL.DeleteObject(item)
            Next

            For Each item In deletedPlans
                Context.TR_PLAN.DeleteObject(item)
            Next
            Dim deletedPlanEmp = (From record In Context.TR_PLAN_EMPLOYEE Where lstId.Contains(record.TR_PLAN_ID))
            For Each item In deletedPlanEmp
                Context.TR_PLAN_EMPLOYEE.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeletePlans")
            Throw ex
        End Try
    End Function

#End Region

#Region "Request"

    Public Function ToDate(ByVal item As Object)
        If IsDBNull(item) Then
            Return Nothing
        Else
            Return DateTime.ParseExact(item, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture)
        End If
    End Function
    Public Function ToDecimal(ByVal item As Object)
        If IsDBNull(item) Then
            Return Nothing
        Else
            Return CDec(item)
        End If
    End Function

    Public Function GetTrainingRequests(ByVal filter As RequestDTO,
                                         ByVal PageIndex As Integer, ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of RequestDTO)
        Try
            Dim lst As List(Of RequestDTO) = New List(Of RequestDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_TRAINING.GET_TR_REQUEST",
                                           New With {.P_YEAR = filter.YEAR,
                                                     .P_STATUS = filter.STATUS_ID,
                                                     .P_ORGID = filter.ORG_ID,
                                                     .P_USERNAME = log.Username.ToUpper,
                                                     .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    lst = (From row As DataRow In dtData.Rows
                       Select New RequestDTO With {.ID = row("ID").ToString(),
                                                   .STATUS_ID = row("STATUS_ID").ToString(),
                                                   .STATUS_NAME = row("STATUS_NAME").ToString(),
                                                   .SENDER_CODE = row("SENDER_CODE").ToString(),
                                                   .SENDER_NAME = row("SENDER_NAME").ToString(),
                                                   .SENDER_EMAIL = row("SENDER_EMAIL").ToString(),
                                                   .SENDER_MOBILE = row("SENDER_MOBILE").ToString(),
                                                   .REQUEST_DATE = ToDate(row("REQUEST_DATE")),
                                                   .COURSE_NAME = row("COURSE_NAME").ToString(),
                                                   .TRAIN_FORM = row("TRAIN_FORM").ToString(),
                                                   .TARGET_TRAIN = row("TARGET_TRAIN").ToString(),
                                                   .CONTENT = row("CONTENT").ToString(),
                                                   .COM_ID = ToDecimal(row("COM_ID")),
                                                   .COM_NAME = row("COM_NAME").ToString(),
                                                   .COM_DESC = row("COM_DESC").ToString(),
                                                   .ORG_ID = ToDecimal(row("ORG_ID")),
                                                   .ORG_NAME = row("ORG_NAME").ToString(),
                                                   .ORG_DESC = row("ORG_DESC").ToString(),
                                                   .EXPECTED_DATE = ToDate(row("EXPECTED_DATE")),
                                                   .START_DATE = ToDate(row("START_DATE")),
                                                   .CENTERS = row("CENTERS").ToString(),
                                                   .UNIT_NAME = row("UNIT_NAME").ToString(),
                                                   .TEACHERS = row("TEACHERS").ToString(),
                                                   .VENUE = row("VENUE").ToString(),
                                                   .EXPECTED_COST = ToDecimal(row("EXPECTED_COST")),
                                                   .REMARK = row("REMARK").ToString(),
                                                   .CREATED_DATE = row("CREATED_DATE").ToString(),
                                                   .REJECT_REASON = row("REJECT_REASON").ToString(),
                                                   .TR_PLAN_NAME = row("TR_PLAN_NAME").ToString()
                                                  }).ToList
                End If
            End Using

            'Using cls As New DataAccess.QueryData
            '    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
            '                     New With {.P_USERNAME = log.Username,
            '                               .P_ORGID = _param.ORG_ID,
            '                               .P_ISDISSOLVE = _param.IS_DISSOLVE})
            'End Using

            'Dim lst = (From p In Context.TR_REQUEST
            '           From plan In Context.TR_PLAN.Where(Function(f) f.ID = p.TR_PLAN_ID).DefaultIfEmpty
            '           From Sender In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.REQUEST_SENDER_ID).DefaultIfEmpty
            '           From SenderInfo In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = Sender.ID).DefaultIfEmpty
            '           From Course In Context.TR_COURSE.Where(Function(f) f.ID = plan.TR_COURSE_ID).DefaultIfEmpty
            '           From TrainForm In Context.OT_OTHER_LIST.Where(Function(f) f.ID = plan.TRAIN_FORM_ID).DefaultIfEmpty
            '           From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
            '           From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
            '           From c In Context.TR_COURSE.Where(Function(f) f.ID = plan.TR_COURSE_ID).DefaultIfEmpty
            '           From unit In Context.TR.Where(Function(f) f.ID = plan.TR_COURSE_ID).DefaultIfEmpty
            '           From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
            '           Select New RequestDTO With {.ID = p.ID,
            '                                       .STATUS_ID = status.ID,
            '                                       .REQUEST_SENDER_ID = Sender.ID,
            '                                       .SENDER_CODE = Sender.EMPLOYEE_CODE,
            '                                       .SENDER_NAME = Sender.FULLNAME_VN,
            '                                       .SENDER_MOBILE = SenderInfo.MOBILE_PHONE,
            '                                       .SENDER_EMAIL = SenderInfo.WORK_EMAIL,
            '                                       .REQUEST_DATE = p.REQUEST_DATE,
            '                                       .COURSE_ID = plan.TR_COURSE_ID,
            '                                       .COURSE_NAME = Course.NAME,
            '                                       .TRAIN_FORM_ID = TrainForm.ID,
            '                                       .TRAIN_FORM = TrainForm.NAME_VN,
            '                                       .TARGET_TRAIN = plan.TARGET_TRAIN,
            '                                       .CONTENT = p.CONTENT,
            '                                       .COM_NAME = org.NAME_VN,
            '                                       .COM_DESC = org.DESCRIPTION_PATH,
            '                                       .ORG_NAME = org.NAME_VN,
            '                                       .ORG_DESC = org.DESCRIPTION_PATH,
            '                                       .YEAR = p.YEAR,
            '                                       .STATUS_NAME = status.NAME_VN,
            '                                       .EXPECTED_COST = p.EXPECTED_COST,
            '                                       .EXPECTED_DATE = p.EXPECTED_DATE,
            '                                       .START_DATE = p.START_DATE,
            '                                       .TR_PLAN_NAME = c.NAME & " - " & plan.NAME,
            '                                       .CENTERS = plan.CENTERS,
            '                                       .VENUE = plan.VENUE,
            '                                       .REMARK = p.REMARK,
            '                                       .CREATED_DATE = p.CREATED_DATE})

            'If filter.YEAR IsNot Nothing Then
            '    lst = (From l In lst Where l.YEAR = filter.YEAR).ToList
            'End If

            'If filter.STATUS_ID IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.STATUS_ID = filter.STATUS_ID)
            'End If

            'If filter.ORG_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(filter.ORG_NAME.ToUpper))
            'End If

            'If filter.REQUEST_DATE IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.REQUEST_DATE = filter.REQUEST_DATE)
            'End If

            'If filter.STATUS_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.STATUS_NAME.ToUpper.Contains(filter.STATUS_NAME.ToUpper))
            'End If

            'If filter.EXPECTED_COST IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.EXPECTED_COST = filter.EXPECTED_COST)
            'End If

            'If filter.EXPECTED_DATE IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.EXPECTED_DATE = filter.EXPECTED_DATE)
            'End If

            'If filter.START_DATE IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.START_DATE = filter.START_DATE)
            'End If

            'If filter.CONTENT <> "" Then
            '    lst = lst.Where(Function(p) p.CONTENT.ToUpper.Contains(filter.CONTENT.ToUpper))
            'End If

            'If filter.TR_PLAN_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.TR_PLAN_NAME.ToUpper.Contains(filter.TR_PLAN_NAME.ToUpper))
            'End If

            'If filter.CENTERS <> "" Then
            '    lst = lst.Where(Function(p) p.CENTERS.ToUpper.Contains(filter.CENTERS.ToUpper))
            'End If

            'If filter.VENUE <> "" Then
            '    lst = lst.Where(Function(p) p.VENUE.ToUpper.Contains(filter.VENUE.ToUpper))
            'End If

            'lst = lst.OrderBy(Sorts)
            lst = (From l In lst Order By Sorts Select l
                   Skip PageIndex * PageSize
                   Take PageSize).ToList
            Total = lst.Count()
            'lst = lst.Skip(PageIndex * PageSize)
            'lst = lst.Take(PageSize)
            Return lst '.ToList

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".LoadTrainingRequests")
            Throw ex
        End Try
    End Function

    Public Function GetTrainingRequestsByID(ByVal filter As RequestDTO) As RequestDTO
        Try
            Dim req As RequestDTO = New RequestDTO
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_TRAINING.GET_TR_REQUEST_BY_ID",
                                           New With {.P_ID = filter.ID,
                                                     .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    req = (From row As DataRow In dtData.Rows
                       Select New RequestDTO With {.ID = row("ID").ToString(),
                                                   .TR_PLAN_ID = row("TR_PLAN_ID").ToString(),
                                                   .ORG_ID = row("ORG_ID").ToString(),
                                                   .ORG_NAME = row("ORG_NAME").ToString(),
                                                   .ORG_DESC = row("ORG_DESC").ToString(),
                                                   .YEAR = row("YEAR").ToString(),
                                                   .IS_IRREGULARLY = row("IS_IRREGULARLY").ToString(),
                                                   .COURSE_ID = row("COURSE_ID").ToString(),
                                                   .COURSE_NAME = row("COURSE_NAME").ToString(),
                                                   .PROGRAM_GROUP = row("PROGRAM_GROUP").ToString(),
                                                   .TRAIN_FIELD = row("TRAIN_FIELD").ToString(),
                                                   .TRAIN_FORM_ID = row("TRAINFORM_ID").ToString(),
                                                   .TRAIN_FORM = row("TRAINFORM").ToString(),
                                                   .PROPERTIES_NEED_ID = row("PROPERTIES_NEED_ID").ToString(),
                                                   .PROPERTIES_NEED = row("PROPERTIES_NEED").ToString(),
                                                   .EXPECTED_DATE = ToDate(row("EXPECTED_DATE")),
                                                   .START_DATE = ToDate(row("START_DATE")),
                                                   .CONTENT = row("CONTENT").ToString(),
                                                   .CENTERS = row("CENTERS").ToString(),
                                                   .CENTERS_ID = row("CENTERS_ID").ToString(),
                                                   .TEACHERS = row("TEACHERS").ToString(),
                                                   .TEACHERS_ID = row("TEACHERS_ID").ToString(),
                                                   .UNIT_ID = row("UNIT_ID").ToString(),
                                                   .UNIT_NAME = row("UNIT").ToString(),
                                                   .EXPECTED_COST = row("EXPECTED_COST").ToString(),
                                                   .TR_CURRENCY_ID = row("CURRENCY_ID").ToString(),
                                                   .TR_CURRENCY_NAME = row("CURRENCY").ToString(),
                                                   .STATUS_ID = row("STATUS_ID").ToString(),
                                                   .STATUS_NAME = row("STATUS").ToString(),
                                                   .TARGET_TRAIN = row("TARGET_TRAIN").ToString(),
                                                   .VENUE = row("VENUE").ToString(),
                                                   .REQUEST_SENDER_ID = row("SENDER_ID").ToString(),
                                                   .SENDER_CODE = row("SENDER_CODE").ToString(),
                                                   .SENDER_NAME = row("SENDER_NAME").ToString(),
                                                   .SENDER_EMAIL = row("SENDER_EMAIL").ToString(),
                                                   .SENDER_MOBILE = row("SENDER_PHONE").ToString(),
                                                   .REQUEST_DATE = ToDate(row("REQUEST_DATE")),
                                                   .ATTACH_FILE = row("ATTACH_FILE").ToString(),
                                                   .REMARK = row("REMARK").ToString(),
                                                   .REJECT_REASON = row("REJECT_REASON").ToString()}).FirstOrDefault
                End If

                dtData = cls.ExecuteStore("PKG_TRAINING.GET_EMPLOYEE_BY_REQUEST_ID",
                                           New With {.P_ID = filter.ID,
                                                     .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    req.lstEmp = (From row As DataRow In dtData.Rows
                       Select New RequestEmpDTO With {.EMPLOYEE_ID = row("EMPLOYEE_ID").ToString(),
                                                      .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString(),
                                                      .EMPLOYEE_NAME = row("EMPLOYEE_NAME").ToString(),
                                                      .BIRTH_DATE = row("BIRTH_DATE").ToString(),
                                                      .TITLE_NAME = row("TITLE_NAME").ToString(),
                                                      .COM_NAME = row("COM_NAME").ToString(),
                                                      .ORG_NAME = row("ORG_NAME").ToString(),
                                                      .WORK_INVOLVE = row("WORK_INVOLVE").ToString()}).ToList
                End If
            End Using

            Return req

            'Dim lst = (From p In Context.TR_REQUEST
            '           From plan In Context.TR_PLAN.Where(Function(f) f.ID = p.TR_PLAN_ID)
            '           From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
            '           Where p.ID = filter.ID
            '       Select New RequestDTO With {.ID = p.ID,
            '                                   .ORG_ID = p.ORG_ID,
            '                                   .ORG_NAME = org.NAME_VN,
            '                                   .REQUEST_DATE = p.REQUEST_DATE,
            '                                   .YEAR = p.YEAR,
            '                                   .EXPECTED_COST = p.EXPECTED_COST,
            '                                   .EXPECTED_DATE = p.EXPECTED_DATE,
            '                                   .START_DATE = p.START_DATE,
            '                                   .CONTENT = p.CONTENT,
            '                                   .CENTERS = plan.CENTERS,
            '                                   .VENUE = plan.VENUE,
            '                                   .TR_PLAN_ID = p.TR_PLAN_ID,
            '                                   .TR_CURRENCY_ID = p.TR_CURRENCY_ID,
            '                                   .STATUS_ID = p.STATUS_ID,
            '                                   .CREATED_DATE = p.CREATED_DATE})

            'Dim obj = lst.FirstOrDefault
            'obj.lstEmp = (From req_emp In Context.TR_REQUEST_EMPLOYEE
            '              From p In Context.HU_EMPLOYEE.Where(Function(f) f.ID = req_emp.EMPLOYEE_ID)
            '              From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
            '              From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER).DefaultIfEmpty
            '              From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
            '             Where req_emp.TR_REQUEST_ID = obj.ID
            '              Select New RequestEmpDTO With {.EMPLOYEE_ID = p.ID,
            '                                             .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
            '                                             .EMPLOYEE_NAME = p.FULLNAME_VN,
            '                                             .BIRTH_DATE = cv.BIRTH_DATE,
            '                                             .GENDER_NAME = gender.NAME_VN,
            '                                             .TITLE_NAME = title.NAME_VN}).ToList
            'Return obj
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetTrainingRequestsByID")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeByImportRequest(ByRef lstEmpCode As List(Of RequestEmpDTO)) As String
        Try
            Dim sError As String = ""
            Dim lstError As New List(Of String)
            For Each item In lstEmpCode
                Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = item.EMPLOYEE_CODE).FirstOrDefault
                If emp Is Nothing Then
                    lstError.Add(item.EMPLOYEE_CODE)
                Else
                    item.EMPLOYEE_ID = emp.ID
                End If
            Next
            If lstError.Count > 0 Then
                sError = lstError.Aggregate(Function(x, y) x & ", " & y)
            End If
            Return sError

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetTrainingRequestsByID")
            Throw ex
        End Try
    End Function

    Public Function InsertRequest(ByVal Request As RequestDTO,
                                  ByVal lstEmp As List(Of RequestEmpDTO),
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objRequest As New TR_REQUEST
        Try
            With objRequest
                .ID = Utilities.GetNextSequence(Context, Context.TR_REQUEST.EntitySet.Name)
                .TR_PLAN_ID = Request.TR_PLAN_ID
                .ORG_ID = Request.ORG_ID
                .YEAR = Request.YEAR
                .IS_IRREGULARLY = Request.IS_IRREGULARLY
                .TR_COURSE_ID = Request.COURSE_ID
                .TRAIN_FORM_ID = Request.TRAIN_FORM_ID
                .PROPERTIES_NEED_ID = Request.PROPERTIES_NEED_ID
                .EXPECTED_DATE = Request.EXPECTED_DATE
                .START_DATE = Request.START_DATE
                .CONTENT = Request.CONTENT
                .TR_UNIT_ID = Request.UNIT_ID
                .EXPECTED_COST = Request.EXPECTED_COST
                .TR_CURRENCY_ID = Request.TR_CURRENCY_ID
                .STATUS_ID = TrainingCommon.TR_REQUEST_STATUS.WAIT_ID
                .TARGET_TRAIN = Request.TARGET_TRAIN
                .VENUE = Request.VENUE
                .REQUEST_SENDER_ID = Request.REQUEST_SENDER_ID
                .REQUEST_DATE = Request.REQUEST_DATE
                .ATTACH_FILE = Request.ATTACH_FILE
                .REMARK = Request.REMARK
            End With
            Context.TR_REQUEST.AddObject(objRequest)

            If lstEmp IsNot Nothing Then
                For Each item In lstEmp
                    Dim objProgramEmpData As New TR_REQUEST_EMPLOYEE
                    objProgramEmpData.ID = Utilities.GetNextSequence(Context, Context.TR_REQUEST_EMPLOYEE.EntitySet.Name)
                    objProgramEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    objProgramEmpData.TR_REQUEST_ID = objRequest.ID
                    Context.TR_REQUEST_EMPLOYEE.AddObject(objProgramEmpData)
                Next
            End If

            If Request.lstCenters IsNot Nothing Then
                For Each item In Request.lstCenters
                    Dim objProgramCenterData As New TR_REQUEST_CENTER
                    objProgramCenterData.ID = Utilities.GetNextSequence(Context, Context.TR_REQUEST_CENTER.EntitySet.Name)
                    objProgramCenterData.TR_REQUEST_ID = objRequest.ID
                    objProgramCenterData.CENTER_ID = item.ID
                    Context.TR_REQUEST_CENTER.AddObject(objProgramCenterData)
                Next
            End If

            If Request.lstTeachers IsNot Nothing Then
                For Each item In Request.lstTeachers
                    Dim objProgramTeacherData As New TR_REQUEST_TEACHER
                    objProgramTeacherData.ID = Utilities.GetNextSequence(Context, Context.TR_REQUEST_TEACHER.EntitySet.Name)
                    objProgramTeacherData.TR_REQUEST_ID = objRequest.ID
                    objProgramTeacherData.TEACHER_ID = item.ID
                    Context.TR_REQUEST_TEACHER.AddObject(objProgramTeacherData)
                Next
            End If

            Context.SaveChanges(log)
            gID = objRequest.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertRequest")
            Throw ex
        End Try
    End Function

    Public Function ModifyRequest(ByVal Request As RequestDTO,
                                  ByVal lstEmp As List(Of RequestEmpDTO),
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objRequest As New TR_REQUEST With {.ID = Request.ID}
        Try
            Context.TR_REQUEST.Attach(objRequest)
            With objRequest
                .TR_PLAN_ID = Request.TR_PLAN_ID
                .ORG_ID = Request.ORG_ID
                .YEAR = Request.YEAR
                .IS_IRREGULARLY = Request.IS_IRREGULARLY
                .TR_COURSE_ID = Request.COURSE_ID
                .TRAIN_FORM_ID = Request.TRAIN_FORM_ID
                .PROPERTIES_NEED_ID = Request.PROPERTIES_NEED_ID
                .EXPECTED_DATE = Request.EXPECTED_DATE
                .START_DATE = Request.START_DATE
                .CONTENT = Request.CONTENT
                .TR_UNIT_ID = Request.UNIT_ID
                .EXPECTED_COST = Request.EXPECTED_COST
                .TR_CURRENCY_ID = Request.TR_CURRENCY_ID
                .STATUS_ID = TrainingCommon.TR_REQUEST_STATUS.WAIT_ID
                .TARGET_TRAIN = Request.TARGET_TRAIN
                .VENUE = Request.VENUE
                .REQUEST_SENDER_ID = Request.REQUEST_SENDER_ID
                .REQUEST_DATE = Request.REQUEST_DATE
                .ATTACH_FILE = Request.ATTACH_FILE
                .REMARK = Request.REMARK
            End With

            Dim lstRegEmp = (From p In Context.TR_REQUEST_EMPLOYEE Where p.TR_REQUEST_ID = Request.ID).ToList
            For Each item In lstRegEmp
                Context.TR_REQUEST_EMPLOYEE.DeleteObject(item)
            Next
            If lstEmp IsNot Nothing Then
                For Each item In lstEmp
                    Dim objProgramEmpData As New TR_REQUEST_EMPLOYEE
                    objProgramEmpData.ID = Utilities.GetNextSequence(Context, Context.TR_REQUEST_EMPLOYEE.EntitySet.Name)
                    objProgramEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    objProgramEmpData.TR_REQUEST_ID = objRequest.ID
                    Context.TR_REQUEST_EMPLOYEE.AddObject(objProgramEmpData)
                Next
            End If

            Dim lstRegCen = (From p In Context.TR_REQUEST_CENTER Where p.TR_REQUEST_ID = Request.ID).ToList
            For Each item In lstRegCen
                Context.TR_REQUEST_CENTER.DeleteObject(item)
            Next
            If Request.lstCenters IsNot Nothing Then
                For Each item In Request.lstCenters
                    Dim objProgramCenterData As New TR_REQUEST_CENTER
                    objProgramCenterData.ID = Utilities.GetNextSequence(Context, Context.TR_REQUEST_CENTER.EntitySet.Name)
                    objProgramCenterData.TR_REQUEST_ID = objRequest.ID
                    objProgramCenterData.CENTER_ID = item.ID
                    Context.TR_REQUEST_CENTER.AddObject(objProgramCenterData)
                Next
            End If

            Dim lstRegTea = (From p In Context.TR_REQUEST_TEACHER Where p.TR_REQUEST_ID = Request.ID).ToList
            For Each item In lstRegTea
                Context.TR_REQUEST_TEACHER.DeleteObject(item)
            Next
            If Request.lstTeachers IsNot Nothing Then
                For Each item In Request.lstTeachers
                    Dim objProgramTeacherData As New TR_REQUEST_TEACHER
                    objProgramTeacherData.ID = Utilities.GetNextSequence(Context, Context.TR_REQUEST_TEACHER.EntitySet.Name)
                    objProgramTeacherData.TR_REQUEST_ID = objRequest.ID
                    objProgramTeacherData.TEACHER_ID = item.ID
                    Context.TR_REQUEST_TEACHER.AddObject(objProgramTeacherData)
                Next
            End If

            Context.SaveChanges(log)
            gID = objRequest.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyRequest")
            Throw ex
        End Try

    End Function

    Public Function UpdateStatusTrainingRequests(ByVal lstID As List(Of Decimal), ByVal status As Decimal) As Boolean
        Try
            Dim lstRequest = (From p In Context.TR_REQUEST Where lstID.Contains(p.ID)).ToList
            For Each item In lstRequest
                item.STATUS_ID = status
            Next

            'Context.SaveChanges()
            'Return True

            If status = TrainingCommon.TR_REQUEST_STATUS.APPROVE_ID Then
                For Each item In lstRequest
                    Dim objProgram As ProgramDTO
                    If item.TR_PLAN_ID IsNot Nothing Then
                        objProgram = (From p In Context.TR_PLAN
                                    From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                                    Where p.ID = item.TR_PLAN_ID
                                    Select New ProgramDTO With {.YEAR = p.YEAR,
                                                                .DURATION = p.DURATION,
                                                                .NAME = p.NAME,
                                                                .ORG_ID = p.ORG_ID,
                                                                .ORG_NAME = org.NAME_VN,
                                                                .REMARK = p.REMARK,
                                                                .TARGET_TRAIN = p.TARGET_TRAIN,
                                                                .TR_COURSE_ID = p.TR_COURSE_ID,
                                                                .TR_CURRENCY_ID = p.TR_CURRENCY_ID,
                                                                .TR_DURATION_UNIT_ID = p.TR_DURATION_UNIT_ID,
                                                                .VENUE = p.VENUE,
                                                                .Departments_NAME = p.DEPARTMENTS,
                                                                .Titles_NAME = p.TITLES,
                                                                .Centers_NAME = p.CENTERS,
                                                                .TR_PLAN_ID = p.ID}).FirstOrDefault
                    Else
                        objProgram = New ProgramDTO
                        objProgram.YEAR = item.YEAR
                        objProgram.ORG_ID = item.ORG_ID
                        objProgram.REMARK = item.REMARK
                        objProgram.TARGET_TRAIN = item.TARGET_TRAIN
                        objProgram.TR_COURSE_ID = item.TR_COURSE_ID
                        objProgram.TR_CURRENCY_ID = item.TR_CURRENCY_ID
                        objProgram.VENUE = item.VENUE
                    End If
                    

                    With objProgram
                        .IS_REIMBURSE = 0
                        '.IS_LOCAL = 0
                        .TR_REQUEST_ID = item.ID

                    End With

                    objProgram.Units = (From p In Context.TR_PLAN_UNIT
                                     From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                                     Where p.TR_PLAN_ID = item.TR_PLAN_ID
                                     Select New ProgramOrgDTO With {.ID = p.ORG_ID,
                                                             .NAME = o.NAME_VN}).ToList

                    objProgram.Titles = (From p In Context.TR_PLAN_TITLE
                                      From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                                      Where p.TR_PLAN_ID = item.TR_PLAN_ID
                                      Select New ProgramTitleDTO With {.ID = p.TITLE_ID,
                                                                .NAME = t.NAME_VN}).ToList

                    objProgram.Centers = (From p In Context.TR_PLAN_CENTER
                                       From c In Context.TR_CENTER.Where(Function(f) f.ID = p.CENTER_ID)
                                       Where p.TR_PLAN_ID = item.TR_PLAN_ID
                                       Select New ProgramCenterDTO With {.ID = p.CENTER_ID,
                                                                  .NAME_EN = c.NAME_EN,
                                                                  .NAME_VN = c.NAME_VN}).ToList

                    objProgram.Lectures = New List(Of ProgramLectureDTO)
                    InsertProgram(objProgram, Nothing, 0, False)
                Next
            End If
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateStatusTrainingRequests")
            Throw ex
        End Try
    End Function

    Public Function DeleteTrainingRequests(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim deletedRequestEmployee = (From record In Context.TR_REQUEST_EMPLOYEE Where lstID.Contains(record.TR_REQUEST_ID))
            For Each item In deletedRequestEmployee
                Context.TR_REQUEST_EMPLOYEE.DeleteObject(item)
            Next

            Dim deletedRequestCenter = (From record In Context.TR_REQUEST_CENTER Where lstID.Contains(record.TR_REQUEST_ID))
            For Each item In deletedRequestCenter
                Context.TR_REQUEST_CENTER.DeleteObject(item)
            Next

            Dim deletedRequestTeacher = (From record In Context.TR_REQUEST_TEACHER Where lstID.Contains(record.TR_REQUEST_ID))
            For Each item In deletedRequestTeacher
                Context.TR_REQUEST_TEACHER.DeleteObject(item)
            Next

            Dim deletedRequests = (From record In Context.TR_REQUEST Where lstID.Contains(record.ID))
            For Each item In deletedRequests
                Context.TR_REQUEST.DeleteObject(item)
            Next

            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTrainingRequests")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeByPlanID(ByVal filter As RequestDTO) As List(Of RequestEmpDTO)
        Try
            Dim lst As List(Of RequestEmpDTO) = New List(Of RequestEmpDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_TRAINING.GET_EMPLOYEE_BY_PLANID",
                                           New With {.P_PLAN_ID = filter.TR_PLAN_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    'For Each row As DataRow In dtData.Rows
                    '    lst.Add(New RequestEmpDTO With {.EMPLOYEE_ID = row("EMPLOYEE_ID").ToString(),
                    '                                  .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString(),
                    '                                  .EMPLOYEE_NAME = row("EMPLOYEE_NAME").ToString(),
                    '                                  .BIRTH_DATE = row("BIRTH_DATE").ToString(),
                    '                                  .TITLE_NAME = row("TITLE_NAME").ToString(),
                    '                                  .COM_NAME = row("COM_NAME").ToString(),
                    '                                  .ORG_NAME = row("ORG_NAME").ToString(),
                    '                                  .WORK_INVOLVE = row("WORK_INVOLVE").ToString()})
                    'Next
                    lst = (From row As DataRow In dtData.Rows
                       Select New RequestEmpDTO With {.EMPLOYEE_ID = row("EMPLOYEE_ID").ToString(),
                                                      .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString(),
                                                      .EMPLOYEE_NAME = row("EMPLOYEE_NAME").ToString(),
                                                      .BIRTH_DATE = row("BIRTH_DATE").ToString(),
                                                      .TITLE_NAME = row("TITLE_NAME").ToString(),
                                                      .COM_NAME = row("COM_NAME").ToString(),
                                                      .ORG_NAME = row("ORG_NAME").ToString(),
                                                      .WORK_INVOLVE = row("WORK_INVOLVE").ToString()}).ToList
                End If
            End Using
            'Dim lst = (From p In Context.HU_EMPLOYEE
            '           From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
            '           From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER).DefaultIfEmpty
            '           From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
            '           From org_check In Context.TR_PLAN_UNIT.Where(Function(f) f.ORG_ID = p.ORG_ID)
            '           From title_check In Context.TR_PLAN_TITLE.Where(Function(f) f.TITLE_ID = p.TITLE_ID)
            '           From pro In Context.TR_PLAN.Where(Function(f) f.ID = org_check.TR_PLAN_ID And _
            '                                                    f.ID = title_check.TR_PLAN_ID)
            '           Where p.TER_EFFECT_DATE Is Nothing And pro.ID = filter.TR_PLAN_ID
            '           Select New RequestEmpDTO With {.EMPLOYEE_ID = p.ID,
            '                                          .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
            '                                          .EMPLOYEE_NAME = p.FULLNAME_VN,
            '                                          .BIRTH_DATE = cv.BIRTH_DATE,
            '                                          .GENDER_NAME = gender.NAME_VN,
            '                                          .TITLE_NAME = title.NAME_VN})


            Return lst.ToList

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetEmployeeByPlanID")
            Throw ex
        End Try
    End Function

    Public Function GetPlanRequestByID(ByVal Id As Decimal) As PlanDTO
        Try
            Dim objPlan As PlanDTO = (From plan In Context.TR_PLAN
                                      From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = plan.ORG_ID)
                                      Where plan.ID = Id
                                      Select New PlanDTO With {.ID = plan.ID,
                                                               .YEAR = plan.YEAR,
                                                               .COST_INCURRED = plan.COST_INCURRED,
                                                               .COST_OF_STUDENT = plan.COST_OF_STUDENT,
                                                               .COST_OF_STUDENT_USD = plan.COST_OF_STUDENT_USD,
                                                               .COST_OTHER = plan.COST_OTHER,
                                                               .COST_TOTAL = plan.COST_TOTAL,
                                                                .COST_TOTAL_USD = plan.COST_TOTAL_USD,
                                                               .COST_TRAIN = plan.COST_TRAIN,
                                                               .COST_TRAVEL = plan.COST_TRAVEL,
                                                               .DURATION = plan.DURATION,
                                                               .NAME = plan.NAME,
                                                               .ORG_ID = plan.ORG_ID,
                                                               .ORG_NAME = org.NAME_VN,
                                                               .PLAN_T1 = plan.PLAN_T1,
                                                               .PLAN_T10 = plan.PLAN_T10,
                                                               .PLAN_T11 = plan.PLAN_T11,
                                                               .PLAN_T12 = plan.PLAN_T12,
                                                               .PLAN_T2 = plan.PLAN_T2,
                                                               .PLAN_T3 = plan.PLAN_T3,
                                                               .PLAN_T4 = plan.PLAN_T4,
                                                               .PLAN_T5 = plan.PLAN_T5,
                                                               .PLAN_T6 = plan.PLAN_T6,
                                                               .PLAN_T7 = plan.PLAN_T7,
                                                               .PLAN_T8 = plan.PLAN_T8,
                                                               .PLAN_T9 = plan.PLAN_T9,
                                                               .REMARK = plan.REMARK,
                                                               .STUDENT_NUMBER = plan.STUDENT_NUMBER,
                                                               .TARGET_TRAIN = plan.TARGET_TRAIN,
                                                               .TEACHER_NUMBER = plan.TEACHER_NUMBER,
                                                               .TR_COURSE_ID = plan.TR_COURSE_ID,
                                                               .TR_CURRENCY_ID = plan.TR_CURRENCY_ID,
                                                               .TR_DURATION_UNIT_ID = plan.TR_DURATION_UNIT_ID,
                                                               .VENUE = plan.VENUE,
                                                               .Centers_NAME = plan.CENTERS,
                                                               .Departments_NAME = plan.DEPARTMENTS,
                                                               .Months_NAME = plan.MONTHS,
                                                               .TR_TRAIN_FORM_ID = plan.TRAIN_FORM_ID,
                                                               .PROPERTIES_NEED_ID = plan.PROPERTIES_NEED_ID,
                                                                    .UNIT_ID = plan.UNIT_ID,
                                                               .ATTACHFILE = plan.ATTACHFILE,
                                                               .Titles_NAME = plan.TITLES}).FirstOrDefault

            Return objPlan
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPlanRequestByID")
            Throw ex
        End Try
    End Function

#End Region

#Region "Program"

    Public Function GetPlan_Cost_Detail(ByVal Id As Decimal) As List(Of CostDetailDTO)
        Try
            Dim lst = (From p In Context.TR_PLAN_COST_DETAIL
                       Where p.PLAN_ID = Id
                       Select New CostDetailDTO With {.ID = p.ID,
                                                      .PLAN_ID = p.PLAN_ID,
                                                      .TYPE_ID = p.TYPE_ID,
                                                      .MONEY = p.MONEY,
                                                      .MONEY_TYPE = p.MONEY_TYPE,
                                                      .MODIFIED_DATE = p.MODIFIED_DATE
                                                     }).ToList

            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPlan_Cost_Detail")
            Throw ex
        End Try
    End Function
    Public Function GetProgramByChooseFormId(ByVal Id As Decimal) As ProgramDTO
        Try
            Dim idProgram = (From p In Context.TR_CHOOSE_FORM Where p.ID = Id Select p.TR_PROGRAM_ID).FirstOrDefault
            Dim objProgram As ProgramDTO = (From Program In Context.TR_PROGRAM
                                      Where Program.ID = idProgram
                                      Select New ProgramDTO With {.ID = Program.ID,
                                                                  .YEAR = Program.YEAR,
                                                                  .VENUE = Program.VENUE,
                                                                  .START_DATE = Program.START_DATE,
                                                                  .END_DATE = Program.END_DATE,
                                                                  .Centers_NAME = Program.CENTERS,
                                                                  .Lectures_NAME = Program.LECTURES}).FirstOrDefault

            Return objProgram
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgramByChooseFormId")
            Throw ex
        End Try
    End Function

    Public Function GetRequestsForProgram(ByVal ReqID As Decimal) As RequestDTO
        Try
            Dim lst As List(Of RequestDTO) = New List(Of RequestDTO)
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_TRAINING.GET_TR_REQUEST_FOR_PROGRAM",
                                           New With {.P_ID = ReqID,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CEN = cls.OUT_CURSOR,
                                                     .P_LEC = cls.OUT_CURSOR,
                                                     .P_JOI = cls.OUT_CURSOR}, False)
                Dim dtReq As DataTable = dsData.Tables(0)
                Dim dtCen As DataTable = dsData.Tables(1)
                Dim dtLec As DataTable = dsData.Tables(2)
                Dim dtEmp As DataTable = dsData.Tables(3)
                If dtReq IsNot Nothing Then
                    lst = (From row As DataRow In dtReq.Rows
                           Select New RequestDTO With {.ID = row("ID").ToString(),
                                                       .COURSE_ID = If(row("COURSE_ID") IsNot Nothing, ToDecimal(row("COURSE_ID")), Nothing),
                                                       .COURSE_NAME = row("COURSE_NAME").ToString(),
                                                       .TR_PLAN_NAME = row("PLAN_NAME").ToString(),
                                                       .TRAIN_FORM_ID = If(row("TRAIN_FORM_ID") IsNot Nothing, ToDecimal(row("TRAIN_FORM_ID")), Nothing),
                                                       .PROPERTIES_NEED_ID = If(row("PROPERTIES_NEED_ID") IsNot Nothing, ToDecimal(row("PROPERTIES_NEED_ID")), Nothing),
                                                       .PROGRAM_GROUP = row("PROGRAM_GROUP").ToString(),
                                                       .TRAIN_FIELD = row("TRAIN_FIELD").ToString(),
                                                       .CONTENT = row("CONTENT").ToString(),
                                                       .TARGET_TRAIN = row("TARGET_TRAIN").ToString(),
                                                       .VENUE = row("VENUE").ToString(),
                                                       .REMARK = row("REMARK").ToString(),
                                                       .TR_CURRENCY_NAME = row("CURRENCY_NAME").ToString()
                                                      }).ToList
                    If lst.Count > 0 Then
                        If dtCen IsNot Nothing Then
                            lst(0).lstCenters = (From row As DataRow In dtCen.Rows
                                             Select New PlanCenterDTO With {.ID = row("ID").ToString(),
                                                                            .NAME_VN = row("NAME").ToString()
                                                                           }).ToList
                        End If
                        If dtLec IsNot Nothing Then
                            lst(0).lstTeachers = (From row As DataRow In dtLec.Rows
                                                  Select New LectureDTO With {.ID = row("ID").ToString(),
                                                                              .LECTURE_NAME = row("NAME").ToString()
                                                                             }).ToList
                        End If
                        If dtEmp IsNot Nothing Then
                            lst(0).NUM_EMP = dtEmp.Rows.Count

                            lst(0).lstOrgs = (From row As DataRow In dtEmp.Rows
                                              Select New PlanOrgDTO With {.ID = ToDecimal(row("ORG_ID")),
                                                                          .NAME = row("ORG_NAME").ToString()
                                                                         }).ToList
                            lst(0).lstOrgs = lst(0).lstOrgs.GroupBy(Function(x) x.ID).Select(Function(y) y.First).ToList

                            lst(0).lstTits = (From row As DataRow In dtEmp.Rows
                                              Select New PlanTitleDTO With {.ID = ToDecimal(row("TIT_ID")),
                                                                            .NAME = row("TIT_NAME").ToString()
                                                                           }).ToList
                            lst(0).lstTits = lst(0).lstTits.GroupBy(Function(x) x.ID).Select(Function(y) y.First).ToList

                            lst(0).lstWIs = (From row As DataRow In dtEmp.Rows
                                             Select New PlanTitleDTO With {.ID = ToDecimal(row("WI_ID")),
                                                                           .NAME = row("WI_NAME").ToString()
                                                                          }).ToList
                            lst(0).lstWIs = lst(0).lstWIs.GroupBy(Function(x) x.ID).Select(Function(y) y.First).ToList
                        End If
                    End If

                    If lst.Count > 0 Then
                        Return lst(0)
                    End If
                End If
            End Using
            Return Nothing
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".LoadTrainingRequests")
            Throw ex
        End Try
    End Function

    Public Function GetPrograms(ByVal filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = If(_param.IS_DISSOLVE = "False", "0", "1")})
            End Using

            Dim query = From Program In Context.TR_PROGRAM
                        From Request In Context.TR_REQUEST.Where(Function(f) f.ID = Program.TR_REQUEST_ID).DefaultIfEmpty
                        From Plan In Context.TR_PLAN.Where(Function(f) f.ID = Request.TR_PLAN_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = Program.ORG_ID).DefaultIfEmpty
                        From cou In Context.TR_COURSE.Where(Function(f) f.ID = Program.TR_COURSE_ID).DefaultIfEmpty
                        From group In Context.TR_PROGRAM_GROUP.Where(Function(f) f.ID = cou.TR_PROGRAM_GROUP).DefaultIfEmpty
                        From field In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cou.TR_TRAIN_FIELD).DefaultIfEmpty
                        From form In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TRAIN_FORM_ID).DefaultIfEmpty
                        From proned In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.PROPERTIES_NEED_ID).DefaultIfEmpty
                        From duruni In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TR_DURATION_UNIT_ID).DefaultIfEmpty
                        From durstu In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.DURATION_STUDY_ID).DefaultIfEmpty
                        From lang In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TR_LANGUAGE_ID).DefaultIfEmpty
                        From unit In Context.TR_UNIT.Where(Function(f) f.ID = Program.TR_UNIT_ID).DefaultIfEmpty
                        From cure In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Request.TR_CURRENCY_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = Program.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Select New ProgramDTO With {.ID = Program.ID,
                                                    .TR_REQUEST_ID = Program.TR_REQUEST_ID,
                                                    .TR_COURSE_ID = Program.TR_COURSE_ID,
                                                    .TR_COURSE_NAME = cou.NAME,
                                                    .YEAR = Program.YEAR,
                                                    .ORG_ID = Program.ORG_ID,
                                                    .ORG_NAME = org.NAME_VN,
                                                    .NAME = Program.NAME,
                                                    .PROGRAM_GROUP = group.NAME,
                                                    .TRAIN_FIELD = field.NAME_VN,
                                                    .TRAIN_FORM_ID = Program.TRAIN_FORM_ID,
                                                    .TRAIN_FORM_NAME = form.NAME_VN,
                                                    .PROPERTIES_NEED_ID = Program.PROPERTIES_NEED_ID,
                                                    .PROPERTIES_NEED_NAME = proned.NAME_VN,
                                                    .DURATION = Program.DURATION,
                                                    .TR_DURATION_UNIT_ID = Program.TR_DURATION_UNIT_ID,
                                                    .TR_DURATION_UNIT_NAME = duruni.NAME_VN,
                                                    .START_DATE = Program.START_DATE,
                                                    .END_DATE = Program.END_DATE,
                                                    .DURATION_STUDY_ID = Program.DURATION_STUDY_ID,
                                                    .DURATION_STUDY_NAME = durstu.NAME_VN,
                                                    .DURATION_HC = Program.DURATION_HC,
                                                    .DURATION_OT = Program.DURATION_OT,
                                                    .IS_REIMBURSE = Program.IS_REIMBURSE,
                                                    .IS_RETEST = Program.IS_RETEST,
                                                    .TR_CURRENCY_NAME = cure.NAME_VN,
                                                    .PLAN_STUDENT_NUMBER = Plan.STUDENT_NUMBER,
                                                    .PLAN_COST_TOTAL_US = Plan.COST_TOTAL_USD,
                                                    .PLAN_COST_TOTAL = Plan.COST_TOTAL,
                                                    .STUDENT_NUMBER = Program.STUDENT_NUMBER,
                                                    .COST_TOTAL_US = Program.COST_TOTAL_US,
                                                    .COST_STUDENT_US = Program.COST_STUDENT_US,
                                                    .COST_TOTAL = Program.COST_TOTAL,
                                                    .COST_STUDENT = Program.COST_STUDENT,
                                                    .Departments_NAME = Program.DEPARTMENTS,
                                                    .Titles_NAME = Program.TITLES,
                                                    .TR_LANGUAGE_ID = Program.TR_LANGUAGE_ID,
                                                    .TR_LANGUAGE_NAME = lang.NAME_VN,
                                                    .TR_UNIT_ID = Program.TR_UNIT_ID,
                                                    .TR_UNIT_NAME = unit.NAME,
                                                    .Centers_NAME = Program.CENTERS,
                                                    .Lectures_NAME = Program.LECTURES,
                                                    .CONTENT = Program.CONTENT,
                                                    .TARGET_TRAIN = Program.TARGET_TRAIN,
                                                    .VENUE = Program.VENUE,
                                                    .REMARK = Program.REMARK,
                                                    .CREATED_DATE = Program.CREATED_DATE}

            Dim lst = query
            If filter.START_DATE.HasValue Then
                lst = lst.Where(Function(p) p.START_DATE >= filter.START_DATE)
            End If
            If filter.END_DATE.HasValue Then
                lst = lst.Where(Function(p) p.END_DATE <= filter.END_DATE)
            End If
            'If filter.NAME <> "" Then
            '    lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(filter.NAME.ToUpper))
            'End If
            'If filter.ORG_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(filter.ORG_NAME.ToUpper))
            'End If
            'If filter.TR_COURSE_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.TR_COURSE_NAME.ToUpper.Contains(filter.TR_COURSE_NAME.ToUpper))
            'End If
            'If filter.TR_TRAIN_ENTRIES_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.TR_TRAIN_ENTRIES_NAME.ToUpper.Contains(filter.TR_TRAIN_ENTRIES_NAME.ToUpper))
            'End If
            'If filter.TR_DURATION_UNIT_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.TR_DURATION_UNIT_NAME.ToUpper.Contains(filter.TR_DURATION_UNIT_NAME.ToUpper))
            'End If
            'If filter.DURATION.HasValue Then
            '    lst = lst.Where(Function(p) p.DURATION = filter.DURATION)
            'End If
            'If filter.CURRENCY <> "" Then
            '    lst = lst.Where(Function(p) p.CURRENCY.ToUpper.Contains(filter.CURRENCY.ToUpper))
            'End If
            'If filter.TARGET_TRAIN <> "" Then
            '    lst = lst.Where(Function(p) p.TARGET_TRAIN.ToUpper.Contains(filter.TARGET_TRAIN.ToUpper))
            'End If
            'If filter.VENUE <> "" Then
            '    lst = lst.Where(Function(p) p.VENUE.ToUpper.Contains(filter.VENUE.ToUpper))
            'End If
            'If filter.REMARK <> "" Then
            '    lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(filter.REMARK.ToUpper))
            'End If
            'If filter.Departments_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.Departments_NAME.ToUpper.Contains(filter.Departments_NAME.ToUpper))
            'End If
            'If filter.Titles_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.Titles_NAME.ToUpper.Contains(filter.Titles_NAME.ToUpper))
            'End If
            'If filter.Lectures_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.Lectures_NAME.ToUpper.Contains(filter.Lectures_NAME.ToUpper))
            'End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            ' Return Nothing
            Return lst.ToList()
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPrograms")
            Throw ex
        End Try
    End Function

    Public Function GetProgramById(ByVal Id As Decimal) As ProgramDTO
        Try
            Dim objProgram As ProgramDTO = (From Program In Context.TR_PROGRAM
                                            From Request In Context.TR_REQUEST.Where(Function(f) f.ID = Program.TR_REQUEST_ID).DefaultIfEmpty
                                            From Plan In Context.TR_PLAN.Where(Function(f) f.ID = Request.TR_PLAN_ID).DefaultIfEmpty
                                            From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = Program.ORG_ID).DefaultIfEmpty
                                            From cou In Context.TR_COURSE.Where(Function(f) f.ID = Program.TR_COURSE_ID).DefaultIfEmpty
                                            From group In Context.TR_PROGRAM_GROUP.Where(Function(f) f.ID = cou.TR_PROGRAM_GROUP).DefaultIfEmpty
                                            From field In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cou.TR_TRAIN_FIELD).DefaultIfEmpty
                                            From form In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TRAIN_FORM_ID).DefaultIfEmpty
                                            From proned In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.PROPERTIES_NEED_ID).DefaultIfEmpty
                                            From duruni In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TR_DURATION_UNIT_ID).DefaultIfEmpty
                                            From durstu In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.DURATION_STUDY_ID).DefaultIfEmpty
                                            From lang In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TR_LANGUAGE_ID).DefaultIfEmpty
                                            From unit In Context.TR_UNIT.Where(Function(f) f.ID = Program.TR_UNIT_ID).DefaultIfEmpty
                                            From cure In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Request.TR_CURRENCY_ID).DefaultIfEmpty
                                            Where Program.ID = Id
                                            Select New ProgramDTO With {.ID = Program.ID,
                                                                        .TR_REQUEST_ID = Program.TR_REQUEST_ID,
                                                                        .TR_COURSE_ID = Program.TR_COURSE_ID,
                                                                        .TR_COURSE_NAME = cou.NAME,
                                                                        .YEAR = Program.YEAR,
                                                                        .ORG_ID = Program.ORG_ID,
                                                                        .ORG_NAME = org.NAME_VN,
                                                                        .NAME = Program.NAME,
                                                                        .PROGRAM_GROUP = group.NAME,
                                                                        .TRAIN_FIELD = field.NAME_VN,
                                                                        .TRAIN_FORM_ID = Program.TRAIN_FORM_ID,
                                                                        .TRAIN_FORM_NAME = form.NAME_VN,
                                                                        .PROPERTIES_NEED_ID = Program.PROPERTIES_NEED_ID,
                                                                        .PROPERTIES_NEED_NAME = proned.NAME_VN,
                                                                        .DURATION = Program.DURATION,
                                                                        .TR_DURATION_UNIT_ID = Program.TR_DURATION_UNIT_ID,
                                                                        .TR_DURATION_UNIT_NAME = duruni.NAME_VN,
                                                                        .START_DATE = Program.START_DATE,
                                                                        .END_DATE = Program.END_DATE,
                                                                        .DURATION_STUDY_ID = Program.DURATION_STUDY_ID,
                                                                        .DURATION_STUDY_NAME = durstu.NAME_VN,
                                                                        .DURATION_HC = Program.DURATION_HC,
                                                                        .DURATION_OT = Program.DURATION_OT,
                                                                        .IS_REIMBURSE = Program.IS_REIMBURSE,
                                                                        .IS_RETEST = Program.IS_RETEST,
                                                                        .TR_CURRENCY_NAME = cure.NAME_VN,
                                                                        .PLAN_STUDENT_NUMBER = Plan.STUDENT_NUMBER,
                                                                        .PLAN_COST_TOTAL_US = Plan.COST_TOTAL_USD,
                                                                        .PLAN_COST_TOTAL = Plan.COST_TOTAL,
                                                                        .STUDENT_NUMBER = Program.STUDENT_NUMBER,
                                                                        .COST_TOTAL_US = Program.COST_TOTAL_US,
                                                                        .COST_STUDENT_US = Program.COST_STUDENT_US,
                                                                        .COST_TOTAL = Program.COST_TOTAL,
                                                                        .COST_STUDENT = Program.COST_STUDENT,
                                                                        .Departments_NAME = Program.DEPARTMENTS,
                                                                        .Titles_NAME = Program.TITLES,
                                                                        .TR_LANGUAGE_ID = Program.TR_LANGUAGE_ID,
                                                                        .TR_LANGUAGE_NAME = lang.NAME_VN,
                                                                        .TR_UNIT_ID = Program.TR_UNIT_ID,
                                                                        .TR_UNIT_NAME = unit.NAME,
                                                                        .Centers_NAME = Program.CENTERS,
                                                                        .Lectures_NAME = Program.LECTURES,
                                                                        .CONTENT = Program.CONTENT,
                                                                        .TARGET_TRAIN = Program.TARGET_TRAIN,
                                                                        .VENUE = Program.VENUE,
                                                                        .REMARK = Program.REMARK,
                                                                        .CREATED_DATE = Program.CREATED_DATE}).FirstOrDefault

            objProgram.Costs = (From p In Context.TR_PROGRAM_COST
                                From org In Context.HU_ORGANIZATION.Where(Function(o) o.ID = p.ORG_ID).DefaultIfEmpty
                                Where p.TR_PROGRAM_ID = Id
                                Select New ProgramCostDTO With {.ID = p.ID,
                                                                .TR_PROGRAM_ID = p.TR_PROGRAM_ID,
                                                                .ORG_ID = p.ORG_ID,
                                                                .ORG_NAME = org.NAME_VN,
                                                                .COST_COMPANY = p.COST_COMPANY}).ToList

            objProgram.Units = (From p In Context.TR_PROGRAM_UNIT
                                From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                                Where p.TR_PROGRAM_ID = Id
                                Select New ProgramOrgDTO With {.ID = p.ORG_ID,
                                                               .NAME = o.NAME_VN}).ToList

            objProgram.Titles = (From p In Context.TR_PROGRAM_TITLE
                                 From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                                 Where p.TR_PROGRAM_ID = Id
                                 Select New ProgramTitleDTO With {.ID = p.TITLE_ID,
                                                                  .NAME = t.NAME_VN}).ToList

            objProgram.WIs = (From p In Context.TR_PROGRAM_TITLE
                              From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                              From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = t.WORK_INVOLVE_ID)
                              Where p.TR_PROGRAM_ID = Id
                              Select New ProgramTitleDTO With {.ID = p.TITLE_ID,
                                                               .NAME = t.NAME_VN}).ToList

            objProgram.Centers = (From p In Context.TR_PROGRAM_CENTER
                                  From c In Context.TR_CENTER.Where(Function(f) f.ID = p.TR_CENTER_ID)
                                  Where p.TR_PROGRAM_ID = Id
                                  Select New ProgramCenterDTO With {.ID = p.TR_CENTER_ID,
                                                                    .NAME_EN = c.NAME_EN,
                                                                    .NAME_VN = c.NAME_VN}).ToList

            objProgram.Lectures = (From p In Context.TR_PROGRAM_LECTURE
                                   From c In Context.TR_LECTURE.Where(Function(f) f.ID = p.TR_LECTURE_ID)
                                   From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = c.LECTURE_ID).DefaultIfEmpty
                                   Where p.TR_PROGRAM_ID = Id
                                   Select New ProgramLectureDTO With {.ID = c.ID,
                                                                      .NAME = If(c.LECTURE_ID IsNot Nothing,
                                                                                 emp.EMPLOYEE_CODE & " - " & emp.FULLNAME_VN,
                                                                                 c.CODE & " - " & c.NAME)}).ToList

            'Dim lstCost = (From p In Context.TR_PROGRAM_COST Where p.TR_PROGRAM_ID = objProgram.ID).ToList
            'If lstCost.Count > 0 Then
            '    Dim costCompany As Decimal = 0
            '    Dim numberStudent As Decimal = 0
            '    Dim costOfStudent As Decimal = 0
            '    For Each item In lstCost
            '        If item.COST_COMPANY IsNot Nothing Then
            '            costCompany += item.COST_COMPANY
            '        End If
            '        If item.STUDENT_NUMBER IsNot Nothing Then
            '            numberStudent += item.STUDENT_NUMBER
            '        End If
            '    Next
            '    objProgram.COST_TOTAL = costCompany
            '    objProgram.STUDENT_NUMBER = numberStudent
            '    If numberStudent <> 0 Then
            '        costOfStudent = Decimal.Round(costCompany / numberStudent, 2)
            '    Else
            '        costOfStudent = 0
            '    End If
            '    objProgram.COST_STUDENT = costOfStudent
            '    Dim objCommit = (From p In Context.TR_COMMIT_AFTER_TRAIN
            '                     Where costOfStudent >= p.COST_TRAIN_FROM And _
            '                     ((p.COST_TRAIN_TO.HasValue And costOfStudent <= p.COST_TRAIN_TO) Or
            '                     (Not p.COST_TRAIN_TO.HasValue))
            '                     Order By p.CREATED_DATE Descending).FirstOrDefault

            '    If objCommit IsNot Nothing Then
            '        objProgram.COMMIT_WORK = objCommit.COMMIT_WORK
            '    End If
            'End If
            Return objProgram
            Return Nothing
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgramById")
            Throw ex
        End Try
    End Function

    Public Function InsertProgram(ByVal Program As ProgramDTO, ByVal log As UserLog, ByRef gID As Decimal,
                                 Optional ByVal isInsert As Boolean = True) As Boolean
        Dim objProgram As New TR_PROGRAM
        Try
            With objProgram
                .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM.EntitySet.Name)
                .TR_REQUEST_ID = Program.TR_REQUEST_ID
                .TR_COURSE_ID = Program.TR_COURSE_ID
                .YEAR = Program.YEAR
                .ORG_ID = Program.ORG_ID
                .NAME = Program.NAME
                .TRAIN_FORM_ID = Program.TRAIN_FORM_ID
                .PROPERTIES_NEED_ID = Program.PROPERTIES_NEED_ID
                .DURATION = Program.DURATION
                .TR_DURATION_UNIT_ID = Program.TR_DURATION_UNIT_ID
                .START_DATE = Program.START_DATE
                .END_DATE = Program.END_DATE
                .DURATION_STUDY_ID = Program.DURATION_STUDY_ID
                .DURATION_HC = Program.DURATION_HC
                .DURATION_OT = Program.DURATION_OT
                .IS_REIMBURSE = Program.IS_REIMBURSE
                .IS_RETEST = Program.IS_RETEST
                .STUDENT_NUMBER = Program.STUDENT_NUMBER
                .COST_TOTAL_US = Program.COST_TOTAL_US
                .COST_STUDENT_US = Program.COST_STUDENT_US
                .COST_TOTAL = Program.COST_TOTAL
                .COST_STUDENT = Program.COST_STUDENT
                .DEPARTMENTS = Program.Departments_NAME
                .TITLES = Program.Titles_NAME
                .TR_LANGUAGE_ID = Program.TR_LANGUAGE_ID
                .TR_UNIT_ID = Program.TR_UNIT_ID
                .CENTERS = Program.Centers_NAME
                .LECTURES = Program.Lectures_NAME
                .CONTENT = Program.CONTENT
                .TARGET_TRAIN = Program.TARGET_TRAIN
                .VENUE = Program.VENUE
                .REMARK = Program.REMARK
            End With
            gID = objProgram.ID
            Context.TR_PROGRAM.AddObject(objProgram)


            '.Costs = lstOrgCost
            '.Units = (From item In lstPartDepts.Items Select New ProgramOrgDTO With {.ID = item.Value}).ToList()
            '.TITLES = (From item In lstPositions.CheckedItems Select New ProgramTitleDTO With {.ID = item.Value}).ToList()
            '.CENTERS = (From item In lstCenter.CheckedItems Select New ProgramCenterDTO With {.ID = item.Value}).ToList()
            '.LECTURES = (From item In lstLecture.CheckedItems Select New ProgramLectureDTO With {.ID = item.Value}).ToList()

            If Program.Costs IsNot Nothing Then
                For Each Cost As ProgramCostDTO In Program.Costs
                    Context.TR_PROGRAM_COST.AddObject(New TR_PROGRAM_COST With {
                                                   .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_COST.EntitySet.Name),
                                                   .TR_PROGRAM_ID = objProgram.ID,
                                                   .ORG_ID = Cost.ORG_ID,
                                                   .COST_COMPANY = Cost.COST_COMPANY})
                Next
            End If

            If Program.Units IsNot Nothing Then
                For Each unit In Program.Units
                    Context.TR_PROGRAM_UNIT.AddObject(New TR_PROGRAM_UNIT With {
                                                   .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_UNIT.EntitySet.Name),
                                                   .TR_PROGRAM_ID = objProgram.ID,
                                                   .ORG_ID = unit.ID})
                Next
            End If

            If Program.Titles IsNot Nothing Then
                For Each title In Program.Titles
                    Context.TR_PROGRAM_TITLE.AddObject(New TR_PROGRAM_TITLE With {
                                                    .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_TITLE.EntitySet.Name),
                                                    .TR_PROGRAM_ID = objProgram.ID,
                                                    .TITLE_ID = title.ID})
                Next
            End If

            If Program.Centers IsNot Nothing Then
                For Each center In Program.Centers
                    Context.TR_PROGRAM_CENTER.AddObject(New TR_PROGRAM_CENTER With {
                                                     .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_CENTER.EntitySet.Name),
                                                     .TR_PROGRAM_ID = objProgram.ID,
                                                     .TR_CENTER_ID = center.ID})
                Next
            End If

            If Program.Lectures IsNot Nothing Then
                For Each lec In Program.Lectures
                    Context.TR_PROGRAM_LECTURE.AddObject(New TR_PROGRAM_LECTURE With {
                                                     .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_LECTURE.EntitySet.Name),
                                                     .TR_PROGRAM_ID = objProgram.ID,
                                                     .TR_LECTURE_ID = lec.ID})
                Next
            End If

            If isInsert Then
                Context.SaveChanges(log)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertProgram")
            Throw ex
        End Try
    End Function

    Public Function ModifyProgram(ByVal Program As ProgramDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objProgram As New TR_PROGRAM With {.ID = Program.ID}
        Try
            Context.TR_PROGRAM.Attach(objProgram)
            With objProgram
                .TR_REQUEST_ID = Program.TR_REQUEST_ID
                .TR_COURSE_ID = Program.TR_COURSE_ID
                .YEAR = Program.YEAR
                .ORG_ID = Program.ORG_ID
                .NAME = Program.NAME
                .TRAIN_FORM_ID = Program.TRAIN_FORM_ID
                .PROPERTIES_NEED_ID = Program.PROPERTIES_NEED_ID
                .DURATION = Program.DURATION
                .TR_DURATION_UNIT_ID = Program.TR_DURATION_UNIT_ID
                .START_DATE = Program.START_DATE
                .END_DATE = Program.END_DATE
                .DURATION_STUDY_ID = Program.DURATION_STUDY_ID
                .DURATION_HC = Program.DURATION_HC
                .DURATION_OT = Program.DURATION_OT
                .IS_REIMBURSE = Program.IS_REIMBURSE
                .IS_RETEST = Program.IS_RETEST
                .STUDENT_NUMBER = Program.STUDENT_NUMBER
                .COST_TOTAL_US = Program.COST_TOTAL_US
                .COST_STUDENT_US = Program.COST_STUDENT_US
                .COST_TOTAL = Program.COST_TOTAL
                .COST_STUDENT = Program.COST_STUDENT
                .DEPARTMENTS = Program.Departments_NAME
                .TITLES = Program.Titles_NAME
                .TR_LANGUAGE_ID = Program.TR_LANGUAGE_ID
                .TR_UNIT_ID = Program.TR_UNIT_ID
                .CENTERS = Program.Centers_NAME
                .LECTURES = Program.Lectures_NAME
                .CONTENT = Program.CONTENT
                .TARGET_TRAIN = Program.TARGET_TRAIN
                .VENUE = Program.VENUE
                .REMARK = Program.REMARK
            End With

            Dim oldCosts = From i In Context.TR_PROGRAM_COST Where i.TR_PROGRAM_ID = Program.ID
            For Each cost In oldCosts
                Context.TR_PROGRAM_COST.DeleteObject(cost)
            Next
            If Program.Costs IsNot Nothing Then
                For Each Cost As ProgramCostDTO In Program.Costs
                    Context.TR_PROGRAM_COST.AddObject(New TR_PROGRAM_COST With {
                                                   .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_COST.EntitySet.Name),
                                                   .TR_PROGRAM_ID = objProgram.ID,
                                                   .ORG_ID = Cost.ORG_ID,
                                                   .COST_COMPANY = Cost.COST_COMPANY})
                Next
            End If

            Dim oldUnits = From i In Context.TR_PROGRAM_UNIT Where i.TR_PROGRAM_ID = Program.ID
            For Each unit In oldUnits
                Context.TR_PROGRAM_UNIT.DeleteObject(unit)
            Next
            If Program.Units IsNot Nothing Then
                For Each unit In Program.Units
                    Context.TR_PROGRAM_UNIT.AddObject(New TR_PROGRAM_UNIT With {
                                                      .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_UNIT.EntitySet.Name),
                                                      .TR_PROGRAM_ID = objProgram.ID,
                                                      .ORG_ID = unit.ID})
                Next
            End If

            Dim oldTItles = From i In Context.TR_PROGRAM_TITLE Where i.TR_PROGRAM_ID = Program.ID
            For Each item In oldTItles
                Context.TR_PROGRAM_TITLE.DeleteObject(item)
            Next
            If Program.Titles IsNot Nothing Then
                For Each title In Program.Titles
                    Context.TR_PROGRAM_TITLE.AddObject(New TR_PROGRAM_TITLE With {
                                                       .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_TITLE.EntitySet.Name),
                                                       .TR_PROGRAM_ID = objProgram.ID,
                                                       .TITLE_ID = title.ID})
                Next
            End If

            Dim oldCenters = From i In Context.TR_PROGRAM_CENTER Where i.TR_PROGRAM_ID = Program.ID
            For Each item In oldCenters
                Context.TR_PROGRAM_CENTER.DeleteObject(item)
            Next
            If Program.Centers IsNot Nothing Then
                For Each center In Program.Centers
                    Context.TR_PROGRAM_CENTER.AddObject(New TR_PROGRAM_CENTER With
                                                        {.ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_CENTER.EntitySet.Name),
                                                         .TR_PROGRAM_ID = objProgram.ID,
                                                         .TR_CENTER_ID = center.ID})
                Next
            End If

            Dim oldLectures = From i In Context.TR_PROGRAM_LECTURE Where i.TR_PROGRAM_ID = Program.ID
            For Each item In oldLectures
                Context.TR_PROGRAM_LECTURE.DeleteObject(item)
            Next
            If Program.Lectures IsNot Nothing Then
                For Each lec In Program.Lectures
                    Context.TR_PROGRAM_LECTURE.AddObject(New TR_PROGRAM_LECTURE With {
                                                     .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_LECTURE.EntitySet.Name),
                                                     .TR_PROGRAM_ID = objProgram.ID,
                                                     .TR_LECTURE_ID = lec.ID})
                Next
            End If

            Context.SaveChanges(log)
            gID = objProgram.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyProgram")
            Throw ex
        End Try

    End Function

    Public Function DeletePrograms(ByVal lstId As List(Of Decimal)) As Boolean
        Try
            Dim proPrepare = (From p In Context.TR_PREPARE Where lstId.Contains(p.TR_PROGRAM_ID))
            Dim proClass = (From p In Context.TR_CLASS Where lstId.Contains(p.TR_PROGRAM_ID))
            Dim proCommit = (From p In Context.TR_PROGRAM_COMMIT Where lstId.Contains(p.TR_PROGRAM_ID))

            If proPrepare.Count > 0 Or proClass.Count > 0 Or proCommit.Count > 0 Then
                Return False
            End If

            Dim deletedPrograms = (From record In Context.TR_PROGRAM Where lstId.Contains(record.ID))

            Dim deletedProgramDeparment = (From record In Context.TR_PROGRAM_UNIT Where lstId.Contains(record.TR_PROGRAM_ID))
            For Each item In deletedProgramDeparment
                Context.TR_PROGRAM_UNIT.DeleteObject(item)
            Next

            Dim deletedProgramTitle = (From record In Context.TR_PROGRAM_TITLE Where lstId.Contains(record.TR_PROGRAM_ID))
            For Each item In deletedProgramTitle
                Context.TR_PROGRAM_TITLE.DeleteObject(item)
            Next

            Dim deletedProgramCenter = (From record In Context.TR_PROGRAM_CENTER Where lstId.Contains(record.TR_PROGRAM_ID))
            For Each item In deletedProgramCenter
                Context.TR_PROGRAM_CENTER.DeleteObject(item)
            Next

            Dim deletedProgramLecture = (From record In Context.TR_PROGRAM_LECTURE Where lstId.Contains(record.TR_PROGRAM_ID))
            For Each item In deletedProgramLecture
                Context.TR_PROGRAM_LECTURE.DeleteObject(item)
            Next

            Dim deletedProgramCost = (From record In Context.TR_PROGRAM_COST Where lstId.Contains(record.TR_PROGRAM_ID))
            For Each item In deletedProgramCost
                Context.TR_PROGRAM_COST.DeleteObject(item)
            Next

            For Each item In deletedPrograms
                Context.TR_PROGRAM.DeleteObject(item)
            Next

            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeletePrograms")
            Throw ex
        End Try
    End Function

    Public Function ValidateClassProgram(ByVal lstId As List(Of Decimal)) As Boolean
        Try
            Dim num As Integer
            Dim query = From p In Context.TR_PROGRAM
                        Join c In Context.TR_CLASS On p.ID Equals c.TR_PROGRAM_ID
                        Where lstId.Contains(p.ID)
            num = query.Count

            If num > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception

        End Try
    End Function

#End Region

#Region "Prepare"

    Public Function GetPrepare(ByVal _filter As ProgramPrepareDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramPrepareDTO)

        Try
            Dim query = From p In Context.TR_PREPARE
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From pre In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TR_LIST_PREPARE_ID).DefaultIfEmpty
                        Where p.TR_PROGRAM_ID = _filter.TR_PROGRAM_ID
                        Select New ProgramPrepareDTO With {
                            .ID = p.ID,
                            .EMPLOYEE_ID = p.EMPLOYEE_ID,
                            .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                            .EMPLOYEE_NAME = emp.FULLNAME_VN,
                            .TR_LIST_PREPARE_ID = p.TR_LIST_PREPARE_ID,
                            .TR_LIST_PREPARE_NAME = pre.NAME_VN,
                            .TR_PROGRAM_ID = p.TR_PROGRAM_ID,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.TR_LIST_PREPARE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_LIST_PREPARE_NAME.ToUpper.Contains(_filter.TR_LIST_PREPARE_NAME.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPrepare")
            Throw ex
        End Try

    End Function

    Public Function InsertPrepare(ByVal objPrepare As ProgramPrepareDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPrepareData As New TR_PREPARE
        Try
            objPrepareData.ID = Utilities.GetNextSequence(Context, Context.TR_PREPARE.EntitySet.Name)
            objPrepareData.EMPLOYEE_ID = objPrepare.EMPLOYEE_ID
            objPrepareData.TR_LIST_PREPARE_ID = objPrepare.TR_LIST_PREPARE_ID
            objPrepareData.TR_PROGRAM_ID = objPrepare.TR_PROGRAM_ID
            Context.TR_PREPARE.AddObject(objPrepareData)
            Context.SaveChanges(log)
            gID = objPrepareData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertPrepare")
            Throw ex
        End Try
    End Function

    Public Function ModifyPrepare(ByVal objPrepare As ProgramPrepareDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPrepareData As New TR_PREPARE With {.ID = objPrepare.ID}
        Try
            Context.TR_PREPARE.Attach(objPrepareData)
            objPrepareData.EMPLOYEE_ID = objPrepare.EMPLOYEE_ID
            objPrepareData.TR_LIST_PREPARE_ID = objPrepare.TR_LIST_PREPARE_ID
            objPrepareData.TR_PROGRAM_ID = objPrepare.TR_PROGRAM_ID
            Context.SaveChanges(log)
            gID = objPrepareData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyPrepare")
            Throw ex
        End Try

    End Function

    Public Function DeletePrepare(ByVal lstPrepare() As ProgramPrepareDTO) As Boolean
        Dim lstPrepareData As List(Of TR_PREPARE)
        Dim lstIDPrepare As List(Of Decimal) = (From p In lstPrepare.ToList Select p.ID).ToList
        Try
            lstPrepareData = (From p In Context.TR_PREPARE Where lstIDPrepare.Contains(p.ID)).ToList
            For index = 0 To lstPrepareData.Count - 1
                Context.TR_PREPARE.DeleteObject(lstPrepareData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeletePrepare")
            Throw ex
        End Try

    End Function

#End Region

#Region "Class"

    Public Function GetClass(ByVal _filter As ProgramClassDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramClassDTO)

        Try
            Dim query = From p In Context.TR_CLASS
                        From district In Context.HU_DISTRICT.Where(Function(f) f.ID = p.DISTRICT_ID).DefaultIfEmpty
                        From province In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PROVINCE_ID).DefaultIfEmpty
                        Where p.TR_PROGRAM_ID = _filter.TR_PROGRAM_ID
                        Select New ProgramClassDTO With {
                            .ID = p.ID,
                            .NAME = p.NAME,
                            .ADDRESS = p.ADDRESS,
                            .DISTRICT_ID = p.DISTRICT_ID,
                            .DISTRICT_NAME = district.NAME_VN,
                            .PROVINCE_ID = p.PROVINCE_ID,
                            .PROVINCE_NAME = province.NAME_VN,
                            .END_DATE = p.END_DATE,
                            .START_DATE = p.START_DATE,
                            .TR_PROGRAM_ID = p.TR_PROGRAM_ID,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.ADDRESS <> "" Then
                lst = lst.Where(Function(p) p.ADDRESS.ToUpper.Contains(_filter.ADDRESS.ToUpper))
            End If
            If _filter.DISTRICT_NAME <> "" Then
                lst = lst.Where(Function(p) p.DISTRICT_NAME.ToUpper.Contains(_filter.DISTRICT_NAME.ToUpper))
            End If
            If _filter.PROVINCE_NAME <> "" Then
                lst = lst.Where(Function(p) p.PROVINCE_NAME.ToUpper.Contains(_filter.PROVINCE_NAME.ToUpper))
            End If
            If _filter.START_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.START_DATE = _filter.START_DATE)
            End If
            If _filter.END_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.END_DATE = _filter.END_DATE)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetClass")
            Throw ex
        End Try

    End Function


    Public Function GetClassByID(ByVal _filter As ProgramClassDTO) As ProgramClassDTO

        Try
            Dim query = From p In Context.TR_CLASS
                        From district In Context.HU_DISTRICT.Where(Function(f) f.ID = p.DISTRICT_ID).DefaultIfEmpty
                        From province In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PROVINCE_ID).DefaultIfEmpty
                        Where p.ID = _filter.ID
                        Select New ProgramClassDTO With {
                            .ID = p.ID,
                            .NAME = p.NAME,
                            .ADDRESS = p.ADDRESS,
                            .DISTRICT_ID = p.DISTRICT_ID,
                            .DISTRICT_NAME = district.NAME_VN,
                            .PROVINCE_ID = p.PROVINCE_ID,
                            .PROVINCE_NAME = province.NAME_VN,
                            .END_DATE = p.END_DATE,
                            .START_DATE = p.START_DATE,
                            .TR_PROGRAM_ID = p.TR_PROGRAM_ID,
                            .CREATED_DATE = p.CREATED_DATE}

            Return query.FirstOrDefault
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetClass")
            Throw ex
        End Try

    End Function

    Public Function InsertClass(ByVal objClass As ProgramClassDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objClassData As New TR_CLASS
        Try
            objClassData.ID = Utilities.GetNextSequence(Context, Context.TR_CLASS.EntitySet.Name)
            objClassData.ADDRESS = objClass.ADDRESS
            objClassData.PROVINCE_ID = objClass.PROVINCE_ID
            objClassData.DISTRICT_ID = objClass.DISTRICT_ID
            objClassData.END_DATE = objClass.END_DATE
            objClassData.NAME = objClass.NAME
            objClassData.START_DATE = objClass.START_DATE
            objClassData.TR_PROGRAM_ID = objClass.TR_PROGRAM_ID
            Context.TR_CLASS.AddObject(objClassData)
            Context.SaveChanges(log)
            gID = objClassData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertClass")
            Throw ex
        End Try
    End Function

    Public Function ModifyClass(ByVal objClass As ProgramClassDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objClassData As New TR_CLASS With {.ID = objClass.ID}
        Try
            Context.TR_CLASS.Attach(objClassData)
            objClassData.ADDRESS = objClass.ADDRESS
            objClassData.PROVINCE_ID = objClass.PROVINCE_ID
            objClassData.DISTRICT_ID = objClass.DISTRICT_ID
            objClassData.END_DATE = objClass.END_DATE
            objClassData.NAME = objClass.NAME
            objClassData.START_DATE = objClass.START_DATE
            objClassData.TR_PROGRAM_ID = objClass.TR_PROGRAM_ID
            Context.SaveChanges(log)
            gID = objClassData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyClass")
            Throw ex
        End Try

    End Function

    Public Function DeleteClass(ByVal lstClass() As ProgramClassDTO) As Boolean
        Dim lstClassData As List(Of TR_CLASS)
        Dim lstIDClass As List(Of Decimal) = (From p In lstClass.ToList Select p.ID).ToList
        Try
            lstClassData = (From p In Context.TR_CLASS Where lstIDClass.Contains(p.ID)).ToList
            For index = 0 To lstClassData.Count - 1
                Context.TR_CLASS.DeleteObject(lstClassData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteClass")
            Throw ex
        End Try

    End Function



#End Region

#Region "ClassStudent"
    Private Sub GetOrgChild(ByVal orgID As Decimal, ByRef lstORG As List(Of Decimal))
        Dim orgs = (From o In Context.HU_ORGANIZATION Where o.PARENT_ID = orgID Select o.ID)
        lstORG.Add(orgID)
        If orgs.Count > 0 Then
            lstORG.AddRange(orgs)
            For Each org In orgs
                GetOrgChild(org, lstORG)
            Next
        End If
    End Sub

    Public Function GetEmployeeNotByClassID(ByVal _filter As ProgramClassStudentDTO, ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer, ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO)
        Try
            Dim query As IQueryable(Of ProgramClassStudentDTO)
            If _filter.IS_PLAN Then
                query = From p In Context.HU_EMPLOYEE
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From ctr In Context.HU_CONTRACT.Where(Function(f) f.ID = p.CONTRACT_ID).DefaultIfEmpty
                        From ctr_type In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = ctr.CONTRACT_TYPE_ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER).DefaultIfEmpty
                        From pro In Context.TR_REQUEST_EMPLOYEE.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                        From request In Context.TR_REQUEST.Where(Function(f) f.ID = pro.TR_REQUEST_ID)
                        From program In Context.TR_PROGRAM.Where(Function(f) f.TR_REQUEST_ID = request.ID)
                        From cl In Context.TR_CLASS.Where(Function(f) f.TR_PROGRAM_ID = program.ID)
                        Where cl.ID = _filter.TR_CLASS_ID _
                        And Not (From can In Context.TR_CLASS_STUDENT
                                 Where can.TR_CLASS_ID = _filter.TR_CLASS_ID
                                 Select can.EMPLOYEE_ID).Contains(p.ID)
                             Select New ProgramClassStudentDTO With {
                                 .EMPLOYEE_ID = p.ID,
                                 .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                 .EMPLOYEE_NAME = p.FULLNAME_VN,
                                 .BIRTH_DATE = cv.BIRTH_DATE,
                                 .ID_NO = cv.ID_NO,
                                 .ID_DATE = cv.ID_DATE,
                                 .TITLE_NAME = title.NAME_VN,
                                 .ORG_NAME = org.NAME_VN,
                                 .CONTRACT_TYPE_NAME = ctr_type.NAME,
                                 .GENDER_NAME = gender.NAME_VN}
            Else
                query = From p In Context.HU_EMPLOYEE
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From ctr In Context.HU_CONTRACT.Where(Function(f) f.ID = p.CONTRACT_ID).DefaultIfEmpty
                        From ctr_type In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = ctr.CONTRACT_TYPE_ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER).DefaultIfEmpty
                        Where Not (From can In Context.TR_CLASS_STUDENT
                                   Where can.TR_CLASS_ID = _filter.TR_CLASS_ID
                                   Select can.EMPLOYEE_ID).Contains(p.ID)
                               Select New ProgramClassStudentDTO With {.EMPLOYEE_ID = p.ID,
                                                                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                                                       .EMPLOYEE_NAME = p.FULLNAME_VN,
                                                                       .ORG_ID = p.ORG_ID,
                                                                       .TITLE_ID = p.TITLE_ID,
                                                                       .CONTRACT_TYPE_ID = ctr_type.ID,
                                                                       .BIRTH_DATE = cv.BIRTH_DATE,
                                                                       .ID_NO = cv.ID_NO,
                                                                       .ID_DATE = cv.ID_DATE,
                                                                       .TITLE_NAME = title.NAME_VN,
                                                                       .ORG_NAME = org.NAME_VN,
                                                                       .CONTRACT_TYPE_NAME = ctr_type.NAME,
                                                                       .GENDER_NAME = gender.NAME_VN}

                If _filter.ORG_ID IsNot Nothing Then
                    Dim lstOrg As New List(Of Decimal)
                    GetOrgChild(_filter.ORG_ID, lstOrg)
                    query = query.Where(Function(f) lstOrg.Contains(f.ORG_ID))
                    'query = query.Where(Function(f) f.ORG_ID = _filter.ORG_ID)
                End If

                'If _filter.lstTitle IsNot Nothing Then
                '    query = query.Where(Function(f) _filter.lstTitle.Contains(f.TITLE_ID))
                'End If

                'If _filter.lstContractType IsNot Nothing Then
                '    query = query.Where(Function(f) _filter.lstContractType.Contains(f.CONTRACT_TYPE_ID))
                'End If
            End If

            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetEmployeeNotByClassID")
            Throw ex
        End Try

    End Function

    Public Function GetEmployeeByClassID(ByVal _filter As ProgramClassStudentDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO)

        Try
            Dim query = From student In Context.TR_CLASS_STUDENT
                        From p In Context.HU_EMPLOYEE.Where(Function(f) f.ID = student.EMPLOYEE_ID)
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From ctr In Context.HU_CONTRACT.Where(Function(f) f.ID = p.CONTRACT_ID).DefaultIfEmpty
                        From ctr_type In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = ctr.CONTRACT_TYPE_ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER).DefaultIfEmpty
                        Where student.TR_CLASS_ID = _filter.TR_CLASS_ID
                        Select New ProgramClassStudentDTO With {
                            .ID = p.ID,
                            .EMPLOYEE_ID = p.ID,
                            .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                            .EMPLOYEE_NAME = p.FULLNAME_VN,
                            .BIRTH_DATE = cv.BIRTH_DATE,
                            .ID_NO = cv.ID_NO,
                            .ID_DATE = cv.ID_DATE,
                            .TITLE_NAME = title.NAME_VN,
                            .ORG_NAME = org.NAME_VN,
                            .CONTRACT_TYPE_NAME = ctr_type.NAME,
                            .GENDER_NAME = gender.NAME_VN}

            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetEmployeeByClassID")
            Throw ex
        End Try

    End Function

    Public Function InsertClassStudent(ByVal lst As List(Of ProgramClassStudentDTO),
                                   ByVal log As UserLog) As Boolean
        Dim iCount As Integer = 0
        Dim objData As TR_CLASS_STUDENT
        Try
            For Each obj In lst
                Dim isExist = (From p In Context.TR_CLASS_STUDENT
                               Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID _
                               And p.TR_CLASS_ID = obj.TR_CLASS_ID).Any
                If Not isExist Then
                    objData = New TR_CLASS_STUDENT
                    objData.ID = Utilities.GetNextSequence(Context, Context.TR_CLASS_STUDENT.EntitySet.Name)
                    objData.TR_CLASS_ID = obj.TR_CLASS_ID
                    objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
                    Context.TR_CLASS_STUDENT.AddObject(objData)
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteClassStudent(ByVal lst As List(Of ProgramClassStudentDTO),
                                       ByVal log As UserLog) As Boolean
        Try
            For Each obj As ProgramClassStudentDTO In lst
                Dim Students = (From p In Context.TR_CLASS_STUDENT
                               Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID _
                               And p.TR_CLASS_ID = obj.TR_CLASS_ID).ToList
                For Each Student In Students
                    Context.TR_CLASS_STUDENT.DeleteObject(Student)
                Next
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception

        End Try
    End Function

#End Region

#Region "ClassSchedule"

    Public Function GetClassSchedule(ByVal _filter As ProgramClassScheduleDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramClassScheduleDTO)

        Try
            Dim query = From p In Context.TR_CLASS_SCHEDULE
                        Where p.TR_CLASS_ID = _filter.TR_CLASS_ID
                        Select New ProgramClassScheduleDTO With {
                            .ID = p.ID,
                            .TR_CLASS_ID = p.TR_CLASS_ID,
                            .START_TIME = p.START_TIME,
                            .END_TIME = p.END_TIME,
                            .CONTENT = p.CONTENT,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.START_TIME IsNot Nothing Then
                lst = lst.Where(Function(p) p.START_TIME = _filter.START_TIME)
            End If
            If _filter.END_TIME IsNot Nothing Then
                lst = lst.Where(Function(p) p.END_TIME = _filter.END_TIME)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetClassSchedule")
            Throw ex
        End Try
    End Function

    Public Function GetClassScheduleByID(ByVal _filter As ProgramClassScheduleDTO) As ProgramClassScheduleDTO
        Try
            Dim query = From p In Context.TR_CLASS_SCHEDULE
                        Where p.TR_CLASS_ID = _filter.TR_CLASS_ID
                        Select New ProgramClassScheduleDTO With {
                            .ID = p.ID,
                            .TR_CLASS_ID = p.TR_CLASS_ID,
                            .START_TIME = p.START_TIME,
                            .END_TIME = p.END_TIME,
                            .CONTENT = p.CONTENT,
                            .CREATED_DATE = p.CREATED_DATE}
            Return query.FirstOrDefault
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetClassSchedule")
            Throw ex
        End Try
    End Function

    Public Function InsertClassSchedule(ByVal objClassSchedule As ProgramClassScheduleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objClassScheduleData As New TR_CLASS_SCHEDULE
        Try
            objClassScheduleData.ID = Utilities.GetNextSequence(Context, Context.TR_CLASS_SCHEDULE.EntitySet.Name)
            objClassScheduleData.TR_CLASS_ID = objClassSchedule.TR_CLASS_ID
            objClassScheduleData.START_TIME = objClassSchedule.START_TIME
            objClassScheduleData.END_TIME = objClassSchedule.END_TIME
            objClassScheduleData.CONTENT = objClassSchedule.CONTENT
            Context.TR_CLASS_SCHEDULE.AddObject(objClassScheduleData)
            Context.SaveChanges(log)
            gID = objClassScheduleData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertClassSchedule")
            Throw ex
        End Try
    End Function

    Public Function ModifyClassSchedule(ByVal objClassSchedule As ProgramClassScheduleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objClassScheduleData As New TR_CLASS_SCHEDULE With {.ID = objClassSchedule.ID}
        Try
            Context.TR_CLASS_SCHEDULE.Attach(objClassScheduleData)
            objClassScheduleData.START_TIME = objClassSchedule.START_TIME
            objClassScheduleData.END_TIME = objClassSchedule.END_TIME
            objClassScheduleData.CONTENT = objClassSchedule.CONTENT
            Context.SaveChanges(log)
            gID = objClassScheduleData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyClassSchedule")
            Throw ex
        End Try
    End Function

    Public Function DeleteClassSchedule(ByVal lstClassSchedule() As ProgramClassScheduleDTO) As Boolean
        Dim lstClassScheduleData As List(Of TR_CLASS_SCHEDULE)
        Dim lstIDClassSchedule As List(Of Decimal) = (From p In lstClassSchedule.ToList Select p.ID).ToList
        Try
            lstClassScheduleData = (From p In Context.TR_CLASS_SCHEDULE Where lstIDClassSchedule.Contains(p.ID)).ToList
            For index = 0 To lstClassScheduleData.Count - 1
                Context.TR_CLASS_SCHEDULE.DeleteObject(lstClassScheduleData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteClassSchedule")
            Throw ex
        End Try
    End Function

#End Region

#Region "ProgramCommit"

    Public Function GetProgramCommit(ByVal _filter As ProgramCommitDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramCommitDTO)
        Try
            Dim query = From student In Context.TR_CLASS_STUDENT
                        From cls In Context.TR_CLASS.Where(Function(f) f.ID = student.TR_CLASS_ID)
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = student.EMPLOYEE_ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID).DefaultIfEmpty
                        From commit In Context.TR_PROGRAM_COMMIT.Where(Function(f) f.EMPLOYEE_ID = student.EMPLOYEE_ID _
                                                                               And f.TR_PROGRAM_ID = cls.TR_PROGRAM_ID).DefaultIfEmpty
                        From pro In Context.TR_PROGRAM.Where(Function(f) f.ID = cls.TR_PROGRAM_ID).DefaultIfEmpty
                        From cat In Context.TR_COMMIT_AFTER_TRAIN.Where(Function(f) f.COST_TRAIN_FROM <= pro.COST_STUDENT _
                                                                                And pro.COST_STUDENT <= f.COST_TRAIN_TO).DefaultIfEmpty
                        From app In Context.HU_EMPLOYEE.Where(Function(f) f.ID = commit.APPROVER_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = app.TITLE_ID).DefaultIfEmpty
                        Where cls.TR_PROGRAM_ID = _filter.TR_PROGRAM_ID
                        Select New ProgramCommitDTO With {.EMPLOYEE_ID = emp.ID,
                                                          .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                                                          .EMPLOYEE_NAME = emp.FULLNAME_VN,
                                                          .ORG_NAME = org.NAME_VN,
                                                          .COMMIT_NO = commit.COMMIT_NO,
                                                          .COMMIT_DATE = commit.COMMIT_DATE,
                                                          .CONVERED_TIME = cat.COMMIT_WORK,
                                                          .COMMIT_WORK = commit.COMMIT_WORK,
                                                          .APPROVER_ID = commit.APPROVER_ID,
                                                          .APPROVER_NAME = app.FULLNAME_VN,
                                                          .APPROVER_TITLE = title.NAME_VN,
                                                          .COMMIT_REMARK = commit.COMMIT_REMARK}
            Dim lst = query.Distinct

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgramCommit")
            Throw ex
        End Try
    End Function

    Public Function UpdateProgramCommit(ByVal lst As List(Of ProgramCommitDTO),
                                   ByVal log As UserLog) As Boolean
        Try
            For Each obj In lst
                Dim objData = (From p In Context.TR_PROGRAM_COMMIT
                               Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID _
                               And p.TR_PROGRAM_ID = obj.TR_PROGRAM_ID).FirstOrDefault

                If objData Is Nothing Then
                    objData = New TR_PROGRAM_COMMIT
                    With objData
                        .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_COMMIT.EntitySet.Name)
                        .TR_PROGRAM_ID = obj.TR_PROGRAM_ID
                        .EMPLOYEE_ID = obj.EMPLOYEE_ID
                        .COMMIT_NO = obj.COMMIT_NO
                        .COMMIT_DATE = obj.COMMIT_DATE
                        '.CONVERED_TIME = obj.CONVERED_TIME
                        .COMMIT_WORK = obj.COMMIT_WORK
                        .APPROVER_ID = obj.APPROVER_ID
                        .COMMIT_REMARK = obj.COMMIT_REMARK
                        Context.TR_PROGRAM_COMMIT.AddObject(objData)
                    End With
                Else
                    With objData
                        .COMMIT_NO = obj.COMMIT_NO
                        .COMMIT_DATE = obj.COMMIT_DATE
                        '.CONVERED_TIME = obj.CONVERED_TIME
                        .COMMIT_WORK = obj.COMMIT_WORK
                        .APPROVER_ID = obj.APPROVER_ID
                        .COMMIT_REMARK = obj.COMMIT_REMARK
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

#Region "ProgramResult"

    Public Function GetProgramResult(ByVal _filter As ProgramResultDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramResultDTO)
        Try
            Dim query = From student In Context.TR_CLASS_STUDENT
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = student.EMPLOYEE_ID)
                        From cls In Context.TR_CLASS.Where(Function(f) f.ID = student.TR_CLASS_ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID).DefaultIfEmpty
                        From Result In Context.TR_PROGRAM_RESULT.Where(Function(f) f.EMPLOYEE_ID = student.EMPLOYEE_ID _
                                                                               And f.TR_PROGRAM_ID = cls.TR_PROGRAM_ID).DefaultIfEmpty
                        From rank In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Result.TR_RANK_ID).DefaultIfEmpty
                        From rerank In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Result.RETEST_RANK_ID).DefaultIfEmpty
                        From commit In Context.TR_PROGRAM_COMMIT.Where(Function(f) f.EMPLOYEE_ID = student.EMPLOYEE_ID _
                                                                               And f.TR_PROGRAM_ID = cls.TR_PROGRAM_ID).DefaultIfEmpty
                        Where cls.TR_PROGRAM_ID = _filter.TR_PROGRAM_ID
                        Select New ProgramResultDTO With {.ID = Result.ID,
                                                          .EMPLOYEE_ID = emp.ID,
                                                          .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                                                          .EMPLOYEE_NAME = emp.FULLNAME_VN,
                                                          .ORG_NAME = org.NAME_VN,
                                                          .DURATION = Result.DURATION,
                                                          .IS_EXAMS = Result.IS_EXAMS,
                                                          .IS_END = Result.IS_END,
                                                          .IS_REACH = Result.IS_REACH,
                                                          .IS_CERTIFICATE = Result.IS_CERTIFICATE,
                                                          .CERTIFICATE_DATE = Result.CERTIFICATE_DATE,
                                                          .CERTIFICATE_NO = Result.CERTIFICATE_NO,
                                                          .CER_RECEIVE_DATE = Result.CER_RECEIVE_DATE,
                                                          .TOIEC_BENCHMARK = Result.TOIEC_BENCHMARK,
                                                          .TOIEC_SCORE_IN = Result.TOIEC_SCORE_IN,
                                                          .TOIEC_SCORE_OUT = Result.TOIEC_SCORE_OUT,
                                                          .INCREMENT_SCORE = Result.INCREMENT_SCORE,
                                                          .TR_RANK_ID = Result.TR_RANK_ID,
                                                          .TR_RANK_NAME = rank.NAME_VN,
                                                          .RETEST_SCORE = Result.RETEST_SCORE,
                                                          .RETEST_RANK_ID = Result.RETEST_RANK_ID,
                                                          .RETEST_RANK_NAME = rerank.NAME_VN,
                                                          .RETEST_REMARK = Result.RETEST_REMARK,
                                                          .FINAL_SCORE = Result.FINAL_SCORE,
                                                          .ABSENT_REASON = Result.ABSENT_REASON,
                                                          .ABSENT_UNREASON = Result.ABSENT_UNREASON,
                                                          .COMMIT_STARTDATE = Result.COMMIT_STARTDATE,
                                                          .COMMIT_ENDDATE = Result.COMMIT_ENDDATE,
                                                          .COMMIT_WORKMONTH = Result.COMMIT_WORKMONTH,
                                                          .IS_REFUND_FEE = Result.IS_REFUND_FEE,
                                                          .IS_RESERVES = Result.IS_RESERVES,
                                                          .REMARK = Result.REMARK,
                                                          .ATTACH_FILE = Result.ATTACH_FILE}

            Dim lst = query.Distinct

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgramResult")
            Throw ex
        End Try
    End Function

    Public Function UpdateProgramResult(ByVal lst As List(Of ProgramResultDTO),
                                   ByVal log As UserLog) As Boolean
        Try
            For Each obj In lst
                Dim objData = (From p In Context.TR_PROGRAM_RESULT
                               Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID _
                               And p.TR_PROGRAM_ID = obj.TR_PROGRAM_ID).FirstOrDefault

                If objData Is Nothing Then
                    objData = New TR_PROGRAM_RESULT
                    With objData
                        .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_RESULT.EntitySet.Name)
                        .TR_PROGRAM_ID = obj.TR_PROGRAM_ID
                        .EMPLOYEE_ID = obj.EMPLOYEE_ID
                        .DURATION = obj.DURATION
                        .IS_EXAMS = obj.IS_EXAMS
                        .IS_END = obj.IS_END
                        .IS_REACH = obj.IS_REACH
                        .IS_CERTIFICATE = obj.IS_CERTIFICATE
                        .CERTIFICATE_DATE = obj.CERTIFICATE_DATE
                        .CERTIFICATE_NO = obj.CERTIFICATE_NO
                        .CER_RECEIVE_DATE = obj.CER_RECEIVE_DATE
                        .TOIEC_BENCHMARK = obj.TOIEC_BENCHMARK
                        .TOIEC_SCORE_IN = obj.TOIEC_SCORE_IN
                        .TOIEC_SCORE_OUT = obj.TOIEC_SCORE_OUT
                        .INCREMENT_SCORE = obj.INCREMENT_SCORE
                        .TR_RANK_ID = obj.TR_RANK_ID
                        .RETEST_SCORE = obj.RETEST_SCORE
                        .RETEST_RANK_ID = obj.RETEST_RANK_ID
                        .RETEST_REMARK = obj.RETEST_REMARK
                        .FINAL_SCORE = obj.FINAL_SCORE
                        .ABSENT_REASON = obj.ABSENT_REASON
                        .ABSENT_UNREASON = obj.ABSENT_UNREASON
                        .COMMIT_STARTDATE = obj.COMMIT_STARTDATE
                        .COMMIT_ENDDATE = obj.COMMIT_ENDDATE
                        .COMMIT_WORKMONTH = obj.COMMIT_WORKMONTH
                        .IS_REFUND_FEE = obj.IS_REFUND_FEE
                        .IS_RESERVES = obj.IS_RESERVES
                        .REMARK = obj.REMARK
                        .ATTACH_FILE = obj.ATTACH_FILE
                        Context.TR_PROGRAM_RESULT.AddObject(objData)
                    End With
                Else
                    With objData
                        .TR_PROGRAM_ID = obj.TR_PROGRAM_ID
                        .EMPLOYEE_ID = obj.EMPLOYEE_ID
                        .DURATION = obj.DURATION
                        .IS_EXAMS = obj.IS_EXAMS
                        .IS_END = obj.IS_END
                        .IS_REACH = obj.IS_REACH
                        .IS_CERTIFICATE = obj.IS_CERTIFICATE
                        .CERTIFICATE_DATE = obj.CERTIFICATE_DATE
                        .CERTIFICATE_NO = obj.CERTIFICATE_NO
                        .CER_RECEIVE_DATE = obj.CER_RECEIVE_DATE
                        .TOIEC_BENCHMARK = obj.TOIEC_BENCHMARK
                        .TOIEC_SCORE_IN = obj.TOIEC_SCORE_IN
                        .TOIEC_SCORE_OUT = obj.TOIEC_SCORE_OUT
                        .INCREMENT_SCORE = obj.INCREMENT_SCORE
                        .TR_RANK_ID = obj.TR_RANK_ID
                        .RETEST_SCORE = obj.RETEST_SCORE
                        .RETEST_RANK_ID = obj.RETEST_RANK_ID
                        .RETEST_REMARK = obj.RETEST_REMARK
                        .FINAL_SCORE = obj.FINAL_SCORE
                        .ABSENT_REASON = obj.ABSENT_REASON
                        .ABSENT_UNREASON = obj.ABSENT_UNREASON
                        .COMMIT_STARTDATE = obj.COMMIT_STARTDATE
                        .COMMIT_ENDDATE = obj.COMMIT_ENDDATE
                        .COMMIT_WORKMONTH = obj.COMMIT_WORKMONTH
                        .IS_REFUND_FEE = obj.IS_REFUND_FEE
                        .IS_RESERVES = obj.IS_RESERVES
                        .REMARK = obj.REMARK
                        .ATTACH_FILE = obj.ATTACH_FILE
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

#Region "ProgramCost"

    Public Function GetProgramCost(ByVal _filter As ProgramCostDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramCostDTO)

        Try
            Dim query = From p In Context.TR_PROGRAM_COST
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        Where p.TR_PROGRAM_ID = _filter.TR_PROGRAM_ID
                        Select New ProgramCostDTO With {
                            .ID = p.ID,
                            .ORG_ID = p.ORG_ID,
                            .ORG_NAME = org.NAME_VN,
                            .COST_COMPANY = p.COST_COMPANY,
                            .COST_OF_STUDENT = p.COST_OF_STUDENT,
                            .STUDENT_NUMBER = p.STUDENT_NUMBER,
                            .TR_PROGRAM_ID = p.TR_PROGRAM_ID,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.COST_COMPANY IsNot Nothing Then
                lst = lst.Where(Function(p) p.COST_COMPANY = _filter.COST_COMPANY)
            End If
            If _filter.COST_OF_STUDENT IsNot Nothing Then
                lst = lst.Where(Function(p) p.COST_OF_STUDENT = _filter.COST_OF_STUDENT)
            End If
            If _filter.STUDENT_NUMBER IsNot Nothing Then
                lst = lst.Where(Function(p) p.STUDENT_NUMBER = _filter.STUDENT_NUMBER)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgramCost")
            Throw ex
        End Try

    End Function

    Public Function InsertProgramCost(ByVal objProgramCost As ProgramCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objProgramCostData As New TR_PROGRAM_COST
        Try
            objProgramCostData.ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_COST.EntitySet.Name)
            objProgramCostData.COST_COMPANY = objProgramCost.COST_COMPANY
            objProgramCostData.COST_OF_STUDENT = objProgramCost.COST_OF_STUDENT
            objProgramCostData.STUDENT_NUMBER = objProgramCost.STUDENT_NUMBER
            objProgramCostData.ORG_ID = objProgramCost.ORG_ID
            objProgramCostData.TR_PROGRAM_ID = objProgramCost.TR_PROGRAM_ID
            Context.TR_PROGRAM_COST.AddObject(objProgramCostData)
            Context.SaveChanges(log)
            gID = objProgramCostData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertProgramCost")
            Throw ex
        End Try
    End Function

    Public Function ValidateProgramCost(ByVal _validate As ProgramCostDTO)
        Dim query
        Try
            If _validate.ORG_ID IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.TR_PROGRAM_COST
                             Where p.ORG_ID = _validate.ORG_ID _
                             And p.TR_PROGRAM_ID = _validate.TR_PROGRAM_ID _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.TR_PROGRAM_COST
                             Where p.ORG_ID = _validate.ORG_ID _
                             And p.TR_PROGRAM_ID = _validate.TR_PROGRAM_ID).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateProgramCost")
            Throw ex
        End Try
    End Function

    Public Function ModifyProgramCost(ByVal objProgramCost As ProgramCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objProgramCostData As New TR_PROGRAM_COST With {.ID = objProgramCost.ID}
        Try
            Context.TR_PROGRAM_COST.Attach(objProgramCostData)
            objProgramCostData.COST_COMPANY = objProgramCost.COST_COMPANY
            objProgramCostData.COST_OF_STUDENT = objProgramCost.COST_OF_STUDENT
            objProgramCostData.STUDENT_NUMBER = objProgramCost.STUDENT_NUMBER
            Context.SaveChanges(log)
            gID = objProgramCostData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyProgramCost")
            Throw ex
        End Try

    End Function

    Public Function DeleteProgramCost(ByVal lstProgramCost() As ProgramCostDTO) As Boolean
        Dim lstProgramCostData As List(Of TR_PROGRAM_COST)
        Dim lstIDProgramCost As List(Of Decimal) = (From p In lstProgramCost.ToList Select p.ID).ToList
        Try
            lstProgramCostData = (From p In Context.TR_PROGRAM_COST Where lstIDProgramCost.Contains(p.ID)).ToList
            For index = 0 To lstProgramCostData.Count - 1
                Context.TR_PROGRAM_COST.DeleteObject(lstProgramCostData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteProgramCost")
            Throw ex
        End Try

    End Function

#End Region

#Region "Reimbursement"

    Public Function GetReimbursement(ByVal _filter As ReimbursementDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ReimbursementDTO)

        Try
            Dim query = From p In Context.TR_REIMBURSEMENT
                        From pro In Context.TR_PROGRAM.Where(Function(f) f.ID = p.TR_PROGRAM_ID)
                        From course In Context.TR_COURSE.Where(Function(f) f.ID = pro.TR_COURSE_ID)
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From tit In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID).DefaultIfEmpty
                        From cm In Context.TR_PROGRAM_COMMIT.Where(Function(f) f.TR_PROGRAM_ID = pro.ID _
                                                                           And f.EMPLOYEE_ID = emp.ID).DefaultIfEmpty()
                        Select New ReimbursementDTO With {.ID = p.ID,
                                                          .TR_PROGRAM_ID = p.TR_PROGRAM_ID,
                                                          .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                          .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                                                          .EMPLOYEE_NAME = emp.FULLNAME_VN,
                                                          .TITLE = tit.NAME_VN,
                                                          .ORG_NAME = org.NAME_VN,
                                                          .YEAR = p.YEAR,
                                                          .TR_PROGRAM_NAME = course.NAME & " - " & pro.NAME,
                                                          .FROM_DATE = pro.START_DATE,
                                                          .TO_DATE = pro.END_DATE,
                                                          .COST_OF_STUDENT = pro.COST_STUDENT,
                                                          .COMMIT_WORK = cm.COMMIT_WORK,
                                                          .COST_REIMBURSE = p.COST_REIMBURSE,
                                                          .START_DATE = p.START_DATE,
                                                          .IS_RESERVES = p.IS_RESERVES,
                                                          .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.TR_PROGRAM_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_PROGRAM_NAME.ToUpper.Contains(_filter.TR_PROGRAM_NAME.ToUpper))
            End If
            If _filter.START_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.START_DATE = _filter.START_DATE)
            End If
            If _filter.YEAR IsNot Nothing Then
                lst = lst.Where(Function(p) p.YEAR = _filter.YEAR)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Dim lstRes = lst.ToList
            For Each item In lstRes
                item.WORK_AFTER = Math.Round(DateDiff(DateInterval.Day, item.TO_DATE.Value, item.START_DATE.Value) / 30, 1)
            Next
            Return lstRes
            'Dim lstRes = lst.ToList
            'For Each item In lstRes
            '    Dim lstCost = (From p In Context.TR_PROGRAM_COST Where p.TR_PROGRAM_ID = item.TR_PROGRAM_ID).ToList
            '    If lstCost.Count > 0 Then
            '        Dim costCompany As Decimal = 0
            '        Dim numberStudent As Decimal = 0
            '        Dim costOfStudent As Decimal = 0
            '        For Each item1 In lstCost
            '            If item1.COST_COMPANY IsNot Nothing Then
            '                costCompany += item1.COST_COMPANY
            '            End If
            '            If item1.STUDENT_NUMBER IsNot Nothing Then
            '                numberStudent += item1.STUDENT_NUMBER
            '            End If
            '        Next
            '        item.COST_COMPANY = costCompany
            '        item.STUDENT_NUMBER = numberStudent
            '        If numberStudent <> 0 Then
            '            costOfStudent = Decimal.Round(costCompany / numberStudent, 2)
            '        Else
            '            costOfStudent = 0
            '        End If
            '        item.COST_OF_STUDENT = costOfStudent
            '    End If
            'Next
            'Return lstRes
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetReimbursement")
            Throw ex
        End Try

    End Function

    Public Function InsertReimbursement(ByVal objReimbursement As ReimbursementDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objReimbursementData As New TR_REIMBURSEMENT
        Try
            objReimbursementData.ID = Utilities.GetNextSequence(Context, Context.TR_REIMBURSEMENT.EntitySet.Name)
            objReimbursementData.YEAR = objReimbursement.YEAR
            objReimbursementData.TR_PROGRAM_ID = objReimbursement.TR_PROGRAM_ID
            objReimbursementData.EMPLOYEE_ID = objReimbursement.EMPLOYEE_ID
            objReimbursementData.START_DATE = objReimbursement.START_DATE
            If objReimbursement.COST_OF_STUDENT IsNot Nothing Then
                'Thời gian cam kết sau đào tạo
                Dim CAT = objReimbursement.COMMIT_WORK
                'Thời gian đã làm việc sau đào tạo = Ngày bắt đầu bồi hoàn – Ngày kết thức đào tạo
                Dim WAT = Math.Round(DateDiff(DateInterval.Day, objReimbursement.TO_DATE.Value, objReimbursement.START_DATE.Value) / 30, 1)
                'COST_REIMBURSE = Thời gian đã làm việc sau đào tạo / Thời gian cam kết sau đào tạo * học phí đào tạo
                objReimbursementData.COST_REIMBURSE = WAT / CAT * objReimbursement.COST_OF_STUDENT
            End If
            objReimbursementData.IS_RESERVES = objReimbursement.IS_RESERVES
            Context.TR_REIMBURSEMENT.AddObject(objReimbursementData)
            Context.SaveChanges(log)
            gID = objReimbursementData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertReimbursement")
            Throw ex
        End Try
    End Function

    Public Function ValidateReimbursement(ByVal _validate As ReimbursementDTO)
        Dim query
        Try
            If _validate.EMPLOYEE_ID IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.TR_REIMBURSEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.ID <> _validate.ID _
                             And p.TR_PROGRAM_ID <> _validate.TR_PROGRAM_ID).FirstOrDefault
                Else
                    query = (From p In Context.TR_REIMBURSEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.TR_PROGRAM_ID <> _validate.TR_PROGRAM_ID).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateReimbursement")
            Throw ex
        End Try
    End Function

    Public Function ModifyReimbursement(ByVal objReimbursement As ReimbursementDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objReimbursementData As New TR_REIMBURSEMENT With {.ID = objReimbursement.ID}
        Try
            Context.TR_REIMBURSEMENT.Attach(objReimbursementData)
            objReimbursementData.YEAR = objReimbursement.YEAR
            objReimbursementData.TR_PROGRAM_ID = objReimbursement.TR_PROGRAM_ID
            objReimbursementData.EMPLOYEE_ID = objReimbursement.EMPLOYEE_ID
            objReimbursementData.START_DATE = objReimbursement.START_DATE
            If objReimbursement.COST_OF_STUDENT IsNot Nothing Then
                'Thời gian cam kết sau đào tạo
                Dim CAT = objReimbursement.COMMIT_WORK
                'Thời gian đã làm việc sau đào tạo = Ngày bắt đầu bồi hoàn – Ngày kết thức đào tạo
                Dim WAT = Math.Round(DateDiff(DateInterval.Day, objReimbursement.TO_DATE.Value, objReimbursement.START_DATE.Value) / 30, 1)
                'COST_REIMBURSE = Thời gian đã làm việc sau đào tạo / Thời gian cam kết sau đào tạo * học phí đào tạo
                objReimbursementData.COST_REIMBURSE = WAT / CAT * objReimbursement.COST_OF_STUDENT
            End If
            objReimbursementData.IS_RESERVES = objReimbursement.IS_RESERVES
            Context.SaveChanges(log)
            gID = objReimbursementData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyReimbursement")
            Throw ex
        End Try
    End Function

#End Region

#Region "ChooseForm"

    Public Function GetChooseForm(ByVal _filter As ChooseFormDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChooseFormDTO)

        Try
            Dim query = From p In Context.TR_CHOOSE_FORM
                        From pro In Context.TR_PROGRAM.Where(Function(f) f.ID = p.TR_PROGRAM_ID)
                        From course In Context.TR_COURSE.Where(Function(f) f.ID = pro.TR_COURSE_ID)
                        From form In Context.TR_ASSESSMENT_FORM.Where(Function(f) f.ID = p.TR_ASSESSMENT_FORM_ID)
                        Select New ChooseFormDTO With {
                            .ID = p.ID,
                            .YEAR = p.YEAR,
                            .TR_PROGRAM_ID = p.TR_PROGRAM_ID,
                            .TR_PROGRAM_NAME = course.NAME & " - " & pro.NAME,
                            .TR_ASSESSMENT_FORM_NAME = form.NAME,
                            .TR_ASSESSMENT_FORM_ID = p.TR_ASSESSMENT_FORM_ID,
                            .REMARK = p.REMARK,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.TR_ASSESSMENT_FORM_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_ASSESSMENT_FORM_NAME.ToUpper.Contains(_filter.TR_ASSESSMENT_FORM_NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.TR_PROGRAM_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_PROGRAM_NAME.ToUpper.Contains(_filter.TR_PROGRAM_NAME.ToUpper))
            End If
            If _filter.YEAR IsNot Nothing Then
                lst = lst.Where(Function(p) p.YEAR = _filter.YEAR)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetChooseForm")
            Throw ex
        End Try

    End Function

    Public Function InsertChooseForm(ByVal objChooseForm As ChooseFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objChooseFormData As New TR_CHOOSE_FORM
        Try
            objChooseFormData.ID = Utilities.GetNextSequence(Context, Context.TR_CHOOSE_FORM.EntitySet.Name)
            objChooseFormData.TR_ASSESSMENT_FORM_ID = objChooseForm.TR_ASSESSMENT_FORM_ID
            objChooseFormData.REMARK = objChooseForm.REMARK
            objChooseFormData.TR_PROGRAM_ID = objChooseForm.TR_PROGRAM_ID
            objChooseFormData.YEAR = objChooseForm.YEAR
            Context.TR_CHOOSE_FORM.AddObject(objChooseFormData)
            Context.SaveChanges(log)
            gID = objChooseFormData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertChooseForm")
            Throw ex
        End Try
    End Function

    Public Function ValidateChooseForm(ByVal _validate As ChooseFormDTO)
        Dim query
        Try
            If _validate.TR_PROGRAM_ID IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.TR_CHOOSE_FORM
                             Where p.ID <> _validate.ID _
                             And p.TR_PROGRAM_ID = _validate.TR_PROGRAM_ID).FirstOrDefault
                Else
                    query = (From p In Context.TR_CHOOSE_FORM
                             Where p.TR_PROGRAM_ID = _validate.TR_PROGRAM_ID).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateChooseForm")
            Throw ex
        End Try
    End Function

    Public Function ModifyChooseForm(ByVal objChooseForm As ChooseFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objChooseFormData As New TR_CHOOSE_FORM With {.ID = objChooseForm.ID}
        Try
            Context.TR_CHOOSE_FORM.Attach(objChooseFormData)
            objChooseFormData.TR_ASSESSMENT_FORM_ID = objChooseForm.TR_ASSESSMENT_FORM_ID
            objChooseFormData.REMARK = objChooseForm.REMARK
            objChooseFormData.TR_PROGRAM_ID = objChooseForm.TR_PROGRAM_ID
            objChooseFormData.YEAR = objChooseForm.YEAR
            Context.SaveChanges(log)
            gID = objChooseFormData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyChooseForm")
            Throw ex
        End Try

    End Function

    Public Function DeleteChooseForm(ByVal lst() As ChooseFormDTO) As Boolean
        Dim lstProgramCostData As List(Of TR_CHOOSE_FORM)
        Dim lstID As List(Of Decimal) = (From p In lst.ToList Select p.ID).ToList
        Try
            lstProgramCostData = (From p In Context.TR_CHOOSE_FORM Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstProgramCostData.Count - 1
                Context.TR_CHOOSE_FORM.DeleteObject(lstProgramCostData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteChooseFrom")
            Throw ex
        End Try

    End Function

#End Region

#Region "AssessmentResult"

    Public Function GetEmployeeAssessmentResult(ByVal _filter As AssessmentResultDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of AssessmentResultDTO)

        Try
            Dim query = From student In Context.TR_CLASS_STUDENT
                        From choose In Context.TR_CHOOSE_FORM.Where(Function(f) f.ID = _filter.TR_CHOOSE_FORM_ID)
                        From cls In Context.TR_CLASS.Where(Function(f) f.ID = student.TR_CLASS_ID _
                                                               And f.TR_PROGRAM_ID = choose.TR_PROGRAM_ID)
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = student.EMPLOYEE_ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID).DefaultIfEmpty
                        Select New AssessmentResultDTO With {
                            .EMPLOYEE_ID = emp.ID,
                            .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                            .EMPLOYEE_NAME = emp.FULLNAME_VN,
                            .ORG_NAME = org.NAME_VN}

            Dim lst = query.Distinct

            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetEmployeeAssessmentResult")
            Throw ex
        End Try

    End Function

    Public Function GetAssessmentResultByID(ByVal _filter As AssessmentResultDtlDTO) As List(Of AssessmentResultDtlDTO)

        Try
            Dim query = From choose In Context.TR_CHOOSE_FORM
                        From form In Context.TR_ASSESSMENT_FORM.Where(Function(f) f.ID = choose.TR_ASSESSMENT_FORM_ID)
                        From sett_form In Context.TR_SETTING_ASS_FORM.Where(Function(f) f.TR_ASSESSMENT_FORM_ID = form.ID)
                        From sett_cri In Context.TR_SETTING_CRI_GRP.Where(Function(f) f.TR_CRITERIA_GROUP_ID = sett_form.TR_CRITERIA_GROUP_ID)
                        From cri In Context.TR_CRITERIA.Where(Function(f) f.ID = sett_cri.TR_CRITERIA_ID)
                        From cri_group In Context.TR_CRITERIA_GROUP.Where(Function(f) f.ID = sett_cri.TR_CRITERIA_GROUP_ID)
                        From dtl In (From dtl1 In Context.TR_ASSESSMENT_RESULT_DTL
                                     From rs In Context.TR_ASSESSMENT_RESULT.Where(Function(f) f.ID = dtl1.TR_ASSESSMENT_RESULT_ID And _
                                                                                       f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
                                     Select dtl1).Where(Function(f) f.TR_CRITERIA_ID = sett_cri.TR_CRITERIA_ID And _
                                                            f.TR_CRITERIA_GROUP_ID = sett_cri.TR_CRITERIA_GROUP_ID And _
                                                            f.TR_CHOOSE_FORM_ID = choose.ID).DefaultIfEmpty
                        Where choose.ID = _filter.TR_CHOOSE_FORM_ID
                        Order By cri.CODE, cri.NAME
                        Select New AssessmentResultDtlDTO With {
                            .TR_ASSESSMENT_RESULT_ID = dtl.TR_ASSESSMENT_RESULT_ID,
                            .TR_CHOOSE_FORM_ID = _filter.TR_CHOOSE_FORM_ID,
                            .EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                            .EMPLOYEE_CODE = (From p In Context.HU_EMPLOYEE
                                              Where p.ID = _filter.EMPLOYEE_ID Select p.EMPLOYEE_CODE).FirstOrDefault,
                            .EMPLOYEE_NAME = (From p In Context.HU_EMPLOYEE
                                              Where p.ID = _filter.EMPLOYEE_ID Select p.FULLNAME_VN).FirstOrDefault,
                            .TR_CRITERIA_ID = cri.ID,
                            .TR_CRITERIA_CODE = cri.CODE,
                            .TR_CRITERIA_NAME = cri.NAME,
                            .TR_CRITERIA_GROUP_ID = cri_group.ID,
                            .TR_CRITERIA_GROUP_CODE = cri_group.CODE,
                            .TR_CRITERIA_GROUP_NAME = cri_group.NAME,
                            .TR_CRITERIA_POINT_MAX = cri.POINT_MAX,
                            .POINT_ASS = dtl.POINT_ASS,
                            .REMARK = dtl.REMARK}

            Return query.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetAssessmentResultByID")
            Throw ex
        End Try

    End Function

    Public Function UpdateAssessmentResult(ByVal obj As AssessmentResultDTO,
                                   ByVal log As UserLog) As Boolean
        Try
            Dim objData = (From p In Context.TR_ASSESSMENT_RESULT
                           Where p.TR_CHOOSE_FORM_ID = obj.TR_CHOOSE_FORM_ID _
                           And p.EMPLOYEE_ID = obj.EMPLOYEE_ID
                           Select p).FirstOrDefault

            If objData Is Nothing Then
                objData = New TR_ASSESSMENT_RESULT
                With objData
                    .ID = Utilities.GetNextSequence(Context, Context.TR_ASSESSMENT_RESULT.EntitySet.Name)
                    .TR_CHOOSE_FORM_ID = obj.TR_CHOOSE_FORM_ID
                    .EMPLOYEE_ID = obj.EMPLOYEE_ID
                    Context.TR_ASSESSMENT_RESULT.AddObject(objData)
                End With
            End If

            Dim lstDtl = (From p In Context.TR_ASSESSMENT_RESULT_DTL
                              Where p.TR_ASSESSMENT_RESULT_ID = objData.ID
                              Select p).ToList

            For Each item In lstDtl
                Context.TR_ASSESSMENT_RESULT_DTL.DeleteObject(item)
            Next

            For Each item In obj.lstResult
                Dim objDtl = New TR_ASSESSMENT_RESULT_DTL
                objDtl.ID = Utilities.GetNextSequence(Context, Context.TR_ASSESSMENT_RESULT_DTL.EntitySet.Name)
                objDtl.POINT_ASS = item.POINT_ASS
                objDtl.REMARK = item.REMARK
                objDtl.TR_ASSESSMENT_RESULT_ID = objData.ID
                objDtl.TR_CRITERIA_GROUP_ID = item.TR_CRITERIA_GROUP_ID
                objDtl.TR_CRITERIA_ID = item.TR_CRITERIA_ID
                objDtl.TR_CHOOSE_FORM_ID = item.TR_CHOOSE_FORM_ID
                Context.TR_ASSESSMENT_RESULT_DTL.AddObject(objDtl)
            Next

            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function


#End Region

#Region "EmployeeRecord"

    Public Function GetListEmployeePaging(ByVal _filter As RecordEmployeeDTO,
                                              ByVal PageIndex As Integer,
                                              ByVal PageSize As Integer,
                                              ByRef Total As Integer, ByVal _param As ParamDTO,
                                              Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                              Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            'Dim query = From pro_c In Context.TR_PROGRAM_COMMIT
            Dim query = From re In Context.TR_REQUEST_EMPLOYEE
                         From r In Context.TR_REQUEST.Where(Function(f) f.ID = re.TR_REQUEST_ID).DefaultIfEmpty
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = re.EMPLOYEE_ID).DefaultIfEmpty
                         From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                         From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                         From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.WORK_STATUS).DefaultIfEmpty
                         From c In Context.TR_COURSE.Where(Function(f) f.ID = r.TR_COURSE_ID).DefaultIfEmpty
                         From ce In Context.TR_CERTIFICATE.Where(Function(f) f.ID = c.TR_CERTIFICATE_ID)
                         From pr In Context.TR_PROGRAM.Where(Function(f) f.TR_REQUEST_ID = r.ID).DefaultIfEmpty
                         From prg In Context.TR_PROGRAM_GROUP.Where(Function(f) f.ID = c.TR_PROGRAM_GROUP).DefaultIfEmpty
                         From tf In Context.OT_OTHER_LIST.Where(Function(f) f.ID = c.TR_TRAIN_FIELD).DefaultIfEmpty
                         From tfr In Context.OT_OTHER_LIST.Where(Function(f) f.ID = r.TRAIN_FORM_ID).DefaultIfEmpty
                         From lang In Context.OT_OTHER_LIST.Where(Function(f) f.ID = pr.TR_LANGUAGE_ID).DefaultIfEmpty
                         From result In Context.TR_PROGRAM_RESULT.Where(Function(f) f.TR_PROGRAM_ID = pr.ID And f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                         From rank In Context.OT_OTHER_LIST.Where(Function(f) f.ID = result.TR_RANK_ID).DefaultIfEmpty
                         From pcomit In Context.TR_PROGRAM_COMMIT.Where(Function(f) f.EMPLOYEE_ID = e.ID And f.TR_PROGRAM_ID = pr.ID).DefaultIfEmpty
                         From k In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = r.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper).DefaultIfEmpty
                         Where k.USERNAME.ToUpper = log.Username.ToUpper
                         Order By re.ID Descending, e.EMPLOYEE_CODE

            Dim lst = query.Select(Function(p) New RecordEmployeeDTO With {
                                         .ID = p.e.ID,
                                         .EMPLOYEE_ID = p.e.ID,
                                         .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                         .FULLNAME_VN = p.e.FULLNAME_VN,
                                         .TITLE_ID = p.t.ID,
                                         .TITLE_NAME_VN = p.t.NAME_VN,
                                         .ORG_ID = p.org.ID,
                                         .ORG_NAME = p.org.NAME_VN,
                                         .ORG_DESC = p.org.DESCRIPTION_PATH,
                                         .WORK_STATUS = p.e.WORK_STATUS,
                                         .WORK_STATUS_NAME = p.o.NAME_VN,
                                         .TR_COURSE_ID = p.r.TR_COURSE_ID,
                                         .TR_COURSE_NAME = p.c.NAME,
                                         .TR_PROGRAM_ID = p.pr.ID,
                                         .TR_PROGRAM_NAME = p.pr.NAME,
                                         .TR_PROGRAM_GROUP_ID = p.prg.ID,
                                         .TR_PROGRAM_GROUP_NAME = p.prg.NAME,
                                         .FIELDS_ID = p.tf.ID,
                                         .TR_TRAIN_FIELD_NAME = p.tf.NAME_VN,
                                         .TR_TRAIN_FORM_ID = p.r.TRAIN_FORM_ID,
                                         .TR_TRAIN_FORM_NAME = p.tfr.NAME_VN,
                                         .DURATION = p.pr.DURATION,
                                         .START_DATE = p.pr.START_DATE,
                                         .END_DATE = p.pr.END_DATE,
                                         .DURATION_HC = p.pr.DURATION_HC,
                                         .DURATION_OT = p.pr.DURATION_OT,
                                         .COST_TOTAL = p.pr.COST_TOTAL,
                                         .COST_OF_STUDENT = p.pr.COST_STUDENT,
                                         .COST_TOTAL_USD = p.pr.COST_TOTAL_US,
                                         .COST_OF_STUDENT_USD = p.pr.COST_STUDENT_US,
                                         .NO_OF_STUDENT = p.pr.STUDENT_NUMBER,
                                         .IS_REIMBURSE = p.pr.IS_REIMBURSE,
                                         .TR_LANGUAGE_ID = p.pr.TR_LANGUAGE_ID,
                                         .TR_LANGUAGE_NAME = p.lang.NAME_VN,
                                         .TR_UNIT_NAME = p.pr.CENTERS,
                                         .CONTENT = p.pr.CONTENT,
                                         .TARGET_TRAIN = p.pr.TARGET_TRAIN,
                                         .VENUE = p.pr.VENUE,
                                         .IS_EXAMS = If(p.result.RETEST_SCORE Is Nothing, "Không", "Có"),
                                         .IS_END = If(p.result.IS_END = -1, True, False),
                                         .IS_REACH = If(p.result.IS_REACH = -1, "Đạt", "Không đạt"),
                                         .IS_CERTIFICATE = p.result.IS_CERTIFICATE,
                                         .CERTIFICATE_NO = p.result.CERTIFICATE_NO,
                                         .CERTIFICATE_DATE = p.result.CERTIFICATE_DATE,
                                         .CER_RECEIVE_DATE = p.result.CER_RECEIVE_DATE,
                                         .CERTIFICATE_DURATION = p.ce.DURATION,
                                         .COMITMENT_TRAIN_NO = p.pcomit.COMMIT_NO,
                                         .COMMIT_WORK = p.result.COMMIT_WORKMONTH,
                                         .COMITMENT_START_DATE = p.result.COMMIT_STARTDATE,
                                         .COMITMENT_END_DATE = p.result.COMMIT_ENDDATE,
                                         .RANK_ID = p.rank.ID,
                                         .RANK_NAME = p.rank.NAME_VN,
                                         .TOEIC_FINAL_SCORE = p.result.FINAL_SCORE,
            .REMARK = p.r.REMARK
                                        })

            'Lọc theo chức danh 
            'If _filter.TITLE_ID IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.TITLE_ID = _filter.TITLE_ID)
            'End If
            Dim dateNow = Date.Now.Date
            'Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            'Lọc ra cả những nhân viên nghỉ việc
            If Not _filter.IS_AND_TER Then  'Lọc ra những nhân viên nghỉ việc
                If _filter.IS_TER Then
                    lst = lst.Where(Function(p) (p.WORK_STATUS.HasValue And p.WORK_STATUS = 257 And (p.TER_EFFECT_DATE <= dateNow Or p.TER_LAST_DATE <= dateNow)))
                End If
            End If

            If _filter.EMPLOYEE_CODE <> "" Then
                'lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper().IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
                'ThanhNT added lấy full thông tin (Mã/Tên/CMND/Mã cũ)
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0 Or _
                                            p.FULLNAME_VN.ToUpper.IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            'If _filter.FULLNAME_VN <> "" Then
            '    lst = lst.Where(Function(p) p.FULLNAME_VN.ToUpper().IndexOf(_filter.FULLNAME_VN.ToUpper) >= 0)
            'End If

            'If _filter.TITLE_NAME_VN <> "" Then
            '    lst = lst.Where(Function(p) p.TITLE_NAME_VN.ToUpper().IndexOf(_filter.TITLE_NAME_VN.ToUpper) >= 0)
            'End If

            'loc theo don vi
            'lst = lst.Where(Function(p) p.ORG_ID = _filter.ORG_ID)

            'If _filter.JOIN_DATE IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.JOIN_DATE = _filter.JOIN_DATE)
            'End If

            If _filter.FROM_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE >= _filter.FROM_DATE)
            End If

            If _filter.TO_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE <= _filter.TO_DATE)
            End If

            'Lọc theo khoa dao tao
            If _filter.TR_COURSE_ID.ToString() <> "" Then
                lst = lst.Where(Function(p) p.TR_COURSE_ID = _filter.TR_COURSE_ID)
            End If
            'Lọc theo nhom chuong trinh
            If _filter.TR_PROGRAM_GROUP_ID.ToString() <> "" Then
                lst = lst.Where(Function(p) p.TR_PROGRAM_GROUP_ID = _filter.TR_PROGRAM_GROUP_ID)
            End If

            'Lọc theo linh vuc dao tao
            If (_filter.FIELDS_ID IsNot Nothing) Then
                lst = lst.Where(Function(p) p.FIELDS_ID = _filter.FIELDS_ID)
            End If

            'If _filter.WORK_STATUS.ToString() <> "" Then
            '    lst = lst.Where(Function(p) p.WORK_STATUS = _filter.WORK_STATUS)
            'End If

            ''Lọc theo loại hợp đồng
            'If _filter.CONTRACT_TYPE_ID.ToString() <> "" Then
            '    lst = lst.Where(Function(p) p.CONTRACT_TYPE_ID = _filter.CONTRACT_TYPE_ID)
            'End If

            ''Lọc những nhân viên chưa có hợp đồng
            'If _filter.IS_NOT_CONTRACT Then
            '    lst = lst.Where(Function(p) p.CONTRACT_TYPE_ID.Equals(Nothing))
            'End If

            'If _filter.WORK_STATUS_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.WORK_STATUS_NAME.ToUpper().IndexOf(_filter.WORK_STATUS_NAME.ToUpper) >= 0)
            'End If

            'Dim lstresult = lst.ToList().Distinct()
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, Me.ToString() & ".EmployeeRecord")
            Throw ex
        End Try
    End Function
    Public Function GetEmployeeRecord(ByVal _filter As RecordEmployeeDTO,
                                              ByVal PageIndex As Integer,
                                              ByVal PageSize As Integer,
                                              ByRef Total As Integer, ByVal _param As ParamDTO,
                                              Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                              Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From re In Context.TR_REQUEST_EMPLOYEE
                         From r In Context.TR_REQUEST.Where(Function(f) f.ID = re.TR_REQUEST_ID).DefaultIfEmpty
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = re.EMPLOYEE_ID).DefaultIfEmpty
                         From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                         From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                         From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.WORK_STATUS).DefaultIfEmpty
                         From c In Context.TR_COURSE.Where(Function(f) f.ID = r.TR_COURSE_ID).DefaultIfEmpty
                         From ce In Context.TR_CERTIFICATE.Where(Function(f) f.ID = c.TR_CERTIFICATE_ID)
                         From pr In Context.TR_PROGRAM.Where(Function(f) f.TR_REQUEST_ID = r.ID).DefaultIfEmpty
                         From prg In Context.TR_PROGRAM_GROUP.Where(Function(f) f.ID = c.TR_PROGRAM_GROUP).DefaultIfEmpty
                         From tf In Context.OT_OTHER_LIST.Where(Function(f) f.ID = c.TR_TRAIN_FIELD).DefaultIfEmpty
                         From tfr In Context.OT_OTHER_LIST.Where(Function(f) f.ID = r.TRAIN_FORM_ID).DefaultIfEmpty
                         From lang In Context.OT_OTHER_LIST.Where(Function(f) f.ID = pr.TR_LANGUAGE_ID).DefaultIfEmpty
                         From result In Context.TR_PROGRAM_RESULT.Where(Function(f) f.TR_PROGRAM_ID = pr.ID And f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                         From rank In Context.OT_OTHER_LIST.Where(Function(f) f.ID = result.TR_RANK_ID).DefaultIfEmpty
                         From pcomit In Context.TR_PROGRAM_COMMIT.Where(Function(f) f.EMPLOYEE_ID = e.ID And f.TR_PROGRAM_ID = pr.ID).DefaultIfEmpty
                         From k In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = r.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper).DefaultIfEmpty
                         Where k.USERNAME.ToUpper = log.Username.ToUpper
                         Order By re.ID Descending, e.EMPLOYEE_CODE

            Dim lst = query.Select(Function(p) New RecordEmployeeDTO With {
                                         .ID = p.e.ID,
                                         .EMPLOYEE_ID = p.e.ID,
                                         .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                         .FULLNAME_VN = p.e.FULLNAME_VN,
                                         .TITLE_ID = p.t.ID,
                                         .TITLE_NAME_VN = p.t.NAME_VN,
                                         .ORG_ID = p.org.ID,
                                         .ORG_NAME = p.org.NAME_VN,
                                         .ORG_DESC = p.org.DESCRIPTION_PATH,
                                         .WORK_STATUS = p.e.WORK_STATUS,
                                         .WORK_STATUS_NAME = p.o.NAME_VN,
                                         .TR_COURSE_ID = p.r.TR_COURSE_ID,
                                         .TR_COURSE_NAME = p.c.NAME,
                                         .TR_PROGRAM_ID = p.pr.ID,
                                         .TR_PROGRAM_NAME = p.pr.NAME,
                                         .TR_PROGRAM_GROUP_ID = p.prg.ID,
                                         .TR_PROGRAM_GROUP_NAME = p.prg.NAME,
                                         .FIELDS_ID = p.tf.ID,
                                         .TR_TRAIN_FIELD_NAME = p.tf.NAME_VN,
                                         .TR_TRAIN_FORM_ID = p.r.TRAIN_FORM_ID,
                                         .TR_TRAIN_FORM_NAME = p.tfr.NAME_VN,
                                         .DURATION = p.pr.DURATION,
                                         .START_DATE = p.pr.START_DATE,
                                         .END_DATE = p.pr.END_DATE,
                                         .DURATION_HC = p.pr.DURATION_HC,
                                         .DURATION_OT = p.pr.DURATION_OT,
                                         .COST_TOTAL = p.pr.COST_TOTAL,
                                         .COST_OF_STUDENT = p.pr.COST_STUDENT,
                                         .COST_TOTAL_USD = p.pr.COST_TOTAL_US,
                                         .COST_OF_STUDENT_USD = p.pr.COST_STUDENT_US,
                                         .NO_OF_STUDENT = p.pr.STUDENT_NUMBER,
                                         .IS_REIMBURSE = p.pr.IS_REIMBURSE,
                                         .TR_LANGUAGE_ID = p.pr.TR_LANGUAGE_ID,
                                         .TR_LANGUAGE_NAME = p.lang.NAME_VN,
                                         .TR_UNIT_NAME = p.pr.CENTERS,
                                         .CONTENT = p.pr.CONTENT,
                                         .TARGET_TRAIN = p.pr.TARGET_TRAIN,
                                         .VENUE = p.pr.VENUE,
                                         .IS_EXAMS = If(p.result.RETEST_SCORE Is Nothing, "Không", "Có"),
                                         .IS_END = If(p.result.IS_END = -1, True, False),
                                         .IS_REACH = If(p.result.IS_REACH = -1, "Đạt", "Không đạt"),
                                         .IS_CERTIFICATE = p.result.IS_CERTIFICATE,
                                         .CERTIFICATE_NO = p.result.CERTIFICATE_NO,
                                         .CERTIFICATE_DATE = p.result.CERTIFICATE_DATE,
                                         .CER_RECEIVE_DATE = p.result.CER_RECEIVE_DATE,
                                         .CERTIFICATE_DURATION = p.ce.DURATION,
                                         .COMITMENT_TRAIN_NO = p.pcomit.COMMIT_NO,
                                         .COMMIT_WORK = p.result.COMMIT_WORKMONTH,
                                         .COMITMENT_START_DATE = p.result.COMMIT_STARTDATE,
                                         .COMITMENT_END_DATE = p.result.COMMIT_ENDDATE,
                                         .RANK_ID = p.rank.ID,
                                         .RANK_NAME = p.rank.NAME_VN,
                                         .TOEIC_FINAL_SCORE = p.result.FINAL_SCORE,
            .REMARK = p.r.REMARK
                                        })

            'Lọc ra cả những nhân viên nghỉ việc
            'If Not _filter.IS_AND_TER Then  'Lọc ra những nhân viên nghỉ việc
            '    If _filter.IS_TER Then
            '        lst = lst.Where(Function(p) (p.WORK_STATUS.HasValue And p.WORK_STATUS = terID And (p.TER_EFFECT_DATE <= dateNow Or p.TER_LAST_DATE <= dateNow)))
            '    End If
            'End If

            If _filter.EMPLOYEE_CODE <> "" Then
                'lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper().IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
                'ThanhNT added lấy full thông tin (Mã/Tên/CMND/Mã cũ)
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0 Or _
                                            p.FULLNAME_VN.ToUpper.IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
            End If
            'If _filter.ORG_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            'End If
            If _filter.FROM_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE >= _filter.FROM_DATE)
            End If

            If _filter.TO_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE <= _filter.TO_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, Me.ToString() & ".EmployeeRecord")
            Throw ex
        End Try
    End Function
#End Region

#Region "Employee Title Course"
    Public Function GetEmployeeTitleCourse(ByVal filter As EmployeeTitleCourseDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "HU_EMPLOYEE_ID desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of EmployeeTitleCourseDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From e In Context.HU_EMPLOYEE
                        From tico In Context.TR_TITLE_COURSE.Where(Function(f) f.HU_TITLE_ID = e.TITLE_ID).DefaultIfEmpty
                        From cou In Context.TR_COURSE.Where(Function(f) f.ID = tico.TR_COURSE_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From org In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                    Select New EmployeeTitleCourseDTO With {
                        .HU_EMPLOYEE_ID = e.ID,
                        .HU_EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                        .HU_EMPLOYEE_NAME = e.FULLNAME_VN,
                        .HU_ORG_ID = e.ORG_ID,
                        .HU_TITLE_ID = e.TITLE_ID,
                        .HU_TITLE_CODE = title.CODE,
                        .HU_TITLE_NAME = title.NAME_VN,
                        .TR_COURSE_ID = cou.ID,
                        .TR_COURSE_CODE = cou.CODE,
                        .TR_COURSE_NAME = cou.NAME,
                        .TR_COURSE_REMARK = cou.REMARK,
                        .CREATED_DATE = tico.CREATED_DATE}

            Dim lst = query
            'If filter.NAME <> "" Then
            '    lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(filter.NAME.ToUpper))
            'End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList()
            Return Nothing
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetEmployeeTitleCourse")
            Throw ex
        End Try
    End Function
#End Region




End Class
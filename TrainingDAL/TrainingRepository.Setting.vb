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

#Region "SettingCriteriaGroup"

    Public Function GetCriteriaNotByGroupID(ByVal _filter As SettingCriteriaGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_CODE desc") As List(Of SettingCriteriaGroupDTO)

        Try
            Dim query As IQueryable(Of SettingCriteriaGroupDTO)
            query = From p In Context.TR_CRITERIA
                         Where p.ACTFLG = True _
                         And Not (From can In Context.TR_SETTING_CRI_GRP
                                  Where can.TR_CRITERIA_GROUP_ID = _filter.TR_CRITERIA_GROUP_ID
                                  Select can.TR_CRITERIA_ID).Contains(p.ID)
                              Select New SettingCriteriaGroupDTO With {
                                  .TR_CRITERIA_ID = p.ID,
                                  .TR_CRITERIA_CODE = p.CODE,
                                  .TR_CRITERIA_NAME = p.NAME,
                                  .TR_CRITERIA_POINT_MAX = p.POINT_MAX}
            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCriteriaNotByGroupID")
            Throw ex
        End Try

    End Function

    Public Function GetCriteriaByGroupID(ByVal _filter As SettingCriteriaGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_CODE desc") As List(Of SettingCriteriaGroupDTO)

        Try
            Dim query = From sett In Context.TR_SETTING_CRI_GRP
                        From p In Context.TR_CRITERIA.Where(Function(f) f.ID = sett.TR_CRITERIA_ID)
                        Where sett.TR_CRITERIA_GROUP_ID = _filter.TR_CRITERIA_GROUP_ID
                        Select New SettingCriteriaGroupDTO With {
                            .ID = sett.ID,
                            .TR_CRITERIA_ID = p.ID,
                            .TR_CRITERIA_CODE = p.CODE,
                            .TR_CRITERIA_NAME = p.NAME,
                            .TR_CRITERIA_POINT_MAX = p.POINT_MAX}

            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCriteriaByGroupID")
            Throw ex
        End Try

    End Function

    Public Function InsertSettingCriteriaGroup(ByVal lst As List(Of SettingCriteriaGroupDTO),
                                   ByVal log As UserLog) As Boolean
        Dim iCount As Integer = 0
        Dim objData As TR_SETTING_CRI_GRP
        Try
            For Each obj In lst
                objData = New TR_SETTING_CRI_GRP
                objData.ID = Utilities.GetNextSequence(Context, Context.TR_SETTING_CRI_GRP.EntitySet.Name)
                objData.TR_CRITERIA_GROUP_ID = obj.TR_CRITERIA_GROUP_ID
                objData.TR_CRITERIA_ID = obj.TR_CRITERIA_ID
                Context.TR_SETTING_CRI_GRP.AddObject(objData)
            Next

            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteSettingCriteriaGroup(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean
        Try
            Dim lstData = (From p In Context.TR_SETTING_CRI_GRP Where lst.Contains(p.ID)).ToList
            For Each objData In lstData
                Context.TR_SETTING_CRI_GRP.DeleteObject(objData)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception

        End Try
    End Function

#End Region

#Region "SettingAssForm"

    Public Function GetCriteriaGroupNotByFormID(ByVal _filter As SettingAssFormDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_GROUP_CODE desc") As List(Of SettingAssFormDTO)

        Try
            Dim query As IQueryable(Of SettingAssFormDTO)
            query = From p In Context.TR_CRITERIA_GROUP
                         Where p.ACTFLG = True _
                         And Not (From can In Context.TR_SETTING_ASS_FORM
                                  Where can.TR_ASSESSMENT_FORM_ID = _filter.TR_ASSESSMENT_FORM_ID
                                  Select can.TR_CRITERIA_GROUP_ID).Contains(p.ID)
                              Select New SettingAssFormDTO With {
                                  .TR_CRITERIA_GROUP_ID = p.ID,
                                  .TR_CRITERIA_GROUP_CODE = p.CODE,
                                  .TR_CRITERIA_GROUP_NAME = p.NAME,
                                  .REMARK = p.REMARK}
            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCriteriaGroupNotByFormID")
            Throw ex
        End Try

    End Function

    Public Function GetCriteriaGroupByFormID(ByVal _filter As SettingAssFormDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_GROUP_CODE desc") As List(Of SettingAssFormDTO)

        Try
            Dim query = From sett In Context.TR_SETTING_ASS_FORM
                        From p In Context.TR_CRITERIA_GROUP.Where(Function(f) f.ID = sett.TR_CRITERIA_GROUP_ID)
                        Where sett.TR_ASSESSMENT_FORM_ID = _filter.TR_ASSESSMENT_FORM_ID
                        Select New SettingAssFormDTO With {
                            .ID = sett.ID,
                            .TR_CRITERIA_GROUP_ID = p.ID,
                            .TR_CRITERIA_GROUP_CODE = p.CODE,
                            .TR_CRITERIA_GROUP_NAME = p.NAME,
                            .REMARK = p.REMARK}

            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCriteriaGroupByFormID")
            Throw ex
        End Try

    End Function

    Public Function InsertSettingAssForm(ByVal lst As List(Of SettingAssFormDTO),
                                   ByVal log As UserLog) As Boolean
        Dim iCount As Integer = 0
        Dim objData As TR_SETTING_ASS_FORM
        Try
            For Each obj In lst
                objData = New TR_SETTING_ASS_FORM
                objData.ID = Utilities.GetNextSequence(Context, Context.TR_SETTING_ASS_FORM.EntitySet.Name)
                objData.TR_CRITERIA_GROUP_ID = obj.TR_CRITERIA_GROUP_ID
                objData.TR_ASSESSMENT_FORM_ID = obj.TR_ASSESSMENT_FORM_ID
                Context.TR_SETTING_ASS_FORM.AddObject(objData)
            Next

            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteSettingAssForm(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean
        Try
            Dim lstData = (From p In Context.TR_SETTING_ASS_FORM Where lst.Contains(p.ID)).ToList
            For Each objData In lstData
                Context.TR_SETTING_ASS_FORM.DeleteObject(objData)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Setting Title Course"
    Public Function GetTitleCourse(ByVal _filter As TitleCourseDTO, ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE") As List(Of TitleCourseDTO)

        Try
            Dim query = From p In Context.TR_TITLE_COURSE
                         From course In Context.TR_COURSE.Where(Function(f) f.ID = p.TR_COURSE_ID).DefaultIfEmpty
                        Select New TitleCourseDTO With {
                                        .ID = p.ID,
                                        .HU_TITLE_ID = p.HU_TITLE_ID,
                                        .TR_COURSE_ID = p.TR_COURSE_ID,
                                        .TR_COURSE_CODE = course.CODE,
                                        .TR_COURSE_NAME = course.NAME,
                                        .TR_COURSE_REMARK = course.REMARK,
                                        .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If _filter.HU_TITLE_ID.HasValue Then
                lst = lst.Where(Function(p) p.HU_TITLE_ID = _filter.HU_TITLE_ID)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetTitleCourse")
            Throw ex
        End Try

    End Function
    Public Function UpdateTitleCourse(ByVal objExams As TitleCourseDTO, ByVal log As UserLog) As Boolean
        Dim objTitleCourse As TR_TITLE_COURSE
        Try
            objTitleCourse = (From p In Context.TR_TITLE_COURSE
                            Where p.ID = objExams.ID).FirstOrDefault
            If objTitleCourse Is Nothing Then
                objTitleCourse = New TR_TITLE_COURSE
                objTitleCourse.ID = Utilities.GetNextSequence(Context, Context.TR_TITLE_COURSE.EntitySet.Name)
                objTitleCourse.HU_TITLE_ID = objExams.HU_TITLE_ID
                objTitleCourse.TR_COURSE_ID = objExams.TR_COURSE_ID
                objTitleCourse.ACTFLG = "A"
                Context.TR_TITLE_COURSE.AddObject(objTitleCourse)
                Context.SaveChanges(log)
            Else
                Context.TR_TITLE_COURSE.Attach(objTitleCourse)
                objTitleCourse.HU_TITLE_ID = objExams.HU_TITLE_ID
                objTitleCourse.TR_COURSE_ID = objExams.TR_COURSE_ID
                objTitleCourse.ACTFLG = objExams.ACTFLG
                'Context.TR_TITLE_COURSE.AddObject(objTitleCourse)
                Context.SaveChanges(log)
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateTitleCourse")
            Throw ex
        End Try
    End Function

    Public Function DeleteTitleCourse(ByVal obj As TitleCourseDTO) As Boolean
        Dim objDel As TR_TITLE_COURSE
        Try
            objDel = (From p In Context.TR_TITLE_COURSE Where obj.ID = p.ID).FirstOrDefault
            Context.TR_TITLE_COURSE.DeleteObject(objDel)
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTitleCourse")
            Throw ex
        End Try

    End Function



#End Region


End Class
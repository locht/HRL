Imports System.Transactions
Imports System.Web
Imports Framework.Data
Imports System.Data.Objects
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Data.EntityClient
Imports Framework.Data.System.Linq.Dynamic
Imports System.Data.Entity
Imports System.Text.RegularExpressions
Imports System.Reflection

Partial Class ProfileRepository

#Region "Competency Course"

    Public Function GetCompetencyCourse(ByVal _filter As CompetencyCourseDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyCourseDTO)

        Try
            Dim query = From p In Context.HU_COMPETENCY_COURSE
                        From Competencygroup In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = p.COMPETENCY_GROUP_ID).DefaultIfEmpty
                        From Competency In Context.HU_COMPETENCY.Where(Function(f) f.ID = p.COMPETENCY_ID).DefaultIfEmpty
                        From Course In Context.TR_COURSE.Where(Function(f) f.ID = p.TR_COURSE_ID).DefaultIfEmpty
                        Select New CompetencyCourseDTO With {
                            .ID = p.ID,
                            .COMPETENCY_GROUP_ID = p.COMPETENCY_GROUP_ID,
                            .COMPETENCY_GROUP_NAME = Competencygroup.NAME,
                            .COMPETENCY_ID = p.COMPETENCY_ID,
                            .COMPETENCY_NAME = Competency.NAME,
                            .LEVEL_NUMBER = p.LEVEL_NUMBER,
                            .TR_COURSE_ID = p.TR_COURSE_ID,
                            .TR_COURSE_NAME = Course.NAME,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.COMPETENCY_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_GROUP_NAME.ToUpper.Contains(_filter.COMPETENCY_GROUP_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_NAME.ToUpper.Contains(_filter.COMPETENCY_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TR_COURSE_NAME) Then
                lst = lst.Where(Function(p) p.TR_COURSE_NAME.ToUpper.Contains(_filter.TR_COURSE_NAME.ToUpper))
            End If
            If _filter.LEVEL_NUMBER IsNot Nothing Then
                lst = lst.Where(Function(p) p.LEVEL_NUMBER = _filter.LEVEL_NUMBER)
            End If
            'If _filter.COMPETENCY_GROUP_ID IsNot Nothing Then
            lst = lst.Where(Function(p) p.COMPETENCY_GROUP_ID = _filter.COMPETENCY_GROUP_ID)
            'End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCompetency")
            Throw ex
        End Try
    End Function
    Public Function InsertCompetencyCourse(ByVal objCompetencyCourse As CompetencyCourseDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyCourseData As New HU_COMPETENCY_COURSE
        Dim iCount As Integer = 0
        Try
            objCompetencyCourseData = (From p In Context.HU_COMPETENCY_COURSE
                                   Where p.COMPETENCY_ID = objCompetencyCourse.COMPETENCY_ID And
                                   p.COMPETENCY_GROUP_ID = objCompetencyCourse.COMPETENCY_GROUP_ID).FirstOrDefault

            If objCompetencyCourseData IsNot Nothing Then
                objCompetencyCourseData.LEVEL_NUMBER = objCompetencyCourse.LEVEL_NUMBER
                objCompetencyCourseData.TR_COURSE_ID = objCompetencyCourse.TR_COURSE_ID
            Else
                objCompetencyCourseData = New HU_COMPETENCY_COURSE
                objCompetencyCourseData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_COURSE.EntitySet.Name)
                objCompetencyCourseData.COMPETENCY_GROUP_ID = objCompetencyCourse.COMPETENCY_GROUP_ID
                objCompetencyCourseData.COMPETENCY_ID = objCompetencyCourse.COMPETENCY_ID
                objCompetencyCourseData.LEVEL_NUMBER = objCompetencyCourse.LEVEL_NUMBER
                objCompetencyCourseData.TR_COURSE_ID = objCompetencyCourse.TR_COURSE_ID
                Context.HU_COMPETENCY_COURSE.AddObject(objCompetencyCourseData)
            End If
            Context.SaveChanges(log)
            gID = objCompetencyCourseData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCompetency")
            Throw ex
        End Try

    End Function
    Public Function ModifyCompetencyCourse(ByVal objCompetencyCourse As CompetencyCourseDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyCourseData As HU_COMPETENCY_COURSE
        Try
            objCompetencyCourseData = (From p In Context.HU_COMPETENCY_COURSE Where p.ID = objCompetencyCourse.ID).FirstOrDefault
            objCompetencyCourseData.COMPETENCY_GROUP_ID = objCompetencyCourse.COMPETENCY_GROUP_ID
            objCompetencyCourseData.COMPETENCY_ID = objCompetencyCourse.COMPETENCY_ID
            objCompetencyCourseData.LEVEL_NUMBER = objCompetencyCourse.LEVEL_NUMBER
            objCompetencyCourseData.TR_COURSE_ID = objCompetencyCourse.TR_COURSE_ID
            Context.SaveChanges(log)
            gID = objCompetencyCourseData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCompetency")
            Throw ex
        End Try

    End Function

    Public Function DeleteCompetencyCourse(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstCompetencyCourseData As List(Of HU_COMPETENCY_COURSE)
        Try

            lstCompetencyCourseData = (From p In Context.HU_COMPETENCY_COURSE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyCourseData.Count - 1
                Context.HU_COMPETENCY_COURSE.DeleteObject(lstCompetencyCourseData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCompetency")
            Throw ex
        End Try

    End Function

#End Region
End Class

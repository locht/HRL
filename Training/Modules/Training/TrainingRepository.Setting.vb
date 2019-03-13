﻿Imports Training.TrainingBusiness
Imports Framework.UI

Partial Class TrainingRepository

#Region "SettingCriteriaGroup"

    Public Function GetCriteriaNotByGroupID(ByVal _filter As SettingCriteriaGroupDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_CODE desc") As List(Of SettingCriteriaGroupDTO)
        Dim lstClass As List(Of SettingCriteriaGroupDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClass = rep.GetCriteriaNotByGroupID(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClass
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCriteriaByGroupID(ByVal _filter As SettingCriteriaGroupDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_CODE desc") As List(Of SettingCriteriaGroupDTO)
        Dim lstClass As List(Of SettingCriteriaGroupDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClass = rep.GetCriteriaByGroupID(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClass
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertSettingCriteriaGroup(ByVal lst As List(Of SettingCriteriaGroupDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertSettingCriteriaGroup(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using


    End Function

    Public Function DeleteSettingCriteriaGroup(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteSettingCriteriaGroup(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "SettingAssForm"

    Public Function GetCriteriaGroupNotByFormID(ByVal _filter As SettingAssFormDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_GROUP_CODE desc") As List(Of SettingAssFormDTO)
        Dim lstClass As List(Of SettingAssFormDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClass = rep.GetCriteriaGroupNotByFormID(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClass
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCriteriaGroupByFormID(ByVal _filter As SettingAssFormDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_GROUP_CODE desc") As List(Of SettingAssFormDTO)
        Dim lstClass As List(Of SettingAssFormDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClass = rep.GetCriteriaGroupByFormID(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClass
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertSettingAssForm(ByVal lst As List(Of SettingAssFormDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertSettingAssForm(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using


    End Function

    Public Function DeleteSettingAssForm(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteSettingAssForm(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Setting Title Course"

    Public Function GetTitleCourse(ByVal _filter As TitleCourseDTO,
                                    ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE") As List(Of TitleCourseDTO)
        Dim lstTittleCourseDTO As List(Of TitleCourseDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstTittleCourseDTO = rep.GetTitleCourse(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstTittleCourseDTO
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateTitleCourse(ByVal obj As TitleCourseDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.UpdateTitleCourse(obj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteTitleCourse(ByVal obj As TitleCourseDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteTitleCourse(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

End Class

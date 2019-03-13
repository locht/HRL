Imports Framework.Data
Imports System.Data.Objects
Imports System.Transactions
Imports System.Data.Entity
Imports System.Data.Common
Imports System.Text
Imports Framework.Data.System.Linq.Dynamic
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net
Imports System.IO
Imports Framework.Data.SystemConfig
Imports System.Configuration
Imports System.Reflection

Partial Public Class CommonRepository
    Inherits CommonRepositoryBase

#Region "Kiểm tra dữ liệu đang được sử dụng"


    Public Function GetATOrgPeriod(ByVal periodID As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_AT_ORG_PERIOD",
                                           New With {.P_PERIOD = periodID,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' Kiểm tra dữ liệu Other list đã được sử dụng hay chưa?
    ''' </summary>
    ''' <param name="table">Enum Table_Name</param>
    ''' <returns>true:chưa có/false:có rồi</returns>
    ''' <remarks></remarks>
    Public Function CheckOtherListExistInDatabase(ByVal lstID As List(Of Decimal), ByVal typeID As Decimal) As Boolean
        Dim isExist As Boolean = False
        Try
            Dim strListID As String = lstID.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
            Dim sql = ""
            Select Case typeID
                Case 1 'ACADEMY
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_EDUCATION", "ACADEMY", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case 3 'ACTION_TYPE
                Case 5 'ASSET_GROUP
                Case 7 'ATTATCH_TYPE
                Case 20 'COMMEND_LEVEL
                Case 21 'COMMEND_OBJECT
                Case 22 'COMMEND_TYPE
                Case 24 'CONTRACT_STATUS
                    isExist = Execute_ExistInDatabase("HU_CONTRACT", "STATUS_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case 25 'DB_CHANGE
                Case 26 'DB_EMP
                Case 28 'DECISION_TYPE
                Case 29 'DISCIPLINE_LEVEL
                Case 30 'DISCIPLINE_OBJECT
                Case 31 'DISCIPLINE_TYPE
                Case 32 'FAMILY_STATUS
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "MARITAL_STATUS", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case 34 'GENDER
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "GENDER", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case 38 'LANGUAGE_LEVEL
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_EDUCATION", "LANGUAGE_LEVEL", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case 39 'LEARNING_LEVEL
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_EDUCATION", "LEARNING_LEVEL", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case 40 'MAJOR
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_EDUCATION", "MAJOR", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case 42 'NATIVE
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "NATIVE", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case 43 'ORG_LEVEL
                    isExist = Execute_ExistInDatabase("HU_ORGANIZATION", "LEVEL_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case 48 'RELATION
                Case 49 'RELIGION
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "RELIGION", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case 50 'SYSTEM_LANGUAGE
                Case 51 'TER_REASON
                Case 52 'TER_STATUS
                Case 53 'TER_TYPE
                Case 54 'TRANSFER_REASON
                Case 55 'DECISION_STATUS
                Case 56 'TRANSFER_TYPE
                Case 57 'UNIT
                Case 59 'WORK_STATUS
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE", "WORK_STATUS", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case 141 'MARK_EDU
                Case 142 'TRAINING_FORM
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_EDUCATION", "TRAINING_FORM", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case 180 'COMMEND_STATUS
                Case 181 'DISCIPLINE_STATUS
                Case 961 'WORK_TIME
                Case 1000 'TR_CER_GROUP
                Case 1001 'TR_TRAIN_FORM
                Case 1002 'TR_TRAIN_ENTRIES
                Case 1003 'TR_DURATION_UNIT
                Case 1004 'TR_CURRENCY
                Case 1005 'TR_REQUEST_STATUS
                Case 1006 'TR_DURATION_STUDY
                Case 1007 'TR_LANGUAGE
                Case 1008 'TR_LIST_PREPARE
                Case 1009 'TR_RANK
                Case 1010 'RC_RECRUIT_REASON
                Case 1011 'RC_PLAN_REG_STATUS
                Case 1012 'RC_REQUEST_STATUS
                Case 1013 'RC_RECRUIT_TYPE
                Case 1014 'RC_GROUP_WORK
                Case 1015 'RC_PRIORITY_LEVEL
                Case 1016 'RC_TRAINING_LEVEL
                Case 1017 'RC_TRAINING_SCHOOL
                Case 1019 'RC_GRADUATION_TYPE
                Case 1020 'RC_LANGUAGE
                Case 1021 'RC_LANGUAGE_LEVEL
                Case 1022 'RC_COMPUTER_LEVEL
                Case 1023 'RC_APPEARANCE
                Case 1024 'RC_HEALTH_STATUS
                Case 1025 'RC_PROGRAM_STATUS
                Case 1026 'RC_RECRUIT_SCOPE
                Case 1027 'RC_SOFT_SKILL
                Case 1028 'RC_CHARACTER
                Case 1029 'RC_CANDIDATE_STATUS
                Case 1032 'RC_FORM
                Case 1035 'TYPE_PUNISH
                Case 1036 'TYPE_SHIFT
                Case 1037 'TYPE_PAYMENT
                Case 1038 'TYPE_EMPLOYEE
                Case 1039 'TYPE_REST_DAY
                Case 2000 'HU_TITLE_GROUP
                    isExist = Execute_ExistInDatabase("HU_TITLE", "TITLE_GROUP_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case 2001 'PA_RESIDENT
                Case 2002 'CALCULATION
                Case 2003 'INSURANCE_CHANGE_TYPE
                Case 2021 'GROUP_ARISING_TYPE
                Case 2022 'STATUS_NOBOOK
                Case 2023 'STATUS_CARD
                Case 2024 'LOCATION
                Case 2025 'ORG_INS
                Case 2026 'REASON
                Case 2027 'TYPE_DSVM
                Case 2028 'HS_OT
                Case 2029 'DEFAULT_RICE

            End Select
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Private Function Execute_ExistInDatabase(tableName As String, colName As String, strListID As String)
        Try
            Dim count As Decimal = 0
            Dim Sql = "SELECT COUNT(" & colName & ") FROM " & tableName & " WHERE " & colName & " IN (" & strListID & ")"
            count = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault
            If count > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Otherlist"
    Public Function GetOtherListByTypeToCombo(ByVal sType As String) As List(Of OtherListDTO)
        Try
            Dim query = (From p In Context.OT_OTHER_LIST
                         Where p.ACTFLG = "A" And
                            p.OT_OTHER_LIST_TYPE.CODE.ToUpper = sType.ToUpper
                        Order By p.NAME_VN
                        Select New OtherListDTO With {
                            .ID = p.ID,
                            .NAME_VN = p.NAME_VN,
                            .NAME_EN = p.NAME_EN,
                            .CODE = p.CODE})
            Return query.ToList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetOtherList(ByVal sACT As String) As List(Of OtherListDTO)
        Dim query As ObjectQuery(Of OtherListDTO)
        If sACT = "" Then
            query = (From p In Context.OT_OTHER_LIST
                      Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Order By q.NAME, p.NAME_VN
           Select New OtherListDTO With {.ID = p.ID,
                                         .NAME_VN = p.NAME_VN,
                                         .NAME_EN = p.NAME_EN,
                                         .CODE = p.CODE,
                                         .TYPE_ID = p.TYPE_ID,
                                         .TYPE_NAME = q.NAME,
                                         .TYPE_CODE = q.CODE,
                                         .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})
        Else
            query = (From p In Context.OT_OTHER_LIST
                      Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Order By q.NAME, p.NAME_VN
                     Where p.ACTFLG = sACT
           Select New OtherListDTO With {.ID = p.ID,
                                         .NAME_VN = p.NAME_VN,
                                         .NAME_EN = p.NAME_EN,
                                         .CODE = p.CODE,
                                         .TYPE_ID = p.TYPE_ID,
                                         .TYPE_NAME = q.NAME,
                                         .TYPE_CODE = q.CODE,
                                         .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})
        End If

        '''Logger.LogInfo(query)
        Return query.ToList

    End Function

    Public Function GetOtherListByType(ByVal gID As Decimal,
                                       ByVal _filter As OtherListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OtherListDTO)
        Dim query = From p In Context.OT_OTHER_LIST Where p.TYPE_ID = gID
                     Select New OtherListDTO With {.ID = p.ID,
                                                   .NAME_VN = p.NAME_VN,
                                                   .NAME_EN = p.NAME_EN,
                                                   .CODE = p.CODE,
                                                   .REMARK = p.REMARK,
                                                   .TYPE_ID = p.TYPE_ID,
                                                   .TYPE_NAME = p.OT_OTHER_LIST_TYPE.NAME,
                                                   .TYPE_CODE = p.OT_OTHER_LIST_TYPE.CODE,
                                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                   .CREATED_DATE = p.CREATED_DATE}

        If _filter.CODE <> "" Then
            query = query.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
        End If
        If _filter.NAME_EN <> "" Then
            query = query.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
        End If
        If _filter.NAME_VN <> "" Then
            query = query.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
        End If
        If _filter.ACTFLG <> "" Then
            query = query.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
        End If
        If _filter.REMARK <> "" Then
            query = query.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
        End If
        query = query.OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)

        Return query.ToList

    End Function

    Public Function InsertOtherList(ByVal objOtherList As OtherListDTO, ByVal log As UserLog) As Boolean
        Try
            Dim objOtherListData As New OT_OTHER_LIST
            objOtherListData.ID = Utilities.GetNextSequence(Context, Context.OT_OTHER_LIST.EntitySet.Name)
            objOtherListData.NAME_EN = objOtherList.NAME_EN
            objOtherListData.NAME_VN = objOtherList.NAME_VN
            objOtherListData.TYPE_ID = objOtherList.TYPE_ID
            objOtherListData.CODE = objOtherList.CODE
            objOtherListData.ACTFLG = objOtherList.ACTFLG
            objOtherListData.REMARK = objOtherList.REMARK
            Context.OT_OTHER_LIST.AddObject(objOtherListData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ModifyOtherList(ByVal objOtherList As OtherListDTO, ByVal log As UserLog) As Boolean
        Dim objOtherListData As New OT_OTHER_LIST With {.ID = objOtherList.ID}
        Context.OT_OTHER_LIST.Attach(objOtherListData)
        objOtherListData.NAME_EN = objOtherList.NAME_EN
        objOtherListData.NAME_VN = objOtherList.NAME_VN
        objOtherListData.TYPE_ID = objOtherList.TYPE_ID
        objOtherListData.CODE = objOtherList.CODE
        objOtherListData.REMARK = objOtherList.REMARK
        Context.SaveChanges(log)
        Return True
    End Function


    Public Function ValidateOtherList(ByVal _validate As OtherListDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.OT_OTHER_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.TYPE_ID = _validate.TYPE_ID _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.OT_OTHER_LIST
                             Where p.TYPE_ID = _validate.TYPE_ID _
                             And p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ID <> 0 Then
                    query = (From p In Context.OT_OTHER_LIST
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If

            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ActiveOtherList(ByVal lstOtherList As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        Dim lstOtherListData As List(Of OT_OTHER_LIST)
        lstOtherListData = (From p In Context.OT_OTHER_LIST Where lstOtherList.Contains(p.ID)).ToList
        For index = 0 To lstOtherListData.Count - 1
            lstOtherListData(index).ACTFLG = sActive
        Next
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function DeleteOtherList(ByVal lstOtherList As List(Of OtherListDTO)) As Boolean
        Dim lstOtherListData As List(Of OT_OTHER_LIST)
        Dim lstIDOtherList As List(Of Decimal) = (From p In lstOtherList.ToList Select p.ID).ToList
        Try
            lstOtherListData = (From p In Context.OT_OTHER_LIST Where lstIDOtherList.Contains(p.ID)).ToList
            For index = 0 To lstOtherListData.Count - 1
                Context.OT_OTHER_LIST.DeleteObject(lstOtherListData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "OtherListGroup"

    Public Function GetOtherListGroupBySystem(ByVal systemName As String) As List(Of OtherListGroupDTO)
        ''Dim query As ObjectQuery(Of OtherListGroupDTO)
        'If systemName = "" Then
        '    Dim query = (From p In Context.OT_OTHER_LIST_GROUP
        '           Select New OtherListGroupDTO With {.ID = p.ID, .NAME = p.NAME, .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}).ToList

        '    Return query
        'Else
        '    Dim queryParam As Decimal
        '    'Do đang lấy tất cả danh mục dùng chung lên form profile nên lấy theo id mặc định.
        '    'Select Case systemName.Trim
        '    '    Case SystemCodes.Payroll.ToString()
        '    '        queryParam = SystemCodes.Payroll
        '    '    Case SystemCodes.Attendance.ToString()
        '    '        queryParam = SystemCodes.Attendance
        '    '    Case SystemCodes.Profile.ToString()
        '    '        queryParam = SystemCodes.Profile
        '    '    Case SystemCodes.Recruitment.ToString()
        '    '        queryParam = SystemCodes.Recruitment
        '    '    Case SystemCodes.Training.ToString()
        '    '        queryParam = SystemCodes.Training
        '    'End Select
        '    'Where p.ID = queryParam
        '    Dim query = (From p In Context.OT_OTHER_LIST_GROUP
        '            Where p.ID = 1 Or p.ID = 16 Or p.ID = 17
        '           Select New OtherListGroupDTO With {.ID = p.ID,
        '                                        .NAME = p.NAME,
        '                                        .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}).ToList

        '    Return query
        'End If
        '''
        If systemName = "" Then
            Dim query = (From p In Context.OT_OTHER_LIST_GROUP
                   Select New OtherListGroupDTO With {.ID = p.ID, .NAME = p.NAME, .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}).ToList

            Return query
        Else
            Dim queryParam As Decimal

            Select Case systemName.Trim
                Case SystemCodes.Payroll.ToString()
                    queryParam = SystemCodes.Payroll
                Case SystemCodes.Attendance.ToString()
                    queryParam = SystemCodes.Attendance
                Case SystemCodes.Profile.ToString()
                    queryParam = SystemCodes.Profile
                Case SystemCodes.Training.ToString()
                    queryParam = SystemCodes.Training
                Case SystemCodes.Recruitment.ToString
                    queryParam = SystemCodes.Recruitment
                Case SystemCodes.Performance.ToString
                    queryParam = SystemCodes.Performance
                Case SystemCodes.Organize.ToString
                    queryParam = SystemCodes.Organize
                Case SystemCodes.Insurance.ToString
                    queryParam = SystemCodes.Insurance
            End Select

            Dim query = (From p In Context.OT_OTHER_LIST_GROUP
                         Where p.ACTFLG = "A" And p.ID =queryParam
                   Select New OtherListGroupDTO With {.ID = p.ID,
                                                .NAME = p.NAME,
                                                .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}).ToList

            Return query
        End If
    End Function

    Public Function GetOtherListGroup(ByVal sACT As String) As List(Of OtherListGroupDTO)
        Dim query As ObjectQuery(Of OtherListGroupDTO)
        If sACT = "" Then
            query = (From p In Context.OT_OTHER_LIST_GROUP
                   Select New OtherListGroupDTO With {.ID = p.ID,
                                                .NAME = p.NAME,
                                                .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})

        Else
            query = (From p In Context.OT_OTHER_LIST_GROUP
                   Select New OtherListGroupDTO With {.ID = p.ID,
                                                .NAME = p.NAME,
                                                .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})
        End If
        ''Logger.LogInfo(query)
        Return query.ToList

    End Function

    Public Function InsertOtherListGroup(ByVal objOtherListGroup As OtherListGroupDTO, ByVal log As UserLog) As Boolean
        Dim objOtherListGroupData As New OT_OTHER_LIST_GROUP
        objOtherListGroupData.ID = Utilities.GetNextSequence(Context, Context.OT_OTHER_LIST_GROUP.EntitySet.Name)
        objOtherListGroupData.NAME = objOtherListGroup.NAME
        objOtherListGroupData.ACTFLG = objOtherListGroup.ACTFLG
        objOtherListGroupData.CREATED_DATE = DateTime.Now
        objOtherListGroupData.CREATED_BY = log.Username
        objOtherListGroupData.CREATED_LOG = log.ComputerName
        objOtherListGroupData.MODIFIED_DATE = DateTime.Now
        objOtherListGroupData.MODIFIED_BY = log.Username
        objOtherListGroupData.MODIFIED_LOG = log.ComputerName
        Context.OT_OTHER_LIST_GROUP.AddObject(objOtherListGroupData)
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function ModifyOtherListGroup(ByVal objOtherListGroup As OtherListGroupDTO, ByVal log As UserLog) As Boolean
        Dim objOtherListGroupData As New OT_OTHER_LIST_GROUP With {.ID = objOtherListGroup.ID}
        Context.OT_OTHER_LIST_GROUP.Attach(objOtherListGroupData)
        objOtherListGroupData.ID = objOtherListGroup.ID
        objOtherListGroupData.NAME = objOtherListGroup.NAME
        objOtherListGroupData.MODIFIED_DATE = DateTime.Now
        objOtherListGroupData.MODIFIED_BY = log.Username
        objOtherListGroupData.MODIFIED_LOG = log.ComputerName
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function ActiveOtherListGroup(ByVal lstOtherListGroup() As OtherListGroupDTO, ByVal sActive As String, ByVal log As UserLog) As Boolean
        Dim lstOtherListGroupData As List(Of OT_OTHER_LIST_GROUP)
        Dim lstIDOtherListGroup As List(Of Decimal) = (From p In lstOtherListGroup.ToList Select p.ID).ToList
        lstOtherListGroupData = (From p In Context.OT_OTHER_LIST_GROUP Where lstIDOtherListGroup.Contains(p.ID)).ToList
        For index = 0 To lstOtherListGroupData.Count - 1
            lstOtherListGroupData(index).ACTFLG = sActive
            lstOtherListGroupData(index).MODIFIED_DATE = DateTime.Now
            lstOtherListGroupData(index).MODIFIED_BY = log.Username
            lstOtherListGroupData(index).MODIFIED_LOG = log.ComputerName
        Next
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function DeleteOtherListGroup(ByVal lstOtherListGroup As List(Of OtherListGroupDTO)) As Boolean
        Dim lstOtherListGroupData As List(Of OT_OTHER_LIST_GROUP)
        Dim lstIDOtherListGroup As List(Of Decimal) = (From p In lstOtherListGroup.ToList Select p.ID).ToList
        Try
            lstOtherListGroupData = (From p In Context.OT_OTHER_LIST_GROUP Where lstIDOtherListGroup.Contains(p.ID)).ToList
            For index = 0 To lstOtherListGroupData.Count - 1
                Context.OT_OTHER_LIST_GROUP.DeleteObject(lstOtherListGroupData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "OtherListType"
    Public Function GetOtherListTypeSystem(ByVal systemName As String) As List(Of OtherListTypeDTO)
        Dim query As ObjectQuery(Of OtherListTypeDTO)
        Dim queryParam As Decimal
        If systemName <> "" Then
            Select Case systemName.Trim()
                Case SystemCodes.Attendance.ToString()
                    queryParam = SystemCodes.Attendance
                Case SystemCodes.Payroll.ToString()
                    queryParam = SystemCodes.Payroll
                Case SystemCodes.Profile.ToString()
                    queryParam = SystemCodes.Profile
                Case SystemCodes.Recruitment.ToString()
                    queryParam = SystemCodes.Recruitment
                Case SystemCodes.Training.ToString()
                    queryParam = SystemCodes.Training
            End Select
            query = (From p In Context.OT_OTHER_LIST_TYPE
                     Where p.GROUP_ID = queryParam
                     Order By p.OT_OTHER_LIST_GROUP.NAME, p.NAME
                     Select New OtherListTypeDTO With {.ID = p.ID,
                                                     .NAME = p.NAME,
                                                     .CODE = p.CODE,
                                                     .GROUP_ID = p.GROUP_ID,
                                                     .GROUP_NAME = p.OT_OTHER_LIST_GROUP.NAME,
                                                     .IS_SYSTEM = p.IS_SYSTEM,
                                                       .ACTFLG_DB = p.ACTFLG,
                                                     .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})

        End If
        Return query.ToList
    End Function

    Public Function GetOtherListType(ByVal sACT As String) As List(Of OtherListTypeDTO)
        Dim query As ObjectQuery(Of OtherListTypeDTO)
        If sACT = "" Then
            query = (From p In Context.OT_OTHER_LIST_TYPE
                     Order By p.OT_OTHER_LIST_GROUP.NAME, p.NAME
                   Select New OtherListTypeDTO With {.ID = p.ID,
                                                     .NAME = p.NAME,
                                                     .CODE = p.CODE,
                                                     .GROUP_ID = p.GROUP_ID,
                                                     .GROUP_NAME = p.OT_OTHER_LIST_GROUP.NAME,
                                                     .IS_SYSTEM = p.IS_SYSTEM,
                                                     .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})
        Else
            query = (From p In Context.OT_OTHER_LIST_TYPE
                     Where p.ACTFLG = sACT
                     Order By p.OT_OTHER_LIST_GROUP.NAME, p.NAME
                   Select New OtherListTypeDTO With {.ID = p.ID,
                                                     .NAME = p.NAME,
                                                     .CODE = p.CODE,
                                                     .GROUP_ID = p.GROUP_ID,
                                                     .GROUP_NAME = p.OT_OTHER_LIST_GROUP.NAME,
                                                     .IS_SYSTEM = p.IS_SYSTEM,
                                                     .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})
        End If

        ''Logger.LogInfo(query)
        Return query.ToList

    End Function

    Public Function InsertOtherListType(ByVal objOtherListType As OtherListTypeDTO, ByVal log As UserLog) As Boolean
        Dim objOtherListTypeData As New OT_OTHER_LIST_TYPE
        objOtherListTypeData.ID = Utilities.GetNextSequence(Context, Context.OT_OTHER_LIST_TYPE.EntitySet.Name)
        objOtherListTypeData.CODE = objOtherListType.CODE
        objOtherListTypeData.NAME = objOtherListType.NAME
        objOtherListTypeData.ACTFLG = objOtherListType.ACTFLG
        objOtherListTypeData.GROUP_ID = IIf(objOtherListType.GROUP_ID Is Nothing, Nothing, objOtherListType.GROUP_ID)
        objOtherListTypeData.CREATED_DATE = DateTime.Now
        objOtherListTypeData.CREATED_BY = log.Username
        objOtherListTypeData.CREATED_LOG = log.ComputerName
        objOtherListTypeData.MODIFIED_DATE = DateTime.Now
        objOtherListTypeData.MODIFIED_BY = log.Username
        objOtherListTypeData.MODIFIED_LOG = log.ComputerName
        Context.OT_OTHER_LIST_TYPE.AddObject(objOtherListTypeData)
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function ModifyOtherListType(ByVal objOtherListType As OtherListTypeDTO, ByVal log As UserLog) As Boolean
        Dim objOtherListTypeData As OT_OTHER_LIST_TYPE
        objOtherListTypeData = (From p In Context.OT_OTHER_LIST_TYPE Where p.ID = objOtherListType.ID).SingleOrDefault
        objOtherListTypeData.ID = objOtherListType.ID
        objOtherListTypeData.CODE = objOtherListType.CODE
        objOtherListTypeData.NAME = objOtherListType.NAME
        objOtherListTypeData.GROUP_ID = IIf(objOtherListType.GROUP_ID Is Nothing, Nothing, objOtherListType.GROUP_ID)
        objOtherListTypeData.MODIFIED_DATE = DateTime.Now
        objOtherListTypeData.MODIFIED_BY = log.Username
        objOtherListTypeData.MODIFIED_LOG = log.ComputerName
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function ActiveOtherListType(ByVal lstOtherListType() As OtherListTypeDTO, ByVal sActive As String, ByVal log As UserLog) As Boolean
        Dim lstOtherListTypeData As List(Of OT_OTHER_LIST_TYPE)
        Dim lstIDOtherListType As List(Of Decimal) = (From p In lstOtherListType.ToList Select p.ID).ToList
        lstOtherListTypeData = (From p In Context.OT_OTHER_LIST_TYPE Where lstIDOtherListType.Contains(p.ID)).ToList
        For index = 0 To lstOtherListTypeData.Count - 1
            lstOtherListTypeData(index).ACTFLG = sActive
            lstOtherListTypeData(index).MODIFIED_DATE = DateTime.Now
            lstOtherListTypeData(index).MODIFIED_BY = log.Username
            lstOtherListTypeData(index).MODIFIED_LOG = log.ComputerName
        Next
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function DeleteOtherListType(ByVal lstOtherListType As List(Of OtherListTypeDTO)) As Boolean
        Dim lstOtherListTypeData As List(Of OT_OTHER_LIST_TYPE)
        Dim lstIDOtherListType As List(Of Decimal) = (From p In lstOtherListType.ToList Select p.ID).ToList
        Try
            lstOtherListTypeData = (From p In Context.OT_OTHER_LIST_TYPE Where lstIDOtherListType.Contains(p.ID)).ToList
            For index = 0 To lstOtherListTypeData.Count - 1
                Context.OT_OTHER_LIST_TYPE.DeleteObject(lstOtherListTypeData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Get Combo List Data"

    ''' <summary>
    ''' Lấy dữ liệu cho combobox
    ''' </summary>
    ''' <param name="_combolistDTO">Trả về dữ liệu combobox</param>
    ''' <returns>TRUE: Success</returns>
    ''' <remarks></remarks>
    Public Function GetComboList(ByRef _combolistDTO As ComboBoxDataDTO) As Boolean
        Dim query
        Try
            If _combolistDTO.GET_MODULE Then
                query = (From p In Context.SE_MODULE Order By p.NAME.ToUpper
                         Select New ModuleDTO With {
                             .ID = p.ID,
                             .MID = p.MID,
                             .NAME = p.NAME}).ToList
                _combolistDTO.LIST_MODULE = query
            End If
            If _combolistDTO.GET_FUNCTION_GROUP Then
                query = (From p In Context.SE_FUNCTION_GROUP Order By p.NAME.ToUpper
                         Select New FunctionGroupDTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME = p.NAME}).ToList
                _combolistDTO.LIST_FUNCTION_GROUP = query
            End If

            If _combolistDTO.GET_USER_GROUP Then
                query = (From p In Context.SE_GROUP Order By p.NAME.ToUpper
                         Select New GroupDTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME = p.NAME}).ToList
                _combolistDTO.LIST_USER_GROUP = query
            End If

            If _combolistDTO.GET_FUNCTION Then
                query = (From p In Context.SE_FUNCTION Order By p.NAME.ToUpper
                         Select New FunctionDTO With {
                             .ID = p.ID,
                             .FID = p.FID,
                             .NAME = p.NAME}).ToList
                _combolistDTO.LIST_FUNCTION = query
            End If

            If _combolistDTO.GET_ACTION_NAME Then
                query = (From p In Context.OT_OTHER_LIST
                        Where p.OT_OTHER_LIST_TYPE.CODE = "ACTION_TYPE"
                        Order By p.NAME_VN.ToUpper
                         Select New OtherListDTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_VN = p.NAME_VN,
                             .NAME_EN = p.NAME_EN}).ToList
                _combolistDTO.LIST_ACTION_NAME = query
            End If

            If _combolistDTO.GET_OTHER_LIST_GROUP Then
                query = (From p In Context.OT_OTHER_LIST_GROUP
                        Order By p.NAME.ToUpper
                         Select New OtherListGroupDTO With {
                             .ID = p.ID,
                             .NAME = p.NAME}).ToList
                _combolistDTO.LIST_OTHER_LIST_GROUP = query
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetAllTrCertificateList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_ALL_TR_CERTIFICATE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetCourseByList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TR_COURSE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetPeriodYear(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_LIST.GET_PERIOD_YEAR",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetPeriodByYear(ByVal isBlank As Boolean, ByVal year As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_LIST.GET_PERIOD_BY_YEAR",
                                           New With {.P_ISBLANK = isBlank,
                                                     .PERIOD_YEAR = year,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetClassification(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_LIST.GET_CLASSIFICATION",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetLearningLevel(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_INTEGRATED.GET_LEARNING_LEVEL",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetHU_CompetencyList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_ALL_HU_COMPETENCY",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "SendMail"

    ''' <summary>
    ''' Gọi hàm gửi mail queue trong DB
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SendMail() As Boolean
        'Dim sMail As System.Net.Mail.SmtpClient
        Dim _server As String
        Dim _port As String
        Dim _account As String
        Dim _password As String
        Dim _status As String
        Dim _ssl As Boolean
        Dim _authen As Boolean
        Try

            Dim config As Dictionary(Of String, String)
            config = GetConfig(ModuleID.All)
            _server = If(config.ContainsKey("MailServer"), config("MailServer"), "")
            _port = If(config.ContainsKey("MailPort"), config("MailPort"), "")
            _account = If(config.ContainsKey("MailAccount"), config("MailAccount"), "")
            _password = If(config.ContainsKey("MailAccountPassword"), config("MailAccountPassword"), "")
            _ssl = If(config.ContainsKey("MailIsSSL"), config("MailIsSSL"), 0)
            _authen = If(config.ContainsKey("MailIsAuthen"), config("MailIsAuthen"), 0)
            If _password <> "" Then _password = New EncryptData().DecryptString(_password)
            If _server = "" Then Return False

            'Dim lstAtt = (From p In Context.SE_MAIL
            '              Select p
            '              Where p.ACTFLG = "A").ToList

            'For Each itm In lstAtt
            '    If itm.ATTACHMENT <> "" Then
            '        For Each item In itm.ATTACHMENT.Split(";")
            '            If File.Exists(item) Then

            '                Try

            '                    File.Delete(item)
            '                Catch ex As Exception
            '                    WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCommon")

            '                End Try
            '            End If
            '        Next
            '    End If
            'Next

            Dim query = (From p In Context.SE_MAIL Select p
                         Where p.ACTFLG = "I" Order By p.ACTFLG
                         Skip 0 Take 50).ToList

            If query.Count = 0 Then Return True

            'Config mail server

            Using sMail As New System.Net.Mail.SmtpClient(_server)
                Try
                    If _port <> "" Then
                        sMail.Port = _port
                    End If
                    sMail.EnableSsl = _ssl
                    If _authen Then
                        sMail.Credentials = New NetworkCredential(_account, _password)
                    End If

                    For i = 0 To query.Count - 1
                        Try
                            Using message As New System.Net.Mail.MailMessage()
                                _status = "A"
                                message.From = New System.Net.Mail.MailAddress(query(i).MAIL_FROM)
                                For Each item In query(i).MAIL_TO.Split(";")
                                    If item.Trim <> "" Then
                                        Try
                                            message.To.Add(New System.Net.Mail.MailAddress(item))
                                        Catch ex As Exception

                                        End Try
                                    End If
                                Next
                                'Try
                                '    message.Bcc.Add(New System.Net.Mail.MailAddress(query(i).MAIL_FROM))

                                'Catch ex As Exception

                                'End Try
                                If query(i).MAIL_CC IsNot Nothing Then
                                    For Each item In query(i).MAIL_CC.Split(";")
                                        If item.Trim <> "" Then
                                            Try
                                                message.CC.Add(New System.Net.Mail.MailAddress(item))

                                            Catch ex As Exception

                                            End Try
                                        End If
                                    Next
                                End If


                                message.IsBodyHtml = True
                                message.BodyEncoding = ASCIIEncoding.UTF8
                                message.Subject = query(i).SUBJECT
                                message.Priority = System.Net.Mail.MailPriority.Normal
                                message.Body = query(i).CONTENT

                                If query(i).ATTACHMENT IsNot Nothing Then
                                    For Each item In query(i).ATTACHMENT.Split(";")
                                        If item.Trim <> "" Then
                                            If File.Exists(item.Trim) Then
                                                Dim attachment As System.Net.Mail.Attachment
                                                attachment = New System.Net.Mail.Attachment(item)
                                                message.Attachments.Add(attachment)
                                            End If
                                        End If
                                    Next
                                End If


                                sMail.Send(message)
                                For Each att In message.Attachments
                                    att.Dispose()
                                Next

                            End Using

                            If query(i).ATTACHMENT IsNot Nothing Then
                                For Each item In query(i).ATTACHMENT.Split(";")
                                    If item.Trim <> "" Then
                                        Try
                                            File.Delete(item.Trim)
                                        Catch ex As Exception
                                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCommon")

                                        End Try
                                    End If
                                Next
                            End If
                        Catch ex As Exception
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCommon")
                            'Logger.LogError(ex)
                            _status = "E"
                        End Try
UpdateMail:
                        Dim id As Decimal = query(i).ID
                        Dim _editMail = (From p In Context.SE_MAIL Where p.ID = id Select p).SingleOrDefault
                        _editMail.ACTFLG = _status
                    Next
                    Context.SaveChanges()
                    Return True
                Catch ex As Exception
                Finally
                    If sMail IsNot Nothing Then
                        sMail.Dispose()
                    End If
                End Try
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCommon")
            'Logger.LogError(ex)
        End Try
    End Function

    Public Function InsertMail(ByVal _from As String, ByVal _to As String, ByVal _subject As String, ByVal _content As String, Optional ByVal _cc As String = "", Optional ByVal _bcc As String = "", Optional ByVal _viewName As String = "")
        Try
            Dim _newMail As New SE_MAIL
            _newMail.ID = Utilities.GetNextSequence(Context, Context.SE_MAIL.EntitySet.Name)
            _newMail.MAIL_FROM = _from
            _newMail.MAIL_TO = _to
            _newMail.MAIL_CC = _cc
            _newMail.MAIL_BCC = _bcc
            _newMail.SUBJECT = _subject
            _newMail.CONTENT = _content
            _newMail.VIEW_NAME = _viewName
            _newMail.ACTFLG = "I"
            Context.SE_MAIL.AddObject(_newMail)
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Ldap"

    Public Function GetLdap(ByVal _filter As LdapDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "LDAP_NAME desc") As List(Of LdapDTO)

        Try
            Dim query = From p In Context.SE_LDAP
                       Select New LdapDTO With {
                                  .ID = p.ID,
                                  .BASE_DN = p.BASE_DN,
                                  .DOMAIN_NAME = p.DOMAIN_NAME,
                                  .LDAP_NAME = p.LDAP_NAME}

            Dim lst = query

            If _filter.BASE_DN <> "" Then
                lst = lst.Where(Function(p) p.BASE_DN.ToUpper.Contains(_filter.BASE_DN.ToUpper))
            End If
            If _filter.DOMAIN_NAME <> "" Then
                lst = lst.Where(Function(p) p.DOMAIN_NAME.ToUpper.Contains(_filter.DOMAIN_NAME.ToUpper))
            End If
            If _filter.LDAP_NAME <> "" Then
                lst = lst.Where(Function(p) p.LDAP_NAME.ToUpper.Contains(_filter.LDAP_NAME.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function InsertLdap(ByVal objLdap As LdapDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLdapData As New SE_LDAP
        Dim iCount As Integer = 0
        Try
            objLdapData.ID = Utilities.GetNextSequence(Context, Context.SE_LDAP.EntitySet.Name)
            objLdapData.BASE_DN = objLdap.BASE_DN
            objLdapData.DOMAIN_NAME = objLdap.DOMAIN_NAME
            objLdapData.LDAP_NAME = objLdap.LDAP_NAME
            Context.SE_LDAP.AddObject(objLdapData)
            Context.SaveChanges(log)
            gID = objLdapData.ID
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ModifyLdap(ByVal objLdap As LdapDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLdapData As SE_LDAP
        Try
            objLdapData = (From p In Context.SE_LDAP Where p.ID = objLdap.ID).FirstOrDefault
            objLdapData.BASE_DN = objLdap.BASE_DN
            objLdapData.DOMAIN_NAME = objLdap.DOMAIN_NAME
            objLdapData.LDAP_NAME = objLdap.LDAP_NAME
            Context.SaveChanges(log)
            gID = objLdapData.ID
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteLdap(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstLdapData As List(Of SE_LDAP)
        Try

            lstLdapData = (From p In Context.SE_LDAP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstLdapData.Count - 1
                Context.SE_LDAP.DeleteObject(lstLdapData(index))
            Next

            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function


#End Region

#Region "Validate ID Table"
    Public Function ValidateComboboxActive(ByVal tableName As String, ByVal colName As String, ByVal ID As Decimal) As Boolean
        Try
            Dim count As Decimal = 0
            Dim Sql = "SELECT COUNT(" & colName & ") FROM " & tableName & " WHERE " & colName & " = " & ID & " and ACTFLG = 'A'"
            count = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault
            If count > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCommon")
            Throw ex
        End Try
    End Function
    Public Function ValidateIDTable(ByVal tableName As String, ByVal colName As String, ByVal strListID As String)
        Try
            Dim count As Decimal = 0
            Dim Sql = "SELECT COUNT(" & colName & ") FROM " & tableName & " WHERE " & colName & " IN (" & strListID & ")"
            count = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault
            If count > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCommon")
            Throw ex
        End Try
    End Function
    Public Function CheckExistIDTable(ByVal lstID As List(Of Decimal), ByVal table As String, ByVal column As String) As Boolean

        Try
            Dim strListID As String = lstID.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
            Return ValidateIDTable(table, column, strListID)
        Catch ex As Exception
        End Try
        Return True
    End Function
    Public Function CheckExistValue(ByVal lstID As List(Of String), ByVal table As String, ByVal column As String) As Boolean

        Try
            Dim strListID As String = lstID.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
            Return ValidateIDTable(table, column, strListID)
        Catch ex As Exception
        End Try
        Return True
    End Function
    Public Function CheckWorkStatus(ByVal colName As String, ByVal Value As String) As Boolean
        Try
            Dim count As Decimal = 0
            Dim Sql = "SELECT COUNT(" & colName & ") FROM HU_EMPLOYEE WHERE (WORK_STATUS = 257 and TER_EFFECT_DATE <= sysdate) and " & colName & " = (" & Value & ")"
            count = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault
            If count > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCommon")
            Throw ex
        End Try
    End Function

    Public Function CheckExistProgram(ByVal strTableName As String, ByVal strcolName As String, ByVal strValue As String) As Boolean
        Try
            Dim count As Decimal = 0
            Dim Sql = "SELECT COUNT(" & strcolName & ") FROM " & strTableName & "  WHERE " & strcolName & " = " & strValue
            count = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault
            If count > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCommon")
            Throw ex
        End Try
    End Function

    Public Function AutoGenColID(ByVal strTableName As String, ByVal strColName As String) As Decimal
        Dim strRelExcute As String = String.Empty
        Try
            strRelExcute = Context.ExecuteStoreQuery(Of Decimal)("SELECT MAX(" & strColName & ") FROM " & strTableName).FirstOrDefault
            If String.IsNullOrEmpty(strRelExcute) Then
                Return 1
            Else
                Return Convert.ToDecimal(strRelExcute) + 1
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCommon")
            Throw ex
        End Try
    End Function

#End Region
#Region "DynamicControl"
    Public Function GetListControl(ByVal KeyView As String) As DataTable

        Using cls As New DataAccess.QueryData
            Dim valuereturn = New DataTable
            valuereturn = cls.ExecuteStore("PKG_COMMON_LIST.GET_DYNAMIC_CONTROL",
                                       New With {.P_CODENAME = KeyView,
                                                 .P_CUR = cls.OUT_CURSOR})
            Return valuereturn
        End Using
    End Function

    Public Function Insert_Edit_Dynamic_Control(ByVal KeyView As String, ByVal dataView As String) As DataTable
        Using cls As New DataAccess.QueryData
            Dim valuereturn = New DataTable
            valuereturn = cls.ExecuteStore("PKG_COMMON_LIST.Insert_Edit_DYNAMIC_CONTROL",
                                       New With {.P_CODENAME = KeyView,
                                                 .P_DATAVIEW = dataView,
                                                 .P_CUR = cls.OUT_CURSOR})
            Return valuereturn
        End Using
    End Function
#End Region
#Region "Config Display view"
    Public Function GetConfigView(ByVal KeyView As String) As DataTable

        Using cls As New DataAccess.QueryData
            Dim valuereturn = New DataTable
            valuereturn = cls.ExecuteStore("PKG_COMMON_LIST.GET_VIEW_CONFIG",
                                       New With {.P_CODENAME = KeyView,
                                                 .P_CUR = cls.OUT_CURSOR})
            Return valuereturn
        End Using
    End Function
#End Region

End Class

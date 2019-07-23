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

#Region "salary group"

    ''' <summary>
    ''' Lay data vao combo cho bang luong
    ''' </summary>
    ''' <param name="dateValue">Ma bang luong</param>
    ''' <param name="isBlank">0: Khong lay dong empty; 1: Co lay dong empty</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSalaryGroupCombo(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_SAL_GROUP",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_DATE = dateValue,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetSalaryRankCombo(ByVal SalaryLevel As Decimal, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_SAL_RANK",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_SAL_GROUP = SalaryLevel,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetSalaryLevelCombo(ByVal SalaryGroup As Decimal, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_SAL_LEVEL",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_SAL_GROUP = SalaryGroup,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetSalaryLevelComboNotByGroup(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_SAL_LEVEL",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_SAL_GROUP = 0,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "Title"

    Public Function GetTitle(ByVal _filter As TitleDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleDTO)

        Try
            Dim query = From p In Context.HU_TITLE
                        From group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_GROUP_ID).DefaultIfEmpty
                        From orgLv In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From orgType In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.ORG_TYPE).DefaultIfEmpty
                        Select New TitleDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME_EN = p.NAME_EN,
                                   .NAME_VN = p.NAME_VN,
                                   .REMARK = p.REMARK,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                   .TITLE_GROUP_ID = p.TITLE_GROUP_ID,
                                   .TITLE_GROUP_NAME = group.NAME_VN,
                                   .ORG_ID = p.ORG_ID,
                                   .ORG_ID_NAME = orgLv.NAME_VN,
                                   .ORG_TYPE = p.ORG_TYPE,
                                   .ORG_TYPE_NAME = orgType.NAME_VN,
                                   .HURTFUL_CHECK = If(p.HURTFUL = -1, True, False),
                                   .OVT_CHECK = If(p.OVT = -1, True, False),
                                   .SPEC_HURFUL_CHECK = If(p.SPEC_HURFUL = -1, True, False),
                                   .UPLOAD_FILE = p.UPLOAD_FILE,
                                   .FILENAME = p.FILENAME}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.TITLE_GROUP_NAME.ToUpper.Contains(_filter.TITLE_GROUP_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetTitleID(ByVal ID As Decimal) As TitleDTO

        Try
            Dim query = (From p In Context.HU_TITLE
                         From group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_GROUP_ID).DefaultIfEmpty
                         From work In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.WORK_INVOLVE_ID).DefaultIfEmpty
                         From title In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.LEVEL_ID).DefaultIfEmpty
                         Where p.ID = ID
                         Select New TitleDTO With {
                                    .ID = p.ID,
                                    .CODE = p.CODE,
                                    .NAME_EN = p.NAME_EN,
                                    .NAME_VN = p.NAME_VN,
                                    .REMARK = p.REMARK,
                                    .CREATED_DATE = p.CREATED_DATE,
                                    .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                    .TITLE_GROUP_ID = p.TITLE_GROUP_ID,
                                    .TITLE_GROUP_NAME = group.NAME_VN,
                                    .WORK_INVOLVE_NAME = work.NAME_VN,
                                    .LEVEL_TITLE_NAME = title.NAME_VN,
                                    .DRIVE_INFOR = p.DRIVE_INFOR,
                                    .DRIVE_INFOR_CHECK = If(p.DRIVE_INFOR = "-1", True, False),
                                    .WORK_INVOLVE_ID = p.WORK_INVOLVE_ID,
                                    .OVT = p.OVT,
                                    .OVT_CHECK = If(p.OVT = "-1", True, False),
                                    .UPLOAD_FILE = p.UPLOAD_FILE,
                                    .LEVEL_ID = p.LEVEL_ID}).SingleOrDefault

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function InsertTitle(ByVal objTitle As TitleDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_TITLE
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.HU_TITLE.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.TITLE_GROUP_ID = objTitle.TITLE_GROUP_ID
            objTitleData.HURTFUL = objTitle.HURTFUL
            objTitleData.SPEC_HURFUL = objTitle.SPEC_HURFUL
            objTitleData.OVT = objTitle.OVT
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.ORG_TYPE = objTitle.ORG_TYPE
            objTitleData.UPLOAD_FILE = objTitle.UPLOAD_FILE
            objTitleData.FILENAME = objTitle.FILENAME
            Context.HU_TITLE.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateTitle(ByVal _validate As TitleDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_TITLE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_TITLE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_TITLE
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_TITLE
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyTitle(ByVal objTitle As TitleDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As HU_TITLE
        Try
            objTitleData = (From p In Context.HU_TITLE Where p.ID = objTitle.ID).FirstOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.TITLE_GROUP_ID = objTitle.TITLE_GROUP_ID
            objTitleData.HURTFUL = objTitle.HURTFUL
            objTitleData.SPEC_HURFUL = objTitle.SPEC_HURFUL
            objTitleData.OVT = objTitle.OVT
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.ORG_TYPE = objTitle.ORG_TYPE
            objTitleData.UPLOAD_FILE = objTitle.UPLOAD_FILE
            objTitleData.FILENAME = objTitle.FILENAME
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveTitle(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstTitleData As List(Of HU_TITLE)
        Dim lstOrgTitle As List(Of HU_ORG_TITLE)
        Try
            lstTitleData = (From p In Context.HU_TITLE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                lstTitleData(index).ACTFLG = sActive
                Dim id As Decimal = lstTitleData(index).ID
                lstOrgTitle = (From p In Context.HU_ORG_TITLE Where p.TITLE_ID = id And p.ACTFLG <> sActive).ToList
                For i = 0 To lstOrgTitle.Count - 1
                    lstOrgTitle(i).ACTFLG = sActive
                Next
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteTitle(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstTitleData As List(Of HU_TITLE)
        Try

            lstTitleData = (From p In Context.HU_TITLE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                Context.HU_TITLE.DeleteObject(lstTitleData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Get title by ID
    ''' </summary>
    ''' <param name="sID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTitleByID(ByVal sID As Decimal) As List(Of TitleDTO)

        Try
            Dim query = (From p In Context.HU_TITLE.ToList Where p.ID = sID
                         Select New TitleDTO With {.ID = p.ID,
                                                  .CODE = p.CODE,
                                                  .NAME_EN = p.NAME_EN,
                                                   .NAME_VN = p.NAME_VN,
                                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try



    End Function
#End Region

#Region "TitleConcurrent"
    Public Function GetTitleConcurrent1(ByVal _filter As TitleConcurrentDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of TitleConcurrentDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_TITLE_CONCURRENT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)
                        Select New TitleConcurrentDTO With {
                                   .ID = p.ID,
                                   .ORG_ID = p.ORG_ID,
                                   .ORG_NAME = org.NAME_VN,
                                   .TITLE_ID = p.TITLE_ID,
                                   .NAME = title.NAME_VN,
                                   .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                   .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                   .EMPLOYEE_NAME = e.FULLNAME_VN,
                                   .DECISION_NO = p.DECISION_NO,
                                   .EFFECT_DATE = p.EFFECT_DATE,
                                   .EXPIRE_DATE = p.EXPIRE_DATE,
                                   .NOTE = p.NOTE,
                                   .CREATED_DATE = p.CREATED_DATE}
            Dim lst = query
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetTitleConcurrent(ByVal _filter As TitleConcurrentDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleConcurrentDTO)

        Try

            Dim query = From p In Context.HU_TITLE_CONCURRENT
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        Where p.EMPLOYEE_ID = _filter.EMPLOYEE_ID
                        Select New TitleConcurrentDTO With {
                                   .ID = p.ID,
                                   .ORG_ID = p.ORG_ID,
                                   .ORG_NAME = org.NAME_VN,
                                   .TITLE_ID = p.TITLE_ID,
                                   .NAME = p.NAME,
                                   .EFFECT_DATE = p.EFFECT_DATE,
                                   .EXPIRE_DATE = p.EXPIRE_DATE,
                                   .NOTE = p.NOTE,
                                   .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertTitleConcurrent(ByVal objTitleConcurrent As TitleConcurrentDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleConcurrentData As New HU_TITLE_CONCURRENT
        Dim iCount As Integer = 0
        Try
            objTitleConcurrentData.ID = Utilities.GetNextSequence(Context, Context.HU_TITLE_CONCURRENT.EntitySet.Name)
            objTitleConcurrentData.ORG_ID = objTitleConcurrent.ORG_ID
            objTitleConcurrentData.TITLE_ID = objTitleConcurrent.TITLE_ID
            objTitleConcurrentData.NAME = objTitleConcurrent.NAME
            objTitleConcurrentData.EFFECT_DATE = objTitleConcurrent.EFFECT_DATE
            objTitleConcurrentData.EXPIRE_DATE = objTitleConcurrent.EXPIRE_DATE
            objTitleConcurrentData.EMPLOYEE_ID = objTitleConcurrent.EMPLOYEE_ID
            objTitleConcurrentData.NOTE = objTitleConcurrent.NOTE
            objTitleConcurrentData.DECISION_NO = objTitleConcurrent.DECISION_NO
            Context.HU_TITLE_CONCURRENT.AddObject(objTitleConcurrentData)
            Context.SaveChanges(log)
            gID = objTitleConcurrentData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyTitleConcurrent(ByVal objTitleConcurrent As TitleConcurrentDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleConcurrentData As HU_TITLE_CONCURRENT
        Try
            objTitleConcurrentData = (From p In Context.HU_TITLE_CONCURRENT Where p.ID = objTitleConcurrent.ID).FirstOrDefault
            objTitleConcurrentData.ORG_ID = objTitleConcurrent.ORG_ID
            objTitleConcurrentData.TITLE_ID = objTitleConcurrent.TITLE_ID
            objTitleConcurrentData.NAME = objTitleConcurrent.NAME
            objTitleConcurrentData.EFFECT_DATE = objTitleConcurrent.EFFECT_DATE
            objTitleConcurrentData.EXPIRE_DATE = objTitleConcurrent.EXPIRE_DATE
            objTitleConcurrentData.EMPLOYEE_ID = objTitleConcurrent.EMPLOYEE_ID
            objTitleConcurrentData.NOTE = objTitleConcurrent.NOTE
            objTitleConcurrentData.DECISION_NO = objTitleConcurrent.DECISION_NO
            Context.SaveChanges(log)
            gID = objTitleConcurrentData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteTitleConcurrent(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstTitleConcurrentData As List(Of HU_TITLE_CONCURRENT)
        Try
            lstTitleConcurrentData = (From p In Context.HU_TITLE_CONCURRENT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleConcurrentData.Count - 1
                Context.HU_TITLE_CONCURRENT.DeleteObject(lstTitleConcurrentData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "ContractType"

    Public Function GetContractType(ByVal _filter As ContractTypeDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ContractTypeDTO)

        Try

            Dim query = From p In Context.HU_CONTRACT_TYPE
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID).DefaultIfEmpty

            Dim lst = query.Select(Function(f) New ContractTypeDTO With {
                                       .ID = f.p.ID,
                                       .CODE = f.p.CODE,
                                       .PERIOD = f.p.PERIOD,
                                       .REMARK = f.p.REMARK,
                                       .NAME = f.p.NAME,
                                       .TYPE_ID = f.p.TYPE_ID,
                                       .TYPE_NAME = f.ot.NAME_VN,
                                       .ACTFLG = If(f.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .CREATED_DATE = f.p.CREATED_DATE})
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.PERIOD <> 0 Then
                lst = lst.Where(Function(p) p.PERIOD = _filter.PERIOD)
            End If

            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function InsertContractType(ByVal objContractType As ContractTypeDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objContractTypeData As New HU_CONTRACT_TYPE
        Try
            objContractTypeData.ID = Utilities.GetNextSequence(Context, Context.HU_CONTRACT_TYPE.EntitySet.Name)
            objContractTypeData.CODE = objContractType.CODE.Trim
            objContractTypeData.NAME = objContractType.NAME.Trim
            objContractTypeData.REMARK = objContractType.REMARK.Trim
            objContractTypeData.PERIOD = objContractType.PERIOD
            objContractTypeData.TYPE_ID = objContractType.TYPE_ID
            objContractTypeData.ACTFLG = objContractType.ACTFLG
            Context.HU_CONTRACT_TYPE.AddObject(objContractTypeData)
            Context.SaveChanges(log)
            gID = objContractTypeData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateContractType(ByVal _validate As ContractTypeDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_CONTRACT_TYPE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_CONTRACT_TYPE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_CONTRACT_TYPE
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_CONTRACT_TYPE
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyContractType(ByVal objContractType As ContractTypeDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractTypeData As New HU_CONTRACT_TYPE With {.ID = objContractType.ID}
        Try
            objContractTypeData = (From p In Context.HU_CONTRACT_TYPE Where p.ID = objContractType.ID).FirstOrDefault
            objContractTypeData.ID = objContractType.ID
            objContractTypeData.CODE = objContractType.CODE.Trim
            objContractTypeData.NAME = objContractType.NAME.Trim
            objContractTypeData.PERIOD = objContractType.PERIOD
            objContractTypeData.REMARK = objContractType.REMARK.Trim
            objContractTypeData.TYPE_ID = objContractType.TYPE_ID
            Context.SaveChanges(log)
            gID = objContractTypeData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function ActiveContractType(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstContractTypeData As List(Of HU_CONTRACT_TYPE)
        Try
            lstContractTypeData = (From p In Context.HU_CONTRACT_TYPE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstContractTypeData.Count - 1
                lstContractTypeData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteContractType(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstContractTypeData As List(Of HU_CONTRACT_TYPE)
        Try
            lstContractTypeData = (From p In Context.HU_CONTRACT_TYPE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstContractTypeData.Count - 1
                Context.HU_CONTRACT_TYPE.DeleteObject(lstContractTypeData(index))
            Next
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "WelfareList"

    Public Function GetWelfareList(ByVal _filter As WelfareListDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WelfareListDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _filter.param.ORG_ID,
                                           .P_ISDISSOLVE = _filter.param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_WELFARE_LIST
                        From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = p.ORG_ID)
                         From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_GROUP_ID).DefaultIfEmpty
                        From org In Context.SE_CHOSEN_ORG.Where(Function(org) org.ORG_ID = o.ID And
                                                                    org.USERNAME = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New WelfareListDTO With {
                                       .ID = p.p.ID,
                                       .CODE = p.p.CODE,
                                       .NAME = p.p.NAME,
                                       .CONTRACT_TYPE = p.p.CONTRACT_TYPE,
                                       .CONTRACT_TYPE_NAME = p.p.CONTRACT_TYPE_NAME,
                                       .GENDER = p.p.GENDER,
                                       .TITLE_GROUP_ID = p.p.TITLE_GROUP_ID,
                                       .TITLE_GROUP_NAME = p.ot.NAME_VN,
                                       .GENDER_NAME = p.p.GENDER_NAME,
                                       .SENIORITY = p.p.SENIORITY,
                                       .CHILD_OLD_FROM = p.p.CHILD_OLD_FROM,
                                       .CHILD_OLD_TO = p.p.CHILD_OLD_TO,
                                       .MONEY = p.p.MONEY,
                                       .START_DATE = p.p.START_DATE,
                                       .END_DATE = p.p.END_DATE,
                                       .IS_AUTO = p.p.IS_AUTO,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .ID_NAME = p.p.ID_NAME})

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.CONTRACT_TYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.CONTRACT_TYPE_NAME.ToUpper.Contains(_filter.CONTRACT_TYPE_NAME.ToUpper))
            End If
            If _filter.GENDER_NAME <> "" Then
                lst = lst.Where(Function(p) p.GENDER_NAME.ToUpper.Contains(_filter.GENDER_NAME.ToUpper))
            End If
            If _filter.SENIORITY <> 0 Then
                lst = lst.Where(Function(p) p.SENIORITY = _filter.SENIORITY)
            End If
            If _filter.MONEY <> 0 Then
                lst = lst.Where(Function(p) p.MONEY = _filter.MONEY)
            End If
            If _filter.SENIORITY <> 0 Then
                lst = lst.Where(Function(p) p.SENIORITY = _filter.SENIORITY)
            End If
            If _filter.START_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.START_DATE = _filter.START_DATE)
            End If
            If _filter.END_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.END_DATE = _filter.END_DATE)
            End If
            If _filter.IS_AUTO IsNot Nothing Then
                lst = lst.Where(Function(p) p.IS_AUTO = _filter.IS_AUTO)
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            If _filter.ORG_ID <> 0 Then
                lst = lst.Where(Function(p) p.ORG_ID = _filter.ORG_ID)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function InsertWelfareList(ByVal objWelfareList As WelfareListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objWelfareListData As New HU_WELFARE_LIST
        Try
            objWelfareListData.ID = Utilities.GetNextSequence(Context, Context.HU_WELFARE_LIST.EntitySet.Name)
            objWelfareListData.CODE = objWelfareList.CODE.Trim
            objWelfareListData.NAME = objWelfareList.NAME.Trim
            objWelfareListData.CONTRACT_TYPE = objWelfareList.CONTRACT_TYPE
            objWelfareListData.CONTRACT_TYPE_NAME = objWelfareList.CONTRACT_TYPE_NAME
            objWelfareListData.GENDER = objWelfareList.GENDER
            objWelfareListData.TITLE_GROUP_ID = objWelfareList.TITLE_GROUP_ID
            objWelfareListData.GENDER_NAME = objWelfareList.GENDER_NAME
            objWelfareListData.SENIORITY = objWelfareList.SENIORITY
            objWelfareListData.CHILD_OLD_FROM = objWelfareList.CHILD_OLD_FROM
            objWelfareListData.CHILD_OLD_TO = objWelfareList.CHILD_OLD_TO
            objWelfareListData.MONEY = objWelfareList.MONEY
            objWelfareListData.START_DATE = objWelfareList.START_DATE
            objWelfareListData.END_DATE = objWelfareList.END_DATE
            objWelfareListData.IS_AUTO = objWelfareList.IS_AUTO
            objWelfareListData.ACTFLG = objWelfareList.ACTFLG
            objWelfareListData.ORG_ID = objWelfareList.ORG_ID
            objWelfareListData.ID_NAME = objWelfareList.ID_NAME
            Context.HU_WELFARE_LIST.AddObject(objWelfareListData)
            Context.SaveChanges(log)
            gID = objWelfareListData.ID
            If objWelfareList.GENDER IsNot Nothing Then
                Dim lstT = objWelfareList.CONTRACT_TYPE.Split(",")
                If lstT IsNot Nothing Then
                    For Each s As String In lstT
                        Dim obj As New HU_WELFARE_LIST_GW
                        obj.ID = Utilities.GetNextSequence(Context, Context.HU_WELFARE_LIST.EntitySet.Name)
                        obj.HU_WELFARE_LIST_ID = gID
                        obj.CONTRACT_TYPE_ID = Utilities.Obj2Decima(s)
                        Context.HU_WELFARE_LIST_GW.AddObject(obj)
                    Next
                End If
            End If

            If objWelfareList.GENDER IsNot Nothing Then
                Dim lstG = objWelfareList.GENDER.Split(",")
                For Each s As String In lstG
                    Dim obj As New HU_WELFARE_LIST_GW
                    obj.ID = Utilities.GetNextSequence(Context, Context.HU_WELFARE_LIST.EntitySet.Name)
                    obj.HU_WELFARE_LIST_ID = gID
                    obj.GENDER_ID = Utilities.Obj2Decima(s)
                    Context.HU_WELFARE_LIST_GW.AddObject(obj)
                Next
            End If

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateWelfareList(ByVal _validate As WelfareListDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_WELFARE_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_WELFARE_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    Public Function ModifyWelfareList(ByVal objWelfareList As WelfareListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objWelfareListData As New HU_WELFARE_LIST With {.ID = objWelfareList.ID}
        Try
            objWelfareListData = (From p In Context.HU_WELFARE_LIST Where p.ID = objWelfareList.ID).FirstOrDefault
            objWelfareListData.ID = objWelfareList.ID
            objWelfareListData.CODE = objWelfareList.CODE.Trim
            objWelfareListData.NAME = objWelfareList.NAME.Trim
            objWelfareListData.CONTRACT_TYPE = objWelfareList.CONTRACT_TYPE
            objWelfareListData.CONTRACT_TYPE_NAME = objWelfareList.CONTRACT_TYPE_NAME
            objWelfareListData.GENDER = objWelfareList.GENDER
            objWelfareListData.GENDER_NAME = objWelfareList.GENDER_NAME
            objWelfareListData.SENIORITY = objWelfareList.SENIORITY
            objWelfareListData.CHILD_OLD_FROM = objWelfareList.CHILD_OLD_FROM
            objWelfareListData.CHILD_OLD_TO = objWelfareList.CHILD_OLD_TO
            objWelfareListData.MONEY = objWelfareList.MONEY
            objWelfareListData.TITLE_GROUP_ID = objWelfareList.TITLE_GROUP_ID
            objWelfareListData.START_DATE = objWelfareList.START_DATE
            objWelfareListData.END_DATE = objWelfareList.END_DATE
            objWelfareListData.IS_AUTO = objWelfareList.IS_AUTO
            objWelfareListData.ID_NAME = objWelfareList.ID_NAME
            Context.SaveChanges()
            Context.SaveChanges(log)
            gID = objWelfareListData.ID

            Dim lstWelfareListData As List(Of HU_WELFARE_LIST_GW)
            lstWelfareListData = (From p In Context.HU_WELFARE_LIST_GW Where p.HU_WELFARE_LIST_ID = objWelfareListData.ID).ToList
            For index = 0 To lstWelfareListData.Count - 1
                Context.HU_WELFARE_LIST_GW.DeleteObject(lstWelfareListData(index))
            Next
            Context.SaveChanges(log)
            If objWelfareList.CONTRACT_TYPE IsNot Nothing Then
                Dim lstT = objWelfareList.CONTRACT_TYPE.Split(",")
                If lstT IsNot Nothing Then
                    For Each s As String In lstT
                        Dim obj As New HU_WELFARE_LIST_GW
                        obj.ID = Utilities.GetNextSequence(Context, Context.HU_WELFARE_LIST.EntitySet.Name)
                        obj.HU_WELFARE_LIST_ID = gID
                        obj.CONTRACT_TYPE_ID = Utilities.Obj2Decima(s)
                        Context.HU_WELFARE_LIST_GW.AddObject(obj)
                    Next
                End If
            End If

            If objWelfareList.GENDER IsNot Nothing Then
                Dim lstG = objWelfareList.GENDER.Split(",")
                If lstG IsNot Nothing Then
                    For Each s As String In lstG
                        Dim obj As New HU_WELFARE_LIST_GW
                        obj.ID = Utilities.GetNextSequence(Context, Context.HU_WELFARE_LIST.EntitySet.Name)
                        obj.HU_WELFARE_LIST_ID = gID
                        obj.GENDER_ID = Utilities.Obj2Decima(s)
                        Context.HU_WELFARE_LIST_GW.AddObject(obj)
                    Next
                End If
            End If

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveWelfareList(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstWelfareListData As List(Of HU_WELFARE_LIST)
        lstWelfareListData = (From p In Context.HU_WELFARE_LIST Where lstID.Contains(p.ID)).ToList
        For index = 0 To lstWelfareListData.Count - 1
            lstWelfareListData(index).ACTFLG = sActive
        Next
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function DeleteWelfareList(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstWelfareListData As List(Of HU_WELFARE_LIST)
        Try
            'Check ID da su dung trong phuc loi ca nhan hay tap the chua? HU_WELFARE_MNG
            Dim lstWelfareListUse As List(Of HU_WELFARE_MNG)
            lstWelfareListUse = (From mng In Context.HU_WELFARE_MNG Where lstID.Contains(mng.WELFARE_ID)).ToList
            If (lstWelfareListUse.Count > 0) Then

                For Each l In lstWelfareListUse
                    If (l.WELFARE_ID IsNot Nothing) Then
                        lstID.Remove(l.WELFARE_ID)
                    End If
                Next
            End If
            If (lstID.Count > 0) Then
                lstWelfareListData = (From Wel In Context.HU_WELFARE_LIST Where lstID.Contains(Wel.ID)).ToList
                For index = 0 To lstWelfareListData.Count - 1
                    Context.HU_WELFARE_LIST.DeleteObject(lstWelfareListData(index))
                Next
                Context.SaveChanges(log)
                Return True
            Else
                Context.SaveChanges(log)
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "AllowanceList"

    Public Function GetAllowanceList(ByVal _filter As AllowanceListDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceListDTO)

        Try
            Dim query = From p In Context.HU_ALLOWANCE_LIST

            Dim lst = query.Select(Function(p) New AllowanceListDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .IS_INSURANCE = p.IS_INSURANCE,
                                       .ALLOW_TYPE = p.ALLOWANCE_TYPE,
                                       .ALLOW_TYPE_NAME = If(p.ALLOWANCE_TYPE = 1, "Theo tháng",
                                                        If(p.ALLOWANCE_TYPE = 2, "Theo công hưởng lương",
                                                           If(p.ALLOWANCE_TYPE = 3, "Theo công làm việc", ""))),
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .CREATED_DATE = p.CREATED_DATE})
            If _filter.ID > 0 Then
                lst = lst.Where(Function(p) p.ID = _filter.ID)
            End If
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.ALLOW_TYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.ALLOW_TYPE_NAME.ToUpper.Contains(_filter.ALLOW_TYPE_NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function InsertAllowanceList(ByVal objAllowanceList As AllowanceListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objAllowanceListData As New HU_ALLOWANCE_LIST
        Try
            objAllowanceListData.ID = Utilities.GetNextSequence(Context, Context.HU_ALLOWANCE_LIST.EntitySet.Name)
            objAllowanceListData.CODE = objAllowanceList.CODE.Trim
            objAllowanceListData.NAME = objAllowanceList.NAME.Trim
            objAllowanceListData.ACTFLG = objAllowanceList.ACTFLG
            objAllowanceListData.ALLOWANCE_TYPE = objAllowanceList.ALLOW_TYPE
            objAllowanceListData.REMARK = objAllowanceList.REMARK
            objAllowanceListData.IS_INSURANCE = objAllowanceList.IS_INSURANCE
            Context.HU_ALLOWANCE_LIST.AddObject(objAllowanceListData)
            Context.SaveChanges(log)
            gID = objAllowanceListData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ValidateAllowanceList(ByVal _validate As AllowanceListDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_ALLOWANCE_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_ALLOWANCE_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_ALLOWANCE_LIST
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_ALLOWANCE_LIST
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyAllowanceList(ByVal objAllowanceList As AllowanceListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objAllowanceListData As New HU_ALLOWANCE_LIST With {.ID = objAllowanceList.ID}
        Try
            objAllowanceListData = (From p In Context.HU_ALLOWANCE_LIST Where p.ID = objAllowanceList.ID).FirstOrDefault
            objAllowanceListData.ID = objAllowanceList.ID
            objAllowanceListData.CODE = objAllowanceList.CODE
            objAllowanceListData.NAME = objAllowanceList.NAME
            objAllowanceListData.ALLOWANCE_TYPE = objAllowanceList.ALLOW_TYPE
            objAllowanceListData.REMARK = objAllowanceList.REMARK
            objAllowanceListData.IS_INSURANCE = objAllowanceList.IS_INSURANCE
            Context.SaveChanges(log)
            gID = objAllowanceListData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveAllowanceList(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstAllowanceListData As List(Of HU_ALLOWANCE_LIST)
        lstAllowanceListData = (From p In Context.HU_ALLOWANCE_LIST Where lstID.Contains(p.ID)).ToList
        For index = 0 To lstAllowanceListData.Count - 1
            lstAllowanceListData(index).ACTFLG = sActive
        Next
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function DeleteAllowanceList(ByVal lstAllowanceList() As AllowanceListDTO, ByVal log As UserLog) As Boolean
        Dim lstAllowanceListData As List(Of HU_ALLOWANCE_LIST)
        Dim lstIDAllowanceList As List(Of Decimal) = (From p In lstAllowanceList.ToList Select p.ID).ToList
        Try
            lstAllowanceListData = (From p In Context.HU_ALLOWANCE_LIST Where lstIDAllowanceList.Contains(p.ID)).ToList
            For index = 0 To lstAllowanceListData.Count - 1
                Context.HU_ALLOWANCE_LIST.DeleteObject(lstAllowanceListData(index))
            Next
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "RelationshipList"

    Public Function GetRelationshipGroupList() As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_REL_GROUP_LIST",
                                           New With {.P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetRelationshipList(ByVal _filter As RelationshipListDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RelationshipListDTO)

        Try
            Dim query = From p In Context.HU_RELATIONSHIP_LIST
                        From g In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.REL_GROUP_ID).DefaultIfEmpty
                        Select New RelationshipListDTO With {
                                       .ID = p.ID,
                                       .REL_GROUP_ID = p.REL_GROUP_ID,
                                       .REL_GROUP_NAME = g.NAME_VN,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.ID > 0 Then
                lst = lst.Where(Function(p) p.ID = _filter.ID)
            End If
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            If _filter.REL_GROUP_NAME <> "" Then
                lst = lst.Where(Function(p) p.REL_GROUP_NAME.ToUpper.Contains(_filter.REL_GROUP_NAME.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function InsertRelationshipList(ByVal objRelationshipList As RelationshipListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objRelationshipListData As New HU_RELATIONSHIP_LIST
        Try
            objRelationshipListData.ID = Utilities.GetNextSequence(Context, Context.HU_RELATIONSHIP_LIST.EntitySet.Name)
            objRelationshipListData.REL_GROUP_ID = objRelationshipList.REL_GROUP_ID
            objRelationshipListData.CODE = objRelationshipList.CODE.Trim
            objRelationshipListData.NAME = objRelationshipList.NAME.Trim
            objRelationshipListData.ACTFLG = objRelationshipList.ACTFLG
            objRelationshipListData.REMARK = objRelationshipList.REMARK
            Context.HU_RELATIONSHIP_LIST.AddObject(objRelationshipListData)
            Context.SaveChanges(log)
            gID = objRelationshipListData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ValidateRelationshipList(ByVal _validate As RelationshipListDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_RELATIONSHIP_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_RELATIONSHIP_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_RELATIONSHIP_LIST
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_RELATIONSHIP_LIST
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyRelationshipList(ByVal objRelationshipList As RelationshipListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objRelationshipListData As New HU_RELATIONSHIP_LIST With {.ID = objRelationshipList.ID}
        Try
            objRelationshipListData = (From p In Context.HU_RELATIONSHIP_LIST Where p.ID = objRelationshipList.ID).FirstOrDefault
            objRelationshipListData.ID = objRelationshipList.ID
            objRelationshipListData.REL_GROUP_ID = objRelationshipList.REL_GROUP_ID
            objRelationshipListData.CODE = objRelationshipList.CODE
            objRelationshipListData.NAME = objRelationshipList.NAME
            objRelationshipListData.REMARK = objRelationshipList.REMARK
            Context.SaveChanges(log)
            gID = objRelationshipListData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveRelationshipList(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstRelationshipListData As List(Of HU_RELATIONSHIP_LIST)
        lstRelationshipListData = (From p In Context.HU_RELATIONSHIP_LIST Where lstID.Contains(p.ID)).ToList
        For index = 0 To lstRelationshipListData.Count - 1
            lstRelationshipListData(index).ACTFLG = sActive
        Next
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function DeleteRelationshipList(ByVal lstRelationshipList() As RelationshipListDTO, ByVal log As UserLog) As Boolean
        Dim lstRelationshipListData As List(Of HU_RELATIONSHIP_LIST)
        Dim lstIDRelationshipList As List(Of Decimal) = (From p In lstRelationshipList.ToList Select p.ID).ToList
        Try
            lstRelationshipListData = (From p In Context.HU_RELATIONSHIP_LIST Where lstIDRelationshipList.Contains(p.ID)).ToList
            For index = 0 To lstRelationshipListData.Count - 1
                Context.HU_RELATIONSHIP_LIST.DeleteObject(lstRelationshipListData(index))
            Next
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "Organization"
    Public Function GetTreeOrgByID(ByVal ID As Decimal) As OrganizationTreeDTO
        Dim orgTree As OrganizationTreeDTO
        Try
            orgTree = (From p In Context.HUV_ORGANIZATION
                       From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID5).DefaultIfEmpty
                       From E In Context.HU_EMPLOYEE.Where(Function(F) F.ID = o.REPRESENTATIVE_ID).DefaultIfEmpty
                       Where p.ID = ID
                        Select New OrganizationTreeDTO With {.ID = p.ID,
                                                             .NAME_VN = p.NAME_VN,
                                                             .CODE = p.CODE,
                                                             .ORG_CODE1 = p.ORG_CODE1,
                                                             .ORG_CODE2 = p.ORG_CODE2,
                                                             .ORG_CODE3 = p.ORG_CODE3,
                                                             .ORG_CODE4 = p.ORG_CODE4,
                                                             .ORG_CODE5 = p.ORG_CODE5,
                                                             .ORG_CODE6 = p.ORG_CODE6,
                                                             .ORG_CODE7 = p.ORG_CODE7,
                                                             .ORG_CODE8 = p.ORG_CODE8,
                                                             .ORG_CODE9 = p.ORG_CODE9,
                                                             .ORG_ID1 = p.ORG_ID1,
                                                             .ORG_ID2 = p.ORG_ID2,
                                                             .ORG_ID3 = p.ORG_ID3,
                                                             .ORG_ID4 = p.ORG_ID4,
                                                             .ORG_ID5 = p.ORG_ID5,
                                                             .ORG_ID6 = p.ORG_ID6,
                                                             .ORG_ID7 = p.ORG_ID7,
                                                             .ORG_ID8 = p.ORG_ID8,
                                                             .ORG_ID9 = p.ORG_ID9,
                                                             .ORG_NAME = p.ORG_NAME,
                                                             .ORG_NAME1 = p.ORG_NAME1,
                                                             .ORG_NAME2 = p.ORG_NAME2,
                                                             .ORG_NAME3 = p.ORG_NAME3,
                                                             .ORG_NAME4 = p.ORG_NAME4,
                                                             .ORG_NAME5 = p.ORG_NAME5,
                                                             .ORG_NAME6 = p.ORG_NAME6,
                                                             .ORG_NAME7 = p.ORG_NAME7,
                                                             .ORG_NAME8 = p.ORG_NAME8,
                                                             .ORG_NAME9 = p.ORG_NAME9,
                                                             .ORG_PATH = p.ORG_PATH,
                                                             .PARENT_ID = p.PARENT_ID,
                                                             .REPRESENTATIVE_ID = o.REPRESENTATIVE_ID,
                                                             .REPRESENTATIVE_NAME = E.FULLNAME_VN
                            }).SingleOrDefault
            Return orgTree
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetOrganizationByID(ByVal ID As Decimal) As OrganizationDTO
        Dim query As OrganizationDTO
        Try
            query = (From p In Context.HU_ORGANIZATION
                     From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.REPRESENTATIVE_ID).DefaultIfEmpty
                     Where p.ID = ID
                     Select New OrganizationDTO With {.ID = p.ID,
                                                      .CODE = p.CODE,
                                                      .NAME_VN = p.NAME_VN,
                                                      .NAME_EN = p.NAME_EN,
                                                      .PARENT_ID = p.PARENT_ID,
                                                      .EFFECT_DATE = p.EFFECT_DATE,
                                                      .FOUNDATION_DATE = p.FOUNDATION_DATE,
                                                      .DISSOLVE_DATE = p.DISSOLVE_DATE,
                                                      .REMARK = p.REMARK,
                                                      .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                      .ADDRESS = p.ADDRESS,
                                                      .FAX = p.FAX,
                                                      .NUMBER_BUSINESS = p.NUMBER_BUSINESS,
                                                      .DATE_BUSINESS = p.DATE_BUSINESS,
                                                      .PIT_NO = p.PIT_NO,
                                                      .MOBILE = p.MOBILE,
                                                      .REPRESENTATIVE_ID = p.REPRESENTATIVE_ID,
                                                      .REPRESENTATIVE_CODE = e.EMPLOYEE_CODE,
                                                      .REPRESENTATIVE_NAME = e.FULLNAME_VN,
                                                      .U_INSURANCE = p.U_INSURANCE,
                                                    .ORG_LEVEL = p.ORG_LEVEL,
                                                    .REGION_ID = p.REGION_ID}).SingleOrDefault
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetOrganization(ByVal sACT As String) As List(Of OrganizationDTO)
        Dim query As ObjectQuery(Of OrganizationDTO)
        Try
            If sACT = "" Then
                query = (From p In Context.HU_ORGANIZATION
                         From parent In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.PARENT_ID).DefaultIfEmpty
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.REPRESENTATIVE_ID).DefaultIfEmpty()
                         From v In Context.HU_EMPLOYEE_CV.Where(Function(b) b.EMPLOYEE_ID = e.ID).DefaultIfEmpty()
                         From t In Context.HU_TITLE.Where(Function(a) a.ID = e.TITLE_ID).DefaultIfEmpty()
                         Order By p.DESCRIPTION_PATH
                         Select New OrganizationDTO With {.ID = p.ID,
                                                          .CODE = p.CODE,
                                                          .NAME_VN = p.NAME_VN,
                                                          .NAME_EN = p.NAME_EN,
                                                          .PARENT_ID = p.PARENT_ID,
                                                          .PARENT_NAME = parent.NAME_VN,
                                                          .EFFECT_DATE = p.EFFECT_DATE,
                                                          .FOUNDATION_DATE = p.FOUNDATION_DATE,
                                                          .DISSOLVE_DATE = p.DISSOLVE_DATE,
                                                          .REMARK = p.REMARK,
                                                          .ACTFLG = p.ACTFLG,
                                                          .ADDRESS = p.ADDRESS,
                                                          .FAX = p.FAX,
                                                          .MOBILE = p.MOBILE,
                                                          .PROVINCE_NAME = p.PROVINCE_NAME,
                                                          .COST_CENTER_CODE = p.COST_CENTER_CODE,
                                                          .ORD_NO = p.ORD_NO,
                                                          .NUMBER_BUSINESS = p.NUMBER_BUSINESS,
                                                          .DATE_BUSINESS = p.DATE_BUSINESS,
                                                          .PIT_NO = p.PIT_NO,
                                                          .UNIT_LEVEL = p.UNIT_LEVEL,
                                                          .REPRESENTATIVE_ID = p.REPRESENTATIVE_ID,
                                                          .REPRESENTATIVE_CODE = e.EMPLOYEE_CODE,
                                                          .REPRESENTATIVE_NAME = e.FULLNAME_VN,
                                                          .TITLE_NAME = t.NAME_VN,
                                                          .IMAGE = v.IMAGE,
                                                           .U_INSURANCE = p.U_INSURANCE,
                                                          .ORG_LEVEL = p.ORG_LEVEL,
                                                          .REGION_ID = p.REGION_ID,
                                                          .TYPE_DECISION = p.TYPE_DECISION,
                                                          .NUMBER_DECISION = p.NUMBER_DECISION,
                                                          .CHK_ORGCHART = p.CHK_ORGCHART,
                                                          .LOCATION_WORK = p.LOCATION_WORK,
                                                          .FILES = p.FILES
                                                         })
                '.AutoGenTimeSheet = p.AUTOGENTIMESHEET
            Else
                query = (From p In Context.HU_ORGANIZATION
                         From parent In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.PARENT_ID).DefaultIfEmpty
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.REPRESENTATIVE_ID).DefaultIfEmpty
                         Where p.ACTFLG = sACT
                         Order By p.DESCRIPTION_PATH
                         Select New OrganizationDTO With {.ID = p.ID,
                                                          .CODE = p.CODE,
                                                          .NAME_VN = p.NAME_VN,
                                                          .NAME_EN = p.NAME_EN,
                                                          .PARENT_ID = p.PARENT_ID,
                                                          .PARENT_NAME = parent.NAME_VN,
                                                          .EFFECT_DATE = p.EFFECT_DATE,
                                                          .FOUNDATION_DATE = p.FOUNDATION_DATE,
                                                          .DISSOLVE_DATE = p.DISSOLVE_DATE,
                                                          .REMARK = p.REMARK,
                                                          .ACTFLG = p.ACTFLG,
                                                          .ADDRESS = p.ADDRESS,
                                                          .FAX = p.FAX,
                                                          .MOBILE = p.MOBILE,
                                                          .PROVINCE_NAME = p.PROVINCE_NAME,
                                                          .COST_CENTER_CODE = p.COST_CENTER_CODE,
                                                          .ORD_NO = p.ORD_NO,
                                                          .REPRESENTATIVE_ID = p.REPRESENTATIVE_ID,
                                                          .REPRESENTATIVE_CODE = e.EMPLOYEE_CODE,
                                                          .REPRESENTATIVE_NAME = e.FULLNAME_VN,
                                                          .U_INSURANCE = p.U_INSURANCE,
                                                          .ORG_LEVEL = p.ORG_LEVEL,
                                                          .REGION_ID = p.REGION_ID,
                                                            .TYPE_DECISION = p.TYPE_DECISION,
                                                          .NUMBER_DECISION = p.NUMBER_DECISION,
                                                          .CHK_ORGCHART = p.CHK_ORGCHART,
                                                          .LOCATION_WORK = p.LOCATION_WORK,
                                                          .FILES = p.FILES
                                                         })
            End If

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertOrganization(ByVal objOrganization As OrganizationDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objOrganizationData As New HU_ORGANIZATION
        Try
            objOrganizationData.ID = Utilities.GetNextSequence(Context, Context.HU_ORGANIZATION.EntitySet.Name)
            objOrganizationData.CODE = objOrganization.CODE
            objOrganizationData.NAME_VN = objOrganization.NAME_VN.Trim
            objOrganizationData.NAME_EN = objOrganization.NAME_EN.Trim
            objOrganizationData.COST_CENTER_CODE = objOrganization.COST_CENTER_CODE
            objOrganizationData.EFFECT_DATE = objOrganization.EFFECT_DATE
            objOrganizationData.FOUNDATION_DATE = objOrganization.FOUNDATION_DATE
            objOrganizationData.DISSOLVE_DATE = objOrganization.DISSOLVE_DATE
            objOrganizationData.PARENT_ID = objOrganization.PARENT_ID
            objOrganizationData.DESCRIPTION_PATH = objOrganization.DESCRIPTION_PATH
            objOrganizationData.HIERARCHICAL_PATH = objOrganization.HIERARCHICAL_PATH
            objOrganizationData.ACTFLG = objOrganization.ACTFLG
            objOrganizationData.REMARK = objOrganization.REMARK
            objOrganizationData.ADDRESS = objOrganization.ADDRESS
            objOrganizationData.FAX = objOrganization.FAX
            objOrganizationData.MOBILE = objOrganization.MOBILE
            objOrganizationData.PROVINCE_NAME = objOrganization.PROVINCE_NAME
            objOrganizationData.COST_CENTER_CODE = objOrganization.COST_CENTER_CODE
            objOrganizationData.ORD_NO = objOrganization.ORD_NO
            objOrganizationData.REPRESENTATIVE_ID = objOrganization.REPRESENTATIVE_ID
            objOrganizationData.NUMBER_BUSINESS = objOrganization.NUMBER_BUSINESS
            objOrganizationData.DATE_BUSINESS = objOrganization.DATE_BUSINESS
            objOrganizationData.PIT_NO = objOrganization.PIT_NO
            objOrganizationData.DISSOLVE_DATE = objOrganization.DISSOLVE_DATE
            objOrganizationData.NUMBER_DECISION = objOrganization.NUMBER_DECISION
            objOrganizationData.TYPE_DECISION = objOrganization.TYPE_DECISION
            objOrganizationData.LOCATION_WORK = objOrganization.LOCATION_WORK
            objOrganizationData.CHK_ORGCHART = objOrganization.CHK_ORGCHART
            objOrganizationData.FILES = objOrganization.FILES
            Context.HU_ORGANIZATION.AddObject(objOrganizationData)
            'EDIT BY: CHIENNV
            'EDIT DATE:11/10/2017
            'ADD FIELD U_INSURANCE IN A Context HU_ORGANIZATION
            objOrganizationData.U_INSURANCE = objOrganization.U_INSURANCE
            objOrganizationData.REGION_ID = objOrganization.REGION_ID
            objOrganizationData.ORG_LEVEL = objOrganization.ORG_LEVEL
            objOrganizationData.UNIT_LEVEL = objOrganization.UNIT_LEVEL
            'objOrganizationData.AUTOGENTIMESHEET = objOrganization.AutoGenTimeSheet
            'END EDIT;
            Context.SaveChanges(log)
            gID = objOrganizationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateOrganization(ByVal _validate As OrganizationDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_ORGANIZATION
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_ORGANIZATION
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            Else
                If (_validate.NAME_VN IsNot Nothing And _validate.NAME_EN IsNot Nothing) Then
                    query = (From p In Context.HU_ORGANIZATION
                             Where (p.NAME_VN.ToUpper = _validate.NAME_VN.ToUpper _
                             Or p.NAME_EN.ToUpper = _validate.NAME_VN.ToUpper) _
                         And p.ACTFLG = _validate.ACTFLG).FirstOrDefault
                End If
                Return (query IsNot Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ValidateCostCenterCode(ByVal _validate As OrganizationDTO)
        Dim query
        Try
            If _validate.COST_CENTER_CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_ORGANIZATION
                             Where p.COST_CENTER_CODE.ToUpper = _validate.COST_CENTER_CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_ORGANIZATION
                             Where p.COST_CENTER_CODE.ToUpper = _validate.COST_CENTER_CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    Public Function CheckEmployeeInOrganization(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            Dim i As Integer = (From p In Context.HU_EMPLOYEE
                                Where lstID.Contains(p.ORG_ID) And
                                (Not p.WORK_STATUS.HasValue Or
                                    (p.WORK_STATUS.HasValue And
                                        ((p.WORK_STATUS <> terID) Or
                                            (p.WORK_STATUS = terID And
                                                p.TER_EFFECT_DATE > dateNow))))).Count
            If i > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyOrganization(ByVal objOrganization As OrganizationDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objOrganizationData As New HU_ORGANIZATION With {.ID = objOrganization.ID}

        Try
            objOrganizationData = (From p In Context.HU_ORGANIZATION Where p.ID = objOrganization.ID).FirstOrDefault
            objOrganizationData.ID = objOrganization.ID
            objOrganizationData.CODE = objOrganization.CODE.Trim
            objOrganizationData.NAME_VN = objOrganization.NAME_VN.Trim
            objOrganizationData.NAME_EN = objOrganization.NAME_EN.Trim
            objOrganizationData.COST_CENTER_CODE = objOrganization.COST_CENTER_CODE
            objOrganizationData.EFFECT_DATE = objOrganization.EFFECT_DATE
            objOrganizationData.FOUNDATION_DATE = objOrganization.FOUNDATION_DATE
            objOrganizationData.DISSOLVE_DATE = objOrganization.DISSOLVE_DATE
            objOrganizationData.DESCRIPTION_PATH = objOrganization.DESCRIPTION_PATH
            objOrganizationData.HIERARCHICAL_PATH = objOrganization.HIERARCHICAL_PATH
            objOrganizationData.REMARK = objOrganization.REMARK
            objOrganizationData.ADDRESS = objOrganization.ADDRESS
            objOrganizationData.FAX = objOrganization.FAX
            objOrganizationData.MOBILE = objOrganization.MOBILE
            objOrganizationData.PROVINCE_NAME = objOrganization.PROVINCE_NAME
            objOrganizationData.COST_CENTER_CODE = objOrganization.COST_CENTER_CODE
            objOrganizationData.NUMBER_BUSINESS = objOrganization.NUMBER_BUSINESS
            objOrganizationData.DATE_BUSINESS = objOrganization.DATE_BUSINESS
            objOrganizationData.PIT_NO = objOrganization.PIT_NO
            objOrganizationData.DISSOLVE_DATE = objOrganization.DISSOLVE_DATE
            objOrganizationData.NUMBER_DECISION = objOrganization.NUMBER_DECISION
            objOrganizationData.TYPE_DECISION = objOrganization.TYPE_DECISION
            objOrganizationData.LOCATION_WORK = objOrganization.LOCATION_WORK
            objOrganizationData.CHK_ORGCHART = objOrganization.CHK_ORGCHART
            objOrganizationData.FILES = objOrganization.FILES
            objOrganizationData.ORD_NO = objOrganization.ORD_NO
            'EDIT BY: CHIENNV
            'EDIT DATE:11/10/2017
            'ADD FIELD U_INSURANCE IN A Context HU_ORGANIZATION
            objOrganizationData.U_INSURANCE = objOrganization.U_INSURANCE
            objOrganizationData.REGION_ID = objOrganization.REGION_ID
            objOrganizationData.ORG_LEVEL = objOrganization.ORG_LEVEL
            objOrganizationData.UNIT_LEVEL = objOrganization.UNIT_LEVEL
            'END EDIT;
            objOrganizationData.REPRESENTATIVE_ID = objOrganization.REPRESENTATIVE_ID
            Context.SaveChanges(log)
            gID = objOrganizationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyOrganizationPath(ByVal lstPath As List(Of OrganizationPathDTO)) As Boolean
        Try

            For Each item As OrganizationPathDTO In lstPath
                Dim objOrganizationData As New HU_ORGANIZATION With {.ID = item.ID}
                Context.HU_ORGANIZATION.Attach(objOrganizationData)
                objOrganizationData.DESCRIPTION_PATH = item.DESCRIPTION_PATH
                objOrganizationData.HIERARCHICAL_PATH = item.HIERARCHICAL_PATH
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveOrganization(ByVal lstOrganization() As OrganizationDTO, ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstOrganizationData As List(Of HU_ORGANIZATION)
        Dim lstIDOrganization As List(Of Decimal) = (From p In lstOrganization.ToList Select p.ID).ToList
        lstOrganizationData = (From p In Context.HU_ORGANIZATION Where lstIDOrganization.Contains(p.ID)).ToList
        For index = 0 To lstOrganizationData.Count - 1
            lstOrganizationData(index).ACTFLG = sActive
            If sActive = "A" Then
                lstOrganizationData(index).DISSOLVE_DATE = Nothing
            End If
            lstOrganizationData(index).MODIFIED_DATE = DateTime.Now
            lstOrganizationData(index).MODIFIED_BY = log.Username
            lstOrganizationData(index).MODIFIED_LOG = log.ComputerName
        Next
        Context.SaveChanges(log)
        Return True
    End Function

#End Region

#Region "OrgTitle"

    Public Function GetOrgTitle(ByVal filter As OrgTitleDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OrgTitleDTO)
        Try
            Dim query = From p In Context.HU_ORG_TITLE
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID And f.ID = filter.ORG_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From t2 In Context.HU_TITLE.Where(Function(f) f.ID = p.PARENT_ID).DefaultIfEmpty
                        From group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = t.TITLE_GROUP_ID).DefaultIfEmpty
                        Order By t.CODE


            Dim org = query.Select(Function(p) New OrgTitleDTO With {.ID = p.p.ID,
                                                                     .ORG_ID = p.p.ORG_ID,
                                                                     .TITLE_ID = p.p.TITLE_ID,
                                                                     .CODE = p.t.CODE,
                                                                     .NAME_EN = p.t.NAME_EN,
                                                                     .NAME_VN = p.t.NAME_VN,
                                                                     .TITLE_GROUP_ID = p.group.ID,
                                                                     .TITLE_GROUP_NAME = p.group.NAME_VN,
                                                                     .REMARK = p.t.REMARK,
                                                                     .PARENT_NAME = p.t2.NAME_VN,
                                                                     .PARENT_ID = p.p.PARENT_ID,
                                                                     .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})
            If filter.TITLE_ID <> 0 Then
                org = org.Where(Function(p) p.TITLE_ID = filter.TITLE_ID)
            End If
            If filter.NAME_EN <> "" Then
                org = org.Where(Function(p) p.NAME_EN.ToUpper.Contains(filter.NAME_EN.ToUpper))
            End If
            If filter.NAME_VN <> "" Then
                org = org.Where(Function(p) p.NAME_VN.ToUpper.Contains(filter.NAME_VN.ToUpper))
            End If
            If filter.PARENT_NAME <> "" Then
                org = org.Where(Function(p) p.PARENT_NAME.ToUpper.Contains(filter.PARENT_NAME.ToUpper))
            End If
            If filter.ACTFLG <> "" Then
                org = org.Where(Function(p) p.ACTFLG.ToUpper.Contains(filter.ACTFLG.ToUpper))
            End If

            Total = org.Count
            org = org.Skip(PageIndex * PageSize).Take(PageSize)
            Return org.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function InsertOrgTitle(ByVal lstOrgTitle As List(Of OrgTitleDTO),
                                   ByVal log As UserLog, ByRef gID As Decimal,
                                   Optional ByVal isSave As Boolean = True) As Boolean
        Dim litOrgID As List(Of Decimal) = (From p In lstOrgTitle Select p.ORG_ID).Distinct().ToList
        Try
            Dim lstOrgTitleExist = (From p In Context.HU_ORG_TITLE
                                    Where litOrgID.Contains(p.ORG_ID)
                                    Select p.ORG_ID, p.TITLE_ID).ToList

            lstOrgTitle = lstOrgTitle.Where(Function(w) Not lstOrgTitleExist.Any(Function(f) f.TITLE_ID = w.TITLE_ID)).ToList

            For Each obj As OrgTitleDTO In lstOrgTitle
                Dim objData As New HU_ORG_TITLE
                objData.ID = Utilities.GetNextSequence(Context, Context.HU_ORG_TITLE.EntitySet.Name)
                objData.ORG_ID = obj.ORG_ID
                objData.TITLE_ID = obj.TITLE_ID
                objData.ACTFLG = obj.ACTFLG
                objData.PARENT_ID = obj.PARENT_ID
                Context.HU_ORG_TITLE.AddObject(objData)
            Next
            If isSave AndAlso lstOrgTitle.Any Then
                Context.SaveChanges(log)
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function CheckTitleInEmployee(ByVal lstID As List(Of Decimal), ByVal orgID As Decimal) As Boolean
        Try
            Dim lstTitleID = (From p In Context.HU_ORG_TITLE Where lstID.Contains(p.ID) Select p.TITLE_ID).ToList
            Dim i As Integer = (From p In Context.HU_EMPLOYEE
                                Where lstTitleID.Contains(p.TITLE_ID) And p.ORG_ID = orgID).Count
            If i > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveOrgTitle(ByVal lstOrgTitle As List(Of Decimal), ByVal sActive As String,
                               ByVal log As UserLog) As Boolean
        Dim lstOrgTitleData As List(Of HU_ORG_TITLE)
        Try
            lstOrgTitleData = (From p In Context.HU_ORG_TITLE Where lstOrgTitle.Contains(p.ID)).ToList
            For index = 0 To lstOrgTitleData.Count - 1
                lstOrgTitleData(index).ACTFLG = sActive
                lstOrgTitleData(index).MODIFIED_DATE = DateTime.Now
                lstOrgTitleData(index).MODIFIED_BY = log.Username
                lstOrgTitleData(index).MODIFIED_LOG = log.ComputerName
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteOrgTitle(ByVal lstOrgTitle As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean
        Dim lstOrgTitleData As List(Of HU_ORG_TITLE)
        Try
            lstOrgTitleData = (From p In Context.HU_ORG_TITLE Where lstOrgTitle.Contains(p.ID)).ToList

            For idx = 0 To lstOrgTitleData.Count - 1
                Context.HU_ORG_TITLE.DeleteObject(lstOrgTitleData(idx))
            Next

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

    ''' <summary>
    ''' Kiểm tra dữ liệu đã được sử dụng hay chưa?
    ''' </summary>
    ''' <param name="table">Enum Table_Name</param>
    ''' <returns>true:chưa có/false:có rồi</returns>
    ''' <remarks></remarks>
    ''' 

    Public Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As ProfileCommon.TABLE_NAME) As Boolean
        Dim isExist As Boolean = False
        Dim strListID As String = lstID.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
        Dim count As Decimal = 0
        Try

            Select Case table
                Case ProfileCommon.TABLE_NAME.HU_CONTRACT_TYPE
                    isExist = Execute_ExistInDatabase("HU_CONTRACT", "CONTRACT_TYPE_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_TITLE
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE", "TITLE_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_ORG_TITLE", "TITLE_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_WORKING", "TITLE_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    'isExist = Execute_ExistInDatabase("HU_TITLE_CONCURRENT", "TITLE_ID", strListID)
                    'If Not isExist Then
                    '    Return isExist
                    'End If
                Case ProfileCommon.TABLE_NAME.HU_ALLOWANCE_LIST
                    isExist = Execute_ExistInDatabase("HU_WORKING_ALLOW", "ALLOWANCE_LIST_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_NATION
                    isExist = Execute_ExistInDatabase("HU_PROVINCE", "NATION_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "NATIONALITY", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_PROVINCE
                    isExist = Execute_ExistInDatabase("HU_DISTRICT", "PROVINCE_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "PER_PROVINCE", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "NAV_PROVINCE", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("INS_WHEREHEALTH", "ID_PROVINCE", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_BANK
                    isExist = Execute_ExistInDatabase("HU_BANK_BRANCH", "BANK_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "BANK_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_BANK_BRANCH
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "BANK_BRANCH_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_ASSET
                    isExist = Execute_ExistInDatabase("HU_ASSET_MNG", "ASSET_DECLARE_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_DISTRICT
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "PER_DISTRICT", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "NAV_DISTRICT", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_WARD
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "PER_WARD", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "NAV_WARD", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_STAFF_RANK
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE", "STAFF_RANK_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_WORKING", "STAFF_RANK_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("AT_SETUP_SPECIAL", "STAFF_RANK_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_COMMENDLEVER
                    isExist = Execute_ExistInDatabase("HU_COMMEND", "COMMEND_LEVEL", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Private Function Execute_ExistInDatabase(ByVal tableName As String, ByVal colName As String, ByVal strListID As String)
        Try
            Dim count As Decimal = 0
            Dim Sql = "SELECT COUNT(" & colName & ") FROM " & tableName & " WHERE " & colName & " IN (" & strListID & ")"
            count = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault
            If count > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CHECK_ExistInDatabase(ByVal tableName As String, ByVal colName As String, ByVal strListID As String)
        Try
            Dim count As Decimal = 0
            Dim Sql = "SELECT COUNT(" & colName & ") FROM " & tableName & " WHERE " & colName & " IN (" & strListID & ") and ACTFLG = 'A'"
            count = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault
            If count > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#Region "Danh muc tham so he thong"
    Public Function ValidateOtherList(ByVal _validate As OtherListDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    If _validate.ACTFLG <> "" Then
                        query = (From p In Context.OT_OTHER_LIST
                                 Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                                 Where q.CODE.ToUpper = _validate.CODE.ToUpper _
                                 And p.ID = _validate.ID _
                                 And p.ACTFLG = _validate.ACTFLG
                             ).FirstOrDefault
                        Return (Not query Is Nothing)
                    Else
                        query = (From p In Context.OT_OTHER_LIST
                                 Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                                 And p.ID <> _validate.ID
                             ).SingleOrDefault
                        Return (query Is Nothing)
                    End If
                Else
                    query = (From p In Context.OT_OTHER_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper
                            ).FirstOrDefault
                    Return (query Is Nothing)
                End If

            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.OT_OTHER_LIST
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    'And p.TYPE_ID = _validate.TYPE_ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.OT_OTHER_LIST
                             Where p.ID = _validate.ID).FirstOrDefault
                    ' And p.TYPE_ID = _validate.TYPE_ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
                If (_validate.NAME_VN <> "") Then
                    If (_validate.ID <> 0) Then
                        If (_validate.TYPE_CODE <> "") Then
                            query = (From p In Context.OT_OTHER_LIST
                                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.ID = p.TYPE_ID)
                                     Where p.ACTFLG.ToUpper = "A" _
                                          And p.ID = _validate.ID _
                                          And p.NAME_VN.ToUpper.Trim = _validate.NAME_VN.ToUpper.Trim _
                                          And t.CODE = _validate.TYPE_CODE).FirstOrDefault
                            Return (Not query Is Nothing)
                        Else
                            query = (From p In Context.OT_OTHER_LIST
                                     Where p.ACTFLG.ToUpper = "A" And p.ID = _validate.ID And p.NAME_VN.ToUpper.Trim = _validate.NAME_VN.ToUpper.Trim).FirstOrDefault
                            Return (Not query Is Nothing)
                        End If

                    Else
                        If (_validate.TYPE_CODE <> "") Then
                            query = (From p In Context.OT_OTHER_LIST
                                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.ID = p.TYPE_ID)
                                     Where p.ACTFLG.ToUpper = "A" _
                                           And p.NAME_VN.ToUpper.Trim = _validate.NAME_VN.ToUpper.Trim _
                                           And t.CODE = _validate.TYPE_CODE).FirstOrDefault
                            Return (Not query Is Nothing)
                        Else
                            query = (From p In Context.OT_OTHER_LIST
                                     Where p.ACTFLG.ToUpper = "A" And p.NAME_VN.ToUpper.Trim = _validate.NAME_VN.ToUpper.Trim).FirstOrDefault
                            Return (Not query Is Nothing)
                        End If

                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region
#Region "Nation -Danh muc quoc gia"
    Public Function GetNation(ByVal _filter As NationDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of NationDTO)
        Try
            Dim query = From p In Context.HU_NATION
            Dim lst = query.Select(Function(p) New NationDTO With {.ID = p.ID,
                                                            .NAME_EN = p.NAME_EN,
                                                            .NAME_VN = p.NAME_VN,
                                                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                            .CODE = p.CODE,
                                                            .CREATED_DATE = p.CREATED_DATE
                                                             })
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If _filter.NAME_EN <> "" Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            '''''Logger.LogError(ex)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, "Profile.GetNation")
            Throw ex
        End Try
    End Function

    Public Function InsertNation(ByVal objNation As NationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objNationData As New HU_NATION
            objNationData.ID = Utilities.GetNextSequence(Context, Context.HU_NATION.EntitySet.Name)
            'objNationData.NAME_EN = objNation.NAME_EN
            objNationData.NAME_VN = objNation.NAME_VN
            objNationData.ACTFLG = objNation.ACTFLG
            objNationData.CODE = objNation.CODE
            Context.HU_NATION.AddObject(objNationData)
            Context.SaveChanges(log)
            gID = objNationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyNation(ByVal objNation As NationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objNationData As New HU_NATION With {.ID = objNation.ID}

            objNationData = (From p In Context.HU_NATION Where p.ID = objNation.ID).FirstOrDefault
            objNationData.ID = objNation.ID
            objNationData.CODE = objNation.CODE
            'objNationData.NAME_EN = objNation.NAME_EN
            objNationData.NAME_VN = objNation.NAME_VN
            Context.SaveChanges(log)
            gID = objNation.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateNation(ByVal _validate As NationDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_NATION
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.HU_NATION
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_NATION
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_NATION
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveNation(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        Try
            Dim lstNationData As List(Of HU_NATION)
            lstNationData = (From p In Context.HU_NATION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstNationData.Count - 1
                lstNationData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteNation(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        Try
            Dim lstData As List(Of HU_NATION)
            lstData = (From p In Context.HU_NATION Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_NATION.DeleteObject(lstData(index))
            Next
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region

#Region "Province -Danh muc tinh thanh"
    Public Function GetProvinceByNationID(ByVal sNationID As Decimal, ByVal sACTFLG As String) As List(Of ProvinceDTO)
        Try
            Dim query As System.Linq.IQueryable(Of ProfileDAL.ProvinceDTO)
            If sACTFLG <> "" Then
                query = (From p In Context.HU_PROVINCE Where p.NATION_ID = sNationID And p.ACTFLG = sACTFLG Order By p.NAME_VN.ToUpper
                         Select New ProvinceDTO With {.ID = p.ID,
                                                      .NAME_VN = p.NAME_VN
                                                      })
            Else
                query = (From p In Context.HU_PROVINCE Where p.NATION_ID = sNationID Order By p.NAME_VN.ToUpper
                         Select New ProvinceDTO With {.ID = p.ID,
                                                      .NAME_VN = p.NAME_VN
                                                      })
            End If

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetProvinceByNationCode(ByVal sNationCode As String, ByVal sACTFLG As String) As List(Of ProvinceDTO)
        Try
            Dim query As System.Linq.IQueryable(Of ProfileDAL.ProvinceDTO)
            If sACTFLG <> "" Then
                query = (From p In Context.HU_PROVINCE Where p.HU_NATION.CODE = sNationCode And p.ACTFLG = sACTFLG Order By p.NAME_VN.ToUpper
                         Select New ProvinceDTO With {.ID = p.ID,
                                                      .NAME_VN = p.NAME_VN,
                                                      .NAME_EN = p.NAME_EN
                                                      })
            Else
                query = (From p In Context.HU_PROVINCE Where p.HU_NATION.CODE = sNationCode Order By p.NAME_VN.ToUpper
                         Select New ProvinceDTO With {.ID = p.ID,
                                                      .NAME_VN = p.NAME_VN,
                                                       .NAME_EN = p.NAME_EN
                                                      })
            End If
            ''''Logger.LogError(ex)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetProvince(ByVal _filter As ProvinceDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProvinceDTO)
        Try
            Dim query = From p In Context.HU_PROVINCE
            Dim lst = query.Select(Function(p) New ProvinceDTO With {.ID = p.ID,
                                                   .NAME_EN = p.NAME_EN,
                                                   .NAME_VN = p.NAME_VN,
                                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                   .CODE = p.CODE,
                                                   .NATION_ID = p.NATION_ID,
                                                   .NATION_NAME = p.HU_NATION.NAME_VN,
                                                   .CREATED_DATE = p.CREATED_DATE})
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If _filter.NAME_EN <> "" Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If _filter.NATION_NAME <> "" Then
                lst = lst.Where(Function(p) p.NATION_NAME.ToUpper.Contains(_filter.NATION_NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertProvince(ByVal objProvince As ProvinceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objProvinceData As New HU_PROVINCE

            objProvinceData.ID = Utilities.GetNextSequence(Context, Context.HU_PROVINCE.EntitySet.Name)
            'objProvinceData.NAME_EN = objProvince.NAME_EN
            objProvinceData.NAME_VN = objProvince.NAME_VN
            objProvinceData.ACTFLG = objProvince.ACTFLG
            objProvinceData.CODE = objProvince.CODE
            objProvinceData.NATION_ID = objProvince.NATION_ID
            Context.HU_PROVINCE.AddObject(objProvinceData)
            Context.SaveChanges(log)
            gID = objProvinceData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateProvince(ByVal _validate As ProvinceDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_PROVINCE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.HU_PROVINCE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_PROVINCE
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_PROVINCE
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyProvince(ByVal objProvince As ProvinceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objProvinceData As New HU_PROVINCE With {.ID = objProvince.ID}

            objProvinceData = (From p In Context.HU_PROVINCE Where p.ID = objProvince.ID).FirstOrDefault
            objProvinceData.ID = objProvince.ID
            objProvinceData.CODE = objProvince.CODE
            objProvinceData.NATION_ID = objProvince.NATION_ID
            'objProvinceData.NAME_EN = objProvince.NAME_EN
            objProvinceData.NAME_VN = objProvince.NAME_VN
            Context.SaveChanges(log)
            gID = objProvinceData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveProvince(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        Try
            Dim lstProvinceData As List(Of HU_PROVINCE)
            lstProvinceData = (From p In Context.HU_PROVINCE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstProvinceData.Count - 1
                lstProvinceData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteProvince(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        Try
            Dim lstData As List(Of HU_PROVINCE)
            lstData = (From p In Context.HU_PROVINCE Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_PROVINCE.DeleteObject(lstData(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region

#Region "District -Danh muc quan huyen"
    Public Function GetDistrictByProvinceID(ByVal sProvinceID As Decimal, ByVal sACTFLG As String) As List(Of DistrictDTO)
        Try
            Dim query As System.Linq.IQueryable(Of ProfileDAL.DistrictDTO)
            If sACTFLG <> "" Then
                query = (From p In Context.HU_DISTRICT Where p.PROVINCE_ID = sProvinceID And p.ACTFLG = sACTFLG Order By p.NAME_VN.ToUpper
                         Select New DistrictDTO With {.ID = p.ID,
                                                      .NAME_VN = p.NAME_VN})
            Else
                query = (From p In Context.HU_DISTRICT Where p.PROVINCE_ID = sProvinceID Order By p.NAME_VN.ToUpper
                         Select New DistrictDTO With {.ID = p.ID,
                                                      .NAME_VN = p.NAME_VN})
            End If
            ''''Logger.LogError(ex)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function GetDistrict(ByVal _filter As DistrictDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DistrictDTO)
        Try
            Dim query = From p In Context.HU_DISTRICT
            Dim lst = query.Select(Function(p) New DistrictDTO With {.ID = p.ID,
                                                            .NAME_EN = p.NAME_EN,
                                                            .NAME_VN = p.NAME_VN,
                                                            .CODE = p.CODE,
                                                            .NATION_NAME = p.HU_PROVINCE.HU_NATION.NAME_VN,
                                                            .NATION_ID = p.HU_PROVINCE.NATION_ID,
                                                            .PROVINCE_NAME = p.HU_PROVINCE.NAME_VN,
                                                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                            .PROVINCE_ID = p.PROVINCE_ID,
                                                            .CREATED_DATE = p.CREATED_DATE})

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If _filter.NAME_EN <> "" Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If _filter.NATION_NAME <> "" Then
                lst = lst.Where(Function(p) p.NATION_NAME.ToUpper.Contains(_filter.NATION_NAME.ToUpper))
            End If
            If _filter.PROVINCE_NAME <> "" Then
                lst = lst.Where(Function(p) p.PROVINCE_NAME.ToUpper.Contains(_filter.PROVINCE_NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateDistrict(ByVal _validate As DistrictDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_DISTRICT
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_DISTRICT
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_DISTRICT
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_DISTRICT
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertDistrict(ByVal objDistrict As DistrictDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objDistrictData As New HU_DISTRICT
            objDistrictData.ID = Utilities.GetNextSequence(Context, Context.HU_DISTRICT.EntitySet.Name)
            objDistrictData.CODE = objDistrict.CODE
            objDistrictData.NAME_VN = objDistrict.NAME_VN
            objDistrictData.ACTFLG = objDistrict.ACTFLG
            objDistrictData.PROVINCE_ID = objDistrict.PROVINCE_ID
            Context.HU_DISTRICT.AddObject(objDistrictData)
            Context.SaveChanges(log)
            gID = objDistrictData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyDistrict(ByVal objDistrict As DistrictDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objDistrictData As New HU_DISTRICT With {.ID = objDistrict.ID}
            objDistrictData = (From p In Context.HU_DISTRICT Where p.ID = objDistrict.ID).FirstOrDefault
            objDistrictData.ID = objDistrict.ID
            objDistrictData.CODE = objDistrict.CODE
            objDistrictData.PROVINCE_ID = objDistrict.PROVINCE_ID
            objDistrictData.NAME_VN = objDistrict.NAME_VN
            Context.SaveChanges(log)
            gID = objDistrictData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveDistrict(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        Try
            Dim lstDistrictData As List(Of HU_DISTRICT)
            lstDistrictData = (From p In Context.HU_DISTRICT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstDistrictData.Count - 1
                lstDistrictData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteDistrict(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        Try
            Dim lstData As List(Of HU_DISTRICT)
            lstData = (From p In Context.HU_DISTRICT Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_DISTRICT.DeleteObject(lstData(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region

#Region "HU_WARD danh mục xã phường"
    Public Function GetWardByDistrictID(ByVal sProvinceID As Decimal, ByVal sACTFLG As String) As List(Of Ward_DTO)
        Try
            Dim query As System.Linq.IQueryable(Of ProfileDAL.Ward_DTO)
            If sACTFLG <> "" Then
                query = (From p In Context.HU_DISTRICT Where p.PROVINCE_ID = sProvinceID And p.ACTFLG = sACTFLG Order By p.NAME_VN.ToUpper
                         Select New Ward_DTO With {.ID = p.ID,
                                                      .NAME_VN = p.NAME_VN})
            Else
                query = (From p In Context.HU_DISTRICT Where p.PROVINCE_ID = sProvinceID Order By p.NAME_VN.ToUpper
                         Select New Ward_DTO With {.ID = p.ID,
                                                      .NAME_VN = p.NAME_VN})
            End If
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetDistrictByProvinceID")
            Throw ex
        End Try


    End Function

    Public Function GetWard(ByVal _filter As Ward_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Ward_DTO)
        Try
            Dim query = From p In Context.HU_WARD
                        From d In Context.HU_DISTRICT.Where(Function(F) F.ID = p.DISTRICT_ID)
                        From pr In Context.HU_PROVINCE.Where(Function(F) F.ID = d.PROVINCE_ID)
                        From n In Context.HU_NATION.Where(Function(F) F.ID = pr.NATION_ID)
            Dim lst = query.Select(Function(p) New Ward_DTO With {.ID = p.p.ID,
                                                            .CODE = p.p.CODE,
                                                            .NAME_EN = p.p.NAME_EN,
                                                            .NAME_VN = p.p.NAME_VN,
                                                            .DISTRICT_ID = p.p.DISTRICT_ID,
                                                            .DISTRICT_NAME = p.d.NAME_VN,
                                                            .PROVINCE_ID = p.d.PROVINCE_ID,
                                                            .PROVINCE_NAME = p.pr.NAME_VN,
                                                            .NATION_ID = p.pr.NATION_ID,
                                                            .NATION_NAME = p.n.NAME_VN,
                                                            .NOTE = p.p.NOTE,
                                                            .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                            .CREATED_DATE = p.p.CREATED_DATE})

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If _filter.NAME_EN <> "" Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If _filter.DISTRICT_NAME <> "" Then
                lst = lst.Where(Function(p) p.DISTRICT_NAME.ToUpper.Contains(_filter.DISTRICT_NAME.ToUpper))
            End If
            If _filter.PROVINCE_NAME <> "" Then
                lst = lst.Where(Function(p) p.PROVINCE_NAME.ToUpper.Contains(_filter.PROVINCE_NAME.ToUpper))
            End If
            If _filter.NATION_NAME <> "" Then
                lst = lst.Where(Function(p) p.NATION_NAME.ToUpper.Contains(_filter.NATION_NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            If _filter.NOTE <> "" Then
                lst = lst.Where(Function(p) p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetWard")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateWard(ByVal _validate As Ward_DTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_WARD
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_WARD
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_WARD
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_WARD
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateWard")
            Throw ex
        End Try
    End Function

    Public Function InsertWard(ByVal objWard As Ward_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objWardData As New HU_WARD
            objWardData.ID = Utilities.GetNextSequence(Context, Context.HU_WARD.EntitySet.Name)
            objWardData.CODE = objWard.CODE
            objWardData.NAME_VN = objWard.NAME_VN
            objWardData.DISTRICT_ID = objWard.DISTRICT_ID
            objWardData.ACTFLG = objWard.ACTFLG
            objWardData.NOTE = objWard.NOTE
            Context.HU_WARD.AddObject(objWardData)
            Context.SaveChanges(log)
            gID = objWardData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertWard")
            Throw ex
        End Try

    End Function

    Public Function ModifyWard(ByVal objWard As Ward_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objWardData As New HU_WARD With {.ID = objWard.ID}
            objWardData = (From p In Context.HU_WARD Where p.ID = objWard.ID).FirstOrDefault
            objWardData.ID = objWard.ID
            objWardData.CODE = objWard.CODE
            objWardData.DISTRICT_ID = objWard.DISTRICT_ID
            objWardData.NAME_VN = objWard.NAME_VN
            objWardData.NOTE = objWard.NOTE
            Context.SaveChanges(log)
            gID = objWardData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyWard")
            Throw ex
        End Try

    End Function

    Public Function ActiveWard(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of HU_WARD)
        Try
            lstData = (From p In Context.HU_WARD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ActiveWard")
            Throw ex
        End Try
    End Function

    Public Function DeleteWard(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstData As List(Of HU_WARD)
        Try
            lstData = (From p In Context.HU_WARD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_WARD.DeleteObject(lstData(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteWard")
            Throw ex
        End Try
    End Function
#End Region

#Region "Bank -Danh muc Ngan hang"


    Public Function GetBank(ByVal _filter As BankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BankDTO)
        Try
            Dim query = From p In Context.HU_BANK
            Dim lst = query.Select(Function(p) New BankDTO With {.ID = p.ID,
                                                             .NAME = p.NAME,
                                                             .SHORT_NAME = p.SHORT_NAME,
                                                             .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                             .CODE = p.CODE,
                                                             .CREATED_DATE = p.CREATED_DATE})
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.SHORT_NAME <> "" Then
                lst = lst.Where(Function(p) p.SHORT_NAME.ToUpper.Contains(_filter.SHORT_NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function InsertBank(ByVal objBank As BankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objBankData As New HU_BANK
            objBankData.ID = Utilities.GetNextSequence(Context, Context.HU_BANK.EntitySet.Name)
            objBankData.NAME = objBank.NAME
            objBankData.SHORT_NAME = objBank.SHORT_NAME
            objBankData.ACTFLG = objBank.ACTFLG
            objBankData.CODE = objBank.CODE
            Context.HU_BANK.AddObject(objBankData)
            Context.SaveChanges(log)
            gID = objBankData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyBank(ByVal objBank As BankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objBankData As New HU_BANK With {.ID = objBank.ID}
            objBankData = (From p In Context.HU_BANK Where p.ID = objBank.ID).FirstOrDefault
            objBankData.ID = objBank.ID
            objBankData.NAME = objBank.NAME
            objBankData.SHORT_NAME = objBank.SHORT_NAME
            objBankData.CODE = objBank.CODE
            Context.SaveChanges(log)
            gID = objBankData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateBank(ByVal _validate As BankDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_BANK
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.HU_BANK
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_BANK
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_BANK
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveBank(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        Try
            Dim lstBankData As List(Of HU_BANK)
            lstBankData = (From p In Context.HU_BANK Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstBankData.Count - 1
                lstBankData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteBank(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        Try
            Dim lstData As List(Of HU_BANK)
            lstData = (From p In Context.HU_BANK Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_BANK.DeleteObject(lstData(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

#Region "BankBranch -Danh muc chi nhanh ngan hang"

    Public Function GetBankBranchByBankID(ByVal sBank_ID As Decimal) As List(Of BankBranchDTO)
        Try
            Dim query = (From p In Context.HU_BANK_BRANCH
                         From bank In Context.HU_BANK.Where(Function(f) f.ID = p.BANK_ID).DefaultIfEmpty
                         Select New BankBranchDTO With {.ID = p.ID,
                                                        .CODE = p.CODE,
                                                        .NAME = p.NAME,
                                                        .BANK_CODE = bank.CODE,
                                                        .BANK_NAME = bank.NAME,
                                                        .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                        .REMARK = p.REMARK})
            ''''Logger.LogError(ex)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function GetBankBranch(ByVal _filter As BankBranchDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BankBranchDTO)
        Try
            Dim queryProvince = From p In Context.HU_PROVINCE Select p
            Dim query = From p In Context.HU_BANK_BRANCH
                        From bank In Context.HU_BANK.Where(Function(f) f.ID = p.BANK_ID).DefaultIfEmpty
                        Select New BankBranchDTO With {.ID = p.ID,
                                                       .CODE = p.CODE,
                                                       .NAME = p.NAME,
                                                       .BANK_CODE = bank.CODE,
                                                       .BANK_NAME = bank.NAME,
                                                       .BANK_ID = p.BANK_ID,
                                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                       .REMARK = p.REMARK,
                                                       .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.BANK_NAME <> "" Then
                lst = lst.Where(Function(p) p.BANK_NAME.ToUpper.Contains(_filter.BANK_NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            'Logger.LogError(ex)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function InsertBankBranch(ByVal objBankBranch As BankBranchDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objBankBranchData As New HU_BANK_BRANCH
            objBankBranchData.ID = Utilities.GetNextSequence(Context, Context.HU_BANK_BRANCH.EntitySet.Name)
            objBankBranchData.NAME = objBankBranch.NAME
            objBankBranchData.ACTFLG = objBankBranch.ACTFLG
            objBankBranchData.CODE = objBankBranch.CODE
            objBankBranchData.BANK_ID = objBankBranch.BANK_ID
            objBankBranchData.REMARK = objBankBranch.REMARK
            Context.HU_BANK_BRANCH.AddObject(objBankBranchData)
            Context.SaveChanges(log)
            gID = objBankBranchData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyBankBranch(ByVal objBankBranch As BankBranchDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objBankBranchData As New HU_BANK_BRANCH With {.ID = objBankBranch.ID}
            objBankBranchData = (From p In Context.HU_BANK_BRANCH Where p.ID = objBankBranch.ID).FirstOrDefault
            objBankBranchData.ID = objBankBranch.ID
            objBankBranchData.NAME = objBankBranch.NAME
            objBankBranchData.CODE = objBankBranch.CODE
            objBankBranchData.BANK_ID = objBankBranch.BANK_ID
            objBankBranchData.REMARK = objBankBranch.REMARK
            Context.SaveChanges(log)
            gID = objBankBranchData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateBankBranch(ByVal _validate As BankBranchDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_BANK_BRANCH
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.HU_BANK_BRANCH
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_BANK_BRANCH
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_BANK_BRANCH
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveBankBranch(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        Try
            Dim lstBankBranchData As List(Of HU_BANK_BRANCH)
            lstBankBranchData = (From p In Context.HU_BANK_BRANCH Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstBankBranchData.Count - 1
                lstBankBranchData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteBankBranch(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        Try
            Dim lstData As List(Of HU_BANK_BRANCH)
            lstData = (From p In Context.HU_BANK_BRANCH Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_BANK_BRANCH.DeleteObject(lstData(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

#Region "Asset -Danh muc tài sản cấp phát"

    Public Function GetAsset(ByVal _filter As AssetDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssetDTO)
        Try
            Dim query = From p In Context.HU_ASSET
            Dim lst = query.Select(Function(p) New AssetDTO With {.ID = p.ID,
                                                                  .NAME = p.NAME,
                                                                  .ACTFLG = p.ACTFLG,
                                                                  .ACTFLG2 = p.ACTFLG,
                                                                  .CODE = p.CODE,
                                                                  .GROUP_ID = p.GROUP_ID,
                                                                     .REMARK = p.REMARK,
                                                                  .CREATED_DATE = p.CREATED_DATE})
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.GROUP_NAME <> "" Then
                lst = lst.Where(Function(p) p.GROUP_NAME.ToUpper.Contains(_filter.GROUP_NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            If _filter.ACTFLG2 <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG2 = _filter.ACTFLG2)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function InsertAsset(ByVal objAsset As AssetDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objAssetData As New HU_ASSET
            objAssetData.ID = Utilities.GetNextSequence(Context, Context.HU_ASSET.EntitySet.Name)
            objAssetData.NAME = objAsset.NAME
            objAssetData.ACTFLG = objAsset.ACTFLG
            objAssetData.GROUP_ID = objAsset.GROUP_ID
            objAssetData.REMARK = objAsset.REMARK
            objAssetData.CODE = objAsset.CODE
            Context.HU_ASSET.AddObject(objAssetData)
            Context.SaveChanges(log)
            gID = objAssetData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyAsset(ByVal objAsset As AssetDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objAssetData As New HU_ASSET With {.ID = objAsset.ID}
            objAssetData = (From p In Context.HU_ASSET Where p.ID = objAsset.ID).FirstOrDefault
            objAssetData.ID = objAsset.ID
            objAssetData.NAME = objAsset.NAME
            objAssetData.CODE = objAsset.CODE
            objAssetData.REMARK = objAsset.REMARK
            objAssetData.GROUP_ID = objAsset.GROUP_ID
            Context.SaveChanges(log)
            gID = objAssetData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateAsset(ByVal _validate As AssetDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_ASSET
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.HU_ASSET
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_ASSET
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_ASSET
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveAsset(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        Try
            Dim lstAssetData As List(Of HU_ASSET)
            lstAssetData = (From p In Context.HU_ASSET Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstAssetData.Count - 1
                lstAssetData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteAsset(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        Try
            Dim lstData As List(Of HU_ASSET)
            lstData = (From p In Context.HU_ASSET Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_ASSET.DeleteObject(lstData(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

    Public Function GetOrgFromUsername(ByVal username As String) As Decimal?
        Try
            Dim user As SE_USER = Context.SE_USER.FirstOrDefault(Function(p) p.USERNAME.ToUpper = username.ToUpper)

            If user Is Nothing OrElse user.EMPLOYEE_CODE & "" = "" Then
                Return Nothing
            End If

            Dim employee = (From p In Context.HU_EMPLOYEE
                            Where p.EMPLOYEE_CODE.ToUpper = user.EMPLOYEE_CODE.ToUpper
                            Order By p.ID Descending).FirstOrDefault

            If employee Is Nothing Then
                Return Nothing
            End If

            Dim working = Context.HU_WORKING.FirstOrDefault(Function(p) p.EMPLOYEE_ID = employee.ID AndAlso p.EFFECT_DATE <= Date.Now AndAlso p.EXPIRE_DATE >= Date.Now)

            If working Is Nothing Then
                Return Nothing
            End If

            Return working.ORG_ID
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

    Public Function GetLineManager(ByVal username As String) As List(Of EmployeeDTO)
        Try
            Dim rep As New ProfileRepository
            Dim listReturn As New List(Of EmployeeDTO)

            Dim user As SE_USER = Context.SE_USER.FirstOrDefault(Function(p) p.USERNAME.ToUpper = username.ToUpper)

            If user Is Nothing OrElse user.EMPLOYEE_CODE & "" = "" Then
                Return listReturn
            End If

            Dim employee = (From p In Context.HU_EMPLOYEE
                            Join o In Context.HU_ORGANIZATION On p.ORG_ID Equals o.ID
                            Join c In Context.HU_CONTRACT On p.CONTRACT_ID Equals c.ID
                            Join t In Context.HU_TITLE On p.TITLE_ID Equals t.ID
                            Where p.EMPLOYEE_CODE = user.EMPLOYEE_CODE
                            Order By p.ID Descending
                            Select New EmployeeDTO With {
                                .ID = p.ID,
                                .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                .FULLNAME_VN = p.FULLNAME_VN,
                                .ORG_ID = p.ORG_ID,
                                .ORG_NAME = o.NAME_VN,
                                .TITLE_NAME_VN = t.NAME_VN,
                                .JOIN_DATE = p.JOIN_DATE,
                                .CONTRACT_NO = c.CONTRACT_NO,
                                .CONTRACT_EFFECT_DATE = c.START_DATE,
                                .CONTRACT_EXPIRE_DATE = c.EXPIRE_DATE}).FirstOrDefault

            If employee Is Nothing Then
                Return listReturn
            End If

            listReturn.Add(employee)

            While Not employee.DIRECT_MANAGER.HasValue
                Dim m_employee = rep.GetEmployeeByID(employee.DIRECT_MANAGER.Value)
                listReturn.Add(m_employee)
            End While

            Return listReturn
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

#Region "StaffRank"

    Public Function GetStaffRank(ByVal _filter As StaffRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of StaffRankDTO)

        Try
            Dim query = From p In Context.HU_STAFF_RANK
                        Select New StaffRankDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .REMARK = p.REMARK,
                            .LEVER = p.LEVEL_STAFF,
                            .LEVEL_STAFF = p.LEVEL_STAFF,
                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE}
            '.LEAVE_COUNT = p.LEAVE_COUNT,
            '.IS_OVT = If(p.IS_OVT = -1, True, False),
            Dim lst = query
            If _filter.LEVER <> "" Then
                lst = lst.Where(Function(p) p.LEVER.ToUpper.Contains(_filter.LEVER.ToUpper))
            End If
            If _filter.LEVEL_STAFF <> 0 Then
                lst = lst.Where(Function(p) p.LEVEL_STAFF = _filter.LEVEL_STAFF)
            End If
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper = _filter.ACTFLG.ToUpper)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetStaffRank")
            Throw ex
        End Try

    End Function

    Public Function InsertStaffRank(ByVal objStaffRank As StaffRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objStaffRankData As New HU_STAFF_RANK
        Try
            objStaffRankData.ID = Utilities.GetNextSequence(Context, Context.HU_STAFF_RANK.EntitySet.Name)
            objStaffRankData.CODE = objStaffRank.CODE
            objStaffRankData.NAME = objStaffRank.NAME
            objStaffRankData.ACTFLG = objStaffRank.ACTFLG
            objStaffRankData.REMARK = objStaffRank.REMARK
            objStaffRankData.LEVEL_STAFF = objStaffRank.LEVER
            'objStaffRankData.LEAVE_COUNT = objStaffRank.LEAVE_COUNT
            'objStaffRankData.IS_OVT = objStaffRank.IS_OVT
            Context.HU_STAFF_RANK.AddObject(objStaffRankData)
            Context.SaveChanges(log)
            gID = objStaffRankData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertStaffRank")
            Throw ex
        End Try
    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateStaffRank(ByVal _validate As StaffRankDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_STAFF_RANK
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_STAFF_RANK
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_STAFF_RANK
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_STAFF_RANK
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateStaffRank")
            Throw ex
        End Try
    End Function

    Public Function ModifyStaffRank(ByVal objStaffRank As StaffRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objStaffRankData As New HU_STAFF_RANK With {.ID = objStaffRank.ID}
        Try
            objStaffRankData = (From p In Context.HU_STAFF_RANK Where p.ID = objStaffRank.ID).FirstOrDefault
            objStaffRankData.CODE = objStaffRank.CODE
            objStaffRankData.NAME = objStaffRank.NAME
            objStaffRankData.REMARK = objStaffRank.REMARK
            objStaffRankData.LEVEL_STAFF = objStaffRank.LEVER
            'objStaffRankData.LEAVE_COUNT = objStaffRank.LEAVE_COUNT
            'objStaffRankData.IS_OVT = objStaffRank.IS_OVT
            Context.SaveChanges(log)
            gID = objStaffRankData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyStaffRank")
            Throw ex
        End Try

    End Function

    Public Function ActiveStaffRank(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal sActive As String) As Boolean
        Dim lstData As List(Of HU_STAFF_RANK)
        Try
            lstData = (From p In Context.HU_STAFF_RANK Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteStaffRank(ByVal lstStaffRank() As StaffRankDTO, ByVal log As UserLog) As Boolean
        Dim lstStaffRankData As List(Of HU_STAFF_RANK)
        Dim lstIDStaffRank As List(Of Decimal) = (From p In lstStaffRank.ToList Select p.ID).ToList
        Try
            lstStaffRankData = (From p In Context.HU_STAFF_RANK Where lstIDStaffRank.Contains(p.ID)).ToList
            For index = 0 To lstStaffRankData.Count - 1
                Context.HU_STAFF_RANK.DeleteObject(lstStaffRankData(index))
            Next
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteStaffRank")
            Throw ex
        End Try

    End Function

#End Region


#Region "Danh mục bảo hộ lao động"
    Public Function GetLabourProtection(ByVal _filter As LabourProtectionDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LabourProtectionDTO)
        Try

            Dim query = From p In Context.HU_LABOURPROTECTION

            Dim lst = query.Select(Function(s) New LabourProtectionDTO With {
                                        .ID = s.ID,
                                        .CODE = s.CODE,
                                        .NAME = s.NAME,
                                        .UNIT_PRICE = s.UNIT_PRICE,
                                        .SDESC = s.SDESC,
                                        .ACTFLG = If(s.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                        .CREATED_DATE = s.CREATED_DATE})
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.UNIT_PRICE.HasValue Then
                lst = lst.Where(Function(p) p.UNIT_PRICE = _filter.UNIT_PRICE)
            End If
            If _filter.SDESC <> "" Then
                lst = lst.Where(Function(p) p.SDESC.ToUpper.Contains(_filter.SDESC.ToUpper))
            End If

            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertLabourProtection(ByVal objTitle As LabourProtectionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_LABOURPROTECTION
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.HU_LABOURPROTECTION.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME = objTitle.NAME
            objTitleData.UNIT_PRICE = objTitle.UNIT_PRICE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.ACTFLG = "A"
            Context.HU_LABOURPROTECTION.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyLabourProtection(ByVal objTitle As LabourProtectionDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As HU_LABOURPROTECTION
        Try
            objTitleData = (From p In Context.HU_LABOURPROTECTION Where p.ID = objTitle.ID).SingleOrDefault
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME = objTitle.NAME
            objTitleData.UNIT_PRICE = objTitle.UNIT_PRICE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateLabourProtection(ByVal _validate As LabourProtectionDTO) As Boolean
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_LABOURPROTECTION
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_LABOURPROTECTION
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_LABOURPROTECTION
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_LABOURPROTECTION
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveLabourProtection(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of HU_LABOURPROTECTION)
        Try
            lstData = (From p In Context.HU_LABOURPROTECTION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteLabourProtection(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstLabourProtectionData As List(Of HU_LABOURPROTECTION)
        Try
            lstLabourProtectionData = (From p In Context.HU_LABOURPROTECTION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstLabourProtectionData.Count - 1
                Context.HU_LABOURPROTECTION.DeleteObject(lstLabourProtectionData(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".LabourProtection")
            Throw ex
        End Try
    End Function

#End Region

#Region "Danh mục thông tin lương "
    Public Function GetPeriodbyYear(ByVal year As Decimal) As List(Of ATPeriodDTO)
        Try
            Dim query = From p In Context.AT_PERIOD Where p.YEAR = year Order By p.MONTH Ascending, p.START_DATE Ascending
            Dim Period = query.Select(Function(p) New ATPeriodDTO With {
                                       .ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .MONTH = p.MONTH,
                                       .PERIOD_NAME = p.PERIOD_NAME,
                                       .START_DATE = p.START_DATE,
                                       .END_DATE = p.END_DATE,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .PERIOD_STANDARD = p.PERIOD_STANDARD})


            Return Period.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "Năng lực"

#Region "CompetencyGroup"

    Public Function GetCompetencyGroup(ByVal _filter As CompetencyGroupDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyGroupDTO)

        Try
            Dim query = From p In Context.HU_COMPETENCY_GROUP
                        Select New CompetencyGroupDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME = p.NAME,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertCompetencyGroup(ByVal objCompetencyGroup As CompetencyGroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyGroupData As New HU_COMPETENCY_GROUP
        Dim iCount As Integer = 0
        Try
            objCompetencyGroupData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_GROUP.EntitySet.Name)
            objCompetencyGroupData.CODE = objCompetencyGroup.CODE
            objCompetencyGroupData.NAME = objCompetencyGroup.NAME
            Context.HU_COMPETENCY_GROUP.AddObject(objCompetencyGroupData)
            Context.SaveChanges(log)
            gID = objCompetencyGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateCompetencyGroup(ByVal _validate As CompetencyGroupDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_COMPETENCY_GROUP
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_COMPETENCY_GROUP
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_COMPETENCY_GROUP
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_COMPETENCY_GROUP
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyCompetencyGroup(ByVal objCompetencyGroup As CompetencyGroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyGroupData As HU_COMPETENCY_GROUP
        Try
            objCompetencyGroupData = (From p In Context.HU_COMPETENCY_GROUP Where p.ID = objCompetencyGroup.ID).FirstOrDefault
            objCompetencyGroupData.CODE = objCompetencyGroup.CODE
            objCompetencyGroupData.NAME = objCompetencyGroup.NAME
            Context.SaveChanges(log)
            gID = objCompetencyGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveCompetencyGroup(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstCompetencyGroupData As List(Of HU_COMPETENCY_GROUP)
        Try
            lstCompetencyGroupData = (From p In Context.HU_COMPETENCY_GROUP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyGroupData.Count - 1
                lstCompetencyGroupData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteCompetencyGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstCompetencyGroupData As List(Of HU_COMPETENCY_GROUP)
        Try

            lstCompetencyGroupData = (From p In Context.HU_COMPETENCY_GROUP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyGroupData.Count - 1
                Context.HU_COMPETENCY_GROUP.DeleteObject(lstCompetencyGroupData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "Competency"

    Public Function GetCompetency(ByVal _filter As CompetencyDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyDTO)

        Try
            Dim query = From p In Context.HU_COMPETENCY
                        From group In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = p.COMPETENCY_GROUP_ID)
                        Select New CompetencyDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME = p.NAME,
                                   .COMPETENCY_GROUP_ID = p.COMPETENCY_GROUP_ID,
                                   .COMPETENCY_GROUP_NAME = group.NAME,
                                   .EFFECT_DATE = p.EFFECT_DATE,
                                   .EXPIRE_DATE = p.EXPIRE_DATE,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_GROUP_NAME.ToUpper.Contains(_filter.COMPETENCY_GROUP_NAME.ToUpper))
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertCompetency(ByVal objCompetency As CompetencyDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyData As New HU_COMPETENCY
        Dim iCount As Integer = 0
        Try
            objCompetencyData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY.EntitySet.Name)
            objCompetencyData.CODE = objCompetency.CODE
            objCompetencyData.NAME = objCompetency.NAME
            objCompetencyData.COMPETENCY_GROUP_ID = objCompetency.COMPETENCY_GROUP_ID
            objCompetencyData.EFFECT_DATE = objCompetency.EFFECT_DATE
            objCompetencyData.EXPIRE_DATE = objCompetency.EXPIRE_DATE
            objCompetencyData.REMARK = objCompetency.REMARK
            Context.HU_COMPETENCY.AddObject(objCompetencyData)
            Context.SaveChanges(log)
            gID = objCompetencyData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateCompetency(ByVal _validate As CompetencyDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_COMPETENCY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_COMPETENCY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_COMPETENCY
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_COMPETENCY
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyCompetency(ByVal objCompetency As CompetencyDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyData As HU_COMPETENCY
        Try
            objCompetencyData = (From p In Context.HU_COMPETENCY Where p.ID = objCompetency.ID).FirstOrDefault
            objCompetencyData.CODE = objCompetency.CODE
            objCompetencyData.NAME = objCompetency.NAME
            objCompetencyData.COMPETENCY_GROUP_ID = objCompetency.COMPETENCY_GROUP_ID
            objCompetencyData.EFFECT_DATE = objCompetency.EFFECT_DATE
            objCompetencyData.EXPIRE_DATE = objCompetency.EXPIRE_DATE
            objCompetencyData.REMARK = objCompetency.REMARK
            Context.SaveChanges(log)
            gID = objCompetencyData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveCompetency(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstCompetencyData As List(Of HU_COMPETENCY)
        Try
            lstCompetencyData = (From p In Context.HU_COMPETENCY Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyData.Count - 1
                lstCompetencyData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteCompetency(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstCompetencyData As List(Of HU_COMPETENCY)
        Try

            lstCompetencyData = (From p In Context.HU_COMPETENCY Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyData.Count - 1
                Context.HU_COMPETENCY.DeleteObject(lstCompetencyData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "CompetencyBuild"

    Public Function GetCompetencyBuild(ByVal _filter As CompetencyBuildDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyBuildDTO)

        Try
            Dim query = From p In Context.HU_COMPETENCY_BUILD
                        From Competencygroup In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = p.COMPETENCY_GROUP_ID)
                        From Competency In Context.HU_COMPETENCY.Where(Function(f) f.ID = p.COMPETENCY_ID)
                        Select New CompetencyBuildDTO With {
                            .ID = p.ID,
                            .COMPETENCY_GROUP_ID = p.COMPETENCY_GROUP_ID,
                            .COMPETENCY_GROUP_NAME = Competencygroup.NAME,
                            .COMPETENCY_ID = p.COMPETENCY_ID,
                            .COMPETENCY_NAME = Competency.NAME,
                            .LEVEL_NUMBER = p.LEVEL_NUMBER,
                            .REMARK = p.REMARK,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.COMPETENCY_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_GROUP_NAME.ToUpper.Contains(_filter.COMPETENCY_GROUP_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_NAME.ToUpper.Contains(_filter.COMPETENCY_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.LEVEL_NUMBER IsNot Nothing Then
                lst = lst.Where(Function(p) p.LEVEL_NUMBER = _filter.LEVEL_NUMBER)
            End If
            lst = lst.Where(Function(p) p.COMPETENCY_GROUP_ID = _filter.COMPETENCY_GROUP_ID)

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertCompetencyBuild(ByVal objCompetencyBuild As CompetencyBuildDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyBuildData As New HU_COMPETENCY_BUILD
        Dim iCount As Integer = 0
        Try
            objCompetencyBuildData = (From p In Context.HU_COMPETENCY_BUILD
                                      Where p.COMPETENCY_ID = objCompetencyBuild.COMPETENCY_ID And
                                      p.COMPETENCY_GROUP_ID = objCompetencyBuild.COMPETENCY_GROUP_ID).FirstOrDefault

            If objCompetencyBuildData IsNot Nothing Then
                objCompetencyBuildData.LEVEL_NUMBER = objCompetencyBuild.LEVEL_NUMBER
                objCompetencyBuildData.REMARK = objCompetencyBuild.REMARK
            Else
                objCompetencyBuildData = New HU_COMPETENCY_BUILD
                objCompetencyBuildData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_BUILD.EntitySet.Name)
                objCompetencyBuildData.COMPETENCY_GROUP_ID = objCompetencyBuild.COMPETENCY_GROUP_ID
                objCompetencyBuildData.COMPETENCY_ID = objCompetencyBuild.COMPETENCY_ID
                objCompetencyBuildData.LEVEL_NUMBER = objCompetencyBuild.LEVEL_NUMBER
                objCompetencyBuildData.REMARK = objCompetencyBuild.REMARK
                Context.HU_COMPETENCY_BUILD.AddObject(objCompetencyBuildData)
            End If
            Context.SaveChanges(log)
            gID = objCompetencyBuildData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyCompetencyBuild(ByVal objCompetencyBuild As CompetencyBuildDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyBuildData As HU_COMPETENCY_BUILD
        Try
            objCompetencyBuildData = (From p In Context.HU_COMPETENCY_BUILD Where p.ID = objCompetencyBuild.ID).FirstOrDefault
            objCompetencyBuildData.COMPETENCY_GROUP_ID = objCompetencyBuild.COMPETENCY_GROUP_ID
            objCompetencyBuildData.COMPETENCY_ID = objCompetencyBuild.COMPETENCY_ID
            objCompetencyBuildData.LEVEL_NUMBER = objCompetencyBuild.LEVEL_NUMBER
            objCompetencyBuildData.REMARK = objCompetencyBuild.REMARK
            Context.SaveChanges(log)
            gID = objCompetencyBuildData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteCompetencyBuild(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstCompetencyBuildData As List(Of HU_COMPETENCY_BUILD)
        Try

            lstCompetencyBuildData = (From p In Context.HU_COMPETENCY_BUILD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyBuildData.Count - 1
                Context.HU_COMPETENCY_BUILD.DeleteObject(lstCompetencyBuildData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "CompetencyStandard"

    Public Function GetCompetencyStandard(ByVal _filter As CompetencyStandardDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyStandardDTO)

        Try
            Dim query = From p In Context.HU_COMPETENCY_STANDARD
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        From Competency In Context.HU_COMPETENCY.Where(Function(f) f.ID = p.COMPETENCY_ID)
                        From Competencygroup In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = Competency.COMPETENCY_GROUP_ID)
                        Select New CompetencyStandardDTO With {
                            .ID = p.ID,
                            .TITLE_ID = p.TITLE_ID,
                            .TITLE_NAME = title.NAME_VN,
                            .COMPETENCY_GROUP_ID = Competency.COMPETENCY_GROUP_ID,
                            .COMPETENCY_GROUP_NAME = Competencygroup.NAME,
                            .COMPETENCY_ID = p.COMPETENCY_ID,
                            .COMPETENCY_NAME = Competency.NAME,
                            .LEVEL_NUMBER = p.LEVEL_NUMBER,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_GROUP_NAME.ToUpper.Contains(_filter.COMPETENCY_GROUP_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_NAME.ToUpper.Contains(_filter.COMPETENCY_NAME.ToUpper))
            End If
            If _filter.LEVEL_NUMBER IsNot Nothing Then
                lst = lst.Where(Function(p) p.LEVEL_NUMBER = _filter.LEVEL_NUMBER)
            End If
            lst = lst.Where(Function(p) p.TITLE_ID = _filter.TITLE_ID)

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Dim lstData = lst.ToList
            For Each item In lstData
                item.LEVEL_NUMBER_NAME = item.LEVEL_NUMBER.Value.ToString & "/4"
            Next
            Return lstData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertCompetencyStandard(ByVal objCompetencyStandard As CompetencyStandardDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyStandardData As New HU_COMPETENCY_STANDARD
        Dim iCount As Integer = 0
        Try
            objCompetencyStandardData = (From p In Context.HU_COMPETENCY_STANDARD
                                         Where p.COMPETENCY_ID = objCompetencyStandard.COMPETENCY_ID And
                                         p.TITLE_ID = objCompetencyStandard.TITLE_ID).FirstOrDefault

            If objCompetencyStandardData IsNot Nothing Then
                objCompetencyStandardData.LEVEL_NUMBER = objCompetencyStandard.LEVEL_NUMBER
            Else
                objCompetencyStandardData = New HU_COMPETENCY_STANDARD
                objCompetencyStandardData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_STANDARD.EntitySet.Name)
                objCompetencyStandardData.TITLE_ID = objCompetencyStandard.TITLE_ID
                objCompetencyStandardData.COMPETENCY_ID = objCompetencyStandard.COMPETENCY_ID
                objCompetencyStandardData.LEVEL_NUMBER = objCompetencyStandard.LEVEL_NUMBER
                Context.HU_COMPETENCY_STANDARD.AddObject(objCompetencyStandardData)
            End If
            Context.SaveChanges(log)
            gID = objCompetencyStandardData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyCompetencyStandard(ByVal objCompetencyStandard As CompetencyStandardDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyStandardData As HU_COMPETENCY_STANDARD
        Try
            objCompetencyStandardData = (From p In Context.HU_COMPETENCY_STANDARD Where p.ID = objCompetencyStandard.ID).FirstOrDefault
            objCompetencyStandardData.TITLE_ID = objCompetencyStandard.TITLE_ID
            objCompetencyStandardData.COMPETENCY_ID = objCompetencyStandard.COMPETENCY_ID
            objCompetencyStandardData.LEVEL_NUMBER = objCompetencyStandard.LEVEL_NUMBER
            Context.SaveChanges(log)
            gID = objCompetencyStandardData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteCompetencyStandard(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstCompetencyStandardData As List(Of HU_COMPETENCY_STANDARD)
        Try

            lstCompetencyStandardData = (From p In Context.HU_COMPETENCY_STANDARD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyStandardData.Count - 1
                Context.HU_COMPETENCY_STANDARD.DeleteObject(lstCompetencyStandardData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "CompetencyAppendix"

    Public Function GetCompetencyAppendix(ByVal _filter As CompetencyAppendixDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyAppendixDTO)

        Try
            Dim query = From p In Context.HU_COMPETENCY_APPENDIX
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        Select New CompetencyAppendixDTO With {
                                   .ID = p.ID,
                                   .TITLE_ID = p.TITLE_ID,
                                   .TITLE_NAME = title.NAME_VN,
                                   .REMARK = p.REMARK,
                                   .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.TITLE_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.TITLE_ID = _filter.TITLE_ID)

            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertCompetencyAppendix(ByVal objCompetencyAppendix As CompetencyAppendixDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyAppendixData As New HU_COMPETENCY_APPENDIX
        Dim iCount As Integer = 0
        Try
            objCompetencyAppendixData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_APPENDIX.EntitySet.Name)
            objCompetencyAppendixData.TITLE_ID = objCompetencyAppendix.TITLE_ID
            objCompetencyAppendixData.REMARK = objCompetencyAppendix.REMARK
            Context.HU_COMPETENCY_APPENDIX.AddObject(objCompetencyAppendixData)
            Context.SaveChanges(log)
            gID = objCompetencyAppendixData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyCompetencyAppendix(ByVal objCompetencyAppendix As CompetencyAppendixDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyAppendixData As HU_COMPETENCY_APPENDIX
        Try
            objCompetencyAppendixData = (From p In Context.HU_COMPETENCY_APPENDIX Where p.ID = objCompetencyAppendix.ID).FirstOrDefault
            objCompetencyAppendixData.TITLE_ID = objCompetencyAppendix.TITLE_ID
            objCompetencyAppendixData.REMARK = objCompetencyAppendix.REMARK
            Context.SaveChanges(log)
            gID = objCompetencyAppendixData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteCompetencyAppendix(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstCompetencyAppendixData As List(Of HU_COMPETENCY_APPENDIX)
        Try

            lstCompetencyAppendixData = (From p In Context.HU_COMPETENCY_APPENDIX Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyAppendixData.Count - 1
                Context.HU_COMPETENCY_APPENDIX.DeleteObject(lstCompetencyAppendixData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "CompetencyEmp"

    Public Function GetCompetencyEmp(ByVal _filter As CompetencyEmpDTO) As List(Of CompetencyEmpDTO)

        Try
            Dim query = From stand In Context.HU_COMPETENCY_STANDARD
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = stand.TITLE_ID)
                        From Competency In Context.HU_COMPETENCY.Where(Function(f) f.ID = stand.COMPETENCY_ID)
                        From Competencygroup In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = Competency.COMPETENCY_GROUP_ID)
                        From p In Context.HU_COMPETENCY_EMP.Where(Function(f) f.TITLE_ID = stand.TITLE_ID And
                                                                      f.EMPLOYEE_ID = _filter.EMPLOYEE_ID And
                                                                      f.COMPETENCY_ID = stand.COMPETENCY_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) p.EMPLOYEE_ID = f.ID).DefaultIfEmpty
                        Where stand.TITLE_ID = _filter.TITLE_ID
                        Select New CompetencyEmpDTO With {
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .EMPLOYEE_ID = p.EMPLOYEE_ID,
                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .TITLE_ID = stand.TITLE_ID,
                            .TITLE_NAME = title.NAME_VN,
                            .COMPETENCY_GROUP_ID = Competency.COMPETENCY_GROUP_ID,
                            .COMPETENCY_GROUP_NAME = Competencygroup.NAME,
                            .COMPETENCY_ID = stand.COMPETENCY_ID,
                            .COMPETENCY_NAME = Competency.NAME,
                            .LEVEL_NUMBER_STANDARD = stand.LEVEL_NUMBER,
                            .LEVEL_NUMBER_EMP = p.LEVEL_NUMBER,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_GROUP_NAME.ToUpper.Contains(_filter.COMPETENCY_GROUP_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_NAME.ToUpper.Contains(_filter.COMPETENCY_NAME.ToUpper))
            End If

            Dim lstData = lst.ToList
            For Each item In lstData
                If item.LEVEL_NUMBER_EMP IsNot Nothing Then
                    item.LEVEL_NUMBER_EMP_NAME = item.LEVEL_NUMBER_EMP.Value.ToString & "/4"
                End If
                If item.LEVEL_NUMBER_STANDARD IsNot Nothing Then
                    item.LEVEL_NUMBER_STANDARD_NAME = item.LEVEL_NUMBER_STANDARD.Value.ToString & "/4"
                End If
            Next
            Return lstData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function UpdateCompetencyEmp(ByVal lstCom As List(Of CompetencyEmpDTO), ByVal log As UserLog) As Boolean
        Dim objCompetencyEmpData As New HU_COMPETENCY_EMP
        Dim iCount As Integer = 0
        Try
            For Each obj In lstCom
                objCompetencyEmpData = (From p In Context.HU_COMPETENCY_EMP
                                        Where p.COMPETENCY_ID = obj.COMPETENCY_ID And
                                        p.EMPLOYEE_ID = obj.EMPLOYEE_ID).FirstOrDefault

                If objCompetencyEmpData IsNot Nothing Then
                    objCompetencyEmpData.LEVEL_NUMBER = obj.LEVEL_NUMBER_EMP
                Else
                    objCompetencyEmpData = New HU_COMPETENCY_EMP
                    objCompetencyEmpData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_STANDARD.EntitySet.Name)
                    objCompetencyEmpData.EMPLOYEE_ID = obj.EMPLOYEE_ID
                    objCompetencyEmpData.TITLE_ID = obj.TITLE_ID
                    objCompetencyEmpData.COMPETENCY_ID = obj.COMPETENCY_ID
                    objCompetencyEmpData.LEVEL_NUMBER = obj.LEVEL_NUMBER_EMP
                    Context.HU_COMPETENCY_EMP.AddObject(objCompetencyEmpData)
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "CompetencyPeriod"

    Public Function GetCompetencyPeriod(ByVal _filter As CompetencyPeriodDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyPeriodDTO)

        Try
            Dim query = From p In Context.HU_COMPETENCY_PERIOD
                        Select New CompetencyPeriodDTO With {
                                   .ID = p.ID,
                                   .YEAR = p.YEAR,
                                   .NAME = p.NAME,
                                   .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If _filter.YEAR IsNot Nothing Then
                lst = lst.Where(Function(p) p.YEAR = _filter.YEAR)
            End If
            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertCompetencyPeriod(ByVal objCompetencyPeriod As CompetencyPeriodDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyPeriodData As New HU_COMPETENCY_PERIOD
        Dim iCount As Integer = 0
        Try
            objCompetencyPeriodData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_PERIOD.EntitySet.Name)
            objCompetencyPeriodData.YEAR = objCompetencyPeriod.YEAR
            objCompetencyPeriodData.NAME = objCompetencyPeriod.NAME
            Context.HU_COMPETENCY_PERIOD.AddObject(objCompetencyPeriodData)
            Context.SaveChanges(log)
            gID = objCompetencyPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyCompetencyPeriod(ByVal objCompetencyPeriod As CompetencyPeriodDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyPeriodData As HU_COMPETENCY_PERIOD
        Try
            objCompetencyPeriodData = (From p In Context.HU_COMPETENCY_PERIOD Where p.ID = objCompetencyPeriod.ID).FirstOrDefault
            objCompetencyPeriodData.YEAR = objCompetencyPeriod.YEAR
            objCompetencyPeriodData.NAME = objCompetencyPeriod.NAME
            Context.SaveChanges(log)
            gID = objCompetencyPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteCompetencyPeriod(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstCompetencyPeriodData As List(Of HU_COMPETENCY_PERIOD)
        Try

            lstCompetencyPeriodData = (From p In Context.HU_COMPETENCY_PERIOD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyPeriodData.Count - 1
                Context.HU_COMPETENCY_PERIOD.DeleteObject(lstCompetencyPeriodData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "CompetencyAssDtl"

    Public Function GetCompetencyAss(ByVal _filter As CompetencyAssDTO) As List(Of CompetencyAssDTO)

        Try
            Dim query = From ass In Context.HU_COMPETENCY_ASS
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ass.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = ass.TITLE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID)
                        Where ass.COMPETENCY_PERIOD_ID = _filter.COMPETENCY_PERIOD_ID
                        Select New CompetencyAssDTO With {
                            .ID = ass.ID,
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .EMPLOYEE_ID = ass.EMPLOYEE_ID,
                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .TITLE_ID = t.ID,
                            .TITLE_NAME = t.NAME_VN,
                            .ORG_ID = o.ID,
                            .ORG_NAME = o.NAME_VN,
                            .CREATED_DATE = ass.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.ID <> 0 Then
                lst = lst.Where(Function(p) p.ID = _filter.ID)
            End If
            If _filter.EMPLOYEE_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            Dim lstData = lst.ToList
            Return lstData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetCompetencyAssDtl(ByVal _filter As CompetencyAssDtlDTO) As List(Of CompetencyAssDtlDTO)

        Try
            Dim query = From stand In Context.HU_COMPETENCY_STANDARD
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = stand.TITLE_ID)
                        From Competency In Context.HU_COMPETENCY.Where(Function(f) f.ID = stand.COMPETENCY_ID)
                        From Competencygroup In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = Competency.COMPETENCY_GROUP_ID)
                        From ass In Context.HU_COMPETENCY_ASS.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID And
                                                                        f.COMPETENCY_PERIOD_ID = _filter.COMPETENCY_PERIOD_ID And
                                                                        stand.TITLE_ID = f.TITLE_ID).DefaultIfEmpty
                        From p In Context.HU_COMPETENCY_ASSDTL.Where(Function(f) f.COMPETENCY_ASS_ID = ass.ID And
                                                                         f.COMPETENCY_ID = stand.COMPETENCY_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ass.EMPLOYEE_ID).DefaultIfEmpty
                        Where stand.TITLE_ID = _filter.TITLE_ID
                        Select New CompetencyAssDtlDTO With {
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .EMPLOYEE_ID = ass.EMPLOYEE_ID,
                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .TITLE_ID = stand.TITLE_ID,
                            .TITLE_NAME = title.NAME_VN,
                            .COMPETENCY_GROUP_ID = Competency.COMPETENCY_GROUP_ID,
                            .COMPETENCY_GROUP_NAME = Competencygroup.NAME,
                            .COMPETENCY_ID = stand.COMPETENCY_ID,
                            .COMPETENCY_NAME = Competency.NAME,
                            .LEVEL_NUMBER_STANDARD = stand.LEVEL_NUMBER,
                            .LEVEL_NUMBER_EMP = p.LEVEL_NUMBER,
                            .LEVEL_NUMBER_ASS = p.LEVEL_NUMBER,
                            .REMARK = p.REMARK,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_GROUP_NAME.ToUpper.Contains(_filter.COMPETENCY_GROUP_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_NAME.ToUpper.Contains(_filter.COMPETENCY_NAME.ToUpper))
            End If

            Dim lstData = lst.ToList
            For Each item In lstData
                If item.LEVEL_NUMBER_EMP IsNot Nothing Then
                    item.LEVEL_NUMBER_EMP_NAME = item.LEVEL_NUMBER_EMP.Value.ToString & "/4"
                End If
                If item.LEVEL_NUMBER_STANDARD IsNot Nothing Then
                    item.LEVEL_NUMBER_STANDARD_NAME = item.LEVEL_NUMBER_STANDARD.Value.ToString & "/4"
                End If
                If item.LEVEL_NUMBER_ASS IsNot Nothing Then
                    item.LEVEL_NUMBER_ASS_NAME = item.LEVEL_NUMBER_ASS.Value.ToString & "/4"
                End If
            Next
            Return lstData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function UpdateCompetencyAssDtl(ByVal objAss As CompetencyAssDTO, ByVal lstCom As List(Of CompetencyAssDtlDTO), ByVal log As UserLog) As Boolean
        Dim objCompetencyAssDtlData As HU_COMPETENCY_ASSDTL
        Dim objCompetencyAssData As HU_COMPETENCY_ASS
        Dim iCount As Integer = 0
        Try
            If lstCom.Count > 0 Then
                objCompetencyAssData = (From p In Context.HU_COMPETENCY_ASS
                                        Where p.COMPETENCY_PERIOD_ID = objAss.COMPETENCY_PERIOD_ID And
                                        p.EMPLOYEE_ID = objAss.EMPLOYEE_ID).FirstOrDefault
                If objCompetencyAssData Is Nothing Then
                    objCompetencyAssData = New HU_COMPETENCY_ASS
                    objCompetencyAssData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_ASS.EntitySet.Name)
                    objCompetencyAssData.EMPLOYEE_ID = objAss.EMPLOYEE_ID
                    objCompetencyAssData.COMPETENCY_PERIOD_ID = objAss.COMPETENCY_PERIOD_ID
                    objCompetencyAssData.TITLE_ID = objAss.TITLE_ID
                    Context.HU_COMPETENCY_ASS.AddObject(objCompetencyAssData)
                End If
            End If

            For Each obj In lstCom
                objCompetencyAssDtlData = (From p In Context.HU_COMPETENCY_ASSDTL
                                           Where p.COMPETENCY_ID = obj.COMPETENCY_ID And
                                           p.COMPETENCY_ASS_ID = objCompetencyAssData.ID).FirstOrDefault

                If objCompetencyAssDtlData IsNot Nothing Then
                    objCompetencyAssDtlData.LEVEL_NUMBER = obj.LEVEL_NUMBER_ASS
                    objCompetencyAssDtlData.REMARK = obj.REMARK
                Else
                    objCompetencyAssDtlData = New HU_COMPETENCY_ASSDTL
                    objCompetencyAssDtlData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_ASSDTL.EntitySet.Name)
                    objCompetencyAssDtlData.COMPETENCY_ASS_ID = objCompetencyAssData.ID
                    objCompetencyAssDtlData.COMPETENCY_ID = obj.COMPETENCY_ID
                    objCompetencyAssDtlData.LEVEL_NUMBER = obj.LEVEL_NUMBER_ASS
                    objCompetencyAssDtlData.REMARK = obj.REMARK
                    Context.HU_COMPETENCY_ASSDTL.AddObject(objCompetencyAssDtlData)
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteCompetencyAss(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstCompetencyAssData As List(Of HU_COMPETENCY_ASS)
        Dim lstCompetencyAssDtlData As List(Of HU_COMPETENCY_ASSDTL)
        Try

            lstCompetencyAssData = (From p In Context.HU_COMPETENCY_ASS Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyAssData.Count - 1
                Context.HU_COMPETENCY_ASS.DeleteObject(lstCompetencyAssData(index))
            Next

            lstCompetencyAssDtlData = (From p In Context.HU_COMPETENCY_ASSDTL Where lstID.Contains(p.COMPETENCY_ASS_ID)).ToList
            For index = 0 To lstCompetencyAssDtlData.Count - 1
                Context.HU_COMPETENCY_ASSDTL.DeleteObject(lstCompetencyAssDtlData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#End Region
#Region "DM Khen thưởng"
    Public Function GetCommendList(ByVal _filter As CommendListDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendListDTO)
        Try
            Dim query = From p In Context.HU_COMMEND_LIST
                        From da In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DATATYPE_ID).DefaultIfEmpty
                        From t In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.TYPE_ID).DefaultIfEmpty
                        From l In Context.HU_COMMEND_LEVEL.Where(Function(x) x.ID = p.LEVEL_ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.OBJECT_ID).DefaultIfEmpty
                        Select New CommendListDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME = p.NAME,
                                   .REMARK = p.REMARK,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                   .DATATYPE_ID = p.DATATYPE_ID,
                                   .DATATYPE_NAME = da.NAME_VN,
                                   .TYPE_ID = p.TYPE_ID,
                                   .TYPE_NAME = t.NAME_VN,
                                   .NUMBER_ORDER = p.NUMBER_ORDER,
                                   .LEVEL_ID = p.LEVEL_ID,
                                   .LEVEL_NAME = l.NAME,
                                   .OBJECT_ID = p.OBJECT_ID,
                                   .OBJECT_NAME = o.NAME_VN,
                                   .EXCEL = p.EXCEL,
                                   .EXCEL_BOOL = If(p.EXCEL = -1, True, False)}

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
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            If _filter.DATATYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.DATATYPE_NAME.ToUpper.Contains(_filter.DATATYPE_NAME.ToUpper))
            End If

            If _filter.TYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TYPE_NAME.ToUpper.Contains(_filter.TYPE_NAME.ToUpper))
            End If

            If _filter.LEVEL_NAME <> "" Then
                lst = lst.Where(Function(p) p.LEVEL_NAME.ToUpper.Contains(_filter.LEVEL_NAME.ToUpper))
            End If

            If _filter.OBJECT_NAME <> "" Then
                lst = lst.Where(Function(p) p.OBJECT_NAME.ToUpper.Contains(_filter.OBJECT_NAME.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetListCommendList(ByVal actflg As String) As List(Of CommendListDTO)
        Try
            If actflg <> "" Then
                Dim query = From p In Context.HU_COMMEND_LIST
                            From da In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DATATYPE_ID).DefaultIfEmpty
                            From t In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.TYPE_ID).DefaultIfEmpty
                            From l In Context.HU_COMMEND_LEVEL.Where(Function(x) x.ID = p.LEVEL_ID).DefaultIfEmpty
                            From o In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.OBJECT_ID).DefaultIfEmpty
                            Where p.ACTFLG = actflg
                            Select New CommendListDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .DATATYPE_ID = p.DATATYPE_ID,
                                       .DATATYPE_NAME = da.NAME_VN,
                                       .TYPE_ID = p.TYPE_ID,
                                       .TYPE_NAME = t.NAME_VN,
                                       .NUMBER_ORDER = p.NUMBER_ORDER,
                                       .LEVEL_ID = p.LEVEL_ID,
                                       .LEVEL_NAME = l.NAME,
                                       .OBJECT_ID = p.OBJECT_ID,
                                       .OBJECT_NAME = o.NAME_VN,
                                       .EXCEL = p.EXCEL,
                                       .EXCEL_BOOL = If(p.EXCEL = -1, True, False)}

                Return query.ToList
            Else
                Dim query = From p In Context.HU_COMMEND_LIST
                            From da In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DATATYPE_ID).DefaultIfEmpty
                            From t In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.TYPE_ID).DefaultIfEmpty
                            From l In Context.HU_COMMEND_LEVEL.Where(Function(x) x.ID = p.LEVEL_ID).DefaultIfEmpty
                            From o In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.OBJECT_ID).DefaultIfEmpty
                            Select New CommendListDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .DATATYPE_ID = p.DATATYPE_ID,
                                       .DATATYPE_NAME = da.NAME_VN,
                                       .TYPE_ID = p.TYPE_ID,
                                       .TYPE_NAME = t.NAME_VN,
                                       .NUMBER_ORDER = p.NUMBER_ORDER,
                                       .LEVEL_ID = p.LEVEL_ID,
                                       .LEVEL_NAME = l.NAME,
                                       .OBJECT_ID = p.OBJECT_ID,
                                       .OBJECT_NAME = o.NAME_VN,
                                       .EXCEL = p.EXCEL,
                                       .EXCEL_BOOL = If(p.EXCEL = -1, True, False)}

                Return query.ToList
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetCommendListID(ByVal ID As Decimal) As List(Of CommendListDTO)
        Try
            Dim query = From p In Context.HU_COMMEND_LIST
                        From da In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DATATYPE_ID).DefaultIfEmpty
                        From t In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.TYPE_ID).DefaultIfEmpty
                        From l In Context.HU_COMMEND_LEVEL.Where(Function(x) x.ID = p.LEVEL_ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.OBJECT_ID).DefaultIfEmpty
                        Where p.ID = ID
                        Select New CommendListDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME = p.NAME,
                                   .REMARK = p.REMARK,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                   .DATATYPE_ID = p.DATATYPE_ID,
                                   .DATATYPE_NAME = da.NAME_VN,
                                   .TYPE_ID = p.TYPE_ID,
                                   .TYPE_NAME = t.NAME_VN,
                                   .NUMBER_ORDER = p.NUMBER_ORDER,
                                   .LEVEL_ID = p.LEVEL_ID,
                                   .LEVEL_NAME = l.NAME,
                                   .OBJECT_ID = p.OBJECT_ID,
                                   .OBJECT_NAME = o.NAME_VN,
                                  .EXCEL = p.EXCEL,
                                   .EXCEL_BOOL = If(p.EXCEL = -1, True, False)}

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertCommendList(ByVal objCommendList As CommendListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCommendListData As New HU_COMMEND_LIST
        Try
            objCommendListData.ID = Utilities.GetNextSequence(Context, Context.HU_COMMEND_LIST.EntitySet.Name)
            objCommendListData.CODE = objCommendList.CODE
            objCommendListData.NAME = objCommendList.NAME
            objCommendListData.DATATYPE_ID = objCommendList.DATATYPE_ID
            objCommendListData.TYPE_ID = objCommendList.TYPE_ID
            objCommendListData.OBJECT_ID = objCommendList.OBJECT_ID
            objCommendListData.NUMBER_ORDER = objCommendList.NUMBER_ORDER
            objCommendListData.LEVEL_ID = objCommendList.LEVEL_ID
            objCommendListData.REMARK = objCommendList.REMARK
            objCommendListData.ACTFLG = objCommendList.ACTFLG
            objCommendListData.EXCEL = objCommendList.EXCEL
            Context.HU_COMMEND_LIST.AddObject(objCommendListData)
            Context.SaveChanges(log)
            gID = objCommendListData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ModifyCommendList(ByVal objCommendList As CommendListDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCommendListData As New HU_COMMEND_LIST With {.ID = objCommendList.ID}
        Try
            Context.HU_COMMEND_LIST.Attach(objCommendListData)
            objCommendListData.ID = objCommendList.ID
            objCommendListData.CODE = objCommendList.CODE
            objCommendListData.NAME = objCommendList.NAME
            objCommendListData.DATATYPE_ID = objCommendList.DATATYPE_ID
            objCommendListData.TYPE_ID = objCommendList.TYPE_ID
            objCommendListData.OBJECT_ID = objCommendList.OBJECT_ID
            objCommendListData.NUMBER_ORDER = objCommendList.NUMBER_ORDER
            objCommendListData.LEVEL_ID = objCommendList.LEVEL_ID
            objCommendListData.REMARK = objCommendList.REMARK
            objCommendListData.EXCEL = objCommendList.EXCEL
            Context.SaveChanges(log)
            gID = objCommendListData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ActiveCommendList(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                  ByVal log As UserLog) As Boolean
        Dim lstCommendListData As List(Of HU_COMMEND_LIST)
        Try
            lstCommendListData = (From p In Context.HU_COMMEND_LIST Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCommendListData.Count - 1
                lstCommendListData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function DeleteCommendList(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        Try
            Dim lstData As List(Of HU_COMMEND_LIST)
            lstData = (From p In Context.HU_COMMEND_LIST Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_COMMEND_LIST.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateCommendList(ByVal _validate As CommendListDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_COMMEND_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_COMMEND_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If Not IsNothing(_validate.LEVEL_ID) Then
                    If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                        query = (From p In Context.HU_COMMEND_LEVEL
                                 Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                                 And p.ID = _validate.ID).FirstOrDefault
                        Return (Not query Is Nothing)
                    End If
                    If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                        query = (From p In Context.HU_COMMEND_LEVEL
                                 Where p.ID = _validate.ID).FirstOrDefault
                        Return (query Is Nothing)
                    End If
                Else
                    If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                        query = (From p In Context.HU_COMMEND_LIST
                                 Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                                 And p.ID = _validate.ID).FirstOrDefault
                        Return (Not query Is Nothing)
                    End If
                    If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                        query = (From p In Context.HU_COMMEND_LIST
                                 Where p.ID = _validate.ID).FirstOrDefault
                        Return (query Is Nothing)
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetCommendCode(ByVal id As Decimal) As String
        Try
            Dim query = From p In Context.HU_COMMEND_LIST
                        Where p.ID = id
                        Select p.CODE

            Return query.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

#Region "Commend_Level - Cấp khen thưởng"


    Public Function GetCommendLevel(ByVal _filter As CommendLevelDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendLevelDTO)

        Try
            Dim query = From p In Context.HU_COMMEND_LEVEL
                        Select New CommendLevelDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME = p.NAME,
                                   .COMMEND_LEVEL = p.COMMEND_LEVEL,
                                   .REMARK = p.REMARK,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}

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
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetCommendLevelID(ByVal ID As Decimal) As CommendLevelDTO

        Try
            Dim query = (From p In Context.HU_COMMEND_LEVEL
                         Where p.ID = ID
                         Select New CommendLevelDTO With {
                                    .ID = p.ID,
                                    .CODE = p.CODE,
                                   .NAME = p.NAME,
                                    .COMMEND_LEVEL = p.COMMEND_LEVEL,
                                    .REMARK = p.REMARK,
                                    .CREATED_DATE = p.CREATED_DATE,
                                    .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}).SingleOrDefault

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertCommendLevel(ByVal objCommendLevel As CommendLevelDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCommendLevelData As New HU_COMMEND_LEVEL
        Dim iCount As Integer = 0
        Try
            objCommendLevelData.ID = Utilities.GetNextSequence(Context, Context.HU_COMMEND_LEVEL.EntitySet.Name)
            objCommendLevelData.CODE = objCommendLevel.CODE
            objCommendLevelData.NAME = objCommendLevel.NAME
            objCommendLevelData.COMMEND_LEVEL = objCommendLevel.COMMEND_LEVEL
            objCommendLevelData.ACTFLG = objCommendLevel.ACTFLG
            objCommendLevelData.REMARK = objCommendLevel.REMARK
            Context.HU_COMMEND_LEVEL.AddObject(objCommendLevelData)
            Context.SaveChanges(log)
            gID = objCommendLevelData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateCommendLevel(ByVal _validate As CommendLevelDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_COMMEND_LEVEL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_COMMEND_LEVEL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_COMMEND_LEVEL
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_COMMEND_LEVEL
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyCommendLevel(ByVal objCommendLevel As CommendLevelDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCommendLevelData As HU_COMMEND_LEVEL
        Try
            objCommendLevelData = (From p In Context.HU_COMMEND_LEVEL Where p.ID = objCommendLevel.ID).FirstOrDefault
            objCommendLevelData.ID = objCommendLevel.ID
            objCommendLevelData.CODE = objCommendLevel.CODE
            objCommendLevelData.NAME = objCommendLevel.NAME
            objCommendLevelData.COMMEND_LEVEL = objCommendLevel.COMMEND_LEVEL
            objCommendLevelData.REMARK = objCommendLevel.REMARK
            Context.SaveChanges(log)
            gID = objCommendLevelData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveCommendLevel(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstCommendLevelData As List(Of HU_COMMEND_LEVEL)
        Try
            lstCommendLevelData = (From p In Context.HU_COMMEND_LEVEL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCommendLevelData.Count - 1
                lstCommendLevelData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteCommendLevel(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstCommendLevelData As List(Of HU_COMMEND_LEVEL)
        Try

            lstCommendLevelData = (From p In Context.HU_COMMEND_LEVEL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCommendLevelData.Count - 1
                Context.HU_COMMEND_LEVEL.DeleteObject(lstCommendLevelData(index))
            Next

            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region

#Region "Khoa dao tao - Course"
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
#End Region

#Region "Tìm kiếm kế nhiệm (Talent Pool)"

    Public Function GetTalentPool(ByVal PageIndex As Integer,
                                  ByVal PageSize As Integer,
                                  ByRef Total As Integer, ByVal _param As ParamDTO,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc",
                                  Optional ByVal log As UserLog = Nothing) As List(Of TalentPoolDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_TALENT_POOL
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From e_cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New TalentPoolDTO With {.ID = p.p.ID,
                                                                    .CODE = p.e.EMPLOYEE_CODE,
                                                                    .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                                                    .BIRTH_DAY = p.e_cv.BIRTH_DATE,
                                                                    .GENDER = If(p.e_cv.GENDER = 565, "Nam", "Nữ"),
                                                                    .TITLE_NAME = p.t.NAME_VN,
                                                                    .ORG_NAME = p.o.NAME_VN,
                                                                    .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                                    .ACTFLG = p.p.ACTFLG,
                                                                    .FILTER_ID = p.p.FILTER_ID,
                                                                    .EMP_SUCCESS_ID = p.p.EMP_SUCCESS_ID,
                                                                    .EMP_SUCCESS_NAME = p.e.FULLNAME_VN,
                                                                    .TITLE_SUCCESS_NAME = p.t.NAME_VN,
                                                                    .NOTE = p.p.NOTE,
                                                                    .CREATED_DATE = p.p.CREATED_DATE,
                                                                    .CREATED_BY = p.p.CREATED_BY,
                                                                    .CREATED_LOG = p.p.CREATED_LOG,
                                                                    .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                    .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                    .MODIFIED_LOG = p.p.MODIFIED_LOG})

            lst = lst.Where(Function(p) p.ACTFLG = "A")

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertTalentPool(ByVal lstTalentPool As List(Of TalentPoolDTO), ByVal log As UserLog) As Boolean
        Try
            For idx = 0 To lstTalentPool.Count - 1
                Dim obj As TalentPoolDTO = lstTalentPool(idx)
                Dim objTalentPoolData As New HU_TALENT_POOL
                objTalentPoolData.ID = Utilities.GetNextSequence(Context, Context.HU_TALENT_POOL.EntitySet.Name)
                objTalentPoolData.EMPLOYEE_ID = obj.EMPLOYEE_ID
                objTalentPoolData.FILTER_ID = obj.FILTER_ID
                objTalentPoolData.ACTFLG = "A"
                Context.HU_TALENT_POOL.AddObject(objTalentPoolData)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveTalentPool(ByVal lst As List(Of Decimal), ByVal sActive As String,
                               ByVal log As UserLog) As Boolean

        Dim lstTalentPool As List(Of HU_TALENT_POOL)
        Try
            lstTalentPool = (From p In Context.HU_TALENT_POOL Where lst.Contains(p.ID)).ToList
            For index = 0 To lstTalentPool.Count - 1
                lstTalentPool(index).ACTFLG = sActive
                lstTalentPool(index).MODIFIED_DATE = DateTime.Now
                lstTalentPool(index).MODIFIED_BY = log.Username
                lstTalentPool(index).MODIFIED_LOG = log.ComputerName
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function FILTER_TALENT_POOL(ByVal obj As FilterParamDTO, ByVal log As UserLog) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataSet = cls.ExecuteStore("PKG_TALENT_POOL.FILTER_TALENT_POOL",
                                           New With {.P_CON_POSTION = obj.P_CON_POSTION,
                                                     .P_CON_KN_VT = obj.P_CON_KN_VT,
                                                     .P_CON_DT_CT = obj.P_CON_DT_CT,
                                                     .P_CON_SENIOR_KN = obj.P_CON_SENIOR_KN,
                                                     .P_CON_AGE = obj.P_CON_AGE,
                                                     .P_CON_TD_HV = obj.P_CON_TD_HV,
                                                     .P_CON_BC_CC = obj.P_CON_BC_CC,
                                                     .P_CON_KDT = obj.P_CON_KDT,
                                                     .P_CON_NL_CM = obj.P_CON_NL_CM,
                                                     .P_CON_GENDER = obj.P_CON_GENDER,
                                                     .P_CON_KY_DG = obj.P_CON_KY_DG,
                                                     .P_CON_KQ_DG = obj.P_CON_KQ_DG,
                                                     .P_CON_STAFF_RANK = obj.P_CON_STAFF_RANK,
                                                     .P_USERNAME = log.Username,
                                                     .P_ORG_ID = obj.P_ORG_ID,
                                                     .DATA = cls.OUT_CURSOR}, False)
            Return dtData.Tables(0)
        End Using
        Return Nothing
    End Function
#End Region

#Region "Location"
    Public Function GetLocationID(ByVal ID As Decimal) As LocationDTO
        Dim query As LocationDTO
        Try
            query = (From p In Context.HU_LOCATION
                     Where p.ID = ID
                     Select New LocationDTO With {.ID = p.ID,
                                                  .CODE = p.CODE,
                                                  .ORG_ID = p.ORG_ID,
                                                  .ADDRESS = p.ADDRESS,
                                                  .CONTRACT_PLACE = p.CONTRACT_PLACE,
                                                  .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                  .LOCATION_SHORT_NAME = p.LOCATION_SHORT_NAME,
                                                  .WORK_ADDRESS = p.WORK_ADDRESS,
                                                  .PHONE = p.PHONE,
                                                  .FAX = p.FAX,
                                                  .WEBSITE = p.WEBSITE,
                                                  .ACCOUNT_NUMBER = p.ACCOUNT_NUMBER,
                                                  .BANK_ID = p.BANK_ID,
                                                  .TAX_CODE = p.TAX_CODE,
                                                  .TAX_DATE = p.TAX_DATE,
                                                  .TAX_PLACE = p.TAX_PLACE,
                                                  .EMP_LAW_ID = p.EMP_LAW_ID,
                                                  .EMP_SIGNCONTRACT_ID = p.EMP_SIGNCONTRACT_ID,
                                                  .BUSINESS_NAME = p.BUSINESS_NAME,
                                                  .BUSINESS_NUMBER = p.BUSINESS_NUMBER,
                                                  .NOTE = p.NOTE,
                                                  .LOCATION_EN_NAME = p.LOCATION_EN_NAME,
                                                  .LOCATION_VN_NAME = p.LOCATION_VN_NAME,
                                                  .BUSINESS_REG_DATE = p.BUSINESS_REG_DATE,
                                                  .BANK_BRANCH_ID = p.BANK_BRANCH_ID,
                                                  .PROVINCE_ID = p.PROVINCE_ID,
                                                  .DISTRICT_ID = p.DISTRICT_ID,
                                                  .WARD_ID = p.WARD_ID,
                                                  .IS_SIGN_CONTRACT = p.IS_SIGN_CONTRACT,
                                                  .FILE_LOGO = p.FILE_LOGO,
                                                  .FILE_HEADER = p.FILE_HEADER,
                                                  .FILE_FOOTER = p.FILE_FOOTER
                                                  }).SingleOrDefault
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetLocation(ByVal sACT As String, ByVal lstOrgID As List(Of Decimal)) As List(Of LocationDTO)
        Dim query As ObjectQuery(Of LocationDTO)
        Try
            If sACT = "" Then
                query = (From p In Context.HU_LOCATION.Where(Function(x) lstOrgID.Contains(x.ORG_ID))
                          Where p.LOCATION_VN_NAME IsNot Nothing
                         Select New LocationDTO With {.ID = p.ID,
                                                      .CODE = p.CODE,
                                                      .ORG_ID = p.ORG_ID,
                                                      .ADDRESS = p.ADDRESS,
                                                      .CONTRACT_PLACE = p.CONTRACT_PLACE,
                                                      .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                      .LOCATION_SHORT_NAME = p.LOCATION_SHORT_NAME,
                                                      .WORK_ADDRESS = p.WORK_ADDRESS,
                                                      .PHONE = p.PHONE,
                                                      .FAX = p.FAX,
                                                      .WEBSITE = p.WEBSITE,
                                                      .ACCOUNT_NUMBER = p.ACCOUNT_NUMBER,
                                                      .BANK_ID = p.BANK_ID,
                                                      .TAX_CODE = p.TAX_CODE,
                                                      .TAX_DATE = p.TAX_DATE,
                                                      .TAX_PLACE = p.TAX_PLACE,
                                                      .EMP_LAW_ID = p.EMP_LAW_ID,
                                                      .EMP_SIGNCONTRACT_ID = p.EMP_SIGNCONTRACT_ID,
                                                      .BUSINESS_NAME = p.BUSINESS_NAME,
                                                      .BUSINESS_NUMBER = p.BUSINESS_NUMBER,
                                                      .NOTE = p.NOTE,
                                                      .LOCATION_EN_NAME = p.LOCATION_EN_NAME,
                                                      .LOCATION_VN_NAME = p.LOCATION_VN_NAME,
                                                      .BUSINESS_REG_DATE = p.BUSINESS_REG_DATE,
                                                      .BANK_BRANCH_ID = p.BANK_BRANCH_ID,
                                                      .PROVINCE_ID = p.PROVINCE_ID,
                                                      .DISTRICT_ID = p.DISTRICT_ID,
                                                      .WARD_ID = p.WARD_ID,
                                                      .IS_SIGN_CONTRACT = p.IS_SIGN_CONTRACT,
                                                      .FILE_LOGO = p.FILE_LOGO,
                                                      .FILE_HEADER = p.FILE_HEADER,
                                                      .FILE_FOOTER = p.FILE_FOOTER})
            Else
                query = (From p In Context.HU_LOCATION.Where(Function(x) lstOrgID.Contains(x.ORG_ID))
                         Where p.ACTFLG = sACT AndAlso p.LOCATION_VN_NAME IsNot Nothing
                         Select New LocationDTO With {.ID = p.ID,
                                                     .CODE = p.CODE,
                                                    .ORG_ID = p.ORG_ID,
                                                      .ADDRESS = p.ADDRESS,
                                                      .CONTRACT_PLACE = p.CONTRACT_PLACE,
                                                      .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                     .LOCATION_SHORT_NAME = p.LOCATION_SHORT_NAME,
                                                   .WORK_ADDRESS = p.WORK_ADDRESS,
                                                     .PHONE = p.PHONE,
                                                     .FAX = p.FAX,
                                                     .WEBSITE = p.WEBSITE,
                                                    .ACCOUNT_NUMBER = p.ACCOUNT_NUMBER,
                                                     .BANK_ID = p.BANK_ID,
                                                     .TAX_CODE = p.TAX_CODE,
                                                     .TAX_DATE = p.TAX_DATE,
                                                     .TAX_PLACE = p.TAX_PLACE,
                                                     .EMP_LAW_ID = p.EMP_LAW_ID,
                                                     .EMP_SIGNCONTRACT_ID = p.EMP_SIGNCONTRACT_ID,
                                                     .BUSINESS_NAME = p.BUSINESS_NAME,
                                                     .BUSINESS_NUMBER = p.BUSINESS_NUMBER,
                                                     .NOTE = p.NOTE,
                                                     .LOCATION_EN_NAME = p.LOCATION_EN_NAME,
                                                     .LOCATION_VN_NAME = p.LOCATION_VN_NAME,
                                                     .BUSINESS_REG_DATE = p.BUSINESS_REG_DATE,
                                                     .BANK_BRANCH_ID = p.BANK_BRANCH_ID,
                                                      .PROVINCE_ID = p.PROVINCE_ID,
                                                      .DISTRICT_ID = p.DISTRICT_ID,
                                                      .WARD_ID = p.WARD_ID,
                                                      .IS_SIGN_CONTRACT = p.IS_SIGN_CONTRACT,
                                                      .FILE_LOGO = p.FILE_LOGO,
                                                      .FILE_HEADER = p.FILE_HEADER,
                                                      .FILE_FOOTER = p.FILE_FOOTER})
            End If

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function InsertLocation(ByVal objLocation As LocationDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLocationData As New HU_LOCATION
        Try
            objLocationData.ID = Utilities.GetNextSequence(Context, Context.HU_LOCATION.EntitySet.Name)
            ' objLocationData.CODE = objLocation.CODE
            objLocationData.ORG_ID = objLocation.ORG_ID
            objLocationData.ADDRESS = objLocation.ADDRESS
            objLocationData.CONTRACT_PLACE = objLocation.CONTRACT_PLACE
            objLocationData.LOCATION_SHORT_NAME = objLocation.LOCATION_SHORT_NAME
            objLocationData.WORK_ADDRESS = objLocation.WORK_ADDRESS
            objLocationData.PHONE = objLocation.PHONE
            objLocationData.FAX = objLocation.FAX
            objLocationData.WEBSITE = objLocation.WEBSITE
            objLocationData.ACCOUNT_NUMBER = objLocation.ACCOUNT_NUMBER
            objLocationData.BANK_ID = objLocation.BANK_ID
            objLocationData.TAX_CODE = objLocation.TAX_CODE
            objLocationData.ACTFLG = objLocation.ACTFLG
            objLocationData.TAX_DATE = objLocation.TAX_DATE
            objLocationData.TAX_PLACE = objLocation.TAX_PLACE
            objLocationData.EMP_LAW_ID = objLocation.EMP_LAW_ID
            objLocationData.EMP_SIGNCONTRACT_ID = objLocation.EMP_SIGNCONTRACT_ID
            objLocationData.BUSINESS_NAME = objLocation.BUSINESS_NAME
            objLocationData.BUSINESS_NUMBER = objLocation.BUSINESS_NUMBER
            objLocationData.NOTE = objLocation.NOTE
            objLocationData.LOCATION_EN_NAME = objLocation.LOCATION_EN_NAME
            objLocationData.LOCATION_VN_NAME = objLocation.LOCATION_VN_NAME
            objLocationData.BUSINESS_REG_DATE = objLocation.BUSINESS_REG_DATE
            objLocationData.BANK_BRANCH_ID = objLocation.BANK_BRANCH_ID
            objLocationData.PROVINCE_ID = objLocation.PROVINCE_ID
            objLocationData.DISTRICT_ID = objLocation.DISTRICT_ID
            objLocationData.WARD_ID = objLocation.WARD_ID
            objLocationData.IS_SIGN_CONTRACT = objLocation.IS_SIGN_CONTRACT
            objLocationData.FILE_LOGO = objLocation.FILE_LOGO
            objLocationData.FILE_HEADER = objLocation.FILE_HEADER
            objLocationData.FILE_FOOTER = objLocation.FILE_FOOTER
            Context.HU_LOCATION.AddObject(objLocationData)
            Context.SaveChanges(log)
            gID = objLocationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyLocation(ByVal objLocation As LocationDTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLocationData As New HU_LOCATION With {.ID = objLocation.ID}

        Try
            Context.HU_LOCATION.Attach(objLocationData)
            ' objLocationData.CODE = objLocation.CODE
            objLocationData.ORG_ID = objLocation.ORG_ID
            objLocationData.ADDRESS = objLocation.ADDRESS
            objLocationData.CONTRACT_PLACE = objLocation.CONTRACT_PLACE
            objLocationData.LOCATION_SHORT_NAME = objLocation.LOCATION_SHORT_NAME
            objLocationData.WORK_ADDRESS = objLocation.WORK_ADDRESS
            objLocationData.PHONE = objLocation.PHONE
            objLocationData.FAX = objLocation.FAX
            objLocationData.WEBSITE = objLocation.WEBSITE
            objLocationData.ACCOUNT_NUMBER = objLocation.ACCOUNT_NUMBER
            objLocationData.BANK_ID = objLocation.BANK_ID
            objLocationData.TAX_CODE = objLocation.TAX_CODE
            objLocationData.TAX_DATE = objLocation.TAX_DATE
            objLocationData.TAX_PLACE = objLocation.TAX_PLACE
            objLocationData.EMP_LAW_ID = objLocation.EMP_LAW_ID
            objLocationData.EMP_SIGNCONTRACT_ID = objLocation.EMP_SIGNCONTRACT_ID
            objLocationData.BUSINESS_NAME = objLocation.BUSINESS_NAME
            objLocationData.BUSINESS_NUMBER = objLocation.BUSINESS_NUMBER
            objLocationData.NOTE = objLocation.NOTE
            objLocationData.LOCATION_EN_NAME = objLocation.LOCATION_EN_NAME
            objLocationData.LOCATION_VN_NAME = objLocation.LOCATION_VN_NAME
            objLocationData.BUSINESS_REG_DATE = objLocation.BUSINESS_REG_DATE
            objLocationData.BANK_BRANCH_ID = objLocation.BANK_BRANCH_ID
            objLocationData.PROVINCE_ID = objLocation.PROVINCE_ID
            objLocationData.DISTRICT_ID = objLocation.DISTRICT_ID
            objLocationData.WARD_ID = objLocation.WARD_ID
            objLocationData.IS_SIGN_CONTRACT = objLocation.IS_SIGN_CONTRACT
            objLocationData.FILE_LOGO = objLocation.FILE_LOGO
            objLocationData.FILE_HEADER = objLocation.FILE_HEADER
            objLocationData.FILE_FOOTER = objLocation.FILE_FOOTER
            Context.SaveChanges(log)
            gID = objLocationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveLocation(ByVal lstLocation As List(Of LocationDTO), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstLocationData As List(Of HU_LOCATION)
        Dim lstIDLocation As List(Of Decimal) = (From p In lstLocation.ToList Select p.ID).ToList
        lstLocationData = (From p In Context.HU_LOCATION Where lstIDLocation.Contains(p.ID)).ToList
        For index = 0 To lstLocationData.Count - 1
            lstLocationData(index).ACTFLG = sActive
            lstLocationData(index).MODIFIED_DATE = DateTime.Now
            lstLocationData(index).MODIFIED_BY = log.Username
            lstLocationData(index).MODIFIED_LOG = log.ComputerName
        Next
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function ActiveLocationID(ByVal lstLocation As LocationDTO, ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim location As HU_LOCATION
        Try
            location = (From p In Context.HU_LOCATION Where p.ID = lstLocation.ID).SingleOrDefault()
            If location IsNot Nothing Then
                location.ACTFLG = sActive
                location.MODIFIED_DATE = DateTime.Now
                location.MODIFIED_BY = log.Username
                location.MODIFIED_LOG = log.ComputerName
                Context.SaveChanges(log)
                Return True
            End If
            Return False
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteLocationID(ByVal lstlocation As Decimal,
                                  ByVal log As UserLog) As Boolean
        Dim location As HU_LOCATION
        Try
            location = (From p In Context.HU_LOCATION Where p.ID = lstlocation).SingleOrDefault()
            If location IsNot Nothing Then
                Context.HU_LOCATION.DeleteObject(location)
                Context.SaveChanges(log)
                Return True
            End If
            Return False
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region

#Region " danh mục người ký"
    'load dữ liệu
    Public Function GET_HU_SIGNER(ByVal _filter As SignerDTO) As DataTable
        Try
            Dim userNameID = _filter.USER_ID
            Dim check As String = "Dùng chung"
            Dim active As String = "Áp dụng"
            Dim inactive As String = "Không áp dụng"
            Dim lstOrgID = New List(Of Decimal?)
            lstOrgID = (From p In Context.SE_USER_ORG_ACCESS
                From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                Where p.USER_ID = userNameID
                Select p.ORG_ID).ToList
            Dim query = From p In Context.HU_SIGNER
                        Group Join g In Context.HU_ORGANIZATION On p.ORG_ID Equals g.ID Into g_olg = Group
                        From olg In g_olg.DefaultIfEmpty
            Dim lst = query.Select(Function(f) New SignerDTO With {
                                    .ID = f.p.ID,
                                    .SIGNER_CODE = f.p.SIGNER_CODE,
                                    .NAME = f.p.NAME,
                                    .TITLE_NAME = f.p.TITLE_NAME,
                                    .ORG_NAME = If(f.p.ORG_ID = -1, check, f.olg.NAME_VN),
                                    .REMARK = f.p.REMARK,
                                    .ORG_ID = f.p.ORG_ID,
                                    .ACTFLG = If(f.p.ACTFLG <> 0, active, inactive)
                                    }).AsQueryable
            If lstOrgID.Count > 0 And userNameID <> 1 Then
                lst = lst.Where(Function(f) (lstOrgID.Contains(f.ORG_ID) Or f.ORG_ID = -1))
            End If
            Dim view As DataView = New DataView(lst.ToList.ToTable())
            Dim dt As DataTable = view.ToTable(True, "ID", "SIGNER_CODE", "NAME", "TITLE_NAME", "ORG_NAME", "ACTFLG", "REMARK", "ORG_ID")
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'thêm người ký
    Public Function INSERT_HU_SIGNER(ByVal PA As SignerDTO) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData = cls.ExecuteStore("PKG_HU_IPROFILE_LIST.INSERT_HU_SIGNER", New With {
                                                                                                 PA.NAME,
                                                                                                 PA.SIGNER_CODE,
                                                                                                 PA.TITLE_NAME,
                                                                                                PA.REMARK,
                                                                                                 PA.ORG_ID,
                                                                                                  PA.CREATED_BY,
                                                                                                PA.CREATED_LOG,
                                                                                                 .P_OUT = cls.OUT_NUMBER})
                Return True
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ' update dữ liệu 

    Public Function UPDATE_HU_SIGNER(ByVal PA As SignerDTO) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData = cls.ExecuteStore("PKG_HU_IPROFILE_LIST.UPDATE_HU_SIGNER", New With {
                                                                                            PA.ID,
                                                                                            PA.NAME,
                                                                                            PA.SIGNER_CODE,
                                                                                            PA.TITLE_NAME,
                                                                                            PA.REMARK,
                                                                                            PA.ORG_ID,
                                                                                            PA.CREATED_BY,
                                                                                            PA.CREATED_LOG,
                                                                                            .P_OUT = cls.OUT_NUMBER})
                Return True
            End Using
        Catch ex As Exception

        End Try
    End Function
    'CHECK DL DA TON TAI HAY CHUA
    Public Function CHECK_EXIT(ByVal P_ID As String, ByVal idemp As Decimal) As Boolean
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_HU_IPROFILE_LIST.CHECK_EXIT", New With {
                                                                                    P_ID,
                                                                                    idemp,
                                                                                  .P_OUT = cls.OUT_CURSOR})
                If Decimal.Parse(dtData(0)("CHECK1").ToString) > 0 Then
                    Return True
                Else
                    Return False
                End If


            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'XOA
    Public Function DeleteSigner(ByVal lstID As String)
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData = cls.ExecuteStore("PKG_HU_IPROFILE_LIST.DELETE_SIGNER", New With {
                                                                                    lstID
                                                                                 })
                Return True
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'AP DUNG HOAC NGUNG AP DUNG
    Public Function DeactiveAndActiveSigner(ByVal lstID As String, ByVal sActive As Decimal)
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData = cls.ExecuteStore("PKG_HU_IPROFILE_LIST.DeactiveAndActiveSigner", New With {
                                                                                    lstID,
                                                                                    sActive
                                                                                 })
                Return True
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return True
    End Function
#End Region
End Class

Imports System.IO
Imports Framework.Data
Imports System.Data.Objects
Imports Framework.Data.System.Linq.Dynamic
Imports Framework.Data.DataAccess
Imports Oracle.DataAccess.Client
Imports System.Reflection

Partial Class ProfileRepository

#Region "Family"

    Public Function InsertEmployeeFamily(ByVal objFamily As FamilyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objFamilyData As New HU_FAMILY
            objFamilyData.ID = Utilities.GetNextSequence(Context, Context.HU_FAMILY.EntitySet.Name)
            objFamilyData.EMPLOYEE_ID = objFamily.EMPLOYEE_ID
            objFamilyData.FULLNAME = objFamily.FULLNAME
            objFamilyData.RELATION_ID = objFamily.RELATION_ID
            objFamilyData.PROVINCE_ID = objFamily.PROVINCE_ID
            objFamilyData.CAREER = objFamily.CAREER
            objFamilyData.TITLE_NAME = objFamily.TITLE_NAME
            objFamilyData.BIRTH_DATE = objFamily.BIRTH_DATE
            objFamilyData.DEDUCT_REG = objFamily.DEDUCT_REG
            objFamilyData.ID_NO = objFamily.ID_NO
            objFamilyData.TAXTATION = objFamily.TAXTATION
            objFamilyData.IS_DEDUCT = objFamily.IS_DEDUCT
            objFamilyData.DEDUCT_FROM = objFamily.DEDUCT_FROM
            objFamilyData.DEDUCT_TO = objFamily.DEDUCT_TO
            objFamilyData.REMARK = objFamily.REMARK
            objFamilyData.ADDRESS = objFamily.ADDRESS
            Context.HU_FAMILY.AddObject(objFamilyData)
            Context.SaveChanges(log)
            gID = objFamilyData.ID
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyEmployeeFamily(ByVal objFamily As FamilyDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objFamilyData As New HU_FAMILY With {.ID = objFamily.ID}
        Try
            objFamilyData = (From p In Context.HU_FAMILY Where p.ID = objFamily.ID).FirstOrDefault
            objFamilyData.EMPLOYEE_ID = objFamily.EMPLOYEE_ID
            objFamilyData.FULLNAME = objFamily.FULLNAME
            objFamilyData.RELATION_ID = objFamily.RELATION_ID
            objFamilyData.PROVINCE_ID = objFamily.PROVINCE_ID
            objFamilyData.CAREER = objFamily.CAREER
            objFamilyData.TITLE_NAME = objFamily.TITLE_NAME
            objFamilyData.BIRTH_DATE = objFamily.BIRTH_DATE
            objFamilyData.DEDUCT_REG = objFamily.DEDUCT_REG
            objFamilyData.ID_NO = objFamily.ID_NO
            objFamilyData.TAXTATION = objFamily.TAXTATION
            objFamilyData.IS_DEDUCT = objFamily.IS_DEDUCT
            objFamilyData.DEDUCT_FROM = objFamily.DEDUCT_FROM
            objFamilyData.DEDUCT_TO = objFamily.DEDUCT_TO
            objFamilyData.REMARK = objFamily.REMARK
            objFamilyData.ADDRESS = objFamily.ADDRESS
            Context.SaveChanges(log)
            gID = objFamilyData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetEmployeeFamily(ByVal _filter As FamilyDTO) As List(Of FamilyDTO)
        Dim query As ObjectQuery(Of FamilyDTO)
        Try
            query = (From p In Context.HU_FAMILY
                     Group Join m In Context.HU_RELATIONSHIP_LIST On p.RELATION_ID Equals m.ID Into gGroup = Group
                     From p_g In gGroup.DefaultIfEmpty
                     Group Join n In Context.HU_PROVINCE On p.PROVINCE_ID Equals n.ID Into nGroup = Group
                     From n_g In nGroup.DefaultIfEmpty
                   Select New FamilyDTO With {
                    .ID = p.ID,
                    .ADDRESS = p.ADDRESS,
                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                    .FULLNAME = p.FULLNAME,
                    .RELATION_ID = p.RELATION_ID,
                    .RELATION_NAME = p_g.NAME,
                    .PROVINCE_ID = p.PROVINCE_ID,
                    .PROVINCE_NAME = n_g.NAME_VN,
                    .CAREER = p.CAREER,
                    .TITLE_NAME = p.TITLE_NAME,
                    .BIRTH_DATE = p.BIRTH_DATE,
                    .TAXTATION = p.TAXTATION,
                    .DEDUCT_REG = p.DEDUCT_REG,
                    .ID_NO = p.ID_NO,
                    .IS_DEDUCT = p.IS_DEDUCT,
                    .DEDUCT_FROM = p.DEDUCT_FROM,
                    .DEDUCT_TO = p.DEDUCT_TO,
                    .REMARK = p.REMARK})
            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.ID <> 0 Then
                query = query.Where(Function(p) p.ID = _filter.ID)
            End If
            If _filter.RELATION_NAME <> "" Then
                query = query.Where(Function(p) p.RELATION_NAME.ToLower().Contains(_filter.RELATION_NAME.ToLower))
            End If
            If _filter.PROVINCE_NAME <> "" Then
                query = query.Where(Function(p) p.PROVINCE_NAME.ToLower().Contains(_filter.PROVINCE_NAME.ToLower))
            End If
            If _filter.FULLNAME <> "" Then
                query = query.Where(Function(p) p.FULLNAME.ToLower().Contains(_filter.FULLNAME.ToLower))
            End If
            If _filter.BIRTH_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.BIRTH_DATE = _filter.BIRTH_DATE)
            End If
            If _filter.ID_NO <> "" Then
                query = query.Where(Function(p) p.ID_NO.ToUpper().IndexOf(_filter.ID_NO.ToUpper) >= 0)
            End If
            If _filter.DEDUCT_REG IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_REG = _filter.DEDUCT_REG)
            End If
            If _filter.DEDUCT_FROM IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_FROM >= _filter.DEDUCT_FROM)
            End If
            If _filter.DEDUCT_TO IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_TO <= _filter.DEDUCT_TO)
            End If
            If _filter.ADDRESS <> "" Then
                query = query.Where(Function(p) p.ADDRESS.ToLower().Contains(_filter.ADDRESS.ToLower))
            End If
            If _filter.REMARK <> "" Then
                query = query.Where(Function(p) p.REMARK.ToLower().Contains(_filter.REMARK.ToLower))
            End If

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteEmployeeFamily(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_FAMILY)
        Try
            lst = (From p In Context.HU_FAMILY Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.HU_FAMILY.DeleteObject(lst(i))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Hàm check trùng CMND của thân nhân.
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateFamily(ByVal _validate As FamilyDTO)
        Try
            If _validate.ID_NO <> "" Then
                Dim query = (From p In Context.HU_FAMILY
                         Where p.ID_NO.ToUpper = _validate.ID_NO.ToUpper And _
                         p.ID <> _validate.ID)

                Return query.Count = 0
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
            Throw ex
        End Try
    End Function

#End Region

#Region "FamilyEdit"

    Public Function InsertEmployeeFamilyEdit(ByVal objFamilyEdit As FamilyEditDTO,
                                             ByVal log As UserLog,
                                             ByRef gID As Decimal) As Boolean
        Try

            Dim objFamilyEditData As New HU_FAMILY_EDIT
            objFamilyEditData.ID = Utilities.GetNextSequence(Context, Context.HU_FAMILY_EDIT.EntitySet.Name)
            objFamilyEditData.EMPLOYEE_ID = objFamilyEdit.EMPLOYEE_ID
            objFamilyEditData.FULLNAME = objFamilyEdit.FULLNAME
            objFamilyEditData.RELATION_ID = objFamilyEdit.RELATION_ID
            objFamilyEditData.BIRTH_DATE = objFamilyEdit.BIRTH_DATE
            objFamilyEditData.TAXTATION = objFamilyEdit.TAXTATION
            objFamilyEditData.DEDUCT_REG = objFamilyEdit.DEDUCT_REG
            objFamilyEditData.ID_NO = objFamilyEdit.ID_NO
            objFamilyEditData.IS_DEDUCT = objFamilyEdit.IS_DEDUCT
            objFamilyEditData.DEDUCT_FROM = objFamilyEdit.DEDUCT_FROM
            objFamilyEditData.DEDUCT_TO = objFamilyEdit.DEDUCT_TO
            objFamilyEditData.REMARK = objFamilyEdit.REMARK
            objFamilyEditData.ADDRESS = objFamilyEdit.ADDRESS
            objFamilyEditData.STATUS = 0
            objFamilyEditData.REASON_UNAPROVE = objFamilyEdit.REASON_UNAPROVE
            objFamilyEditData.FK_PKEY = objFamilyEdit.FK_PKEY
            objFamilyEditData.CAREER = objFamilyEdit.CAREER
            objFamilyEditData.TITLE_NAME = objFamilyEdit.TITLE_NAME
            objFamilyEditData.PROVINCE_ID = objFamilyEdit.PROVINCE_ID
            objFamilyEditData.CERTIFICATE_CODE = objFamilyEdit.CERTIFICATE_CODE
            objFamilyEditData.CERTIFICATE_NUM = objFamilyEdit.CERTIFICATE_NUM
            objFamilyEditData.ADDRESS_TT = objFamilyEdit.ADDRESS_TT
            objFamilyEditData.AD_PROVINCE_ID = objFamilyEdit.AD_PROVINCE_ID
            objFamilyEditData.AD_DISTRICT_ID = objFamilyEdit.AD_DISTRICT_ID
            objFamilyEditData.AD_WARD_ID = objFamilyEdit.AD_WARD_ID
            objFamilyEditData.AD_VILLAGE = objFamilyEdit.AD_VILLAGE
            objFamilyEditData.TT_PROVINCE_ID = objFamilyEdit.TT_PROVINCE_ID
            objFamilyEditData.TT_DISTRICT_ID = objFamilyEdit.TT_DISTRICT_ID
            objFamilyEditData.TT_WARD_ID = objFamilyEdit.TT_WARD_ID
            objFamilyEditData.IS_OWNER = objFamilyEdit.IS_OWNER
            objFamilyEditData.IS_PASS = objFamilyEdit.IS_PASS
            Context.HU_FAMILY_EDIT.AddObject(objFamilyEditData)
            Context.SaveChanges(log)
            gID = objFamilyEditData.ID
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyEmployeeFamilyEdit(ByVal objFamilyEdit As FamilyEditDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objFamilyEditData As New HU_FAMILY_EDIT With {.ID = objFamilyEdit.ID}
        Try
            objFamilyEditData = (From p In Context.HU_FAMILY_EDIT Where p.ID = objFamilyEdit.ID).FirstOrDefault
            objFamilyEditData.EMPLOYEE_ID = objFamilyEdit.EMPLOYEE_ID
            objFamilyEditData.FULLNAME = objFamilyEdit.FULLNAME
            objFamilyEditData.RELATION_ID = objFamilyEdit.RELATION_ID
            objFamilyEditData.BIRTH_DATE = objFamilyEdit.BIRTH_DATE
            objFamilyEditData.DEDUCT_REG = objFamilyEdit.DEDUCT_REG
            objFamilyEditData.ID_NO = objFamilyEdit.ID_NO
            objFamilyEditData.TAXTATION = objFamilyEdit.TAXTATION
            objFamilyEditData.IS_DEDUCT = objFamilyEdit.IS_DEDUCT
            objFamilyEditData.DEDUCT_FROM = objFamilyEdit.DEDUCT_FROM
            objFamilyEditData.DEDUCT_TO = objFamilyEdit.DEDUCT_TO
            objFamilyEditData.REMARK = objFamilyEdit.REMARK
            objFamilyEditData.ADDRESS = objFamilyEdit.ADDRESS
            objFamilyEditData.REASON_UNAPROVE = objFamilyEdit.REASON_UNAPROVE
            objFamilyEditData.FK_PKEY = objFamilyEdit.FK_PKEY
            objFamilyEditData.CAREER = objFamilyEdit.CAREER
            objFamilyEditData.TITLE_NAME = objFamilyEdit.TITLE_NAME
            objFamilyEditData.PROVINCE_ID = objFamilyEdit.PROVINCE_ID
            objFamilyEditData.CERTIFICATE_CODE = objFamilyEdit.CERTIFICATE_CODE
            objFamilyEditData.CERTIFICATE_NUM = objFamilyEdit.CERTIFICATE_NUM
            objFamilyEditData.ADDRESS_TT = objFamilyEdit.ADDRESS_TT
            objFamilyEditData.AD_PROVINCE_ID = objFamilyEdit.AD_PROVINCE_ID
            objFamilyEditData.AD_DISTRICT_ID = objFamilyEdit.AD_DISTRICT_ID
            objFamilyEditData.AD_WARD_ID = objFamilyEdit.AD_WARD_ID
            objFamilyEditData.AD_VILLAGE = objFamilyEdit.AD_VILLAGE
            objFamilyEditData.TT_PROVINCE_ID = objFamilyEdit.TT_PROVINCE_ID
            objFamilyEditData.TT_DISTRICT_ID = objFamilyEdit.TT_DISTRICT_ID
            objFamilyEditData.TT_WARD_ID = objFamilyEdit.TT_WARD_ID
            objFamilyEditData.IS_OWNER = objFamilyEdit.IS_OWNER
            objFamilyEditData.IS_PASS = objFamilyEdit.IS_PASS
            Context.SaveChanges(log)
            gID = objFamilyEditData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetEmployeeFamilyEdit(ByVal _filter As FamilyEditDTO) As List(Of FamilyEditDTO)
        Dim query As ObjectQuery(Of FamilyEditDTO)
        Try
            query = (From p In Context.HU_FAMILY_EDIT
                     Group Join m In Context.HU_RELATIONSHIP_LIST On p.RELATION_ID Equals m.ID Into gGroup = Group
                     From p_g In gGroup.DefaultIfEmpty
                     Group Join n In Context.HU_PROVINCE On p.PROVINCE_ID Equals n.ID Into nGroup = Group
                     From n_g In nGroup.DefaultIfEmpty
                     Select New FamilyEditDTO With {
                         .ID = p.ID,
                         .ADDRESS = p.ADDRESS,
                         .EMPLOYEE_ID = p.EMPLOYEE_ID,
                         .FULLNAME = p.FULLNAME,
                         .RELATION_ID = p.RELATION_ID,
                         .RELATION_NAME = p_g.NAME,
                         .PROVINCE_ID = p.PROVINCE_ID,
                         .PROVINCE_NAME = n_g.NAME_VN,
                         .BIRTH_DATE = p.BIRTH_DATE,
                         .TAXTATION = p.TAXTATION,
                         .DEDUCT_REG = p.DEDUCT_REG,
                         .ID_NO = p.ID_NO,
                         .IS_DEDUCT = p.IS_DEDUCT,
                         .DEDUCT_FROM = p.DEDUCT_FROM,
                         .DEDUCT_TO = p.DEDUCT_TO,
                         .REMARK = p.REMARK,
                         .CERTIFICATE_NUM = p.CERTIFICATE_NUM,
                         .CERTIFICATE_CODE = p.CERTIFICATE_CODE,
                         .ADDRESS_TT = p.ADDRESS_TT,
                         .AD_PROVINCE_ID = p.AD_PROVINCE_ID,
                         .AD_DISTRICT_ID = p.AD_DISTRICT_ID,
                         .AD_WARD_ID = p.AD_WARD_ID,
                         .AD_VILLAGE = p.AD_VILLAGE,
                         .TT_PROVINCE_ID = p.TT_PROVINCE_ID,
                         .TT_DISTRICT_ID = p.TT_DISTRICT_ID,
                         .TT_WARD_ID = p.TT_WARD_ID,
                         .IS_OWNER = p.IS_OWNER,
                         .IS_PASS = p.IS_PASS,
                         .REASON_UNAPROVE = p.REASON_UNAPROVE,
                         .FK_PKEY = p.FK_PKEY,
                         .TITLE_NAME = p.TITLE_NAME,
                         .CAREER = p.CAREER,
                         .STATUS = p.STATUS,
                         .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                           If(p.STATUS = 1, "Chờ phê duyệt",
                                              If(p.STATUS = 2, "Phê duyệt",
                                                 If(p.STATUS = 3, "Không phê duyệt", ""))))})

            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.STATUS <> "" Then
                query = query.Where(Function(p) p.STATUS = _filter.STATUS)
            End If
            If _filter.ID <> 0 Then
                query = query.Where(Function(p) p.ID = _filter.ID)
            End If

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetApproveFamilyEdit(ByVal _filter As FamilyEditDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of FamilyEditDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = (From p In Context.HU_FAMILY_EDIT
                         From p_g In Context.HU_RELATIONSHIP_LIST.Where(Function(f) p.RELATION_ID = f.ID).DefaultIfEmpty
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        Where p.STATUS = 1
                        Select New FamilyEditDTO With {
                            .ID = p.ID,
                            .ADDRESS = p.ADDRESS,
                            .EMPLOYEE_ID = p.EMPLOYEE_ID,
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .FULLNAME = p.FULLNAME,
                            .RELATION_ID = p.RELATION_ID,
                            .RELATION_NAME = p_g.NAME,
                            .BIRTH_DATE = p.BIRTH_DATE,
                            .DEDUCT_REG = p.DEDUCT_REG,
                            .ID_NO = p.ID_NO,
                            .IS_DEDUCT = p.IS_DEDUCT,
                            .DEDUCT_FROM = p.DEDUCT_FROM,
                            .DEDUCT_TO = p.DEDUCT_TO,
                            .REMARK = p.REMARK,
                            .TITLE_ID = e.TITLE_ID,
                            .TITLE_NAME = p.TITLE_NAME,
                            .CAREER = p.CAREER,
                            .FK_PKEY = p.FK_PKEY,
                            .STATUS = p.STATUS,
                            .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                              If(p.STATUS = 1, "Chờ phê duyệt",
                                                 If(p.STATUS = 2, "Phê duyệt",
                                                    If(p.STATUS = 3, "Không phê duyệt", ""))))})


            If _filter.DEDUCT_REG IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_REG = _filter.DEDUCT_REG)
            End If

            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If

            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                query = query.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.FULLNAME IsNot Nothing Then
                query = query.Where(Function(p) p.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
            End If

            If _filter.ADDRESS IsNot Nothing Then
                query = query.Where(Function(p) p.ADDRESS.ToUpper.Contains(_filter.ADDRESS.ToUpper))
            End If

            If _filter.RELATION_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.RELATION_NAME.ToUpper.Contains(_filter.RELATION_NAME.ToUpper))
            End If

            If _filter.STATUS_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If

            If _filter.ID_NO IsNot Nothing Then
                query = query.Where(Function(p) p.ID_NO.ToUpper.Contains(_filter.ID_NO.ToUpper))
            End If

            If _filter.REMARK IsNot Nothing Then
                query = query.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If

            If _filter.DEDUCT_TO IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_TO = _filter.DEDUCT_TO)
            End If

            If _filter.DEDUCT_FROM IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_FROM = _filter.DEDUCT_FROM)
            End If

            If _filter.IS_DEDUCT IsNot Nothing Then
                query = query.Where(Function(p) p.IS_DEDUCT = _filter.IS_DEDUCT)
            End If
            Dim working = query

            working = working.OrderBy(Sorts)
            Total = working.Count
            working = working.Skip(PageIndex * PageSize).Take(PageSize)

            Return working.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteEmployeeFamilyEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_FAMILY_EDIT)
        Try
            lst = (From p In Context.HU_FAMILY_EDIT Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.HU_FAMILY_EDIT.DeleteObject(lst(i))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function CheckExistFamilyEdit(ByVal pk_key As Decimal) As FamilyEditDTO
        Try
            Dim query = (From p In Context.HU_FAMILY_EDIT
                         Where p.STATUS <> 2 And p.FK_PKEY = pk_key
                         Select New FamilyEditDTO With {
                             .ID = p.ID,
                             .STATUS = p.STATUS}).FirstOrDefault

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function SendEmployeeFamilyEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean

        Try
            Dim lstObj = (From p In Context.HU_FAMILY_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lstObj
                item.SEND_DATE = Date.Now
                item.STATUS = 1
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function UpdateStatusEmployeeFamilyEdit(ByVal lstID As List(Of Decimal),
                                                   status As String,
                                                   ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_FAMILY_EDIT)
        Dim sStatus() As String = status.Split(":")

        Try
            lst = (From p In Context.HU_FAMILY_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lst
                item.STATUS = sStatus(0)
                item.REASON_UNAPROVE = sStatus(1)

                If sStatus(0) = 2 Then
                    If item.FK_PKEY IsNot Nothing Then
                        Dim objFamilyData As HU_FAMILY
                        objFamilyData = (From p In Context.HU_FAMILY Where p.ID = item.FK_PKEY).FirstOrDefault
                        objFamilyData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objFamilyData.FULLNAME = item.FULLNAME
                        objFamilyData.RELATION_ID = item.RELATION_ID
                        objFamilyData.BIRTH_DATE = item.BIRTH_DATE
                        objFamilyData.TAXTATION = item.TAXTATION
                        objFamilyData.DEDUCT_REG = item.DEDUCT_REG
                        objFamilyData.ID_NO = item.ID_NO
                        objFamilyData.IS_DEDUCT = item.IS_DEDUCT
                        objFamilyData.DEDUCT_FROM = item.DEDUCT_FROM
                        objFamilyData.DEDUCT_TO = item.DEDUCT_TO
                        objFamilyData.REMARK = item.REMARK
                        objFamilyData.ADDRESS = item.ADDRESS

                        ' 20190520 CanhNX: Edit cho lưu Nguyên quán, Nghề nghiệp, Chức danh
                        objFamilyData.CAREER = item.CAREER
                        objFamilyData.TITLE_NAME = item.TITLE_NAME
                        objFamilyData.PROVINCE_ID = item.PROVINCE_ID

                    Else
                        Dim objFamilyData As New HU_FAMILY
                        objFamilyData.ID = Utilities.GetNextSequence(Context, Context.HU_FAMILY.EntitySet.Name)
                        objFamilyData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objFamilyData.FULLNAME = item.FULLNAME
                        objFamilyData.RELATION_ID = item.RELATION_ID
                        objFamilyData.BIRTH_DATE = item.BIRTH_DATE
                        objFamilyData.DEDUCT_REG = item.DEDUCT_REG
                        objFamilyData.ID_NO = item.ID_NO
                        objFamilyData.IS_DEDUCT = item.IS_DEDUCT
                        objFamilyData.DEDUCT_FROM = item.DEDUCT_FROM
                        objFamilyData.TAXTATION = item.TAXTATION
                        objFamilyData.DEDUCT_TO = item.DEDUCT_TO
                        objFamilyData.REMARK = item.REMARK
                        objFamilyData.ADDRESS = item.ADDRESS

                        ' 20190520 CanhNX: Edit cho lưu Nguyên quán, Nghề nghiệp, Chức danh
                        objFamilyData.CAREER = item.CAREER
                        objFamilyData.TITLE_NAME = item.TITLE_NAME
                        objFamilyData.PROVINCE_ID = item.PROVINCE_ID

                        Context.HU_FAMILY.AddObject(objFamilyData)
                    End If
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

End Class

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

Partial Public Class CommonRepository

#Region "Process Setup"
    Public Function GetSignList() As List(Of ATTimeManualDTO)
        Try
            Dim listSign = (From t In Context.AT_TIME_MANUAL
                            Where t.ACTFLG = "A"
                            Select New ATTimeManualDTO With {
                                .ID = t.ID, .NAME = t.NAME
                            }).ToList()
            Return listSign
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTitleList() As List(Of OtherListDTO)
        Try
            Dim listTitle = (From t In Context.OT_OTHER_LIST
                             Where t.TYPE_ID = 2000 And t.ACTFLG = "A"
                             Select New OtherListDTO With {
                                .CODE = t.CODE, .NAME_VN = t.NAME_VN
                             }).ToList()
            Return listTitle
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetApproveProcess() As List(Of ApproveProcessDTO)

        Try
            Dim listProcess = (From pr In Context.SE_APP_PROCESS
                               Select New ApproveProcessDTO With {
                                                                .ID = pr.ID,
                                                                .NAME = pr.NAME,
                                                                .ACTFLG = pr.ACTFLG,
                                                                .PROCESS_CODE = pr.PROCESS_CODE,
                                                                .NUMREQUEST = pr.NUMREQUEST,
                                                                .EMAIL = pr.EMAIL
                                                                }).ToList()

            Return listProcess
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetApproveProcess(ByVal processId As Decimal) As ApproveProcessDTO
        Try
            Dim itemGet = (From a In Context.SE_APP_PROCESS
                           Where a.ID = processId
                           Select New ApproveProcessDTO With {
                               .ID = a.ID,
                               .ACTFLG = a.ACTFLG,
                               .NAME = a.NAME,
                               .NUMREQUEST = a.NUMREQUEST,
                               .EMAIL = a.EMAIL
                           }).FirstOrDefault


            Return itemGet

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function InsertApproveProcess(ByVal item As ApproveProcessDTO) As Boolean
        Try
            Dim itemInsert As New SE_APP_PROCESS With {.ID = Utilities.GetNextSequence(Context, Context.SE_APP_PROCESS.EntitySet.Name),
                                                       .NAME = item.NAME,
                                                       .ACTFLG = "A",
                                                       .NUMREQUEST = item.NUMREQUEST,
                                                       .EMAIL = item.EMAIL,
                                                       .PROCESS_CODE = "AA"
                                                      }

            Context.SE_APP_PROCESS.AddObject(itemInsert)

            Context.SaveChanges()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateApproveProcess(ByVal item As ApproveProcessDTO) As Boolean
        Try
            Dim objectEdit As SE_APP_PROCESS = Context.SE_APP_PROCESS.FirstOrDefault(Function(p) p.ID = item.ID)

            If objectEdit IsNot Nothing Then
                With objectEdit
                    .NAME = item.NAME
                    .NUMREQUEST = item.NUMREQUEST
                    .EMAIL = item.EMAIL
                End With
            End If

            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateApproveProcessStatus(ByVal itemUpdates As List(Of Decimal), ByVal status As String) As Boolean
        Try
            Dim objectEdit As List(Of SE_APP_PROCESS) = Context.SE_APP_PROCESS.Where(Function(p) itemUpdates.Contains(p.ID))

            If objectEdit IsNot Nothing Then
                For Each item In objectEdit
                    With item
                        .ACTFLG = status
                    End With
                Next
            End If

            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Approve Setup"

    Public Function GetApproveSetupByEmployee(ByVal employeeId As Decimal,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupDTO)
        Try
            Dim itemReturn = From s In Context.SE_APP_SETUP
                             From proc In Context.SE_APP_PROCESS.Where(Function(f) f.ID = s.PROCESS_ID)
                             From temp In Context.SE_APP_TEMPLATE.Where(Function(f) f.ID = s.TEMPLATE_ID)
                              Where s.EMPLOYEE_ID = employeeId
                              Select New ApproveSetupDTO With {
                                  .ID = s.ID,
                                  .EMPLOYEE_ID = s.EMPLOYEE_ID,
                                  .TEMPLATE_ID = s.TEMPLATE_ID,
                                  .ORG_ID = s.ORG_ID,
                                  .FROM_DATE = s.FROM_DATE,
                                  .TO_DATE = s.TO_DATE,
                                  .PROCESS_NAME = proc.NAME,
                                  .TEMPLATE_NAME = temp.TEMPLATE_NAME,
                                  .NUM_REQUEST = proc.NUMREQUEST,
                                  .REQUEST_EMAIL = proc.EMAIL
                              }

            Return itemReturn.OrderBy(Function(p) Sorts).ThenByDescending(Function(p) p.FROM_DATE).ToList

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetApproveSetupByOrg(ByVal orgId As Decimal,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupDTO)
        Try
            Dim itemReturn = From s In Context.SE_APP_SETUP
                             From proc In Context.SE_APP_PROCESS.Where(Function(f) f.ID = s.PROCESS_ID And f.ACTFLG = "A")
                             From temp In Context.SE_APP_TEMPLATE.Where(Function(f) f.ID = s.TEMPLATE_ID And f.ACTFLG = "A")
                              Where s.ORG_ID = orgId
                              Select New ApproveSetupDTO With {
                                  .ID = s.ID,
                                  .EMPLOYEE_ID = s.EMPLOYEE_ID,
                                  .TEMPLATE_ID = s.TEMPLATE_ID,
                                  .ORG_ID = s.ORG_ID,
                                  .FROM_DATE = s.FROM_DATE,
                                  .TO_DATE = s.TO_DATE,
                                  .PROCESS_NAME = proc.NAME,
                                  .TEMPLATE_NAME = temp.TEMPLATE_NAME,
                                  .NUM_REQUEST = proc.NUMREQUEST,
                                  .REQUEST_EMAIL = proc.EMAIL,
                                  .SIGN_ID = s.SIGN_ID,
                                  .TITLE_ID = s.TITLE_ID
                              }

            Return itemReturn.OrderBy(Function(p) Sorts).ThenByDescending(Function(p) p.FROM_DATE).ToList 'itemReturn.OrderBy(Function(p) p.PROCESS_NAME).ThenByDescending(Function(p) p.FROM_DATE).ToList

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetApproveSetup(ByVal id As Decimal) As ApproveSetupDTO
        Try
            Dim itemReturn = (From s In Context.SE_APP_SETUP
                              Where s.ID = id
                              Select New ApproveSetupDTO With {
                                  .ID = s.ID,
                                  .PROCESS_ID = s.PROCESS_ID,
                                  .TEMPLATE_ID = s.TEMPLATE_ID,
                                  .TITLE_ID = s.TITLE_ID,
                                  .SIGN_ID = s.SIGN_ID,
                                  .FROM_HOUR = s.FROM_HOUR,
                                  .TO_HOUR = s.TO_HOUR,
                                  .FROM_DAY = s.FROM_DAY,
                                  .TO_DAY = s.TO_DAY,
                                  .EMPLOYEE_ID = s.EMPLOYEE_ID,
                                  .ORG_ID = s.ORG_ID,
                                  .FROM_DATE = s.FROM_DATE,
                                  .TO_DATE = s.TO_DATE,
                                  .MAIL_ACCEPTED = s.MAIL_ACCEPTED,
                                  .MAIL_ACCEPTING = s.MAIL_ACCEPTING
                              }).FirstOrDefault

            Return itemReturn

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function InsertApproveSetup(ByVal item As ApproveSetupDTO, ByVal log As UserLog) As Boolean
        Try
            Dim itemInsert As New SE_APP_SETUP With {
                .ID = Utilities.GetNextSequence(Context, Context.SE_APP_SETUP.EntitySet.Name),
                .PROCESS_ID = item.PROCESS_ID,
                .TEMPLATE_ID = item.TEMPLATE_ID,
                .EMPLOYEE_ID = item.EMPLOYEE_ID,
                .ORG_ID = item.ORG_ID,
                .TITLE_ID = item.TITLE_ID,
                .SIGN_ID = item.SIGN_ID,
                .FROM_HOUR = item.FROM_HOUR,
                .TO_HOUR = item.TO_HOUR,
                .FROM_DAY = item.FROM_DAY,
                .TO_DAY = item.TO_DAY,
                .FROM_DATE = item.FROM_DATE,
                .TO_DATE = item.TO_DATE,
                .MAIL_ACCEPTED = item.MAIL_ACCEPTED,
                .MAIL_ACCEPTING = item.MAIL_ACCEPTING,
                .CREATED_BY = log.Username,
                .CREATED_LOG = log.ComputerName,
                .CREATED_DATE = Date.Now,
                .MODIFIED_BY = log.Username,
                .MODIFIED_DATE = Date.Now,
            .MODIFIED_LOG = log.ComputerName
            }

            Context.SE_APP_SETUP.AddObject(itemInsert)

            Context.SaveChanges()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateApproveSetup(ByVal item As ApproveSetupDTO, ByVal log As UserLog) As Boolean
        Try

            Dim itemUpdate = Context.SE_APP_SETUP.FirstOrDefault(Function(p) p.ID = item.ID)

            If itemUpdate IsNot Nothing Then
                With itemUpdate
                    .PROCESS_ID = item.PROCESS_ID
                    .TEMPLATE_ID = item.TEMPLATE_ID
                    .TITLE_ID = item.TITLE_ID
                    .SIGN_ID = item.SIGN_ID
                    .FROM_HOUR = item.FROM_HOUR
                    .TO_HOUR = item.TO_HOUR
                    .FROM_DAY = item.FROM_DAY
                    .TO_DAY = item.TO_DAY
                    .FROM_DATE = item.FROM_DATE
                    .TO_DATE = item.TO_DATE

                    .MAIL_ACCEPTED = item.MAIL_ACCEPTED
                    .MAIL_ACCEPTING = item.MAIL_ACCEPTING

                    .MODIFIED_BY = log.Username
                    .MODIFIED_DATE = Date.Now
                    .MODIFIED_LOG = log.ComputerName
                End With

                Context.SaveChanges()
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DeleteApproveSetup(ByVal itemDeletes As List(Of Decimal)) As Boolean
        Try
            Dim items = Context.SE_APP_SETUP.Where(Function(p) itemDeletes.Contains(p.ID))

            For Each item As SE_APP_SETUP In items
                Context.SE_APP_SETUP.DeleteObject(item)
            Next

            Context.SaveChanges()

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsExistSetupByDate(ByVal itemCheck As ApproveSetupDTO, Optional ByVal idExclude As Decimal? = Nothing) As Boolean
        Try
            Dim toDate As Date

            If itemCheck.TO_DATE.HasValue Then
                toDate = itemCheck.TO_DATE.Value
            Else
                toDate = Date.MaxValue
            End If

            Dim orgId As Decimal? = itemCheck.ORG_ID
            Dim employeeId As Decimal? = itemCheck.EMPLOYEE_ID
            Dim processId As Decimal = itemCheck.PROCESS_ID
            Dim fromDate As Date = itemCheck.FROM_DATE

            Dim tempCheck = From a In Context.SE_APP_SETUP
                            Where _
                            ((orgId.HasValue AndAlso a.ORG_ID = orgId) OrElse (Not orgId.HasValue)) _
                            AndAlso ((employeeId.HasValue AndAlso a.EMPLOYEE_ID = employeeId) OrElse (Not employeeId.HasValue)) _
                            AndAlso a.PROCESS_ID = processId _
                            AndAlso (Not idExclude.HasValue OrElse (idExclude.HasValue AndAlso a.ID <> idExclude)) _
                            AndAlso
                                ( _
                                    (a.TO_DATE.HasValue _
                                        AndAlso
                                        ( _
                                            (a.FROM_DATE <= fromDate AndAlso fromDate <= a.TO_DATE.Value) OrElse _
                                            (a.FROM_DATE <= toDate AndAlso toDate <= a.TO_DATE.Value) OrElse _
                                            (fromDate <= a.FROM_DATE AndAlso a.FROM_DATE <= toDate) OrElse _
                                            (fromDate <= a.TO_DATE.Value AndAlso a.TO_DATE.Value <= toDate) _
                                        ) _
                                    ) _
                                OrElse _
                                    (Not a.TO_DATE.HasValue _
                                        AndAlso _
                                        ( _
                                            (fromDate < a.FROM_DATE AndAlso toDate >= a.FROM_DATE) OrElse _
                                            (fromDate >= a.FROM_DATE) _
                                        ) _
                                    ) _
                                )

            'Logger.LogInfo(tempCheck)

            If tempCheck.Count() > 0 Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Approve Template"
    ''' <lastupdate>
    ''' 24/07/2017 14:03
    ''' </lastupdate>
    ''' <summary>
    ''' Thêm mới thiết lập template
    ''' </summary>
    ''' <param name="item"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertApproveTemplate(ByVal item As ApproveTemplateDTO, ByVal log As UserLog) As Boolean
        Try
            Dim itemInsert As New SE_APP_TEMPLATE With {
                .ID = Utilities.GetNextSequence(Context, Context.SE_APP_TEMPLATE.EntitySet.Name),
                .TEMPLATE_NAME = item.TEMPLATE_NAME,
                .TEMPLATE_TYPE = item.TEMPLATE_TYPE,
                .TEMPLATE_ORDER = item.TEMPLATE_ORDER,
                .ACTFLG = "A",
                .CREATED_BY = log.Username,
                .CREATED_LOG = log.ComputerName,
                .CREATED_DATE = Date.Now,
                .MODIFIED_BY = log.Username,
                .MODIFIED_DATE = Date.Now,
                .MODIFIED_LOG = log.ComputerName
            }

            Context.SE_APP_TEMPLATE.AddObject(itemInsert)
            Context.SaveChanges()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 24/07/2017 14:03
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật thiết lập template
    ''' </summary>
    ''' <param name="item"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateApproveTemplate(ByVal item As ApproveTemplateDTO, ByVal log As UserLog) As Boolean
        Try

            Dim itemUpdate = Context.SE_APP_TEMPLATE.FirstOrDefault(Function(p) p.ID = item.ID)

            If itemUpdate IsNot Nothing Then
                With itemUpdate
                    .TEMPLATE_NAME = item.TEMPLATE_NAME
                    .TEMPLATE_TYPE = item.TEMPLATE_TYPE
                    .TEMPLATE_ORDER = item.TEMPLATE_ORDER
                    .ACTFLG = item.ACTFLG
                    .MODIFIED_BY = log.Username
                    .MODIFIED_DATE = Date.Now
                    .MODIFIED_LOG = log.ComputerName
                End With

                Context.SaveChanges()
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 24/07/2017 14:03
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy thiết lập template
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetApproveTemplate(ByVal id As Decimal) As ApproveTemplateDTO
        Try
            Dim itemReturn = (From s In Context.SE_APP_TEMPLATE
                              Where s.ID = id
                              Select New ApproveTemplateDTO With {
                                      .ID = s.ID,
                                  .TEMPLATE_NAME = s.TEMPLATE_NAME,
                                  .TEMPLATE_TYPE = s.TEMPLATE_TYPE,
                                  .TEMPLATE_ORDER = s.TEMPLATE_ORDER,
                                  .ACTFLG = s.ACTFLG
                              }).FirstOrDefault

            Return itemReturn

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 24/07/2017 14:03
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy danh sách thiết lập template
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetApproveTemplate() As List(Of ApproveTemplateDTO)
        Try
            Dim itemReturn = (From s In Context.SE_APP_TEMPLATE
                              Select New ApproveTemplateDTO With {
                                  .ID = s.ID,
                                  .TEMPLATE_NAME = s.TEMPLATE_NAME,
                                  .TEMPLATE_TYPE = s.TEMPLATE_TYPE,
                                  .TEMPLATE_ORDER = s.TEMPLATE_ORDER,
                                  .TEMPLATE_CODE = s.TEMPLATE_CODE,
                                  .ACTFLG = s.ACTFLG
                              }).ToList

            Return itemReturn

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 24/07/2017 14:03
    ''' </lastupdate>
    ''' <summary>
    ''' Xóa thiết lập template
    ''' </summary>
    ''' <param name="itemDeletes"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteApproveTemplate(ByVal itemDeletes As List(Of Decimal)) As Boolean
        Try
            Dim items = Context.SE_APP_TEMPLATE.Where(Function(p) itemDeletes.Contains(p.ID)).ToList

            For Each item As SE_APP_TEMPLATE In items
                Dim lstDtl = (From p In Context.SE_APP_TEMPLATE_DTL Where p.TEMPLATE_ID = item.ID)
                For Each dtl In lstDtl
                    Context.SE_APP_TEMPLATE_DTL.DeleteObject(dtl)
                Next
                Dim lstExt = (From p In Context.SE_APP_TEMPLATE_DTL Where item.ID = p.TEMPLATE_ID).ToList
                For Each ext In lstExt
                    Context.SE_APP_TEMPLATE_DTL.DeleteObject(ext)
                Next
                Context.SE_APP_TEMPLATE.DeleteObject(item)
            Next

            Context.SaveChanges()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Check thiết lập template đã được sử dụng hay chưa
    ''' </summary>
    ''' <param name="templateID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsApproveTemplateUsed(ByVal templateID As Decimal) As Boolean
        Try
            Dim listUsed = Context.SE_APP_SETUP.Count(Function(p) p.TEMPLATE_ID = templateID)

            If listUsed > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Approve Template Detail"
    ''' <lastupdate>
    ''' 24/07/2017
    ''' </lastupdate>
    ''' <summary>
    ''' Thêm mới thiết lập phê duyệt chi tiết template
    ''' </summary>
    ''' <param name="item"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertApproveTemplateDetail(ByVal item As ApproveTemplateDetailDTO, ByVal log As UserLog) As Boolean
        Try
            Dim itemInsert As New SE_APP_TEMPLATE_DTL With {
                .ID = Utilities.GetNextSequence(Context, Context.SE_APP_TEMPLATE_DTL.EntitySet.Name),
                .TEMPLATE_ID = item.TEMPLATE_ID,
                .APP_LEVEL = item.APP_LEVEL,
                .APP_TYPE = item.APP_TYPE,
                .APP_ID = item.APP_ID,
                .INFORM_DATE = item.INFORM_DATE,
                .INFORM_EMAIL = item.INFORM_EMAIL,
                .CREATED_BY = log.Username,
                .CREATED_LOG = log.ComputerName,
                .CREATED_DATE = Date.Now,
                .MODIFIED_BY = log.Username,
                .MODIFIED_DATE = Date.Now,
                .MODIFIED_LOG = log.ComputerName
            }

            Context.SE_APP_TEMPLATE_DTL.AddObject(itemInsert)

            Context.SaveChanges()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 24/07/2017
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật thiết lập cho chi tiết template
    ''' </summary>
    ''' <param name="item"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateApproveTemplateDetail(ByVal item As ApproveTemplateDetailDTO, ByVal log As UserLog) As Boolean
        Try

            Dim itemUpdate = Context.SE_APP_TEMPLATE_DTL.FirstOrDefault(Function(p) p.ID = item.ID)

            If itemUpdate IsNot Nothing Then
                With itemUpdate
                    .APP_LEVEL = item.APP_LEVEL
                    .APP_TYPE = item.APP_TYPE
                    .APP_ID = item.APP_ID
                    .INFORM_DATE = item.INFORM_DATE
                    .INFORM_EMAIL = item.INFORM_EMAIL

                    .MODIFIED_BY = log.Username
                    .MODIFIED_DATE = Date.Now
                    .MODIFIED_LOG = log.ComputerName
                End With

                Context.SaveChanges()
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 24/07/2017
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy thông tin thiết lập chi tiết template
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetApproveTemplateDetail(ByVal id As Decimal) As ApproveTemplateDetailDTO
        Try
            Dim itemReturn = (From item In Context.SE_APP_TEMPLATE_DTL
                              From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = item.APP_ID).DefaultIfEmpty
                              Where item.ID = id
                              Select New ApproveTemplateDetailDTO With {
                                .ID = item.ID,
                                .TEMPLATE_ID = item.TEMPLATE_ID,
                                .APP_LEVEL = item.APP_LEVEL,
                                .APP_TYPE = item.APP_TYPE,
                                .APP_ID = item.APP_ID,
                                .INFORM_DATE = item.INFORM_DATE,
                                .INFORM_EMAIL = item.INFORM_EMAIL,
                                .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                .EMPLOYEE_NAME = e.FULLNAME_VN
                              }).FirstOrDefault

            Return itemReturn

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 24/07/2017
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy danh sách thông tin chi tiết thiết lập template 
    ''' </summary>
    ''' <param name="templateId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetApproveTemplateDetailList(ByVal templateId As Decimal) As List(Of ApproveTemplateDetailDTO)
        Try
            Dim itemReturn = (From item In Context.SE_APP_TEMPLATE_DTL
                              From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = item.APP_ID).DefaultIfEmpty
                              Where item.TEMPLATE_ID = templateId
                              Select New ApproveTemplateDetailDTO With {
                                .ID = item.ID,
                                .TEMPLATE_ID = item.TEMPLATE_ID,
                                .APP_LEVEL = item.APP_LEVEL,
                                .APP_TYPE = item.APP_TYPE,
                                .APP_ID = item.APP_ID,
                                .INFORM_DATE = item.INFORM_DATE,
                                .INFORM_EMAIL = item.INFORM_EMAIL,
                                .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                .EMPLOYEE_NAME = e.FULLNAME_VN
                              }).ToList

            Return itemReturn

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Xóa thông tin chi tiết thiết lập cho template
    ''' </summary>
    ''' <param name="itemDeletes"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteApproveTemplateDetail(ByVal itemDeletes As List(Of Decimal)) As Boolean
        Try
            Dim items = Context.SE_APP_TEMPLATE_DTL.Where(Function(p) itemDeletes.Contains(p.ID))
            For Each item As SE_APP_TEMPLATE_DTL In items
                Context.SE_APP_TEMPLATE_DTL.DeleteObject(item)
            Next

            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Kiểm tra thêm mới cấp độ
    ''' </summary>
    ''' <param name="level"></param>
    ''' <param name="idExclude"></param>
    ''' <param name="idTemplate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckLevelInsert(ByVal level As Decimal, ByVal idExclude As Decimal, ByVal idTemplate As Decimal) As Boolean
        Try
            Dim item = Context.SE_APP_TEMPLATE_DTL.Where(Function(p) p.APP_LEVEL = level And p.TEMPLATE_ID = idTemplate)

            If idExclude <> 0 Then
                item = item.Where(Function(p) p.ID <> idExclude)
            End If

            If item.Count > 0 Then
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Setup Approve Ext - Thiết lập người thay thế"
    Public Function InsertApproveSetupExt(ByVal item As ApproveSetupExtDTO, ByVal log As UserLog) As Boolean
        Try
            Dim itemInsert As New SE_APP_SETUPEXT With {
                .ID = Utilities.GetNextSequence(Context, Context.SE_APP_SETUPEXT.EntitySet.Name),
                .EMPLOYEE_ID = item.EMPLOYEE_ID,
                .SUB_EMPLOYEE_ID = item.SUB_EMPLOYEE_ID,
                .PROCESS_ID = item.PROCESS_ID,
                .FROM_DATE = item.FROM_DATE,
                .TO_DATE = item.TO_DATE,
                .CREATED_BY = log.Username,
                .CREATED_LOG = log.ComputerName,
                .CREATED_DATE = Date.Now,
                .MODIFIED_BY = log.Username,
                .MODIFIED_DATE = Date.Now,
                .REPLACEALL = item.REPALCEALL,
                .MODIFIED_LOG = log.ComputerName
            }

            Context.SE_APP_SETUPEXT.AddObject(itemInsert)

            Context.SaveChanges()
            Dim check = Context.SE_APP_SETUPEXT.OrderByDescending(Function(p) p.ID).FirstOrDefault().ID
            UpdateIfCheckReplace(check)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateApproveSetupExt(ByVal item As ApproveSetupExtDTO, ByVal log As UserLog) As Boolean
        Try

            Dim itemUpdate = Context.SE_APP_SETUPEXT.FirstOrDefault(Function(p) p.ID = item.ID)

            If itemUpdate IsNot Nothing Then
                With itemUpdate
                    .PROCESS_ID = item.PROCESS_ID
                    .SUB_EMPLOYEE_ID = item.SUB_EMPLOYEE_ID
                    .FROM_DATE = item.FROM_DATE
                    .TO_DATE = item.TO_DATE
                    .REPLACEALL = item.REPALCEALL
                    .MODIFIED_BY = log.Username
                    .MODIFIED_DATE = Date.Now
                    .MODIFIED_LOG = log.ComputerName
                End With

                Context.SaveChanges()
            End If
            UpdateIfCheckReplace(item.ID)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateIfCheckReplace(ByVal check As Integer) As Boolean
        Try
            Dim ktra = Context.SE_APP_SETUPEXT.FirstOrDefault(Function(p) p.ID = check).REPLACEALL
            If ktra = -1 Then
                Dim s1 = Context.SE_APP_SETUPEXT.FirstOrDefault(Function(p) p.ID = check).EMPLOYEE_ID
                Dim s2 = Context.SE_APP_SETUPEXT.FirstOrDefault(Function(p) p.ID = check).SUB_EMPLOYEE_ID

                Dim itemUpdate = Context.SE_APP_TEMPLATE_DTL.FirstOrDefault(Function(p) p.APP_ID = s1)
                If itemUpdate IsNot Nothing Then
                    With itemUpdate
                        .APP_ID = s2
                    End With
                    Context.SaveChanges()
                End If
            End If

        Catch ex As Exception

        End Try
    End Function
    Public Function GetApproveSetupExt(ByVal id As Decimal) As ApproveSetupExtDTO
        Try
            Dim itemReturn = (From item In Context.SE_APP_SETUPEXT
                              From empExt In Context.HU_EMPLOYEE.Where(Function(f) f.ID = item.SUB_EMPLOYEE_ID).DefaultIfEmpty
                              From proc In Context.SE_APP_PROCESS.Where(Function(f) f.ID = item.PROCESS_ID)
                              Where item.ID = id
                              Select New ApproveSetupExtDTO With {
                                .ID = item.ID,
                                .EMPLOYEE_ID = item.EMPLOYEE_ID,
                                .SUB_EMPLOYEE_ID = item.SUB_EMPLOYEE_ID,
                                .SUB_EMPLOYEE_CODE = empExt.EMPLOYEE_CODE,
                                .SUB_EMPLOYEE_NAME = empExt.FULLNAME_VN,
                                .PROCESS_ID = item.PROCESS_ID,
                                .PROCESS_NAME = proc.NAME,
                                .FROM_DATE = item.FROM_DATE,
                                .TO_DATE = item.TO_DATE,
                                .REPALCEALL = item.REPLACEALL
                              }).FirstOrDefault

            Return itemReturn

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetApproveSetupExtList(ByVal employeeId As Decimal,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupExtDTO)
        Try
            Dim itemReturn = (From item In Context.SE_APP_SETUPEXT
                              From empExt In Context.HU_EMPLOYEE.Where(Function(f) f.ID = item.SUB_EMPLOYEE_ID).DefaultIfEmpty
                              From proc In Context.SE_APP_PROCESS.Where(Function(f) f.ID = item.PROCESS_ID)
                              Where item.EMPLOYEE_ID = employeeId
                              Select New ApproveSetupExtDTO With {
                                .ID = item.ID,
                                .EMPLOYEE_ID = item.EMPLOYEE_ID,
                                .SUB_EMPLOYEE_ID = item.SUB_EMPLOYEE_ID,
                                .SUB_EMPLOYEE_CODE = empExt.EMPLOYEE_CODE,
                                .SUB_EMPLOYEE_NAME = empExt.FULLNAME_VN,
                                .PROCESS_ID = item.PROCESS_ID,
                                .PROCESS_NAME = proc.NAME,
                                .FROM_DATE = item.FROM_DATE,
                                .TO_DATE = item.TO_DATE
                              }).OrderBy(Function(p) Sorts).ToList

            Return itemReturn

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DeleteApproveSetupExt(ByVal itemDeletes As List(Of Decimal)) As Boolean
        Try
            Dim items = Context.SE_APP_SETUPEXT.Where(Function(p) itemDeletes.Contains(p.ID))

            For Each item As SE_APP_SETUPEXT In items
                Context.SE_APP_SETUPEXT.DeleteObject(item)
            Next

            Context.SaveChanges()

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsExistSetupExtByDate(ByVal itemCheck As ApproveSetupExtDTO, Optional ByVal idExclude As Decimal? = Nothing) As Boolean
        Try

            Dim fromDate As Date = itemCheck.FROM_DATE
            Dim toDate As Date = itemCheck.TO_DATE

            Dim employeeId As Decimal = itemCheck.EMPLOYEE_ID
            Dim processId As Decimal = itemCheck.PROCESS_ID

            Dim tempCheck = From a In Context.SE_APP_SETUPEXT
                            Where _
                            a.EMPLOYEE_ID = employeeId _
                            AndAlso a.PROCESS_ID = processId _
                            AndAlso (Not idExclude.HasValue OrElse (idExclude.HasValue AndAlso a.ID <> idExclude)) _
                            AndAlso _
                            ( _
                                (a.FROM_DATE <= fromDate AndAlso fromDate <= a.TO_DATE) OrElse _
                                (a.FROM_DATE <= toDate AndAlso toDate <= a.TO_DATE) OrElse _
                                (fromDate <= a.FROM_DATE AndAlso a.FROM_DATE <= toDate) OrElse _
                                (fromDate <= a.TO_DATE AndAlso a.TO_DATE <= toDate) _
                            )

            If tempCheck.Count() > 0 Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Lấy thông tin về quy trình phê duyệt cho nhân viên khi đăng ký"

    ''' <summary>
    ''' Lấy danh sách người phê duyệt đối với nhân viên đang xử lý
    ''' </summary>
    ''' <param name="employeeId">ID của nhân viên cần lấy</param>
    ''' <param name="processCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetApproveUsers(ByVal employeeId As Decimal, ByVal processCode As String,
                                    Optional ByVal lstOrg As List(Of Decimal) = Nothing,
                                    Optional ByVal isTimesheet As Boolean = False) As List(Of ApproveUserDTO)
        Try
            Dim listResult As New List(Of ApproveUserDTO)

            Dim process = Context.SE_APP_PROCESS.FirstOrDefault(Function(p) p.PROCESS_CODE = processCode)

            If process Is Nothing Then
                Throw New Exception("Chưa thiết lập quy trình phê duyệt HOẶC Mã quy trình phê duyệt sai.")
            End If

            'Lấy template phê duyệt đang áp dụng cho nhân viên
            Dim usingSetups As List(Of SE_APP_SETUP) = GetCurrentEmployeeSetup(employeeId, process, lstOrg, isTimesheet)

            If usingSetups.Count > 0 Then

                Dim firstTemplate = (From p In usingSetups
                                    From temp In Context.SE_APP_TEMPLATE.Where(Function(f) f.ID = p.TEMPLATE_ID)
                                    Select temp.TEMPLATE_ORDER
                                    Order By TEMPLATE_ORDER Descending).FirstOrDefault()

                Dim usingTemplateDetail As List(Of SE_APP_TEMPLATE_DTL) = (From p In Context.SE_APP_TEMPLATE_DTL
                                                                          Where p.TEMPLATE_ID = firstTemplate).ToList()

                For Each detailSetting As SE_APP_TEMPLATE_DTL In usingTemplateDetail
                    Dim itemAdd As ApproveUserDTO = Nothing
                    If detailSetting.APP_TYPE = 0 Then
                        itemAdd = GetDirectManagerApprove(employeeId, detailSetting.APP_LEVEL)
                    Else
                        itemAdd = GetEmployeeApprove(detailSetting.APP_ID, detailSetting.APP_LEVEL)
                    End If

                    If itemAdd IsNot Nothing Then
                        itemAdd.INFORM_DATE = detailSetting.INFORM_DATE
                        itemAdd.INFORM_EMAIL = detailSetting.INFORM_EMAIL

                        listResult.Add(itemAdd)
                    End If
                Next

                If usingTemplateDetail.Count = listResult.Count Then
                    Return listResult
                Else
                    Return Nothing
                End If
            End If

            Return listResult
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "Lấy người phê duyệt theo từng cấp của nhân viên"
    Private Function GetDirectManagerApprove(ByVal employeeId As Decimal, ByVal level As Integer) As ApproveUserDTO
        Dim employee = Context.HU_EMPLOYEE.FirstOrDefault(Function(p) p.ID = employeeId)

        If employee.DIRECT_MANAGER.HasValue Then
            Dim approveUser = (From cv In Context.HU_EMPLOYEE_CV
                              From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = cv.EMPLOYEE_ID)
                              Where cv.EMPLOYEE_ID = employee.DIRECT_MANAGER).FirstOrDefault

            If approveUser IsNot Nothing Then
                Return New ApproveUserDTO With {
                    .EMPLOYEE_ID = approveUser.cv.EMPLOYEE_ID,
                    .EMPLOYEE_NAME = approveUser.e.FULLNAME_VN,
                    .EMAIL = approveUser.cv.WORK_EMAIL,
                    .LEVEL = level
                }
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If
        Return Nothing
    End Function

    Private Function GetEmployeeApprove(ByVal employeeId As Decimal, ByVal level As Integer) As ApproveUserDTO

        Dim approveUser = (From p In Context.HU_EMPLOYEE_CV
                           From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                           Where p.EMPLOYEE_ID = employeeId
                           Select New ApproveUserDTO With {.EMPLOYEE_ID = employeeId,
                                                           .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                           .EMAIL = p.WORK_EMAIL,
                                                           .LEVEL = level
                                                          }).FirstOrDefault()

        Return approveUser

    End Function
#End Region

#Region "Lấy thông tin thiết lập phê duyệt cho nhân viên"
    Private Function GetCurrentEmployeeSetup(ByVal employeeId As Decimal, ByVal process As SE_APP_PROCESS,
                                    Optional ByVal lstOrg As List(Of Decimal) = Nothing,
                                    Optional ByVal isTimesheet As Boolean = False) As List(Of SE_APP_SETUP)

        Dim _setup As New List(Of SE_APP_SETUP)
        Dim setupEmployee = Context.SE_APP_SETUP.FirstOrDefault(Function(p) p.EMPLOYEE_ID = employeeId _
                                                                     AndAlso p.PROCESS_ID = process.ID _
                                                                     AndAlso (p.FROM_DATE <= Date.Now _
                                                                              AndAlso (Not p.TO_DATE.HasValue _
                                                                                       OrElse (p.TO_DATE.HasValue _
                                                                                               AndAlso Date.Now <= p.TO_DATE.Value))))

        If setupEmployee IsNot Nothing Then
            _setup.Add(setupEmployee)
        End If

        Dim setupOrg = GetCurrentEmployeeOrgSetup(employeeId, process, lstOrg, isTimesheet)
        If setupOrg.Count > 0 Then
            _setup.AddRange(setupOrg)
        End If
        '_setup.Add(GetCurrentEmployeeOrgSetup(employeeId, process))
        Return _setup
    End Function

    Private Function GetCurrentEmployeeOrgSetup(ByVal employeeId As Decimal, ByVal process As SE_APP_PROCESS,
                                                Optional ByVal lstOrg As List(Of Decimal) = Nothing,
                                                Optional ByVal isTimesheet As Boolean = False) As List(Of SE_APP_SETUP)

        Dim _setup As List(Of SE_APP_SETUP) = New List(Of SE_APP_SETUP)
        If isTimesheet Then
            ' lấy thiết lập theo org truyền vào
            If lstOrg.Count = 0 Then
                Return Nothing
            End If
            Dim lstTemp = (From p In Context.HU_ORGANIZATION Where lstOrg.Contains(p.ID)).ToList

            For Each Org In lstTemp
                Dim currentOrgSetup = GetCurrentOrgSetup(Org.ID, process)
                If currentOrgSetup IsNot Nothing Then
                    _setup.Add(currentOrgSetup)
                End If
            Next

            If lstTemp.Count <> _setup.Count Then
                Dim isParent As Boolean = False
                For Each Org In lstTemp
                    While Org.PARENT_ID.HasValue
                        Org = Context.HU_ORGANIZATION.FirstOrDefault(Function(p) p.ID = Org.PARENT_ID.Value)
                        Dim OrgSetup = GetCurrentOrgSetup(Org.ID, process)
                        If OrgSetup IsNot Nothing Then
                            isParent = True
                            _setup.Add(OrgSetup)
                        End If
                    End While
                Next
                If isParent Then
                    Return _setup
                Else
                    Return New List(Of SE_APP_SETUP)
                End If
            Else
                For Each Org In lstTemp
                    While Org.PARENT_ID.HasValue
                        Org = Context.HU_ORGANIZATION.FirstOrDefault(Function(p) p.ID = Org.PARENT_ID.Value)
                        Dim OrgSetup = GetCurrentOrgSetup(Org.ID, process)
                        If OrgSetup IsNot Nothing Then
                            _setup.Add(OrgSetup)
                        End If
                    End While
                Next
            End If
        Else
            ' lấy ORG hiện tại của nhân viên (lấy trong HU_WORKING)
            Dim Org = (From p In Context.HU_EMPLOYEE
                       From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                       Where p.ID = employeeId
                       Select o).FirstOrDefault

            Dim currentOrgSetup = GetCurrentOrgSetup(Org.ID, process)

            If currentOrgSetup IsNot Nothing Then
                _setup.Add(currentOrgSetup)
            End If

            While Org.PARENT_ID.HasValue

                Org = Context.HU_ORGANIZATION.FirstOrDefault(Function(p) p.ID = Org.PARENT_ID.Value)

                Dim OrgSetup = GetCurrentOrgSetup(Org.ID, process)
                If OrgSetup IsNot Nothing Then
                    _setup.Add(OrgSetup)
                End If
            End While
        End If


        Return _setup
    End Function

    Private Function GetCurrentOrgSetup(ByVal orgId As Decimal, ByVal process As SE_APP_PROCESS) As SE_APP_SETUP

        Dim setupOrg = Context.SE_APP_SETUP.FirstOrDefault(Function(p) p.ORG_ID.HasValue _
                                                                AndAlso p.ORG_ID = orgId _
                                                                AndAlso p.PROCESS_ID = process.ID _
                                                                AndAlso (p.FROM_DATE <= Date.Now _
                                                                         AndAlso (Not p.TO_DATE.HasValue _
                                                                                  OrElse (p.TO_DATE.HasValue _
                                                                                          AndAlso Date.Now <= p.TO_DATE.Value))))

        'Nếu có thiết lập riêng cho nhân viên
        Return setupOrg

    End Function
#End Region


#End Region

    Public Function GetListEmployee(ByVal _orgIds As List(Of Decimal)) As List(Of EmployeeDTO)
        Dim query As ObjectQuery(Of EmployeeDTO)
        query = (From p In Context.HU_EMPLOYEE
                 Where _orgIds.Contains(p.ORG_ID) And _
                 p.CONTRACT_ID.HasValue And _
                 ((p.WORK_STATUS.HasValue And p.WORK_STATUS <> CommonCommon.OT_WORK_STATUS.TERMINATE_ID) Or Not p.WORK_STATUS.HasValue)
                 Order By p.EMPLOYEE_CODE
                    Select New EmployeeDTO With {
                     .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                     .ID = p.ID,
                     .FULLNAME_VN = p.FULLNAME_VN
                    })

        Return query.ToList

    End Function

End Class
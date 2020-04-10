Imports System.IO
Imports Framework.Data
Imports System.Data.Objects
Imports Framework.Data.System.Linq.Dynamic
Imports System.Net
Imports System.ComponentModel
Imports System.Globalization
Imports System.Reflection

Partial Class ProfileRepository

#Region "Service Auto Update Employee Information"
    Public Function CheckAndUpdateEmployeeInformation() As Boolean
        UpdateWorking()
        UpdateTerminate()
        UpdateContract()
        Return True
    End Function

    Private Sub UpdateWorking()
        Try
            Dim query = (From p In Context.HUV_CURRENT_WORKING
                         Select New WorkingDTO With {
                             .EMPLOYEE_ID = p.EMPLOYEE_ID,
                             .TITLE_ID = p.TITLE_ID,
                             .ORG_ID = p.ORG_ID,
                             .ID = p.ID,
                             .STAFF_RANK_ID = p.STAFF_RANK_ID,
                             .EFFECT_DATE = p.EFFECT_DATE,
                             .DIRECT_MANAGER = p.DIRECT_MANAGER,
                             .OBJECT_ATTENDANCE = p.OBJECT_ATTENDANCE,
                             .OBJECT_LABOR = p.OBJECT_LABOR,
                             .JOB_POSITION = p.JOB_POSITION,
                             .JOB_DESCRIPTION = p.JOB_DESCRIPTION,
                             .IS_HURTFUL = p.IS_HURTFUL,
                             .IS_HDLD = p.IS_THHDLD
                             }).ToList

            For i As Integer = 0 To query.Count - 1
                ApproveWorking1(query(i))
                ApproveWorking(query(i))
            Next
            If query.Count > 0 Then Context.SaveChanges()

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
        End Try
    End Sub

    Private Sub UpdateTerminate()
        Try
            Dim query = (From p In Context.HUV_TERMINATE_CURRENT
                         Select New TerminateDTO With {
                             .EMPLOYEE_ID = p.EMPLOYEE_ID,
                             .ID = p.ID,
                             .EFFECT_DATE = p.EFFECT_DATE,
                             .LAST_DATE = p.LAST_DATE
                             }).ToList

            For i As Integer = 0 To query.Count - 1
                Dim empId = query(i).EMPLOYEE_ID
                Dim item = (From p In Context.HU_EMPLOYEE Where p.ID = empId).FirstOrDefault
                If item IsNot Nothing Then
                    Dim Employeelist = (From p In Context.HU_EMPLOYEE Where item.EMPLOYEE_CODE = p.EMPLOYEE_CODE Select p).ToList
                    For Each objEmployeeData As HU_EMPLOYEE In Employeelist
                        objEmployeeData.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
                        objEmployeeData.TER_EFFECT_DATE = query(i).EFFECT_DATE
                        objEmployeeData.TER_LAST_DATE = query(i).LAST_DATE
                        objEmployeeData.EMP_STATUS = 6
                    Next
                End If
            Next

            If query.Count > 0 Then Context.SaveChanges()

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
        End Try
    End Sub

    Private Sub UpdateContract()
        Try
            Dim query = (From p In Context.HUV_CURRENT_CONTRACT
                         Select New ContractDTO With {
                             .ID = p.ID,
                             .START_DATE = p.START_DATE,
                             .EXPIRE_DATE = p.EXPIRE_DATE,
                             .WORKING_ID = p.WORKING_ID,
                             .EMPLOYEE_ID = p.EMPLOYEE_ID,
                             .CONTRACTTYPE_ID = p.CONTRACT_TYPE_ID}).ToList

            For i As Integer = 0 To query.Count - 1
                Try
                    Dim empId = query(i).EMPLOYEE_ID
                    Dim contractID = query(i).ID
                    Dim emp = (From p In Context.HU_EMPLOYEE Where p.ID = empId).FirstOrDefault
                    If (emp.TER_EFFECT_DATE Is Nothing) OrElse (emp.TER_EFFECT_DATE IsNot Nothing AndAlso query(i).START_DATE <= emp.TER_EFFECT_DATE) Then
                        ApproveContract(query(i))
                        Dim objContract As ContractDTO = (From p In Context.HU_CONTRACT
                                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                                        Where p.ID = contractID
                                        Select New ContractDTO With {
                      .EMPLOYEE_ID = p.EMPLOYEE_ID,
                      .WORKING_ID = p.WORKING_ID,
                      .ORG_ID = p.ORG_ID,
                      .TITLE_ID = p.TITLE_ID,
                      .START_DATE = p.START_DATE,
                      .SIGN_ID = p.SIGN_ID,
                      .SIGNER_TITLE = p.SIGNER_TITLE,
                      .SIGN_DATE = p.SIGN_DATE,
                      .OBJECTTIMEKEEPING = e.OBJECTTIMEKEEPING,
                      .DIRECT_MANAGER = e.DIRECT_MANAGER,
                      .SIGNER_NAME = p.SIGNER_NAME
                                            }).FirstOrDefault
                        If IsFirstContract(objContract) Then
                            InsertDecision(objContract)
                        End If
                    End If
                Catch ex As Exception
                    Continue For
                End Try
            Next
            If query.Count > 0 Then Context.SaveChanges()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
        End Try
    End Sub

#End Region
End Class

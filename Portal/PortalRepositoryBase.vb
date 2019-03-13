Imports Portal.PortalBusiness
Imports Framework.UI
Imports Common.CommonBusiness
Imports Common

Public Class PortalRepositoryBase

    Public ReadOnly Property Log As UserLog
        Get
            Return LogHelper.GetUserLog
        End Get
    End Property


End Class

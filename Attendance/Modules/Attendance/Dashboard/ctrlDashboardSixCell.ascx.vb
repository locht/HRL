﻿Imports Framework.UI
Imports Telerik.Web.UI

Public Class ctrlDashboardSixCell
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
#End Region

#Region "Page"
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        IsShowDenyMessage = False

    End Sub
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try

            ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "SizeToFit(0);", True)
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "Event"

#End Region

#Region "Custom"

#End Region
End Class
Function: SendMail
Datetime: 02/11/2017 3:47:13 PM
Error message: The underlying provider failed on Open.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\CommonDAL\CommonRepository.Secure.vb:line 2204
   at CommonDAL.CommonRepository.SendMail() in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\CommonDAL\CommonRepository.vb:line 859
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iCommon


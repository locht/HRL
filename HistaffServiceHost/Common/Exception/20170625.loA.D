Function: SendMail
Datetime: 6/25/2017 4:26:30 PM
Error message: The underlying provider failed on Open.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in C:\Users\thuydinh\Documents\Visual Studio 2010\Projects\Project1\CommonDAL\CommonRepository.Secure.vb:line 2145
   at CommonDAL.CommonRepository.SendMail() in C:\Users\thuydinh\Documents\Visual Studio 2010\Projects\Project1\CommonDAL\CommonRepository.vb:line 747
Inner error message: ORA-12170: TNS:Connect timeout occurred
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iCommon


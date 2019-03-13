Function: SendMail
Datetime: 09/11/2017 1:30:37 PM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\CommonDAL\CommonRepository.Secure.vb:line 2204
   at CommonDAL.CommonRepository.SendMail() in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\CommonDAL\CommonRepository.vb:line 859
Inner error message: ORA-03135: connection lost contact
Process ID: 2876
Session ID: 120 Serial number: 32488
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Common.DbCommand.ExecuteReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iCommon

Function: SendMail
Datetime: 09/11/2017 1:56:08 PM
Error message: The underlying provider failed on Open.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\CommonDAL\CommonRepository.Secure.vb:line 2204
   at CommonDAL.CommonRepository.SendMail() in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\CommonDAL\CommonRepository.vb:line 859
Inner error message: ORA-12516: TNS:listener could not find available handler with matching protocol stack
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iCommon

Function: SendMail
Datetime: 09/11/2017 5:20:01 PM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\CommonDAL\CommonRepository.Secure.vb:line 2204
   at CommonDAL.CommonRepository.SendMail() in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\CommonDAL\CommonRepository.vb:line 859
Inner error message: ORA-03135: connection lost contact
Process ID: 4068
Session ID: 115 Serial number: 48988
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Common.DbCommand.ExecuteReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iCommon


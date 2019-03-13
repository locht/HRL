Function: SendMail
Datetime: 01/11/2017 10:16:31 AM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\CommonDAL\CommonRepository.Secure.vb:line 2204
   at CommonDAL.CommonRepository.SendMail() in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\CommonDAL\CommonRepository.vb:line 859
Inner error message: ORA-03113: end-of-file on communication channel
Process ID: 3288
Session ID: 105 Serial number: 9699
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Common.DbCommand.ExecuteReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iCommon

Function: SendMail
Datetime: 01/11/2017 12:35:35 PM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\CommonDAL\CommonRepository.Secure.vb:line 2204
   at CommonDAL.CommonRepository.SendMail() in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\CommonDAL\CommonRepository.vb:line 859
Inner error message: ORA-03135: connection lost contact
Process ID: 3516
Session ID: 61 Serial number: 8648
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Common.DbCommand.ExecuteReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iCommon


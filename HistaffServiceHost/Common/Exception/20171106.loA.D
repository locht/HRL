Function: SendMail
Datetime: 06/11/2017 4:15:20 PM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\CommonDAL\CommonRepository.Secure.vb:line 2204
   at CommonDAL.CommonRepository.SendMail() in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\CommonDAL\CommonRepository.vb:line 859
Inner error message: ORA-03113: end-of-file on communication channel
Process ID: 2896
Session ID: 76 Serial number: 15287
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Common.DbCommand.ExecuteReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iCommon

Function: SendMail
Datetime: 06/11/2017 5:01:06 PM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\CommonDAL\CommonRepository.Secure.vb:line 2204
   at CommonDAL.CommonRepository.SendMail() in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\CommonDAL\CommonRepository.vb:line 859
Inner error message: ORA-03135: connection lost contact
Process ID: 2456
Session ID: 63 Serial number: 12638
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Common.DbCommand.ExecuteReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iCommon


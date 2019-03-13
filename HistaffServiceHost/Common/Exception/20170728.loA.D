Function: SendMail
Datetime: 28/07/2017 5:54:25 PM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.Secure.vb:line 2147
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 754
Inner error message: ORA-03135: connection lost contact
Process ID: 3128
Session ID: 142 Serial number: 16508
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Common.DbCommand.ExecuteReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iCommon


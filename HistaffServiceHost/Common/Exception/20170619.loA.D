Function: SendMail
Datetime: 6/19/2017 10:22:16 AM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in D:\HISTAFF\02.Sourcecode\01.SourceCode\CommonDAL\CommonRepository.Secure.vb:line 2145
   at CommonDAL.CommonRepository.SendMail() in D:\HISTAFF\02.Sourcecode\01.SourceCode\CommonDAL\CommonRepository.vb:line 747
Inner error message: ORA-03113: end-of-file on communication channel
Process ID: 3096
Session ID: 24 Serial number: 6022
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Common.DbCommand.ExecuteReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iCommon


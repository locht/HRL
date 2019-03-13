Function: SendMail
Datetime: 03/08/2017 12:38:59 PM
Error message: The underlying provider failed on Open.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.Secure.vb:line 2147
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 754
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iCommon

Function: SendMail
Datetime: 03/08/2017 3:43:53 PM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.Secure.vb:line 2147
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 754
Inner error message: ORA-03135: connection lost contact
Process ID: 2236
Session ID: 210 Serial number: 47764
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Common.DbCommand.ExecuteReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iCommon

Function: SendMail
Datetime: 03/08/2017 3:55:26 PM
Error message: The underlying provider failed on Open.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.Secure.vb:line 2147
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 754
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iCommon


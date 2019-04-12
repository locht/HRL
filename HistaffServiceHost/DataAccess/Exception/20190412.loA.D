Function: ExecuteStore
Datetime: 04/12/2019 11:03:24 AM
Error message: ORA-04063: package body "HISTAFFHCM.PKG_COMMON_LIST" has errors
ORA-06508: PL/SQL: could not find program unit being called: "HISTAFFHCM.PKG_COMMON_LIST"
ORA-06512: at line 1
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\MyProject\histaffHCM\histaffhcm\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess


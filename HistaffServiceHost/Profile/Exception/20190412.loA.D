Function: GetOtherList
Datetime: 04/12/2019 11:03:24 AM
Error message: ORA-04063: package body "HISTAFFHCM.PKG_COMMON_LIST" has errors
ORA-06508: PL/SQL: could not find program unit being called: "HISTAFFHCM.PKG_COMMON_LIST"
ORA-06512: at line 1
Stack trace:    at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\MyProject\histaffHCM\histaffhcm\Framework.Data\DataAccess\Query.vb:line 306
   at ProfileDAL.ProfileRepository.GetOtherList(String sType, String sLang, Boolean isBlank) in D:\MyProject\histaffHCM\histaffhcm\ProfileDAL\ProfileRepository.vb:line 49
Other Text: iProfile


Function: SendMail
Datetime: 31/07/2017 8:58:02 AM
Error message: Schema specified is not valid. Errors: 
CommonContext.ssdl(7856,6) : error 0075: Key Part: 'ID' for type HU_WELFARE_LIST_GW is not valid. All parts of the key must be non nullable.
CommonContext.ssdl(10734,6) : error 0075: Key Part: 'ID' for type RC_PROGRAM_SCHEDULE is not valid. All parts of the key must be non nullable.
CommonContext.ssdl(11113,6) : error 0075: Key Part: 'ID' for type SE_ACCESS_LOG is not valid. All parts of the key must be non nullable.
Stack trace:    at System.Data.Metadata.Edm.StoreItemCollection.Loader.ThrowOnNonWarningErrors()
   at System.Data.Metadata.Edm.StoreItemCollection.Loader.LoadItems(IEnumerable`1 xmlReaders, IEnumerable`1 sourceFilePaths)
   at System.Data.Metadata.Edm.StoreItemCollection.Init(IEnumerable`1 xmlReaders, IEnumerable`1 filePaths, Boolean throwOnError, DbProviderManifest& providerManifest, DbProviderFactory& providerFactory, String& providerManifestToken, Memoizer`2& cachedCTypeFunction)
   at System.Data.Metadata.Edm.StoreItemCollection..ctor(IEnumerable`1 xmlReaders, IEnumerable`1 filePaths)
   at System.Data.Metadata.Edm.MetadataCache.StoreMetadataEntry.LoadStoreCollection(EdmItemCollection edmItemCollection, MetadataArtifactLoader loader)
   at System.Data.Metadata.Edm.MetadataCache.StoreItemCollectionLoader.LoadItemCollection(StoreMetadataEntry entry)
   at System.Data.Metadata.Edm.MetadataCache.LoadItemCollection[T](IItemCollectionLoader`1 itemCollectionLoader, T entry)
   at System.Data.Metadata.Edm.MetadataCache.GetOrCreateStoreAndMappingItemCollections(String cacheKey, MetadataArtifactLoader loader, EdmItemCollection edmItemCollection, Object& entryToken)
   at System.Data.EntityClient.EntityConnection.LoadStoreItemCollections(MetadataWorkspace workspace, DbConnection storeConnection, DbProviderFactory factory, DbConnectionOptions connectionOptions, EdmItemCollection edmItemCollection, MetadataArtifactLoader artifactLoader)
   at System.Data.EntityClient.EntityConnection.GetMetadataWorkspace(Boolean initializeAllCollections)
   at System.Data.EntityClient.EntityConnection.InitializeMetadata(DbConnection newConnection, DbConnection originalConnection, Boolean closeOriginalConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 785
Other Text: iCommon

Function: SendMail
Datetime: 31/07/2017 8:58:59 AM
Error message: Schema specified is not valid. Errors: 
CommonContext.ssdl(7856,6) : error 0075: Key Part: 'ID' for type HU_WELFARE_LIST_GW is not valid. All parts of the key must be non nullable.
CommonContext.ssdl(10734,6) : error 0075: Key Part: 'ID' for type RC_PROGRAM_SCHEDULE is not valid. All parts of the key must be non nullable.
CommonContext.ssdl(11113,6) : error 0075: Key Part: 'ID' for type SE_ACCESS_LOG is not valid. All parts of the key must be non nullable.
Stack trace:    at System.Data.Metadata.Edm.StoreItemCollection.Loader.ThrowOnNonWarningErrors()
   at System.Data.Metadata.Edm.StoreItemCollection.Loader.LoadItems(IEnumerable`1 xmlReaders, IEnumerable`1 sourceFilePaths)
   at System.Data.Metadata.Edm.StoreItemCollection.Init(IEnumerable`1 xmlReaders, IEnumerable`1 filePaths, Boolean throwOnError, DbProviderManifest& providerManifest, DbProviderFactory& providerFactory, String& providerManifestToken, Memoizer`2& cachedCTypeFunction)
   at System.Data.Metadata.Edm.StoreItemCollection..ctor(IEnumerable`1 xmlReaders, IEnumerable`1 filePaths)
   at System.Data.Metadata.Edm.MetadataCache.StoreMetadataEntry.LoadStoreCollection(EdmItemCollection edmItemCollection, MetadataArtifactLoader loader)
   at System.Data.Metadata.Edm.MetadataCache.StoreItemCollectionLoader.LoadItemCollection(StoreMetadataEntry entry)
   at System.Data.Metadata.Edm.MetadataCache.LoadItemCollection[T](IItemCollectionLoader`1 itemCollectionLoader, T entry)
   at System.Data.Metadata.Edm.MetadataCache.GetOrCreateStoreAndMappingItemCollections(String cacheKey, MetadataArtifactLoader loader, EdmItemCollection edmItemCollection, Object& entryToken)
   at System.Data.EntityClient.EntityConnection.LoadStoreItemCollections(MetadataWorkspace workspace, DbConnection storeConnection, DbProviderFactory factory, DbConnectionOptions connectionOptions, EdmItemCollection edmItemCollection, MetadataArtifactLoader artifactLoader)
   at System.Data.EntityClient.EntityConnection.GetMetadataWorkspace(Boolean initializeAllCollections)
   at System.Data.EntityClient.EntityConnection.InitializeMetadata(DbConnection newConnection, DbConnection originalConnection, Boolean closeOriginalConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 785
Other Text: iCommon

Function: SendMail
Datetime: 31/07/2017 8:59:59 AM
Error message: Schema specified is not valid. Errors: 
CommonContext.ssdl(7856,6) : error 0075: Key Part: 'ID' for type HU_WELFARE_LIST_GW is not valid. All parts of the key must be non nullable.
CommonContext.ssdl(10734,6) : error 0075: Key Part: 'ID' for type RC_PROGRAM_SCHEDULE is not valid. All parts of the key must be non nullable.
CommonContext.ssdl(11113,6) : error 0075: Key Part: 'ID' for type SE_ACCESS_LOG is not valid. All parts of the key must be non nullable.
Stack trace:    at System.Data.Metadata.Edm.StoreItemCollection.Loader.ThrowOnNonWarningErrors()
   at System.Data.Metadata.Edm.StoreItemCollection.Loader.LoadItems(IEnumerable`1 xmlReaders, IEnumerable`1 sourceFilePaths)
   at System.Data.Metadata.Edm.StoreItemCollection.Init(IEnumerable`1 xmlReaders, IEnumerable`1 filePaths, Boolean throwOnError, DbProviderManifest& providerManifest, DbProviderFactory& providerFactory, String& providerManifestToken, Memoizer`2& cachedCTypeFunction)
   at System.Data.Metadata.Edm.StoreItemCollection..ctor(IEnumerable`1 xmlReaders, IEnumerable`1 filePaths)
   at System.Data.Metadata.Edm.MetadataCache.StoreMetadataEntry.LoadStoreCollection(EdmItemCollection edmItemCollection, MetadataArtifactLoader loader)
   at System.Data.Metadata.Edm.MetadataCache.StoreItemCollectionLoader.LoadItemCollection(StoreMetadataEntry entry)
   at System.Data.Metadata.Edm.MetadataCache.LoadItemCollection[T](IItemCollectionLoader`1 itemCollectionLoader, T entry)
   at System.Data.Metadata.Edm.MetadataCache.GetOrCreateStoreAndMappingItemCollections(String cacheKey, MetadataArtifactLoader loader, EdmItemCollection edmItemCollection, Object& entryToken)
   at System.Data.EntityClient.EntityConnection.LoadStoreItemCollections(MetadataWorkspace workspace, DbConnection storeConnection, DbProviderFactory factory, DbConnectionOptions connectionOptions, EdmItemCollection edmItemCollection, MetadataArtifactLoader artifactLoader)
   at System.Data.EntityClient.EntityConnection.GetMetadataWorkspace(Boolean initializeAllCollections)
   at System.Data.EntityClient.EntityConnection.InitializeMetadata(DbConnection newConnection, DbConnection originalConnection, Boolean closeOriginalConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 785
Other Text: iCommon

Function: SendMail
Datetime: 31/07/2017 9:00:59 AM
Error message: Schema specified is not valid. Errors: 
CommonContext.ssdl(7856,6) : error 0075: Key Part: 'ID' for type HU_WELFARE_LIST_GW is not valid. All parts of the key must be non nullable.
CommonContext.ssdl(10734,6) : error 0075: Key Part: 'ID' for type RC_PROGRAM_SCHEDULE is not valid. All parts of the key must be non nullable.
CommonContext.ssdl(11113,6) : error 0075: Key Part: 'ID' for type SE_ACCESS_LOG is not valid. All parts of the key must be non nullable.
Stack trace:    at System.Data.Metadata.Edm.StoreItemCollection.Loader.ThrowOnNonWarningErrors()
   at System.Data.Metadata.Edm.StoreItemCollection.Loader.LoadItems(IEnumerable`1 xmlReaders, IEnumerable`1 sourceFilePaths)
   at System.Data.Metadata.Edm.StoreItemCollection.Init(IEnumerable`1 xmlReaders, IEnumerable`1 filePaths, Boolean throwOnError, DbProviderManifest& providerManifest, DbProviderFactory& providerFactory, String& providerManifestToken, Memoizer`2& cachedCTypeFunction)
   at System.Data.Metadata.Edm.StoreItemCollection..ctor(IEnumerable`1 xmlReaders, IEnumerable`1 filePaths)
   at System.Data.Metadata.Edm.MetadataCache.StoreMetadataEntry.LoadStoreCollection(EdmItemCollection edmItemCollection, MetadataArtifactLoader loader)
   at System.Data.Metadata.Edm.MetadataCache.StoreItemCollectionLoader.LoadItemCollection(StoreMetadataEntry entry)
   at System.Data.Metadata.Edm.MetadataCache.LoadItemCollection[T](IItemCollectionLoader`1 itemCollectionLoader, T entry)
   at System.Data.Metadata.Edm.MetadataCache.GetOrCreateStoreAndMappingItemCollections(String cacheKey, MetadataArtifactLoader loader, EdmItemCollection edmItemCollection, Object& entryToken)
   at System.Data.EntityClient.EntityConnection.LoadStoreItemCollections(MetadataWorkspace workspace, DbConnection storeConnection, DbProviderFactory factory, DbConnectionOptions connectionOptions, EdmItemCollection edmItemCollection, MetadataArtifactLoader artifactLoader)
   at System.Data.EntityClient.EntityConnection.GetMetadataWorkspace(Boolean initializeAllCollections)
   at System.Data.EntityClient.EntityConnection.InitializeMetadata(DbConnection newConnection, DbConnection originalConnection, Boolean closeOriginalConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 785
Other Text: iCommon

Function: SendMail
Datetime: 31/07/2017 9:01:59 AM
Error message: Schema specified is not valid. Errors: 
CommonContext.ssdl(7856,6) : error 0075: Key Part: 'ID' for type HU_WELFARE_LIST_GW is not valid. All parts of the key must be non nullable.
CommonContext.ssdl(10734,6) : error 0075: Key Part: 'ID' for type RC_PROGRAM_SCHEDULE is not valid. All parts of the key must be non nullable.
CommonContext.ssdl(11113,6) : error 0075: Key Part: 'ID' for type SE_ACCESS_LOG is not valid. All parts of the key must be non nullable.
Stack trace:    at System.Data.Metadata.Edm.StoreItemCollection.Loader.ThrowOnNonWarningErrors()
   at System.Data.Metadata.Edm.StoreItemCollection.Loader.LoadItems(IEnumerable`1 xmlReaders, IEnumerable`1 sourceFilePaths)
   at System.Data.Metadata.Edm.StoreItemCollection.Init(IEnumerable`1 xmlReaders, IEnumerable`1 filePaths, Boolean throwOnError, DbProviderManifest& providerManifest, DbProviderFactory& providerFactory, String& providerManifestToken, Memoizer`2& cachedCTypeFunction)
   at System.Data.Metadata.Edm.StoreItemCollection..ctor(IEnumerable`1 xmlReaders, IEnumerable`1 filePaths)
   at System.Data.Metadata.Edm.MetadataCache.StoreMetadataEntry.LoadStoreCollection(EdmItemCollection edmItemCollection, MetadataArtifactLoader loader)
   at System.Data.Metadata.Edm.MetadataCache.StoreItemCollectionLoader.LoadItemCollection(StoreMetadataEntry entry)
   at System.Data.Metadata.Edm.MetadataCache.LoadItemCollection[T](IItemCollectionLoader`1 itemCollectionLoader, T entry)
   at System.Data.Metadata.Edm.MetadataCache.GetOrCreateStoreAndMappingItemCollections(String cacheKey, MetadataArtifactLoader loader, EdmItemCollection edmItemCollection, Object& entryToken)
   at System.Data.EntityClient.EntityConnection.LoadStoreItemCollections(MetadataWorkspace workspace, DbConnection storeConnection, DbProviderFactory factory, DbConnectionOptions connectionOptions, EdmItemCollection edmItemCollection, MetadataArtifactLoader artifactLoader)
   at System.Data.EntityClient.EntityConnection.GetMetadataWorkspace(Boolean initializeAllCollections)
   at System.Data.EntityClient.EntityConnection.InitializeMetadata(DbConnection newConnection, DbConnection originalConnection, Boolean closeOriginalConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 785
Other Text: iCommon

Function: SendMail
Datetime: 31/07/2017 9:02:59 AM
Error message: Schema specified is not valid. Errors: 
CommonContext.ssdl(7856,6) : error 0075: Key Part: 'ID' for type HU_WELFARE_LIST_GW is not valid. All parts of the key must be non nullable.
CommonContext.ssdl(10734,6) : error 0075: Key Part: 'ID' for type RC_PROGRAM_SCHEDULE is not valid. All parts of the key must be non nullable.
CommonContext.ssdl(11113,6) : error 0075: Key Part: 'ID' for type SE_ACCESS_LOG is not valid. All parts of the key must be non nullable.
Stack trace:    at System.Data.Metadata.Edm.StoreItemCollection.Loader.ThrowOnNonWarningErrors()
   at System.Data.Metadata.Edm.StoreItemCollection.Loader.LoadItems(IEnumerable`1 xmlReaders, IEnumerable`1 sourceFilePaths)
   at System.Data.Metadata.Edm.StoreItemCollection.Init(IEnumerable`1 xmlReaders, IEnumerable`1 filePaths, Boolean throwOnError, DbProviderManifest& providerManifest, DbProviderFactory& providerFactory, String& providerManifestToken, Memoizer`2& cachedCTypeFunction)
   at System.Data.Metadata.Edm.StoreItemCollection..ctor(IEnumerable`1 xmlReaders, IEnumerable`1 filePaths)
   at System.Data.Metadata.Edm.MetadataCache.StoreMetadataEntry.LoadStoreCollection(EdmItemCollection edmItemCollection, MetadataArtifactLoader loader)
   at System.Data.Metadata.Edm.MetadataCache.StoreItemCollectionLoader.LoadItemCollection(StoreMetadataEntry entry)
   at System.Data.Metadata.Edm.MetadataCache.LoadItemCollection[T](IItemCollectionLoader`1 itemCollectionLoader, T entry)
   at System.Data.Metadata.Edm.MetadataCache.GetOrCreateStoreAndMappingItemCollections(String cacheKey, MetadataArtifactLoader loader, EdmItemCollection edmItemCollection, Object& entryToken)
   at System.Data.EntityClient.EntityConnection.LoadStoreItemCollections(MetadataWorkspace workspace, DbConnection storeConnection, DbProviderFactory factory, DbConnectionOptions connectionOptions, EdmItemCollection edmItemCollection, MetadataArtifactLoader artifactLoader)
   at System.Data.EntityClient.EntityConnection.GetMetadataWorkspace(Boolean initializeAllCollections)
   at System.Data.EntityClient.EntityConnection.InitializeMetadata(DbConnection newConnection, DbConnection originalConnection, Boolean closeOriginalConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 785
Other Text: iCommon

Function: SendMail
Datetime: 31/07/2017 9:05:03 AM
Error message: Schema specified is not valid. Errors: 
CommonContext.ssdl(7856,6) : error 0075: Key Part: 'ID' for type HU_WELFARE_LIST_GW is not valid. All parts of the key must be non nullable.
CommonContext.ssdl(10734,6) : error 0075: Key Part: 'ID' for type RC_PROGRAM_SCHEDULE is not valid. All parts of the key must be non nullable.
CommonContext.ssdl(11113,6) : error 0075: Key Part: 'ID' for type SE_ACCESS_LOG is not valid. All parts of the key must be non nullable.
Stack trace:    at System.Data.Metadata.Edm.StoreItemCollection.Loader.ThrowOnNonWarningErrors()
   at System.Data.Metadata.Edm.StoreItemCollection.Loader.LoadItems(IEnumerable`1 xmlReaders, IEnumerable`1 sourceFilePaths)
   at System.Data.Metadata.Edm.StoreItemCollection.Init(IEnumerable`1 xmlReaders, IEnumerable`1 filePaths, Boolean throwOnError, DbProviderManifest& providerManifest, DbProviderFactory& providerFactory, String& providerManifestToken, Memoizer`2& cachedCTypeFunction)
   at System.Data.Metadata.Edm.StoreItemCollection..ctor(IEnumerable`1 xmlReaders, IEnumerable`1 filePaths)
   at System.Data.Metadata.Edm.MetadataCache.StoreMetadataEntry.LoadStoreCollection(EdmItemCollection edmItemCollection, MetadataArtifactLoader loader)
   at System.Data.Metadata.Edm.MetadataCache.StoreItemCollectionLoader.LoadItemCollection(StoreMetadataEntry entry)
   at System.Data.Metadata.Edm.MetadataCache.LoadItemCollection[T](IItemCollectionLoader`1 itemCollectionLoader, T entry)
   at System.Data.Metadata.Edm.MetadataCache.GetOrCreateStoreAndMappingItemCollections(String cacheKey, MetadataArtifactLoader loader, EdmItemCollection edmItemCollection, Object& entryToken)
   at System.Data.EntityClient.EntityConnection.LoadStoreItemCollections(MetadataWorkspace workspace, DbConnection storeConnection, DbProviderFactory factory, DbConnectionOptions connectionOptions, EdmItemCollection edmItemCollection, MetadataArtifactLoader artifactLoader)
   at System.Data.EntityClient.EntityConnection.GetMetadataWorkspace(Boolean initializeAllCollections)
   at System.Data.EntityClient.EntityConnection.InitializeMetadata(DbConnection newConnection, DbConnection originalConnection, Boolean closeOriginalConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 785
Other Text: iCommon

Function: SendMail
Datetime: 31/07/2017 9:08:53 AM
Error message: Schema specified is not valid. Errors: 
CommonContext.ssdl(7856,6) : error 0075: Key Part: 'ID' for type HU_WELFARE_LIST_GW is not valid. All parts of the key must be non nullable.
CommonContext.ssdl(10734,6) : error 0075: Key Part: 'ID' for type RC_PROGRAM_SCHEDULE is not valid. All parts of the key must be non nullable.
CommonContext.ssdl(11113,6) : error 0075: Key Part: 'ID' for type SE_ACCESS_LOG is not valid. All parts of the key must be non nullable.
Stack trace:    at System.Data.Metadata.Edm.StoreItemCollection.Loader.ThrowOnNonWarningErrors()
   at System.Data.Metadata.Edm.StoreItemCollection.Loader.LoadItems(IEnumerable`1 xmlReaders, IEnumerable`1 sourceFilePaths)
   at System.Data.Metadata.Edm.StoreItemCollection.Init(IEnumerable`1 xmlReaders, IEnumerable`1 filePaths, Boolean throwOnError, DbProviderManifest& providerManifest, DbProviderFactory& providerFactory, String& providerManifestToken, Memoizer`2& cachedCTypeFunction)
   at System.Data.Metadata.Edm.StoreItemCollection..ctor(IEnumerable`1 xmlReaders, IEnumerable`1 filePaths)
   at System.Data.Metadata.Edm.MetadataCache.StoreMetadataEntry.LoadStoreCollection(EdmItemCollection edmItemCollection, MetadataArtifactLoader loader)
   at System.Data.Metadata.Edm.MetadataCache.StoreItemCollectionLoader.LoadItemCollection(StoreMetadataEntry entry)
   at System.Data.Metadata.Edm.MetadataCache.LoadItemCollection[T](IItemCollectionLoader`1 itemCollectionLoader, T entry)
   at System.Data.Metadata.Edm.MetadataCache.GetOrCreateStoreAndMappingItemCollections(String cacheKey, MetadataArtifactLoader loader, EdmItemCollection edmItemCollection, Object& entryToken)
   at System.Data.EntityClient.EntityConnection.LoadStoreItemCollections(MetadataWorkspace workspace, DbConnection storeConnection, DbProviderFactory factory, DbConnectionOptions connectionOptions, EdmItemCollection edmItemCollection, MetadataArtifactLoader artifactLoader)
   at System.Data.EntityClient.EntityConnection.GetMetadataWorkspace(Boolean initializeAllCollections)
   at System.Data.EntityClient.EntityConnection.InitializeMetadata(DbConnection newConnection, DbConnection originalConnection, Boolean closeOriginalConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 785
Other Text: iCommon

Function: SendMail
Datetime: 31/07/2017 9:12:50 AM
Error message: An error occurred while preparing the command definition. See the inner exception for details.
Stack trace:    at System.Data.EntityClient.EntityCommandDefinition..ctor(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
   at System.Data.EntityClient.EntityProviderServices.CreateCommandDefinition(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
   at System.Data.EntityClient.EntityProviderServices.CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
   at System.Data.Common.DbProviderServices.CreateCommandDefinition(DbCommandTree commandTree)
   at System.Data.Objects.Internal.ObjectQueryExecutionPlan.Prepare(ObjectContext context, DbQueryCommandTree tree, Type elementType, MergeOption mergeOption, Span span, ReadOnlyCollection`1 compiledQueryParameters, AliasGenerator aliasGenerator)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 785
Inner error message: 
CommonContext.msl(3,4) : error 3027: No mapping specified for the following EntitySet/AssociationSet - SE_GROUP, SE_FUNCTION, SE_USER, SE_MODULE, SE_GROUP_PERMISSION, OT_OTHER_LIST, OT_OTHER_LIST_GROUP, OT_OTHER_LIST_TYPE, SE_REPORT, HU_ORGANIZATION, SE_FUNCTION_GROUP, HU_EMPLOYEE, SE_MAIL, SE_REMINDER, HU_TITLE, SE_APP_PROCESS, SE_APP_SETUP, SE_APP_SETUPEXT, SE_APP_TEMPLATE, SE_APP_TEMPLATE_DTL, HU_WORKING, SE_CHOSEN_ORG, HU_EMPLOYEE_CV, HU_STAFF_RANK, SE_USER_ORG_ACCESS, SE_USER_PERMISSION, SE_GRP_SE_USR, SE_GROUP_REPORT, SE_USER_REPORT.
Inner stack trace:    at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.SerializedGenerateViews(StorageEntityContainerMapping entityContainerMap, Dictionary`2 resultDictionary)
   at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.SerializedGetGeneratedViews(EntityContainer container)
   at System.Data.Common.Utils.Memoizer`2.<>c__DisplayClass4_0.<Evaluate>b__0()
   at System.Data.Common.Utils.Memoizer`2.Result.GetValue()
   at System.Data.Common.Utils.Memoizer`2.Evaluate(TArg arg)
   at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.GetGeneratedView(EntitySetBase extent, MetadataWorkspace workspace, StorageMappingItemCollection storageMappingItemCollection)
   at System.Data.Metadata.Edm.MetadataWorkspace.GetGeneratedView(EntitySetBase extent)
   at System.Data.Query.PlanCompiler.PreProcessor.ExpandView(Node node, ScanTableOp scanTableOp, IsOfOp& typeFilter)
   at System.Data.Query.PlanCompiler.PreProcessor.ProcessScanTable(Node scanTableNode, ScanTableOp scanTableOp, IsOfOp& typeFilter)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(ScanTableOp op, Node n)
   at System.Data.Query.InternalTrees.ScanTableOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(FilterOp op, Node n)
   at System.Data.Query.InternalTrees.FilterOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(ProjectOp op, Node n)
   at System.Data.Query.InternalTrees.ProjectOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitSortOp(SortBaseOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.Visit(ConstrainedSortOp op, Node n)
   at System.Data.Query.InternalTrees.ConstrainedSortOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfNode.VisitDefault(Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfNode.VisitPhysicalOpDefault(PhysicalOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.Visit(PhysicalProjectOp op, Node n)
   at System.Data.Query.InternalTrees.PhysicalProjectOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Process(Dictionary`2& tvfResultKeys)
   at System.Data.Query.PlanCompiler.PreProcessor.Process(PlanCompiler planCompilerState, StructuredTypeInfo& typeInfo, Dictionary`2& tvfResultKeys)
   at System.Data.Query.PlanCompiler.PlanCompiler.Compile(List`1& providerCommands, ColumnMap& resultColumnMap, Int32& columnCount, Set`1& entitySets)
   at System.Data.Query.PlanCompiler.PlanCompiler.Compile(DbCommandTree ctree, List`1& providerCommands, ColumnMap& resultColumnMap, Int32& columnCount, Set`1& entitySets)
   at System.Data.EntityClient.EntityCommandDefinition..ctor(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
Other Text: iCommon

Function: SendMail
Datetime: 31/07/2017 9:13:49 AM
Error message: An error occurred while preparing the command definition. See the inner exception for details.
Stack trace:    at System.Data.EntityClient.EntityCommandDefinition..ctor(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
   at System.Data.EntityClient.EntityProviderServices.CreateCommandDefinition(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
   at System.Data.EntityClient.EntityProviderServices.CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
   at System.Data.Common.DbProviderServices.CreateCommandDefinition(DbCommandTree commandTree)
   at System.Data.Objects.Internal.ObjectQueryExecutionPlan.Prepare(ObjectContext context, DbQueryCommandTree tree, Type elementType, MergeOption mergeOption, Span span, ReadOnlyCollection`1 compiledQueryParameters, AliasGenerator aliasGenerator)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 785
Inner error message: 
CommonContext.msl(3,4) : error 3027: No mapping specified for the following EntitySet/AssociationSet - SE_GROUP, SE_FUNCTION, SE_USER, SE_MODULE, SE_GROUP_PERMISSION, OT_OTHER_LIST, OT_OTHER_LIST_GROUP, OT_OTHER_LIST_TYPE, SE_REPORT, HU_ORGANIZATION, SE_FUNCTION_GROUP, HU_EMPLOYEE, SE_MAIL, SE_REMINDER, HU_TITLE, SE_APP_PROCESS, SE_APP_SETUP, SE_APP_SETUPEXT, SE_APP_TEMPLATE, SE_APP_TEMPLATE_DTL, HU_WORKING, SE_CHOSEN_ORG, HU_EMPLOYEE_CV, HU_STAFF_RANK, SE_USER_ORG_ACCESS, SE_USER_PERMISSION, SE_GRP_SE_USR, SE_GROUP_REPORT, SE_USER_REPORT.
Inner stack trace:    at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.SerializedGenerateViews(StorageEntityContainerMapping entityContainerMap, Dictionary`2 resultDictionary)
   at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.SerializedGetGeneratedViews(EntityContainer container)
   at System.Data.Common.Utils.Memoizer`2.<>c__DisplayClass4_0.<Evaluate>b__0()
   at System.Data.Common.Utils.Memoizer`2.Result.GetValue()
   at System.Data.Common.Utils.Memoizer`2.Evaluate(TArg arg)
   at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.GetGeneratedView(EntitySetBase extent, MetadataWorkspace workspace, StorageMappingItemCollection storageMappingItemCollection)
   at System.Data.Metadata.Edm.MetadataWorkspace.GetGeneratedView(EntitySetBase extent)
   at System.Data.Query.PlanCompiler.PreProcessor.ExpandView(Node node, ScanTableOp scanTableOp, IsOfOp& typeFilter)
   at System.Data.Query.PlanCompiler.PreProcessor.ProcessScanTable(Node scanTableNode, ScanTableOp scanTableOp, IsOfOp& typeFilter)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(ScanTableOp op, Node n)
   at System.Data.Query.InternalTrees.ScanTableOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(FilterOp op, Node n)
   at System.Data.Query.InternalTrees.FilterOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(ProjectOp op, Node n)
   at System.Data.Query.InternalTrees.ProjectOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitSortOp(SortBaseOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.Visit(ConstrainedSortOp op, Node n)
   at System.Data.Query.InternalTrees.ConstrainedSortOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfNode.VisitDefault(Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfNode.VisitPhysicalOpDefault(PhysicalOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.Visit(PhysicalProjectOp op, Node n)
   at System.Data.Query.InternalTrees.PhysicalProjectOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Process(Dictionary`2& tvfResultKeys)
   at System.Data.Query.PlanCompiler.PreProcessor.Process(PlanCompiler planCompilerState, StructuredTypeInfo& typeInfo, Dictionary`2& tvfResultKeys)
   at System.Data.Query.PlanCompiler.PlanCompiler.Compile(List`1& providerCommands, ColumnMap& resultColumnMap, Int32& columnCount, Set`1& entitySets)
   at System.Data.Query.PlanCompiler.PlanCompiler.Compile(DbCommandTree ctree, List`1& providerCommands, ColumnMap& resultColumnMap, Int32& columnCount, Set`1& entitySets)
   at System.Data.EntityClient.EntityCommandDefinition..ctor(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
Other Text: iCommon

Function: SendMail
Datetime: 31/07/2017 9:14:49 AM
Error message: An error occurred while preparing the command definition. See the inner exception for details.
Stack trace:    at System.Data.EntityClient.EntityCommandDefinition..ctor(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
   at System.Data.EntityClient.EntityProviderServices.CreateCommandDefinition(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
   at System.Data.EntityClient.EntityProviderServices.CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
   at System.Data.Common.DbProviderServices.CreateCommandDefinition(DbCommandTree commandTree)
   at System.Data.Objects.Internal.ObjectQueryExecutionPlan.Prepare(ObjectContext context, DbQueryCommandTree tree, Type elementType, MergeOption mergeOption, Span span, ReadOnlyCollection`1 compiledQueryParameters, AliasGenerator aliasGenerator)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 785
Inner error message: 
CommonContext.msl(3,4) : error 3027: No mapping specified for the following EntitySet/AssociationSet - SE_GROUP, SE_FUNCTION, SE_USER, SE_MODULE, SE_GROUP_PERMISSION, OT_OTHER_LIST, OT_OTHER_LIST_GROUP, OT_OTHER_LIST_TYPE, SE_REPORT, HU_ORGANIZATION, SE_FUNCTION_GROUP, HU_EMPLOYEE, SE_MAIL, SE_REMINDER, HU_TITLE, SE_APP_PROCESS, SE_APP_SETUP, SE_APP_SETUPEXT, SE_APP_TEMPLATE, SE_APP_TEMPLATE_DTL, HU_WORKING, SE_CHOSEN_ORG, HU_EMPLOYEE_CV, HU_STAFF_RANK, SE_USER_ORG_ACCESS, SE_USER_PERMISSION, SE_GRP_SE_USR, SE_GROUP_REPORT, SE_USER_REPORT.
Inner stack trace:    at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.SerializedGenerateViews(StorageEntityContainerMapping entityContainerMap, Dictionary`2 resultDictionary)
   at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.SerializedGetGeneratedViews(EntityContainer container)
   at System.Data.Common.Utils.Memoizer`2.<>c__DisplayClass4_0.<Evaluate>b__0()
   at System.Data.Common.Utils.Memoizer`2.Result.GetValue()
   at System.Data.Common.Utils.Memoizer`2.Evaluate(TArg arg)
   at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.GetGeneratedView(EntitySetBase extent, MetadataWorkspace workspace, StorageMappingItemCollection storageMappingItemCollection)
   at System.Data.Metadata.Edm.MetadataWorkspace.GetGeneratedView(EntitySetBase extent)
   at System.Data.Query.PlanCompiler.PreProcessor.ExpandView(Node node, ScanTableOp scanTableOp, IsOfOp& typeFilter)
   at System.Data.Query.PlanCompiler.PreProcessor.ProcessScanTable(Node scanTableNode, ScanTableOp scanTableOp, IsOfOp& typeFilter)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(ScanTableOp op, Node n)
   at System.Data.Query.InternalTrees.ScanTableOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(FilterOp op, Node n)
   at System.Data.Query.InternalTrees.FilterOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(ProjectOp op, Node n)
   at System.Data.Query.InternalTrees.ProjectOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitSortOp(SortBaseOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.Visit(ConstrainedSortOp op, Node n)
   at System.Data.Query.InternalTrees.ConstrainedSortOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfNode.VisitDefault(Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfNode.VisitPhysicalOpDefault(PhysicalOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.Visit(PhysicalProjectOp op, Node n)
   at System.Data.Query.InternalTrees.PhysicalProjectOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Process(Dictionary`2& tvfResultKeys)
   at System.Data.Query.PlanCompiler.PreProcessor.Process(PlanCompiler planCompilerState, StructuredTypeInfo& typeInfo, Dictionary`2& tvfResultKeys)
   at System.Data.Query.PlanCompiler.PlanCompiler.Compile(List`1& providerCommands, ColumnMap& resultColumnMap, Int32& columnCount, Set`1& entitySets)
   at System.Data.Query.PlanCompiler.PlanCompiler.Compile(DbCommandTree ctree, List`1& providerCommands, ColumnMap& resultColumnMap, Int32& columnCount, Set`1& entitySets)
   at System.Data.EntityClient.EntityCommandDefinition..ctor(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
Other Text: iCommon

Function: SendMail
Datetime: 31/07/2017 9:15:49 AM
Error message: An error occurred while preparing the command definition. See the inner exception for details.
Stack trace:    at System.Data.EntityClient.EntityCommandDefinition..ctor(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
   at System.Data.EntityClient.EntityProviderServices.CreateCommandDefinition(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
   at System.Data.EntityClient.EntityProviderServices.CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
   at System.Data.Common.DbProviderServices.CreateCommandDefinition(DbCommandTree commandTree)
   at System.Data.Objects.Internal.ObjectQueryExecutionPlan.Prepare(ObjectContext context, DbQueryCommandTree tree, Type elementType, MergeOption mergeOption, Span span, ReadOnlyCollection`1 compiledQueryParameters, AliasGenerator aliasGenerator)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 785
Inner error message: 
CommonContext.msl(3,4) : error 3027: No mapping specified for the following EntitySet/AssociationSet - SE_GROUP, SE_FUNCTION, SE_USER, SE_MODULE, SE_GROUP_PERMISSION, OT_OTHER_LIST, OT_OTHER_LIST_GROUP, OT_OTHER_LIST_TYPE, SE_REPORT, HU_ORGANIZATION, SE_FUNCTION_GROUP, HU_EMPLOYEE, SE_MAIL, SE_REMINDER, HU_TITLE, SE_APP_PROCESS, SE_APP_SETUP, SE_APP_SETUPEXT, SE_APP_TEMPLATE, SE_APP_TEMPLATE_DTL, HU_WORKING, SE_CHOSEN_ORG, HU_EMPLOYEE_CV, HU_STAFF_RANK, SE_USER_ORG_ACCESS, SE_USER_PERMISSION, SE_GRP_SE_USR, SE_GROUP_REPORT, SE_USER_REPORT.
Inner stack trace:    at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.SerializedGenerateViews(StorageEntityContainerMapping entityContainerMap, Dictionary`2 resultDictionary)
   at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.SerializedGetGeneratedViews(EntityContainer container)
   at System.Data.Common.Utils.Memoizer`2.<>c__DisplayClass4_0.<Evaluate>b__0()
   at System.Data.Common.Utils.Memoizer`2.Result.GetValue()
   at System.Data.Common.Utils.Memoizer`2.Evaluate(TArg arg)
   at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.GetGeneratedView(EntitySetBase extent, MetadataWorkspace workspace, StorageMappingItemCollection storageMappingItemCollection)
   at System.Data.Metadata.Edm.MetadataWorkspace.GetGeneratedView(EntitySetBase extent)
   at System.Data.Query.PlanCompiler.PreProcessor.ExpandView(Node node, ScanTableOp scanTableOp, IsOfOp& typeFilter)
   at System.Data.Query.PlanCompiler.PreProcessor.ProcessScanTable(Node scanTableNode, ScanTableOp scanTableOp, IsOfOp& typeFilter)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(ScanTableOp op, Node n)
   at System.Data.Query.InternalTrees.ScanTableOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(FilterOp op, Node n)
   at System.Data.Query.InternalTrees.FilterOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(ProjectOp op, Node n)
   at System.Data.Query.InternalTrees.ProjectOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitSortOp(SortBaseOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.Visit(ConstrainedSortOp op, Node n)
   at System.Data.Query.InternalTrees.ConstrainedSortOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfNode.VisitDefault(Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfNode.VisitPhysicalOpDefault(PhysicalOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.Visit(PhysicalProjectOp op, Node n)
   at System.Data.Query.InternalTrees.PhysicalProjectOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Process(Dictionary`2& tvfResultKeys)
   at System.Data.Query.PlanCompiler.PreProcessor.Process(PlanCompiler planCompilerState, StructuredTypeInfo& typeInfo, Dictionary`2& tvfResultKeys)
   at System.Data.Query.PlanCompiler.PlanCompiler.Compile(List`1& providerCommands, ColumnMap& resultColumnMap, Int32& columnCount, Set`1& entitySets)
   at System.Data.Query.PlanCompiler.PlanCompiler.Compile(DbCommandTree ctree, List`1& providerCommands, ColumnMap& resultColumnMap, Int32& columnCount, Set`1& entitySets)
   at System.Data.EntityClient.EntityCommandDefinition..ctor(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
Other Text: iCommon

Function: SendMail
Datetime: 31/07/2017 9:16:49 AM
Error message: An error occurred while preparing the command definition. See the inner exception for details.
Stack trace:    at System.Data.EntityClient.EntityCommandDefinition..ctor(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
   at System.Data.EntityClient.EntityProviderServices.CreateCommandDefinition(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
   at System.Data.EntityClient.EntityProviderServices.CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
   at System.Data.Common.DbProviderServices.CreateCommandDefinition(DbCommandTree commandTree)
   at System.Data.Objects.Internal.ObjectQueryExecutionPlan.Prepare(ObjectContext context, DbQueryCommandTree tree, Type elementType, MergeOption mergeOption, Span span, ReadOnlyCollection`1 compiledQueryParameters, AliasGenerator aliasGenerator)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 785
Inner error message: 
CommonContext.msl(3,4) : error 3027: No mapping specified for the following EntitySet/AssociationSet - SE_GROUP, SE_FUNCTION, SE_USER, SE_MODULE, SE_GROUP_PERMISSION, OT_OTHER_LIST, OT_OTHER_LIST_GROUP, OT_OTHER_LIST_TYPE, SE_REPORT, HU_ORGANIZATION, SE_FUNCTION_GROUP, HU_EMPLOYEE, SE_MAIL, SE_REMINDER, HU_TITLE, SE_APP_PROCESS, SE_APP_SETUP, SE_APP_SETUPEXT, SE_APP_TEMPLATE, SE_APP_TEMPLATE_DTL, HU_WORKING, SE_CHOSEN_ORG, HU_EMPLOYEE_CV, HU_STAFF_RANK, SE_USER_ORG_ACCESS, SE_USER_PERMISSION, SE_GRP_SE_USR, SE_GROUP_REPORT, SE_USER_REPORT.
Inner stack trace:    at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.SerializedGenerateViews(StorageEntityContainerMapping entityContainerMap, Dictionary`2 resultDictionary)
   at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.SerializedGetGeneratedViews(EntityContainer container)
   at System.Data.Common.Utils.Memoizer`2.<>c__DisplayClass4_0.<Evaluate>b__0()
   at System.Data.Common.Utils.Memoizer`2.Result.GetValue()
   at System.Data.Common.Utils.Memoizer`2.Evaluate(TArg arg)
   at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.GetGeneratedView(EntitySetBase extent, MetadataWorkspace workspace, StorageMappingItemCollection storageMappingItemCollection)
   at System.Data.Metadata.Edm.MetadataWorkspace.GetGeneratedView(EntitySetBase extent)
   at System.Data.Query.PlanCompiler.PreProcessor.ExpandView(Node node, ScanTableOp scanTableOp, IsOfOp& typeFilter)
   at System.Data.Query.PlanCompiler.PreProcessor.ProcessScanTable(Node scanTableNode, ScanTableOp scanTableOp, IsOfOp& typeFilter)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(ScanTableOp op, Node n)
   at System.Data.Query.InternalTrees.ScanTableOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(FilterOp op, Node n)
   at System.Data.Query.InternalTrees.FilterOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(ProjectOp op, Node n)
   at System.Data.Query.InternalTrees.ProjectOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitSortOp(SortBaseOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.Visit(ConstrainedSortOp op, Node n)
   at System.Data.Query.InternalTrees.ConstrainedSortOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfNode.VisitDefault(Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfNode.VisitPhysicalOpDefault(PhysicalOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.Visit(PhysicalProjectOp op, Node n)
   at System.Data.Query.InternalTrees.PhysicalProjectOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Process(Dictionary`2& tvfResultKeys)
   at System.Data.Query.PlanCompiler.PreProcessor.Process(PlanCompiler planCompilerState, StructuredTypeInfo& typeInfo, Dictionary`2& tvfResultKeys)
   at System.Data.Query.PlanCompiler.PlanCompiler.Compile(List`1& providerCommands, ColumnMap& resultColumnMap, Int32& columnCount, Set`1& entitySets)
   at System.Data.Query.PlanCompiler.PlanCompiler.Compile(DbCommandTree ctree, List`1& providerCommands, ColumnMap& resultColumnMap, Int32& columnCount, Set`1& entitySets)
   at System.Data.EntityClient.EntityCommandDefinition..ctor(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
Other Text: iCommon

Function: SendMail
Datetime: 31/07/2017 9:17:49 AM
Error message: An error occurred while preparing the command definition. See the inner exception for details.
Stack trace:    at System.Data.EntityClient.EntityCommandDefinition..ctor(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
   at System.Data.EntityClient.EntityProviderServices.CreateCommandDefinition(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
   at System.Data.EntityClient.EntityProviderServices.CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
   at System.Data.Common.DbProviderServices.CreateCommandDefinition(DbCommandTree commandTree)
   at System.Data.Objects.Internal.ObjectQueryExecutionPlan.Prepare(ObjectContext context, DbQueryCommandTree tree, Type elementType, MergeOption mergeOption, Span span, ReadOnlyCollection`1 compiledQueryParameters, AliasGenerator aliasGenerator)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\CommonDAL\CommonRepository.vb:line 785
Inner error message: 
CommonContext.msl(3,4) : error 3027: No mapping specified for the following EntitySet/AssociationSet - SE_GROUP, SE_FUNCTION, SE_USER, SE_MODULE, SE_GROUP_PERMISSION, OT_OTHER_LIST, OT_OTHER_LIST_GROUP, OT_OTHER_LIST_TYPE, SE_REPORT, HU_ORGANIZATION, SE_FUNCTION_GROUP, HU_EMPLOYEE, SE_MAIL, SE_REMINDER, HU_TITLE, SE_APP_PROCESS, SE_APP_SETUP, SE_APP_SETUPEXT, SE_APP_TEMPLATE, SE_APP_TEMPLATE_DTL, HU_WORKING, SE_CHOSEN_ORG, HU_EMPLOYEE_CV, HU_STAFF_RANK, SE_USER_ORG_ACCESS, SE_USER_PERMISSION, SE_GRP_SE_USR, SE_GROUP_REPORT, SE_USER_REPORT.
Inner stack trace:    at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.SerializedGenerateViews(StorageEntityContainerMapping entityContainerMap, Dictionary`2 resultDictionary)
   at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.SerializedGetGeneratedViews(EntityContainer container)
   at System.Data.Common.Utils.Memoizer`2.<>c__DisplayClass4_0.<Evaluate>b__0()
   at System.Data.Common.Utils.Memoizer`2.Result.GetValue()
   at System.Data.Common.Utils.Memoizer`2.Evaluate(TArg arg)
   at System.Data.Mapping.StorageMappingItemCollection.ViewDictionary.GetGeneratedView(EntitySetBase extent, MetadataWorkspace workspace, StorageMappingItemCollection storageMappingItemCollection)
   at System.Data.Metadata.Edm.MetadataWorkspace.GetGeneratedView(EntitySetBase extent)
   at System.Data.Query.PlanCompiler.PreProcessor.ExpandView(Node node, ScanTableOp scanTableOp, IsOfOp& typeFilter)
   at System.Data.Query.PlanCompiler.PreProcessor.ProcessScanTable(Node scanTableNode, ScanTableOp scanTableOp, IsOfOp& typeFilter)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(ScanTableOp op, Node n)
   at System.Data.Query.InternalTrees.ScanTableOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(FilterOp op, Node n)
   at System.Data.Query.InternalTrees.FilterOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Visit(ProjectOp op, Node n)
   at System.Data.Query.InternalTrees.ProjectOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitRelOpDefault(RelOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitSortOp(SortBaseOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.Visit(ConstrainedSortOp op, Node n)
   at System.Data.Query.InternalTrees.ConstrainedSortOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.SubqueryTrackingVisitor.VisitChildren(Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfNode.VisitDefault(Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfNode.VisitPhysicalOpDefault(PhysicalOp op, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.Visit(PhysicalProjectOp op, Node n)
   at System.Data.Query.InternalTrees.PhysicalProjectOp.Accept[TResultType](BasicOpVisitorOfT`1 v, Node n)
   at System.Data.Query.InternalTrees.BasicOpVisitorOfT`1.VisitNode(Node n)
   at System.Data.Query.PlanCompiler.PreProcessor.Process(Dictionary`2& tvfResultKeys)
   at System.Data.Query.PlanCompiler.PreProcessor.Process(PlanCompiler planCompilerState, StructuredTypeInfo& typeInfo, Dictionary`2& tvfResultKeys)
   at System.Data.Query.PlanCompiler.PlanCompiler.Compile(List`1& providerCommands, ColumnMap& resultColumnMap, Int32& columnCount, Set`1& entitySets)
   at System.Data.Query.PlanCompiler.PlanCompiler.Compile(DbCommandTree ctree, List`1& providerCommands, ColumnMap& resultColumnMap, Int32& columnCount, Set`1& entitySets)
   at System.Data.EntityClient.EntityCommandDefinition..ctor(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
Other Text: iCommon


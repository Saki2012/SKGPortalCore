using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.SKGPortalCore.Business.MasterData;
using System.Runtime.InteropServices;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 代收類別庫
    /// </summary>
    [ProgId(SystemCP.ProgId_CollectionType)]
    public class CollectionTypeRepository : BasicRepository<CollectionTypeSet>
    {
        #region Construct
        public CollectionTypeRepository(ApplicationDbContext dataAccess) : base(dataAccess) { }
        #endregion

        #region Protected
        protected override void AfterSetEntity(CollectionTypeSet set, FuncAction action)
        {
            base.AfterSetEntity(set, action);
            BizCollectionType.CheckData(set, Message);
            BizCollectionType.SetData(set);
        }
        #endregion
    }
}

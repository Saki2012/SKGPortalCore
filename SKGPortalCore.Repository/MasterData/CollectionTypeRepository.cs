﻿using System.Runtime.InteropServices;
using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Repository.Entity;
using SKGPortalCore.Interface.IRepository.MasterData;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Repository.SKGPortalCore.Business.MasterData;

namespace SKGPortalCore.Repository.MasterData
{
    /// <summary>
    /// 代收類別庫
    /// </summary>
    [ProgId(SystemCP.ProgId_CollectionType)]
    public class CollectionTypeRepository : BasicRepository<CollectionTypeSet>, ICollectionTypeRepository
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

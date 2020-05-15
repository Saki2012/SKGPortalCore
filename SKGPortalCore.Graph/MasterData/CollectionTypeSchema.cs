using SKGPortalCore.Core;
using SKGPortalCore.Core.GraphQL;
using SKGPortalCore.Interface.IGraphQL.MasterData;
using SKGPortalCore.Interface.IRepository.MasterData;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Graph.MasterData
{
    //Schema
    public class CollectionTypeSchema : BaseSchema<CollectionTypeSet, CollectionTypeQuery, CollectionTypeMutation>, ICollectionTypeSchema
    {
        public CollectionTypeSchema(ICollectionTypeRepository repo, ISessionWrapper session) : base(repo, session) { }
    }
    //Operate
    public class CollectionTypeQuery : BaseQueryType<CollectionTypeSet, CollectionTypeSetType, CollectionTypeType>
    {
        public CollectionTypeQuery(ICollectionTypeRepository repository, ISessionWrapper session) : base(repository, session) { }
    }
    public class CollectionTypeMutation : BaseMutationType<CollectionTypeSet, CollectionTypeSetType, CollectionTypeSetInputType>
    {
        public CollectionTypeMutation(ICollectionTypeRepository repository, ISessionWrapper session) : base(repository, session) { }
    }
    //Input
    public class CollectionTypeSetInputType : BaseInputSetGraphType<CollectionTypeSet> { }
    public class CollectionTypeInputType : BaseInputFieldGraphType<CollectionTypeModel> { }
    public class CollectionTypeDetailInputType : BaseInputFieldGraphType<CollectionTypeDetailModel> { }
    public class CollectionTypeVerifyPeriodInputType : BaseInputFieldGraphType<CollectionTypeVerifyPeriodModel> { }

    //Query
    public class CollectionTypeSetType : BaseQuerySetGraphType<CollectionTypeSet> { }
    public class CollectionTypeType : BaseQueryFieldGraphType<CollectionTypeModel> { }
    public class CollectionTypeDetailType : BaseQueryFieldGraphType<CollectionTypeDetailModel> { }
    public class CollectionTypeVerifyPeriodType : BaseQueryFieldGraphType<CollectionTypeVerifyPeriodModel> { }


}

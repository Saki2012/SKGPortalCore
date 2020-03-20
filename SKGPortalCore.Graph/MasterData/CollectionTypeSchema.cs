using GraphQL;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.Graph.MasterData
{
    //Schema
    public class CollectionTypeSchema : BaseSchema<CollectionTypeQuery, CollectionTypeMutation>
    {
        public CollectionTypeSchema(IDependencyResolver resolver) : base(resolver) { }
    }
    //Operate
    public class CollectionTypeQuery : BaseQueryType<CollectionTypeSet, CollectionTypeSetType, CollectionTypeType>
    {
        public CollectionTypeQuery(CollectionTypeRepository repository, ISessionWrapper session) : base(repository, session) { }
    }
    public class CollectionTypeMutation : BaseMutationType<CollectionTypeSet, CollectionTypeSetType, CollectionTypeSetInputType>
    {
        public CollectionTypeMutation(CollectionTypeRepository repository, ISessionWrapper session) : base(repository, session) { }
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

using GraphQL;
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
    public class CollectionTypeQuery : BaseQueryType<CollectionTypeSet, CollectionTypeSetType>
    {
        public CollectionTypeQuery(CollectionTypeRepository repository) : base(repository) { }
    }
    public class CollectionTypeMutation : BaseMutationType<CollectionTypeSet, CollectionTypeSetType, CollectionTypeSetInputType>
    {
        public CollectionTypeMutation(CollectionTypeRepository repository) : base(repository) { }
    }
    //Input
    public class CollectionTypeSetInputType : BaseInputSetGraphType<CollectionTypeSet> { }
    public class CollectionTypeInputType : BaseInputFieldGraphType<CollectionTypeModel> { }
    //Query
    public class CollectionTypeSetType : BaseQuerySetGraphType<CollectionTypeSet> { }
    public class CollectionTypeType : BaseQueryFieldGraphType<CollectionTypeModel> { }
}

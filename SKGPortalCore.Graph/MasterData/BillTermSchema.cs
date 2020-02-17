using GraphQL;
using GraphQL.Types;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.Graph.MasterData
{
    //Schema
    public class BillTermSchema : BaseSchema<BillTermQuery, BillTermMutation>
    {
        public BillTermSchema(IDependencyResolver resolver) : base(resolver) { }
    }
    //Operate
    public class BillTermQuery : BaseQueryType<BillTermSet, BillTermSetType>
    {
        public BillTermQuery(BillTermRepository repository) : base(repository) { }
    }
    public class BillTermMutation : BaseMutationType<BillTermSet, BillTermSetType, BillTermSetInputType>
    {
        public BillTermMutation(BillTermRepository repository) : base(repository) { }
    }
    //Input
    public class BillTermSetInputType : BaseInputSetGraphType<BillTermSet> { }
    public class BillTermInputType : BaseInputFieldGraphType<BillTermModel> { }
    public class BillTermDetailInputType : BaseInputFieldGraphType<BillTermDetailModel> { }
    //Query
    public class BillTermSetType : BaseQuerySetGraphType<BillTermSet> { }
    public class BillTermType : BaseQueryFieldGraphType<BillTermModel> { }
    public class BillTermDetailType : BaseQueryFieldGraphType<BillTermDetailModel> { }
}

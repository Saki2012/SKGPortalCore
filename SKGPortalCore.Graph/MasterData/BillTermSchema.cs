using SKGPortalCore.Core;
using SKGPortalCore.Core.GraphQL;
using SKGPortalCore.Interface.IGraphQL.MasterData;
using SKGPortalCore.Interface.IRepository.MasterData;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Graph.MasterData
{
    //Schema
    public class BillTermSchema : BaseSchema<BillTermSet, BillTermQuery, BillTermMutation>, IBillTermSchema
    {
        public BillTermSchema(IBillTermRepository repo, ISessionWrapper session) : base(repo, session) { }
    }
    //Operate
    public class BillTermQuery : BaseQueryType<BillTermSet, BillTermSetType, BillTermType>
    {
        public BillTermQuery(IBillTermRepository repository, ISessionWrapper session) : base(repository, session) { }
    }
    public class BillTermMutation : BaseMutationType<BillTermSet, BillTermSetType, BillTermSetInputType>
    {
        public BillTermMutation(IBillTermRepository repository, ISessionWrapper session) : base(repository, session) { }
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

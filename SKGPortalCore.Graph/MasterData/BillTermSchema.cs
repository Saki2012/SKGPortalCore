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
    public class BillTermQuery : BaseQueryType<BillTermSet,BillTermSetType>
    {
        public BillTermQuery(BillTermRepository repo) : base(repo)
        {
        }
    }
    public class BillTermMutation : BaseMutationType<BillTermSet, BillTermSetType, BillTermSetInputType>
    {
        public BillTermMutation(BillTermRepository repo) : base(repo)
        {
        }
    }
    //Input
    public class BillTermSetInputType : BaseInputSetGraphType<BillTermSet>
    {
        public BillTermSetInputType()
        {
            Field<BillTermType>(" BillTerm");
            Field<ListGraphType<BillTermDetailType>>(" BillTermDetail");
        }
    }
    public class BillTermInputType : BaseInputFieldGraphType<BillTermModel> { }
    public class BillTermDetailInputType : BaseInputFieldGraphType<BillTermDetailModel> { }
    //Query
    public class BillTermSetType : BaseQuerySetGraphType<BillTermSet>
    {
        public BillTermSetType()
        {
            Field<BillTermType>(" BillTerm");
            Field<ListGraphType<BillTermDetailType>>(" BillTermDetail");
        }
    }
    public class BillTermType : BaseQueryFieldGraphType<BillTermModel> { }
    public class BillTermDetailType : BaseQueryFieldGraphType<BillTermDetailModel> { }
}

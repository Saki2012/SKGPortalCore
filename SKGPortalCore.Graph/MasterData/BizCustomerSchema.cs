using SKGPortalCore.Core;
using SKGPortalCore.Core.GraphQL;
using SKGPortalCore.Interface.IGraphQL.MasterData;
using SKGPortalCore.Interface.IRepository.MasterData;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Graph.MasterData
{
    //Schema
    public class BizCustomerSchema : BaseSchema<BizCustomerSet, BizCustomerQuery, BizCustomerMutation>, IBizCustomerSchema
    {
        public BizCustomerSchema(IBizCustomerRepository repo, ISessionWrapper session) : base(repo, session) { }
    }
    //Operate
    public class BizCustomerQuery : BaseQueryType<BizCustomerSet, BizCustomerSetType, BizCustomerType>
    {
        public BizCustomerQuery(IBizCustomerRepository repository, ISessionWrapper session) : base(repository, session) { }
    }
    public class BizCustomerMutation : BaseMutationType<BizCustomerSet, BizCustomerSetType, BizCustomerSetInputType>
    {
        public BizCustomerMutation(IBizCustomerRepository repository, ISessionWrapper session) : base(repository, session) { }
    }
    //Input
    public class BizCustomerSetInputType : BaseInputSetGraphType<BizCustomerSet> { }
    public class BizCustomerInputType : BaseInputFieldGraphType<BizCustomerModel> { }
    public class BizCustomerFeeDetailInputType : BaseInputFieldGraphType<BizCustomerFeeDetailModel> { }

    //Query
    public class BizCustomerSetType : BaseQuerySetGraphType<BizCustomerSet> { }
    public class BizCustomerType : BaseQueryFieldGraphType<BizCustomerModel> { }
    public class BizCustomerFeeDetailType : BaseQueryFieldGraphType<BizCustomerFeeDetailModel> { }

}

using SKGPortalCore.Core;
using SKGPortalCore.Core.GraphQL;
using SKGPortalCore.Interface.IGraphQL.MasterData;
using SKGPortalCore.Interface.IRepository.MasterData;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Graph.MasterData
{
    //Schema
    public class PayerSchema : BaseSchema<PayerSet, PayerQuery, PayerMutation>, IPayerSchema
    {
        public PayerSchema(IPayerRepository repo, ISessionWrapper session) : base(repo, session) { }
    }
    //Operate
    public class PayerQuery : BaseQueryType<PayerSet, PayerSetType, PayerType>
    {
        public PayerQuery(IPayerRepository repo, ISessionWrapper session) : base(repo, session) { }
    }
    public class PayerMutation : BaseMutationType<PayerSet, PayerSetType, PayerSetInputType>
    {
        public PayerMutation(IPayerRepository repo, ISessionWrapper session) : base(repo, session) { }
    }
    //Input
    public class PayerSetInputType : BaseInputSetGraphType<PayerSet> { }
    public class PayerInputType : BaseInputFieldGraphType<PayerModel> { }
    //Query
    public class PayerSetType : BaseQuerySetGraphType<PayerSet> { }
    public class PayerType : BaseQueryFieldGraphType<PayerModel> { }
}

using GraphQL;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.Graph.MasterData
{
    //Schema
    public class PayerSchema : BaseSchema<PayerQuery, PayerMutation>
    {
        public PayerSchema(IDependencyResolver resolver) : base(resolver) { }
    }
    //Operate
    public class PayerQuery : BaseQueryType<PayerSet, PayerSetType, PayerType>
    {
        public PayerQuery(PayerRepository repository, ISessionWrapper session) : base(repository,session) { }
    }
    public class PayerMutation : BaseMutationType<PayerSet, PayerSetType, PayerSetInputType>
    {
        public PayerMutation(PayerRepository repository) : base(repository) { }
    }
    //Input
    public class PayerSetInputType : BaseInputSetGraphType<PayerSet> { }
    public class PayerInputType : BaseInputFieldGraphType<PayerModel> { }
    //Query
    public class PayerSetType : BaseQuerySetGraphType<PayerSet>    {    }
    public class PayerType : BaseQueryFieldGraphType<PayerModel> { }
}

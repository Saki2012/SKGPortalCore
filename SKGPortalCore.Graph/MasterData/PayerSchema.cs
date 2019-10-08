using GraphQL;
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
    public class PayerQuery : BaseQueryType<PayerSet, PayerSetType>
    {
        public PayerQuery(PayerRepository repository) : base(repository) { }
    }
    public class PayerMutation : BaseMutationType<PayerSet, PayerSetType, PayerSetInputType>
    {
        public PayerMutation(PayerRepository repository) : base(repository) { }
    }
    //Input
    public class PayerSetInputType : BaseInputSetGraphType<PayerSet> { }
    public class PayerInputType : BaseInputFieldGraphType<PayerModel> { }
    //Query
    public class PayerSetType : BaseQuerySetGraphType<PayerSet>
    {
        public PayerSetType()
        {
            Field<PayerType>("Payer");
        }
    }
    public class PayerType : BaseQueryFieldGraphType<PayerModel> { }
}

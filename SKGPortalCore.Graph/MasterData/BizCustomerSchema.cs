using GraphQL;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.Graph.MasterData
{
    //Schema
    public class BizCustomerSchema : BaseSchema<BizCustomerQuery, BizCustomerMutation>
    {
        public BizCustomerSchema(IDependencyResolver resolver) : base(resolver) { }
    }
    //Operate
    public class BizCustomerQuery : BaseQueryType<BizCustomerSet, BizCustomerSetType, BizCustomerType>
    {
        public BizCustomerQuery(BizCustomerRepository repository) : base(repository) { }
    }
    public class BizCustomerMutation : BaseMutationType<BizCustomerSet, BizCustomerSetType, BizCustomerSetInputType>
    {
        public BizCustomerMutation(BizCustomerRepository repository) : base(repository) { }
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

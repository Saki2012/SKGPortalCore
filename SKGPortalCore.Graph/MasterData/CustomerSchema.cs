using GraphQL;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.Graph.MasterData
{
    //Schema
    public class CustomerSchema : BaseSchema<CustomerQuery, CustomerMutation>
    {
        public CustomerSchema(IDependencyResolver resolver) : base(resolver) { }
    }
    //Operate
    public class CustomerQuery : BaseQueryType<CustomerSet, CustomerSetType, CustomerType>
    {
        public CustomerQuery(CustomerRepository repository) : base(repository) { }
    }
    public class CustomerMutation : BaseMutationType<CustomerSet, CustomerSetType, CustomerSetInputType>
    {
        public CustomerMutation(CustomerRepository repository) : base(repository) { }
    }
    //Input
    public class CustomerSetInputType : BaseInputSetGraphType<CustomerSet> { }
    public class CustomerInputType : BaseInputFieldGraphType<CustomerModel> { }
    //Query
    public class CustomerSetType : BaseQuerySetGraphType<CustomerSet> { }
    public class CustomerType : BaseQueryFieldGraphType<CustomerModel> { }
}

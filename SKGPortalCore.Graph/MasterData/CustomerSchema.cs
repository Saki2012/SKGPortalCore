using SKGPortalCore.Core;
using SKGPortalCore.Core.GraphQL;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Interface.IGraphQL.MasterData;
using SKGPortalCore.Interface.IRepository.MasterData;

namespace SKGPortalCore.Graph.MasterData
{
    //Schema
    public class CustomerSchema : BaseSchema<CustomerSet, CustomerQuery, CustomerMutation>, ICustomerSchema
    {
        public CustomerSchema(ICustomerRepository repo, ISessionWrapper session) : base(repo, session) { }
    }
    //Operate
    public class CustomerQuery : BaseQueryType<CustomerSet, CustomerSetType, CustomerType>
    {
        public CustomerQuery(ICustomerRepository repository, ISessionWrapper session) : base(repository, session) { }
    }
    public class CustomerMutation : BaseMutationType<CustomerSet, CustomerSetType, CustomerSetInputType>
    {
        public CustomerMutation(ICustomerRepository repository, ISessionWrapper session) : base(repository, session) { }
    }
    //Input
    public class CustomerSetInputType : BaseInputSetGraphType<CustomerSet> { }
    public class CustomerInputType : BaseInputFieldGraphType<CustomerModel> { }
    //Query
    public class CustomerSetType : BaseQuerySetGraphType<CustomerSet> { }
    public class CustomerType : BaseQueryFieldGraphType<CustomerModel> { }
}

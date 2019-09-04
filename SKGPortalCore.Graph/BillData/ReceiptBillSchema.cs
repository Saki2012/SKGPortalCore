using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Repository.BillData;

namespace SKGPortalCore.Graph.BillData
{
    //Schema
    public class ReceiptBillSchema : BaseSchema<ReceiptBillQuery, ReceiptBillMutation>
    {
        public ReceiptBillSchema(IDependencyResolver resolver) : base(resolver)
        {
        }
    }
    //Operate
    public class ReceiptBillQuery : BaseQueryType<ReceiptBillSet,ReceiptBillSetType>
    {
        public ReceiptBillQuery(ReceiptBillRepository repository) : base(repository) { }
    }
    public class ReceiptBillMutation : BaseMutationType<ReceiptBillSet, ReceiptBillSetType, ReceiptBillSetInputType>
    {
        public ReceiptBillMutation(ReceiptBillRepository repository) : base(repository) { }
    }
    //Input
    public class ReceiptBillSetInputType : BaseInputSetGraphType<ReceiptBillSet> { }
    public class ReceiptBillInputType : BaseInputFieldGraphType<ReceiptBillModel> { }
    //Query
    public class ReceiptBillSetType : BaseQuerySetGraphType<ReceiptBillSet> { }
    public class ReceiptBillType : BaseQueryFieldGraphType<ReceiptBillModel> { }
}

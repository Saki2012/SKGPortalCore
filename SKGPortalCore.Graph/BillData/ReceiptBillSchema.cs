using GraphQL;
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
    public class ReceiptBillQuery : BaseQueryType<ReceiptBillSet, ReceiptBillSetType, ReceiptBillType>
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
    public class ReceiptBillChangeInputType : BaseInputFieldGraphType<ReceiptBillChangeModel> { }
    //Query
    public class ReceiptBillSetType : BaseQuerySetGraphType<ReceiptBillSet> { }
    public class ReceiptBillType : BaseQueryFieldGraphType<ReceiptBillModel> { }
    public class ReceiptBillChangeType : BaseQueryFieldGraphType<ReceiptBillChangeModel> { }
}

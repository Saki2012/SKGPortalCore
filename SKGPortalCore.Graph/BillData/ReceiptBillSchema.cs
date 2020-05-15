using SKGPortalCore.Core;
using SKGPortalCore.Core.GraphQL;
using SKGPortalCore.Interface.IGraphQL.BillData;
using SKGPortalCore.Interface.IRepository.BillData;
using SKGPortalCore.Model.BillData;

namespace SKGPortalCore.Graph.BillData
{
    //Schema
    public class ReceiptBillSchema : BaseSchema<ReceiptBillSet, ReceiptBillQuery, ReceiptBillMutation>, IReceiptBillSchema
    {
        public ReceiptBillSchema(IReceiptBillRepository repo, ISessionWrapper session) : base(repo, session) { }
    }
    //Operate
    public class ReceiptBillQuery : BaseQueryType<ReceiptBillSet, ReceiptBillSetType, ReceiptBillType>
    {
        public ReceiptBillQuery(IReceiptBillRepository repo, ISessionWrapper session) : base(repo, session) { }
    }
    public class ReceiptBillMutation : BaseMutationType<ReceiptBillSet, ReceiptBillSetType, ReceiptBillSetInputType>
    {
        public ReceiptBillMutation(IReceiptBillRepository repository, ISessionWrapper session) : base(repository, session) { }
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

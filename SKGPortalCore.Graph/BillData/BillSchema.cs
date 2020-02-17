using GraphQL;
using GraphQL.Types;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Repository.BillData;

namespace SKGPortalCore.Graph.BillData
{
    //Schema
    public class BillSchema : BaseSchema<BillQuery, BillMutation>
    {
        public BillSchema(IDependencyResolver resolver) : base(resolver) { }
    }
    //Operate
    public class BillQuery : BaseQueryType<BillSet, BillSetType, BillType>
    {
        public BillQuery(BillRepository repository) : base(repository) { }
    }
    public class BillMutation : BaseMutationType<BillSet, BillSetType, BillSetInputType>
    {
        public BillMutation(BillRepository repository) : base(repository) { }
    }
    //Input
    public class BillSetInputType : BaseInputSetGraphType<BillSet> { }
    public class BillInputType : BaseInputFieldGraphType<BillModel> { }
    public class BillDetailInputType : BaseInputFieldGraphType<BillDetailModel> { }
    public class BillReceiptDetailInputType : BaseInputFieldGraphType<BillReceiptDetailModel> { }
    //Query
    public class BillSetType : BaseQuerySetGraphType<BillSet> { }
    public class BillType : BaseQueryFieldGraphType<BillModel> { }
    public class BillDetailType : BaseQueryFieldGraphType<BillDetailModel> { }
    public class BillReceiptDetailType : BaseQueryFieldGraphType<BillReceiptDetailModel> { }
}

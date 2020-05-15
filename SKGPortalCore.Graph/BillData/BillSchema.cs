using System;
using System.Linq.Expressions;
using System.Reflection;
using SKGPortalCore.Core;
using SKGPortalCore.Core.GraphQL;
using SKGPortalCore.Interface.IGraphQL.BillData;
using SKGPortalCore.Interface.IRepository.BillData;
using SKGPortalCore.Model.BillData;

namespace SKGPortalCore.Graph.BillData
{
    //Schema
    public class BillSchema : BaseSchema<BillSet, BillQuery, BillMutation>, IBillSchema
    {
        public BillSchema(IBillRepository repo, ISessionWrapper session) : base(repo, session) { }
    }
    //Operate
    public class BillQuery : BaseQueryType<BillSet, BillSetType, BillType>
    {
        public BillQuery(IBillRepository repo, ISessionWrapper session) : base(repo, session) { }
    }
    public class BillMutation : BaseMutationType<BillSet, BillSetType, BillSetInputType>
    {
        public BillMutation(IBillRepository repo, ISessionWrapper session) : base(repo, session) { }
    }
    //InputFields
    public class BillSetInputType : BaseInputSetGraphType<BillSet> { }
    public class BillInputType : BaseInputFieldGraphType<BillModel>
    {
        protected override PropertyInfo[] SetExpectProperties(Expression<Func<BillModel, dynamic>> propertyExpression)
        {
            return base.SetExpectProperties(p => new dynamic[] { p.PayAmount, p.HadPayAmount, p.VirtualAccountCode, p.ImportBatchNo, p.PayStatus });
        }
    }
    public class BillDetailInputType : BaseInputFieldGraphType<BillDetailModel> { }
    public class BillReceiptDetailInputType : BaseInputFieldGraphType<BillReceiptDetailModel> { }
    //QueryFields
    public class BillSetType : BaseQuerySetGraphType<BillSet> { }
    public class BillType : BaseQueryFieldGraphType<BillModel> { }
    public class BillDetailType : BaseQueryFieldGraphType<BillDetailModel> { }
    public class BillReceiptDetailType : BaseQueryFieldGraphType<BillReceiptDetailModel> { }
}

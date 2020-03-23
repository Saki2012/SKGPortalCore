using GraphQL;
using GraphQL.Types;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.BillData;
using System;
using System.Linq.Expressions;
using System.Reflection;

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
        public BillQuery(BillRepository repository, ISessionWrapper session) : base(repository, session) { }
    }
    public class BillMutation : BaseMutationType<BillSet, BillSetType, BillSetInputType>
    {
        public BillMutation(BillRepository repository, ISessionWrapper session) : base(repository, session)
        {
            Field(
            type: typeof(BooleanGraphType),
            name: "UploadFile",
            description: "登入帳號",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<FileInfo>> { Name = "file", Description = "檔案資訊" }
            ),
            resolve: context =>
            {
                FileInfo file = context.GetArgument<FileInfo>("file");
                return true;
            });
        }
    }
    //Input
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
    //Query
    public class BillSetType : BaseQuerySetGraphType<BillSet> { }
    public class BillType : BaseQueryFieldGraphType<BillModel> { }
    public class BillDetailType : BaseQueryFieldGraphType<BillDetailModel> { }
    public class BillReceiptDetailType : BaseQueryFieldGraphType<BillReceiptDetailModel> { }
}

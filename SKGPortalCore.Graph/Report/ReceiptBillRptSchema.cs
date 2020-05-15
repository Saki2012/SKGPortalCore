using GraphQL.Types;
using SKGPortalCore.Core;
using SKGPortalCore.Core.GraphQL;
using SKGPortalCore.Core.Libary;
using SKGPortalCore.Interface.IRepository.Report;
using SKGPortalCore.Model.BillData;

namespace SKGPortalCore.Graph.Report
{
    //Schema
    //public class ReceiptBillRptSchema : Schema
    //{
    //    public ReceiptBillRptSchema(IDependencyResolver resolver) : base(resolver)
    //    {
    //        Query = resolver.Resolve<ReceiptBillRptQuery>() as IObjectGraphType;
    //    }
    //}
    //Operate
    public class ReceiptBillRptQuery : ObjectGraphType
    {
        public ReceiptBillRptQuery() : base()
        {
            Field(
         type: typeof(BooleanGraphType),
         name: nameof(IBillRptRepository.BillPayProgressRpt),
         description: SystemCP.DESC_BillPayProgressRpt,
         arguments: new QueryArguments(
             new QueryArgument<NonNullGraphType<StringGraphType>> { Name = nameof(BillModel.CustomerCode), Description = ResxManage.GetDescription<BillModel>(p => p.CustomerCode) },
             new QueryArgument<NonNullGraphType<StringGraphType>> { Name = nameof(BillModel.BillTermId), Description = ResxManage.GetDescription<BillModel>(p => p.BillTermId) }
         ),
         resolve: context =>
         {
             return null;// repository.BillPayProgressRpt(context.GetArgument<string>(nameof(BillModel.CustomerCode)), context.GetArgument<string>(nameof(BillModel.BillTermId)));
         });
        }
    }
    //Query
    public class BillReceiptDetailType : BaseQueryFieldGraphType<BillReceiptDetailModel> { }
}

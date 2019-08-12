using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Business.Func;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace SKGPortalCore.Controllers.BillData
{
    #region Controller
    [Route("[Controller]")]
    public class ReceiptBillController : BaseController
    {
        public ReceiptBillController(IDocumentExecuter documentExecuter, ReceiptBillSchema schema, ISessionWapper sessionWapper) : base(documentExecuter, schema, sessionWapper) { }
    }
    #endregion

    #region GraphQL
    public class ReceiptBillSchema : Schema
    {
        public ReceiptBillSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<ReceiptBillQuery>();
            Mutation = resolver.Resolve<ReceiptBillMutation>();
        }
    }
    public class ReceiptBillQuery : ObjectGraphType<object>
    {
        public ReceiptBillQuery(ReceiptBillRepository repository)
        {
            Field<ListGraphType<ReceiptBillType>>(
                name: "ReceiptBill",
                description: "帳單查詢",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "ReceiptBillNo" }),
                resolve: context =>
                {
                    var session = (ISessionWapper)context.UserContext;
                    if (!BizAccount.CheckAuthenticate(session.SessionId, context.GetArgument<string>("JWT"), "ReceiptBill", FuncAction.Query))
                    {
                        LogHelper log = new LogHelper(context.Errors);
                        log.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(FuncAction.Query));
                        return null;
                    }
                    string ReceiptBillNo = context.GetArgument<string>("ReceiptBillNo");
                    return new List<object>() { repository.QueryData(ReceiptBillNo).ReceiptBill };
                });
        }
    }
    public class ReceiptBillMutation : ObjectGraphType
    {
        public ReceiptBillMutation(ReceiptBillRepository repository)
        {
            Field<ReceiptBillType>(
                name: "create",
                description: "新增帳單",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ReceiptBillInputType>> { Name = "ReceiptBill" }),
                resolve: context =>
                {
                    ReceiptBillSet set = new ReceiptBillSet() { ReceiptBill = context.GetArgument<ReceiptBillModel>("ReceiptBill") };
                    return repository.Create(set);
                });
            Field<ReceiptBillType>(
                name: "update",
                description: "修改帳單",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ReceiptBillInputType>> { Name = "ReceiptBill" }),
                resolve: context =>
                {
                    ReceiptBillSet set = new ReceiptBillSet() { ReceiptBill = context.GetArgument<ReceiptBillModel>("ReceiptBill") };
                    return repository.Update(set);
                });
            Field<ReceiptBillType>(
                name: "delete",
                description: "刪除帳單",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "ReceiptBillNo" }),
                resolve: context =>
                {
                    string ReceiptBillNo = context.GetArgument<string>("ReceiptBillNo");
                    repository.Delete(ReceiptBillNo);
                    return null;
                });
        }
    }
    public class ReceiptBillInputType : InputObjectGraphType<ReceiptBillModel>
    {
        public ReceiptBillInputType()
        {
            PropertyInfo[] properties = typeof(ReceiptBillModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                Field(changeType, property.Name, ResxManage.GetDescription(property));
            }
        }
    }
    public class ReceiptBillType : ObjectGraphType<ReceiptBillModel>
    {
        public ReceiptBillType()
        {
            PropertyInfo[] properties = typeof(ReceiptBillModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                Field(changeType, property.Name, ResxManage.GetDescription(property));
            }
        }
    }
    #endregion

    #region Repository
    public class ReceiptBillRepository : BasicRepository<ReceiptBillSet>
    {
        public ReceiptBillRepository(ApplicationDbContext db) : base(db) { }
    }
    #endregion
}
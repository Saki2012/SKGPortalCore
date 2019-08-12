using System.Collections.Generic;
using System.Reflection;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Business.BillData;
using SKGPortalCore.Business.Func;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Models.BillData;

namespace SKGPortalCore.Controllers.BillData
{
    #region Controller
    [Route("[controller]")]
    public class BillController : BaseController
    {
        public BillController(IDocumentExecuter documentExecuter, BillSchema schema, ISessionWapper sessionWapper) : base(documentExecuter, schema, sessionWapper) { }

    }
    #endregion

    #region GraphQL
    public class BillSchema : Schema
    {
        public BillSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve(typeof(BillQuery)) as IObjectGraphType;
            Mutation = resolver.Resolve(typeof(BillMutation)) as IObjectGraphType;
        }
    }
    public class BillQuery : ObjectGraphType<object>
    {
        public BillQuery(BillRepository repository)
        {
            Field<ListGraphType<BillType>>(
                name: "bill",
                description: "帳單查詢",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "billNo" }, new QueryArgument<StringGraphType> { Name = "jWT" }),
                resolve: context =>
                {
                    var session = (ISessionWapper)context.UserContext;
                    if (!BizAccount.CheckAuthenticate(session.SessionId, context.GetArgument<string>("jWT"), "Bill", FuncAction.Query))
                    {
                        LogHelper log = new LogHelper(context.Errors);
                        log.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(FuncAction.Query));
                        return null;
                    }
                    string billNo = context.GetArgument<string>("billNo");
                    return new List<object>() { repository.QueryData(new[] { billNo }).Bill };
                });
        }
    }
    public class BillMutation : ObjectGraphType
    {
        public BillMutation(BillRepository repository)
        {
            Field<BillType>(
                name: "create",
                description: "新增帳單",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<BillInputType>> { Name = "Bill" }),
                resolve: context =>
                {
                    BillSet set = new BillSet() { Bill = context.GetArgument<BillModel>("Bill") };
                    repository.Create(set);
                    repository.CommitData(FuncAction.Create);
                    return set;
                });
            Field<BillType>(
                name: "update",
                description: "修改帳單",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<BillInputType>> { Name = "Bill" }),
                resolve: context =>
                {
                    BillSet set = new BillSet() { Bill = context.GetArgument<BillModel>("Bill") };
                    repository.Update(set);
                    repository.CommitData(FuncAction.Update);
                    return set;
                });
            Field<BillType>(
                name: "delete",
                description: "刪除帳單",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "billNo" }),
                resolve: context =>
                {
                    string billNo = context.GetArgument<string>("billNo");
                    repository.Delete(new[] { billNo });
                    repository.CommitData(FuncAction.Update);
                    return null;
                });
        }
    }
    public class BillInputType : InputObjectGraphType<BillModel>
    {
        public BillInputType()
        {
            PropertyInfo[] properties = typeof(BillModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                System.Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                Field(changeType, property.Name, ResxManage.GetDescription(property));
            }
        }
    }
    public class BillType : ObjectGraphType<BillModel>
    {
        public BillType()
        {
            PropertyInfo[] properties = typeof(BillModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                System.Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                Field(changeType, property.Name, ResxManage.GetDescription(property));
            }
        }
    }
    #endregion

    #region Repository
    public class BillRepository : BasicRepository<BillSet>
    {
        public BillRepository(ApplicationDbContext database) : base(database) { }
        protected override void BeforeSetEntity(BillSet set)
        {
            base.BeforeSetEntity(set);
            using BizBill biz = new BizBill(Database);
            biz.CheckData(set);
            biz.SetData(set);
        }
        protected override void AfterSetEntity(BillSet set)
        {
            base.AfterSetEntity(set);
        }
    }
    #endregion
}

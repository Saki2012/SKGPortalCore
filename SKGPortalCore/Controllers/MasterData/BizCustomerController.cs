using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Business.Func;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace SKGPortalCore.Controllers.MasterData
{
    #region Controller
    [Route("[Controller]")]
    public class BizCustomerController : BaseController
    {
        #region Constructor
        public BizCustomerController(IDocumentExecuter documentExecuter, BizCustomerSchema schema, ISessionWapper sessionWapper) : base(documentExecuter, schema, sessionWapper)
        {
        }
        #endregion
    }
    #endregion

    #region GraphQL
    public class BizCustomerSchema : Schema
    {
        public BizCustomerSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<BizCustomerQuery>();
            Mutation = resolver.Resolve<BizCustomerMutation>();
        }
    }
    public class BizCustomerQuery : ObjectGraphType<object>
    {
        public BizCustomerQuery(BizCustomerRepository repository)
        {
            Field<BizCustomerSetType>(
                name: "dataQuery",
                description: "客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<StringGraphType> { Name = "BizCustomerId" }),
                resolve: context =>
                {
                    var session = (ISessionWapper)context.UserContext;
                    if (!BizAccount.CheckAuthenticate(session.SessionId, context.GetArgument<string>("jwt"), "BizCustomer", FuncAction.Query))
                    {
                        LogHelper log = new LogHelper(context.Errors);
                        log.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(FuncAction.Query));
                        return null;
                    }
                    string BizCustomerId = context.GetArgument<string>("BizCustomerId");
                    return repository.QueryData(BizCustomerId).BizCustomer;
                });
            //Field<ListGraphType<BizCustomerType>>(
            //     name: "listQuery",
            //     description: "客戶基本資料列表",
            //     arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" }),
            //     resolve: context =>
            //     {
            //         var session = (ISessionWapper)context.UserContext;
            //         if (!BizAccount.CheckAuthenticate(session.SessionId, context.GetArgument<string>("jwt"), "BizCustomer", FuncAction.Query))
            //         {
            //             LogHelper log = new LogHelper(context.Errors);
            //             log.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(FuncAction.Query));
            //             return null;
            //         }
            //         //string BizCustomerNo = context.GetArgument<string>("BizCustomerNo");
            //         return repository.ListData();
            //     });
        }
    }
    public class BizCustomerMutation : ObjectGraphType
    {
        public BizCustomerMutation(BizCustomerRepository repository)
        {
            Field<BizCustomerSetType>(
                name: "create",
                description: "新增客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<NonNullGraphType<BizCustomerInputType>> { Name = "BizCustomer" }),
                resolve: context =>
                {
                    BizCustomerSet set = new BizCustomerSet() { BizCustomer = context.GetArgument<BizCustomerModel>("BizCustomer") };
                    return repository.Create(set);
                });
            Field<BizCustomerType>(
                name: "update",
                description: "修改客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<NonNullGraphType<BizCustomerInputType>> { Name = "BizCustomer" }),
                resolve: context =>
                {
                    BizCustomerSet set = new BizCustomerSet() { BizCustomer = context.GetArgument<BizCustomerModel>("BizCustomer") };
                    return repository.Update(set);
                });
            Field<BizCustomerType>(
                name: "delete",
                description: "刪除客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<StringGraphType> { Name = "BizCustomerId" }),
                resolve: context =>
                {
                    string BizCustomerId = context.GetArgument<string>("BizCustomerId");
                    repository.Delete(BizCustomerId);
                    return null;
                });
        }
    }
    public class BizCustomerInputType : InputObjectGraphType<BizCustomerModel>
    {
        public BizCustomerInputType()
        {
            PropertyInfo[] properties = typeof(BizCustomerModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                Field(changeType, property.Name, ResxManage.GetDescription(property));
            }
        }
    }
    public class BizCustomerSetType : ObjectGraphType<BizCustomerSet>
    {
        public BizCustomerSetType()
        {
            Field<BizCustomerType>("BizCustomer");
        }
    }
    public class BizCustomerType : ObjectGraphType<BizCustomerModel>
    {
        public BizCustomerType()
        {
            PropertyInfo[] properties = typeof(BizCustomerModel).GetProperties();
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
    public class BizCustomerRepository : BasicRepository<BizCustomerSet>
    {
        public BizCustomerRepository(ApplicationDbContext db) : base(db) { }
    }
    #endregion
}
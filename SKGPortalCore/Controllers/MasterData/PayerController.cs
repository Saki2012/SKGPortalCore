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
    public class PayerController : BaseController
    {
        

        #region Constructor
        public PayerController(IDocumentExecuter documentExecuter, PayerSchema schema, ISessionWapper sessionWapper) : base(documentExecuter, schema, sessionWapper)
        {

        }
        #endregion

        
    }
    #endregion

    #region GraphQL
    public class PayerSchema : Schema
    {
        public PayerSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<PayerQuery>();
            Mutation = resolver.Resolve<PayerMutation>();
        }
    }
    public class PayerQuery : ObjectGraphType<object>
    {
        public PayerQuery(PayerRepository repository)
        {
            Field<PayerSetType>(
                name: "dataQuery",
                description: "客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<StringGraphType> { Name = "PayerId" }),
                resolve: context =>
                {
                    var session = (ISessionWapper)context.UserContext;
                    if (!BizAccount.CheckAuthenticate(session.SessionId, context.GetArgument<string>("jwt"), "Payer", FuncAction.Query))
                    {
                        LogHelper log = new LogHelper(context.Errors);
                        log.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(FuncAction.Query));
                        return null;
                    }
                    string PayerId = context.GetArgument<string>("PayerId");
                    return repository.QueryData(PayerId).Payer;
                });
            //Field<ListGraphType<PayerType>>(
            //     name: "listQuery",
            //     description: "客戶基本資料列表",
            //     arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" }),
            //     resolve: context =>
            //     {
            //         var session = (ISessionWapper)context.UserContext;
            //         if (!BizAccount.CheckAuthenticate(session.SessionId, context.GetArgument<string>("jwt"), "Payer", FuncAction.Query))
            //         {
            //             LogHelper log = new LogHelper(context.Errors);
            //             log.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(FuncAction.Query));
            //             return null;
            //         }
            //         //string PayerNo = context.GetArgument<string>("PayerNo");
            //         return repository.ListData();
            //     });
        }
    }
    public class PayerMutation : ObjectGraphType
    {
        public PayerMutation(PayerRepository repository)
        {
            Field<PayerSetType>(
                name: "create",
                description: "新增客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<NonNullGraphType<PayerInputType>> { Name = "Payer" }),
                resolve: context =>
                {
                    PayerSet set = new PayerSet() { Payer = context.GetArgument<PayerModel>("Payer") };
                    return repository.Create(set);
                });
            Field<PayerType>(
                name: "update",
                description: "修改客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<NonNullGraphType<PayerInputType>> { Name = "Payer" }),
                resolve: context =>
                {
                    PayerSet set = new PayerSet() { Payer = context.GetArgument<PayerModel>("Payer") };
                    return repository.Update(set);
                });
            Field<PayerType>(
                name: "delete",
                description: "刪除客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<StringGraphType> { Name = "PayerId" }),
                resolve: context =>
                {
                    string PayerId = context.GetArgument<string>("PayerId");
                    repository.Delete(PayerId);
                    return null;
                });
        }
    }
    public class PayerInputType : InputObjectGraphType<PayerModel>
    {
        public PayerInputType()
        {
            PropertyInfo[] properties = typeof(PayerModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                Field(changeType, property.Name, ResxManage.GetDescription(property));
            }
        }
    }
    public class PayerSetType : ObjectGraphType<PayerSet>
    {
        public PayerSetType()
        {
            Field<PayerType>("Payer");
        }
    }
    public class PayerType : ObjectGraphType<PayerModel>
    {
        public PayerType()
        {
            PropertyInfo[] properties = typeof(PayerModel).GetProperties();
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
    public class PayerRepository : BasicRepository<PayerSet>
    {
        public PayerRepository(ApplicationDbContext db) : base(db) { }
    }
    #endregion
}
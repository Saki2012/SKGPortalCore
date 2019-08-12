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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SKGPortalCore.Controllers.MasterData
{
    #region Controller
    [Route("[Controller]")]
    public class CustomerController : BaseController
    {
        public CustomerController(IDocumentExecuter documentExecuter, CustomerSchema schema, ISessionWapper sessionWapper) : base(documentExecuter, schema, sessionWapper) { }
    }
    #endregion

    #region GraphQL
    public class CustomerSchema : Schema
    {
        public CustomerSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<CustomerQuery>();
            Mutation = resolver.Resolve<CustomerMutation>();
        }
    }
    public class CustomerQuery : ObjectGraphType<object>
    {
        public CustomerQuery(CustomerRepository repository)
        {
            Field<CustomerSetType>(
                name: "dataQuery",
                description: "客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<StringGraphType> { Name = "customerId" }),
                resolve: context =>
                {
                    var session = (ISessionWapper)context.UserContext;
                    if (!BizAccount.CheckAuthenticate(session.SessionId, context.GetArgument<string>("jwt"), "Customer", FuncAction.Query))
                    {
                        LogHelper log = new LogHelper(context.Errors);
                        log.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(FuncAction.Query));
                        return null;
                    }
                    string customerId = context.GetArgument<string>("customerId");
                    return repository.QueryData(customerId).Customer;
                });
            //Field<ListGraphType<CustomerType>>(
            //     name: "listQuery",
            //     description: "客戶基本資料列表",
            //     arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" }),
            //     resolve: context =>
            //     {
            //         var session = (ISessionWapper)context.UserContext;
            //         if (!BizAccount.CheckAuthenticate(session.SessionId, context.GetArgument<string>("jwt"), "Customer", FuncAction.Query))
            //         {
            //             LogHelper log = new LogHelper(context.Errors);
            //             log.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(FuncAction.Query));
            //             return null;
            //         }
            //         //string CustomerNo = context.GetArgument<string>("CustomerNo");
            //         return repository.ListData();
            //     });
        }
    }
    public class CustomerMutation : ObjectGraphType
    {
        public CustomerMutation(CustomerRepository repository)
        {
            Field<CustomerSetType>(
                name: "create",
                description: "新增客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<NonNullGraphType<CustomerInputType>> { Name = "customer" }),
                resolve: context =>
                {
                    CustomerSet set = new CustomerSet() { Customer = context.GetArgument<CustomerModel>("customer") };
                    return repository.Create(set);
                });
            Field<CustomerType>(
                name: "update",
                description: "修改客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<NonNullGraphType<CustomerInputType>> { Name = "customer" }),
                resolve: context =>
                {
                    CustomerSet set = new CustomerSet() { Customer = context.GetArgument<CustomerModel>("customer") };
                    return repository.Update(set);
                });
            Field<CustomerType>(
                name: "delete",
                description: "刪除客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<StringGraphType> { Name = "customerId" }),
                resolve: context =>
                {
                    string customerId = context.GetArgument<string>("customerId");
                    repository.Delete(customerId);
                    return null;
                });
        }
    }
    public class CustomerInputType : InputObjectGraphType<CustomerModel>
    {
        public CustomerInputType()
        {
            PropertyInfo[] properties = typeof(CustomerModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                Field(changeType, property.Name, ResxManage.GetDescription(property));
            }
        }
    }
    public class CustomerSetType : ObjectGraphType<CustomerSet>
    {
        public CustomerSetType()
        {
            Field<CustomerType>("Customer");
        }
    }
    public class CustomerType : ObjectGraphType<CustomerModel>
    {
        public CustomerType()
        {
            PropertyInfo[] properties = typeof(CustomerModel).GetProperties();
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
    public class CustomerRepository : BasicRepository<CustomerSet>
    {
        public CustomerRepository(ApplicationDbContext db) : base(db) { }
    }
    #endregion
}

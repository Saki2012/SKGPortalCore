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
    public class BillTermController : BaseController
    {
        #region Constructor
        public BillTermController(IDocumentExecuter documentExecuter, BillTermSchema schema, ISessionWapper sessionWapper):base(documentExecuter,schema,sessionWapper)
        {
        }
        #endregion
    }
    #endregion

    #region GraphQL
    public class BillTermSchema : Schema
    {
        public BillTermSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<BillTermQuery>();
            Mutation = resolver.Resolve<BillTermMutation>();
        }
    }
    public class BillTermQuery : ObjectGraphType<object>
    {
        public BillTermQuery(BillTermRepository repository)
        {
            Field<BillTermSetType>(
                name: "dataQuery",
                description: "客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<StringGraphType> { Name = " BillTermId" }),
                resolve: context =>
                {
                    var session = (ISessionWapper)context.UserContext;
                    if (!BizAccount.CheckAuthenticate(session.SessionId, context.GetArgument<string>("jwt"), " BillTerm", FuncAction.Query))
                    {
                        LogHelper log = new LogHelper(context.Errors);
                        log.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(FuncAction.Query));
                        return null;
                    }
                    string BillTermId = context.GetArgument<string>(" BillTermId");
                    return repository.QueryData(new[] { BillTermId }).BillTerm;
                });
            //Field<ListGraphType<BillTermType>>(
            //     name: "listQuery",
            //     description: "客戶基本資料列表",
            //     arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" }),
            //     resolve: context =>
            //     {
            //         var session = (ISessionWapper)context.UserContext;
            //         if (!BizAccount.CheckAuthenticate(session.SessionId, context.GetArgument<string>("jwt"), " BillTerm", FuncAction.Query))
            //         {
            //             LogHelper log = new LogHelper(context.Errors);
            //             log.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(FuncAction.Query));
            //             return null;
            //         }
            //         //string  BillTermNo = context.GetArgument<string>(" BillTermNo");
            //         return repository.ListData();
            //     });
        }
    }
    public class BillTermMutation : ObjectGraphType
    {
        public BillTermMutation(BillTermRepository repository)
        {
            Field<BillTermSetType>(
                name: "create",
                description: "新增客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<NonNullGraphType<BillTermInputType>> { Name = " BillTerm" }),
                resolve: context =>
                {
                    BillTermSet set = new BillTermSet() { BillTerm = context.GetArgument<BillTermModel>(" BillTerm") };
                    return repository.Create(set);
                });
            Field<BillTermType>(
                name: "update",
                description: "修改客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<NonNullGraphType<BillTermInputType>> { Name = " BillTerm" }),
                resolve: context =>
                {
                    BillTermSet set = new BillTermSet() { BillTerm = context.GetArgument<BillTermModel>(" BillTerm") };
                    return repository.Update(set);
                });
            Field<BillTermType>(
                name: "delete",
                description: "刪除客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<StringGraphType> { Name = " BillTermId" }),
                resolve: context =>
                {
                    string BillTermId = context.GetArgument<string>(" BillTermId");
                    repository.Delete(BillTermId);
                    return null;
                });
        }
    }
    public class BillTermInputType : InputObjectGraphType<BillTermModel>
    {
        public BillTermInputType()
        {
            PropertyInfo[] properties = typeof(BillTermModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                Field(changeType, property.Name, ResxManage.GetDescription(property));
            }
        }
    }
    public class BillTermSetType : ObjectGraphType<BillTermSet>
    {
        public BillTermSetType()
        {
            Field<BillTermType>(" BillTerm");
        }
    }
    public class BillTermType : ObjectGraphType<BillTermModel>
    {
        public BillTermType()
        {
            PropertyInfo[] properties = typeof(BillTermModel).GetProperties();
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
    public class BillTermRepository : BasicRepository<BillTermSet>
    {
        public BillTermRepository(ApplicationDbContext db) : base(db) { }
    }
    #endregion
}
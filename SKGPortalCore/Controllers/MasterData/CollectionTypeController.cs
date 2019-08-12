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
    public class CollectionTypeController : BaseController
    {
        #region Constructor
        public CollectionTypeController(IDocumentExecuter documentExecuter, CollectionTypeSchema schema, ISessionWapper sessionWapper) : base(documentExecuter, schema, sessionWapper)
        {
        }
        #endregion
    }
    #endregion

    #region GraphQL
    public class CollectionTypeSchema : Schema
    {
        public CollectionTypeSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<CollectionTypeQuery>();
            Mutation = resolver.Resolve<CollectionTypeMutation>();
        }
    }
    public class CollectionTypeQuery : ObjectGraphType<object>
    {
        public CollectionTypeQuery(CollectionTypeRepository repository)
        {
            Field<CollectionTypeSetType>(
                name: "dataQuery",
                description: "客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<StringGraphType> { Name = "CollectionTypeId" }),
                resolve: context =>
                {
                    var session = (ISessionWapper)context.UserContext;
                    if (!BizAccount.CheckAuthenticate(session.SessionId, context.GetArgument<string>("jwt"), "CollectionType", FuncAction.Query))
                    {
                        LogHelper log = new LogHelper(context.Errors);
                        log.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(FuncAction.Query));
                        return null;
                    }
                    string CollectionTypeId = context.GetArgument<string>("CollectionTypeId");
                    return repository.QueryData(CollectionTypeId).CollectionType;
                });
            //Field<ListGraphType<CollectionTypeType>>(
            //     name: "listQuery",
            //     description: "客戶基本資料列表",
            //     arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" }),
            //     resolve: context =>
            //     {
            //         var session = (ISessionWapper)context.UserContext;
            //         if (!BizAccount.CheckAuthenticate(session.SessionId, context.GetArgument<string>("jwt"), "CollectionType", FuncAction.Query))
            //         {
            //             LogHelper log = new LogHelper(context.Errors);
            //             log.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(FuncAction.Query));
            //             return null;
            //         }
            //         //string CollectionTypeNo = context.GetArgument<string>("CollectionTypeNo");
            //         return repository.ListData();
            //     });
        }
    }
    public class CollectionTypeMutation : ObjectGraphType
    {
        public CollectionTypeMutation(CollectionTypeRepository repository)
        {
            Field<CollectionTypeSetType>(
                name: "create",
                description: "新增客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<NonNullGraphType<CollectionTypeInputType>> { Name = "CollectionType" }),
                resolve: context =>
                {
                    CollectionTypeSet set = new CollectionTypeSet() { CollectionType = context.GetArgument<CollectionTypeModel>("CollectionType") };
                    return repository.Create(set);
                });
            Field<CollectionTypeType>(
                name: "update",
                description: "修改客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<NonNullGraphType<CollectionTypeInputType>> { Name = "CollectionType" }),
                resolve: context =>
                {
                    CollectionTypeSet set = new CollectionTypeSet() { CollectionType = context.GetArgument<CollectionTypeModel>("CollectionType") };
                    return repository.Update(set);
                });
            Field<CollectionTypeType>(
                name: "delete",
                description: "刪除客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<StringGraphType> { Name = "CollectionTypeId" }),
                resolve: context =>
                {
                    string CollectionTypeId = context.GetArgument<string>("CollectionTypeId");
                    repository.Delete(CollectionTypeId);
                    return null;
                });
        }
    }
    public class CollectionTypeInputType : InputObjectGraphType<CollectionTypeModel>
    {
        public CollectionTypeInputType()
        {
            PropertyInfo[] properties = typeof(CollectionTypeModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                Field(changeType, property.Name, ResxManage.GetDescription(property));
            }
        }
    }
    public class CollectionTypeSetType : ObjectGraphType<CollectionTypeSet>
    {
        public CollectionTypeSetType()
        {
            Field<CollectionTypeType>("CollectionType");
        }
    }
    public class CollectionTypeType : ObjectGraphType<CollectionTypeModel>
    {
        public CollectionTypeType()
        {
            PropertyInfo[] properties = typeof(CollectionTypeModel).GetProperties();
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
    public class CollectionTypeRepository : BasicRepository<CollectionTypeSet>
    {
        public CollectionTypeRepository(ApplicationDbContext db) : base(db) { }
    }
    #endregion
}
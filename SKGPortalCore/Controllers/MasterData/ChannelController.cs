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
    public class ChannelController :BaseController
    {
        #region Constructor
        public ChannelController(IDocumentExecuter documentExecuter, ChannelSchema schema, ISessionWapper sessionWapper) : base(documentExecuter, schema, sessionWapper)
        {
        }
        #endregion

        #region Public
        #endregion
    }
    #endregion

    #region GraphQL
    public class ChannelSchema : Schema
    {
        public ChannelSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<ChannelQuery>();
            Mutation = resolver.Resolve<ChannelMutation>();
        }
    }
    public class ChannelQuery : ObjectGraphType<object>
    {
        public ChannelQuery(ChannelRepository repository)
        {
            Field<ChannelSetType>(
                name: "dataQuery",
                description: "客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<StringGraphType> { Name = "ChannelId" }),
                resolve: context =>
                {
                    var session = (ISessionWapper)context.UserContext;
                    if (!BizAccount.CheckAuthenticate(session.SessionId, context.GetArgument<string>("jwt"), "Channel", FuncAction.Query))
                    {
                        LogHelper log = new LogHelper(context.Errors);
                        log.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(FuncAction.Query));
                        return null;
                    }
                    string ChannelId = context.GetArgument<string>("ChannelId");
                    return repository.QueryData(ChannelId).Channel;
                });
            //Field<ListGraphType<ChannelType>>(
            //     name: "listQuery",
            //     description: "客戶基本資料列表",
            //     arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" }),
            //     resolve: context =>
            //     {
            //         var session = (ISessionWapper)context.UserContext;
            //         if (!BizAccount.CheckAuthenticate(session.SessionId, context.GetArgument<string>("jwt"), "Channel", FuncAction.Query))
            //         {
            //             LogHelper log = new LogHelper(context.Errors);
            //             log.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(FuncAction.Query));
            //             return null;
            //         }
            //         //string ChannelNo = context.GetArgument<string>("ChannelNo");
            //         return repository.ListData();
            //     });
        }
    }
    public class ChannelMutation : ObjectGraphType
    {
        public ChannelMutation(ChannelRepository repository)
        {
            Field<ChannelSetType>(
                name: "create",
                description: "新增客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<NonNullGraphType<ChannelInputType>> { Name = "Channel" }),
                resolve: context =>
                {
                    ChannelSet set = new ChannelSet() { Channel = context.GetArgument<ChannelModel>("Channel") };
                    return repository.Create(set);
                });
            Field<ChannelType>(
                name: "update",
                description: "修改客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<NonNullGraphType<ChannelInputType>> { Name = "Channel" }),
                resolve: context =>
                {
                    ChannelSet set = new ChannelSet() { Channel = context.GetArgument<ChannelModel>("Channel") };
                    return repository.Update(set);
                });
            Field<ChannelType>(
                name: "delete",
                description: "刪除客戶基本資料",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<StringGraphType> { Name = "ChannelId" }),
                resolve: context =>
                {
                    string ChannelId = context.GetArgument<string>("ChannelId");
                    repository.Delete(ChannelId);
                    return null;
                });
        }
    }
    public class ChannelInputType : InputObjectGraphType<ChannelModel>
    {
        public ChannelInputType()
        {
            PropertyInfo[] properties = typeof(ChannelModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                Field(changeType, property.Name, ResxManage.GetDescription(property));
            }
        }
    }
    public class ChannelSetType : ObjectGraphType<ChannelSet>
    {
        public ChannelSetType()
        {
            Field<ChannelType>("Channel");
        }
    }
    public class ChannelType : ObjectGraphType<ChannelModel>
    {
        public ChannelType()
        {
            PropertyInfo[] properties = typeof(ChannelModel).GetProperties();
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
    public class ChannelRepository : BasicRepository<ChannelSet>
    {
        public ChannelRepository(ApplicationDbContext db) : base(db) { }

    }
    #endregion
}
using System;
using System.Reflection;
using GraphQL;
using GraphQL.Types;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository;

namespace SKGPortalCore.Graph
{
    #region Schema
    public class BaseSchema<TQuery> : Schema
    {
        public BaseSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<TQuery>() as IObjectGraphType;
        }
    }
    public class BaseSchema<TQuery, TMutation> : Schema
    {
        public BaseSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<TQuery>() as IObjectGraphType;
            Mutation = resolver.Resolve<TMutation>() as IObjectGraphType;
        }
    }
    public class BaseSchema<TQuery, TMutation, TSubscription> : Schema
    {
        public BaseSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<TQuery>() as IObjectGraphType;
            Mutation = resolver.Resolve<TMutation>() as IObjectGraphType;
            Subscription = resolver.Resolve<TSubscription>() as IObjectGraphType;
        }
    }
    #endregion

    #region Operate
    public class BaseQueryType<TSet, TSetType> : ObjectGraphType
        where TSetType : BaseQuerySetGraphType<TSet>
    {
        public BaseQueryType(BasicRepository<TSet> repo)
        {
            Field(
                type: typeof(TSetType),
                name: "queryData",
                description: ResxManage.GetDescription(FuncAction.Query),
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = "keyVal", Description = "主鍵" }, new QueryArgument<StringGraphType> { Name = "jWT" }),
                resolve: context =>
                {
                    repo.Message = null;
                    object[] keyVal = context.GetArgument<object>("keyVal") as object[];
                    var set = repo.QueryData(keyVal);
                    context.Errors.AddRange(repo.Message.Errors);
                    repo.Message.WriteLogTxt();
                    return context.Errors.Count == 0 ? set : default;
                });
            Field(
                type: typeof(TSetType),
                name: "queryList",
                description: "查詢列表",
                arguments: null,
                resolve: context =>
                {
                    repo.Message = null;



                    context.Errors.AddRange(repo.Message.Errors);
                    repo.Message.WriteLogTxt();
                    return repo.QueryList();
                });
        }
    }
    public class BaseMutationType<TSet, TSetType, TInputSet> : ObjectGraphType
        where TSetType : BaseQuerySetGraphType<TSet>
        where TInputSet : BaseInputSetGraphType<TSet>
    {
        public BaseMutationType(BasicRepository<TSet> repo)
        {
            Field(
                type: typeof(TSetType),
                name: "create",
                description: ResxManage.GetDescription(FuncAction.Create),
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<TInputSet>> { Name = "set", Description = "表單" }),
                resolve: context =>
                {
                    repo.Message = null;
                    TSet set = context.GetArgument<TSet>("set");
                    TSet result = repo.Create(set);
                    repo.CommitData(FuncAction.Create);
                    context.Errors.AddRange(repo.Message.Errors);
                    repo.Message.WriteLogTxt();
                    return context.Errors.Count == 0 ? result : default;
                });
            Field(
                type: typeof(TSetType),
                name: "update",
                description: ResxManage.GetDescription(FuncAction.Update),
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<TInputSet>> { Name = "set", Description = "表單" }),
                resolve: context =>
                {
#if DEBUG
                    repo.User = SystemOperator.SysOperator;
#endif
                    repo.Message = null;
                    TSet set = context.GetArgument<TSet>("set");
                    TSet result = repo.Update(set);
                    repo.CommitData(FuncAction.Update);
                    context.Errors.AddRange(repo.Message.Errors);
                    repo.Message.WriteLogTxt();
                    return context.Errors.Count == 0 ? result : default;
                });
            Field(
                type: typeof(BooleanGraphType),
                name: "delete",
                description: ResxManage.GetDescription(FuncAction.Delete),
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = "keyVal", Description = "主鍵" }),
                resolve: context =>
                {
                    repo.Message = null;
                    object keyVal = context.GetArgument<object>("keyVal");
                    repo.Delete(new[] { keyVal });
                    repo.CommitData(FuncAction.Delete);
                    context.Errors.AddRange(repo.Message.Errors);
                    repo.Message.WriteLogTxt();
                    return default;
                });
            Field(
                type: typeof(TSetType),
                name: "approve",
                description: ResxManage.GetDescription(FuncAction.Approve),
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = "keyVal", Description = "主鍵" }, new QueryArgument<BooleanGraphType> { Name = "status" }),
                resolve: context =>
                {
                    repo.Message = null;
                    object keyVal = context.GetArgument<object>("keyVal");
                    bool status = context.GetArgument<bool>("status");
                    TSet result = repo.Approve(new[] { keyVal }, status);
                    repo.CommitData(FuncAction.Approve);
                    context.Errors.AddRange(repo.Message.Errors);
                    repo.Message.WriteLogTxt();
                    return context.Errors.Count == 0 ? result : default;
                });
            Field(
                type: typeof(TSetType),
                name: "invalid",
                description: ResxManage.GetDescription(FuncAction.Invalid),
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = "keyVal", Description = "主鍵" }, new QueryArgument<BooleanGraphType> { Name = "status" }),
                resolve: context =>
                {
                    repo.Message = null;
                    object keyVal = context.GetArgument<object>("keyVal");
                    bool status = context.GetArgument<bool>("status");
                    TSet result = repo.Invalid(new[] { keyVal }, status);
                    repo.CommitData(FuncAction.Invalid);
                    context.Errors.AddRange(repo.Message.Errors);
                    repo.Message.WriteLogTxt();
                    return context.Errors.Count == 0 ? result : default;
                });
            Field(
              type: typeof(TSetType),
              name: "endCase",
              description: ResxManage.GetDescription(FuncAction.EndCase),
              arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = "keyVal", Description = "主鍵" }, new QueryArgument<BooleanGraphType> { Name = "status" }),
              resolve: context =>
              {
                  repo.Message = null;
                  object keyVal = context.GetArgument<object>("keyVal");
                  bool status = context.GetArgument<bool>("status");
                  TSet result = repo.Invalid(new[] { keyVal }, status);
                  repo.CommitData(FuncAction.EndCase);
                  context.Errors.AddRange(repo.Message.Errors);
                  repo.Message.WriteLogTxt();
                  return context.Errors.Count == 0 ? result : default;
              });
        }
    }
    #endregion

    #region SetGraph
    public class BaseQuerySetGraphType<TSet> : ObjectGraphType<TSet> { }
    public class BaseInputSetGraphType<TSet> : InputObjectGraphType<TSet> { }
    #endregion

    #region Fields
    public class BaseInputFieldGraphType<TSourceType> : InputObjectGraphType<TSourceType>
    {
        public BaseInputFieldGraphType()
        {
            PropertyInfo[] properties = typeof(TSourceType).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name, descript = ResxManage.GetDescription(property);
                if (!SetType(propertyName, descript))
                {
                    Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                    if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                    Field(changeType, propertyName, descript);
                }
            }
        }
        /// <summary>
        /// 設置Field控制
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="descript"></param>
        /// <returns>有更動時Return true,反之false</returns>
        protected virtual bool SetType(string propertyName, string descript)
        {
            return false;
        }
    }
    public class BaseQueryFieldGraphType<TSourceType> : ObjectGraphType<TSourceType>
    {
        public BaseQueryFieldGraphType()
        {
            PropertyInfo[] properties = typeof(TSourceType).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name, descript = ResxManage.GetDescription(property);
                if (!SetType(propertyName, descript))
                {
                    Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                    if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                    Field(changeType, propertyName, descript);
                }
            }
        }
        protected virtual bool SetType(string propertyName, string descript)
        {
            return false;
        }
    }
    #endregion
}

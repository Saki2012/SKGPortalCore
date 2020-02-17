using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using GraphQL;
using GraphQL.Types;
using SKGPortalCore.Business.Func;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository;
using SKGPortalCore.Graph.BillData;
using SKGPortalCore.Graph.MasterData;
using System.ComponentModel;
using SKGPortalCore.Model.Enum;

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
    public class BaseQueryType<TSet, TSetType, TMasterModelType> : ObjectGraphType where TSetType : BaseQuerySetGraphType<TSet> where TMasterModelType : IGraphType
    {
        public BaseQueryType(BasicRepository<TSet> repo)
        {
            Field(
                type: typeof(TSetType),
                name: "queryData",
                description: ResxManage.GetDescription(FuncAction.Query),
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = "keyVal", Description = "主鍵" }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "jwt", Description = "JWT" }),
                resolve: context =>
                {
                    object[] keyVal = (context.GetArgument<object>("keyVal") as List<object>).ToArray();
                    if (!BaseOperateComm<TSet>.CheckAuthority(context, repo, FuncAction.Query, keyVal)) return default;
                    TSet set = repo.QueryData(keyVal);
                    context.Errors.AddRange(repo.Message.Errors);
                    repo.Message.WriteLogTxt();
                    return context.Errors.Count == 0 ? set : default;
                });
            Field(
                type: typeof(ListGraphType<TMasterModelType>),
                name: "queryList",
                description: "查詢列表",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "condition", Description = "過濾條件" }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "jwt", Description = "JWT" }),
                resolve: context =>
                {
                    string condition = context.GetArgument<string>("condition");

                    if (!BaseOperateComm<TSet>.CheckAuthority(context, repo, FuncAction.Query, null)) return default;
                    context.Errors.AddRange(repo.Message.Errors);
                    repo.Message.WriteLogTxt();
                    return repo.QueryList(condition);
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
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<TInputSet>> { Name = "set", Description = "表單" }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "jwt", Description = "JWT" }),
                resolve: context =>
                {
                    object[] keyVal = context.GetArgument<object>("keyVal") as object[];
                    if (!BaseOperateComm<TSet>.CheckAuthority(context, repo, FuncAction.Create, keyVal)) return default;
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
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<TInputSet>> { Name = "set", Description = "表單" }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "jwt", Description = "JSON Web Token" }),
                resolve: context =>
                {
                    object[] keyVal = context.GetArgument<object>("keyVal") as object[];
                    if (!BaseOperateComm<TSet>.CheckAuthority(context, repo, FuncAction.Update, keyVal)) return default;
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
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = "keyVal", Description = "主鍵" }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "jwt", Description = "JWT" }),
                resolve: context =>
                {
                    object[] keyVal = context.GetArgument<object>("keyVal") as object[];
                    if (!BaseOperateComm<TSet>.CheckAuthority(context, repo, FuncAction.Delete, keyVal)) return default;
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
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = "keyVal", Description = "主鍵" }, new QueryArgument<BooleanGraphType> { Name = "status", Description = "審核狀態" }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "jwt", Description = "JWT" }),
                resolve: context =>
                {
                    object[] keyVal = context.GetArgument<object>("keyVal") as object[];
                    if (!BaseOperateComm<TSet>.CheckAuthority(context, repo, FuncAction.Approve, keyVal)) return default;
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
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = "keyVal", Description = "主鍵" }, new QueryArgument<BooleanGraphType> { Name = "status", Description = "作廢狀態" }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "jwt", Description = "JWT" }),
                resolve: context =>
                {
                    object[] keyVal = context.GetArgument<object>("keyVal") as object[];
                    if (!BaseOperateComm<TSet>.CheckAuthority(context, repo, FuncAction.Invalid, keyVal)) return default;
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
              arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = "keyVal", Description = "主鍵" }, new QueryArgument<BooleanGraphType> { Name = "status", Description = "結案狀態" }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "jwt", Description = "JWT" }),
              resolve: context =>
              {
                  object[] keyVal = context.GetArgument<object>("keyVal") as object[];
                  if (!BaseOperateComm<TSet>.CheckAuthority(context, repo, FuncAction.EndCase, keyVal)) return default;
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
    public class BaseQuerySetGraphType<TSet> : ObjectGraphType<TSet>
    {
        public BaseQuerySetGraphType()
        {
            foreach (var t in typeof(TSet).GetProperties())
            {
                string typeName = $"{GetType().Namespace}.{t.Name}Type";
                string description = ResxManage.GetDescription(t);
                if (typeof(IEnumerable).IsAssignableFrom(t.PropertyType))
                    Field(typeof(ListGraphType<>).MakeGenericType(new[] { Type.GetType(typeName) }), t.Name, description);
                else
                {
                    Field(Type.GetType(typeName), t.Name, description);
                    Field(typeof(ListGraphType<>).MakeGenericType(new[] { Type.GetType(typeName) }), $"{t.Name}List", $"{ description}列表");
                }
            }
        }
    }
    public class BaseInputSetGraphType<TSet> : InputObjectGraphType<TSet>
    {
        public BaseInputSetGraphType()
        {
            foreach (var t in typeof(TSet).GetProperties())
            {
                string typeName = $"{GetType().Namespace}.{t.Name}InputType";
                string description = ResxManage.GetDescription(t);
                if (typeof(IEnumerable).IsAssignableFrom(t.PropertyType))
                    Field(typeof(ListGraphType<>).MakeGenericType(new[] { Type.GetType(typeName) }), t.Name, description);
                else
                    Field(Type.GetType(typeName), t.Name, description);
            }
        }
    }
    #endregion

    #region Fields
    public class BaseInputFieldGraphType<TModelType> : InputObjectGraphType<TModelType>
    {
        public BaseInputFieldGraphType()
        {
            Type t = typeof(TModelType);
            string[] baseProperty = t.BaseType.GetProperties().Select(p => p.Name).Where(p => p.CompareTo("RowState") != 0).ToArray();
            PropertyInfo[] properties = t.GetProperties().Where(p => !baseProperty.Contains(p.Name)).ToArray();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name, descript = ResxManage.GetDescription(property);
                if (!SetType(propertyName, descript))
                {
                    Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                    if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:ModelClass)

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
    public class BaseQueryFieldGraphType<TModelType> : ObjectGraphType<TModelType>
    {
        public BaseQueryFieldGraphType()
        {
            PropertyInfo[] properties = typeof(TModelType).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name, descript = ResxManage.GetDescription(property);
                if (!SetType(propertyName, ref descript))
                {
                    Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                    if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:ModelClass)
                    Field(changeType, propertyName, descript);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="descript"></param>
        /// <returns>回傳True時，將不繼續往下添加欄位</returns>
        protected virtual bool SetType(string propertyName, ref string descript)
        {
            return false;
        }
    }


    #endregion

    #region Comm
    internal static class BaseOperateComm<TSet>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="repository"></param>
        /// <param name="action"></param>
        /// <param name="keyVal"></param>
        internal static bool CheckAuthority(ResolveFieldContext<object> context, BasicRepository<TSet> repository, FuncAction action, object[] keyVal)
        {
            return true;
            ISessionWapper session = ((ISessionWapper)context.UserContext);
            repository.User = session?.User;
            repository.Message = null;
            string progId = repository.GetType().GetCustomAttribute<ProgIdAttribute>()?.Value ?? string.Empty;
            SetDebugUser(repository, context, progId);
            SysOperateLog.SetOperateLog(repository.User.KeyId, session.IP, session.Browser, progId, GetPKValueString(keyVal), ResxManage.GetDescription(action));
            if (repository.User != SystemOperator.SysOperator && !AccountLogin.CheckAuthenticate(context, progId, action))
            {
                repository.Message.AddCustErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(repository), ResxManage.GetDescription(action));
                context.Errors.AddRange(repository.Message.Errors);
                repository.Message.WriteLogTxt();
                return false;
            }
            return true;
        }
        /// <summary>
        /// 設置Debug的User
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="context"></param>
        /// <param name="progId"></param>
        internal static void SetDebugUser(BasicRepository<TSet> repo, ResolveFieldContext<object> context, string progId)
        {
#if DEBUG
            repo.User = SystemOperator.SysOperator;
            //using ApplicationDbContext dataAccess = LibDataAccess.CreateDataAccess();
            //AccountRepository accountRepository = new AccountRepository(dataAccess);
            //CustUserSet userSet = accountRepository.Login("80425514", "admin", "123456");
            //repo.User = userSet.User;
            //Dictionary<string, string> permissions = AccountLogin.GetRolePermissionsToken(((ISessionWapper)context.UserContext).SessionId, userSet.UserRoles);
            //context.Arguments["jwt"] = permissions[progId];
#endif
        }
        /// <summary>
        /// 獲取主鍵值
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        private static string GetPKValueString(object[] objs)
        {
            return LibData.Merge(",", true, objs);
        }
    }
    #endregion
}

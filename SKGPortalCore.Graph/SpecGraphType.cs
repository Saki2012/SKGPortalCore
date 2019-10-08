using System;
using System.Reflection;
using System.Runtime.InteropServices;
using GraphQL;
using GraphQL.Types;
using SKGPortalCore.Business.Func;
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
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = "keyVal", Description = "主鍵" }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "jwt", Description = "JWT" }),
                resolve: context =>
                {
                    ISessionWapper session = ((ISessionWapper)context.UserContext);
                    repo.User = session.User;
                    repo.Message = null;
                    string progId = repo.GetType().GetCustomAttribute<ProgIdAttribute>()?.Value ?? string.Empty;
                    object[] keyVal = context.GetArgument<object>("keyVal") as object[];
                    Comm<TSet>.SetDebugUser(repo, context, progId);
                    Comm<TSet>.SetOperateLog(repo.User.KeyId, session.IP, session.Browser, progId, Comm<TSet>.GetPKValue(keyVal), ResxManage.GetDescription(FuncAction.Query));
                    if (!AccountLogin.CheckAuthenticate(context, progId, FuncAction.Query))
                    {
                        repo.Message.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(repo), ResxManage.GetDescription(FuncAction.Query));
                        context.Errors.AddRange(repo.Message.Errors);
                        repo.Message.WriteLogTxt();
                        return default;
                    }
                    TSet set = repo.QueryData(keyVal);
                    context.Errors.AddRange(repo.Message.Errors);
                    repo.Message.WriteLogTxt();
                    return context.Errors.Count == 0 ? set : default;
                });
            Field(
                type: typeof(TSetType),
                name: "queryList",
                description: "查詢列表",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "jwt", Description = "JWT" }),
                resolve: context =>
                {
                    ISessionWapper session = ((ISessionWapper)context.UserContext);
                    repo.User = session?.User;
                    repo.Message = null;
                    string progId = repo.GetType().GetCustomAttribute<ProgIdAttribute>()?.Value ?? string.Empty;
                    Comm<TSet>.SetDebugUser(repo, context, progId);
                    Comm<TSet>.SetOperateLog(repo.User.KeyId, session.IP, session.Browser, progId, "", ResxManage.GetDescription(FuncAction.Query));
                    if (!AccountLogin.CheckAuthenticate(context, progId, FuncAction.Query))
                    {
                        repo.Message.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(repo), ResxManage.GetDescription(FuncAction.Query));
                        context.Errors.AddRange(repo.Message.Errors);
                        repo.Message.WriteLogTxt();
                        return default;
                    }


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
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<TInputSet>> { Name = "set", Description = "表單" }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "jwt", Description = "JWT" }),
                resolve: context =>
                {
                    ISessionWapper session = ((ISessionWapper)context.UserContext);
                    repo.User = session?.User;
                    repo.Message = null;
                    string progId = repo.GetType().GetCustomAttribute<ProgIdAttribute>()?.Value ?? string.Empty;
                    TSet set = context.GetArgument<TSet>("set");
                    Comm<TSet>.SetDebugUser(repo, context, progId);
                    Comm<TSet>.SetOperateLog(repo.User.KeyId, session.IP, session.Browser, progId, "", ResxManage.GetDescription(FuncAction.Create));
                    if (repo.User != SystemOperator.SysOperator && !AccountLogin.CheckAuthenticate(context, progId, FuncAction.Create))
                    {
                        repo.Message.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(repo), ResxManage.GetDescription(FuncAction.Create));
                        context.Errors.AddRange(repo.Message.Errors);
                        repo.Message.WriteLogTxt();
                        return default;
                    }
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
                    ISessionWapper session = ((ISessionWapper)context.UserContext);
                    repo.User = session?.User;
                    repo.Message = null;
                    string progId = repo.GetType().GetCustomAttribute<ProgIdAttribute>()?.Value ?? string.Empty;
                    TSet set = context.GetArgument<TSet>("set");
                    Comm<TSet>.SetDebugUser(repo, context, progId);
                    Comm<TSet>.SetOperateLog(repo.User.KeyId, session.IP, session.Browser, progId, Comm<TSet>.GetPKValue(repo.GetPKVals(set)), ResxManage.GetDescription(FuncAction.Update));
                    if (repo.User != SystemOperator.SysOperator && !AccountLogin.CheckAuthenticate(context, progId, FuncAction.Update))
                    {
                        repo.Message.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(repo), ResxManage.GetDescription(FuncAction.Update));
                        context.Errors.AddRange(repo.Message.Errors);
                        repo.Message.WriteLogTxt();
                        return default;
                    }
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
                    ISessionWapper session = ((ISessionWapper)context.UserContext);

                    repo.User = session?.User;
                    repo.Message = null;
                    string progId = repo.GetType().GetCustomAttribute<ProgIdAttribute>()?.Value ?? string.Empty;
                    object[] keyVal = context.GetArgument<object>("keyVal") as object[];
                    Comm<TSet>.SetDebugUser(repo, context, progId);
                    Comm<TSet>.SetOperateLog(repo.User.KeyId, session.IP, session.Browser, progId, Comm<TSet>.GetPKValue(keyVal), ResxManage.GetDescription(FuncAction.Delete));
                    if (repo.User != SystemOperator.SysOperator && !AccountLogin.CheckAuthenticate(context, progId, FuncAction.Delete))
                    {
                        repo.Message.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(repo), ResxManage.GetDescription(FuncAction.Delete));
                        context.Errors.AddRange(repo.Message.Errors);
                        repo.Message.WriteLogTxt();
                        return default;
                    }
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
                    ISessionWapper session = ((ISessionWapper)context.UserContext);

                    repo.User = session?.User;
                    repo.Message = null;
                    string progId = repo.GetType().GetCustomAttribute<ProgIdAttribute>()?.Value ?? string.Empty;
                    object[] keyVal = context.GetArgument<object>("keyVal") as object[];
                    Comm<TSet>.SetDebugUser(repo, context, progId);
                    Comm<TSet>.SetOperateLog(repo.User.KeyId, session.IP, session.Browser, progId, Comm<TSet>.GetPKValue(keyVal), ResxManage.GetDescription(FuncAction.Approve));
                    if (repo.User != SystemOperator.SysOperator && !AccountLogin.CheckAuthenticate(context, progId, FuncAction.Approve))
                    {
                        repo.Message.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(repo), ResxManage.GetDescription(FuncAction.Approve));
                        context.Errors.AddRange(repo.Message.Errors);
                        repo.Message.WriteLogTxt();
                        return default;
                    }
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
                    ISessionWapper session = ((ISessionWapper)context.UserContext);
                    repo.User = session?.User;
                    repo.Message = null;
                    string progId = repo.GetType().GetCustomAttribute<ProgIdAttribute>()?.Value ?? string.Empty;
                    object[] keyVal = context.GetArgument<object>("keyVal") as object[];
                    Comm<TSet>.SetDebugUser(repo, context, progId);
                    Comm<TSet>.SetOperateLog(repo.User.KeyId, session.IP, session.Browser, progId, Comm<TSet>.GetPKValue(keyVal), ResxManage.GetDescription(FuncAction.Invalid));
                    if (repo.User != SystemOperator.SysOperator && !AccountLogin.CheckAuthenticate(context, progId, FuncAction.Invalid))
                    {
                        repo.Message.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(repo), ResxManage.GetDescription(FuncAction.Invalid));
                        context.Errors.AddRange(repo.Message.Errors);
                        repo.Message.WriteLogTxt();
                        return default;
                    }
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
                  ISessionWapper session = ((ISessionWapper)context.UserContext);
                  repo.User = session?.User;
                  repo.Message = null;
                  string progId = repo.GetType().GetCustomAttribute<ProgIdAttribute>()?.Value ?? string.Empty;
                  object[] keyVal = context.GetArgument<object>("keyVal") as object[];
                  Comm<TSet>.SetDebugUser(repo, context, progId);
                  Comm<TSet>.SetOperateLog(repo.User.KeyId, session.IP, session.Browser, progId, Comm<TSet>.GetPKValue(keyVal), ResxManage.GetDescription(FuncAction.EndCase));
                  if (repo.User != SystemOperator.SysOperator && !AccountLogin.CheckAuthenticate(context, progId, FuncAction.EndCase))
                  {
                      repo.Message.AddErrorMessage(MessageCode.Code0002, ResxManage.GetDescription(repo), ResxManage.GetDescription(FuncAction.EndCase));
                      context.Errors.AddRange(repo.Message.Errors);
                      repo.Message.WriteLogTxt();
                      return default;
                  }
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
    public class BaseInputFieldGraphType<TModelType> : InputObjectGraphType<TModelType>
    {
        public BaseInputFieldGraphType()
        {
            PropertyInfo[] properties = typeof(TModelType).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name, descript = ResxManage.GetDescription(property);
                if (!SetType(propertyName, descript))
                {
                    Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                    if (property.PropertyType == changeType)
                    {
                        continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                    }

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
                if (!SetType(propertyName, descript))
                {
                    Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                    if (property.PropertyType == changeType)
                    {
                        continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                    }

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

    internal class Comm<TSet>
    {
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

        internal static void SetOperateLog(string userId, string ip, string browser, string progId, string pk, string action)
        {
            try
            {
                using ApplicationDbContext dataAccess = LibDataAccess.CreateDataAccess();
                dataAccess.OperateLog.Add(new OperateLog()
                {
                    UserId = userId,
                    IP = ip,
                    Browser = browser,
                    ProgId = progId,
                    PK = pk,
                    OperateTime = DateTime.Now,
                    Action = action,
                });
                dataAccess.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        internal static string GetPKValue(object[] objs)
        {
            return LibData.Merge(",", true, objs);
        }
    }
}

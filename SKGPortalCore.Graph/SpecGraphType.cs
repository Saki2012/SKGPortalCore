﻿using GraphQL;
using GraphQL.Types;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository;
using SKGPortalCore.Repository.SKGPortalCore.Business.Func;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;

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
        public BaseQueryType(BasicRepository<TSet> repo, ISessionWrapper session)
        {
            Field(
                type: typeof(TSetType),
                name: "queryData",
                description: ResxManage.GetDescription(FuncAction.Query),
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = "keyVal", Description = "主鍵" }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "jwt", Description = "JWT" }),
                resolve: context =>
                {
                    object[] keyVal = (context.GetArgument<object>("keyVal") as List<object>).ToArray();
                    if (!BaseOperateComm<TSet>.CheckAuthority(context, session, repo, FuncAction.Query, keyVal)) return default;
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

                    if (!BaseOperateComm<TSet>.CheckAuthority(context, session, repo, FuncAction.Query, null)) return default;
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
                    //object[] keyVal = context.GetArgument<object>("keyVal") as object[];
                    //if (!BaseOperateComm<TSet>.CheckAuthority(context, repo, FuncAction.Create, null)) return default;
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
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = "keyVal", Description = "主鍵" }, new QueryArgument<NonNullGraphType<TInputSet>> { Name = "set", Description = "表單" }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "jwt", Description = "JSON Web Token" }),
                resolve: context =>
                {
                    object[] keyVal = context.GetArgument<object>("keyVal") as object[];
                    //if (!BaseOperateComm<TSet>.CheckAuthority(context, repo, FuncAction.Update, keyVal)) return default;
                    TSet set = context.GetArgument<TSet>("set");
                    TSet result = repo.Update(keyVal, set);
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
                    //if (!BaseOperateComm<TSet>.CheckAuthority(context, repo, FuncAction.Delete, keyVal)) return default;
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
                    //if (!BaseOperateComm<TSet>.CheckAuthority(context, repo, FuncAction.Approve, keyVal)) return default;
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
                    //if (!BaseOperateComm<TSet>.CheckAuthority(context, repo, FuncAction.Invalid, keyVal)) return default;
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
                  //if (!BaseOperateComm<TSet>.CheckAuthority(context, repo, FuncAction.EndCase, keyVal)) return default;
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
            Name = typeof(TSet).Name;
            foreach (var t in typeof(TSet).GetProperties())
            {
                string typeName = $"{GetType().Namespace}.{t.Name}Type";
                string description = ResxManage.GetDescription(t);
                if (typeof(IEnumerable).IsAssignableFrom(t.PropertyType))
                    Field(typeof(ListGraphType<>).MakeGenericType(new[] { Type.GetType(typeName) }), t.Name, description);
                else
                {
                    Field(Type.GetType(typeName), t.Name, description);
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
            PropertyInfo[] properties = t.GetProperties().Where(p => p.IsDefined(typeof(InputFieldAttribute)) || p.IsDefined(typeof(KeyAttribute))).ToArray();
            PropertyInfo[] expectProperties = SetExpectProperties(null);
            if (null != expectProperties) properties = properties.Except(expectProperties).ToArray();
            foreach (PropertyInfo property in properties)
            {
                Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:ModelClass)
                Field(changeType, property.Name, ResxManage.GetDescription(property));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        protected virtual PropertyInfo[] SetExpectProperties(Expression<Func<TModelType, dynamic>> propertyExpression)
        {
            List<PropertyInfo> props = new List<PropertyInfo>();
            List<string> propsName = new List<string>();
            propsName.AddRange(typeof(BasicDataModel).GetProperties().Select(p => p.Name).ToArray());
            props.AddRange(typeof(TModelType).GetProperties().Where(p => propsName.Contains(p.Name)).ToArray());
            if (null != propertyExpression)
            {
                var fields = ((NewArrayExpression)(propertyExpression.Body)).Expressions;
                foreach (Expression field in fields)
                    if (field.NodeType == ExpressionType.Convert)
                        props.Add((PropertyInfo)((MemberExpression)((UnaryExpression)field).Operand).Member);
                    else
                        props.Add((PropertyInfo)((MemberExpression)field).Member);
            }
            return props.ToArray();
        }
    }
    public class BaseQueryFieldGraphType<TModelType> : ObjectGraphType<TModelType>
    {
        public BaseQueryFieldGraphType()
        {
            Name = typeof(TModelType).Name.Replace("Model", "");
            PropertyInfo[] properties = typeof(TModelType).GetProperties();
            PropertyInfo[] expectProperties = SetExpectProperties(null);
            if (null != expectProperties) properties = properties.Except(expectProperties).ToArray();
            foreach (PropertyInfo property in properties)
            {
                Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:ModelClass)
                Field(changeType, property.Name, ResxManage.GetDescription(property));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        protected virtual PropertyInfo[] SetExpectProperties(Expression<Func<TModelType, object>> propertyExpression)
        {
            List<PropertyInfo> props = new List<PropertyInfo>();
            List<string> propsName = new List<string>();
            propsName.AddRange(typeof(DetailRowState).GetProperties().Select(p => p.Name).ToArray());
            props.AddRange(typeof(TModelType).GetProperties().Where(p => propsName.Contains(p.Name)).ToArray());
            if (null != propertyExpression)
            {
                var fields = ((NewArrayExpression)(propertyExpression.Body)).Expressions;
                foreach (Expression field in fields)
                    if (field.NodeType == ExpressionType.Convert)
                        props.Add((PropertyInfo)((MemberExpression)((UnaryExpression)field).Operand).Member);
                    else
                        props.Add((PropertyInfo)((MemberExpression)field).Member);
            }
            return props.ToArray();
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
        internal static bool CheckAuthority(ResolveFieldContext<object> context, ISessionWrapper session, BasicRepository<TSet> repository, FuncAction action, object[] keyVal)
        {
            SetDebugUser(repository/*, progId*/);
            //return true;
            //ISessionWapper session = ((ISessionWapper)context.UserContext);
            repository.User = session?.User;
            repository.Message = null;
            string progId = repository.GetType().GetCustomAttribute<ProgIdAttribute>()?.Value ?? string.Empty;
            SysOperateLog.SetOperateLog(repository.User.KeyId, session.IP, session.Browser, progId, GetPKValueString(keyVal), ResxManage.GetDescription(action));
            if (repository.User != SystemOperator.SysOperator && !BizAccountLogin.CheckAuthenticate(context, progId, action))
            {
                repository.Message.AddCustErrorMessage(MessageCode.Code0002, ResxManage.GetDescription<TSet>(), ResxManage.GetDescription(action));
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
        internal static void SetDebugUser(BasicRepository<TSet> repo)
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

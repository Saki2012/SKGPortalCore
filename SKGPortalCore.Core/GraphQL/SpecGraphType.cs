using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using GraphQL;
using GraphQL.Language.AST;
using GraphQL.Types;
using SKGPortalCore.Core.Libary;
using SKGPortalCore.Core.LibAttribute;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Model;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Core.Repository;
using SKGPortalCore.Core.Repository.Interface;

namespace SKGPortalCore.Core.GraphQL
{
    #region Schema
    public class BaseSchema<TSet, TQuery> : Schema
    {
        public BaseSchema(IBasicRepository<TSet> repo, ISessionWrapper session) : base()
        {
            Query = LibData.Build<TQuery>()(new object[] { repo, session }) as IObjectGraphType;
        }
    }
    public class BaseSchema<TSet, TQuery, TMutation> : Schema
    {
        public BaseSchema(IBasicRepository<TSet> repo, ISessionWrapper session) : base()
        {
            Query = LibData.Build<TQuery>()(new object[] { repo, session }) as IObjectGraphType;
            Mutation = LibData.Build<TMutation>()(new object[] { repo, session }) as IObjectGraphType;
        }
    }
    public class BaseSchema<TSet, TQuery, TMutation, TSubscription> : Schema
    {
        public BaseSchema(IBasicRepository<TSet> repo, ISessionWrapper session) : base()
        {
            Query = LibData.Build<TQuery>()(new object[] { repo, session }) as IObjectGraphType;
            Mutation = LibData.Build<TMutation>()(new object[] { repo, session }) as IObjectGraphType;
            Subscription = LibData.Build<TSubscription>()(new object[] { repo, session }) as IObjectGraphType;
        }
    }
    #endregion

    #region Operate
    public class BaseQueryType<TSet, TSetType, TMasterModelType> : ObjectGraphType
        where TSetType : BaseQuerySetGraphType<TSet>
        where TMasterModelType : IGraphType
    {
        public BaseQueryType(IBasicRepository<TSet> repo, ISessionWrapper session)
        {
            Field(
                type: typeof(TSetType),
                name: nameof(repo.QueryData),
                description: SystemCP.DESC_QueryData,
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = SystemCP.KeyVal, Description = SystemCP.DESC_KeyVal }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = SystemCP.JWT, Description = SystemCP.DESC_JWT }),
                resolve: context =>
                {
                    object[] keyVal = (context.GetArgument<object>(SystemCP.KeyVal) as List<object>).ToArray();
                    if (!BaseOperateComm<TSet>.CheckAuthority(context, session, repo, FuncAction.Query, keyVal)) return default;
                    TSet set = repo.QueryData(keyVal);
                    context.Errors.AddRange(repo.Message.Errors);
                    repo.Message.WriteLogTxt();
                    return context.Errors.Count == 0 ? set : default;
                });
            Field(
                type: typeof(ListGraphType<TMasterModelType>),
                name: nameof(repo.QueryList),
                description: SystemCP.DESC_QueryList,
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = SystemCP.Condition, Description = SystemCP.DESC_Condition }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = SystemCP.JWT, Description = SystemCP.DESC_JWT },
new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "pageCt", Description = "頁數" }, new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "takeCt", Description = "筆數" }),
                resolve: context =>
                {
                    string selectFields = LibData.Merge(",", false, context.Fragments.Select(p => p.SelectionSet.Selections).FirstOrDefault()?.Select(p => ((Field)p).Name).Where(p => p != "__typename").ToArray());
                    if (!string.IsNullOrEmpty(selectFields)) selectFields = $"new ({selectFields})";
                    string condition = context.GetArgument<string>(SystemCP.Condition);
                    if (!BaseOperateComm<TSet>.CheckAuthority(context, session, repo, FuncAction.Query, null)) return default;
                    context.Errors.AddRange(repo.Message.Errors);
                    repo.Message.WriteLogTxt();
                    int pageCt = context.GetArgument<int>("pageCt");
                    int takeCt = context.GetArgument<int>("takeCt");
                    return repo.QueryList(selectFields, condition, pageCt, takeCt);
                });
        }
    }
    public class BaseMutationType<TSet, TSetType, TInputSet> : ObjectGraphType
        where TSetType : BaseQuerySetGraphType<TSet>
        where TInputSet : BaseInputSetGraphType<TSet>
    {
        public BaseMutationType(IBasicRepository<TSet> repo, ISessionWrapper session)
        {
            Field(
                type: typeof(TSetType),
                name: nameof(repo.Create),
                description: SystemCP.DESC_Create,
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<TInputSet>> { Name = SystemCP.Set, Description = SystemCP.DESC_Set }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = SystemCP.JWT, Description = SystemCP.DESC_JWT }),
                resolve: context =>
                {
                    if (!BaseOperateComm<TSet>.CheckAuthority(context, session, repo, FuncAction.Create, null)) return default;
                    TSet set = context.GetArgument<TSet>(SystemCP.Set);
                    TSet result = repo.Create(set);
                    repo.CommitData(FuncAction.Create);
                    context.Errors.AddRange(repo.Message.Errors);
                    repo.Message.WriteLogTxt();
                    return context.Errors.Count == 0 ? result : default;
                });
            Field(
                type: typeof(TSetType),
                name: nameof(repo.Update),
                description: SystemCP.DESC_Update,
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = SystemCP.KeyVal, Description = SystemCP.DESC_KeyVal }, new QueryArgument<NonNullGraphType<TInputSet>> { Name = SystemCP.Set, Description = SystemCP.DESC_Set }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = SystemCP.JWT, Description = SystemCP.DESC_JWT }),
                resolve: context =>
                {
                    object[] keyVal = context.GetArgument<object>(SystemCP.KeyVal) as object[];
                    if (!BaseOperateComm<TSet>.CheckAuthority(context, session, repo, FuncAction.Update, keyVal)) return default;
                    TSet set = context.GetArgument<TSet>(SystemCP.Set);
                    TSet result = repo.Update(keyVal, set);
                    repo.CommitData(FuncAction.Update);
                    context.Errors.AddRange(repo.Message.Errors);
                    repo.Message.WriteLogTxt();
                    return context.Errors.Count == 0 ? result : default;
                });
            Field(
                type: typeof(BooleanGraphType),
                name: nameof(repo.Delete),
                description: SystemCP.DESC_Delete,
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = SystemCP.KeyVal, Description = SystemCP.DESC_KeyVal }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = SystemCP.JWT, Description = SystemCP.DESC_JWT }),
                resolve: context =>
                {
                    object[] keyVal = context.GetArgument<object>(SystemCP.KeyVal) as object[];
                    if (!BaseOperateComm<TSet>.CheckAuthority(context, session, repo, FuncAction.Delete, keyVal)) return default;
                    repo.Delete(new[] { keyVal });
                    repo.CommitData(FuncAction.Delete);
                    context.Errors.AddRange(repo.Message.Errors);
                    repo.Message.WriteLogTxt();
                    return default;
                });
            Field(
                type: typeof(TSetType),
                name: nameof(repo.Approve),
                description: SystemCP.DESC_Approve,
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = SystemCP.KeyVal, Description = SystemCP.DESC_KeyVal }, new QueryArgument<BooleanGraphType> { Name = SystemCP.Status, Description = SystemCP.DESC_Status }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = SystemCP.JWT, Description = SystemCP.DESC_JWT }),
                resolve: context =>
                {
                    object[] keyVal = context.GetArgument<object>(SystemCP.KeyVal) as object[];
                    if (!BaseOperateComm<TSet>.CheckAuthority(context, session, repo, FuncAction.Approve, keyVal)) return default;
                    bool status = context.GetArgument<bool>(SystemCP.Status);
                    TSet result = repo.Approve(new[] { keyVal }, status);
                    repo.CommitData(FuncAction.Approve);
                    context.Errors.AddRange(repo.Message.Errors);
                    repo.Message.WriteLogTxt();
                    return context.Errors.Count == 0 ? result : default;
                });
            Field(
                type: typeof(TSetType),
                name: nameof(repo.Invalid),
                description: SystemCP.DESC_Invalid,
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = SystemCP.KeyVal, Description = SystemCP.DESC_KeyVal }, new QueryArgument<BooleanGraphType> { Name = SystemCP.Status, Description = SystemCP.DESC_Status }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = SystemCP.JWT, Description = SystemCP.DESC_JWT }),
                resolve: context =>
                {
                    object[] keyVal = context.GetArgument<object>(SystemCP.KeyVal) as object[];
                    if (!BaseOperateComm<TSet>.CheckAuthority(context, session, repo, FuncAction.Invalid, keyVal)) return default;
                    bool status = context.GetArgument<bool>(SystemCP.Status);
                    TSet result = repo.Invalid(new[] { keyVal }, status);
                    repo.CommitData(FuncAction.Invalid);
                    context.Errors.AddRange(repo.Message.Errors);
                    repo.Message.WriteLogTxt();
                    return context.Errors.Count == 0 ? result : default;
                });
            Field(
              type: typeof(TSetType),
              name: nameof(repo.EndCase),
              description: SystemCP.DESC_EndCase,
              arguments: new QueryArguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = SystemCP.KeyVal, Description = SystemCP.DESC_KeyVal }, new QueryArgument<BooleanGraphType> { Name = SystemCP.Status, Description = SystemCP.DESC_Status }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = SystemCP.JWT, Description = SystemCP.DESC_JWT }),
              resolve: context =>
              {
                  object[] keyVal = context.GetArgument<object>(SystemCP.KeyVal) as object[];
                  if (!BaseOperateComm<TSet>.CheckAuthority(context, session, repo, FuncAction.EndCase, keyVal)) return default;
                  bool status = context.GetArgument<bool>(SystemCP.Status);
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
                string typeName = $"{GetType().Namespace}.{t.Name}{SystemCP.Type}";
                string description = ResxManage.GetDescription(t);
                if (typeof(IEnumerable).IsAssignableFrom(t.PropertyType))
                    Field(typeof(ListGraphType<>).MakeGenericType(new[] { GetType().Assembly.GetType(typeName) }), t.Name, description);
                else
                    Field(GetType().Assembly.GetType(typeName), t.Name, description);
            }
        }
    }
    public class BaseInputSetGraphType<TSet> : InputObjectGraphType<TSet>
    {
        public BaseInputSetGraphType()
        {
            Name = $"{typeof(TSet).Name}Input";
            foreach (var t in typeof(TSet).GetProperties())
            {
                string typeName = $"{GetType().Namespace}.{t.Name}{SystemCP.InputType}";
                string description = ResxManage.GetDescription(t);
                if (typeof(IEnumerable).IsAssignableFrom(t.PropertyType))
                    Field(typeof(ListGraphType<>).MakeGenericType(new[] { GetType().Assembly.GetType(typeName) }), t.Name, description);
                else
                    Field(GetType().Assembly.GetType(typeName), t.Name, description);
            }
        }
    }
    #endregion

    #region Fields
    public class BaseInputFieldGraphType<TModelType> : InputObjectGraphType<TModelType>
    {
        public BaseInputFieldGraphType()
        {
            Name = typeof(TModelType).Name.Replace(SystemCP.Model, SystemCP.InputType);
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
                var fields = ((NewArrayExpression)propertyExpression.Body).Expressions;
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
            Name = typeof(TModelType).Name.Replace(SystemCP.Model, string.Empty);
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
                var fields = ((NewArrayExpression)propertyExpression.Body).Expressions;
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
        internal static bool CheckAuthority(ResolveFieldContext<object> context, ISessionWrapper session, IBasicRepository<TSet> repository, FuncAction action, object[] keyVal)
        {
            SetDebugUser(session);
            repository.Message = null;
            repository.User = session.User;
            if (CheckIsLogout(context, session, repository)) return false;
            string progId = ResxManage.GetProgId(repository);
            SysOperateLog.SetOperateLog(session.User.KeyId, session.IP, session.Browser, progId, GetPKValueString(keyVal), ResxManage.GetDescription(action), memo: string.Empty);
            if (BizAccountLogin.CheckAuthenticate(context, repository, session.SessionId, progId, action)) return false;
            return true;
        }
        /// <summary>
        /// 設置Debug的User
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="context"></param>
        internal static void SetDebugUser(ISessionWrapper session)
        {
#if DEBUG
            session.User = SystemOperator.SysOperator;
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

        private static bool CheckIsLogout(ResolveFieldContext<object> context, ISessionWrapper session, IBasicRepository<TSet> repository)
        {
            if (null == session || null == session.User)
            {
                repository.Message.AddCustErrorMessage(MessageCode.Code1020);
                context.Errors.AddRange(repository.Message.Errors);
                repository.Message.WriteLogTxt();
                return true;
            }
            return false;
        }
    }

    public class Permission : BaseQueryFieldGraphType<PermissionTokenModel> { }
    public class FileInfo : BaseInputFieldGraphType<FileInfoModel>
    {
        public FileInfo() : base()
        {
            Field(typeof(ListGraphType<IntGraphType>), "Content", "內容");
        }
    }
    #endregion
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GraphQL;
using GraphQL.Types;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;

namespace SKGPortalCore.Data
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
    public class BaseQueryType<TSet> : ObjectGraphType
    {
        public BaseQueryType(BasicRepository<TSet> repository)
        {
            Field(
                type: typeof(byte),
                name: "queryData",
                description: "查詢",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "billNo" }, new QueryArgument<StringGraphType> { Name = "jWT" }),
                resolve: context =>
                {
                    return repository.QueryData("");
                });
            Field(
                type: typeof(byte),
                name: "queryList",
                description: "查詢列表",
                arguments: null,
                resolve: context =>
                {
                    return repository.QueryData("");
                });
        }
    }
    public class BaseMutationType<TSet, TInputSet> : ObjectGraphType where TInputSet : GraphType
    {
        public BaseMutationType(BasicRepository<TSet> repository)
        {
            Field(
                type: typeof(byte),
                name: "create",
                description: "新增",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<TInputSet>> { Name = "set" }),
                resolve: context =>
                {
                    TSet set = context.GetArgument<TSet>("set");
                    TSet result= repository.Create(set);
                    repository.CommitData(FuncAction.Create);
                    return result;
                });
            Field(
                type: typeof(byte),
                name: "update",
                description: "修改",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<TInputSet>> { Name = "set" }),
                resolve: context =>
                {
                    TSet set = context.GetArgument<TSet>("set");
                    TSet result = repository.Update(set);
                    repository.CommitData(FuncAction.Update);
                    return result;
                });
            Field(
                type: typeof(byte),
                name: "delete",
                description: "刪除",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "keyVal" }),
                resolve: context =>
                {
                    string billNo = context.GetArgument<string>("billNo");
                    repository.Delete(new[] { billNo });
                    repository.CommitData(FuncAction.Delete);
                    return null;
                });
            Field(
                type: typeof(byte),
                name: "invalid",
                description: "作廢",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "billNo" }, new QueryArgument<BooleanGraphType> { Name = "status" }),
                resolve: context =>
                {
                    string billNo = context.GetArgument<string>("billNo");
                    bool status = context.GetArgument<bool>("status");
                    TSet result = repository.Invalid(new[] { billNo }, status);
                    repository.CommitData(FuncAction.Invalid);
                    return result;
                });
        }
    }
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

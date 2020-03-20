using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.MasterData.User;
using SKGPortalCore.Repository.SKGPortalCore.Business.Func;
using System;

namespace SKGPortalCore.Graph.MasterData.User
{
    //Schema
    public class BackendUserSchema : BaseSchema<BackendUserQuery, BackendUserMutation>
    {
        public BackendUserSchema(IDependencyResolver resolver) : base(resolver) { }
    }
    //Operate
    public class BackendUserQuery : BaseQueryType<BackendUserSet, BackendUserSetType, BackendUserType>
    {
        public BackendUserQuery(BackendUserRepository repository, ISessionWrapper session) : base(repository, session)
        {
            Field(
                    type: typeof(ListGraphType<Permission>),
                    name: "Login",
                    description: "登錄帳號",
                    arguments: new QueryArguments(
                        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "account", Description = "帳號" },
                        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "pasuwado", Description = "密碼" }
                    ),
                    resolve: context =>
                    {
                        return repository.Login(session, context.GetArgument<string>("account"), context.GetArgument<string>("pasuwado"));
                    });
        }
    }
    public class BackendUserMutation : BaseMutationType<BackendUserSet, BackendUserSetType, BackendUserSetInputType>
    {
        public BackendUserMutation(BackendUserRepository repository) : base(repository) { }
    }
    //Input
    public class BackendUserSetInputType : BaseInputSetGraphType<BackendUserSet> { }
    public class BackendUserInputType : BaseInputFieldGraphType<BackendUserModel> { }
    public class BackendUserRoleInputType : BaseInputFieldGraphType<BackendUserRoleModel> { }
    //Query
    public class BackendUserSetType : BaseQuerySetGraphType<BackendUserSet> { }
    public class BackendUserType : BaseQueryFieldGraphType<BackendUserModel> { }
    public class BackendUserRoleType : BaseQueryFieldGraphType<BackendUserRoleModel> { }




}

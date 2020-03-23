using GraphQL;
using GraphQL.Types;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.MasterData.User;

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
                    description: "登入帳號",
                    arguments: new QueryArguments(
                        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "account", Description = "帳號" },
                        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "pasuwado", Description = "密碼" }
                    ),
                    resolve: context =>
                    {
                        return repository.Login(session, context.GetArgument<string>("account"), context.GetArgument<string>("pasuwado"));
                    });
            Field(
                    type: typeof(ListGraphType<Permission>),
                    name: "Logout",
                    description: "登出帳號",
                    resolve: context =>
                    {
                        repository.Logout(session);
                        return null;
                    });

        }
    }
    public class BackendUserMutation : BaseMutationType<BackendUserSet, BackendUserSetType, BackendUserSetInputType>
    {
        public BackendUserMutation(BackendUserRepository repository, ISessionWrapper session) : base(repository, session) { }
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

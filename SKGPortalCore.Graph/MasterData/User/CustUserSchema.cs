using GraphQL;
using GraphQL.Types;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.MasterData.User;

namespace SKGPortalCore.Graph.MasterData.User
{
    //Schema
    public class CustUserSchema : BaseSchema<CustUserQuery, CustUserMutation>
    {
        public CustUserSchema(IDependencyResolver resolver) : base(resolver) { }
    }
    //Operate
    public class CustUserQuery : BaseQueryType<CustUserSet, CustUserSetType, CustUserType>
    {
        public CustUserQuery(CustUserRepository repository, ISessionWrapper session) : base(repository, session)
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
    public class CustUserMutation : BaseMutationType<CustUserSet, CustUserSetType, CustUserSetInputType>
    {
        public CustUserMutation(CustUserRepository repository) : base(repository) { }
    }
    //Input
    public class CustUserSetInputType : BaseInputSetGraphType<CustUserSet> { }
    public class CustUserInputType : BaseInputFieldGraphType<CustUserModel> { }
    public class CustUserRoleInputType : BaseInputFieldGraphType<CustUserRoleModel> { }
    //Query
    public class CustUserSetType : BaseQuerySetGraphType<CustUserSet> { }
    public class CustUserType : BaseQueryFieldGraphType<CustUserModel> { }
    public class CustUserRoleType : BaseQueryFieldGraphType<CustUserRoleModel> { }
}

using GraphQL;
using GraphQL.Types;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
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
                    name: SystemCP.DESC_Login,
                    description: SystemCP.DESC_Login,
                    arguments: new QueryArguments(
                        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = SystemCP.DESC_Account, Description =SystemCP.DESC_Account },
                        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = SystemCP.DESC_Pasuwado, Description = SystemCP.DESC_Pasuwado }
                    ),
                    resolve: context =>
                    {
                        return repository.Login(session, context.GetArgument<string>(SystemCP.DESC_Account), context.GetArgument<string>(SystemCP.DESC_Pasuwado));
                    });
            Field(
                    type: typeof(ListGraphType<Permission>),
                    name: SystemCP.DESC_Logout,
                    description: SystemCP.DESC_Logout,
                    resolve: context =>
                    {
                        repository.Logout(session);
                        return null;
                    });
        }
    }
    public class CustUserMutation : BaseMutationType<CustUserSet, CustUserSetType, CustUserSetInputType>
    {
        public CustUserMutation(CustUserRepository repository, ISessionWrapper session) : base(repository, session) { }
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

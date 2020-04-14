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
                    name: SystemCP.GQL_Login,
                    description: ResxManage.GetDescription(SystemCP.GQL_Login),
                    arguments: new QueryArguments(
                        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = SystemCP.GQL_Account, Description = ResxManage.GetDescription(SystemCP.GQL_Account) },
                        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = SystemCP.GQL_Pasuwado, Description = ResxManage.GetDescription(SystemCP.GQL_Pasuwado) }
                    ),
                    resolve: context =>
                    {
                        return repository.Login(session, context.GetArgument<string>(SystemCP.GQL_Account), context.GetArgument<string>(SystemCP.GQL_Pasuwado));
                    });
            Field(
                    type: typeof(ListGraphType<Permission>),
                    name: SystemCP.GQL_Logout,
                    description: ResxManage.GetDescription(SystemCP.GQL_Logout),
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

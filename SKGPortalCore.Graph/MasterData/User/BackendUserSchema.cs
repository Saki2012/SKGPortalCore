using GraphQL;
using GraphQL.Types;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.MasterData.User;
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
                    name: SystemCP.DESC_Login,
                    description: SystemCP.DESC_Login,
                    arguments: new QueryArguments(
                        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = SystemCP.DESC_Account, Description = SystemCP.DESC_Account },
                        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = SystemCP.DESC_Pasuwado, Description = SystemCP.DESC_Pasuwado }
                    ),
                    resolve: context =>
                    {
                        return repository.Login(session, context.GetArgument<string>(SystemCP.DESC_Account), context.GetArgument<string>(SystemCP.DESC_Pasuwado));
                    }
                    );
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

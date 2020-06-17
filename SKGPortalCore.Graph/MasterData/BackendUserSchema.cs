using GraphQL.Types;
using SKGPortalCore.Core;
using SKGPortalCore.Core.GraphQL;
using SKGPortalCore.Core.LibAttribute;
using SKGPortalCore.Core.LibEnum;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Interface.IGraphQL.MasterData;
using SKGPortalCore.Interface.IRepository.MasterData;

namespace SKGPortalCore.Graph.MasterData
{
    //Schema
    [EndPoint(EndType.Backend)]
    public class BackendUserSchema : BaseSchema<BackendUserSet, BackendUserQuery, BackendUserMutation>, IUserSchema
    {
        public BackendUserSchema(IBackendUserRepository repo, ISessionWrapper session) : base(repo, session) { }
    }
    //Operate
    public class BackendUserQuery : BaseQueryType<BackendUserSet, BackendUserSetType, BackendUserType>
    {
        public BackendUserQuery(IBackendUserRepository repo, ISessionWrapper session) : base(repo, session)
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
                        return repo.Login(session, context.GetArgument<string>(SystemCP.DESC_Account), context.GetArgument<string>(SystemCP.DESC_Pasuwado));
                    }
                    );
            Field(
                    type: typeof(ListGraphType<Permission>),
                    name: SystemCP.DESC_Logout,
                    description: SystemCP.DESC_Logout,
                    resolve: context =>
                    {
                        repo.Logout(session);
                        return null;
                    });

        }
    }
    public class BackendUserMutation : BaseMutationType<BackendUserSet, BackendUserSetType, BackendUserSetInputType>
    {
        public BackendUserMutation(IBackendUserRepository repo, ISessionWrapper session) : base(repo, session) { }
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

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
    [EndPoint(EndType.Frontend)]
    public class CustUserSchema : BaseSchema<CustUserSet, CustUserQuery, CustUserMutation>, IUserSchema
    {
        public CustUserSchema(ICustUserRepository repo, ISessionWrapper session) : base(repo, session) { }
    }
    //Operate
    public class CustUserQuery : BaseQueryType<CustUserSet, CustUserSetType, CustUserType>
    {
        public CustUserQuery(ICustUserRepository repo, ISessionWrapper session) : base(repo, session)
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
                    });
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
    public class CustUserMutation : BaseMutationType<CustUserSet, CustUserSetType, CustUserSetInputType>
    {
        public CustUserMutation(ICustUserRepository repo, ISessionWrapper session) : base(repo, session) { }
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

using SKGPortalCore.Core;
using SKGPortalCore.Core.GraphQL;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Interface.IGraphQL.MasterData;
using SKGPortalCore.Interface.IRepository.MasterData;

namespace SKGPortalCore.Graph.MasterData
{
    //Schema
    public class RoleSchema : BaseSchema<RoleSet, RoleQuery, RoleMutation>, IRoleSchema
    {
        public RoleSchema(IRoleRepository repo, ISessionWrapper session) : base(repo, session) { }
    }
    public class RoleMutation : BaseMutationType<RoleSet, RoleSetType, RoleSetInputType>
    {
        public RoleMutation(IRoleRepository repository, ISessionWrapper session) : base(repository, session) { }
    }
    //Input
    public class RoleSetInputType : BaseInputSetGraphType<RoleSet> { }
    public class RoleInputType : BaseInputFieldGraphType<RoleModel> { }
    public class RolePermissionInputType : BaseInputFieldGraphType<RolePermissionModel> { }
    //Query
    public class RoleSetType : BaseQuerySetGraphType<RoleSet> { }
    public class RoleType : BaseQueryFieldGraphType<RoleModel> { }
    public class RolePermissionType : BaseQueryFieldGraphType<RolePermissionModel> { }
}

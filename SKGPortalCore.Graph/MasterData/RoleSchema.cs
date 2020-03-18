using GraphQL;
using GraphQL.Types;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.Graph.MasterData
{
    //Schema
    public class RoleSchema : BaseSchema<RoleQuery, RoleMutation>
    {
        public RoleSchema(IDependencyResolver resolver) : base(resolver) { }
    }
    //Operate
    public class RoleQuery : BaseQueryType<RoleSet, RoleSetType, RoleType>
    {
        public RoleQuery(RoleRepository repository, ISessionWrapper session) : base(repository, session) { }
    }
    public class RoleMutation : BaseMutationType<RoleSet, RoleSetType, RoleSetInputType>
    {
        public RoleMutation(RoleRepository repository) : base(repository) { }
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

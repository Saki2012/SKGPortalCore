using System;
using System.Collections.Generic;
using System.Text;
using GraphQL;
using GraphQL.Types;
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
    public class RoleQuery : BaseQueryType<RoleSet, RoleSetType>
    {
        public RoleQuery(RoleRepository repository) : base(repository) { }
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
    public class RoleSetType : BaseQuerySetGraphType<RoleSet>
    {
        public RoleSetType()
        {
            Field<RoleType>("Role");
            Field<ListGraphType<RolePermissionType>>("RolePermission");
        }
    }
    public class RoleType : BaseQueryFieldGraphType<RoleModel> { }
    public class RolePermissionType : BaseQueryFieldGraphType<RolePermissionModel> { }
}

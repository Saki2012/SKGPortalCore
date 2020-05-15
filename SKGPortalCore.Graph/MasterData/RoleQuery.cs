using SKGPortalCore.Core;
using SKGPortalCore.Core.GraphQL;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Interface.IRepository.MasterData;

namespace SKGPortalCore.Graph.MasterData
{
    //Operate
    public class RoleQuery : BaseQueryType<RoleSet, RoleSetType, RoleType>
    {
        public RoleQuery(IRoleRepository repository, ISessionWrapper session) : base(repository, session) { }
    }
}

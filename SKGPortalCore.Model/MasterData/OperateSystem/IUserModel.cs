using System.Collections.Generic;

namespace SKGPortalCore.Model.MasterData.OperateSystem
{
    public interface IUserModel
    {
        string KeyId { get; set; }
    }
    public interface IRoleModel
    {
        RoleModel Role { get; set; }
        string RoleId { get; set; }
    }
}

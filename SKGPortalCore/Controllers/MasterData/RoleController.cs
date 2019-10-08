using GraphQL;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Data;
using SKGPortalCore.Graph.MasterData;

namespace SKGPortalCore.Controllers.MasterData
{
    [Route("[Controller]")]
    public class RoleController : BaseController
    {
        public RoleController(IDocumentExecuter documentExecuter, RoleSchema schema, ISessionWapper sessionWapper) : base(documentExecuter, schema, sessionWapper) { }
    }
}
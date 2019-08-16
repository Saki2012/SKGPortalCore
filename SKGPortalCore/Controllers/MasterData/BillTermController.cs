using GraphQL;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Data;
using SKGPortalCore.Graph.MasterData;

namespace SKGPortalCore.Controllers.MasterData
{
    [Route("[Controller]")]
    public class BillTermController : BaseController
    {
        public BillTermController(IDocumentExecuter documentExecuter, BillTermSchema schema, ISessionWapper sessionWapper) : base(documentExecuter, schema, sessionWapper) { }
    }
}
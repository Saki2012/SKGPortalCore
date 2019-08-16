using GraphQL;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Data;
using SKGPortalCore.Graph.MasterData;

namespace SKGPortalCore.Controllers.MasterData
{
    [Route("[Controller]")]
    public class CollectionTypeController : BaseController
    {
        public CollectionTypeController(IDocumentExecuter documentExecuter, CollectionTypeSchema schema, ISessionWapper sessionWapper) : base(documentExecuter, schema, sessionWapper) { }
    }
}
using GraphQL;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Data;
using SKGPortalCore.Graph.MasterData;

namespace SKGPortalCore.Controllers.MasterData
{
    [Route("[Controller]")]
    public class ChannelController : BaseController
    {
        public ChannelController(IDocumentExecuter documentExecuter, ChannelSchema schema, ISessionWapper sessionWapper) : base(documentExecuter, schema, sessionWapper) { }
    }
}
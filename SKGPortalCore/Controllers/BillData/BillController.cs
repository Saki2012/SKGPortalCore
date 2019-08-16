using GraphQL;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Data;
using SKGPortalCore.Graph.BillData;

namespace SKGPortalCore.Controllers.BillData
{
    [Route("[Controller]")]
    public class BillController : BaseController
    {
        public BillController(IDocumentExecuter documentExecuter, BillSchema schema, ISessionWapper sessionWapper) : base(documentExecuter, schema, sessionWapper) { }
    }
}

using GraphQL;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Data;
using SKGPortalCore.Graph.MasterData;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SKGPortalCore.Controllers.MasterData
{
    [Route("[Controller]")]
    public class CustomerController : BaseController
    {
        public CustomerController(IDocumentExecuter documentExecuter, CustomerSchema schema, ISessionWapper sessionWapper) : base(documentExecuter, schema, sessionWapper) { }
    }
}

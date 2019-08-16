using GraphQL;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Data;
using SKGPortalCore.Graph.BillData;

namespace SKGPortalCore.Controllers.BillData
{
    [Route("[Controller]")]
    public class ReceiptBillController : BaseController
    {
        public ReceiptBillController(IDocumentExecuter documentExecuter, ReceiptBillSchema schema, ISessionWapper sessionWapper) : base(documentExecuter, schema, sessionWapper) { }
    }
}
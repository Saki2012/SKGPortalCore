using GraphQL;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Data;
using SKGPortalCore.Graph.MasterData;

namespace SKGPortalCore.Controllers.MasterData
{
    #region Controller
    [Route("[Controller]")]
    public class PayerController : BaseController
    {


        #region Constructor
        public PayerController(IDocumentExecuter documentExecuter, PayerSchema schema, ISessionWapper sessionWapper) : base(documentExecuter, schema, sessionWapper)
        {

        }
        #endregion


    }
    #endregion




}
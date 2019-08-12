using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData.OperateSystem;
using System;
using System.Threading.Tasks;

namespace SKGPortalCore.Controllers
{
    public class BaseController : Controller
    {
        #region Property
        protected readonly ISessionWapper _sessionWapper;
        protected readonly ISchema _schema;
        protected readonly IDocumentExecuter _documentExecuter;
        #endregion
        #region Constructor
        public BaseController(IDocumentExecuter documentExecuter, ISchema schema, ISessionWapper sessionWapper)
        {
            _sessionWapper = sessionWapper;
            _schema = schema;
            _documentExecuter = documentExecuter;
        }
        #endregion
        #region Public
        [HttpPost]
        public virtual async Task<IActionResult> PostAsync([FromBody]GraphQLQuery query)
        {
            if (null == query) { throw new ArgumentNullException(nameof(query)); }
            ExecutionOptions options = new ExecutionOptions()
            {
                Schema = _schema,
                ValidationRules = null,
                Query = query.Query,
                UserContext = _sessionWapper,
                Inputs = query.Variables?.ToInputs(),
                ExposeExceptions = true,
            };
            var result = await _documentExecuter.ExecuteAsync(options);
            if (result.Errors?.Count > 0) { return BadRequest(result.Errors); }
            return Ok(result);
        }
        #endregion
    }
}
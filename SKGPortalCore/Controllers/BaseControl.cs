﻿using System;
using System.Text;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Data;
using SKGPortalCore.Model;

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
            ExecutionResult result = await _documentExecuter.ExecuteAsync(options);
            if (result.Errors?.Count > 0) { return BadRequest(/*result.Errors*/GetErrorsMessage(result.Errors)); }
            return Ok(result);
        }
        #endregion
        #region Private
        private string GetErrorsMessage(ExecutionErrors errors)
        {
            StringBuilder str = new StringBuilder();
            foreach (ExecutionError er in errors)
            {
                str.AppendLine(er.Message);
            }

            return str.ToString();
        }
        #endregion
    }
}
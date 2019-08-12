using GraphQL;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Model;
using System;
using System.Threading.Tasks;

namespace SKGPortalCore.Controllers
{
#if DEBUG
    [Route("[controller]")]
    public class GraphQLController : Controller
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;

        public GraphQLController(ISchema schema, IDocumentExecuter documentExecuter)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
        }

        [HttpPost]
        public async Task<ExecutionResult> Post([FromBody] GraphQLQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var inputs = query.Variables.ToInputs();
            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query.Query,
                Inputs = inputs,
                ValidationRules = DocumentValidator.CoreRules()
            };

            var result = await _documentExecuter
                .ExecuteAsync(executionOptions);

            if (result.Errors?.Count > 0)
            {
                return result;
            }

            return result;
        }
    }
#endif
}
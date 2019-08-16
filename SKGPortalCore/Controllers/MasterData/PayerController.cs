using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Business.Func;
using SKGPortalCore.Data;
using SKGPortalCore.Graph.MasterData;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

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
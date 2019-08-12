using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Business.Func;
using SKGPortalCore.Data;
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
    public class BillTermController : BaseController
    {
        #region Constructor
        public BillTermController(IDocumentExecuter documentExecuter, BillTermSchema schema, ISessionWapper sessionWapper) : base(documentExecuter, schema, sessionWapper)
        {
        }
        #endregion
    }
    #endregion

    #region GraphQL
    public class BillTermSchema : BaseSchema<BillTermQuery, BillTermMutation>
    {
        public BillTermSchema(IDependencyResolver resolver) : base(resolver) { }
    }
    public class BillTermQuery : BaseQueryType<BillTermSet>
    {
        public BillTermQuery(BillTermRepository repo) : base(repo)
        {
        }
    }
    public class BillTermMutation : BaseMutationType<BillTermSet, BillTermSetInputType>
    {
        public BillTermMutation(BillTermRepository repo) : base(repo)
        {
        }
    }

    public class BillTermSetInputType : InputObjectGraphType<BillTermSet>
    {
        public BillTermSetInputType()
        {
            Field<BillTermType>(" BillTerm");
            Field<ListGraphType<BillTermDetailType>>(" BillTermDetail");
        }
    }
    public class BillTermInputType : BaseInputFieldGraphType<BillTermModel> { }
    public class BillTermDetailInputType : BaseInputFieldGraphType<BillTermDetailModel> { }

    public class BillTermSetType : ObjectGraphType<BillTermSet>
    {
        public BillTermSetType()
        {
            Field<BillTermType>(" BillTerm");
            Field<ListGraphType<BillTermDetailType>>(" BillTermDetail");
        }
    }
    public class BillTermType : BaseQueryFieldGraphType<BillTermModel> { }
    public class BillTermDetailType : BaseQueryFieldGraphType<BillTermDetailModel> { }
    #endregion

    #region Repository
    public class BillTermRepository : BasicRepository<BillTermSet>
    {
        public BillTermRepository(ApplicationDbContext db) : base(db) { }
    }
    #endregion
}